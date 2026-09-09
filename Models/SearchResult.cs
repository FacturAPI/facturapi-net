using System.Collections.Generic;

namespace Facturapi
{
    public class SearchResult<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public bool? TotalsAreCapped { get; set; }
        public string NextCursor { get; set; }
        public string PreviousCursor { get; set; }
        public List<T> Data { get; set; }
    }
}
