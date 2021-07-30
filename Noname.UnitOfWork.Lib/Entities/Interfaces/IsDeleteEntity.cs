using System;

namespace Noname.UnitOfWork.Lib.Entities.Interfaces
{
    public interface IsDeleteEntity
    {
        long? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}