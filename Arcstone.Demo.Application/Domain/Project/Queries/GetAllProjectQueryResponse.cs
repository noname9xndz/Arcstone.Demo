using System;
using System.Collections.Generic;
using Arcstone.Demo.Application.Models.Others;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class GetAllProjectQueryResponse : ProjectInfo
    {
        public GetAllProjectQueryResponse()
        {
            TaskGroupByDateModels = new List<TaskGroupByDateModel>();
        }
        public Guid Id { set; get; }

        public List<TaskGroupByDateModel> TaskGroupByDateModels { get; set; }

        public int TotalTimeWithCondition { get; set; }
        public int TotalTime { get; set; }
    }
}