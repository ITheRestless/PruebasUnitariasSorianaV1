using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrito_Promos_Elegibles
{
    public partial class BearerToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]

        public Guid RefreshToken { get; set; }

        public static BearerToken FromJson(string json) => JsonConvert.DeserializeObject<BearerToken>(json, Converter.Settings);

    }
}
