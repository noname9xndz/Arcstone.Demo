using System;
using Noname.UnitOfWork.Lib.Entities.Interfaces;

namespace Noname.UnitOfWork.Lib.Entities
{
    /// <summary>
    /// Entity class with auditable fields
    /// </summary>
    public abstract partial class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
    {
        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets Created Date
        /// </summary>
        public long CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets Modified by
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets ModifiedDate
        /// </summary>
        public long? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public bool Active { get; set; } = true;
    }
}