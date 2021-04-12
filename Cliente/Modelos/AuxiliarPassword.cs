using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cliente.Modelos
{
    public partial class AuxiliarPassword
    {
        [JsonProperty("OldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("NewPassword")]
        public string NewPassword { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public static AuxiliarPassword FromJson(string json) => JsonConvert.DeserializeObject<AuxiliarPassword>(json, Converter.Settings);
    }
}
