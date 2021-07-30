using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class ProjectStatisticQueryResponse :  ProjectInfo
    {
        public Guid Id { set; get; }
    }
}
