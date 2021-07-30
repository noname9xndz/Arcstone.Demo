using System;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class GetProjectInfoQueryResponse : ProjectInfo
    {
        public Guid Id { set; get; }
    }
}