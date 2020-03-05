using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;

public partial class IncidentLog : System.Web.UI.Page
{
    #region "Load"
    /// <summary>
    /// Load de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TitleSubtitle1.SetTitle("Parámetros del Reporte");
            //String strBegin = DateTime.Now.Date.ToString("dd/MM/yyyy hh:mm");
            //String strEnd = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            tbStartDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy hh:mm");
            tbEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
        }
    }
    #endregion

    #region "Botones"

    /// <summary>
    /// Boton Generar reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            dsIncidentNotification.__INCIDENT_LOGSDataTable dtIncidentLog = new dsIncidentNotification.__INCIDENT_LOGSDataTable();
            dsIncidentNotificationTableAdapters._INCIDENT_LOGSTableAdapter adapInciden = new dsIncidentNotificationTableAdapters._INCIDENT_LOGSTableAdapter();
            #region DATOS
            /*dtIncidentLog.Rows.Add("Septiembre", "01/09/2011", "01/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / BOWEB", "encolamiento en servidor web", "Herramienta de SistemaºSistemasºBOWEB", "Operaciones / Producción", "44");
        dtIncidentLog.Rows.Add("Septiembre", "01/09/2011", "01/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "Sin causa identificada", "Herramienta de SistemaºSistemasºPortal de Accesos", "Operaciones / Producción", "10");
        dtIncidentLog.Rows.Add("Septiembre", "03/09/2011", "03/09/2011", "Herramientas PosVenta", "n/a", "Problemas con  WEBSPHERE", "Herramienta de SistemaºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "04/09/2011", "04/09/2011", "Herramientas PosVenta", "n/a", "Mantenimiento Programa de habilitación de Nuevo 6509 SWFW2", "Ventana MantenimientoºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "04/09/2011", "04/09/2011", "Herramientas PosVenta", "n/a", "Mantenimiento Programa de habilitación de Nuevo 6509 SWFW2", "Ventana MantenimientoºSistemasºULTIMUS", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "04/09/2011", "04/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "Falla en Glassfish", "Herramienta de SistemaºSistemasºPortal de Accesos", "Operaciones / Producción", "21");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "Suscripción de Promociones", "RECLAMOS / PREPAGO / BONOS Y PROMOCIONES / NOCHES ILIMITADAS", "Mantenimiento programado de actualizaciòn tasa de cambio Septiembre a Diciembre 2011 en NI", "Ventana MantenimientoºPromocionesºActivaciones al 7000", "Operaciones / Producción", "4");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "Suscripción de Promociones", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / SUPER BONOS MOVISTAR / PROMOCIONES", "Mantenimiento programado de actualizaciòn tasa de cambio Septiembre a Diciembre 2011 en NI", "Ventana MantenimientoºPromocionesºSUPERBONO", "Operaciones / Producción", "3");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "Data /Internet /UMTS/3G", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / VELOCIDAD 3G / SERVICIOS DE DATOS", "Mantenimiento programado de actualizaciòn tasa de cambio Septiembre a Diciembre 2011 en NI", "Ventana MantenimientoºPromocionesºActivación UMTS", "Operaciones / Producción", "1");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "SVA  (MMS, Wap, Descargas)", "RECLAMOS / PREPAGO / SERVICIOS DE VALOR AGREGADO / TRANSFERENCIA DE SALDO", "Mantenimiento programado de actualizaciòn tasa de cambio Septiembre a Diciembre 2011 en NI", "Ventana MantenimientoºPromocionesºTransferencia de Saldo", "Operaciones / Producción", "1");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "Sin causa identificada", "Herramienta de SistemaºSistemasºPortal de Accesos", "Operaciones / Producción", "34");
        dtIncidentLog.Rows.Add("Septiembre", "05/09/2011", "05/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / BOWEB", "Sin causa identificada, Soporte PP CA atendio e indico los siguiente:se esta atendiendo a actividades de produccion, pero de parte de weblogic no se ve ningun detalle", "Herramienta de SistemaºSistemasºBOWEB", "Operaciones / Producción", "73");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Recargas", "RECLAMOS / PREPAGO / RECARGAS / RECARGAS NO APLICADAS", "Se realizará aumento del aumento del valor de FILE DESCRIPTOR de 256 A 8192", "Ventana MantenimientoºSistemasºRecarga Electrónica", "Operaciones / Producción", "4");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "SVA  (MMS, Wap, Descargas)", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / BOWEB", "Se realizará aumento del aumento del valor de FILE DESCRIPTOR de 256 A 8192", "Ventana MantenimientoºSistemasºConsulta de Saldo USSD", "Operaciones / Producción", "7");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / BOWEB", "lentitud en tuxedo de consulta de saldo", "Herramienta de SistemaºSistemasºConsulta de Saldo BOWEB", "Operaciones / Producción", "121");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "Incidencia con ALTAMIRA", "Herramienta de SistemaºSistemasºConsulta de Saldo de Gestor Altamira", "Operaciones / Producción", "82");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Recargas", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / RECARGAS NO APLICADAS / BOWEB", "Reinicio Controlado en Máquinas de SDP", "Prestación del ServicioºSistemasºRecarga Electrónica", "Operaciones / Producción", "464");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Suscripción de Promociones", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / SMS ILIMITADO NACIONAL / PROMOCIONES", "Lentitud en ejecución de tuxedos", "Prestación del ServicioºSistemasºActivaciones al 7000", "Operaciones / Producción", "252");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Suscripción de Promociones", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / SMS ILIMITADO NACIONAL / PROMOCIONES", "Lentitud en ejecución de tuxedos", "Prestación del ServicioºSistemasºSUPERBONO", "Operaciones / Producción", "252");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "Herramientas PosVenta", "n/a", "PENDIENTE", "Prestación del ServicioºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "(Dúo, Ilimitada, Día Movistar)", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / DUO / PROMOCIONES", "Intermitencia en la Red", "Prestación del ServicioºREDºComunicación Dúo", "Operaciones / Producción", "9");
        dtIncidentLog.Rows.Add("Septiembre", "06/09/2011", "06/09/2011", "(Dúo, Ilimitada, Día Movistar)", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / DUO / PROMOCIONES", "Inconveniente con el SDP en Nicaragua", "Prestación del ServicioºREDºComunicación Dúo", "Operaciones / Producción", "2");
        dtIncidentLog.Rows.Add("Septiembre", "07/09/2011", "07/09/2011", "Recargas", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / RECARGAS NO APLICADAS / BOWEB", "PENDIENTE", "Prestación del ServicioºSistemasºRecarga Electrónica", "Operaciones / Producción", "2");
        dtIncidentLog.Rows.Add("Septiembre", "07/09/2011", "07/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / RECARGAS NO APLICADAS / BOWEB", "Mantenimiento en el SDP en Ni", "Ventana MantenimientoºSistemasºConsulta de Saldo BOWEB", "Operaciones / Producción", "1");
        dtIncidentLog.Rows.Add("Septiembre", "07/09/2011", "07/09/2011", "Servicios Básicos (SMS,  Buzón de Voz)", "RECLAMOS / POSPAGO / INCIDENCIAS MASIVAS / PAQUETES SMS NACIONAL / NO ENVIO\\RECEPCION DE SMS MASIVO", "Mantenimiento en el SDP en Ni", "Ventana MantenimientoºREDºSMS", "Operaciones / Producción", "8");
        dtIncidentLog.Rows.Add("Septiembre", "07/09/2011", "07/09/2011", "Herramientas PosVenta", "N/A", "JavaDeadLock por threads bloqueados exclusivamente", "Herramienta de SistemaºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Data /Internet /UMTS/3G", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / NAVEGACION INTERNET / SERVICIOS DE DATOS", "Cambio de Configuración en Router de Monterrey", "Ventana MantenimientoºSistemasº3G", "Operaciones / Producción", "1");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "(Dúo, Ilimitada, Día Movistar)", "RECLAMOS / PREPAGO / BONOS Y PROMOCIONES / DIA MOVISTAR / TRIPLICA TU SALDO", "PENDIENTE", "Prestación del ServicioºPromocionesºDía Movistar - Triplica", "Operaciones / Producción", "6");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºConsulta de Saldo Gestor Altamira", "Operaciones / Producción", "8");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºDetalle de llamadas Prepago", "Operaciones / Producción", "9");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºConsulta de Saldo Gestor Altamira", "Operaciones / Producción", "6");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºDetalle de llamadas Prepago", "Operaciones / Producción", "6");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºControl de Recargas", "Operaciones / Producción", "7");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "n/a", "PENDIENTE", "Herramienta de SistemaºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "n/a", "PENDIENTE", "Herramienta de SistemaºSistemasºGAC", "Operaciones / Producción", "n/a");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºControl de Recargas", "Operaciones / Producción", "14");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºDetalle de llamadas Prepago", "Operaciones / Producción", "14");
        dtIncidentLog.Rows.Add("Septiembre", "08/09/2011", "08/09/2011", "Herramientas PosVenta", "RECLAMOS / PREPAGO / INCIDENCIAS MASIVAS / CONSULTA NO EFECTUADA POR INDISPONIBILIDAD SISTEMAS / OTROS SISTEMAS", "PENDIENTE", "Herramienta de SistemaºSistemasºConsulta de Saldo Gestor Altamira", "Operaciones / Producción", "14");*/
            #endregion


            TitleSubtitle1.SetTitle("Bitácora de Incidencias");

            //Validamos que existan registros con los filtros enviados 
            DateTime fecInicio;
            DateTime fecFinal;
            DateTime.TryParseExact(tbStartDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecInicio);
            DateTime.TryParseExact(tbEndDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecFinal);
            dtIncidentLog = adapInciden.GetIncidentLogByDate(fecInicio, fecFinal, Convert.ToDecimal(ddlCountry.SelectedValue));

            //Fuente del Reporte
            dtsReport.rptIncidentLogDataTable tblReport = new dtsReport.rptIncidentLogDataTable();
            dtsReport.rptIncidentLogRow rowReport = null;
            if (dtIncidentLog != null && dtIncidentLog.Rows.Count > 0) //Tiene registros 
            {
                foreach (dsIncidentNotification.__INCIDENT_LOGSRow record in dtIncidentLog)
                {
                    //Agregamos un row 
                    rowReport = tblReport.NewrptIncidentLogRow();

                    if (!record.IsCOD_INCIDENCENull())
                    {
                        rowReport.Codigo = record.COD_INCIDENCE.ToString();
                    }
                    if (!record.IsCRITICALITYNull())
                    {
                        rowReport.Criticidad = record.CRITICALITY.ToString();
                    }
                    if (!record.IsENDDATENull())
                    {
                        rowReport.FechaFin = record.ENDDATE.ToString("dd/MM/yyyy hh:mm:ss tt");
                    }
                    if (!record.IsFOLIO_OTNull() && record.FOLIO_OT != "-1" && record.FOLIO_OT != "-2")
                    {
                        rowReport.FolioOT = record.FOLIO_OT;
                    }
                    if (!record.IsAFFECTEDSERVICESNull())
                    {
                        rowReport.ServAfectado = record.AFFECTEDSERVICES;
                    }
                    if (!record.IsLEVEL_COLORNull() && record.LEVEL_COLOR != "-1" && record.LEVEL_COLOR != "-2")
                    {
                        rowReport.NivelEscalamiento = string.Format("#{0}", record.LEVEL_COLOR);
                    }
                    if (!record.IsMONTHNull())
                    {
                        rowReport.Mes = record.MONTH;
                    }
                    if (!record.IsSEGMENTNull())
                    {
                        rowReport.Segmento = record.SEGMENT;
                    }
                    if (!record.IsTYPENull())
                    {
                        rowReport.Tipo = record.TYPE;
                    }
                    if (!record.IsMOTIVENull())
                    {
                        rowReport.Causa = record.MOTIVE;
                    }
                    if (!record.IsSOLUTIONRESPONSIBLENull())
                    {
                        rowReport.RespSolucion = record.SOLUTIONRESPONSIBLE;
                    }
                    if (!record.IsTOTALINCOMINGCALLSNull())
                    {
                        rowReport.TotalLLamadas = record.TOTALINCOMINGCALLS.ToString();
                    }
                    if (!record.IsLEVEL_SEQUENCENull() && record.LEVEL_SEQUENCE != "-1" && record.LEVEL_SEQUENCE != "-2")
                    {
                        rowReport.SequenciaNivelEscalamiento = record.LEVEL_SEQUENCE;
                    }
                    if (!record.IsNAME_COUNTRYNull())
                    {
                        rowReport.Pais = record.NAME_COUNTRY;
                    }
                    if (!record.IsTIME_SOLUTIONNull() && record.TIME_SOLUTION != "N/D")
                    { rowReport.TotalDuracion = record.TIME_SOLUTION; }
                    else if (!record.IsENDDATENull())
                    {
                        int minutes, seconds;

                        minutes = Convert.ToInt32(Math.Truncate((record.ENDDATE - record.STARTDATE).TotalMinutes));
                        seconds = (record.ENDDATE - record.STARTDATE).Seconds;



                        if (minutes > 999999)
                        {
                            rowReport.TotalDuracion = "##+999999";

                        }
                        else
                        {
                            string strminutes = string.Empty, strseconds = string.Empty;


                            strminutes = ((minutes <= 9) ? "0" + minutes.ToString() : minutes.ToString());
                            strseconds = ((seconds <= 9) ? "0" + seconds.ToString() : seconds.ToString());

                            rowReport.TotalDuracion = strminutes + ":" + strseconds;

                        }
                    }

                    if (!record.IsSUBJECTNull())
                    {
                        rowReport.Asunto = record.SUBJECT;
                    }
                    if (!record.IsCLASIFICACIONNull())
                    {
                        rowReport.Clasificacion = record.CLASIFICACION;
                    }

                    if (!record.IsHORA_CIERRENull())
                    {
                        rowReport.HoraCierre = record.HORA_CIERRE;
                    }
                    rowReport.FechaInicio = record.STARTDATE.ToString("dd/MM/yyyy hh:mm:ss tt");
                    rowReport.Tipologia = record.TYPOLOGY.ToString();

                    //Agregamos el row a la tabla
                    tblReport.AddrptIncidentLogRow(rowReport);
                }


                //Mostramos el reporte
                rvIncidentLog.LocalReport.ReportPath = "Reports\\rptIncidentLog.rdlc";
                rvIncidentLog.LocalReport.DataSources.Clear();
                //rvIncidentLog.LocalReport.DataSources.Add(rds);
                DataSet dtsRpt = new DataSet();
                dtsRpt.Tables.Add(tblReport);
                rvIncidentLog.LocalReport.DataSources.Add(new ReportDataSource("dtsReporte", dtsRpt.Tables[0]));
                rvIncidentLog.LocalReport.Refresh();
                mvAffectedClients.ActiveViewIndex = 1;
            }
            else //No tiene registros
            {
                //Mostramos mensaje
                wucMessageControl.Message = "No se Encontraron Registros.";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.ShowPopup();
            }
        }
        catch (Exception ex)
        {
        }
        

    }

    /// <summary>
    /// Boton regresar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            tbEndDate.Text = DateTime.Now.ToShortDateString();
            tbStartDate.Text = DateTime.Now.ToShortDateString();
            ddlCountry.ClearSelection();
            mvAffectedClients.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {

        }

    }
    
    #endregion

    #region "Combos"
    /// <summary>
    /// Combo Pais
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_DataBound(object sender, EventArgs e)
    {
        ddlCountry.Items.Insert(0, new ListItem("Seleccione", "-1"));
    }
    #endregion

    

}