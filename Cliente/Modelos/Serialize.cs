using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente.Modelos
{
    public static class Serialize
    {
        public static string ToJson(this Cliente1 self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this ClienteAlterno self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this BearerToken self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this AuxiliarPassword self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this TarjetaLealtadM self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
