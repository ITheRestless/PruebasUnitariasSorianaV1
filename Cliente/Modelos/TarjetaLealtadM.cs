using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cliente.Modelos
{
    public partial class TarjetaLealtadM
    {
        [JsonProperty("LoyaltyAccount")]
        public string LoyaltyAccount { get; set; }

        [JsonProperty("TelefonoCelular")]
        public string TelefonoCelular { get; set; }

        [JsonProperty("Ticket")]
        public string Ticket { get; set; }

        [JsonProperty("Mail")]
        public string Mail { get; set; }

        public static TarjetaLealtadM FromJson(string json) => JsonConvert.DeserializeObject<TarjetaLealtadM>(json, Converter.Settings);
    }
}
