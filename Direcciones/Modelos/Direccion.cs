using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Direcciones.Modelos
{
    public partial class Direccion
    {
        [JsonProperty("idTienda")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IdTienda { get; set; }

        [JsonProperty("nomDirCte")]
        public string NomDirCte { get; set; }

        [JsonProperty("idTipoDomCte")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IdTipoDomCte { get; set; }

        [JsonProperty("nomRecibeCte")]
        public string NomRecibeCte { get; set; }

        [JsonProperty("idEstadoCte")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IdEstadoCte { get; set; }

        [JsonProperty("nomCiudadCte")]
        public string NomCiudadCte { get; set; }

        [JsonProperty("nomColoniaCte")]
        public string NomColoniaCte { get; set; }

        [JsonProperty("nomCalleCte")]
        public string NomCalleCte { get; set; }

        [JsonProperty("numExtCalleCte")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NumExtCalleCte { get; set; }

        [JsonProperty("numIntCalleCte_Opc")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NumIntCalleCteOpc { get; set; }

        [JsonProperty("CPCte")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CpCte { get; set; }

        [JsonProperty("telefonoCte")]
        public string TelefonoCte { get; set; }

        [JsonProperty("idCnscDirCte")]
        public long IdCnscDirCte { get; set; }
    }

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
