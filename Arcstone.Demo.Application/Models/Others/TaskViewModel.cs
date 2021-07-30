using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noname.UnitOfWork.Lib.Entities;

namespace Arcstone.Demo.Application.Models.Others
{
    public class TaskViewModel : AuditableAddDelete<Guid>
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid ProjectId { set; get; }
        public int TotalTime { get; set; }
        public long StartTime { set; get; }
        public long EndTime { set; get; }
        public string OtherInfo { set; get; }

    }
}
