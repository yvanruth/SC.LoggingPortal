namespace SC.LoggingPortal.Data.Repository
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using SC.LoggingPortal.Data.Entity;
    using SC.LoggingPortal.Data.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class LoggingRepository : IRepository<LogMessage>
    {
        /// <summary>
        /// The collection
        /// </summary>
        internal readonly LoggingDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbRepository{LogMessage}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public LoggingRepository(LoggingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual async Task InsertAsync(LogMessage entity)
        {
            entity.Id = Guid.NewGuid();
            await dbContext.LogMessages.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual async Task UpdateAsync(LogMessage entity)
        {
            var filter = new FilterDefinitionBuilder<LogMessage>().Eq(x=> x.Id, entity.Id);
            await dbContext.LogMessages.ReplaceOneAsync(filter, entity, new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual async Task DeleteAsync(LogMessage entity)
        {
            await dbContext.LogMessages.DeleteOneAsync<LogMessage>(x => x.Id == entity.Id);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TEntity.</returns>
        public virtual async Task<LogMessage> GetByIdAsync(System.Guid id)
        {
            var filter = new FilterDefinitionBuilder<LogMessage>().Eq(x => x.Id, id);
            var result = await dbContext.LogMessages.FindAsync(filter, new FindOptions<LogMessage,LogMessage> { Limit = 1 }).Result.ToListAsync();
            return result.ToArray().FirstOrDefault();

        }

        public virtual async Task<IEnumerable<LogMessage>> GetAllAsync()
        {
            var result = await dbContext.LogMessages.FindAsync(_ => true).Result.ToListAsync();
            return result;
        }

        public virtual async Task<IEnumerable<LogMessage>> QueryAsync(System.Linq.Expressions.Expression<Func<LogMessage, bool>> predicate)
        {
            var result = await dbContext.LogMessages.FindAsync(predicate);
            return result.ToListAsync().Result.AsEnumerable();
        }
    }
}
