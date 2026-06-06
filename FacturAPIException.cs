using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Facturapi
{
    public class FacturapiException : Exception
    {
        public int? Status { get; private set; }
        public string Code { get; private set; }
        public string Path { get; private set; }
        public string Location { get; private set; }
        public JArray Errors { get; private set; }
        public string LogId { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        public FacturapiException() : base() { }
        public FacturapiException(
            string message,
            int? status = null,
            string code = null,
            string path = null,
            string location = null,
            JArray errors = null,
            string logId = null,
            IReadOnlyDictionary<string, string> headers = null
        ) : base(message)
        {
            Status = status;
            Code = code;
            Path = path;
            Location = location;
            Errors = errors;
            LogId = logId;
            Headers = headers ?? new Dictionary<string, string>();
        }
    }
}
