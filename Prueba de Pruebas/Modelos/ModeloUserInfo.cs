using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class ModeloUserInfo
    {
        [JsonProperty("Estatus")]
        public Estatus Estatus { get; set; }

        [JsonProperty("Persona")]
        public Persona Persona { get; set; }

        [JsonProperty("Direccion")]
        public List<object> Direccion { get; set; }

        [JsonProperty("Cuentas")]
        public List<Cuenta> Cuentas { get; set; }

        [JsonProperty("DatosFiscales")]
        public List<object> DatosFiscales { get; set; }

        [JsonProperty("AppConfig")]
        public AppConfig AppConfig { get; set; }

        [JsonProperty("TerminosCond")]
        public TerminosCond TerminosCond { get; set; }

        public static ModeloUserInfo FromJson(string json) => JsonConvert.DeserializeObject<ModeloUserInfo>(json, Converter.Settings);
    }
}
