namespace Facturapi
{
    public class SearchResult<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public T[] Data { get; set; }
    }
}
