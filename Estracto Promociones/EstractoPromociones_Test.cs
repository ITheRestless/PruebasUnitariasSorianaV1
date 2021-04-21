using Estracto_Promociones.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace Estracto_Promociones
{
    [TestClass]
    public class EstractoPromociones_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        [TestMethod]
        public void Estracto_Promociones()
        {
            string controlador = "/api/categoria/GetEstractoPromocionesPage";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idTienda", "24");
            request.AddHeader("Page", "1");

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("Categorias"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }
    }
}
