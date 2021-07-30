using Arcstone.Demo.Application.Domain.Task.Queries;
using Arcstone.Demo.Application.Models.Others;
using AutoMapper;
using MediatR;
using Noname.UnitOfWork.Lib;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Entities;
using HotelManagementSystem.Models.AutoMapper;

namespace Arcstone.Demo.Application.Domain.Task
{  
    public class TaskQueryHandler :
        IRequestHandler<GetAllTaskByProjectIdQuery, ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>>>,
        IRequestHandler<GetAllTaskQuery, ResponseApi<PaginatedList<GetAllTaskQueryResponse>>>,
        IRequestHandler<GetTaskInfoQuery, ResponseApi<GetTaskInfoQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>>> Handle(GetAllTaskByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>> response = new ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>>();

            Func<IQueryable<TaskModel>, IOrderedQueryable<TaskModel>> orderBy = queryable => queryable.OrderByDescending(x => x.CreatedDate);

            var query = await _unitOfWork.GetCrudRepository<TaskModel>().GetAsync(x=>x.ProjectId.Equals(request.ProjectId), orderBy
                , null, null, null);
            if (query != null && query.Any())
            {
                var data = PaginatedList<TaskModel>.CreatePaging(query, request.PageIndex, request.PageSize);
                response.Result = await _mapper.PaginatedListMap<TaskModel, GetAllTaskByProjectIdQueryResponse>(data);
            }

            return response;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<GetTaskInfoQueryResponse>> Handle(GetTaskInfoQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<GetTaskInfoQueryResponse> response = new ResponseApi<GetTaskInfoQueryResponse>();
            var model = await _unitOfWork.GetCrudRepository<TaskModel>().GetByIdAsync(request.TaskId);
            if (model != null)
            {
                var data = _mapper.Map<TaskModel, GetTaskInfoQueryResponse>(model);
                if (data != null)
                {
                    response.Result = data;
                }
            }
            else
            {
                response.ErrorMessage.Add("not found");
            }

            return response;
        }

        public async Task<ResponseApi<PaginatedList<GetAllTaskQueryResponse>>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<PaginatedList<GetAllTaskQueryResponse>> response = new ResponseApi<PaginatedList<GetAllTaskQueryResponse>>();

            Func<IQueryable<TaskModel>, IOrderedQueryable<TaskModel>> orderBy = queryable => queryable.OrderByDescending(x => x.CreatedDate);

            var query = await _unitOfWork.GetCrudRepository<TaskModel>().GetAsync(null, orderBy
                , null, null, null);
            if (query != null && query.Any())
            {
                var data = PaginatedList<TaskModel>.CreatePaging(query, request.PageIndex, request.PageSize);
                response.Result = await _mapper.PaginatedListMap<TaskModel, GetAllTaskQueryResponse>(data);
            }

            return response;
        }
    }
}