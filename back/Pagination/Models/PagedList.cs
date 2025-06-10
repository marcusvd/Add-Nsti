using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pagination.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPg { get;  set; }
        public int TotalPgs { get;  set; }
        public int PgSize { get;  set; }
        public int TotalCount { get;  set; }
        public bool HasPrevious  { get;  set; }
        public bool HasNext  { get;  set; }
        // public bool HasPrevious => CurrentPg > 1;
        // public bool HasNext => CurrentPg < TotalPgs;
        public List<T> EntitiesToShow { get; set; }

        public PagedList(List<T> items, int totalCount, int currentPg, int pgSize)
        {
            TotalCount = totalCount;
            PgSize = pgSize;
            CurrentPg = currentPg;
            TotalPgs = (int)Math.Ceiling(totalCount / (double)pgSize);
            AddRange(items);
        }
        public PagedList()
        {
        }
        
      
        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, int currentPg, int pgSize)
        {
            var count =  source.Count();
            var items = await source.Skip((currentPg -1) * pgSize).Take(pgSize).ToListAsync();
            return new PagedList<T>(items, count, currentPg, pgSize);
        }

    }

}