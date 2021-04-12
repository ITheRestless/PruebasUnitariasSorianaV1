using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Listas.Modelos
{
    public partial class Lista
    {
        [JsonProperty("Table")]
        public List<Table> Table { get; set; }

        [JsonProperty("Table1")]
        public List<Table1> Table1 { get; set; }
        public static Lista FromJson(string json) => JsonConvert.DeserializeObject<Lista>(json, Converter.Settings);
    }
}
