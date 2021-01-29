using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    public static class Serialize
    {
        public static string ToJson(this BearerToken self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Listas self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this List<RespuestaCrearLista> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Cliente self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this AuxiliarPassword self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this TarjetaLealtadM self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this DatosFiscales self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this List<Articulo> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Orden self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Direccion self) => JsonConvert.SerializeObject(self, Converter.Settings); 
        public static string ToJson(this ComentarioArticulo self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this CanjePuntos self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this ClienteAlterno self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this List<PoblacionTienda> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
