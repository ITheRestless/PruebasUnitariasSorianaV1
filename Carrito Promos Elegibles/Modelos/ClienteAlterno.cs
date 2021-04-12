using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrito_Promos_Elegibles.Modelos
{
    public partial class ClienteAlterno
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("ApellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty("ApellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("HSId")]
        public object HsId { get; set; }

        [JsonProperty("TokenDevelop")]
        public object TokenDevelop { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword
        {
            get

            { return Password; }
        }

        [JsonProperty("FechaNacimiento")]
        public object FechaNacimiento { get; set; }

        [JsonProperty("clientId")]
        public object ClientId { get; set; }

        [JsonProperty("Genero")]
        public object Genero { get; set; }

        [JsonProperty("LoyaltyAccount")]
        public object LoyaltyAccount { get; set; }

        [JsonProperty("ValidationTicketId")]
        public object ValidationTicketId { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("TelefonoCelular")]
        public string TelefonoCelular { get; set; }

        public ClienteAlterno(string nombre, string email, string password, string id)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Id = id;
        }
        public ClienteAlterno(string nombre, string email, string password)
        {
            Nombre = nombre;
            Email = email;
            Password = password;

        }
        public ClienteAlterno(string nombre, string email, string password, string apellidoPat, string apellidoMat)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            ApellidoMaterno = apellidoMat;
            ApellidoPaterno = apellidoPat;
        }
        public static ClienteAlterno FromJson(string json) => JsonConvert.DeserializeObject<ClienteAlterno>(json, Converter.Settings);
    }
}
