using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orden.Modelos;
using RestSharp;
using System;

namespace Orden
{
    [TestClass]
    public class Orden_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        static BearerToken token = new BearerToken();

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        Cliente cliente = RegistrarCliente(clienteTester);

        [TestMethod]
        public void Ordenes_En_Proceso()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/OrdenesEnProceso";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("No tienes pedidos."))
                Assert.Fail();
        }

        [TestMethod]
        public void Detalle_Orden()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/DetalleOrden";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("numOrden", "100863606");
            request.AddHeader("idTiendas", "24");
            request.AddHeader("Page", "1");
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Fecha_Hora_Entrega()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/FechaHora";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idTienda", "24");
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Fecha_Hora_Entrega_2()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/FechaHora2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Forma_Pago()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/FormaPago";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Cancelar_Orden()
        {
            token = ObtenerToken();

            string controlador = "/api/Orden/Cancelar";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"numOrden\": \"9283728\",\"numTransaccion\": \"13847\",\"cancelar\": false}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        // MÉTODO PARA OBTENER EL TOKEN
        public static BearerToken ObtenerToken()
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

        // MÉTODO PARA REGISTRAR CLIENTE
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
