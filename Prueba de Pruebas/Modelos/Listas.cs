using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class Listas
    {
        [JsonProperty("Table")]
        public List<Table> Table { get; set; }

        [JsonProperty("Table1")]
        public List<Table1> Table1 { get; set; }
        public static Listas FromJson(string json) => JsonConvert.DeserializeObject<Listas>(json, Converter.Settings);
    }
}
