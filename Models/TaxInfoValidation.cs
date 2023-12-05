using System.Collections.Generic;

namespace Facturapi
{
    public class TaxInfoError
    {
        public string Message { get; set; }
        public string Path { get; set; }
    }

    public class TaxInfoValidation
    {
        public bool IsValid { get; set; }
        public TaxInfoError[] Errors { get; set; }
    }
}
