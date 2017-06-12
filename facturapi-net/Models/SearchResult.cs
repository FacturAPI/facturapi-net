using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class SearchResult<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public T[] Data { get; set; }
    }
}
