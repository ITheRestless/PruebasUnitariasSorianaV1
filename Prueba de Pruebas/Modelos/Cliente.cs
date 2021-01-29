using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
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

    }
}
