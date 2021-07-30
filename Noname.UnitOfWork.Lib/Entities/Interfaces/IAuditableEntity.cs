using System;

namespace Noname.UnitOfWork.Lib.Entities.Interfaces
{
    /// <summary>
    /// Interface that defines auditable entity
    /// </summary>
    public interface IAuditableEntity<T>
    {
        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets Created Date
        /// </summary>
        long CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets Modified by
        /// </summary>
        string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets ModifiedDate
        /// </summary>
        long? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets ModifiedDate
        /// </summary>
        bool Active { get; set; }
    }
}