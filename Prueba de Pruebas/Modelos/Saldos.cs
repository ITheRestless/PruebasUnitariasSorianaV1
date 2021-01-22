using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class Saldos
    {
        [JsonProperty("SaldoPuntos")]
        public long SaldoPuntos { get; set; }

        [JsonProperty("SaldoDineroElectronico")]
        public string SaldoDineroElectronico { get; set; }

        [JsonProperty("SaldoEfectivo")]
        public string SaldoEfectivo { get; set; }

        [JsonProperty("Saldo")]
        public string Saldo { get; set; }
    }
}
