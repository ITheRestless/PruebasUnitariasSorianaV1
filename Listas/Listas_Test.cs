using Listas.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Listas
{
    [TestClass]
    public class Listas_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        BearerToken token = new BearerToken();

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "testerlistas" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "testerlistas" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        Cliente cliente = RegistrarCliente(clienteTester);

        List<RespuestaCrearLista> listaCrearLista = new List<RespuestaCrearLista>();

        [TestMethod]
        public void Crear_Lista()
        {
            token = ObtenerToken();

            string controlador = "/api/ListaCompra/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("nombre", token.AccessToken.Substring(0, 5));
            request.AddHeader("descripcion", "x3");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            listaCrearLista = null;
            listaCrearLista = RespuestaCrearLista.FromJson(response.Content);

            if (!response.Content.Contains(token.AccessToken.Substring(0, 5)))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Listas()
        {
            token = ObtenerToken();
            CrearLista();

            string controlador = "/api/ListaCompra/GetListas";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);
            
            if (!response.Content.Contains(token.AccessToken.Substring(0, 5)))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Agregar_A_Lista()
        {
            token = ObtenerToken();
            CrearLista();

            List<Articulo> listaArticulos = new List<Articulo>();
            listaArticulos.Add(new Articulo()
            {
                articulo = 245888,
                cantidad = 4,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 521,
                cantidad = 2,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 245890,
                cantidad = 3,
                idLista = listaCrearLista[0].IdLista
            });

            string controlador = "/api/ListaCompra/AgregarArticulosALista";
            string endpoint = urlbase + controlador;
            
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", listaArticulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Detalle_Lista()
        {
            token = ObtenerToken();
            AgregarALista();
            List<RespuestaCrearLista> RespuestaCrearList = listaCrearLista;
            
            string controlador = "/api/ListaCompra/Detalle";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idLista", RespuestaCrearList[0].IdLista.ToString());
            request.AddHeader("idTienda", "24");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            if (!response.Content.Contains(RespuestaCrearList[0].IdLista.ToString()))
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Articulo_Lista()
        {
            token = ObtenerToken();
            CrearLista();

            List<Articulo> eliminarArticulos = new List<Articulo>()
            {
                new Articulo()
                {
                    IdNumSKU = 1229505,
                    idNumLstComp = listaCrearLista[0].IdLista
                },
                new Articulo()
                {
                    IdNumSKU = 2336087
                }
            };

            string controlador = "/api/ListaCompra/EliminarArticulos2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", eliminarArticulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Lista()
        {
            token = ObtenerToken();
            CrearLista();

            string controlador = "/api/ListaCompra/Delete";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idLista", listaCrearLista[0].IdLista.ToString());
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

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

        // MÉTODO PARA CREAR LISTA
        public void CrearLista()
        {
            token = ObtenerToken();

            string controlador = "/api/ListaCompra/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("nombre", token.AccessToken.Substring(0, 5));
            request.AddHeader("descripcion", "x3");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            listaCrearLista = null;
            listaCrearLista = RespuestaCrearLista.FromJson(response.Content);
        }

        // MÉTODO PARA AÑADIR A LISTA
        public void AgregarALista()
        {
            token = ObtenerToken();
            CrearLista();

            List<Articulo> listaArticulos = new List<Articulo>();
            listaArticulos.Add(new Articulo()
            {
                articulo = 245888,
                cantidad = 4,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 521,
                cantidad = 2,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 245890,
                cantidad = 3,
                idLista = listaCrearLista[0].IdLista
            });

            string controlador = "/api/ListaCompra/AgregarArticulosALista";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", listaArticulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
        }
    }
}
