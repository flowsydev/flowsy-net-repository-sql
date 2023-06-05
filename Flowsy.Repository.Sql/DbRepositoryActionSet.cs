namespace Flowsy.Repository.Sql;

public class DbRepositoryActionSet
{
    public static DbRepositoryActionSetBuilder CreateBuilder() => new ();

    public static DbRepositoryActionSet Default { get; set; }
        = CreateBuilder()
            .Create(new DbRepositoryAction("Create"))
            .Update(new DbRepositoryAction("Update"))
            .Patch(new DbRepositoryAction("Patch"))
            .DeleteById(new DbRepositoryAction("DeleteById"))
            .DeleteMany(new DbRepositoryAction("DeleteMany"))
            .GetById(new DbRepositoryAction("GetSimpleById"))
            .GetByIdTranslated(new DbRepositoryAction("GetSimpleTranslatedById"))
            .GetByIdExtended(new DbRepositoryAction("GetExtendedById"))
            .GetByIdExtendedTranslated(new DbRepositoryAction("GetExtendedTranslatedById"))
            .GetOne(new DbRepositoryAction("GetOneSimple"))
            .GetOneTranslated(new DbRepositoryAction("GetOneSimpleTranslated"))
            .GetOneExtended(new DbRepositoryAction("GetOneExtended"))
            .GetOneExtendedTranslated(new DbRepositoryAction("GetOneExtendedTranslated"))
            .GetMany(new DbRepositoryAction("GetManySimple"))
            .GetManyPaged(new DbRepositoryAction("GetManySimplePaged"))
            .GetManyTranslated(new DbRepositoryAction("GetManySimpleTranslated"))
            .GetManyTranslatedPaged(new DbRepositoryAction("GetManySimpleTranslatedPaged"))
            .GetManyExtended(new DbRepositoryAction("GetManyExtended"))
            .GetManyExtendedPaged(new DbRepositoryAction("GetManyExtendedPaged"))
            .GetManyExtendedTranslated(new DbRepositoryAction("GetManyExtendedTranslated"))
            .GetManyExtendedTranslatedPaged(new DbRepositoryAction("GetManyExtendedTranslatedPaged"))
            .Build();
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="create">Action used to create a new entity.</param>
    /// <param name="update">Action used to update an entity.</param>
    /// <param name="patch">Action used to patch an entity.</param>
    /// <param name="deleteById">Action used to delete an entity.Action used to patch an entity.</param>
    /// <param name="deleteMany">Action used to delete multiple entities at once.</param>
    /// <param name="getById">Action used to get an entity identified by a given value.</param>
    /// <param name="getByIdExtended">Action used to get the extended version of an entity identified by a given value.</param>
    /// <param name="getByIdExtendedTranslated">Action used to get the extended and translated version of an entity identified by a given value.</param>
    /// <param name="getByIdTranslated">Action used to get the translated version of an entity identified by a given value.</param>
    /// <param name="getOne">Action used to get a single entity matching a specified filter.</param>
    /// <param name="getOneExtended">Action used to get the extended version of a single entity matching a specified filter.</param>
    /// <param name="getOneExtendedTranslated">Action used to get the extended and translated version of a single entity matching a specified filter.</param>
    /// <param name="getOneTranslated">Action used to get the translated version of a single entity matching a specified filter.</param>
    /// <param name="getMany">Action used to get one or more entities matching a specified filter.</param>
    /// <param name="getManyPaged">Action used to get a page of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtended">Action used to get the extended version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedPaged">Action used to get a page of the extended version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedTranslated">Action used to get the extended and translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedTranslatedPaged">Action used to get a page of the extended and translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyTranslated">Action used to get the translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyTranslatedPaged">Action used to get a page of the translated version of one or more entities matching a specified filter.</param>
    public DbRepositoryActionSet(
        DbRepositoryAction? create = null,
        DbRepositoryAction? update = null,
        DbRepositoryAction? patch = null,
        DbRepositoryAction? deleteById = null,
        DbRepositoryAction? deleteMany = null,
        
        DbRepositoryAction? getById = null,
        DbRepositoryAction? getByIdExtended = null,
        DbRepositoryAction? getByIdExtendedTranslated = null,
        DbRepositoryAction? getByIdTranslated = null,
        
        DbRepositoryAction? getOne = null,
        DbRepositoryAction? getOneExtended = null,
        DbRepositoryAction? getOneExtendedTranslated = null,
        DbRepositoryAction? getOneTranslated = null,
        
        DbRepositoryAction? getMany = null,
        DbRepositoryAction? getManyPaged = null,
        DbRepositoryAction? getManyExtended = null,
        DbRepositoryAction? getManyExtendedPaged = null,
        DbRepositoryAction? getManyExtendedTranslated = null,
        DbRepositoryAction? getManyExtendedTranslatedPaged = null,
        DbRepositoryAction? getManyTranslated = null,
        DbRepositoryAction? getManyTranslatedPaged = null
        )
    {
        _create = create;
        _update = update;
        _patch = patch;
        _deleteById = deleteById;
        _deleteMany = deleteMany;
        
        _getById = getById;
        _getByIdExtended = getByIdExtended;
        _getByIdExtendedTranslated = getByIdExtendedTranslated;
        _getByIdTranslated = getByIdTranslated;
        
        _getOne = getOne;
        _getOneExtended = getOneExtended;
        _getOneExtendedTranslated = getOneExtendedTranslated;
        _getOneTranslated = getOneTranslated;

        _getMany = getMany;
        _getManyPaged = getManyPaged;
        _getManyExtended = getManyExtended;
        _getManyExtendedPaged = getManyExtendedPaged;
        _getManyExtendedTranslated = getManyExtendedTranslated;
        _getManyExtendedTranslatedPaged = getManyExtendedTranslatedPaged;
        _getManyTranslated = getManyTranslated;
        _getManyTranslatedPaged = getManyTranslatedPaged;
    }
    
    private DbRepositoryAction? _create;
    private DbRepositoryAction? _update;
    private DbRepositoryAction? _patch;
    private DbRepositoryAction? _deleteById;
    private DbRepositoryAction? _deleteMany;
    private DbRepositoryAction? _getById;
    private DbRepositoryAction? _getByIdExtended;
    private DbRepositoryAction? _getByIdExtendedTranslated;
    private DbRepositoryAction? _getByIdTranslated;
    private DbRepositoryAction? _getOne;
    private DbRepositoryAction? _getOneExtended;
    private DbRepositoryAction? _getOneExtendedTranslated;
    private DbRepositoryAction? _getOneTranslated;
    private DbRepositoryAction? _getMany;
    private DbRepositoryAction? _getManyPaged;
    private DbRepositoryAction? _getManyExtended;
    private DbRepositoryAction? _getManyExtendedPaged;
    private DbRepositoryAction? _getManyExtendedTranslated;
    private DbRepositoryAction? _getManyExtendedTranslatedPaged;
    private DbRepositoryAction? _getManyTranslated;
    private DbRepositoryAction? _getManyTranslatedPaged;

    /// <summary>
    /// Action used to create a new entity.
    /// </summary>
    public DbRepositoryAction Create { get => _create ?? Default.Create; set => _create = value; }

    /// <summary>
    /// Action used to update an entity.
    /// </summary>
    public DbRepositoryAction Update { get => _update ?? Default.Update; set => _update = value; }

    /// <summary>
    /// Action used to patch an entity.
    /// </summary>
    public DbRepositoryAction Patch { get => _patch ?? Default.Patch; set => _patch = value; }

    /// <summary>
    /// Action used to delete an entity.
    /// </summary>
    public DbRepositoryAction DeleteById { get => _deleteById ?? Default.DeleteById; set => _deleteById = value; }

    /// <summary>
    /// Action used to delete multiple entities at once.
    /// </summary>
    public DbRepositoryAction DeleteMany { get => _deleteMany ?? Default.DeleteMany; set => _deleteMany = value; }

    /// <summary>
    /// Action used to get an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetById { get => _getById ?? Default.GetById; set => _getById = value; }

    /// <summary>
    /// Action used to get the extended version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtended { get => _getByIdExtended ?? Default.GetByIdExtended; set => _getByIdExtended = value; }

    /// <summary>
    /// Action used to get the extended and translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtendedTranslated { get => _getByIdExtendedTranslated ?? Default.GetByIdExtendedTranslated; set => _getByIdExtendedTranslated = value; }

    /// <summary>
    /// Action used to get the translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdTranslated { get => _getByIdTranslated ?? Default.GetByIdTranslated; set => _getByIdTranslated = value; }

    /// <summary>
    /// Action used to get a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOne { get => _getOne ?? Default.GetOne; set => _getOne = value; }

    /// <summary>
    /// Action used to get the extended version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtended { get => _getOneExtended ?? Default.GetOneExtended; set => _getOneExtended = value; }

    /// <summary>
    /// Action used to get the extended and translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtendedTranslated { get => _getOneExtendedTranslated ?? Default.GetOneExtendedTranslated; set => _getOneExtendedTranslated = value; }

    /// <summary>
    /// Action used to get the translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneTranslated { get => _getOneTranslated ?? Default.GetOneTranslated; set => _getOneTranslated = value; }

    /// <summary>
    /// Action used to get one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetMany { get => _getMany ?? Default.GetMany; set => _getMany = value; }

    /// <summary>
    /// Action used to get a page of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyPaged { get => _getManyPaged ?? Default.GetManyPaged; set => _getManyPaged = value; }

    /// <summary>
    /// Action used to get the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtended { get => _getManyExtended ?? Default.GetManyExtended; set => _getManyExtended = value; }

    /// <summary>
    /// Action used to get a page of the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedPaged { get => _getManyExtendedPaged ?? Default.GetManyExtendedPaged; set => _getManyExtendedPaged = value; }

    /// <summary>
    /// Action used to get the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslated { get => _getManyExtendedTranslated ?? Default.GetManyExtendedTranslated; set => _getManyExtendedTranslated = value; }

    /// <summary>
    /// Action used to get a page of the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslatedPaged { get => _getManyExtendedTranslatedPaged ?? Default.GetManyExtendedTranslatedPaged; set => _getManyExtendedTranslatedPaged = value; }

    /// <summary>
    /// Action used to get the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslated { get => _getManyTranslated ?? Default.GetManyTranslated; set => _getManyTranslated = value; }

    /// <summary>
    /// Action used to get a page of the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslatedPaged { get => _getManyTranslatedPaged ?? Default.GetManyTranslatedPaged; set => _getManyTranslatedPaged = value; }
}