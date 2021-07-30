using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Project
{
    public class ProjectInfo
    {
        [Required]
        public string Name { set; get; }

        public string Description { set; get; }

        [Required]
        public string ClientName { set; get; }
    }
}