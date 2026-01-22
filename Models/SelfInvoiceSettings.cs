namespace Facturapi
{
  public class SelfInvoiceSettings
  {
    public string[] AllowedCfdiUses { get; set; }
    public bool ApplyResicoIsr { get; set; }
    public string SupportEmail { get; set; }
    public bool SupportEmailVerified { get; set; }
  }
}