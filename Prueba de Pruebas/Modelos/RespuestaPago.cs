using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class RespuestaPago
    {
        [JsonProperty("Id_Num_Transaccion")]
        public long Id_Num_Transaccion { get; set; }

        [JsonProperty("URL")]
        public string URL { get; set; }

        public static RespuestaPago FromJson(string json) => JsonConvert.DeserializeObject<RespuestaPago>(json, Converter.Settings);
    }
}
