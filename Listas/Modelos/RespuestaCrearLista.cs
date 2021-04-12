using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Listas.Modelos
{
    public class RespuestaCrearLista
    {
        [JsonProperty("idLista")]
        public long IdLista { get; set; }

        [JsonProperty("nombreLista")]
        public string NombreLista { get; set; }

        [JsonProperty("descLista")]
        public string DescLista { get; set; }

        [JsonProperty("descripLista")]
        public object DescripLista { get; set; }

        [JsonProperty("Pagina")]
        public object Pagina { get; set; }

        [JsonProperty("cantRegPag")]
        public object CantRegPag { get; set; }

        [JsonProperty("totalPaginas")]
        public object TotalPaginas { get; set; }

        [JsonProperty("numRegistros")]
        public object NumRegistros { get; set; }

        [JsonProperty("Cant_TotArt")]
        public object CantTotArt { get; set; }

        [JsonProperty("Fec_AltaLstComp")]
        public DateTimeOffset FecAltaLstComp { get; set; }

        [JsonProperty("Fec_UltModifLstComp")]
        public DateTimeOffset FecUltModifLstComp { get; set; }

        public static List<RespuestaCrearLista> FromJson(string json) => JsonConvert.DeserializeObject<List<RespuestaCrearLista>>(json, Converter.Settings);
    }
}
