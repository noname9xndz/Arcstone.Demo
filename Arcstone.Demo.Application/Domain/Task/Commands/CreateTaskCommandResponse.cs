using System;

namespace Arcstone.Demo.Application.Domain.Task.Commands
{
    public class CreateTaskCommandResponse : TaskInfo
    {
        public Guid Id { set; get; }
    }
}