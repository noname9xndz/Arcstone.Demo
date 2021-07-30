using Noname.UnitOfWork.Lib.Entities;
using System;
using Noname.UnitOfWork.Lib.Extensions;

namespace Arcstone.Demo.Application.Models.Entities
{
    public class TaskModel : AuditableAddDelete<Guid>
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid ProjectId { set; get; }
        public ProjectModel Project { get; set; }
        public int TotalTime { get; set; }
        public long StartTime { set; get; }
        public long EndTime { set; get; }
        public string OtherInfo { set; get; }
        public long Date { set; get; }
    }
}