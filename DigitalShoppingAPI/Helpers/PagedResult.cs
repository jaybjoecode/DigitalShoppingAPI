using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Helpers
{
    public class PagedResult<T>
    {
        public PagedList<T> elements { get; set; }
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public PagedResult(IQueryable<T> queryable, BaseCriterial criterial)
        {            
            elements = PagedList<T>.Create(queryable, criterial.PageNumber, criterial.PageSize);

            this.totalCount = elements.TotalCount;
            this.pageSize = elements.PageSize;
            this.currentPage = elements.CurrentPage;
            this.totalPages = elements.TotalPages;            
        }
    }
}
