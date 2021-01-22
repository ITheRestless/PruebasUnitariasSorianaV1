using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class Cuenta
    {
        [JsonProperty("UserId")]
        public long UserId { get; set; }

        [JsonProperty("AccountMask")]
        public string AccountMask { get; set; }

        [JsonProperty("AccountToken")]
        public string AccountToken { get; set; }

        [JsonProperty("AccountImagePath")]
        public string AccountImagePath { get; set; }

        [JsonProperty("AccountName")]
        public string AccountName { get; set; }

        [JsonProperty("BalanceActive")]
        public bool BalanceActive { get; set; }

        [JsonProperty("Saldos")]
        public Saldos Saldos { get; set; }
    }
}
