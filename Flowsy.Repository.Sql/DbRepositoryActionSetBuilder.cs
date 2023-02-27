namespace Flowsy.Repository.Sql;

public class DbRepositoryActionSetBuilder
{
    private readonly DbRepositoryActionSet _actionSet;
    
    internal DbRepositoryActionSetBuilder()
    {
        _actionSet = new DbRepositoryActionSet();
    }

    public DbRepositoryActionSetBuilder Create(DbRepositoryAction action)
    {
        _actionSet.Create = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder Update(DbRepositoryAction action)
    {
        _actionSet.Update = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder Patch(DbRepositoryAction action)
    {
        _actionSet.Patch = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder DeleteById(DbRepositoryAction action)
    {
        _actionSet.DeleteById = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder DeleteMany(DbRepositoryAction action)
    {
        _actionSet.DeleteMany = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetById(DbRepositoryAction action)
    {
        _actionSet.GetById = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetByIdExtended(DbRepositoryAction action)
    {
        _actionSet.GetByIdExtended = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetByIdExtendedTranslated(DbRepositoryAction action)
    {
        _actionSet.GetByIdExtendedTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetByIdTranslated(DbRepositoryAction action)
    {
        _actionSet.GetByIdTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetOne(DbRepositoryAction action)
    {
        _actionSet.GetOne = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetOneExtended(DbRepositoryAction action)
    {
        _actionSet.GetOneExtended = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetOneExtendedTranslated(DbRepositoryAction action)
    {
        _actionSet.GetOneExtendedTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetOneTranslated(DbRepositoryAction action)
    {
        _actionSet.GetOneTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetMany(DbRepositoryAction action)
    {
        _actionSet.GetMany = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyPaged(DbRepositoryAction action)
    {
        _actionSet.GetManyPaged = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyExtended(DbRepositoryAction action)
    {
        _actionSet.GetManyExtended = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyExtendedPaged(DbRepositoryAction action)
    {
        _actionSet.GetManyExtendedPaged = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyExtendedTranslated(DbRepositoryAction action)
    {
        _actionSet.GetManyExtendedTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyExtendedTranslatedPaged(DbRepositoryAction action)
    {
        _actionSet.GetManyExtendedTranslatedPaged = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyTranslated(DbRepositoryAction action)
    {
        _actionSet.GetManyTranslated = action;
        return this;
    }
    
    public DbRepositoryActionSetBuilder GetManyTranslatedPaged(DbRepositoryAction action)
    {
        _actionSet.GetManyTranslatedPaged = action;
        return this;
    }

    public DbRepositoryActionSet Build() => _actionSet;
}