namespace SC.LoggingPortal.Data.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using System.Configuration;

    public class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// The database
        /// </summary>
        internal readonly MongoDatabase database;

        internal readonly MongoClient client;

        internal readonly string dbName;


        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringOrName">Name of the connection string or.</param>
        public MongoDbContext(string connectionStringOrName)
        {
            this.client = GetClient(connectionStringOrName);
            this.dbName = GetDatabaseName(connectionStringOrName);
            this.database = GetDatabase(connectionStringOrName);
        }

        private string GetDatabaseName(string connectionStringOrName)
        {
            Uri connectionStringUri = TryGetConnectionString(connectionStringOrName);
            return connectionStringUri.Segments.Where(segment => segment != "/").FirstOrDefault();
        }

        private MongoClient GetClient(string connectionStringOrName)
        {
            Uri connectionStringUri = TryGetConnectionString(connectionStringOrName);
            var mongoConnection = string.Concat(connectionStringUri.Scheme, "://", connectionStringUri.Authority);
            var mongoClient = new MongoClient(mongoConnection);
            return mongoClient;
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <param name="connectionStringOrName">Name of the connection string or.</param>
        /// <returns>MongoDatabase.</returns>
        private MongoDatabase GetDatabase(string connectionStringOrName)
        {
            var mongoServer = this.client.GetServer();
            return mongoServer.GetDatabase(this.dbName);
        }

        /// <summary>
        /// Tries the get connection string.
        /// </summary>
        /// <param name="connectionStringOrName">Name of the connection string or.</param>
        /// <returns>Uri.</returns>
        /// <exception cref="System.UriFormatException">Invalid connectionstring</exception>
        private Uri TryGetConnectionString(string connectionStringOrName)
        {
            Uri connectionStringUri = null;
            if (Uri.TryCreate(connectionStringOrName, UriKind.Absolute, out connectionStringUri) ||
                Uri.TryCreate(ConfigurationManager.ConnectionStrings[connectionStringOrName].ConnectionString, UriKind.Absolute, out connectionStringUri))
            {
                return connectionStringUri;
            }

            throw new UriFormatException("Invalid connectionstring");
        }

        public MongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.database.GetCollection<TEntity>(typeof(TEntity).Name);            
        }
    }
}
