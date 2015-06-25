namespace SC.LoggingPortal.Data.Repository
{
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using MongoDB.Driver.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SC.LoggingPortal.Data.Entity;
    using SC.LoggingPortal.Data.Persistence;

    public abstract class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// The collection
        /// </summary>
        internal readonly MongoCollection collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public MongoDbRepository(IMongoDbContext dbContext)
        {
            this.collection = dbContext.GetCollection<TEntity>();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            return collection.Insert(entity).Ok;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Update(TEntity entity)
        {
            if (entity.Id == null)
            {
                return Insert(entity);
            }

            return collection.Save(entity).DocumentsAffected > 0;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Delete(TEntity entity)
        {
            return collection.Remove(MongoDB.Driver.Builders.Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TEntity.</returns>
        public virtual TEntity GetById(System.Guid id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return collection.FindAllAs<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.collection.AsQueryable<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return this.collection.FindAllAs<TEntity>().ToList();
        }
    }
}
