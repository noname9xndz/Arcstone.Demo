using Arcstone.Demo.Application.Domain.Project.Queries;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Entities;
using Arcstone.Demo.Application.Models.Others;
using AutoMapper;
using HotelManagementSystem.Models.AutoMapper;
using LinqKit;
using MediatR;
using Noname.UnitOfWork.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Noname.UnitOfWork.Lib.Extensions;

namespace Arcstone.Demo.Application.Domain.Project
{
    public class ProjectQueryHandler :
        IRequestHandler<GetProjectInfoQuery, ResponseApi<GetProjectInfoQueryResponse>>,
        IRequestHandler<GetAllProjectQuery, ResponseApi<PaginatedList<GetAllProjectQueryResponse>>>,
        IRequestHandler<ProjectStatisticQuery, ResponseApi<PaginatedList<GetAllProjectQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<PaginatedList<GetAllProjectQueryResponse>> response = new ResponseApi<PaginatedList<GetAllProjectQueryResponse>>();
            var predicate = PredicateBuilder.True<ProjectModel>();
            if (!request.Keyword.IsNullOrWhiteSpaceCustom())
            {
                var keyword = request.Keyword.ToLower();
                predicate = predicate.And(
                    x => x.Name.ToLower().Contains(keyword) || x.Description.ToLower().Contains(keyword));
            }


            Func<IQueryable<ProjectModel>, IOrderedQueryable<ProjectModel>> orderBy = null;
            orderBy = queryable => queryable.OrderByDescending(x => x.CreatedDate);

            var query = await _unitOfWork.GetCrudRepository<ProjectModel>().GetAsync(predicate, orderBy
                , nameof(ProjectModel.Tasks), null, null);

            query = query?.Select(x => new ProjectModel()
            {
                Id = x.Id,
                Name = x.Name,
                TotalTime = x.Tasks.Sum(y => y.TotalTime),
                TotalTimeWithCondition = x.Tasks.Sum(y => y.TotalTime),
                Active = x.Active,
                ClientName = x.ClientName,
                Tasks = x.Tasks,
                Description = x.Description,
                CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                DeletedAt = x.DeletedAt,
                IsDeleted = x.IsDeleted
            });

            if (request.StartTime != null)
            {
                query = query?.Select(x => new ProjectModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalTime = x.TotalTime,
                    TotalTimeWithCondition = x.Tasks.Where(y => y.StartTime >= request.StartTime.Value).Sum(y => y.TotalTime),
                    Active = x.Active,
                    ClientName = x.ClientName,
                    Tasks = x.Tasks.Where(y => y.StartTime >= request.StartTime.Value).ToList(),
                    Description = x.Description,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedAt = x.DeletedAt,
                    IsDeleted = x.IsDeleted
                });
            }

            if (request.EndTime != null)
            {
                query = query?.Select(x => new ProjectModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalTime = x.TotalTime,
                    TotalTimeWithCondition = x.Tasks.Where(y => y.StartTime <= request.EndTime.Value).Sum(y => y.TotalTime),
                    Active = x.Active,
                    ClientName = x.ClientName,
                    Tasks = x.Tasks.Where(y => y.StartTime <= request.EndTime.Value).ToList(),
                    Description = x.Description,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedAt = x.DeletedAt,
                    IsDeleted = x.IsDeleted
                });
            }

            if (query != null && query.Any())
            {
                var data = PaginatedList<ProjectModel>.CreatePaging(query, request.PageIndex, request.PageSize);
                data.Items = data.Items.Select(x => new ProjectModel()
                {
                    Id =x.Id,
                    Name = x.Name,
                    TotalTime = x.TotalTime,
                    TotalTimeWithCondition = x.TotalTimeWithCondition,
                    TaskGroupByDateModels = x.Tasks.GroupBy(y => y.Date).Select(z => new TaskGroupByDateModel
                    {
                        Date = z.First().Date,
                        TotalTime = z.Sum(tt => tt.TotalTime)
                    }),
                    Active = x.Active,
                    ClientName = x.ClientName,
                    Tasks = x.Tasks,
                    Description = x.Description,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedAt = x.DeletedAt,
                    IsDeleted = x.IsDeleted
                }).ToList();

                response.Result = await _mapper.PaginatedListMap<ProjectModel, GetAllProjectQueryResponse>(data);
            }

            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<GetProjectInfoQueryResponse>> Handle(GetProjectInfoQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<GetProjectInfoQueryResponse> response = new ResponseApi<GetProjectInfoQueryResponse>();
            var model = await _unitOfWork.GetCrudRepository<ProjectModel>().GetByIdAsync(request.ProjectId);
            if (model != null)
            {
                var data = _mapper.Map<ProjectModel, GetProjectInfoQueryResponse>(model);
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

        public async Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> Handle(ProjectStatisticQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<PaginatedList<GetAllProjectQueryResponse>> response = new ResponseApi<PaginatedList<GetAllProjectQueryResponse>>();
            var predicate = PredicateBuilder.True<TaskModel>();
            predicate = predicate.And(x=>x.Date >= request.StartTime && x.Date <= request.EndTime);
            Func<IQueryable<TaskModel>, IOrderedQueryable<TaskModel>> orderBy = null;
            orderBy = queryable => queryable.OrderByDescending(x => x.Date);

            var query = await _unitOfWork.GetCrudRepository<TaskModel>().GetAsync(predicate
                , orderBy
                , null, null, null);
            //todo
            //var queryTest = query.GroupBy(x=>x.Date && x.ProjectId).Select(x => new
            //{
               
            //});

            return response;
        }
    }
}