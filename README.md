# Flowsy Repository Sql
Repository implementation for SQL databases.

## Description
This package is an implementation of the abstractions defined in [Flowsy Repository Core](https://www.nuget.org/packages/Flowsy.Repository.Core)
and offers a simple way of creating repositories targeting SQL databases, designed to perform their tasks through stored routines of the underlying database.

To achieve this goal, the only requirement is to keep our database aligned to certain conventions, which is, by any means, necessary even for projects not using this package.

These conventions can be customized and set as the default values for all the repositories of our applications, but also set individually for every repository.

## Key Concepts
* A repository handles a single type of entity.
* A repository can perform some named actions on the entities stored in the underlying database: Create, Update, Patch, GetById, etc.
* A repository expects certain stored routines to exist in order to perform its actions on the underlying database.
* A repository will determine the name of the routines to execute based on the name of the entity and certain configurable naming conventions.
* The DbRepository class is intended to be the base class of the repositories needed by the application.
  * It implements the basic functionality to execute queries.
  * Its methods are declared as virtual, so any subclass can customize the behavior as needed.
  * Its methods supporting pagination expect the underlying routines to receive two special parameters named **offset** and **limit**.
* Flowsy Repository Sql relies on [Dapper](https://www.nuget.org/packages/Dapper) to perform queries and map database records to entities.


## Quick Start

## 1. Configuration
The simplest way to configure all the conventions at once is to call the DbRepositoryConfiguration.Build method.
The only requirement would be to keep the same naming conventions throughout all of our database objects.
```csharp
DbRepositoryConfiguration
  .Build()
  .Default(new DbRepositoryConfiguration(
      connectionKey: "Default", // The configuration to establish database connections will be resolved by a key named "Default"
      schemaName: "public", // The stored routines are in the 'public' schema
      resolveIdentityPropertyName: entityType => $"{entityType.Name}Id", // Customer -> CustomerId
      autoIdentity: true, // Auto increment primary keys
      routineConvention: new DbRoutineConvention(
        DbRoutineType.StoredFunction,
        NamingConvention.LowerSnakeCase,
        "fn_", // fn_ prefix for stored routines
        string.Empty
        ),
      parameterConvention: new DbRoutineParameterConvention(
          NamingConvention.LowerSnakeCase, // Parameter names in lower snake case
          "p_", // p_ prefix for stored routine parameters
          string.Empty, // No suffix
          (routineName, parameterName, value, routineType) => routineType switch
          {
              DbRoutineType.StoredFunction => $"{parameterName} => @{parameterName}",
              _ => $"@{parameterName}"
          }
      ),
      enumConvention: new DbEnumConvention(DbEnumFormat.Name, NamingConvention.PascalCase), // Use the string representation instead of the ordinal value for enums when executing queries
      actions: DbRepositoryActionSet
        .CreateBuilder()
        // The action used to create entities will be named 'Insert' instead of the 'Create' (default).
        // Stored function example: fn_customer_insert (instead of fn_customer_create)
        .Create(new DbRepositoryAction("Insert"))
        // If not configured, all other actions will use a default name
        .Build()
  ))
  .ForType(typeof(MyRepository), new DbRepositoryConfiguration( /* ... */)) // Configuration for a specific type of repository
  .WithColumnMapping(NamingConvention.LowerSnakeCase, typeof(MyEntity).Assembly) // Database column names in lower snake case
  .WithDateOnlyTypeHandlers() // Support for date-only columns
  .WithTimeOnlyTypeHandlers(); // Support for time-only columns
```
All the parameters not set in the DbRepositoryConfiguration constructor for a specific repository type, will fallback to the default settings.

At a high level, the stored routines to be invoked by the repositories are represented by instances of DbRepositoryAction.
The DbRepositoryActionSet class has a property named Default that will be used as a fallback when no conventions are set for a specific action.

The DbRepositoryActionSet.Default property is set as follows:
```csharp
public static DbRepositoryActionSet Default { get; }
  = CreateBuilder()
      .Create(new DbRepositoryAction("Create"))
      .Update(new DbRepositoryAction("Update"))
      .Patch(new DbRepositoryAction("Patch"))
      .DeleteById(new DbRepositoryAction("DeleteById"))
      .DeleteMany(new DbRepositoryAction("DeleteMany"))
      .GetById(new DbRepositoryAction("SimGetById"))
      .GetByIdTranslated(new DbRepositoryAction("SimTrGetById"))
      .GetByIdExtended(new DbRepositoryAction("ExtGetById"))
      .GetByIdExtendedTranslated(new DbRepositoryAction("ExtTrGetById"))
      .GetOne(new DbRepositoryAction("SimGetOne"))
      .GetOneTranslated(new DbRepositoryAction("SimTrGetOne"))
      .GetOneExtended(new DbRepositoryAction("ExtGetOne"))
      .GetOneExtendedTranslated(new DbRepositoryAction("ExtTrGetOne"))
      .GetMany(new DbRepositoryAction("ExtGetMany"))
      .GetManyPaged(new DbRepositoryAction("SimGetManyPaged"))
      .GetManyTranslated(new DbRepositoryAction("SimTrGetMany"))
      .GetManyTranslatedPaged(new DbRepositoryAction("SimTrGetManyPaged"))
      .GetManyExtended(new DbRepositoryAction("ExtGetMany"))
      .GetManyExtendedPaged(new DbRepositoryAction("ExtGetManyPaged"))
      .GetManyExtendedTranslated(new DbRepositoryAction("ExtTrGetMany"))
      .GetManyExtendedTranslatedPaged(new DbRepositoryAction("ExtTrGetManyPaged"))
      .Build();
``` 
The action names will be translated automatically to stored routine names using the configured naming conventions.


## 2. Domain Modeling
The only requirement for our entities is to implement IEntity.
The IEntity interface has no methods and is used only to mark classes within an assembly to be treated as entities of a repository.

```csharp
// Customer.cs
using Flowsy.Repository.Core;

public class Customer : IEntity
{
    // Basic customer information
    
    public int CustomerId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; } 
}

// Payment.cs
using Flowsy.Repository.Core;

public class Payment : IEntity
{
    // Information of every payment made by a customer

    public long PaymentId { get; set; }
    public int CustomerId { get; set; }
    
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

// CustomerExtended.cs
using Flowsy.Repository.Core;

public class CustomerExtended : Customer
{
     // All the basic information of a customer plus a total paid computed in some query
     public decimal TotalPaid { get; set; }
} 
```

## 3. Define Interfaces
```csharp
// ICustomerRepository.cs
using Flowsy.Repository.Core;

// ICustomerRepository inherits all the methods of IRepository
// and adds the specific methods required to manage Customer entities 
public interface ICustomerRepository : IRepository<Customer, int>
{  
    Task<T?> GetOneExtendedAsync<T>(string email, CancellationToken cancellationToken);
}

// IPaymentRepository.cs
using Flowsy.Repository.Core;

// IPaymentRepository inherits all the methods of IRepository 
public interface IPaymentRepository : IRepository<Payment, long>
{
}
```

## 4. Implementation Details
```csharp
// CustomerRepository.cs
// By inheriting from DbRepository, CustomerRepository gets the basic implementation
// for create, update, patch, delete, and find entities with no additional code.
// The only methods to be implemented are the one defined in ICustomerRepository. 
public class CustomerRepository : DbRepository<Customer, int>, ICustomerRepository
{
    public CustomerRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public CustomerRepository(IDbTransaction transaction) : base(transaction)
    {
    }

    // Search by email and return an object of type CustomerExtended.
    public Task<CustomerExtended?> GetOneExtendedAsync(string email, CancellationToken cancellationToken)
    {
        return GetOneExtendedAsync<CustomerExtended>(email, cancellationToken);
    }

    // Search by email and return and object of type T which shall expose the same properties as CustomerExtended.  
    public Task<T?> GetOneExtendedAsync<T>(string email, CancellationToken cancellationToken)
    {
        // The following version of the GetOneExtendedAsync method is inherited from DbRepository and accepts a dynamic criteria as argument.
        // This version of the method will resolve the name and parameters of the stored routine to execute based on the configuration set for this repository or globally for all repositories:
        // public.fn_customer_ext_get_one(p_email text)
        // We need to create that stored routine in our database, which shall execute a query to join the required tables and return the expected result.
        return GetOneExtendedAsync<T>(
            new 
            { 
                Email = email
            },
            cancellationToken
            );
    }
}

// PaymentRepository.cs
// In this example, only two constructors are needed to have a fully-functional repository able to
// create, update, delete and retrieve entities using all the functionallity inherited from DbRepository.
public class PaymentRepository : DbRepository<Payment, long>, IPaymentRepository
{
    public PaymentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public PaymentRepository(IDbTransaction transaction) : base(transaction)
    {
    }
}
```

## 5. Using the Repositories
The following examples assume our application is using [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

### 5.1 Configure a Connection Factory
All the repositories can be instantiated by the dependency injection system using a single connection factory.

```csharp
builder.Services.AddSingleton<IDbConnectionFactory>(serviceProvider => {
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionConfigurations = new List<DbConnectionConfiguration>();
    // Populate connection configuration list from the IConfiguration instance
    return new DbConnectionFactory(connectionConfigurations.ToArray());
    });
```

### 5.2. Create a customer
````csharp
public class CreateCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    
    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<CreateCustomerCommandResult> Handle(CreateCustomerCommand command, CancellationToken)
    {
        var customer = new Customer();
        // Populate customer from command
        
        var customerId = await _customerRepository.CreateAsync(customer, cancellationToken);
        
        return new CreateCustomerCommandResult(customerId);
    } 
}
````

### 5.3. Search customer by email to see their total paid.
```csharp
public class CustomerByEmailQueryHandler
{
    private readonly ICustomerRepository _customerRepository;
    
    public CustomerByEmailQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<IEnumerable<CustomerExtended?>> Handle(CustomerByEmailQuery query, CancellationToken)
    {
        return _customerRepository.GetOneExtendedAsync(query.Email, cancellationToken);
    } 
}
```

### 5.4. Unit of Work
The class DbUnitOfWork implements the interface IUnitOfWork defined in the package [Flowsy Repository Core](https://www.nuget.org/packages/Flowsy.Repository.Core).
To create units of work, we can think about a specific use case and see what repositories are involved to complete the task.

For example, to create an invoice we also need to create invoice items inside an atomic operation, so first we can define the following interface:
```csharp
using Flowsy.Repository.Core;

public interface ICreateInvoiceUnitOfWork : IUnitOfWork
{
    IInvoiceRepository InvoiceRepository { get; }
    IInvoiceItemRepository InvoiceItemRepository { get; }
}
```

Then, we implement that specific unit of work:
```csharp
public class CreateInvoiceUnitOfWork : DbUnitOfWork
{
    private IInvoiceRepository? _invoiceRepository;
    private IInvoiceItemRepository? _invoiceItemRepository;
    
    public CreateInvoiceUnitOfWork(IDbConnection connection) : base(connection)
    {
    }
    
    public IInvoiceRepository InvoiceRepository
        => _invoiceRepository ??= new InvoiceRepository(Transaction);
    
    public IInvoiceItemRepository InvoiceItemRepository
        => _invoiceItemRepository ??= new InvoiceItemRepository(Transaction);
        
    // Transaction is an instance of IDbTransaction, defined as a protected property of DbUnitOfWork  
}
```

Then, we create a unit of work factory to create instances of CreateInvoiceUnitOfWork as required:
```csharp
using Flowsy.Repository.Core;

public class DbUnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public DbUnitOfWorkFactory(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public T Create<T>(string dataStoreKey = "Default") where T : IUnitOfWork
    {
        IUnitOfWork? unitOfWork = null;
        
        var type = typeof(T);
        
        // Create IUnitOfWork instance
        if (type == typeof(ICreateInvoiceUnitOfWork))
            unitOfWork = new CreateInvoiceUnitOfWork(_dbConnectionFactory.GetConnection(dataStoreKey));

        return (T) (unitOfWork ?? throw new NotSupportedException());
    }
}
```

And finally the application can execute a command that safely runs a transaction without needing to know what kind of database or fancy mechanisms were used to do the job.
```csharp
public class CreateInvoiceCommandHandler
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    
    public CreateInvoiceCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<CreateInvoiceCommandResult> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        // Begin operation
        // IUnitOfWork inherits from IDisposable and IAsyncDisposable, if any exception is thrown, the current operation shall be rolled back
        await using var unitOfWork = _unitOfWorkFactory.Create<ICreateInvoiceUnitOfWork>();

        var invoice = new Invoice();
        // Populate invoice object from properties of command object 
        
        // Create the Invoice entity
        var invoiceId = await unitOfWork.InvoiceRepository.CreateAsync(invoice, cancellationToken);
        
        // Create all the InvoiceItem entities
        foreach (var item in command.Items)
        {
            var invoiceItem = new InvoiceItem();
            // Populate invoiceItem object from properties of item object
            
            // Create each InvoiceItem entity
            await unitOfWork.InvoiceItemRepository.CreateAsync(invoiceItem, cancellationToken); 
        }

        // Save the current operation        
        await unitOfWork.SaveAsync(cancellationToken);
        
        // If something goes wrong, unitOfWork.UndoAsync() will be invoked automatically when unitOfWork is disposed 
        
        // Return the result of the operation
        return new CreateInvoiceCommandResult
        {
            InvoiceId = invoiceId
        };
    }
}
```
