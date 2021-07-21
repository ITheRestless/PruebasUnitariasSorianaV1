using Favoritos.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Favoritos
{
    [TestClass]
    public class Favoritos_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        #region Cliente

        /// <summary>
        /// Variables para decidir si se registrará un nuevo usario para la prueba o si será usado el
        /// usuario existente, en caso de que el numero aleatorio sea entre 0 y 3 se registrará un
        /// nuevo usuario, caso contrario se usará el usuario existente.
        /// </summary>
        static Random random = new Random();
        static int numero = random.Next(0, 10);
        static bool opc = numero > 3 ? true : false;

        /// <summary>
        /// Estructura para crear un usuario
        /// </summary>
        static string NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        static Cliente clienteNuevo = new Cliente("Iván " + NombreTester, "favoritos_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteNuevo2 = new ClienteAlterno("Iván " + NombreTester, "favoritos_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Cliente ya existente dentro de la aplicación
        /// </summary>
        static Cliente clienteExistente = new Cliente("Iván Alejandro", "favoritos_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteExistente2 = new ClienteAlterno("Iván Alejandro", "favoritos_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Asignación del cliente a usar con respecto a la decisión anterior con números aleatorios.
        /// </summary>
        static Cliente cliente = opc ? clienteExistente : RegistrarCliente(clienteNuevo);
        static BearerToken token = opc ? ObtenerToken(clienteExistente2) : ObtenerToken(clienteNuevo2);

        #endregion

        [TestMethod]
        public void Agregar_Articulos_A_Favoritos()
        {
            //Cliente cliente = RegistrarCliente(clienteTester);
            //token = ObtenerToken(clienteTester2);

            List<Articulo> articulos = new List<Articulo>();
            articulos.Add(new Articulo()
            {
                articulo = 245888,
                cantidad = 4,
                idLista = 1
            }
            );
            articulos.Add(new Articulo()
            {
                articulo = 521,
                cantidad = 2,
                idLista = 1
            }
            );

            string controlador = "/api/Favoritos/Agregar";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("idTienda", "24");
            request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Detalle_Favoritos()
        {
            //Cliente cliente = RegistrarCliente(clienteTester3);
            //token = ObtenerToken(clienteTester2);

            List<Articulo> articulos = new List<Articulo>();
            articulos.Add(new Articulo()
            {
                articulo = 245888,
                cantidad = 4,
                idLista = 1
            }
            );
            articulos.Add(new Articulo()
            {
                articulo = 521,
                cantidad = 2,
                idLista = 1
            }
            );

            string controlador = "/api/Favoritos/Detalle";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idLista", "32767");
            request.AddHeader("idTienda", "24");
            request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Articulos_De_Favoritos()
        {
            //Cliente cliente = RegistrarCliente(clienteTester5);
            //token = ObtenerToken(clienteTester2);

            string controlador = "/api/Favoritos/Eliminar";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("ContentType", "application/json");
            request.AddHeader("idTienda", "24");
            request.AddParameter("application/json", "[{\"Id_Num_SKU\": 1398561, \"id_Num_LstComp\" :\"32767\"}]", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }


        // MÉTODO PARA OBTENER EL TOKEN
        public static BearerToken ObtenerToken(ClienteAlterno cliente)
        {
            string controlador = "/api/token/GetToken";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");
            request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return BearerToken.FromJson(response.Content);
        }

        // MÉTODO PARA REGISTRAR CLIENTE
        public static Cliente RegistrarCliente(Cliente cliente)
        {
            string controlador = "/api/account/RegisterNew2";
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
