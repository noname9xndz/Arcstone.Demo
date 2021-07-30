using System;
using Noname.UnitOfWork.Lib.Entities.Interfaces;

namespace Noname.UnitOfWork.Lib.Entities
{
    /// <summary>
    /// Entity class with auditable fields
    /// </summary>
    public abstract partial class AuditableAddDelete<TId> : Entity<TId>, IAuditableEntity<TId>, IsDeleteEntity
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
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets DeletedAt
        /// </summary>
        public long? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}