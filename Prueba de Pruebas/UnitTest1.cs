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
        public void pruebasUnitarias()
        {
            inicializar();

            List<Modelo_Prueba_Ejecutar> pruebasTotales = new List<Modelo_Prueba_Ejecutar>();

            #region Cliente
            // EJECUTAR PRUEBA REGISTRAR USUARIO
            Modelo_Prueba_Ejecutar RespuestaRegistrarUsuario = funciones.RegisterUser(clienteTester);
            RespuestaRegistrarUsuario.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(RespuestaRegistrarUsuario);

            // EJECUTAR PRUEBA OBTENER TOKEN
            Modelo_Prueba_Ejecutar RespuestaObtenerToken = funciones.PruebaLogin_ObtenerToken(clienteTester, out token);
            RespuestaObtenerToken.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(RespuestaObtenerToken);

            // EJECUTAR PRUEBA DE MODIFICAR CLIENTE
            Modelo_Prueba_Ejecutar REspuestaMOdificarCliente = funciones.ModificarCliente(out clienteTester, clienteTester, token);
            REspuestaMOdificarCliente.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(REspuestaMOdificarCliente);

            // EJECUTAR PRUEBA OBTENER USER INFO USUARIO
            Modelo_Prueba_Ejecutar ResponseGetUserInfo = funciones.GetUserInfo(token);
            ResponseGetUserInfo.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseGetUserInfo);

            // EJECUTAR PRUEBA PARA CAMBIAR CONTRASEÑA
            Modelo_Prueba_Ejecutar ResponseCambiarPassword = funciones.ChangePassword(clienteTester, token);
            ResponseCambiarPassword.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseCambiarPassword);

            // EJECUTAR PRUEBA TÉRMINOS Y CONDICIONES
            Modelo_Prueba_Ejecutar ReponseTerminosCond = funciones.AceptarTermCond(token);
            ReponseTerminosCond.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseTerminosCond);

            // EJECUTAR PRUEBA PARA RECUPERAR CONTRASEÑA
            Modelo_Prueba_Ejecutar ReponseRecuperarPass = funciones.RecuperarPass(clienteTester);
            ReponseRecuperarPass.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseRecuperarPass);
            #endregion

            #region DoSearch
            //EJECUTAR PRUEBA DE BÚSQUEDA POR SENTENCIA
            Modelo_Prueba_Ejecutar ReponseDoSearch = funciones.BusquedaPorSentencia();
            ReponseDoSearch.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseDoSearch);

            //EJECUTAR PRUEBA DE VERIFICADOR DE PRECIOS
            Modelo_Prueba_Ejecutar ResponseVerificadorPrecios = funciones.VerificadorPrecios();
            ResponseVerificadorPrecios.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseVerificadorPrecios);

            //EJECUTAR PRUEBA DE SUBCATEGORIAS
            Modelo_Prueba_Ejecutar ResponseSubCategoria = funciones.ProductosPorSubcategoria();
            ResponseSubCategoria.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseSubCategoria);
            #endregion

            #region Carrito promos elegibles
            int vi = 0;

            //EJECUTAR PRUEBA CREAR VISITA
            Modelo_Prueba_Ejecutar ResponseCrearVisita = funciones.CrearVisita(token, out vi);
            ResponseCrearVisita.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseCrearVisita);

            //EJECUTAR PRUEBA AGREGAR ARTÍCULO A CARRITO
            Modelo_Prueba_Ejecutar ResponseAgregarArticuloCarrito = funciones.AgregarArticuloCarrito(token, vi);
            ResponseAgregarArticuloCarrito.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseAgregarArticuloCarrito);

            //EJECUTAR PRUEBA OBTENER DETALLE CARRITO
            Modelo_Prueba_Ejecutar ResponseObtenerDetalleCarrito = funciones.ObtenerDetalleCarrito(token);
            ResponseObtenerDetalleCarrito.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseObtenerDetalleCarrito);

            //EJECUTAR PRUEBA ELIMINAR ARTÍCULO DE CARRITO
            Modelo_Prueba_Ejecutar ResponseEliminarArticuloCarrito = funciones.EliminarArticuloCarrito(token);
            ResponseEliminarArticuloCarrito.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseEliminarArticuloCarrito);

            //EJECUTAR PRUEBA CAMBIO DE TIENDA
            Modelo_Prueba_Ejecutar ReponseCambiodeTienda = funciones.CambiarTienda(token, vi);
            ReponseCambiodeTienda.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseCambiodeTienda);

            // EJECUTAR PRUEBA COMENTARIO EN EL CARRITO
            Modelo_Prueba_Ejecutar ReponseComentarioCarrito = funciones.ComentarioCarrito(clienteTester, token);
            ReponseComentarioCarrito.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseComentarioCarrito);

            //// EJECUTAR PRUEBA AGREGAR ARTICULOS A CARRITO 4 ----------------- FALTA ELIMINAR ARTICULO CARRITO 3
            Modelo_Prueba_Ejecutar ReponseAgregarArregloCarrito4 = funciones.AgregarArregloCarrito4(clienteTester, token);
            ReponseAgregarArregloCarrito4.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseAgregarArregloCarrito4);

            //// EJECUTAR PRUEBA AGREGAR ComentarioArticuloCarrito2
            Modelo_Prueba_Ejecutar ReponseComentarioArticuloCarrito2 = funciones.ComentarioArticuloCarrito2(clienteTester, token);
            ReponseComentarioArticuloCarrito2.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseComentarioArticuloCarrito2);

            //// EJECUTAR PRUEBA CANJEAR PUNTOS
            Modelo_Prueba_Ejecutar ReponseCanjePuntos = funciones.CanjePuntos(clienteTester, token);
            ReponseCanjePuntos.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseCanjePuntos);
            #endregion

            #region Direcciones
            Direccion direccion = new Direccion();
            //EJECUTAR PRUEBA NUEVA DIRECCIÓN
            Modelo_Prueba_Ejecutar ReponseNuevaDireccion = funciones.NuevaDireccion(token, out direccion);
            ReponseNuevaDireccion.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseNuevaDireccion);

            //EJECUTAR PRUEBA MODIFICAR DIRECCIÓN
            Modelo_Prueba_Ejecutar ResponseModificarDireccion = funciones.ModificarDireccion(token, ref direccion, out direccion);
            ResponseModificarDireccion.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseModificarDireccion);

            //EJECUTAR PRUEBA OBTENER DIRECCIONES
            Modelo_Prueba_Ejecutar ResponseObtenerDirecciones = funciones.ObtenerDirecciones(token);
            ResponseObtenerDirecciones.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseObtenerDirecciones);

            // EJECUTAR PRUEBA ELIMINAR DIRECCIÓN
            Modelo_Prueba_Ejecutar ReponseEliminarDireccion = funciones.EliminarDireccion(token, direccion);
            ReponseEliminarDireccion.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseEliminarDireccion);

            // EJECUTAR PRUEBA OBTENER POBLACIÓN
            Modelo_Prueba_Ejecutar ReponseObtenerPoblacion = funciones.ObtenerPoblacion(token);
            ReponseObtenerPoblacion.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseObtenerPoblacion);
            #endregion Direcciones

            #region Listas
            //EJECUTAR PRUEBA CREAR LISTA
            List<RespuestaCrearLista> listaCrearLista = new List<RespuestaCrearLista>();

            Modelo_Prueba_Ejecutar ResponseCrearLista = funciones.CrearLista(token, out listaCrearLista);
            ResponseCrearLista.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseCrearLista);

            // EJECUTAR PRUEBA PARA CAMBIAR EL NOMBRE
            Modelo_Prueba_Ejecutar ReponseCambiarNombre = funciones.CambiarNombre(clienteTester, token, out listaCrearLista, listaCrearLista);
            ReponseCambiarNombre.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseCambiarNombre);

            //EJECUTAR PRUEBA OBTENER LISTAS
            Modelo_Prueba_Ejecutar ResponseObtenerListas = funciones.ObtenerListados(token);
            ResponseObtenerListas.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseObtenerListas);

            List<Articulo> listaArticulos = new List<Articulo>();
            listaArticulos.Add(new Articulo()
            {
                articulo = 245888,
                cantidad = 4,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 521,
                cantidad = 2,
                idLista = listaCrearLista[0].IdLista
            });
            listaArticulos.Add(new Articulo()
            {
                articulo = 245890,
                cantidad = 3,
                idLista = listaCrearLista[0].IdLista
            });

            //EJECUTAR PRUEBA AGREGAR ARTÍCULO A LISTA
            Modelo_Prueba_Ejecutar ResponseAgregarLista = funciones.AgregarALista(token, listaArticulos);
            ResponseAgregarLista.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseAgregarLista);

            //EJECUTAR PRUEBA OBTENER DETALLE DE LISTA
            Modelo_Prueba_Ejecutar ReponseDetalleLista = funciones.ObtenerDetalleLista(token, listaCrearLista);
            ReponseDetalleLista.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseDetalleLista);

            //EJECUTAR PRUEBA ELIMINAR ARTÍCULO DE LISTA
            Modelo_Prueba_Ejecutar ResponseEliminarARticulo = funciones.EliminarArticuloLista(token, listaCrearLista);
            ResponseEliminarARticulo.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseEliminarARticulo);

            //EJECUTAR PRUEBA ELIMINAR   LISTA
            Modelo_Prueba_Ejecutar ResponseEliminarLista = funciones.EliminarLista(token, listaCrearLista);
            ResponseEliminarLista.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseEliminarLista);
            #endregion

            #region Datos fiscales
            DatosFiscales datosFiscales = new DatosFiscales();

            // EJECUTAR PRUEBA REGISTRAR DATOS FISCALES
            Modelo_Prueba_Ejecutar ReponseRegistrarDatosFiscales = funciones.RegistrarDatosFiscales(token, out datosFiscales);
            ReponseRegistrarDatosFiscales.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseRegistrarDatosFiscales);

            // EJECUTAR PRUEBA MODIFICAR DATOS FISCALES
            Modelo_Prueba_Ejecutar ReponseModificarDatosFiscales = funciones.ModificarDatosFiscales(token, datosFiscales.IdFiscal);
            ReponseModificarDatosFiscales.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseModificarDatosFiscales);

            // EJECUTAR PRUEBA ELIMINA DATOS FISCALES
            Modelo_Prueba_Ejecutar ReponseEliminarDatosFiscales = funciones.EliminarDatosFiscales(token, datosFiscales.IdFiscal);
            ReponseEliminarDatosFiscales.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseEliminarDatosFiscales);
            #endregion Datos fiscales

            #region Estracto por categoria padre
            //EJECUTAR PRUEBA OBTENER SUBCATEGORIAS CON PRODUCTOS
            Modelo_Prueba_Ejecutar ResponseSubCategoriaProducto = funciones.SubCategoriaProductos(token);
            ResponseSubCategoriaProducto.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ResponseSubCategoriaProducto);
            #endregion

            #region Estracto de promociones
            //EJECUTAR PRUEBA OBTENER EXTRACTO PROMOCIONES
            Modelo_Prueba_Ejecutar ReponseExtracto = funciones.EstractoPromociones(token);
            ReponseExtracto.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseExtracto);
            #endregion

            #region Pago
            // EJECUTAR PRUEBA GENERAR URL
            long idTransaccion = 0;
            Modelo_Prueba_Ejecutar ReponseGenerarURL = funciones.GenerarURL(token, out idTransaccion);
            ReponseGenerarURL.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseGenerarURL);

            /* EJECUTAR PRUEBA PAGO RESPUESTA
            Modelo_Prueba_Ejecutar ReponsePagoRespuesta = funciones.PagoRespuesta(token, idTransaccion);
            ReponsePagoRespuesta.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponsePagoRespuesta);*/
            #endregion

            #region Orden
            //Orden orden = new Orden();
            /*EJECUTAR PRUEBA CREAR ORDEN
            Modelo_Prueba_Ejecutar ReponseCrearOrden = funciones.CrearOrden(token, out orden);
            ReponseCrearOrden.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseCrearOrden); */

            //// EJECUTAR PRUEBA INFO ORDEN
            Modelo_Prueba_Ejecutar ReponseInfoOrden = funciones.InfoOrden(token);
            ReponseInfoOrden.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseInfoOrden);

            //EJECUTAR PRUEBA ORDENES EN PROCESO
            Modelo_Prueba_Ejecutar ReponseOrdenesProceso = funciones.OrdenesProceso(token);
            ReponseOrdenesProceso.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseOrdenesProceso);

            // EJECUTAR PRUEBA DETALLE DE ORDEN
            Modelo_Prueba_Ejecutar ReponseDetalleOrden = funciones.DetalleOrden(token);
            ReponseDetalleOrden.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseDetalleOrden);

            // EJECUTAR PRUEBA FechaHoraEntrega 
            Modelo_Prueba_Ejecutar ReponseFechaHoraEntrega = funciones.FechaHoraEntrega1(token);
            ReponseFechaHoraEntrega.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseFechaHoraEntrega);

            // EJECUTAR PRUEBA FechaReestructurado
            Modelo_Prueba_Ejecutar ReponseFechaReestructurado = funciones.FechaHoraEntrega2(token);
            ReponseFechaReestructurado.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseFechaReestructurado);

            // EJECUTAR PRUEBA FORMA DE PAGO
            Modelo_Prueba_Ejecutar ReponseFormaPago = funciones.FormaPago(token);
            ReponseFormaPago.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseFormaPago);

            //// EJECUTAR PRUEBA CANCELAR ORDEN
            //Modelo_Prueba_Ejecutar ReponseCancelarOrden = funciones.CancelarOrden(token);
            //ReponseCancelarOrden.idEJECUCION = EjecucionaInsertar;
            // pruebasTotales.Add(ReponseCancelarOrden);
            #endregion

            #region Sin Categoria
            // EJECUTAR PRUEBA RESIZE IMAGE CONTROLLER
            Modelo_Prueba_Ejecutar ReponseResizeImage = funciones.ResizeImage();
            ReponseResizeImage.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseResizeImage);

            // EJECUTAR PRUEBA REGIONES
            Modelo_Prueba_Ejecutar ReponseGetRegiones = funciones.GetFolletos();
            ReponseGetRegiones.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseGetRegiones);

            // EJECUTAR PRUEBA FEEDBACK
            //Modelo_Prueba_Ejecutar ReponseSendFeedback = funciones.SendFeedback(clienteTester); ---se desactivo
            //ReponseSendFeedback.idEJECUCION = EjecucionaInsertar;
            //pruebasTotales.Add(ReponseSendFeedback);

            // EJECUTAR PRUEBA HOME 2 CON PROMOCIONES
            Modelo_Prueba_Ejecutar ReponseGetHome2 = funciones.GetHome2();
            ReponseGetHome2.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseGetHome2);

            // EJECUTAR PRUEBA GET TICKETS
            Modelo_Prueba_Ejecutar ReponseGetTickets = funciones.GetTickets();
            ReponseGetTickets.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseGetTickets);

            // EJECUTAR PRUEBA ESTRUCTURA COMERCIAL
            Modelo_Prueba_Ejecutar ReponseGetEstructuraComercial = funciones.GetEstructuraComercial();
            ReponseGetEstructuraComercial.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseGetEstructuraComercial);
            #endregion

            #region Stores
            // EJECUTAR PRUEBA OBTENER TIENDAS
            Modelo_Prueba_Ejecutar ReponseObtenerTiendas = funciones.ObtenerTiendas();
            ReponseObtenerTiendas.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseObtenerTiendas);

            // EJECUTAR PRUEBA OBTENER TIENDAS POR CP
            Modelo_Prueba_Ejecutar ReponseObtenerTiendasCP = funciones.ObtenerTiendasCP(token);
            ReponseObtenerTiendasCP.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseObtenerTiendasCP);

            // EJECUTAR PRUEBA OBTENER TIENDAS POR CP
            Modelo_Prueba_Ejecutar ReponsePoblacionesTiendas = funciones.PoblacionesTiendas(token);
            ReponsePoblacionesTiendas.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponsePoblacionesTiendas);
            #endregion

            #region Promociones
            // EJECUTAR PRUEBA OBTENER PROMOCIONES
            Modelo_Prueba_Ejecutar ReponseObtenerPromociones = funciones.ObtenerPromociones(token);
            ReponseObtenerPromociones.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseObtenerPromociones);

            // EJECUTAR PRUEBA ACTIVAR PROMOCIONES
            //Modelo_Prueba_Ejecutar ReponseActivarPromocion = funciones.ActivarPromocion(token); ------------------Pedir una promocion para activar
            //ReponseActivarPromocion.idEJECUCION = EjecucionaInsertar;
            //pruebasTotales.Add(ReponseActivarPromocion);
            #endregion

            #region Banners
            // EJECUTAR PRUEBA OBTENER BANNERS DE INICIO
            Modelo_Prueba_Ejecutar ReponseObtenerBannersInicio = funciones.ObtenerBannersInicio(token);
            ReponseObtenerBannersInicio.idEJECUCION = EjecucionaInsertar;
            pruebasTotales.Add(ReponseObtenerBannersInicio);
            #endregion

            List<int> evaluacionPruebas = CorrectasIncorrectas(pruebasTotales);
            EnviarCorreosPruebasMayoresA2000(pruebasTotales);
            InsertarPruebas(pruebasTotales);
        }
    }
}
