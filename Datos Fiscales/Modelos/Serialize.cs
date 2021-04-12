﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Datos_Fiscales.Modelos
{
    static class Serialize
    {
        public static string ToJson(this BearerToken self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Cliente self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this DatosFiscales self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this ClienteAlterno self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
