using System;
using System.Collections.Generic;

namespace Arcstone.Demo.Client.Models.Response
{
    public class GetAllProjectQueryResponse
    {
        public GetAllProjectQueryResponse()
        {
            TaskGroupByDateModels = new List<TaskGroupByDateModel>();
        }
        public Guid Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string ClientName { set; get; }

        public List<TaskGroupByDateModel> TaskGroupByDateModels { get; set; }

        public int TotalTimeWithCondition { get; set; }
        public int TotalTime { get; set; }
    }
}