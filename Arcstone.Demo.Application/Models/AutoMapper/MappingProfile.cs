using Arcstone.Demo.Application.Domain.Project.Commands;
using Arcstone.Demo.Application.Domain.Project.Queries;
using Arcstone.Demo.Application.Domain.Task.Commands;
using Arcstone.Demo.Application.Domain.Task.Queries;
using Arcstone.Demo.Application.Models.Entities;
using Arcstone.Demo.Application.Models.Others;
using AutoMapper;

namespace HotelManagementSystem.Models.AutoMapper
{
    public class MappingProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ProjectModel, GetAllProjectQueryResponse>();
            CreateMap<CreateProjectCommand, ProjectModel>();
            CreateMap<ProjectModel, CreateProjectCommandResponse>();
            CreateMap<ProjectModel, GetProjectInfoQueryResponse>();
            CreateMap<TaskModel, GetAllTaskByProjectIdQueryResponse>();
            CreateMap<TaskModel, GetTaskInfoQueryResponse>();
            CreateMap<TaskModel, GetAllTaskByProjectIdQueryResponse>();
            CreateMap<CreateTaskCommand, TaskModel>();
            CreateMap<TaskModel, CreateTaskCommandResponse>();
            CreateMap<TaskModel, TaskViewModel>();
            CreateMap<TaskModel, GetAllTaskQueryResponse>();
        }
    }
}