using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Sin_Categoria.Modelos;
using System;

namespace Sin_Categoria
{
    [TestClass]
    public class SinCategoria_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        [TestMethod]
        public void Obtener_Folletos()
        {
            string controlador = "/api/Regiones/Get";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Region", "6");
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Home_5()
        {
            string controlador = "/api/homeSuper/getHome5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.Content.Contains("No hay categorías para cargar"))
                response.StatusCode = System.Net.HttpStatusCode.OK;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod] 
        public void Obtener_Home_6()
        {
            string controlador = "/api/homeSuper/getHome6";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.Content.Contains("No hay categorías para cargar"))
                response.StatusCode = System.Net.HttpStatusCode.OK;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Promociones()
        {
            string controlador = "/api/homeSuper/promociones";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Promociones_2()
        {
            string controlador = "/api/homeSuper/promociones2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Tickets()
        {
            string controlador = "/api/Tickets/GetTickets";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"CliId\":\"113737915\",\"PageNumber\":1,\"ItemsByPage\":10}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Estructura_Comercial()
        {
            string controlador = "/api/categoria/Estructura";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Regiones()
        {
            string controlador = "/api/regiones/get3";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

    }
}
