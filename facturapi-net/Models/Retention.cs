using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class Retention
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Customer Customer { get; set; }
        public bool Livemode { get; set; }
        public string Status { get; set; }
        public string VerificationUrl { get; set; }
        public string Type { get; set; }
        public string Uuid { get; set; }
        public DateTime FechaExp { get; set; }
        public string CveRetenc { get; set; }
        public string FolioInt { get; set; }
        public string DescRetenc { get; set; }
        public RetentionPeriod Periodo { get; set; }
        public RetentionTotals Totales { get; set; }
        public List<Namespace> Namespaces { get; set; }
        public string[] Complements { get; set; }
        public string[] Addenda { get; set; }
        public string CancellationReceipt { get; set; }
        public Stamp Stamp { get; set; }
        public string PdfCustomSection { get; set; }
    }

    public class RetentionPeriod
    {
        public int MesIni { get; set; }
        public int MesFin { get; set; }
        public int Ejerc { get; set; }
    }

    public class RetentionTotals
    {
        public Decimal MontoTotGrav { get; set; }
        public Decimal MontoTotExent { get; set; }
        public Decimal MontoTotOperacion { get; set; }
        public Decimal MontoTotRet { get; set; }
        public RetentionRetainedTax[] ImpRetenidos { get; set; }
    }

    public class RetentionRetainedTax
    {
        public Decimal BaseRet { get; set; }
        public string Impuesto { get; set; }
        public Decimal MontoRet { get; set; }
        public bool PagoProvisional { get; set; }
    }
}
