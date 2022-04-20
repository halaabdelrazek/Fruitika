using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceProjectMVC.Models
{
    public class PaginatedList<T>: List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items , int count , int pageIndex , int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(items);
        }
        public bool PreviousPage
        {
            get
            {
                return this.PageIndex >1;
            }
        }

        public bool NextPage
        {
            get { return this.PageIndex < TotalPages; }
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex ,int pageSize)
        {
            var count = await source.CountAsync();
            var itmes = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(itmes, count, pageIndex, pageSize); 
        }
    }
}
