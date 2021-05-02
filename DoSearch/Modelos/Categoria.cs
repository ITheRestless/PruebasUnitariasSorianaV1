using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DoSearch.Modelos
{
    public partial class Promociones
    {
        [JsonProperty("Categorias")]
        public List<Categoria> Categorias { get; set; }

        [JsonProperty("Promociones")]
        public List<Promocion> PromocionesPromociones { get; set; }

        public static Promociones FromJson(string json) => JsonConvert.DeserializeObject<Promociones>(json, Converter.Settings);
    }

    public partial class Categoria
    {
        [JsonProperty("idCategoria")]
        public long IdCategoria { get; set; }

        [JsonProperty("NombreCategoria")]
        public string NombreCategoria { get; set; }

        [JsonProperty("idCategoriaPadre")]
        public long IdCategoriaPadre { get; set; }

        [JsonProperty("urlImg")]
        public object UrlImg { get; set; }

        [JsonProperty("Categorias")]
        public object Categorias { get; set; }

        [JsonProperty("Articulos")]
        public List<Articulo> Articulos { get; set; }

        [JsonProperty("Tag")]
        public string Tag { get; set; }

        [JsonProperty("AccionVisible")]
        public bool AccionVisible { get; set; }

        public static Categoria FromJson(string json) => JsonConvert.DeserializeObject<Categoria>(json, Converter.Settings);
    }

    public partial class Articulo
    {
        [JsonProperty("Promotions")]
        public List<long> Promotions { get; set; }

        [JsonProperty("Index")]
        public long Index { get; set; }

        [JsonProperty("Ranking")]
        public long Ranking { get; set; }

        [JsonProperty("ItemId")]
        public string ItemId { get; set; }

        [JsonProperty("Sku")]
        public long Sku { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("CategoryId")]
        public long CategoryId { get; set; }

        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("BrandId")]
        public long BrandId { get; set; }

        [JsonProperty("BrandName")]
        public string BrandName { get; set; }

        [JsonProperty("SalesUnit")]
        public long SalesUnit { get; set; }

        [JsonProperty("PublicationDate")]
        public DateTimeOffset PublicationDate { get; set; }

        [JsonProperty("UrlImage")]
        public Uri UrlImage { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTimeOffset UpdateDate { get; set; }

        [JsonProperty("NormalSalePrice")]
        public double NormalSalePrice { get; set; }

        [JsonProperty("OfferSalePrice")]
        public long OfferSalePrice { get; set; }

        [JsonProperty("OfferEntryDate")]
        public DateTimeOffset OfferEntryDate { get; set; }

        [JsonProperty("OfferExitDate")]
        public DateTimeOffset OfferExitDate { get; set; }

        [JsonProperty("DiscountPercentage")]
        public long DiscountPercentage { get; set; }

        [JsonProperty("IVA")]
        public long Iva { get; set; }

        [JsonProperty("IEPS")]
        public long Ieps { get; set; }

        [JsonProperty("Templates")]
        public object Templates { get; set; }

        [JsonProperty("ConversionRule")]
        public object ConversionRule { get; set; }

        [JsonProperty("ExtendedDescription")]
        public string ExtendedDescription { get; set; }

        [JsonProperty("AdditionalImages")]
        public List<object> AdditionalImages { get; set; }

        [JsonProperty("Variants")]
        public List<object> Variants { get; set; }

        public static Articulo FromJson(string json) => JsonConvert.DeserializeObject<Articulo>(json, Converter.Settings);
    }

    public partial class Promocion
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("DateIn")]
        public DateTimeOffset DateIn { get; set; }

        [JsonProperty("DateOut")]
        public DateTimeOffset DateOut { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }

        [JsonProperty("Url")]
        public object Url { get; set; }

        [JsonProperty("Elegible")]
        public bool Elegible { get; set; }

        [JsonProperty("Puntos_Necesarios")]
        public long PuntosNecesarios { get; set; }

        [JsonProperty("Prec_Canje")]
        public object PrecCanje { get; set; }

        [JsonProperty("bitPromocion")]
        public bool BitPromocion { get; set; }

        public static Promocion FromJson(string json) => JsonConvert.DeserializeObject<Promocion>(json, Converter.Settings);
    }
}
