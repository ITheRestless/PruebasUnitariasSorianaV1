using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Listas.Modelos
{
    public partial class Table
    {
        [JsonProperty("numError")]
        public long NumError { get; set; }

        [JsonProperty("descError")]
        public string DescError { get; set; }

        [JsonProperty("descErrorCte")]
        public string DescErrorCte { get; set; }
    }
}
