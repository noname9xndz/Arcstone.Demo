using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Task
{
    public class TaskInfo
    {
        [Required]
        public string Name { set; get; }

        public string Description { set; get; }

        [Required]
        public Guid ProjectId { set; get; }

        [Required]
        public long StartTime { set; get; }

        [Required]
        public int TotalTime { get; set; }

        [Required]
        public long EndTime { set; get; }

        public string OtherInfo { set; get; }
    }
}