using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class ForeignTrade
    {
        public Decimal Exchange { get; set; }
        public string Incoterm { get; set; }
        public string TransferMotive { get; set; }
        public string OriginCertificate { get; set; }
        public string ExporterNumber { get; set; }
        public string Notes { get; set; }
        public List<ForeignTradeGood> Goods { get; set; }
        public ForeignTradeIssuer Issuer { get; set; }
        public List<ForeignTradeOwner> Owners { get; set; }
        public List<ForeignTradeAddressee> Addressees { get; set; }
    }

    public class ForeignTradeAddressee
    {
        public string TaxId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class ForeignTradeOwner
    {
        public string TaxId { get; set; }
        public string Country { get; set; }
    }

    public class ForeignTradeIssuer
    {
        public string Curp { get; set; }
    }

    public class ForeignTradeGood
    {
        public string Sku { get; set; }
        public string TariffCode { get; set; }
        public Decimal UnitPriceUsd { get; set; }
        public Decimal Quantity { get; set; }
        public List<ForeignTradeGoodDetails> Details { get; set; }

    }

    public class ForeignTradeGoodDetails
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Submodel { get; set; }
        public string SerialNumber { get; set; }
    }
}
