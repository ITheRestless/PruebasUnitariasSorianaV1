using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class AuxiliarPassword
    {
        [JsonProperty("OldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("NewPassword")]
        public string NewPassword { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public static AuxiliarPassword FromJson(string json) => JsonConvert.DeserializeObject<AuxiliarPassword>(json, Converter.Settings);
    }
}
