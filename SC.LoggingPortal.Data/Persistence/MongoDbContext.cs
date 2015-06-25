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
        internal readonly MongoDatabaseBase database;

        internal readonly MongoClient client;

        internal readonly string dbName;

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
    }
}
