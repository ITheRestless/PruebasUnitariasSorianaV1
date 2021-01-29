using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class Articulo
    {
        [JsonProperty("articulo")]
        public long articulo { get; set; }

        [JsonProperty("cantidad")]
        public long cantidad { get; set; }

        [JsonProperty("unidadMedida")]
        public long unidadMedida { get; set; }

        [JsonProperty("idNumVisita")]
        public long idNumVisita { get; set; }

        [JsonProperty("idTienda")]
        public long idTienda { get; set; }

        [JsonProperty("idLista")]
        public long idLista { get; set; }

        [JsonProperty("Id_Num_SKU")]
        public long IdNumSKU { get; set; }
        
        [JsonProperty("id_Num_LstComp")]
        public long idNumLstComp { get; set; }
    }

    public partial class Articulo2
    {
        [JsonProperty("articulo")]
        public long articulo1 { get; set; }

        [JsonProperty("cantidad")]
        public long cantidad1 { get; set; }

        [JsonProperty("idLista")]
        public long idLista1 { get; set; }

        public Articulo2(long articulo, long cantidad, long idLista)
        {
            articulo1 = articulo;
            cantidad1 = cantidad;
            idLista1 = idLista;
        }
    }

    public partial class ComentarioArticulo
    {
        [JsonProperty("idArticulo")]
        public long IdArticulo { get; set; }

        [JsonProperty("descComentario_Opc")]
        public string DescComentarioOpc { get; set; }

        [JsonProperty("idTienda")]
        public long IdTienda { get; set; }
    }
}
