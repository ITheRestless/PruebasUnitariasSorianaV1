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
    public class UnitTest
    {

        static Funciones funciones = new Funciones();

        static String NombreTester = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() 
            + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        static Cliente clienteTester = new Cliente("Iv�n " + NombreTester, "tester" + NombreTester + "@unittest.com", "12345678", "Rodr�guez", "Quiroz");
        static Cliente clienteTester3 = new Cliente("Iv�n " + NombreTester, "tester2" + NombreTester + "@unittest.com", "12345678", "Rodr�guez", "Quiroz");
        static ClienteAlterno clienteTester2 = new ClienteAlterno("Iv�n " + NombreTester, "tester" + NombreTester + "@unittest.com", "12345678", "Rodr�guez", "Quiroz");

        static List<Cliente> clientes = new List<Cliente>() {
            new Cliente("Javier", "javiersanchezra@gmail.com", "12345678"),
            new Cliente("Tester", "tester299134156@unittest.com", "12345678"),
            new Cliente("Christopher", "christopher@pruebas.com", "12345678"),
            new Cliente("Pedro", "pedro@pruebas.com", "12345678"),
            new Cliente("Hector", "hector@pruebas.com", "12345678")
        };

        static List<RespuestaCrearLista> listaCrearLista = new List<RespuestaCrearLista>();

        static List<Articulo> listaArticulos = new List<Articulo>()
        {
            new Articulo(245888, 4, 1),
            new Articulo(521, 2, 1),
            new Articulo(245890, 3, 1)
        };

        static Cliente clienteprueba = clientes[1];

        static string clienteJSON = clienteprueba.ToJson();

        static BearerToken token = new BearerToken();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        #region Cliente
        
        [TestMethod]
        public void RegistrarUsuario ()
        {
            funciones.RegisterUser(clienteTester);
        }

        [TestMethod]
        public void ObtenerToken()
        {
            RegistrarUsuario();
            funciones.PruebaLogin_ObtenerToken(clienteTester2, out token);
        }

        [TestMethod]
        public void ModificarCliente()
        {
            RegistrarUsuario();
            funciones.ModificarCliente(out clienteTester2, clienteTester2, token);
        }

        [TestMethod]
        public void ObtenerInformacionUsuario()
        {
            RegistrarUsuario();
            funciones.GetUserInfo(token);
        }

        [TestMethod]
        public void CambiarContrasena()
        {
            RegistrarUsuario();
            funciones.ChangePassword(clienteTester, token);
        }

        [TestMethod]
        public void AceptarTermCond()
        {
            RegistrarUsuario();
            funciones.AceptarTermCond(token);
        }

        [TestMethod]
        public void RecuperarContrasena()
        {
            funciones.RecuperarPass(clienteTester);
        }

        [TestMethod]
        public void ConfirmarCodigo()
        {
            funciones.ConfirmarCodigo(token, clienteTester);
        }

        [TestMethod]
        public void ReenviarCodigo()
        {
            funciones.ReenviarCodigo(token, clienteTester);
        }

        [TestMethod]
        public void VincularTarjeta()
        {
            funciones.VincularTarjeta(token, clienteTester);
        }

        [TestMethod]
        public void VincularTarjetaTicket()
        {
            funciones.VincularTarjetaTicket(token);
        }

        [TestMethod]
        public void CrearVirtual()
        {
            funciones.CrearVirtual(token);
        }

        [TestMethod]
        public void CambioTarjeta()
        {
            funciones.CambioTarjeta(token);
        }

        [TestMethod]
        public void RegisterUser2()
        {
            funciones.RegisterUser2(clienteTester3);
        }

        #endregion

        #region DoSearch

        [TestMethod]
        public void BusquedaPorSentencia()
        {
            funciones.BusquedaPorSentencia();
        }

        [TestMethod]
        public void VerificadorPrecios()
        {
            funciones.VerificadorPrecios();
        }

        [TestMethod]
        public void ProductosPorSubcategoria()
        {
            funciones.ProductosPorSubcategoria();
        }

        [TestMethod]
        public void ArticulosPorPromocion()
        {
            funciones.ArticulosPorPromocion();
        }

        #endregion

        #region Carrito Promos Elegibles

        int vi = 0;

        [TestMethod]
        public void CrearVisita()
        {
            funciones.CrearVisita(token, out vi);
        }

        [TestMethod]
        public void AgregarArticuloCarrito()
        {
            funciones.AgregarArticuloCarrito(token, vi);
        }

        [TestMethod]
        public void ObtenerDetalleCarrito()
        {
            funciones.ObtenerDetalleCarrito(token);
        }

        [TestMethod]
        public void EliminarArticuloCarrito()
        {
            funciones.EliminarArticuloCarrito(token);
        }

        [TestMethod]
        public void CambiarTienda()
        {
            funciones.CambiarTienda(token, vi);
        }

        [TestMethod]
        public void AgragarArregloCarrito()
        {
            funciones.AgregarArregloCarrito4(clienteTester, token);
        }

        [TestMethod]
        public void ComentarioArticuloCarrito()
        {
            funciones.ComentarioArticuloCarrito3(clienteTester, token);
        }

        [TestMethod]
        public void CanjePuntos()
        {
            funciones.CanjePuntos(clienteTester, token);
        }

        #endregion

        #region Direccion

        Direccion direccion = new Direccion();

        [TestMethod]
        public void NuevaDireccion()
        {
            funciones.NuevaDireccion(token, out direccion);
        }

        [TestMethod]
        public void ModificarDireccion()
        {
            funciones.ModificarDireccion(token, ref direccion, out direccion);
        }

        [TestMethod]
        public void ObtenerDirecciones()
        {
            funciones.ObtenerDirecciones(token);
        }

        [TestMethod]
        public void EliminarDireccion()
        {
            funciones.EliminarDireccion(token, direccion);
        }

        [TestMethod]
        public void ObtenerPoblacion()
        {
            funciones.ObtenerPoblacion(token);
        }

        [TestMethod]
        public void ValidarCP()
        {
            funciones.ValidarCP();
        }

        #endregion

        #region Listas

        [TestMethod]
        public void CrearLista()
        {
            funciones.CrearLista(token, out listaCrearLista);
        }

        [TestMethod]
        public void ObtenerListados()
        {
            funciones.ObtenerListados(token);
        }

        [TestMethod]
        public void AgregarALista()
        {
            CrearLista();

            funciones.AgregarALista(token, listaArticulos);
        }

        [TestMethod]
        public void ObtenerDetalleLista()
        {
            funciones.ObtenerDetalleLista(token, listaCrearLista);
        }

        [TestMethod]
        public void EliminarArticuloLista()
        {
            funciones.EliminarArticuloLista(token, 1);
        }

        [TestMethod]
        public void EliminarLista()
        {
            funciones.EliminarLista(token, listaCrearLista);
        }

        #endregion

        #region Datos Fiscales

        DatosFiscales datosFiscales = new DatosFiscales();

        [TestMethod]
        public void RegistrarDatosFiscales()
        {
            funciones.RegistrarDatosFiscales(token, out datosFiscales);
        }

        [TestMethod]
        public void ModificarDatosFiscales()
        {
            funciones.ModificarDatosFiscales(token, datosFiscales.IdFiscal);
        }

        [TestMethod]
        public void EliminarDatosFiscales()
        {
            funciones.EliminarDatosFiscales(token, datosFiscales.IdFiscal);
        }

        #endregion

        #region Estracto por Categor�a Padre

        [TestMethod]
        public void SubCategoriaProductos()
        {
            funciones.SubCategoriaProductos(token);
        }

        #endregion

        #region Estracto de Promociones

        [TestMethod]
        public void EstractoPromociones()
        {
            funciones.EstractoPromociones(token);
        }

        #endregion

        #region Pago

        long idTransaccion = 0;

        [TestMethod]
        public void SaveRedirect()
        {
            funciones.SaveRedirect(token);
        }

        #endregion

        #region Orden

        [TestMethod]
        public void InfoOrden()
        {
            funciones.InfoOrden(token);
        }

        [TestMethod]
        public void OrdenesProceso()
        {
            funciones.OrdenesProceso(token);
        }

        [TestMethod]
        public void DetalleOrden()
        {
            funciones.DetalleOrden(token);
        }

        [TestMethod]
        public void FechaHoraEntrega1()
        {
            funciones.FechaHoraEntrega(token);
        }

        [TestMethod]
        public void FechaHoraEntrega2()
        {
            funciones.FechaHoraEntrega2(token);
        }

        [TestMethod]
        public void FormaPago()
        {
            funciones.FormaPago(token);
        }

        [TestMethod]
        public void CancelarOrden()
        {
            funciones.CancelarOrden(token);
        }

        #endregion

        #region Sin Categor�a

        [TestMethod]
        public void ObtenerFolletos()
        {
            funciones.GetFolletos();
        }

        [TestMethod]
        public void GetHome5()
        {
            funciones.GetHome5();
        }

        public void GetHome6()
        {
            funciones.GetHome6();
        }

        [TestMethod]
        public void GetTickets()
        {
            funciones.GetTickets();
        }

        [TestMethod]
        public void GetEstructuraComercial()
        {
            funciones.GetEstructuraComercial();
        }

        [TestMethod]
        public void Regiones()
        {
            funciones.Regiones();
        }

        #endregion

        #region Stores

        [TestMethod]
        public void ObtenerTiendas()
        {
            funciones.ObtenerTiendas();
        }

        [TestMethod]
        public void ObtenerTiendasCP()
        {
            funciones.ObtenerTiendasCP(token);
        }

        [TestMethod]
        public void PoblacionesTiendas()
        {
            funciones.PoblacionesTiendas(token);
        }

        #endregion

        #region Promociones

        [TestMethod]
        public void ObtenerPromociones()
        {
            funciones.ObtenerPromociones(token);
        }

        [TestMethod]
        public void ActivarPromocion()
        {
            funciones.ActivarPromocion(token);
        }

        #endregion

        #region Banners

        [TestMethod]
        public void ObtenerBanners()
        {
            funciones.ObtenerBanners(token);
        }

        [TestMethod]
        public void ObtenerBannersInicio()
        {
            funciones.ObtenerBannersInicio(token);
        }

        #endregion

        #region Favoritos

        [TestMethod]
        public void AgregarArticulosAFavoritos()
        {
            funciones.AgregarArticulosAFavoritos(token, listaArticulos);
        }

        [TestMethod]
        public void DetalleFavoritos()
        {
            funciones.DetalleFavoritos(token, listaArticulos);
        }

        [TestMethod]
        public void EliminarArticulosDeFavoritos()
        {
            funciones.EliminarArticulosDeFavoritos(token);
        }

        #endregion

    }
}
