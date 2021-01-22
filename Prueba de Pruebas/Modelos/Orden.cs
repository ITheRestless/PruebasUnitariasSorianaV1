using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class Orden
    {
        [JsonProperty("idTipoEntrega")]
        public long idTipoEntrega { get; set; }

        [JsonProperty("idCnscDirCte_Opc")]
        public long idCnscDirCte_Opc { get; set; }

        [JsonProperty("fechaEntrega")]
        public string fechaEntrega { get; set; }

        [JsonProperty("bitExpress_Opc")]
        public long bitExpress_Opc { get; set; }

        [JsonProperty("idFormaPago_Opc")]
        public long idFormaPago_Opc { get; set; }

        [JsonProperty("montoEfevo_Opc")]
        public decimal montoEfevo_Opc { get; set; }

        [JsonProperty("montoVales_Opc")]
        public decimal montoVales_Opc { get; set; }

        [JsonProperty("numTarjeta_Opc")]
        public string numTarjeta_Opc { get; set; }

        [JsonProperty("vigenciaTarjeta_Opc")]
        public string vigenciaTarjeta_Opc { get; set; }

        [JsonProperty("nipTarjeta_Opc")]
        public string nipTarjeta_Opc { get; set; }

        [JsonProperty("bancoTarjeta_Opc")]
        public string bancoTarjeta_Opc { get; set; }
    }
}
