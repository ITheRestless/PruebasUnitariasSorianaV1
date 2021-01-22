using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_Pruebas.Modelos
{
    class Modelo_Prueba_Ejecutar
    {
        int idPRUEBAS_EJECUCIONESS;
        int idPRUEBAA;
        string pRUEBA;
        int idEJECUCIONN;
        DateTime fECHAINICIO;
        DateTime fECHAFIN;
        int tIEMPORES;
        string rESPUESTA;
        string eXCEPCION;
        bool estado;
        int iduSUARIO;
        string eNDPOINT;
        string aMBIENTE;
        int bATCH;
        int sTATUSCODE;

        public Modelo_Prueba_Ejecutar()
        {
            rESPUESTA = "";
            eXCEPCION = "";
        }

        public Modelo_Prueba_Ejecutar(int idPRUEBAS_EJECUCIONES, int idPRUEBA, string pRUEBA, int idEJECUCION, DateTime fECHAINICIO, DateTime fECHAFIN, int tIEMPORES, string rESPUESTA, string eXCEPCION, bool eSTADO, int idUSUARIO, string endpoint, string ambiente, int batch)
        {
            this.idPRUEBAS_EJECUCIONES = idPRUEBAS_EJECUCIONES;
            this.idPRUEBA = idPRUEBA;
            PRUEBA = pRUEBA;
            this.idEJECUCION = idEJECUCION;
            FECHAINICIO = fECHAINICIO;
            FECHAFIN = fECHAFIN;
            TIEMPORES = tIEMPORES;
            RESPUESTA = rESPUESTA;
            EXCEPCION = eXCEPCION;
            ESTADO = eSTADO;
            this.idUSUARIO = idUSUARIO;
            ENDPOINT = endpoint;
            AMBIENTE = ambiente;
            BATCH = batch;
        }

        public int idPRUEBAS_EJECUCIONES { get => idPRUEBAS_EJECUCIONESS; set => idPRUEBAS_EJECUCIONESS = value; }
        public int idPRUEBA { get => idPRUEBAA; set => idPRUEBAA = value; }
        public string PRUEBA { get => pRUEBA; set => pRUEBA = value; }
        public int idEJECUCION { get => idEJECUCIONN; set => idEJECUCIONN = value; }
        public DateTime FECHAINICIO { get => fECHAINICIO; set => fECHAINICIO = value; }
        public DateTime FECHAFIN { get => fECHAFIN; set => fECHAFIN = value; }
        public int TIEMPORES { get => tIEMPORES; set => tIEMPORES = value; }
        public string RESPUESTA { get => rESPUESTA; set => rESPUESTA = value; }
        public string EXCEPCION { get => eXCEPCION; set => eXCEPCION = value; }
        public bool ESTADO { get => estado; set => estado = value; }
        public int idUSUARIO { get => iduSUARIO; set => iduSUARIO = value; }
        public string ENDPOINT { get => eNDPOINT; set => eNDPOINT = value; }
        public string AMBIENTE { get => aMBIENTE; set => aMBIENTE = value; }
        public int BATCH { get => bATCH; set => bATCH = value; }
        public int STATUSCODE { get => sTATUSCODE; set => sTATUSCODE = value; }
        public string TIPO { get => "U"; }
    }
}
