using Arcstone.Demo.Application.Helpers;
using AutoMapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.AutoMapper
{
    public static class MappingHelper
    {
        public static async Task<PaginatedList<TDes>> PaginatedListMap<TSrc, TDes>(this IMapper mapper, PaginatedList<TSrc> src)
            where TSrc : class
            where TDes : class

        {
            var result = new PaginatedList<TDes>();
            if (src.Items != null && src.Items.Any())
            {
                result.Items = mapper.Map<List<TSrc>, List<TDes>>(src.Items);
            }
            else
            {
                result.Items = new List<TDes>();
            }
            result.HasNextPage = src.HasNextPage;
            result.HasPreviousPage = src.HasPreviousPage;
            result.FirstRowOnPage = src.FirstRowOnPage;
            result.LastRowOnPage = src.LastRowOnPage;
            result.PageIndex = src.PageIndex;
            result.TotalPages = src.TotalPages;
            result.TotalRecords = src.TotalRecords;
            return await Task.FromResult(result);
        }

        public static async Task<PaginatedList<object>> PaginatedListMap(this object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var res = JsonConvert.DeserializeObject<PaginatedList<object>>(json);
            return await Task.FromResult(res);
        }
    }
}