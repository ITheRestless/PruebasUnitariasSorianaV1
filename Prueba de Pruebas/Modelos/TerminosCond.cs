using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class TerminosCond
    {
        [JsonProperty("Fec_AceptaTermCond")]
        public DateTimeOffset FecAceptaTermCond { get; set; }
    }
}
