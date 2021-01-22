using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class AppConfig
    {
        [JsonProperty("Latitud")]
        public string Latitud { get; set; }

        [JsonProperty("Longitud")]
        public string Longitud { get; set; }

        [JsonProperty("tokenDispositivo")]
        public string TokenDispositivo { get; set; }

        [JsonProperty("idTipoEntrega")]
        public long IdTipoEntrega { get; set; }

        [JsonProperty("nomTipoEntrega")]
        public string NomTipoEntrega { get; set; }

        [JsonProperty("idTienda")]
        public long IdTienda { get; set; }

        [JsonProperty("nomTienda")]
        public string NomTienda { get; set; }

        [JsonProperty("idEstado")]
        public long IdEstado { get; set; }

        [JsonProperty("nomEstadoTienda")]
        public string NomEstadoTienda { get; set; }

        [JsonProperty("idCiudad")]
        public long IdCiudad { get; set; }

        [JsonProperty("nomCiudadTienda")]
        public string NomCiudadTienda { get; set; }
    }
}
