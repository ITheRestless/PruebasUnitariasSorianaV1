using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Orden.Modelos
{
    public class Cliente
    {
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Paterno")]
        public string Paterno { get; set; }

        [JsonProperty("Materno")]
        public string Materno { get; set; }

        [JsonProperty("Mail")]
        public string Mail { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("LoyaltyAccount")]
        public string LoyaltyAccount { get; set; }

        [JsonProperty("CveAcceso")]
        public string CveAcceso { get; set; }

        [JsonProperty("CveRegistro")]
        public string CveRegistro { get; set; }

        [JsonProperty("TarjetaNueva")]
        public string TarjetaNueva { get; set; }

        [JsonProperty("Ticket")]
        public string Ticket { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("Cve_Accion")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CveAccion { get; set; }

        public Cliente(string nombre, string email, string password)
        {
            Nombre = nombre;
            Mail = email;
            Password = password;
            ConfirmPassword = password;
            CveRegistro = ObtenerCodigoRegistro(email);
        }

        public Cliente(string nombre, string email, string password, string apellidoPat, string apellidoMat)
        {
            Nombre = nombre;
            Mail = email;
            Password = password;
            ConfirmPassword = password;
            Paterno = apellidoPat;
            Materno = apellidoMat;
            CveRegistro = ObtenerCodigoRegistro(email);
        }

        public static Cliente FromJson(string json) => JsonConvert.DeserializeObject<Cliente>(json, Converter.Settings);

        public string ObtenerCodigoRegistro(string usr)
        {

            int count1 = usr.Length;
            string count2 = Convert.ToString(Regex.Matches(usr, "a").Count);
            string count3 = Convert.ToString(Regex.Matches(usr, "e").Count);
            string count4 = Convert.ToString(Regex.Matches(usr, "i").Count);
            string count5 = Convert.ToString(Regex.Matches(usr, "o").Count);
            string count6 = Convert.ToString(Regex.Matches(usr, "u").Count);

            var strCode = count1 + count2 + count3 + count4 + count5 + count6;
            return strCode.Substring(0, 6);
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
}
