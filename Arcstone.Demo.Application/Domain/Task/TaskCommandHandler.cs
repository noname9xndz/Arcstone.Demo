using Arcstone.Demo.Application.Domain.Task.Commands;
using Arcstone.Demo.Application.Models.Others;
using AutoMapper;
using MediatR;
using Noname.UnitOfWork.Lib;
using System;
using System.Threading;
using System.Threading.Tasks;
using Arcstone.Demo.Application.Extensions;
using Arcstone.Demo.Application.Models.Entities;
using Microsoft.AspNetCore.Http;
using Noname.UnitOfWork.Lib.Extensions;

namespace Arcstone.Demo.Application.Domain.Task
{
    public class TaskCommandHandler :
     IRequestHandler<CreateTaskCommand, ResponseApi<CreateTaskCommandResponse>>,
     IRequestHandler<UpdateTaskCommand, ResponseApi<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<CreateTaskCommandResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            ResponseApi<CreateTaskCommandResponse> response = new ResponseApi<CreateTaskCommandResponse>();

            var model = _mapper.Map<CreateTaskCommand, TaskModel>(request);
            var time = DateTime.Now.ToTimestamp();
            var userId = _httpContextAccessor.GetUserId();
            model.CreatedDate = time;
            model.ModifiedDate = time;
            model.Date = time.ToDate();
            model.CreatedBy = userId;
            model.ModifiedBy = userId;
            var project = await _unitOfWork.GetCrudRepository<TaskModel>().AddAsync(model);
            var res = await _unitOfWork.SaveChangesAsync();
            if (res > 0)
            {
                response.Result = _mapper.Map<TaskModel, CreateTaskCommandResponse>(project);
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
        public async Task<ResponseApi<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();
            var data = await _unitOfWork.GetCrudRepository<TaskModel>().GetByIdAsync(request.Id);
            if (data != null)
            {
                data.ModifiedBy = _httpContextAccessor.GetUserId();
                data.Name = request.Name;
                data.Description = request.Description;
                data.StartTime = request.StartTime;
                data.EndTime = request.EndTime;
                data.OtherInfo = request.OtherInfo;
                data.TotalTime = request.TotalTime;
                data.ModifiedDate = DateTime.Now.ToFileTime();
                await _unitOfWork.GetCrudRepository<TaskModel>().Update(data);
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
    }
}