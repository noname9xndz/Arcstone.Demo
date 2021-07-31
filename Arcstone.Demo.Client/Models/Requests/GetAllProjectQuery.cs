using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class GetAllProjectQuery
    {
        public string Keyword { set; get; }

        [Required]
        public int PageIndex { set; get; } = 1;

        [Required]
        public int PageSize { set; get; } = 10;

        public long? StartTime { set; get; }
        public long? EndTime { set; get; }
    }
}