using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class RegistroRfcData
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public string FechaLista { get; set; }
        public string Rfc { get; set; }
    }

    public class RegistroRfc
    {
        public bool IsValid { get; set; }
        public RegistroRfcData Data { get; set; }
    }

    public class LcoItem
    {
        public string Estatus { get; set; }
        public string FechaFin { get; set; }
        public string FechaInicio { get; set; }
        public string NumCert { get; set; }
        public string Validez { get; set; }
    }

    public class LcoData
    {
        public string Rfc { get; set; }
        public string Mensaje { get; set; }
        public List<LcoItem> LcoItems { get; set; }
    }
    
    public class Lco
    {
        public bool IsValid { get; set; }
    }

    public class EfosDetalle
    {
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string SituacionContribuyente { get; set; }
        public string NumFechaPresuncion { get; set; }
        public string PubFechaSatPresuntos { get; set; }
        public string NumGlobalPresuncion { get; set; }
        public string PubFechaDofPresuntos { get; set; }
        public string PubSatDefinitivos { get; set; }
        public string PubDofDefinitivos { get; set; }
        public string NumFechaSentFav { get; set; }
        public string PubSatSentFav { get; set; }
    }

    public class EfosData
    {
        public string Mensaje { get; set; }
        public string FechaLista { get; set; }
        public List<EfosDetalle> Detalles { get; set; }
    }

    public class Efos
    {
        public bool IsValid { get; set; }
        public EfosData Data { get; set; }
    }

    public class TaxIdValidation
    {
        public RegistroRfc Registro { get; set; }
        public Lco Lco { get; set; }
        public Efos Efos { get; set; }
    }
}
