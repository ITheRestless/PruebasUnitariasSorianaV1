using Prueba_de_Pruebas.Modelos;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas
{
    class DAO
    {
        string cadena = "Data Source=104.214.62.172; Initial Catalog=laboratorioPruebas; User Id=UserLabQA;Password=5b5fbbee289c15579cbd3f6d9cc41a173d4";
        string QUERY_INSERT_PRUEBA_EJECUCIONES = "INSERT INTO PRUEBAS_EJECUCIONES (idPRUEBA, idEJECUCION, FECHAINICIO, FECHAFIN, TIEMPORES, RESPUESTA, EXCEPCION, ESTADO, idUSUARIO, PRUEBA, RGB, AMBIENTE, ENDPOINT, STATUSCODE, TIPO) VALUES (@idPRUEBA, @idEJECUCION, @FECHAINICIO, @FECHAFIN, @TIEMPORES, @RESPUESTA, @EXCEPCION, @ESTADO, @idUSUARIO, @PRUEBA, @RGB, @AMBIENTE, @ENDPOINT, @STATUSCODE, @TIPO);";
        string QUERY_INSERT_EJECUCION = "INSERT INTO EJECUCIONES(  FECHA ,idUSUARIO) VALUES(  @FECHA ,@idUSUARIO); SELECT CAST(scope_identity() AS int);";

        public bool InsertarPruebas(Modelo_Prueba_Ejecutar prueba_ejecutar)
        {
            SqlConnection con = new SqlConnection(cadena);
            try
            {
                con.Open();
                SqlCommand cmdPrueba = new SqlCommand(QUERY_INSERT_PRUEBA_EJECUCIONES, con);
                cmdPrueba.Parameters.AddWithValue("@idEJECUCION", prueba_ejecutar.idEJECUCION);
                cmdPrueba.Parameters.AddWithValue("@idUSUARIO", prueba_ejecutar.idUSUARIO);
                cmdPrueba.Parameters.AddWithValue("@idPRUEBA", prueba_ejecutar.idPRUEBA);
                cmdPrueba.Parameters.AddWithValue("@FECHAINICIO", prueba_ejecutar.FECHAINICIO);
                cmdPrueba.Parameters.AddWithValue("@FECHAFIN", prueba_ejecutar.FECHAFIN);
                cmdPrueba.Parameters.AddWithValue("@TIEMPORES", prueba_ejecutar.TIEMPORES);
                cmdPrueba.Parameters.AddWithValue("@RESPUESTA", prueba_ejecutar.RESPUESTA);
                cmdPrueba.Parameters.AddWithValue("@EXCEPCION", prueba_ejecutar.EXCEPCION);
                cmdPrueba.Parameters.AddWithValue("@ESTADO", prueba_ejecutar.ESTADO);
                cmdPrueba.Parameters.AddWithValue("@PRUEBA", prueba_ejecutar.PRUEBA);
                cmdPrueba.Parameters.AddWithValue("@AMBIENTE", prueba_ejecutar.AMBIENTE);
                cmdPrueba.Parameters.AddWithValue("@ENDPOINT", prueba_ejecutar.ENDPOINT);
                cmdPrueba.Parameters.AddWithValue("@TIPO", "U");
                cmdPrueba.Parameters.AddWithValue("@STATUSCODE", prueba_ejecutar.STATUSCODE);

                int n = (prueba_ejecutar.TIEMPORES > 1000) ? 100 : prueba_ejecutar.TIEMPORES / 10;

                int R = (255 * n) / 100;
                int G = (255 * (100 - n)) / 100;
                int B = 0;

                string RGB = R + "," + G + "," + B;
                cmdPrueba.Parameters.AddWithValue("@RGB", RGB);
                cmdPrueba.ExecuteNonQuery();
                //if (!prueba_ejecutar.ESTADO) EnviarCorreo(prueba_ejecutar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return true;
        }

        public int InsertarEJECUCION(int User)
        {
            SqlConnection con = new SqlConnection(cadena);
            int idinsertado = 0;

            try
            {
                con.Open();
                SqlCommand cmdPrueba = new SqlCommand(QUERY_INSERT_EJECUCION, con);
                cmdPrueba.Parameters.AddWithValue("@FECHA", DateTime.Now);
                cmdPrueba.Parameters.AddWithValue("@idUSUARIO", User);

                idinsertado = (int)cmdPrueba.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return idinsertado;
        }

        public void EnviarCorreo(Modelo_Prueba_Ejecutar prueba)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                string htmltext = "<html><head></head><body><h2>Reporte de prueba unitaria fallida</h2><h4>Hubo un error en la prueba: " + prueba.PRUEBA + "</h4> <ul> <li>Fecha de ejecución: " + prueba.FECHAINICIO.ToString("dddd d \\de MMMM \\de yyyy HH:mm:ss").ToLower() + " - " + prueba.FECHAFIN.ToString("dddd d \\de MMMM \\de yyyy HH:mm:ss").ToLower() + "</li> <li>Tiempo de respuesta: " + prueba.TIEMPORES + "ms</li> <li>Excepción: " + prueba.EXCEPCION + "</li> <li>Estado: Error</li> <li>Ambiente: " + prueba.AMBIENTE + "</li> <li>Endpoint: " + prueba.ENDPOINT + "</li> <li>Código de: " + prueba.STATUSCODE + "</li> <li>Tipo:</li> </ul> </body> </html>";
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", "key-35e7388efdd202c9d79d75912e0c38d8");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "noreplymail.develop.mx", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Error en prueba unitaria <labpruebas@noreplymail.develop.mx>");
                request.AddParameter("to", "lab.developmx@gmail.com");

                request.AddParameter("subject", "Pruebas unitarias: " + prueba.PRUEBA);
                request.AddParameter("text", "Ocurrió un error en la prueba unitaria: " + prueba.PRUEBA);
                request.AddParameter("html", htmltext);
                request.Method = Method.POST;
                client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void EnviarCorreoTimeOut(List<Modelo_Prueba_Ejecutar> pruebas)
        {
            try
            {
                string bodyHTML = "<html> <head></head> <body> <h2>Reporte de pruebas en time out arriba de 2000 ms</h2>";
                string estado = "";
                for (int i = 0; i < pruebas.Count; i++)
                {
                    estado = pruebas[i].EXCEPCION == "" ? "Correcto" : "Error";
                    bodyHTML += "<h4>Prueba: " + pruebas[i].PRUEBA + "</h4> <ul> <li>Fecha de ejecución: " + pruebas[i].FECHAINICIO.ToString("dddd d \\de MMMM \\de yyyy HH:mm:ss").ToLower() + " - " + pruebas[i].FECHAFIN.ToString("dddd d \\de MMMM \\de yyyy HH:mm:ss").ToLower() + "</li> <li>Tiempo de respuesta: " + pruebas[i].TIEMPORES + "ms</li> <li>Respuesta: " + pruebas[i].RESPUESTA + "</li> <li>Excepción: " + pruebas[i].EXCEPCION + "</li> <li>Estado: " + estado + "</li> <li>Ambiente: " + pruebas[i].AMBIENTE + "</li> <li>Endpoint: " + pruebas[i].ENDPOINT + "</li> <li>Código de: " + pruebas[i].STATUSCODE + "</li> </ul>";
                }
                bodyHTML += "</body></html>";

                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", "key-35e7388efdd202c9d79d75912e0c38d8");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "noreplymail.develop.mx", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Timeout en pruebas unitarias <labpruebas@noreplymail.develop.mx>");
                request.AddParameter("to", "lab.developmx@gmail.com");

                request.AddParameter("subject", "Tiempos de respuestas altos");
                request.AddParameter("text", "Tiempos de respuesta mayores a 2000ms");
                request.AddParameter("html", bodyHTML);
                request.Method = Method.POST;
                client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
