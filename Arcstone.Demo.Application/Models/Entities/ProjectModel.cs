using Noname.UnitOfWork.Lib.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Arcstone.Demo.Application.Models.Others;

namespace Arcstone.Demo.Application.Models.Entities
{
    public class ProjectModel : AuditableAddDelete<Guid>
    {
        public ProjectModel()
        {
            Tasks = new List<TaskModel>();
            TaskGroupByDateModels =new List<TaskGroupByDateModel>();
        }

        public string Name { set; get; }

        public string Description { set; get; }

        public string ClientName { set; get; }

        public List<TaskModel> Tasks { get; set; }

        public IEnumerable<TaskGroupByDateModel> TaskGroupByDateModels { get; set; }

        public int TotalTimeWithCondition { get; set; }
        public int TotalTime { get; set; }

    }
}