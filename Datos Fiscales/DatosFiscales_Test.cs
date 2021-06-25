using Datos_Fiscales.Modelos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace Datos_Fiscales
{
    [TestClass]
    public class DatosFiscales_Test
    {
        static string urlbase = "https://appsor02.soriana.com";

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        static Cliente clienteTester = new Cliente("Iván " + NombreTester, "tester1datosfiscales" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");
        static ClienteAlterno clienteTester2 = new ClienteAlterno("Iván " + NombreTester, "tester1datosfiscales" + NombreTester + "@unittest.com", "123456", "Rodríguez", "Quiroz");

        static Cliente cliente = RegistrarCliente(clienteTester);
        static BearerToken token = ObtenerToken(clienteTester2);

        [TestMethod]
        public void Registrar_Datos_Fiscales()
        {
            string controlador = "/api/DatosFiscales/Register";
            string endpoint = urlbase + controlador;

            DatosFiscales datosFiscales = new DatosFiscales()
            {
                RFC = "DMD190215MJ9",
                RazonSocial = "DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V",
                UsoCFDI = "G03",
                CP = "27266"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", " {\r\n        \"RFC\": \"DMD190215MJ9\",\r\n        \"RazonSocial\": \"DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V\",\r\n        \"UsoCFDI\": \"G03\",\r\n        \"CP\": \"27266\"\r\n }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK || !response.Content.Contains("DEVELOP"))
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Modificar_Datos_Fiscales()
        {
            DatosFiscales datosFiscales = RegistrarDatosFiscales();

            string controlador = "/api/DatosFiscales/Modificar";
            string endpoint = urlbase + controlador;

            DatosFiscales datosFiscalesModificados = new DatosFiscales()
            {
                Identificador = 1609,
                IdFiscal = datosFiscales.IdFiscal,
                RFC = "OEJA940721CY2",
                RazonSocial = "Abraham",
                UsoCFDI = "G03",
                CP = "27266"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", datosFiscalesModificados.ToJson(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Status Code:" + response.StatusCode + " | Contenido respuesta: " + response.Content);
        }

        [TestMethod]
        public void Eliminar_Datos_Fiscales()
        {
            DatosFiscales datosFiscales = RegistrarDatosFiscales();

            string controlador = "/api/DatosFiscales/Delete?idFiscal=" + datosFiscales.IdFiscal;
            string endpoint = urlbase + controlador;

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddHeader("Content-Type", "application/json");

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

        // MÉTODO PARA REGISTRAR DATOS FISCALES
        public static DatosFiscales RegistrarDatosFiscales()
        {
            string controlador = "/api/DatosFiscales/Register";
            string endpoint = urlbase + controlador;

            DatosFiscales datosFiscales = new DatosFiscales()
            {
                RFC = "DMD190215MJ9",
                RazonSocial = "DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V",
                UsoCFDI = "G03",
                CP = "27266"
            };

            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("bearertoken", token.AccessToken);
            request.AddParameter("application/json", " {\r\n        \"RFC\": \"DMD190215MJ9\",\r\n        \"RazonSocial\": \"DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V\",\r\n        \"UsoCFDI\": \"G03\",\r\n        \"CP\": \"27266\"\r\n }", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return datosFiscales;
        }
    }
}
