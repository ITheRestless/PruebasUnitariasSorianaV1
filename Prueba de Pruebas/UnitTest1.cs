using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prueba_de_Pruebas.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Prueba_de_Pruebas
{
    [TestClass]
    public class UnitTest1
    {

        Funciones funciones;
        DAO dao;

        String NombreTester;
        Cliente clienteTester;

        int EjecucionaInsertar;

        Cliente clienteprueba;
        string clienteJSON;

        BearerToken token;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        static List<Cliente> clientes = new List<Cliente>() {
            new Cliente("Javier", "javiersanchezra@gmail.com", "12345678"),
            new Cliente("Tester", "tester299134156@unittest.com", "12345678"),
            new Cliente("Christopher", "christopher@pruebas.com", "12345678"),
            new Cliente("Pedro", "pedro@pruebas.com", "12345678"),
            new Cliente("Hector", "hector@pruebas.com", "12345678")
        };

        public void inicializar()
        {
            Maximize();
            funciones = new Funciones();
            dao = new DAO();

            NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            clienteTester = new Cliente("Edzon " + NombreTester, "tester" + NombreTester + "@unittest.com", "12345678", "Bolivar", "Santos");

            EjecucionaInsertar = dao.InsertarEJECUCION(1);

            clienteprueba = clientes[1];
            clienteJSON = clienteprueba.ToJson();

            token = new BearerToken();

        }

        static List<int> CorrectasIncorrectas(List<Modelo_Prueba_Ejecutar> pruebas)
        {
            List<int> evaluacionPruebas = new List<int>(); //correctas, incorrectas 
            evaluacionPruebas.Add((pruebas.Where(prueba => prueba.ESTADO == true)).ToList().Count);
            evaluacionPruebas.Add((pruebas.Where(prueba => prueba.ESTADO == false)).ToList().Count);
            return evaluacionPruebas;
        }

        static void InsertarPruebas(List<Modelo_Prueba_Ejecutar> pruebas)
        {
            DAO dao = new DAO();
            try
            {
                for (int i = 0; i < pruebas.Count; i++)
                {
                    dao.InsertarPruebas(pruebas[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void EnviarCorreosPruebasMayoresA2000(List<Modelo_Prueba_Ejecutar> pruebas)
        {
            List<Modelo_Prueba_Ejecutar> pruebasTimeOut = (pruebas.Where(prueba => prueba.TIEMPORES >= 2000)).ToList();
            DAO dao = new DAO();
            dao.EnviarCorreoTimeOut(pruebasTimeOut);
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<Modelo_Prueba_Ejecutar> pruebasTotales = new List<Modelo_Prueba_Ejecutar>();

            Modelo_Prueba_Ejecutar RespuestaRegistrarUsuario = funciones.RegisterUser(clienteTester);
            RespuestaRegistrarUsuario.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(RespuestaRegistrarUsuario);

            inicializar();

            List<int> evaluacionPruebas = CorrectasIncorrectas(pruebasTotales);
            EnviarCorreosPruebasMayoresA2000(pruebasTotales);
            InsertarPruebas(pruebasTotales);
        }
    }
}
