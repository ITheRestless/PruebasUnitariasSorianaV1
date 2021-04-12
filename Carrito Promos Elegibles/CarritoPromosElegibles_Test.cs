using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using Carrito_Promos_Elegibles.Modelos;
using System.Collections.Generic;

namespace Carrito_Promos_Elegibles
{
    [TestClass]
    public class CarritoPromosElegibles_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        BearerToken token = new BearerToken();
        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        int visita = 0;

        Cliente cliente = RegistrarCliente(clienteTester);

        [TestMethod]
        public void Crear_Visita()
        {
            token = ObtenerToken();

            string controlador = "/api/Carrito/visita";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            visita = Convert.ToInt32(response.Content);

            if (visita == 0)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Agregar_Articulo_A_Carrito()
        {
            token = ObtenerToken();
            visita = ObtenerVisita();

            string controlador = "/api/carrito/agregarArticulo5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", "{\r\n \"articulo\" : 245888,\r\n \"cantidad\" : 5,\r\n \"unidadMedida\" : 2,\r\n \"idNumVisita\" : 382423099,\r\n \"idTienda\" : 24\r\n }".Replace("382423099", visita.ToString()), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("GELATINA"))
                Assert.Fail();
        }

        [TestMethod]
        public void Obtener_Detalle_Carrito()
        {
            token = ObtenerToken();

            string controlador = "/api/Carrito/detalleCarrito5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Eliminar_Articulo_Carrito()
        {
            token = ObtenerToken();

            string controlador = "/api/carrito/borrarArticulo5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idArticulo", "245888");
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.Content.Contains("GELATINA") && response.Content.Contains("idCarrito"))
                Assert.Fail();
        }

        [TestMethod]
        public void Cambiar_Tienda()
        {
            token = ObtenerToken();
            visita = ObtenerVisita();

            string controlador = "/api/carrito/CambioTienda5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", "{\n                \"NumeroAplicacion\" : \"23\",\n                \"NumeroTiendaNueva\":\"14\",\n                \"NumeroCarritoActual\": \"0\",\n                \"NumeroVisita\" : \"340990345\"\n}".Replace("340990345", visita.ToString()), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("idCarrito"))
                Assert.Fail();
        }

        [TestMethod]
        public void Agregar_Arreglo_Carrito()
        {
            token = ObtenerToken();

            string controlador = "/api/carrito/agregarNarticulosCarrito5";
            string endpoint = urlbase + controlador;

            List<Articulo> articulos = new List<Articulo>()
            {
                new Articulo()
                {
                     articulo=170,
                     cantidad = 1,
                     idNumVisita = 349591956,
                     idTienda = 25,
                     unidadMedida = 1,
                },
                 new Articulo()
                {
                     articulo=456,
                     cantidad = 1,
                     idNumVisita = 349591956,
                     idTienda = 25,
                     unidadMedida = 1,
                },
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Comentario_Articulo()
        {
            token = ObtenerToken();

            ComentarioArticulo comentarioArticulo = new ComentarioArticulo()
            {
                IdArticulo = 8003490,
                DescComentarioOpc = "Comentario de pruebas unitarias por el usuario " + clienteTester.Mail,
                IdTienda = 24
            };

            string controlador = "/api/carrito/comentarioArticulo4";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", comentarioArticulo.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Canje_Puntos()
        {
            token = ObtenerToken();

            CanjePuntos canjePuntos = new CanjePuntos()
            {
                IdNumSku = 1024595,
                CantidadUnidades = 1,
                CantPuntosCanje = 270,
                IdNumPromCanje = 11376930
            };

            string controlador = "/api/carrito/CanjePuntos3";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idTienda", "24");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", canjePuntos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        // MÉTODO PARA OBTENER EL TOKEN
        public BearerToken ObtenerToken()
        {
            string controlador = "/api/token/GetToken";
            string endpoint = urlbase + controlador;
                
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");
            request.AddParameter("application/json", clienteTester2.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return token = BearerToken.FromJson(response.Content);
        }

        // MÉTODO PARA OBTENER LA VISITA DEL CLIENTE
        public int ObtenerVisita()
        {
            token = ObtenerToken();

            string controlador = "/api/Carrito/visita";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            visita = Convert.ToInt32(response.Content);

            return visita;
        }

        // MÉTODO PARA REGISTRAR CLIENTE()
        public static Cliente RegistrarCliente(Cliente cliente)
        {
            string controlador = "/api/account/RegisterNew";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("disp", "Android");
            request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return cliente;
        }
    }
}
