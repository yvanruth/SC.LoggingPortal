namespace SC.LoggingPortal.Data.Persistence
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMongoDbContext
    {
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>MongoCollection&lt;TEntity&gt;.</returns>
        MongoCollection<TEntity> GetCollection<TEntity>();
    }
}
