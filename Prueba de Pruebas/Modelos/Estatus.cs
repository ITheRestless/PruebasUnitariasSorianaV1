using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class Estatus
    {
        [JsonProperty("numError")]
        public long NumError { get; set; }

        [JsonProperty("descError")]
        public string DescError { get; set; }

        [JsonProperty("descErrorCte")]
        public string DescErrorCte { get; set; }
    }
}
