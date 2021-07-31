using System;
using System.Collections.Generic;
using System.Linq;

namespace Arcstone.Demo.Client.Models
{
    public class PaginatedList<T>
    {
        public PaginatedList()
        {
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public List<T> Items { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalRecords = count;

            this.Items = new List<T>();
            this.Items.AddRange(items);
        }

        public int FirstRowOnPage
        {
            set { }
            get
            {
                return (PageIndex - 1) * TotalPages + 1;
            }
        }

        public int LastRowOnPage
        {
            set { }
            get
            {
                return Math.Min(PageIndex * TotalPages, TotalRecords);
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
            set { }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
            set { }
        }

        public static PaginatedList<T> CreatePaging(IQueryable<T> source, int pageIndex = 1, int pageSize = 10)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}