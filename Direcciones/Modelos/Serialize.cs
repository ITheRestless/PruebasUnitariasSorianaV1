using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Direcciones.Modelos
{
    static class Serialize
    {
        public static string ToJson(this ClienteAlterno self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this Cliente self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this Direccion self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
