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

        static BearerToken token = new BearerToken();

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        Cliente cliente = RegistrarCliente(clienteTester);

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
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
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
