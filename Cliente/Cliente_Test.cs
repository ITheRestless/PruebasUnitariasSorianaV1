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

        BearerToken token = new BearerToken();

        #region Creacion de usuarios

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        // CLIENTE ESTÁTICO USADO PARA PRUEBAS QUE NO MODIFICAN EN ALGÚN ASPECTO AL CLIENTE
        ClienteAlterno clienteTester = new ClienteAlterno("Iván Tester", "ivanrqtester@unittest.com", "123456", "Rodríguez", "Quiroz");

        /**
         * DADO QUE LAS PRUEBAS SE REALIZAN DE MANERA SIMULTANEA, ES NECESARIO CREAR DISTINTOS USUARIOS PARA (CASI) CADA UNA DE ELLAS,
         * LAS QUE NO REQUIEREN HACER USO DE UN NUEVO USUARIO SON AQUELLAS QUE NO MODIFICAN ALGÚN DATO DEL MISMO
         */

        // Cliente para la prueba de Registrar_Usuario
        static Cliente1 clienteTester2 = new Cliente1("Iván " + NombreTester, "tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");        
        
        // Cliente para la prueba Confirmar_Codigo
        static Cliente1 clienteTester3 = new Cliente1("Iván A" + NombreTester, "ivan1tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester4 = new ClienteAlterno("Iván A" + NombreTester, "ivan1tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Modificar_Cliente
        static Cliente1 clienteTester5 = new Cliente1("Iván A" + NombreTester, "ivan2tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester6 = new ClienteAlterno("Iván A" + NombreTester, "ivan2tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Cambiar_Contrasena
        static Cliente1 clienteTester7 = new Cliente1("Iván A" + NombreTester, "ivan3tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester8 = new ClienteAlterno("Iván A" + NombreTester, "ivan3tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        //Cliente para la prueba Recuperar_Contrasena
        static Cliente1 clienteTester9 = new Cliente1("Iván A" + NombreTester, "ivan4tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Aceptar_Terminos_Condiciones
        static Cliente1 clienteTester10 = new Cliente1("Iván A" + NombreTester, "ivan5tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester11 = new ClienteAlterno("Iván A" + NombreTester, "ivan5tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Reenviar_Codigo
        static Cliente1 clienteTester12 = new Cliente1("Iván A" + NombreTester, "ivan6tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester13 = new ClienteAlterno("Iván A" + NombreTester, "ivan6tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Vincular_Tarjeta
        static Cliente1 clienteTester14 = new Cliente1("Iván A" + NombreTester, "ivan7tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester15 = new ClienteAlterno("Iván A" + NombreTester, "ivan7tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Crear_Tarjeta_Virtual
        static Cliente1 clienteTester16 = new Cliente1("Iván A" + NombreTester, "ivan8tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester17 = new ClienteAlterno("Iván A" + NombreTester, "ivan8tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        // Cliente para la prueba Registrar_Usuario_2
        static Cliente1 clienteTester18 = new Cliente1("Iván A" + NombreTester, "ivan9tester" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        
        #endregion

        [TestMethod]
        public void Registrar_Usuario()
        {
            string controlador = "/api/account/RegisterNew";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("disp", "Android");
            request.AddParameter("application/json", clienteTester2.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, response.Content);
        }

        [TestMethod]
        public void Obtener_Token()
        {
            string controlador = "/api/token/GetToken";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");
            request.AddParameter("application/json", clienteTester.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Modificar_Cliente()
        {
            Cliente1 cliente1 = RegistrarCliente(clienteTester5);
            token = ObtenerToken(clienteTester6);

            string controlador = "/api/account/UpdateAccountInfo";
            string endpoint = urlbase + controlador;

            ClienteAlterno cliente = clienteTester6;
            cliente.ApellidoPaterno = "Márquez";
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
            
            if (!response.Content.Contains("Márquez"))
                Assert.Fail();
        }

        [TestMethod]
        public void Obtener_Informacion_Usuario()
        {
            token = ObtenerToken(clienteTester);

            string controlador = "/api/account/GetUserWithToken";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Cambiar_Contrasena()
        {
            Cliente1 cliente1 = RegistrarCliente(clienteTester7);
            token = ObtenerToken(clienteTester8);

            AuxiliarPassword auxPass = new AuxiliarPassword();
            auxPass.OldPassword = clienteTester.Password;
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
                Assert.Fail();
        }

        [TestMethod]
        public void Aceptar_Terminos_Condiciones()
        {
            Cliente1 cliente1 = RegistrarCliente(clienteTester10);
            token = ObtenerToken(clienteTester11);

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
                    Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Recuperar_Contrasena()
        {
            Cliente1 cliente = RegistrarCliente(clienteTester9);

            string controlador = "/api/account/RecuperarPass";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("email", cliente.Mail);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Confirmar_Codigo()
        {
            Cliente1 cliente = RegistrarCliente(clienteTester3);
            token = ObtenerToken(clienteTester4);

            string controlador = "/api/account/ValidarCodigo";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", clienteTester3.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        
        /*[TestMethod]
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

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }*/

        [TestMethod]
        public void Vincular_Tarjeta()
        {
            Cliente1 cliente = RegistrarCliente(clienteTester14);
            token = ObtenerToken(clienteTester15);

            string controlador = "/api/account/VincularTarjeta";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = cliente.Mail,
                LoyaltyAccount = "3086812845384691"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Vincular_Tarjeta_Ticket()
        {
            token = ObtenerToken(clienteTester);

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

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Crear_Tarjeta_Virtual()
        {
            Cliente1 cliente = RegistrarCliente(clienteTester16);
            token = ObtenerToken(clienteTester17);

            string controlador = "/api/account/CrearTarjetaVirtual";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = cliente.Mail
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Cambio_Tarjeta()
        {
            token = ObtenerToken(clienteTester);

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

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Registrar_Usuario_2()
        {
            string controlador = "/api/account/RegisterNew2";
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("disp", "Android");
            request.AddParameter("application/json", clienteTester18.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        // MÉTODO PARA OBTENER EL TOKEN
        public BearerToken ObtenerToken(ClienteAlterno cliente)
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

        // MÉTODO PARA REGISTRAR CLIENTE NUEVO
        public static Cliente1 RegistrarCliente(Cliente1 cliente)
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
