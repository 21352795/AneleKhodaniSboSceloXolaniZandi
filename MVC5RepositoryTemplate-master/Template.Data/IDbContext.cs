using System.Data.Entity;

namespace Template.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity:class;
        int SaveChanges();
        void Dispose();
    }
}
