namespace SC.LoggingPortal.Mongo.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CollectionAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionAttribute()
        {

        }

        public CollectionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
