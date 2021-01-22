using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class CanjePuntos
    {
        [JsonProperty("Id_Num_Sku")]
        public long IdNumSku { get; set; }

        [JsonProperty("Cantidad_Unidades")]
        public long CantidadUnidades { get; set; }

        [JsonProperty("Cant_PuntosCanje")]
        public long CantPuntosCanje { get; set; }

        [JsonProperty("Id_Num_PromCanje")]
        public long IdNumPromCanje { get; set; }
    }
}
