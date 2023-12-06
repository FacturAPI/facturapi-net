using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
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
        public Efos Efos { get; set; }
    }
}
