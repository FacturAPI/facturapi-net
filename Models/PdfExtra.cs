namespace Facturapi
{
    public class PdfExtra
    {
        public bool Codes { get; set; }
        public bool AddressCodes { get; set; }
        public bool ProductKey { get; set; }
        public bool RoundUnitPrice { get; set; }
        public bool TaxBreakdown { get; set; }
        public bool IepsBreakdown { get; set; }
        public bool RenderCartaPorte { get; set; }
        public bool RepeatSignature { get; set; }
    }
}