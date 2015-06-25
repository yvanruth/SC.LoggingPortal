namespace SC.LoggingPortal.Data.Entity
{
    using System;

    /// <summary>
    /// Class EntityBase.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
    }
}
