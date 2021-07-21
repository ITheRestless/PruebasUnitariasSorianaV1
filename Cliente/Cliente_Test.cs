using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Cliente.Modelos;

namespace Cliente
{
    [TestClass]
    public class Cliente_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        //BearerToken token = new BearerToken();

        #region Creacion de usuarios

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        // CLIENTE EST�TICO USADO PARA PRUEBAS QUE NO MODIFICAN EN ALG�N ASPECTO AL CLIENTE
        Cliente1 clienteToken = new Cliente1("Iv�n A " + NombreTester, "ivanrqtsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        ClienteAlterno clienteToken2 = new ClienteAlterno("Iv�n A " + NombreTester, "ivanrqtsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        /**
         * DADO QUE LAS PRUEBAS SE REALIZAN DE MANERA SIMULTANEA, ES NECESARIO CREAR DISTINTOS USUARIOS PARA 
         * (CASI) CADA UNA DE ELLAS, LAS QUE NO REQUIEREN HACER USO DE UN NUEVO USUARIO SON AQUELLAS 
         * QUE NO MODIFICAN ALG�N DATO DEL MISMO
        */

        // Cliente para la prueba de Registrar_Usuario
        static Cliente1 clienteTester2 = new Cliente1("Iv�n A " + NombreTester, "testerdeveloptsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");        
        
        // Cliente est�tico
        static Cliente1 clienteTester3 = new Cliente1("Iv�n A" + NombreTester, "ivan1tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester4 = new ClienteAlterno("Iv�n A" + NombreTester, "ivan1tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        // Cliente para la prueba Cambiar_Contrasena
        static Cliente1 clienteTester7 = new Cliente1("Iv�n A" + NombreTester, "ivan3tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester8 = new ClienteAlterno("Iv�n A" + NombreTester, "ivan3tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        
        // Cliente para la prueba Reenviar_Codigo
        static Cliente1 clienteTester12 = new Cliente1("Ivan A" + NombreTester, "ivan6tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester13 = new ClienteAlterno("Ivan A" + NombreTester, "ivan6tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        // Cliente para la prueba Vincular_Tarjeta
        static Cliente1 clienteTester14 = new Cliente1("Iv�n A" + NombreTester, "ivan7tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester15 = new ClienteAlterno("Iv�n A" + NombreTester, "ivan7tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        // Cliente para la prueba Crear_Tarjeta_Virtual
        static Cliente1 clienteTester16 = new Cliente1("Iv�n A" + NombreTester, "ivan8tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester17 = new ClienteAlterno("Iv�n A" + NombreTester, "ivan8tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        // Cliente para la prueba Registrar_Usuario_2
        static Cliente1 clienteTester18 = new Cliente1("Iv�n A" + NombreTester, "ivan9tsttttdev" + NombreTester + "@unittest.com", "123456", "Rodr�guez", "Quiroz");

        #endregion

        static Cliente1 cliente = RegistrarCliente(clienteTester3);
        static BearerToken token = ObtenerToken(clienteTester4);

        [TestMethod]
        public void Registrar_Usuario()
        {
            string controlador = "/api/account/RegisterNew2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("disp", "Android");
            request.AddParameter("application/json", clienteTester2.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content + "Usuario: " + clienteTester2.Mail);
        }

        [TestMethod]
        public void Obtener_Token()
        {
            Cliente1 cliente = RegistrarCliente(clienteToken);

            string controlador = "/api/token/GetToken";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");
            request.AddParameter("application/json", clienteToken2.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }
 
        [TestMethod]
        public void Modificar_Cliente()
        {
            string controlador = "/api/account/UpdateAccountInfo";
            string endpoint = urlbase + controlador;

            ClienteAlterno cliente = clienteTester4;
            cliente.ApellidoPaterno = "M�rquez";
            cliente.ApellidoMaterno = "Horta";
            cliente.Telefono = "8717321111";
            cliente.TelefonoCelular = "8717321111";
            ClienteAlterno clienteNuevo = cliente;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", clienteNuevo.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("M�rquez"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Obtener_Informacion_Usuario()
        {
            string controlador = "/api/account/GetUserWithToken";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Cambiar_Contrasena()
        {
            AuxiliarPassword auxPass = new AuxiliarPassword();
            auxPass.OldPassword = clienteTester8.Password;
            auxPass.NewPassword = "123456prueba";
            auxPass.ConfirmPassword = "123456prueba";

            string controlador = "/api/account/ChangePassword";
            string endpoint = urlbase + controlador;

            string panzon = auxPass.ToJson();
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", auxPass.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("00"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Aceptar_Terminos_Condiciones()
        {
            string controlador = "/api/account/AceptarTerminos";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Content.Contains("false"))
                    throw new Exception("Status Code:" + response.StatusCode + " | Error: " + response.ErrorMessage + " | Contenido respuesta: " + response.Content);
            }
            else
            {
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
            }
        }

        [TestMethod]
        public void Recuperar_Contrasena()
        {
            string controlador = "/api/account/RecuperarPass";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("email", cliente.Mail);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Confirmar_Codigo()
        {
            string controlador = "/api/account/ValidarCodigo";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", clienteTester3.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }
        
        [TestMethod]
        public void Reenviar_Codigo()
        {
            Cliente1 cliente = RegistrarCliente(clienteTester12);
            token = ObtenerToken(clienteTester13);

            string controlador = "/api/account/ReenviarCodigo";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("email", cliente.Mail);
            request.AddHeader("nombre", cliente.Nombre);
            request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Vincular_Tarjeta()
        {
            Cliente1 clienteTarjeta = RegistrarCliente(clienteTester14);
            BearerToken token2 = ObtenerToken(clienteTester15);

            string controlador = "/api/account/VincularTarjeta";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = clienteTarjeta.Mail,
                LoyaltyAccount = "3086812845384691"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token2.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Vincular_Tarjeta_Ticket()
        {
            string controlador = "/api/account/VincularTarjetaTicket";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                LoyaltyAccount = "3086812845384691",
                Ticket = "00041207018001600550"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Crear_Tarjeta_Virtual()
        {
            Cliente1 clienteTarjeta = RegistrarCliente(clienteTester16);
            BearerToken token2 = ObtenerToken(clienteTester17);

            string controlador = "/api/account/CrearTarjetaVirtual";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = clienteTarjeta.Mail
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token2.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Cambio_Tarjeta()
        {
            string controlador = "/api/account/CambioTarjeta";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                LoyaltyAccount = "3086812845384691"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }


        // M�TODO PARA OBTENER EL TOKEN
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

        // M�TODO PARA REGISTRAR CLIENTE NUEVO
        public static Cliente1 RegistrarCliente(Cliente1 cliente)
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
