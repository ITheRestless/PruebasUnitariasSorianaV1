using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
    public Horario Horario { get; set; }

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
    public NomPais NomPais { get; set; }

    [JsonProperty("Nom_Estado")]
    public string NomEstado { get; set; }

    [JsonProperty("Nom_Poblacion")]
    public string NomPoblacion { get; set; }

    public static List<PoblacionTienda> FromJson(string json) => JsonConvert.DeserializeObject<List<PoblacionTienda>>(json, Converter.Settings);
}

public enum Horario { The07002300 };

public enum NomPais { Mexico };

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                HorarioConverter.Singleton,
                NomPaisConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}

internal class HorarioConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(Horario) || t == typeof(Horario?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        if (value == "07:00 - 23:00")
        {
            return Horario.The07002300;
        }
        throw new Exception("Cannot unmarshal type Horario");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (Horario)untypedValue;
        if (value == Horario.The07002300)
        {
            serializer.Serialize(writer, "07:00 - 23:00");
            return;
        }
        throw new Exception("Cannot marshal type Horario");
    }

    public static readonly HorarioConverter Singleton = new HorarioConverter();
}

internal class NomPaisConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(NomPais) || t == typeof(NomPais?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        if (value == "MEXICO")
        {
            return NomPais.Mexico;
        }
        throw new Exception("Cannot unmarshal type NomPais");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (NomPais)untypedValue;
        if (value == NomPais.Mexico)
        {
            serializer.Serialize(writer, "MEXICO");
            return;
        }
        throw new Exception("Cannot marshal type NomPais");
    }

    public static readonly NomPaisConverter Singleton = new NomPaisConverter();
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