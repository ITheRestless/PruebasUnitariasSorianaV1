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
            string controlador = "/api/dosearch/sentencia5";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"sentencia\":\"aguacate\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            if (!response.Content.Contains("AGUACATE"))
            {
                Assert.Fail(response.ErrorMessage);
            }
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
            {
                Assert.Fail(response.ErrorMessage);
            }
        }

        [TestMethod]
        public void ProductosPorSubcategoria()
        {
            string controlador = "/api/dosearch/subcategoria4";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"sentencia\":\"\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"13242\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"11374020\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("ItemId"))
            {
                Assert.Fail(response.ErrorMessage);
            }
        }

        [TestMethod]
        public void ArticulosPorPromocion()
        {
            string controlador = "/api/dosearch/articulosPorPromocion";
            string endpoint = urlbase + controlador;
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"sentencia\":\"\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"\",\r\n  \"tag\":\"320480\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            if (!response.Content.Contains("ItemId"))
            {
                Assert.Fail(response.ErrorMessage);
            }
        }
    }
}
