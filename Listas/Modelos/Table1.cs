﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Listas.Modelos
{
    public partial class Table1
    {
        [JsonProperty("idLista")]
        public long IdLista { get; set; }

        [JsonProperty("nombreLista")]
        public string NombreLista { get; set; }

        [JsonProperty("descLista")]
        public string DescLista { get; set; }

        [JsonProperty("Fec_AltaLstComp")]
        public DateTimeOffset FecAltaLstComp { get; set; }

        [JsonProperty("Fec_UltModifLstComp")]
        public DateTimeOffset FecUltModifLstComp { get; set; }

        [JsonProperty("Cant_TotArt")]
        public long CantTotArt { get; set; }
    }
}
