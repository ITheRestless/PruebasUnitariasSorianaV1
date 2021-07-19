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

        /// <summary>
        /// Inicialización de la variable visita para posteriormente revisar si su valor cambió 
        /// al hacer la petición.
        /// </summary>
        int visita = 0;

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
        static Cliente clienteNuevo = new Cliente("Iván " + NombreTester, "carrito_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteNuevo2 = new ClienteAlterno("Iván " + NombreTester, "carrito_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Cliente ya existente dentro de la aplicación
        /// </summary>
        static Cliente clienteExistente = new Cliente("Iván Alejandro", "carrito_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteExistente2 = new ClienteAlterno("Iván Alejandro", "carrito_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Asignación del cliente a usar con respecto a la decisión anterior con números aleatorios.
        /// </summary>
        static Cliente cliente = opc ? clienteExistente : RegistrarCliente(clienteNuevo);
        static BearerToken token = opc ? ObtenerToken(clienteExistente2) : ObtenerToken(clienteNuevo2);

        [TestMethod]
        public void Crear_Visita()
        {
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
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
            }
        }

        [TestMethod]
        public void Agregar_Articulo_A_Carrito()
        {
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
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Detalle_Carrito()
        {
            string controlador = "/api/Carrito/detalleCarrito5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Articulo_Carrito()
        {
            string controlador = "/api/carrito/borrarArticulo5";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idArticulo", "245888");
            request.AddHeader("idTienda", "24");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Cambiar_Tienda()
        {
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
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK || !response.Content.Contains("idCarrito"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Agregar_Arreglo_Carrito()
        {
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
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Comentario_Articulo()
        {
            ComentarioArticulo comentarioArticulo = new ComentarioArticulo()
            {
                IdArticulo = 8003490,
                DescComentarioOpc = "Comentario de pruebas unitarias por el usuario " + cliente.Mail,
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

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Canje_Puntos()
        {
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

        // MÉTODO PARA OBTENER LA VISITA DEL CLIENTE
        public int ObtenerVisita()
        {
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
