using DoSearch.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Diagnostics;

namespace DoSearch
{
    [TestClass]
    public class DoSearch_Test
    {
        string urlbase = "https://appsor02.soriana.com";

        [TestMethod]
        public void BusquedaPorSentencia()
        {
            string controlador = "/api/dosearch/sentencia6";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"sentencia\":\"aguacate\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK || !response.Content.Contains("AGUACATE"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void VerificadorPrecios()
        {
            string controlador = "/api/dosearch/GetItem3";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\n\"CliId\":\"\",\n\"StoreId\":\"24\",\n\"ItemId\":\"7501055367498\"\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("COCA"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void ProductosPorSubcategoria()
        {
            string controlador = "/api/dosearch/subcategoria5";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"sentencia\":\"\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"13242\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"11374020\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("ItemId"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void ArticulosPorPromocion()
        {
            Promociones promociones = get_Estracto_Promociones();
            string tag = tag_valido(promociones);

            string controlador = "/api/dosearch/articulosPorPromocion2";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{"
                    + "\n" +
                    @"    ""sentencia"":"""","
                    + "\n" +
                    @"    ""page"":""1"", "
                    + "\n" +
                    @"    ""idTienda"":""24"", "
                    + "\n" +
                    @"    ""brandId"":"""", "
                    + "\n" +
                    @"    ""categoryId"":"""", "
                    + "\n" +
                    @"    ""orderType"":""0"", "
                    + "\n" +
                    @"    ""promotionId"":"""", "
                    + "\n" +
                    @"    ""tag"":"" " + tag + " \" "
                    + "\n" +
                    @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

            if (!response.Content.Contains("ItemId"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }


        // MÉTODO PARA OBTENER UN TAG DE PROMOCIÓN VÁLIDO
        public string tag_valido(Promociones promociones)
        {
            string tag = "";

            if (promociones.Categorias != null)
            {
                for (int i = 0; i < promociones.Categorias.Count; i++)
                {
                    tag = promociones.Categorias[i].Tag;

                    string controlador = "/api/dosearch/articulosPorPromocion2";
                    string endpoint = urlbase + controlador;

                    var client = new RestClient(endpoint);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    var body = @"{"
                    + "\n" +
                    @"    ""sentencia"":"""","
                    + "\n" +
                    @"    ""page"":""1"", "
                    + "\n" +
                    @"    ""idTienda"":""24"", "
                    + "\n" +
                    @"    ""brandId"":"""", "
                    + "\n" +
                    @"    ""categoryId"":"""", "
                    + "\n" +
                    @"    ""orderType"":""0"", "
                    + "\n" +
                    @"    ""promotionId"":"""", "
                    + "\n" +
                    @"    ""tag"":"" " + tag + " \" "
                    + "\n" +
                    @"}";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        break;
                }

                return tag;
            }
            else
                return "";
        }

        // MÉTODO PARA OBTENER UNA PROMOCIÓN ACTIVA
        public Promociones get_Estracto_Promociones()
        {
            string controlador = "/api/categoria/GetEstractoPromocionesPage2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");
            request.AddHeader("Page", "1");

            IRestResponse response = client.Execute(request);

            Promociones promociones = Promociones.FromJson(response.Content);
            return promociones;
        }
    }
}
