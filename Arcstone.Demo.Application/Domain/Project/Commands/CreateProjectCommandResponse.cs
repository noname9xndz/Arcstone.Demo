using System;

namespace Arcstone.Demo.Application.Domain.Project.Commands
{
    public class CreateProjectCommandResponse : ProjectInfo
    {
        public Guid Id { set; get; }
    }
}