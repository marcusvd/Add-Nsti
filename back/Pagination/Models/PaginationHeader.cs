namespace Pagination.Models
{
    public class PaginationHeader
    {
        public int CurrentPg { get; set; }
        public int TotalPgs { get; set; }
        public int PgSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public PaginationHeader(int currentPg, int totalPgs,
        int pgSize, int totalCount, bool hasPrevious, bool hasNext)
        {
            CurrentPg = currentPg;
            TotalPgs = totalPgs;
            PgSize = pgSize;
            TotalCount = totalCount;
            HasPrevious = hasPrevious;
            HasNext = hasNext;

        }

    }
}