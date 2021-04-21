using Estracto_Por_Categoria_Padre.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace Estracto_Por_Categoria_Padre
{
    [TestClass]
    public class EstractoPorCategoriaPadre_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        [TestMethod]
        public void Subcategoria_Productos()
        {
            string controlador = "/api/categoria/subcategoria_productosPage";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("idCategoria", "13216");
            request.AddHeader("Page", "2");
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("Categorias"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }
    }
}
