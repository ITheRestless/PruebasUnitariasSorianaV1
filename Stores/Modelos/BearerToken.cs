using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Stores.Modelos
{
    public class BearerToken
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
