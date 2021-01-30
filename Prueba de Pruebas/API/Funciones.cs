using Prueba_de_Pruebas.Modelos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas
{
    class Funciones
    {
        string urlbase = "https://appsor02.soriana.com";

        #region Cliente
        
        public Modelo_Prueba_Ejecutar RegisterUser(Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/RegisterNew";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Registrar Usuario";
            auxRetorno.idPRUEBA = 5;

            try
            {

                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("disp", "Android");
                request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se registró el usuario correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar PruebaLogin_ObtenerToken(ClienteAlterno cli, out BearerToken token)
        {

            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/token/GetToken";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Login_ObtenerToken";
            auxRetorno.idPRUEBA = 1;
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");
                request.AddParameter("application/json", cli.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                token = BearerToken.FromJson(response.Content);
                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvo el token del cliente " + cli.Email + " correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                token = new BearerToken();
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ModificarCliente(out ClienteAlterno clienteNuevo, ClienteAlterno clienteActual, BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

            string controlador = "/api/account/UpdateAccountInfo";
            string endpoint = urlbase + controlador;

            ClienteAlterno cliente = clienteActual;
            cliente.ApellidoPaterno = "Reyes";
            cliente.ApellidoMaterno = "Martínez";
            cliente.Telefono = "8717321111";
            cliente.TelefonoCelular = "8717321111";
            clienteNuevo = cliente;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Modificar Cliente";
            auxRetorno.idPRUEBA = 2;

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", clienteNuevo.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("Martínez"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La modificación del cliente ocurrió correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar GetUserInfo(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/GetUserWithToken";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 62;
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Cookie", "ak_bmsc=1B2DBCB5D80264AA0698B7F0AC518ABCBDF7CF37470800008F7F6B5FEC0F897A~pl/oJgJTrrHhbQTqb4FK0MGGUg6rCfUibWDDgML6mVfnc4voiQnt0bN75qp83XTuKTyEYCh1U6ILMXH71QaJF37B601rg6tJevK8K916oHEpaRqXtKR5ZSwK3VdkH4iyYUQkBJ1zWg+EdCpLPKeFsgVRlVEVKw7YAvgO9i9qbQm9Vx3zIpWWf6xCDcBOa4a6tMYWPEhvRoZ8WlS3llWtt/JuSf67BcnsZk1QiCnyxOEuE=");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();
                IRestResponse response = client.Execute(request);
                sw.Stop();
                DateTime fechaFinal = DateTime.Now;

                auxRetorno.PRUEBA = "Obtener user Info";
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornó la información del cliente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ChangePassword(Cliente cliente, BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            AuxiliarPassword auxPass = new AuxiliarPassword();
            auxPass.OldPassword = cliente.Password;
            auxPass.NewPassword = "123456prueba";
            auxPass.ConfirmPassword = "123456prueba";

            string controlador = "/api/account/ChangePassword";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 3;
            auxRetorno.PRUEBA = "Modificar Password";
            try
            {
                string panzon = auxPass.ToJson();
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", auxPass.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("00"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La modificación del cliente ocurrió correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar AceptarTermCond(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/AceptarTerminos";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 6;
            auxRetorno.PRUEBA = "Aceptar términos y condiciones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Content.Contains("false"))
                    {
                        auxRetorno.ESTADO = false;
                        auxRetorno.STATUSCODE = (int)response.StatusCode;
                        auxRetorno.EXCEPCION = response.Content;
                        ImprimirError(auxRetorno);
                    }
                    else
                    {
                        auxRetorno.ESTADO = true;
                        auxRetorno.STATUSCODE = (int)response.StatusCode;
                        auxRetorno.RESPUESTA = "Se aceptaron los términos y condiciones";
                    }
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar RecuperarPass(Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/RecuperarPass";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 7;
            auxRetorno.PRUEBA = "Recuperar contraseña";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("email", cliente.Mail);
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se recuperó la contraseña correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ConfirmarCodigo(BearerToken token, Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/ValidarCodigo";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Validar Codigo";
            auxRetorno.idPRUEBA = 64;

            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se validó el código correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        } 

        public Modelo_Prueba_Ejecutar ReenviarCodigo(BearerToken token, Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/ReenviarCodigo";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Reenviar Codigo";
            auxRetorno.idPRUEBA = 66;

            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("email", cliente.Mail);
                request.AddHeader("nombre", cliente.Nombre);
                request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se reenvió el código correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar VincularTarjeta(BearerToken token, Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/VincularTarjeta";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = cliente.Mail,
                LoyaltyAccount = "3086812845384691"
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Vincular Tarjeta";
            auxRetorno.idPRUEBA = 67;
            
            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se vinculó la tarjeta correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        } 

        public Modelo_Prueba_Ejecutar VincularTarjetaTicket(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/VincularTarjetaTicket";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                LoyaltyAccount = "3086812845384691",
                Ticket = "00041207018001600550"
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Vincular Tarjeta Ticket";
            auxRetorno.idPRUEBA = 68;

            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se vinculó la tarjeta correctamente con el ticket";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar CrearVirtual(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/CrearTarjetaVirtual";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                Mail = "testmail5@gmail.com"
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Crear Tarjeta Virtual";
            auxRetorno.idPRUEBA = 69;

            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se creó la tarjeta virtual correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar CambioTarjeta(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/CambioTarjeta";
            string endpoint = urlbase + controlador;

            TarjetaLealtadM tarjetaLealtadM = new TarjetaLealtadM()
            {
                LoyaltyAccount = "3086812845384691"
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Cambio Tarjeta";
            auxRetorno.idPRUEBA = 10075;

            try
            {
                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", tarjetaLealtadM.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se cambió la tarjeta correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar RegisterUser2(Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/account/RegisterNew2";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Registrar Usuario 2";
            auxRetorno.idPRUEBA = 10074;

            try
            {

                var client = new RestClient(endpoint);

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("disp", "Android");
                request.AddParameter("application/json", cliente.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se registró el usuario correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Do search

        public Modelo_Prueba_Ejecutar BusquedaPorSentencia()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/dosearch/sentencia5";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Búsqueda por Sentencia";
            auxRetorno.idPRUEBA = 52;
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"sentencia\":\"aguacate\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"\"\r\n}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                }
                else if (!response.Content.Contains("AGUACATE"))
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                else if (response.Content.Contains("AGUACATE"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La consulta ha retornado resultados correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar VerificadorPrecios()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/dosearch/GetItem3";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 61;
            auxRetorno.PRUEBA = "Verificador de precios";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n\"CliId\":\"\",\n\"StoreId\":\"24\",\n\"ItemId\":\"7501055367498\"\n}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("COCA"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "El verificador retornó resultados correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ProductosPorSubcategoria()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/dosearch/subcategoria4";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 50;
            auxRetorno.PRUEBA = "Productos por subcategoría";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"sentencia\":\"\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"24\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"13242\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"11374020\"\r\n}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("Items"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La consulta retornó resultados correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ArticulosPorPromocion()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/dosearch/articulosPorPromocion";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.PRUEBA = "Articulos por Promocion";
            auxRetorno.idPRUEBA = 10072;
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"sentencia\":\"\",\r\n    \"page\":\"1\",\r\n    \"idTienda\":\"14\",\r\n    \"brandId\":\"\",\r\n    \"categoryId\":\"\",\r\n    \"orderType\":\"0\",\r\n    \"promotionId\":\"\",\r\n  \"tag\":\"11386730\"\r\n}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("HARINA"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron los artículos correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Carrito promos elegibles

        public Modelo_Prueba_Ejecutar CrearVisita(BearerToken token, out int visita)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

            string controlador = "/api/Carrito/visita";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 15;
            auxRetorno.PRUEBA = "Crear Visita";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("text/plain", "", ParameterType.RequestBody);
                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);
                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                visita = 0;
                try { visita = Convert.ToInt32(response.Content); } catch { }

                if (visita != 0)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se generó una visita correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                visita = 0;
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar AgregarArticuloCarrito(BearerToken token, int visita)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/carrito/agregarArticulo4";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 59;
            auxRetorno.PRUEBA = "Agregar artículo a carrito";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", "{\r\n \"articulo\" : 245888,\r\n \"cantidad\" : 5,\r\n \"unidadMedida\" : 2,\r\n \"idNumVisita\" : 329654975,\r\n \"idTienda\" : 24\r\n }".Replace("329654975", visita.ToString()), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("GELATINA"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La artículo se agregó correctamente y se retornó el carrito correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerDetalleCarrito(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Carrito/detalleCarrito4";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 60;
            auxRetorno.PRUEBA = "Obtener Carrito con Promociones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idTienda", "24");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                auxRetorno.FECHAFIN = fechaFinal;

                if (response.Content.Contains("GELATINA"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La consulta retornó un carrito correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarArticuloCarrito(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/carrito/borrarArticulo4";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 58;
            auxRetorno.PRUEBA = "Eliminar artículo de carrito";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idArticulo", "245888");
                request.AddHeader("idTienda", "24");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (!response.Content.Contains("GELATINA") && response.Content.Contains("idCarrito"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La artículo se eliminó correctamente y se retornó el carrito correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar CambiarTienda(BearerToken token, int visita)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/carrito/CambioTienda4";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 57;
            auxRetorno.PRUEBA = "Cambio de Tienda";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", "{\n                \"NumeroAplicacion\" : \"23\",\n                \"NumeroTiendaNueva\":\"14\",\n                \"NumeroCarritoActual\": \"0\",\n                \"NumeroVisita\" : \"340990345\"\n}".Replace("340990345", visita.ToString()), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("idCarrito"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se realizó el cambio de tienda correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar AgregarArregloCarrito4(Cliente cliente, BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/carrito/agregarNarticulosCarrito4";
            string endpoint = urlbase + controlador;

            List<Articulo> articulos = new List<Articulo>()
            {
                new Articulo(170, 1, 349591956, 25, 1),
                new Articulo(456, 1, 349591956, 25, 1)
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 17;
            auxRetorno.PRUEBA = "Agregar artículos a carrito 4";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se agregaron correctamente los artículos al carrito";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ComentarioArticuloCarrito3(Cliente cliente, BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

            ComentarioArticulo comentarioArticulo = new ComentarioArticulo()
            {
                IdArticulo = 8003490,
                DescComentarioOpc = "Comentario de pruebas unitarias por el usuario " + cliente.Mail,
                IdTienda = 24
            };

            string controlador = "/api/carrito/comentarioArticulo3";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 18;
            auxRetorno.PRUEBA = "Comentario a articulo de carrito";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", comentarioArticulo.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se agregó un comentario a un articulo del carrito correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar CanjePuntos(Cliente cliente, BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

            CanjePuntos canjePuntos = new CanjePuntos()
            {
                IdNumSku = 1024595,
                CantidadUnidades = 1,
                CantPuntosCanje = 270,
                IdNumPromCanje = 11376930
            };

            string controlador = "/api/carrito/CanjePuntos2";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 19;
            auxRetorno.PRUEBA = "Canjear puntos";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idTienda", "24");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", canjePuntos.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se canjearon los puntos en el carrito correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Direccion

        public Modelo_Prueba_Ejecutar NuevaDireccion(BearerToken token, out Direccion direccion)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

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

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 63;
            auxRetorno.PRUEBA = "Nueva dirección";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();
                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.Content.Contains("oficina prueba"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se agregó una nueva dirección.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ModificarDireccion(BearerToken token, ref Direccion viejaDireccion, out Direccion nuevaDireccion)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();

            Direccion direccion = viejaDireccion;
            direccion.NomDirCte = "La ofi editada 05 oct";
            direccion.NomColoniaCte = "Cumbres dos";
            direccion.NumIntCalleCteOpc = 113;
            direccion.CpCte = 27054;
            direccion.IdCnscDirCte = 1;

            nuevaDireccion = direccion;
            string controlador = "/api/direccion/modificar";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 4;
            auxRetorno.PRUEBA = "Modificar Dirección";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", nuevaDireccion.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("editada"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se modificó la dirección.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerDirecciones(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/direccion/GetDirecciones";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 51;
            auxRetorno.PRUEBA = "Obtener direcciones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("ofi editada"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornó la colección de direcciones.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarDireccion(BearerToken token, Direccion direccion)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/direccion/Delete";
            string endpoint = urlbase + controlador;

            //LÓGICA PROPIA PARA EVALUAR LA PRUEBA UNITARIA
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 21;
            auxRetorno.PRUEBA = "Eliminar dirección";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idDireccion", "1");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se eliminó la dirección correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerPoblacion(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/direccion/poblacion";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 22;
            auxRetorno.PRUEBA = "Obtener población";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("CP", "64610");
                request.AddHeader("bearertoken", token.AccessToken);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvo la población del código postal 64610 correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ValidarCP ()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/direccion/validarCP";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 8;
            auxRetorno.PRUEBA = "Validar CP";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("CP", "64610");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "CP validado correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Listas

        public Modelo_Prueba_Ejecutar CrearLista(BearerToken token, out List<RespuestaCrearLista> RespuestaCrearList)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/New";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 47;
            auxRetorno.PRUEBA = "Crear una lista";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("nombre", token.AccessToken.Substring(0, 5));
                request.AddHeader("descripcion", "x3");
                request.AddParameter("text/plain", "", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();
                IRestResponse response = client.Execute(request);
                RespuestaCrearList = null;
                try
                {
                    RespuestaCrearList = RespuestaCrearLista.FromJson(response.Content);
                }
                catch { }

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                //LÓGICA PROPIA PARA EVALUAR LA PRUEBA UNITARIA

                if (response.Content.Contains(token.AccessToken.Substring(0, 5)))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se creó una lista correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                RespuestaCrearList = null;
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerListados(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/GetListas";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 46;
            auxRetorno.PRUEBA = "Obtener Listas";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();
                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.Content.Contains(token.AccessToken.Substring(0, 5)))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornó la colección de listas correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar AgregarALista(BearerToken token, List<Articulo> lista)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/AgregarArticulosALista";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 56;
            auxRetorno.PRUEBA = "Agregar artículo a una lista";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", lista.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se agregó un artículo a la lista correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerDetalleLista(BearerToken token, List<RespuestaCrearLista> RespuestaCrearList)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/Detalle";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 49;
            auxRetorno.PRUEBA = "Obtener detalle de una lista";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idLista", RespuestaCrearList[0].IdLista.ToString());
                request.AddParameter("text/plain", "", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();
                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains(RespuestaCrearList[0].IdLista.ToString()))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvo el detalle de una lista correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarArticuloLista(BearerToken token, int idLista)
        {
            List<Articulo> eliminarArticulos = new List<Articulo>()
            {
                new Articulo(1229505, 1),
                new Articulo(2336087)
            };

            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/EliminarArticulos2";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 55;
            auxRetorno.PRUEBA = "Eliminar artículo de una lista";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", eliminarArticulos.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains(token.AccessToken.Substring(0, 5)))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se eliminó un artículo de la lista correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarLista(BearerToken token, List<RespuestaCrearLista> RespuestaCrearList)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/ListaCompra/Delete";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 53;
            auxRetorno.PRUEBA = "Eliminar lista";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idLista", RespuestaCrearList[0].IdLista.ToString());
                request.AddParameter("text/plain", "", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.StatusCode == System.Net.HttpStatusCode.OK && !response.Content.Contains(token.AccessToken.Substring(0, 5)))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se eliminó una lista correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Datos Fiscales

        public Modelo_Prueba_Ejecutar RegistrarDatosFiscales(BearerToken token, out DatosFiscales nuevosDatos)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/DatosFiscales/Register";
            string endpoint = urlbase + controlador;

            DatosFiscales datosFiscales = new DatosFiscales()
            {
                RFC = "DMD190215MJ9",
                RazonSocial = "DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V",
                UsoCFDI = "G03",
                CP = "27266"
            };

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", " {\r\n        \"RFC\": \"DMD190215MJ9\",\r\n        \"RazonSocial\": \"DEVELOP MX DESARROLLO A LA MEDIDA 2 S.A. DE C.V\",\r\n        \"UsoCFDI\": \"G03\",\r\n        \"CP\": \"27266\"\r\n }", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                datosFiscales.IdFiscal = DatosFiscales.FromJson(response.Content)[0].IdFiscal;
                nuevosDatos = datosFiscales;
                sw.Stop();
                DateTime fechaFinal = DateTime.Now;

                //LÓGICA PROPIA PARA EVALUAR LA PRUEBA UNITARIA
                auxRetorno.AMBIENTE = urlbase;
                auxRetorno.BATCH = 0;
                auxRetorno.ENDPOINT = endpoint;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.idPRUEBA = 9;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                auxRetorno.PRUEBA = "Registrar datos fiscales";
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Datos fiscales registrados correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                datosFiscales.IdFiscal = -1;
                nuevosDatos = datosFiscales;
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ModificarDatosFiscales(BearerToken token, long idDatos)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/DatosFiscales/Modificar";
            string endpoint = urlbase + controlador;

            DatosFiscales datosFiscalesModificados = new DatosFiscales()
            {
                Identificador = idDatos,
                IdFiscal = idDatos,
                RFC = "OEJA940721CY2",
                RazonSocial = "Abraham",
                UsoCFDI = "G03",
                CP = "27266"
            };

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 10;
            auxRetorno.PRUEBA = "Modificar datos fiscales";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", datosFiscalesModificados.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Datos fiscales modificados correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;

            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarDatosFiscales(BearerToken token, long idDatos)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/DatosFiscales/Delete?idFiscal=" + idDatos;
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 11;
            auxRetorno.PRUEBA = "Eliminar datos fiscales";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Datos fiscales eliminados correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Estracto por categoria padre

        public Modelo_Prueba_Ejecutar SubCategoriaProductos(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/categoria/subcategoria_productosPage";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 54;
            auxRetorno.PRUEBA = "Subcategoría con productos";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("idCategoria", "13216");
                request.AddHeader("Page", "2");
                request.AddHeader("idTienda", "24");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.Content.Contains("Categorias"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornaron las subcategorías correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }
        
        #endregion

        #region Estracto de promociones

        public Modelo_Prueba_Ejecutar EstractoPromociones(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/categoria/GetEstractoPromocionesPage";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 48;
            auxRetorno.PRUEBA = "Promociones con productos";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("idTienda", "47");
                request.AddHeader("Page", "1");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();

                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.Content.Contains("Categorias"))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornaron las promociones correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Pago

        public Modelo_Prueba_Ejecutar GenerarURL(BearerToken token, out long id)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Pago/GenerarURL";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 14;
            auxRetorno.PRUEBA = "Generar URL";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\"Id_Num_Aplicacion\": 23, \"Id_Num_Orden\" :\"100849616\", \"importe\": \"112.32\", \"Csc_MetodoPago\" : \"0\"}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "El URL de pago se generó correctamente";
                    id = RespuestaPago.FromJson(response.Content).Id_Num_Transaccion;
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    id = 0;
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                id = -1;
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar SaveRedirect(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Pago/SaveRedirect";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 70;
            auxRetorno.PRUEBA = "Save Redirect";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\"URL\": 37dsdsdsdsd53, \"NumOrden\" :\"111\"}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se ejecutó correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Orden
        
        public Modelo_Prueba_Ejecutar InfoOrden(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/Info";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 27;
            auxRetorno.PRUEBA = "Información de la orden";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("numOrden", "100869825");
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar OrdenesProceso(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/OrdenesEnProceso";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 28;
            auxRetorno.PRUEBA = "Órdenes en proceso";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornaron correctamente las ordenes en proceso";
                }
                else if(response.Content.Contains("No tienes pedidos."))
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se ejecutó correctamente, sin embargo no cuentas con pedidos";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;

            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar DetalleOrden(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/DetalleOrden";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 29;
            auxRetorno.PRUEBA = "Detalle de orden";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("numOrden", "100863606");
                request.AddHeader("idTiendas", "47");
                request.AddHeader("Page", "1");
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retorno correctamente el detalle de la orden";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar FechaHoraEntrega(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/FechaHora";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 30;
            auxRetorno.PRUEBA = "Horarios disponibles por tienda 1";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idTienda", "4");
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornaron los horarios disponibles de una tienda correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar FechaHoraEntrega2(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/FechaHora2";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 31;
            auxRetorno.PRUEBA = "Horarios disponibles por tienda 2";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("idTienda", "24");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se retornaron los horarios disponibles de una tienda correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar FormaPago(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/FormaPago";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 32;
            auxRetorno.PRUEBA = "Formas de pago";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron las formas de pago correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar CancelarOrden(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Orden/Cancelar";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 33;
            auxRetorno.PRUEBA = "Cancelar orden";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\"numOrden\": \"9283728\",\"numTransaccion\": \"13847\",\"cancelar\": false}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La orden #9283728 ha sido cancelada";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Sin categoria

        public Modelo_Prueba_Ejecutar GetFolletos()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Regiones/Get";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 35;
            auxRetorno.PRUEBA = "Regiones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Region", "6");
                request.AddHeader("Content-Type", "application/json");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron los folletos correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar SendFeedback(Cliente cliente)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Feedback/Send";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 37;
            auxRetorno.PRUEBA = "Feedback";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\"Experiencia\":\"Todo bien \",\"Descripcion\":\"Desc. feedback\",\"Tipo\":\"Bug\"}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se envió correctamente el mensaje de feedback";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar GetHome2()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/homeSuper/getHome2";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 38;
            auxRetorno.PRUEBA = "Home 2 con promociones";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("idTienda", "24");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se recibió correctamente la vista de Home";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar GetTickets()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Tickets/GetTickets";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 39;
            auxRetorno.PRUEBA = "Obtener tickets";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\"CliId\":\"113737915\",\"PageNumber\":1,\"ItemsByPage\":10}", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron correctamente los tickets del cliente #113737915";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar GetEstructuraComercial()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/categoria/Estructura";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 40;
            auxRetorno.PRUEBA = "Estructura comercial";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se recibió correctamente la estructura comercial";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar Regiones()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/regiones/get3";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 10073;
            auxRetorno.PRUEBA = "Regiones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;
                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron las regiones correctamente.";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Stores

        public Modelo_Prueba_Ejecutar ObtenerTiendas()
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Stores/GetStores";
            string endpoint = urlbase + controlador;
            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 41;
            auxRetorno.PRUEBA = "Obtener tiendas";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Las tiendas se obtuvieron correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerTiendasCP(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Stores/GetStoreCP";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 23;
            auxRetorno.PRUEBA = "Obtener tiendas por código postal";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("CP", "27266");
                request.AddHeader("bearertoken", token.AccessToken);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                //EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La tienda con el código postal 27266 se obtuvo correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar PoblacionesTiendas(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
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

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 24;
            auxRetorno.PRUEBA = "Stores Poblaciones";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("CP", "27266");
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddParameter("application/json", poblacionTiendaList.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "La población de la tienda en el código postal 27266 se obtuvo correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Promociones

        public Modelo_Prueba_Ejecutar ObtenerPromociones(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Promociones/Get";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 25;
            auxRetorno.PRUEBA = "Obtener promociones";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron las promociones correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ActivarPromocion(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Promociones/Activar";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 26;
            auxRetorno.PRUEBA = "Activar promoción";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idPromo", "11346661");

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = response.Content.ToString();
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Banners 

        public Modelo_Prueba_Ejecutar ObtenerBanners(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Banners/GetBanners";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 65;
            auxRetorno.PRUEBA = "Obtener banners de inicio";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron los banners de inicio correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar ObtenerBannersInicio(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Banners/GetBannersInicio";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 43;
            auxRetorno.PRUEBA = "Obtener banners de inicio";
            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron los banners de inicio correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        #region Favoritos

        public Modelo_Prueba_Ejecutar AgregarArticulosAFavoritos(BearerToken token, List<Articulo> articulos)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Favoritos/Agregar";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 20;
            auxRetorno.PRUEBA = "Agregar Artículos a Favoritos";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("idTienda", "24");
                request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se agregaron los artículos correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar DetalleFavoritos(BearerToken token, List<Articulo> articulos)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Favoritos/Detalle";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 34;
            auxRetorno.PRUEBA = "Detalle Favoritos";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("idLista", "32767");
                request.AddHeader("idTienda", "24");
                request.AddParameter("application/json", articulos.ToJson(), ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se obtuvieron los detalles correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        public Modelo_Prueba_Ejecutar EliminarArticulosDeFavoritos(BearerToken token)
        {
            Modelo_Prueba_Ejecutar auxRetorno = new Modelo_Prueba_Ejecutar();
            string controlador = "/api/Favoritos/Eliminar";
            string endpoint = urlbase + controlador;

            auxRetorno.AMBIENTE = urlbase;
            auxRetorno.BATCH = 0;
            auxRetorno.ENDPOINT = endpoint;
            auxRetorno.idPRUEBA = 10071;
            auxRetorno.PRUEBA = "Eliminar Articulos de Favoritos";

            try
            {
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("bearertoken", token.AccessToken);
                request.AddHeader("ContentType", "application/json");
                request.AddHeader("idTienda", "24");
                request.AddParameter("application/json", "[{\"Id_Num_SKU\": 1398561, \"id_Num_LstComp\" :\"32767\"}]", ParameterType.RequestBody);

                DateTime fechaInicio = DateTime.Now;
                auxRetorno.FECHAINICIO = fechaInicio;

                Stopwatch sw = Stopwatch.StartNew();

                // EJECUTA
                IRestResponse response = client.Execute(request);

                sw.Stop();
                DateTime fechaFinal = DateTime.Now;
                auxRetorno.FECHAFIN = fechaFinal;
                auxRetorno.TIEMPORES = Convert.ToInt32(sw.ElapsedMilliseconds);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    auxRetorno.ESTADO = true;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.RESPUESTA = "Se eliminaron los articulos correctamente";
                }
                else
                {
                    auxRetorno.ESTADO = false;
                    auxRetorno.STATUSCODE = (int)response.StatusCode;
                    auxRetorno.EXCEPCION = response.Content.ToString();
                    ImprimirError(auxRetorno);
                }
                return auxRetorno;
            }
            catch (Exception ex)
            {
                auxRetorno.ESTADO = false;
                auxRetorno.STATUSCODE = -1;
                auxRetorno.EXCEPCION = "Ocurrió un error en interno en la prueba automática (" + auxRetorno.PRUEBA + "): " + ex;
                ImprimirError(auxRetorno);
                return auxRetorno;
            }
        }

        #endregion

        public void ImprimirError(Modelo_Prueba_Ejecutar auxRetorno)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR EN: \"" + auxRetorno.PRUEBA + "\"");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("ENDPOINT: " + auxRetorno.ENDPOINT);
            Console.WriteLine("STATUS CODE: " + auxRetorno.STATUSCODE);
            Console.WriteLine("TIEMPO DE RESPUESTA: " + auxRetorno.TIEMPORES + " MS");
            Console.WriteLine("FECHA DE EJECUCIÓN: " + auxRetorno.FECHAINICIO.ToString("dddd, dd MMMM yyyy HH:mm:ss").ToUpper() + " - " + auxRetorno.FECHAFIN.ToString("dddd, dd MMMM yyyy HH:mm:ss").ToUpper());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("EXCEPCIÓN: " + auxRetorno.EXCEPCION);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}