using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public partial class PoblacionTienda
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LogoPath")]
        public string LogoPath { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("image")]
        public object Image { get; set; }

        [JsonProperty("Radio")]
        public long Radio { get; set; }

        [JsonProperty("Id_Num_Logo")]
        public long IdNumLogo { get; set; }

        [JsonProperty("TipoTienda")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TipoTienda { get; set; }

        [JsonProperty("Bit_Serv")]
        public bool BitServ { get; set; }

        [JsonProperty("Bit_Reco")]
        public bool BitReco { get; set; }

        [JsonProperty("Horario")]
        public Horario horario { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("Direccion")]
        public string Direccion { get; set; }

        [JsonProperty("Region")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Region { get; set; }

        [JsonProperty("Ids_Num_Poblacion")]
        public long IdsNumPoblacion { get; set; }

        [JsonProperty("Id_Num_Pais")]
        public long IdNumPais { get; set; }

        [JsonProperty("Id_Num_Estado")]
        public long IdNumEstado { get; set; }

        [JsonProperty("Id_Num_Poblacion")]
        public long IdNumPoblacion { get; set; }

        [JsonProperty("Nom_Pais")]
        public NomPais Nom_Pais { get; set; }

        [JsonProperty("Nom_Estado")]
        public string NomEstado { get; set; }

        [JsonProperty("Nom_Poblacion")]
        public string NomPoblacion { get; set; }

        public enum Horario { The07002300 };

        public enum NomPais { Mexico };

        public static List<PoblacionTienda> FromJson(string json) => JsonConvert.DeserializeObject<List<PoblacionTienda>>(json, Converter.Settings);

        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }

            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }
    }

}
