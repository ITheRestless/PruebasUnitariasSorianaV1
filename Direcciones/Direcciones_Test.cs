using Direcciones.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace Direcciones
{
    [TestClass]
    public class Direcciones_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        BearerToken token = new BearerToken();
        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        Direccion direccion = new Direccion();

        Cliente cliente = RegistrarCliente(clienteTester);

        [TestMethod]
        public void Nueva_Direccion()
        {
            token = ObtenerToken();

            Direccion nuevaDireccion = new Direccion()
            {
                IdTienda = 24,
                NomDirCte = "La oficina prueba",
                IdTipoDomCte = 1,
                NomRecibeCte = "Jesús",
                IdEstadoCte = 19,
                NomCiudadCte = "Monterrey",
                NomColoniaCte = "Cumbres",
                NomCalleCte = "Sector 8",
                NumExtCalleCte = 1913,
                NumIntCalleCteOpc = 1913,
                CpCte = 64610,
                TelefonoCte = "8711182334"
            };
            direccion = nuevaDireccion;

            string controlador = "/api/direccion/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("oficina prueba"))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Modificar_Direccion()
        {
            token = ObtenerToken();

            Direccion viejaDireccion = NuevaDireccion();
            Direccion nuevaDireccion = new Direccion();

            nuevaDireccion = viejaDireccion;
            nuevaDireccion.NomDirCte = "La oficina editada " + DateTime.Now;
            nuevaDireccion.NomColoniaCte = "Cumbres dos";
            nuevaDireccion.NumIntCalleCteOpc = 113;
            nuevaDireccion.CpCte = 27054;
            nuevaDireccion.IdCnscDirCte = 2;

            string controlador = "/api/direccion/modificar";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            if (!response.Content.Contains("editada"))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Direcciones()
        {
            token = ObtenerToken();
            AgregarDireccion();

            string controlador = "/api/direccion/GetDirecciones";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("oficina"))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Direccion()
        {
            token = ObtenerToken();
            NuevaDireccion();

            string controlador = "/api/direccion/Delete";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idDireccion", "1");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Poblacion()
        {
            token = ObtenerToken();

            string controlador = "/api/direccion/poblacion";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("CP", "64610");
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Validar_CP()
        {
            string controlador = "/api/direccion/validarCP";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("CP", "64610");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
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

        // MÉTODO PARA UNA NUEVA DIRECCIÓN()
        public Direccion NuevaDireccion()
        {
            token = ObtenerToken();

            Direccion nuevaDireccion = new Direccion()
            {
                IdTienda = 24,
                NomDirCte = "La oficina prueba",
                IdTipoDomCte = 1,
                NomRecibeCte = "Jesús",
                IdEstadoCte = 19,
                NomCiudadCte = "Monterrey",
                NomColoniaCte = "Cumbres",
                NomCalleCte = "Sector 8",
                NumExtCalleCte = 1913,
                NumIntCalleCteOpc = 1913,
                CpCte = 64610,
                TelefonoCte = "8711182334"
            };
            direccion = nuevaDireccion;

            string controlador = "/api/direccion/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return direccion;
        }

        public void AgregarDireccion()
        {
            token = ObtenerToken();

            Direccion nuevaDireccion = new Direccion()
            {
                IdTienda = 24,
                NomDirCte = "La oficina prueba",
                IdTipoDomCte = 1,
                NomRecibeCte = "Jesús",
                IdEstadoCte = 19,
                NomCiudadCte = "Monterrey",
                NomColoniaCte = "Cumbres",
                NomCalleCte = "Sector 8",
                NumExtCalleCte = 1913,
                NumIntCalleCteOpc = 1913,
                CpCte = 64610,
                TelefonoCte = "8711182334"
            };
            direccion = nuevaDireccion;

            string controlador = "/api/direccion/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
        }
    }
}
