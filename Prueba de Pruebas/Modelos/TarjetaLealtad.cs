using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class TarjetaLealtadM
    {
        [JsonProperty("TarjetaLealtad")]
        public string TarjetaLealtad { get; set; }

        [JsonProperty("TelefonoCelular")]
        public string TelefonoCelular { get; set; }

        public static TarjetaLealtadM FromJson(string json) => JsonConvert.DeserializeObject<TarjetaLealtadM>(json, Converter.Settings);
    }
}
