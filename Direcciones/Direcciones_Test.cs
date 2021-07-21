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
        static Cliente clienteNuevo = new Cliente("Iván " + NombreTester, "direcciones_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteNuevo2 = new ClienteAlterno("Iván " + NombreTester, "direcciones_tsttttdev" + NombreTester + "@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Cliente ya existente dentro de la aplicación
        /// </summary>
        static Cliente clienteExistente = new Cliente("Iván Alejandro", "direcciones_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteExistente2 = new ClienteAlterno("Iván Alejandro", "direcciones_tsttttdev@unittest.com", "12345678", "Rodríguez", "Quiroz");

        /// <summary>
        /// Asignación del cliente a usar con respecto a la decisión anterior con números aleatorios.
        /// </summary>
        static Cliente cliente = opc ? clienteExistente : RegistrarCliente(clienteNuevo);
        static BearerToken token = opc ? ObtenerToken(clienteExistente2) : ObtenerToken(clienteNuevo2);

        #endregion

        Direccion direccion = new Direccion();

        [TestMethod]
        public void Nueva_Direccion()
        {
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
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Modificar_Direccion()
        {
            Direccion viejaDireccion = NuevaDireccion(token);
            Direccion nuevaDireccion;
            
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
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK || !response.Content.Contains("editada"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Direcciones()
        {
            AgregarDireccion(token);

            string controlador = "/api/direccion/GetDirecciones";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("oficina"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Direccion()
        {
            NuevaDireccion(token);

            string controlador = "/api/direccion/Delete";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("idDireccion", "1");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Poblacion()
        {
            string controlador = "/api/direccion/poblacion";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("CP", "64610");
            request.AddHeader("bearertoken", token.AccessToken);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
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

        // MÉTODO PARA UNA NUEVA DIRECCIÓN()
        public Direccion NuevaDireccion(BearerToken token)
        {
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

            string controlador = "/api/direccion/New";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return nuevaDireccion;
        }

        public void AgregarDireccion(BearerToken token)
        {
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
