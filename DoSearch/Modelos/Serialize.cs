using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DoSearch.Modelos
{
    static class Serialize
    {
        public static string ToJson(this Categoria self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
