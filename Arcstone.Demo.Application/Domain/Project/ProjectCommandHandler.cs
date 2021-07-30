using Arcstone.Demo.Application.Domain.Project.Commands;
using Arcstone.Demo.Application.Extensions;
using Arcstone.Demo.Application.Models.Entities;
using Arcstone.Demo.Application.Models.Others;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Noname.UnitOfWork.Lib;
using Noname.UnitOfWork.Lib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arcstone.Demo.Application.Domain.Project
{
    public class ProjectCommandHandler :
        IRequestHandler<CreateProjectCommand, ResponseApi<CreateProjectCommandResponse>>,
        IRequestHandler<UpdateProjectCommand, ResponseApi<bool>>,
        IRequestHandler<DeleteProjectCommand, ResponseApi<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<CreateProjectCommandResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            ResponseApi<CreateProjectCommandResponse> response = new ResponseApi<CreateProjectCommandResponse>();

            var model = _mapper.Map<CreateProjectCommand, ProjectModel>(request);
            model.CreatedDate = DateTime.Now.ToTimestamp();
            model.ModifiedDate = DateTime.Now.ToTimestamp();
            model.CreatedBy = _httpContextAccessor.GetUserId();
            model.ModifiedBy = _httpContextAccessor.GetUserId();
            var project = await _unitOfWork.GetCrudRepository<ProjectModel>().AddAsync(model);
            var res = await _unitOfWork.SaveChangesAsync();
            if (res > 0)
            {
                response.Result = _mapper.Map<ProjectModel, CreateProjectCommandResponse>(project);
            }
            else
            {
                response.ErrorMessage.Add("Has error.Try again");
            }

            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();
            var data = await _unitOfWork.GetCrudRepository<ProjectModel>().GetByIdAsync(request.Id);
            if (data != null)
            {
                data.ModifiedBy = _httpContextAccessor.GetUserId();
                data.Name = request.Name;
                data.Description = request.Description;
                data.ClientName = request.ClientName;
                data.ModifiedDate = DateTime.Now.ToFileTime();
                await _unitOfWork.GetCrudRepository<ProjectModel>().Update(data);
                var res = await _unitOfWork.SaveChangesAsync();
                if (res > 0)
                {
                    response.Result = true;
                }
                else
                {
                    response.ErrorMessage.Add("Has error.Try again");
                }
            }
            else
            {
                response.ErrorMessage.Add("Not Found.");
            }
            return response;
        }

        public async Task<ResponseApi<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();
            var data = await _unitOfWork.GetCrudRepository<ProjectModel>().GetByIdAsync(request.Id);
            if (data != null)
            {
                var predicate = PredicateBuilder.True<TaskModel>();
                predicate = predicate.And(x => x.ProjectId == data.Id);
                var query = await _unitOfWork.GetCrudRepository<TaskModel>().GetAsync(predicate, null, nameof(TaskModel.Project), null, null);
                if (query != null && query.Any())
                {
                    response.ErrorMessage.Add("Don't delete.");
                }
                else
                {
                    await _unitOfWork.GetCrudRepository<ProjectModel>().Delete(request.Id);
                    var res = await _unitOfWork.SaveChangesAsync();
                    if (res > 0)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.ErrorMessage.Add("Has error.Try again");
                    }
                }
            }
            else
            {
                response.ErrorMessage.Add("Not Found.");
            }
            return response;
        }
    }
}