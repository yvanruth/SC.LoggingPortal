namespace SC.LoggingPortal.Mongo
{
    using MongoDB.Driver;
    using SC.LoggingPortal.Mongo.Attributes;
    using System;
    using System.Configuration;
    using System.Linq;

    public abstract class MongoDbContext
    {
        public MongoDbContext(string connectionStringOrName)
        {
            if(!connectionStringOrName.StartsWith("mongodb://", StringComparison.OrdinalIgnoreCase))
            {
                connectionStringOrName = ConfigurationManager.ConnectionStrings[connectionStringOrName].ConnectionString;
            }
            
            var mongoUrl = new MongoUrl(connectionStringOrName);
            InstantiateMongoCollections(new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName));
        }

        private void InstantiateMongoCollections(IMongoDatabase mongoDatabase)
        {
            var collectionProperties = this.GetType().GetProperties().Where(prop =>
                prop.PropertyType.IsGenericType &&
                prop.PropertyType.GetGenericTypeDefinition() == typeof(IMongoCollection<>));

            var getCollectionMethod = mongoDatabase.GetType().GetMethod("GetCollection");

            foreach(var property in collectionProperties)
            {
                var collection = getCollectionMethod.MakeGenericMethod(property.PropertyType.GenericTypeArguments[0])
                    .Invoke(mongoDatabase, new object[] 
                    {
                        GetCollectionName(property),
                        null
                    });

                property.SetValue(this, collection);
            }
        }

        private object GetCollectionName(System.Reflection.PropertyInfo property)
        {
            var attribute= property.GetCustomAttributes(typeof(CollectionAttribute), true).FirstOrDefault() as CollectionAttribute;
            if(attribute == null)
            {
                return property.Name;
            }

            return attribute.Name;
        }
    }
}
