using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Stores.Modelos;
using System;
using System.Collections.Generic;

namespace Stores
{
    [TestClass]
    public class Stores_Test
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
        static Cliente clienteNuevo = new Cliente("Iván " + NombreTester, "stores_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteNuevo2 = new ClienteAlterno("Iván " + NombreTester, "stores_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Cliente ya existente dentro de la aplicación
        /// </summary>
        static Cliente clienteExistente = new Cliente("Iván Alejandro", "stores_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteExistente2 = new ClienteAlterno("Iván Alejandro", "stores_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Asignación del cliente a usar con respecto a la decisión anterior con números aleatorios.
        /// </summary>
        static Cliente cliente = opc ? clienteExistente : RegistrarCliente(clienteNuevo);
        static BearerToken token = opc ? ObtenerToken(clienteExistente2) : ObtenerToken(clienteNuevo2);

        #endregion

        [TestMethod]
        public void Obtener_Tiendas()
        {
            string controlador = "/api/Stores/GetStores";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Tiendas_CP()
        {
            string controlador = "/api/Stores/GetStoreCP";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("CP", "27266");
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Poblaciones_Tiendas()
        {
            string controlador = "/api/Stores/StoresPoblacion";
            string endpoint = urlbase + controlador;

            List<PoblacionTienda> poblacionTiendaList = new List<PoblacionTienda>();
            poblacionTiendaList.Add(new PoblacionTienda()
            {
                Id = 1,
                Name = "SANTO DOMINGO",
                LogoPath = "",
                Latitude = 25.7502,
                Longitude = -100.2569,
                Image = null,
                Radio = 5000,
                IdNumLogo = 1,
                TipoTienda = 1,
                BitServ = false,
                BitReco = false,
                Telefono = "8183299099",
                Direccion = "AV. S. DOMINGO Y AV. DIAZ DE BERLANGA NO. 1800  SANTO DOMINGO",
                Region = 2,
                IdsNumPoblacion = 973,
                IdNumPais = 1,
                IdNumEstado = 19,
                IdNumPoblacion = 46,
                NomEstado = "NUEVO LEON",
                NomPoblacion = "SAN NICOLAS DE LOS GARZA"
            });

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("CP", "27266");
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", poblacionTiendaList.ToJson(), ParameterType.RequestBody);

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

            return token = BearerToken.FromJson(response.Content);
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
