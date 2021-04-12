using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrito_Promos_Elegibles.Modelos
{
    public static class Serialize
    {
        public static string ToJson(this List<Articulo> self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this ClienteAlterno self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this Cliente self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this ComentarioArticulo self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this CanjePuntos self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
