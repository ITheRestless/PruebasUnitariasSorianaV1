using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class Persona
    {
        [JsonProperty("nomCte")]
        public string NomCte { get; set; }

        [JsonProperty("apPaternoCte")]
        public string ApPaternoCte { get; set; }

        [JsonProperty("apMaternoCte")]
        public string ApMaternoCte { get; set; }

        [JsonProperty("telefonoCtev")]
        public string TelefonoCtev { get; set; }

        [JsonProperty("celularCtev")]
        public string CelularCtev { get; set; }

        [JsonProperty("idCte")]
        public string IdCte { get; set; }

        [JsonProperty("numTarjCteLealtad")]

        public string NumTarjCteLealtad { get; set; }

        [JsonProperty("numTarjCteLealtadC")]
        public string NumTarjCteLealtadC { get; set; }

        [JsonProperty("telOficina")]
        public string TelOficina { get; set; }

        [JsonProperty("extOficina")]
        public object ExtOficina { get; set; }
    }
}
