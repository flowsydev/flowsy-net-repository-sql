namespace Flowsy.Repository.Sql;

public class DbRepositoryActionSet
{
    public static DbRepositoryActionSet Default { get; set; } = new (
        DbRepositoryAction.Default.Create,
        DbRepositoryAction.Default.Update,
        DbRepositoryAction.Default.Patch,
        DbRepositoryAction.Default.DeleteById,
        DbRepositoryAction.Default.DeleteMany,
        DbRepositoryAction.Default.GetById,
        DbRepositoryAction.Default.GetByIdExtended,
        DbRepositoryAction.Default.GetByIdExtendedTranslated,
        DbRepositoryAction.Default.GetByIdTranslated,
        DbRepositoryAction.Default.GetOne,
        DbRepositoryAction.Default.GetOneExtended,
        DbRepositoryAction.Default.GetOneExtendedTranslated,
        DbRepositoryAction.Default.GetOneTranslated,
        DbRepositoryAction.Default.GetMany,
        DbRepositoryAction.Default.GetManyPaged,
        DbRepositoryAction.Default.GetManyExtended,
        DbRepositoryAction.Default.GetManyExtendedPaged,
        DbRepositoryAction.Default.GetManyExtendedTranslated,
        DbRepositoryAction.Default.GetManyExtendedTranslatedPaged,
        DbRepositoryAction.Default.GetManyTranslated,
        DbRepositoryAction.Default.GetManyTranslatedPaged
        );
    
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
        DbRepositoryAction? create = default,
        DbRepositoryAction? update = default,
        DbRepositoryAction? patch = default,
        DbRepositoryAction? deleteById = default,
        DbRepositoryAction? deleteMany = default,
        
        DbRepositoryAction? getById = default,
        DbRepositoryAction? getByIdExtended = default,
        DbRepositoryAction? getByIdExtendedTranslated = default,
        DbRepositoryAction? getByIdTranslated = default,
        
        DbRepositoryAction? getOne = default,
        DbRepositoryAction? getOneExtended = default,
        DbRepositoryAction? getOneExtendedTranslated = default,
        DbRepositoryAction? getOneTranslated = default,
        
        DbRepositoryAction? getMany = default,
        DbRepositoryAction? getManyPaged = default,
        DbRepositoryAction? getManyExtended = default,
        DbRepositoryAction? getManyExtendedPaged = default,
        DbRepositoryAction? getManyExtendedTranslated = default,
        DbRepositoryAction? getManyExtendedTranslatedPaged = default,
        DbRepositoryAction? getManyTranslated = default,
        DbRepositoryAction? getManyTranslatedPaged = default
        )
    {
        Create = create ?? DbRepositoryAction.Default.Create;
        Update = update ?? DbRepositoryAction.Default.Update;
        Patch = patch ?? DbRepositoryAction.Default.Patch;
        DeleteById = deleteById ?? DbRepositoryAction.Default.DeleteById;
        DeleteMany = deleteMany ?? DbRepositoryAction.Default.DeleteMany;
        
        GetById = getById ?? DbRepositoryAction.Default.GetById;
        GetByIdExtended = getByIdExtended ?? DbRepositoryAction.Default.GetByIdExtended;
        GetByIdExtendedTranslated = getByIdExtendedTranslated ?? DbRepositoryAction.Default.GetByIdExtendedTranslated;
        GetByIdTranslated = getByIdTranslated ?? DbRepositoryAction.Default.GetByIdTranslated;
        
        GetOne = getOne ?? DbRepositoryAction.Default.GetOne;
        GetOneExtended = getOneExtended ?? DbRepositoryAction.Default.GetOneExtended;
        GetOneExtendedTranslated = getOneExtendedTranslated ?? DbRepositoryAction.Default.GetOneExtendedTranslated;
        GetOneTranslated = getOneTranslated ?? DbRepositoryAction.Default.GetOneTranslated;

        GetMany = getMany ?? DbRepositoryAction.Default.GetMany;
        GetManyPaged = getManyPaged ?? DbRepositoryAction.Default.GetManyPaged;
        GetManyExtended = getManyExtended ?? DbRepositoryAction.Default.GetManyExtended;
        GetManyExtendedPaged = getManyExtendedPaged ?? DbRepositoryAction.Default.GetManyExtendedPaged;
        GetManyExtendedTranslated = getManyExtendedTranslated ?? DbRepositoryAction.Default.GetManyExtendedTranslated;
        GetManyExtendedTranslatedPaged = getManyExtendedTranslatedPaged ?? DbRepositoryAction.Default.GetManyExtendedTranslatedPaged;
        GetManyTranslated = getManyTranslated ?? DbRepositoryAction.Default.GetManyTranslated;
        GetManyTranslatedPaged = getManyTranslatedPaged ?? DbRepositoryAction.Default.GetManyTranslatedPaged;
    }

    /// <summary>
    /// Action used to create a new entity.
    /// </summary>
    public DbRepositoryAction Create { get; set; }

    /// <summary>
    /// Action used to update an entity.
    /// </summary>
    public DbRepositoryAction Update { get; set; }

    /// <summary>
    /// Action used to patch an entity.
    /// </summary>
    public DbRepositoryAction Patch { get; set; }

    /// <summary>
    /// Action used to delete an entity.
    /// </summary>
    public DbRepositoryAction DeleteById { get; set; }

    /// <summary>
    /// Action used to delete multiple entities at once.
    /// </summary>
    public DbRepositoryAction DeleteMany { get; set; }

    /// <summary>
    /// Action used to get an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetById { get; set; }

    /// <summary>
    /// Action used to get the extended version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtended { get; set; }

    /// <summary>
    /// Action used to get the extended and translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtendedTranslated { get; set; }

    /// <summary>
    /// Action used to get the translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdTranslated { get; set; }

    /// <summary>
    /// Action used to get a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOne { get; set; }

    /// <summary>
    /// Action used to get the extended version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtended { get; set; }

    /// <summary>
    /// Action used to get the extended and translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtendedTranslated { get; set; }

    /// <summary>
    /// Action used to get the translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneTranslated { get; set; }

    /// <summary>
    /// Action used to get one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetMany { get; set; }

    /// <summary>
    /// Action used to get a page of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyPaged { get; set; }

    /// <summary>
    /// Action used to get the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtended { get; set; }

    /// <summary>
    /// Action used to get a page of the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedPaged { get; set; }

    /// <summary>
    /// Action used to get the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslated { get; set; }

    /// <summary>
    /// Action used to get a page of the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslatedPaged { get; set; }

    /// <summary>
    /// Action used to get the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslated { get; set; }

    /// <summary>
    /// Action used to get a page of the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslatedPaged { get; set; }
}