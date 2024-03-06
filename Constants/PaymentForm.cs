using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public static class PaymentForm
    {
        public const string EFECTIVO = "01";
        public const string CHEQUE_NOMINATIVO = "02";
        public const string TRANSFERENCIA_ELECTRONICA = "03";
        public const string TARJETA_DE_CREDITO = "04";
        public const string MONEDERO_ELECTRONICO = "05";
        public const string DINERO_ELECTRONICO = "06";
        public const string VALES_DE_DESPENSA = "08";
        public const string DACION_EN_PAGO = "12";
        public const string SUBROGACION = "13";
        public const string CONSIGNACION = "14";
        public const string CONDONACION = "15";
        public const string COMPENSACION = "17";
        public const string NOVACION = "23";
        public const string CONFUSION = "24";
        public const string REMISION_DE_DEUDA = "25";
        public const string PRESCRIPCION_O_CADUCIDAD = "26";
        public const string A_SATISFACCION_DEL_ACREEDOR = "27";
        public const string TARJETA_DE_DEBITO = "28";
        public const string TERJETA_DE_SERVICIOS = "29";
        public const string APLICACION_ANTICIPOS = "30";
        public const string INTERMEDIARIO_PAGOS = "31";
        public const string POR_DEFINIR = "99";
    }
}
