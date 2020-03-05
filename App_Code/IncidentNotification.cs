using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Configuration;
using Oracle.DataAccess.Types;

#region ESTRUCTURA GLOBAL
/// <summary>
/// Fecha: 17.Oct.2011
/// Clase utilizada como parámetro de 
/// salida de cada una de las estructuras que retornan los métodos
/// </summary>
public class GlobalStructure
{
    #region VARIABLES

    private string message;
    private int status;

    #endregion

    #region PROPIEDADES

    /// <summary>
    ///  Mensaje del estado de la ejecución de método
    /// </summary>
    public string Message
    {
        get { return message; }
        set { message = value; }
    }

    /// <summary>
    ///  Estado de la ejecución del Método 1= Ejecutado, 0= Error
    /// </summary>
    public int Status
    {
        get { return status; }
        set { status = value; }
    }

    #endregion

    #region CONSTRUCTOR DE LA CLASE

    public GlobalStructure()
    {

    }

    #endregion
}
#endregion

#region CLIENTES AFECTADOS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// Clientes afectados
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:18.Oct.2011
/// </summary>
public class AffectedClient
{
    #region CONSTRUCTOR DE LA CLASE

    public AffectedClient()
    {
        messages = new GlobalStructure();
        dtAffectedClients = new dsIncidentNotification.IN_AFFECTED_CLIENTSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string name;
    private string status;
    private decimal codAffectedClient;
    private DateTime recordDate;

    private dsIncidentNotification.IN_AFFECTED_CLIENTSDataTable dtAffectedClients;

    #endregion

    #region PROPIEDADES

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }


    public DateTime RecordDate
    {
        get { return recordDate; }
        set { recordDate = value; }
    }

    public decimal CodAffectedClient
    {
        get { return codAffectedClient; }
        set { codAffectedClient = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    public dsIncidentNotification.IN_AFFECTED_CLIENTSDataTable AffectedClientsTable
    {
        get { return dtAffectedClients; }
        set { dtAffectedClients = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de clientes Afectados
    /// Fecha : 17.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetAffectedClients(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter IN_AFFECTED_CLIENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter();
            IN_AFFECTED_CLIENTSTableAdapter.Fill(this.AffectedClientsTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la información de un cliente afectado
    /// Fecha : 17.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="CodAffectedClient">código del cliente</param>
    public void EditAffectedClient(Decimal codAffectedClient)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter IN_AFFECTED_CLIENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter();
            IN_AFFECTED_CLIENTSTableAdapter.FillByCodAffectedClient(AffectedClientsTable, codAffectedClient);

            Name = AffectedClientsTable[0].NAME;
            Status = AffectedClientsTable[0].RECORD_STATUS;
            RecordDate = AffectedClientsTable[0].RECORD_DATE;

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un nuevo cliente afectado
    /// Fecha : 17.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">informacion del cliente afectado</param>
    public void InsertAffectedClient(AffectedClient info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter IN_AFFECTED_CLIENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter();
            IN_AFFECTED_CLIENTSTableAdapter.InsertAffectedClient(info.Name, info.Status.ToString());

            this.Messages.Message = "Cliente Afectado Ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza la información de un cliente afectado
    /// Fecha : 17.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info"></param>
    /// <param name="codAffectedClient"></param>
    public void UpdateAffectedClient(AffectedClient info, Decimal codAffectedClient)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter IN_AFFECTED_CLIENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter();
            IN_AFFECTED_CLIENTSTableAdapter.UpdateAffectedClient(info.Name, info.Status.ToString(), codAffectedClient);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la cantidad de 
    /// incidencias no concluidas para este Cliente afectado
    /// </summary>
    /// <param name="codSegment">Codigo del cliente afectado</param>
    /// <returns>cantidad de registros de incidencias</returns>
    public Int32 GetUnfinishedIncidents(Decimal codAffectedClient)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter taClients = new dsIncidentNotificationTableAdapters.IN_AFFECTED_CLIENTSTableAdapter();
            counter = Convert.ToInt32(taClients.UnfinishedIncidences(codAffectedClient));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }


    #endregion
}
#endregion

#region RESPONSABLES DE SOLUCION
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los responsables de solución
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:18.Oct.2011
/// </summary>
public class SolutionResponsible
{
    #region CONSTRUCTOR DE LA CLASE

    public SolutionResponsible()
    {
        messages = new GlobalStructure();
        //dtSolutionResponsibles = new dsIncidentNotification.IN_SOLUTION_RESPONSIBLESDataTable();
        dtSolutionResponsibles = new dsIncidentNotification.IN_SOLUTION_RESPONSIBLES_COUNTRYDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string name;
    private string status;
    private string area;
    private string email;
    private decimal codSolutionResponsible;
    private DateTime recordDate;

    //private dsIncidentNotification.IN_SOLUTION_RESPONSIBLESDataTable dtSolutionResponsibles;
    private dsIncidentNotification.IN_SOLUTION_RESPONSIBLES_COUNTRYDataTable dtSolutionResponsibles;
    private decimal idCountryR;
    private dsIncidentNotification.IN_SOLUTION_RESPONSIBLESDataTable dtSolutionRespEdit;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Nombre del Responsable
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Estado del Responsable 
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    /// <summary>
    /// Correo electrónico del
    /// responsable 
    /// </summary>
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    /// <summary>
    /// Área del responable
    /// </summary>
    public string Area
    {
        get { return area; }
        set { area = value; }
    }

    /// <summary>
    /// Fecha del registro
    /// </summary>
    public DateTime RecordDate
    {
        get { return recordDate; }
        set { recordDate = value; }
    }

    /// <summary>
    /// Id del Responsable de solución
    /// </summary>
    public decimal CodSolutionResponsible
    {
        get { return codSolutionResponsible; }
        set { codSolutionResponsible = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// responsables de solución
    /// </summary>
    public dsIncidentNotification.IN_SOLUTION_RESPONSIBLES_COUNTRYDataTable SolutionResponsiblesTable
    {
        get { return dtSolutionResponsibles; }
        set { dtSolutionResponsibles = value; }
    }


    /// <summary>
    /// Identificador del País del responsable
    /// </summary>
    public decimal IdCountryR
    {
        get { return idCountryR; }
        set { idCountryR = value; }
    }


    /// <summary>
    /// Tabla con los datos de los
    /// responsables de solución para edición
    /// </summary>
    public dsIncidentNotification.IN_SOLUTION_RESPONSIBLESDataTable SolutionRespEdit
    {
        get { return dtSolutionRespEdit; }
        set { dtSolutionRespEdit = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Responsables de solución
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetSolutionResponsibles(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter IN_SOLUTION_RESPONSIBLESTableAdapter = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter();
            IN_SOLUTION_RESPONSIBLESTableAdapter.Fill(SolutionResponsiblesTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene el listado de Responsables de solución
    /// Según país
    /// Fecha : 09/07/2014
    /// Autor : Xolo
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetSolutionResponsiblesByCountry(Decimal idCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter IN_SOLUTION_RESPONSIBLESTableAdapter = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter();
            IN_SOLUTION_RESPONSIBLESTableAdapter.FillByCountry(SolutionResponsiblesTable, idCountry);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene la información de un Responsable de 
    /// solución
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="codSolutionResponsible">código del responsable</param>
    public void EditSolutionResponsible(Decimal codSolutionResponsible)
    {
        try
        {
            // dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter taSolutionResponsible = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter();
            //taSolutionResponsible.FillByCodSolutionResponsible(SolutionResponsiblesTable, codSolutionResponsible);
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter taSolutionResponsible = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLES_COUNTRYTableAdapter();
            taSolutionResponsible.FillByCodSolutionResponsible(SolutionResponsiblesTable, codSolutionResponsible);


            Name = SolutionResponsiblesTable[0].NAME;
            Email = SolutionResponsiblesTable[0].EMAIL;
            Area = SolutionResponsiblesTable[0].AREA;
            Status = SolutionResponsiblesTable[0].RECORD_STATUS;
            RecordDate = SolutionResponsiblesTable[0].RECORD_DATE;
            CodSolutionResponsible = SolutionResponsiblesTable[0].COD_SOLUTION_RESPONSIBLE;
            if (SolutionResponsiblesTable[0].IsIN_COUNTRY_PKNull())
                IdCountryR = -1;
            else
                IdCountryR = SolutionResponsiblesTable[0].IN_COUNTRY_PK;



            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un Responsable de Solución
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">informacion del responsable de solución</param>
    public void InsertSolutionResponsible(SolutionResponsible info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter IN_SOLUTION_RESPONSIBLESTableAdapter = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter();
            IN_SOLUTION_RESPONSIBLESTableAdapter.InsertSolutionResponsible(info.Name, info.Email, info.Area, info.Status, info.IdCountryR);

            this.Messages.Message = "Responsable de solución ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza la información de un 
    /// Responsable de solución
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">información modificada del responsable</param>
    /// <param name="codsolutionResponsible">código del responsable</param>
    public void UpdateAffectedClient(SolutionResponsible info, Decimal codSolutionResponsible)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter IN_SOLUTION_RESPONSIBLESTableAdapter = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter();
            IN_SOLUTION_RESPONSIBLESTableAdapter.UpdateSolutionResponsible(info.Name, info.Email, info.Area, info.Status, info.IdCountryR, codSolutionResponsible);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la cantidad de 
    /// incidencias no concluidas para este Responsable de Solucion
    /// </summary>
    /// <param name="codResponsible">Codigo del Responsable</param>
    /// <returns>cantidad de registros de incidencias</returns>
    public Int32 GetUnfinishedIncidents(Decimal codResponsible)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter taResponsibles = new dsIncidentNotificationTableAdapters.IN_SOLUTION_RESPONSIBLESTableAdapter();
            counter = Convert.ToInt32(taResponsibles.UnfinishedIncidences(codResponsible));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }

    #endregion
}

#endregion

#region SEGMENTOS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// Segmentos
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:18.Oct.2011
/// </summary>
public class Segment
{
    #region CONSTRUCTOR DE LA CLASE

    public Segment()
    {
        messages = new GlobalStructure();
        dtSegments = new dsIncidentNotification.IN_SEGMENTSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string name;
    private string description;
    private string status;
    private decimal codSegment;
    private dsIncidentNotification.IN_SEGMENTSDataTable dtSegments;
    private string countries;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Nombre del Segmento
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    ///  Descripción del Segmento
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Estado del Segmento
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    /// <summary>
    /// Id del Segmento
    /// </summary>
    public decimal CodSegment
    {
        get { return codSegment; }
        set { codSegment = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// segmentos
    /// </summary>
    public dsIncidentNotification.IN_SEGMENTSDataTable SegmentsTable
    {
        get { return dtSegments; }
        set { dtSegments = value; }
    }

    /// <summary>
    /// Países
    /// Lista de países, separados por caracter "|" 
    /// </summary>
    public string Countries
    {
        get { return countries; }
        set { countries = value; }
    }



    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Segmentos
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetSegments(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            IN_SEGMENTSTableAdapter.Fill(SegmentsTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene Todo el listado de Segmentos
    /// Según id de país
    /// Fecha : 09/07/2014
    /// Autor : XOLO 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetSegmentsByCountry(Decimal idCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            IN_SEGMENTSTableAdapter.FillByCountry(SegmentsTable, idCountry);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene el listado de Segmentos
    /// Según id de país, en Estado Activo en caso que esté asociado en la tabla
    /// IN_COUNTRY_SEGMENT
    /// Fecha : 09/07/2014
    /// Autor : XOLO 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetSegmentsByCountryAct(Decimal idCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            IN_SEGMENTSTableAdapter.FillByCountryAct(SegmentsTable, idCountry);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }




    /// <summary>
    /// Obtiene la información de un Segmento
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="CodSegment">código del segmento</param>
    public void EditSegment(Decimal codSegment)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            IN_SEGMENTSTableAdapter.FillByCodSegment(SegmentsTable, codSegment);

            Name = SegmentsTable[0].SEGMENT_NAME;
            Description = SegmentsTable[0].DESCRIPTION;
            Status = SegmentsTable[0].RECORD_STATUS;
            CodSegment = SegmentsTable[0].COD_SEGMENT;

            //Obteniendo los países que tienen en común el segmento
            dsIncidentNotificationTableAdapters.SEGMENT_COUNTRYTableAdapter taCountr = new dsIncidentNotificationTableAdapters.SEGMENT_COUNTRYTableAdapter();
            dsIncidentNotification.SEGMENT_COUNTRYDataTable dtCountr = new dsIncidentNotification.SEGMENT_COUNTRYDataTable();
            taCountr.Fill(dtCountr, codSegment);
            if (dtCountr.Rows.Count > 0)
            {
                int counter = 0;
                foreach (DataRow dr in dtCountr.Rows)
                {
                    counter += 1;
                    if (counter == 1)
                        Countries = dr["IN_COUNTRY_PK"].ToString();
                    else
                        Countries = Countries + "|" + dr["IN_COUNTRY_PK"].ToString();
                }

            }


            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un Segmento
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">informacion del del segmento</param>
    public void InsertSegment(Segment info)
    {
        try
        {

            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();

            decimal codSegm = 0;
            codSegm = (decimal)IN_SEGMENTSTableAdapter.GetSeq();

            IN_SEGMENTSTableAdapter.InsertSegment(codSegm, info.Name, info.Description, info.Status);
            InsertSegmCountry(codSegm, info.countries);

            this.Messages.Message = "Segmento ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Insertando en tabla intermedia de Segmentos y Países
    /// Tabla IN_COUNTRY_SEGMENT
    /// </summary>
    /// <param name="codSegm">Código del Segmento</param>
    /// <param name="codCountries">Cadena de Países</param>
    public void InsertSegmCountry(decimal codSegm, string codCountries)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter taCountrySegm = new dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter();

            string[] listCod = codCountries.Split('|');

            foreach (var item in listCod)
            {
                taCountrySegm.InsertCountrySegm(decimal.Parse(item), codSegm, 1);
                this.Messages.Message = string.Format("Segmento {0} y País {1} ingresado", codSegm, item);
                this.Messages.Status = 1;
            }


        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());

        }
    }


    /// <summary>
    /// Actualiza la información de un 
    /// Segmento
    /// Fecha : 18.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">información modificada del segmento</param>
    /// <param name="CodSegment">código del segmento</param>
    public void UpdateSegment(Segment info, Decimal codSegment)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter IN_SEGMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            IN_SEGMENTSTableAdapter.UpdateSegment(info.Name, info.Description, info.Status, codSegment);
            updateCountriesSeg(codSegment, info.countries);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }


        //Cambiar los estados de los tipos asociados en caso de que el segmento haya sido deshabilitado
        if (info.Status == "0")
        {
            try
            {
                dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
                IN_TYPESTableAdapter.ChangeTypesStatus("0", codSegment);

                this.Messages.Message = "Información actualizada con éxito";
                this.Messages.Status = 1;
            }
            catch (Exception ex)
            {
                this.Messages.Message = ex.Message.ToString();
                this.Messages.Status = 0;
                SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            }
        }
    }

    /// <summary>
    /// Método para Actualización de países en segmentos
    /// </summary>
    /// <param name="codSegment">Código del Segmento</param>
    /// <param name="countries">Identificador de Países elegidos</param>
    public void updateCountriesSeg(decimal codSegment, string countries)
    {
        try
        {

            dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter taCountr = new dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter();
            dsIncidentNotification.IN_COUNTRY_SEGMENTDataTable dtCountr = new dsIncidentNotification.IN_COUNTRY_SEGMENTDataTable();

            string[] countryIds = countries.Split('|');

            int res = 0;
            //Consulta para actualizar el estado de los registros, según los países no seleccionados
            string sqlExec = "UPDATE IN_COUNTRY_SEGMENT SET RECORD_STATUS = 0 " +
                            "WHERE (COD_SEGMENT = " + codSegment + ") AND IN_COUNTRY_PK NOT IN (" + countries.Replace('|', ',') + ") ";

            res = ConfigurationTool.ExecQuery(sqlExec); //Ejecutar la consulta

            //Revisando los países seleccionados, en caso que no exista registro en la tabla IN_COUNTRY_SEGMENT
            //Se inserta el nuevo registro. Si existe, se verifica que tenga estado en 1.
            foreach (var item in countryIds)
            {
                taCountr.FillByCountrySegm(dtCountr, decimal.Parse(item.ToString()), codSegment);
                int counter = 0;
                decimal idRec = 0;
                if (dtCountr.Rows.Count > 0)
                {
                    idRec = decimal.Parse(dtCountr.Rows[counter]["IN_COUNTRY_SEGM_PK"].ToString());
                    taCountr.UpdateStatus(1, idRec);
                    counter += 1;

                }
                else
                {
                    taCountr.InsertCountrySegm(decimal.Parse(item), codSegment, 1);
                }

            }



        }
        catch
        { }

    }



    /// <summary>
    /// Obtiene la cantidad de 
    /// incidencias no concluidas para este segmento
    /// </summary>
    /// <param name="codSegment">Codigo del segmento</param>
    /// <returns>cantidad de registros de incidencias</returns>
    public Int32 GetUnfinishedIncidents(Decimal codSegment)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter taSegments = new dsIncidentNotificationTableAdapters.IN_SEGMENTSTableAdapter();
            counter = Convert.ToInt32(taSegments.UnfinishedIncidences(codSegment));

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }

    #endregion
}

#endregion

#region TIPOS

/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los Tipos, Segundo Nivel de Segmentos
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:19.Oct.2011
/// </summary>
public class Type
{
    #region CONSTRUCTOR DE LA CLASE

    public Type()
    {
        messages = new GlobalStructure();
        dtTypes = new dsIncidentNotification.IN_TYPESDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string name;
    private string description;
    private string status;
    private decimal codType;
    private decimal codSegment;
    private dsIncidentNotification.IN_TYPESDataTable dtTypes;
    private decimal idContry;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Nombre del Tipo
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    ///  Descripción del Tipo
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Estado del Tipo
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    /// <summary>
    /// Id del Tipo
    /// </summary>
    public decimal CodType
    {
        get { return codType; }
        set { codType = value; }
    }

    /// <summary>
    /// Id del Segmento al cual
    /// esta asignado el tipo
    /// </summary>
    public decimal CodSegment
    {
        get { return codSegment; }
        set { codSegment = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// responsables de solución
    /// </summary>
    public dsIncidentNotification.IN_TYPESDataTable TypesTable
    {
        get { return dtTypes; }
        set { dtTypes = value; }
    }

    /// <summary>
    /// Id del País al cual
    /// esta asignado el tipo
    /// </summary>
    public decimal IdCountry
    {
        get { return idContry; }
        set { idContry = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Tipos
    /// asociados a un Segmento
    /// Fecha : 19.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="CodSegment">código del segmento que contiene a los tipos</param>
    public void GetTypes(string filter, decimal codSegment)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.Fill(TypesTable, codSegment, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene el listado de Tipos
    /// asociados a un Segmento y país determinado
    /// Fecha : 04/07/2014
    /// XOLO 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="CodSegment">código del segmento que contiene a los tipos</param>
    public void GetTypesByCountry(string filter, decimal codSegment, decimal codCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.FillByCountry(TypesTable, codSegment, codCountry, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la información de un Tipo
    /// Fecha : 19.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="codType">código del tipo</param>
    public void EditType(Decimal codType)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.FillByCodType(TypesTable, codType);

            Name = TypesTable[0].TYPE_NAME;
            Description = TypesTable[0].DESCRIPTION;
            Status = TypesTable[0].RECORD_STATUS;
            CodType = TypesTable[0].COD_TYPE;
            CodSegment = TypesTable[0].COD_SEGMENT;

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un Tipo a un 
    /// segmento
    /// Fecha : 19.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">informacion del tipo</param>
    public void InsertType(Type info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.InsertType(info.CodSegment, info.Name, info.Description, info.Status, info.IdCountry);

            this.Messages.Message = "Tipo ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza la información de un 
    /// tipo
    /// Fecha : 19.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">información modificada del tipo</param>
    /// <param name="codType">código del tipo a modificar</param>
    public void UpdateType(Type info, Decimal codType)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.UpdateType(info.Name, info.Description, info.Status, codType);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Actualiza el estado de un tipo asociado 
    /// a un Segmento
    /// Fecha : 19.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// <remarks>Ej: Si el segmento fue cambiado a inactivo, los tipos asociados 
    /// a él también pasarán a inactivos
    /// </remarks>
    /// <param name="recordStatus">Estado al que serán cambiados Activo = 1, Inactivo = 0</param>
    /// <param name="CodSegment">Id del Segmento de los tipos</param>
    /// </summary>
    public void ChangeTypeStatus(string recordStatus, decimal codSegment)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.ChangeTypesStatus(recordStatus, codSegment);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene una lista con los tipos asociados a un segmento, 
    /// que posean al menos un nivel asignado
    /// </summary>
    /// <param name="codSegment">código del segmento</param>
    /// <returns></returns>
    public DataTable GetAssignedLevelsTypes(Decimal codSegment)
    {
        DataTable dtReturn;
        dsIncidentNotification.IN_TYPESDataTable dtType = new dsIncidentNotification.IN_TYPESDataTable();
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            //IN_TYPESTableAdapter.GetLevelAssignedTypes(dtType, codSegment);

            //IN_TYPESTableAdapter.GetLevelAssignedTypesCountr(dtType, codSegment);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        dtReturn = (DataTable)dtType;

        return dtReturn;
    }

    /// <summary>
    /// Obtiene una lista con los tipos asociados a un segmento, 
    /// que posean al menos un nivel asignado
    /// </summary>
    /// <param name="codSegment">código del segmento</param>
    /// <returns></returns>
    public DataTable GetAssignedLevelsTypesCountr(Decimal codSegment, Decimal CountryId)
    {
        DataTable dtReturn;
        dsIncidentNotification.IN_TYPESDataTable dtType = new dsIncidentNotification.IN_TYPESDataTable();
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter IN_TYPESTableAdapter = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            IN_TYPESTableAdapter.GetLevelAssignedTypesCountr(dtType, codSegment, CountryId);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        dtReturn = (DataTable)dtType;

        return dtReturn;
    }



    /// <summary>
    /// Obtiene la cantidad de 
    /// incidencias no concluidas para este tipo
    /// </summary>
    /// <param name="codSegment">Codigo del tipo</param>
    /// <returns>cantidad de registros de incidencias</returns>
    public Int32 GetUnfinishedIncidents(Decimal codSegment)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter taTypes = new dsIncidentNotificationTableAdapters.IN_TYPESTableAdapter();
            counter = Convert.ToInt32(taTypes.UnfinishedIncidences(codSegment));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }
    #endregion
}


#endregion

#region NIVELES

/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los Niveles, Tercer Nivel de Segmentos
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:20.Oct.2011
/// </summary>
public class Level
{
    #region CONSTRUCTOR DE LA CLASE

    public Level()
    {
        messages = new GlobalStructure();
        dtLevels = new dsIncidentNotification.IN_LEVELSDataTable();
        dtEmails = new dsIncidentNotification.IN_LEVEL_EMAILSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string name;
    private string description;
    private string status;
    private string color;
    private decimal sequence;
    private decimal waitTime;
    private decimal codLevel;
    private decimal codType;
    private dsIncidentNotification.IN_LEVELSDataTable dtLevels;
    private dsIncidentNotification.IN_LEVEL_EMAILSDataTable dtEmails;
    private decimal codSegm;
    private decimal codCountry;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Nombre del Nivel
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    ///  Descripción del Nivel
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Estado del Nivel
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    /// <summary>
    /// Color del Nivel, en Hexadecimal
    /// </summary>
    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    /// <summary>
    /// Sequencia dentro de la lista de
    /// niveles asociados a un tipo
    /// </summary>
    public decimal Sequence
    {
        get { return sequence; }
        set { sequence = value; }
    }

    /// <summary>
    /// Tiempo de espera antes de pasar al siguiente
    /// nivel
    /// </summary>
    public decimal WaitTime
    {
        get { return waitTime; }
        set { waitTime = value; }
    }

    /// <summary>
    /// Id del Tipo
    /// </summary>
    public decimal CodType
    {
        get { return codType; }
        set { codType = value; }
    }

    /// <summary>
    /// Id del Segmento al cual
    /// esta asignado el tipo
    /// </summary>
    public decimal CodLevel
    {
        get { return codLevel; }
        set { codLevel = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// responsables de solución
    /// </summary>
    public dsIncidentNotification.IN_LEVELSDataTable LevelsTable
    {
        get { return dtLevels; }
        set { dtLevels = value; }
    }

    /// <summary>
    /// Tabla que contiene los 
    /// emails asociados al nivel
    /// </summary>
    public dsIncidentNotification.IN_LEVEL_EMAILSDataTable LevelEmailsTable
    {
        get { return dtEmails; }
        set { dtEmails = value; }
    }


    /// <summary>
    /// Id del Segmento al cual
    /// se le asignará el nivel
    /// </summary>
    public decimal CodSegm
    {
        get { return codSegm; }
        set { codSegm = value; }
    }

    /// <summary>
    /// Id del Segmento al cual
    /// se le asignará el nivel
    /// </summary>
    public decimal CodCountry
    {
        get { return codCountry; }
        set { codCountry = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Niveles
    /// asociados a un Tipo
    /// Fecha : 20.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="codType">código del tipo que contiene a los niveles</param>
    public void GetLevels(string filter, decimal codSegment, decimal idCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            IN_LEVELSTableAdatper.Fill(LevelsTable, codSegment, filter);
            //dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            //IN_LEVELSTableAdatper.FillBySegmentCountr(LevelsTable, codSegment, idCountry, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene el registro de nivel
    /// según el id
    /// Fecha : 13.Mar.2015
    /// Autor : Xolo 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="codType">código del tipo que contiene a los niveles</param>
    public void GetLevelRec(decimal codLevel)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            IN_LEVELSTableAdatper.FillByCodLevel(LevelsTable, codLevel);

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene el registro de nivel
    /// según el id
    /// Fecha : 13.Mar.2015
    /// Autor : Xolo 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="codType">código del tipo que contiene a los niveles</param>
    public void GetLevelsType(decimal codType)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            IN_LEVELSTableAdatper.Fill(LevelsTable, codType, string.Empty);

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene el registro de nivel
    /// según el id
    /// Fecha : 13.Mar.2015
    /// Autor : Xolo 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="codType">código del tipo que contiene a los niveles</param>
    public decimal GetCodLevelType(decimal codType)
    {
        try
        {
            decimal codLevelResult = 0;
            string query = string.Format(" SELECT MAX(COD_LEVEL) " +
                                         " FROM IN_LEVELS WHERE COD_TYPE={0}", codType);
            DataTable result = new DataTable();
            result = ConfigurationTool.GetQueryResult(query);
            if (result.Rows.Count > 0)
            {
                codLevelResult = decimal.Parse(result.Rows[0]["COD_LEVEL"].ToString());
            }
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
            return codLevelResult;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return 0;
        }
    }


    /// <summary>
    /// Obtiene el identificador de registro de nivel
    /// según el nivel y el 
    /// Fecha : 13.Mar.2015
    /// Autor : Xolo 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="codType">código del tipo que contiene a los niveles</param>
    public decimal GetCodLevelTypeLevel(decimal codType, decimal levelSeq)
    {
        try
        {
            decimal codLevelResult = 0;
            string query = string.Format(" SELECT COD_LEVEL FROM IN_LEVELS WHERE COD_TYPE={0} AND LEVEL_SEQUENCE={1}", codType, levelSeq);
            DataTable result = new DataTable();
            result = ConfigurationTool.GetQueryResult(query);
            if (result.Rows.Count > 0)
            {
                codLevelResult = decimal.Parse(result.Rows[0]["COD_LEVEL"].ToString());
            }
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
            return codLevelResult;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return 0;
        }
    }



    /// <summary>
    /// Obtiene el listado de los países según segmento seleccionado
    /// </summary>
    /// <param name="codSegment">Código del Segmento</param>
    public DataTable GetCountriesBySegment(decimal codSegment)
    {
        try
        {
            dsIncidentNotificationTableAdapters.SEGMENT_COUNTRYTableAdapter taSegmentCountr = new dsIncidentNotificationTableAdapters.SEGMENT_COUNTRYTableAdapter();
            dsIncidentNotification.SEGMENT_COUNTRYDataTable dtSegmentCountr = new dsIncidentNotification.SEGMENT_COUNTRYDataTable();
            taSegmentCountr.FillByStatusA(dtSegmentCountr, codSegment);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
            return dtSegmentCountr;

        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return null;

        }
    }
    /// <summary>
    /// Obtiene el siguiente numero en
    /// la sequencia de los niveles
    /// </summary>
    /// <param name="codType">Tipo que contiene a los niveles</param>
    /// <returns>El valor siguiente en la secuencia</returns>
    public Decimal GetSequence(Decimal codType, Decimal idCountry)
    {
        dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
        return (Decimal)IN_LEVELSTableAdatper.GetSequence(codType);
        //return (Decimal)IN_LEVELSTableAdatper.GetSeqLevel(codType, idCountry);
    }

    /// <summary>
    /// Obtiene la información de un Nivel
    /// Fecha : 20.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="codLevel">código del nivel</param>
    public void EditLevel(Decimal codLevel)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            IN_LEVELSTableAdatper.FillByCodLevel(LevelsTable, codLevel);

            Name = LevelsTable[0].LEVEL_NAME;
            Description = LevelsTable[0].DESCRIPTION;
            Status = LevelsTable[0].RECORD_STATUS;
            Color = LevelsTable[0].LEVEL_COLOR;
            WaitTime = LevelsTable[0].WAIT_TIME;
            Sequence = LevelsTable[0].LEVEL_SEQUENCE;
            CodLevel = LevelsTable[0].COD_LEVEL;
            CodType = LevelsTable[0].COD_TYPE;
            new dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter().Fill(LevelEmailsTable, codLevel);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un Nivel a un 
    /// tipo
    /// Fecha : 20.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">informacion del nivel</param>
    public Decimal InsertLevel(Level info)
    {
        Decimal CodLevel = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            //IN_LEVELSTableAdatper.InsertLevel(info.CodType, info.Name, info.Description, info.WaitTime, info.Color, out CodLevel, info.Status,info.CodCountry );
            //IN_LEVELSTableAdatper.InsertLevel(info.CodType, info.Name, info.Description, info.WaitTime, info.Color, out CodLevel, info.Status,info.CodSegm,info.CodCountry);

            string query = string.Format("SELECT IN_OPERATIONS_API.INSERT_LEVEL_FUN({0},'{1}','{2}',{3},'{4}','{5}') RESP FROM DUAL", info.CodType, info.Name, info.Description, info.waitTime, info.Color, info.Status);
            DataTable result = new DataTable();
            result = ConfigurationTool.GetQueryResult(query);

            int counter = 0;
            decimal idRec = 0;
            //Obteniendo el resultado
            if (result.Rows.Count > 0)
            {
                idRec = decimal.Parse(result.Rows[counter]["RESP"].ToString());
                if (idRec != -1)
                {
                    CodLevel = idRec;
                    this.Messages.Message = "Nivel ingresado con éxito";
                    this.Messages.Status = 1;
                }
                else
                {
                    this.Messages.Message = "Ocurrión un error en la operación IN_OPERATIONS_API.INSERT_LEVEL_FUN";
                    this.Messages.Status = 0;
                    CodLevel = 0;
                }
            }
            else
            {
                this.Messages.Message = "Ocurrión un error en la operación IN_OPERATIONS_API.INSERT_LEVEL_FUN";
                this.Messages.Status = 0;
                CodLevel = 0;
            }



        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message;
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            CodLevel = 0;
        }
        return CodLevel;
    }


    /// <summary>
    /// Actualiza la información de un 
    /// nivel
    /// Fecha : 20.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="info">información modificada del nivel</param>
    /// <param name="codLevel">código del nivel a modificar</param>
    public void UpdateLevel(Level info, Decimal codLevel)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper IN_LEVELSTableAdatper = new dsIncidentNotificationTableAdapters.IN_LEVELSTableAdatper();
            IN_LEVELSTableAdatper.UpdateLevel(info.WaitTime, info.Color, codLevel, info.status);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = this.Messages.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la informacion del menor nivel 
    /// de un tipo
    /// </summary>
    /// <param name="codType">codigo del tipo seleccionado</param>
    public DataTable getLessSequence(Decimal codType)
    {
        string query = "SELECT COD_LEVEL, LEVEL_NAME " +
                        "FROM IN_LEVELS " +
                        "WHERE LEVEL_SEQUENCE =  (SELECT DECODE(MIN(LEVEL_SEQUENCE),NULL,-1,MIN(LEVEL_SEQUENCE)) FROM IN_LEVELS)" +
                        "AND COD_TYPE = " + codType + " ";
        DataTable result = new DataTable();

        try
        {
            result = ConfigurationTool.GetQueryResult(query);

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = this.Messages.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        return result;
    }


    #endregion
}


#endregion

#region CORREOS DE UN NIVEL

/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los Correos asociados a un nivel
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:24.Oct.2011
/// </summary>
public class Email
{
    #region CONSTRUCTOR DE LA CLASE

    public Email()
    {
        messages = new GlobalStructure();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string email;
    private decimal codLevel;


    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Id del Segmento al cual
    /// esta asignado el tipo
    /// </summary>
    public decimal CodLevel
    {
        get { return codLevel; }
        set { codLevel = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Ingresa un Correo asociado a un nivel
    /// Fecha : 24.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    ///<param name="codLevel">Codigo del Nivel al cual esta asociado el correo</param>
    ///<param name="email">correo a ingresar</param>
    public void InsertEmail(string email, Decimal codLevel)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter taEmails = new dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter();
            taEmails.InsertLevelEmail(codLevel, email);

            this.Messages.Message = "Correo ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message;
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza un Correo asociado a un nivel
    /// Fecha : 24.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    ///<param name="codEmail">Codigo del correo</param>
    ///<param name="email">nuevo valor del correo</param>
    public void UpdateEmail(string email, Decimal codEmail)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter taEmails = new dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter();
            taEmails.UpdateLevelEmail(email, codEmail);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = this.Messages.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Borra un Correo de la lista
    /// Fecha : 24.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    ///<param name="codEmail">Codigo del correo</param>
    public void DeleteEmail(Decimal codEmail)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter taEmails = new dsIncidentNotificationTableAdapters.IN_LEVEL_EMAILSTableAdapter();
            taEmails.DeleteLevelEmail(codEmail);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = this.Messages.Message.Contains("ORA-00001") ? "Dato duplicado, verifique y reintente" : ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }
    #endregion
}


#endregion

#region MOTIVOS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// las Incidencias
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:28.Oct.2011
/// </summary>
public class Motive
{
    #region CONSTRUCTOR DE LA CLASE

    public Motive()
    {
        messages = new GlobalStructure();
        dtMotives = new dsIncidentNotification.IN_CAT_MOTIVESDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private Decimal codMotive;
    private String motiveName;
    private dsIncidentNotification.IN_CAT_MOTIVESDataTable dtMotives;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Codigo del tipo de 
    /// motivo, ya sea INICIO, SEGUIMIENTO, FIN
    /// </summary>
    public Decimal CodMotive
    {
        get { return codMotive; }
        set { codMotive = value; }
    }


    /// <summary>
    /// Nombre del Motivo
    /// </summary>
    public String MotiveName
    {
        get { return motiveName; }
        set { motiveName = value; }
    }


    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// motivos
    /// </summary>
    public dsIncidentNotification.IN_CAT_MOTIVESDataTable MotivesTable
    {
        get { return dtMotives; }
        set { dtMotives = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Motivos
    /// Fecha : 31.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    public void GetMotives()
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter taCatMotives = new dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter();
            taCatMotives.Fill(MotivesTable);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }
    #endregion
}

#endregion

#region SERVICIOS AFECTADOS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los servicios afectados asociados a una incidencia
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:01.Nov.2011
/// </summary>
public class AffectedService
{
    #region CONSTRUCTOR DE LA CLASE

    public AffectedService()
    {
        messages = new GlobalStructure();
        dtAffectedServices = new dsIncidentNotification.IN_AFFECTED_SERVICESDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private Decimal codAffectedService;
    private String name;
    private Decimal receivedCalls;
    private Decimal codIncidence;
    private dsIncidentNotification.IN_AFFECTED_SERVICESDataTable dtAffectedServices;

    #endregion

    #region PROPIEDADES


    /// <summary>
    /// Codigo del Servicio Afectado
    /// </summary>
    public Decimal CodAffectedService
    {
        get { return codAffectedService; }
        set { codAffectedService = value; }
    }

    /// <summary>
    /// Codigo de la incidencia
    /// a la que esta asociada el servicio
    /// </summary>
    public Decimal CodIncidence
    {
        get { return codIncidence; }
        set { codIncidence = value; }
    }

    /// <summary>
    /// Nombre del Servicio
    /// </summary>
    public String Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Llamadas recibidas
    /// </summary>
    public Decimal ReceivedCalls
    {
        get { return receivedCalls; }
        set { receivedCalls = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// segmentos
    /// </summary>
    public dsIncidentNotification.IN_AFFECTED_SERVICESDataTable ServicesTable
    {
        get { return dtAffectedServices; }
        set { dtAffectedServices = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Servicios Afectados 
    /// para una incidencia
    /// Fecha : 01.Nov.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="codIncidence">Codigo de la incidencia seleccionada</param>
    public void GetAffectedServices(Decimal codIncidence)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_AFFECTED_SERVICESTableAdapter taAffectedServices = new dsIncidentNotificationTableAdapters.IN_AFFECTED_SERVICESTableAdapter();
            taAffectedServices.Fill(ServicesTable, codIncidence);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    #endregion
}

#endregion

#region INCIDENCIAS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// las Incidencias
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:28.Oct.2011
/// </summary>
public class Incidence
{
    #region CONSTRUCTOR DE LA CLASE

    public Incidence()
    {
        messages = new GlobalStructure();
        dtIncidences = new dsIncidentNotification.IN_INCIDENCESDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private Decimal codIncidence;
    private Decimal codSegment;
    private Decimal codType;
    private Decimal codLevel;
    private Decimal codMotive;
    private String description;
    private String monitoring;
    private String incidenceCause;
    private DateTime startDate;
    private DateTime? endDate;
    private String script;
    private String typology;
    private dsIncidentNotification.IN_INCIDENCESDataTable dtIncidences;
    private Decimal idCountry;
    private Decimal critical;
    private String _Folio_OT;
    private String subject;
    private String maintenance;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Codigo del tipo de 
    /// motivo, ya sea INICIO, SEGUIMIENTO, FIN
    /// </summary>
    public Decimal CodMotive
    {
        get { return codMotive; }
        set { codMotive = value; }
    }

    /// <summary>
    /// Codigo del nivel asociado 
    /// al tipo
    /// </summary>
    public Decimal CodLevel
    {
        get { return codLevel; }
        set { codLevel = value; }
    }

    /// <summary>
    /// Codigo del tipo asociado al 
    /// segmento de la incidencia
    /// </summary>
    public Decimal CodType
    {
        get { return codType; }
        set { codType = value; }
    }

    /// <summary>
    /// Codigo del Segmento en el cual esta la
    /// incidencia
    /// </summary>
    public Decimal CodSegment
    {
        get { return codSegment; }
        set { codSegment = value; }
    }

    /// <summary>
    /// Codigo de la incidencia
    /// </summary>
    public Decimal CodIncidence
    {
        get { return codIncidence; }
        set { codIncidence = value; }
    }

    /// <summary>
    /// Causa de la incidencia
    /// </summary>
    public String IncidenceCause
    {
        get { return incidenceCause; }
        set { incidenceCause = value; }
    }


    /// <summary>
    /// Monitoreo
    /// </summary>
    public String Monitoring
    {
        get { return monitoring; }
        set { monitoring = value; }
    }

    /// <summary>
    /// Descripcion de la incidencia
    /// </summary>
    public String Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Fecha de inicio de la Incidencia
    /// </summary>
    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    /// <summary>
    /// Fecha de fin de la incidencia
    /// </summary>
    public DateTime? EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }

    /// <summary>
    /// Texto que se muestra al cliente 
    /// </summary>
    public String Script
    {
        get { return script; }
        set { script = value; }
    }

    /// <summary>
    /// Tipologia con la que se uincio el caso 
    /// </summary>
    public String Typology
    {
        get { return typology; }
        set { typology = value; }
    }

    /// <summary>
    /// Asunto de la incidencia
    /// </summary>
    public String Subject
    {
        get { return subject; }
        set { subject = value; }
    }

    /// <summary>
    /// Si es incidencia de mantenimiento
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public String Maintenance
    {
        get { return maintenance; }
        set { maintenance = value; }
    }


    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// segmentos
    /// </summary>
    public dsIncidentNotification.IN_INCIDENCESDataTable IncidencesTable
    {
        get { return dtIncidences; }
        set { dtIncidences = value; }
    }


    /// <summary>
    /// Identificador del País seleccionado
    /// </summary>
    public Decimal IdCountry
    {
        get { return idCountry; }
        set { idCountry = value; }
    }

    /// <summary>
    /// Identificador del País seleccionado
    /// </summary>
    public Decimal Critical
    {
        get { return critical; }
        set { critical = value; }
    }

    /// <summary>
    /// Numero de Folio/OT
    /// </summary>
    public String Folio_OT
    {
        get { return _Folio_OT; }
        set { _Folio_OT = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Incidencias
    /// Fecha : 28.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    /// <param name="IN_COUNTRY_PK">Pk del Pais</param>
    public void GetIncidences(string filter, Int32 IN_COUNTRY_PK)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_INCIDENCESTableAdapter taIncidences = new dsIncidentNotificationTableAdapters.IN_INCIDENCESTableAdapter();
            taIncidences.Fill(IncidencesTable, filter, IN_COUNTRY_PK, SafetyPad.GetParameterXSA_CUENTA(1));           
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la información de una Incidencia 
    /// Fecha : 28.Oct.2011
    /// Autor : Manuel Gutiérrez Rojas
    /// </summary>
    /// <param name="codIncidence">código de la incidencia</param>
    public void EditIncidence(Decimal codIncidence)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_INCIDENCESTableAdapter taIncidences = new dsIncidentNotificationTableAdapters.IN_INCIDENCESTableAdapter();
            taIncidences.FillByCodIncidence(IncidencesTable, codIncidence, SafetyPad.GetParameterXSA_CUENTA(1));      
           
            CodSegment = IncidencesTable[0].COD_SEGMENT;
            CodType = IncidencesTable[0].COD_TYPE;
            CodLevel = IncidencesTable[0].COD_LEVEL;
            CodMotive = IncidencesTable[0].COD_MOTIVE;
            Description = IncidencesTable[0].DESCRIPTION;
            IncidenceCause = IncidencesTable[0].INCIDENCE_CAUSE;            
            Monitoring = (IncidencesTable.Rows[0]["MONITORING"].ToString()); //== null) ? string.Empty : IncidencesTable[0].MONITORING;
            Script = IncidencesTable[0].SCRIPT;
            Typology = IncidencesTable[0].TYPOLOGY;

            if (IncidencesTable[0].IsSUBJECTNull())
                Subject = string.Empty;
            else
                Subject = IncidencesTable[0].SUBJECT;

            if (IncidencesTable[0].IsMAINTENANCENull())
                Maintenance = string.Empty;
            else
                Maintenance = IncidencesTable[0].MAINTENANCE;

            if (IncidencesTable[0].IsIN_COUNTRY_PKNull())
                IdCountry = -1;
            else
                IdCountry = IncidencesTable[0].IN_COUNTRY_PK;
            //Critical = IncidencesTable[0].cr ;

            IncidencesTable[0].LEVEL_NAME = "";
            IncidencesTable[0].SEGMENT_NAME = "";
            IncidencesTable[0].TYPE_NAME = "";
            IncidencesTable[0].MOTIVE_NAME = "";

            //Campo Folio OT
            if (IncidencesTable[0].IsFOLIO_OTNull())
                Folio_OT = string.Empty;
            else
                Folio_OT = IncidencesTable[0].FOLIO_OT;



            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }
    
    /// <summary>
    /// Obtiene el estado de la relación país-segmento
    /// </summary>
    /// <param name="idCountr">Identificador del País</param>
    /// <param name="idSegmt">Identificador del Segmento</param>
    /// <returns></returns>
    public decimal GetStatusCountrySegm(decimal idCountr, decimal idSegmt)
    {
        decimal statusValue = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter taCounSeg = new dsIncidentNotificationTableAdapters.IN_COUNTRY_SEGMENTTableAdapter();
            statusValue = (decimal)taCounSeg.GetStatus(idSegmt, idCountr);
        }
        catch
        {
            statusValue = 0;
        }
        return statusValue;
    }

    /// <summary>
    /// Obtiene el nivel de criticidad de una incidencia
    /// </summary>
    /// <param name="codIncidence">Identificador de la incidencia</param>
    /// <returns></returns>
    public decimal GetCritical(decimal codIncidence)
    {
        try
        {
            decimal resp = 0;
            string mySentence = string.Format("SELECT CRITICALITY FROM IN_INCIDENCE_LOGS " +
                                "WHERE COD_INCIDENCE_LOG IN (SELECT MAX(COD_INCIDENCE_LOG) FROM IN_INCIDENCE_LOGS WHERE COD_INCIDENCE = {0})", codIncidence);

            DataTable result = new DataTable();

            result = ConfigurationTool.GetQueryResult(mySentence);
            if (result.Rows.Count > 0)
            {
                //dtTypes.Columns["COD_TYPE"].ColumnName;
                resp = decimal.Parse(result.Rows[0]["CRITICALITY"].ToString());//resp = decimal.Parse(result.Columns["CRITICALITY"][0].DefaultValue.ToString());  // dr[0].ToString();
            }
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
            return resp;

        }
        catch (Exception ex)
        {
            this.Messages.Message = "Inconveniente en consulta Nivel Criticidad";
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return -1;
        }

    }

    

    
    /// <summary>
    /// Inserta una incidencia
    /// Manuel Gutierrez Rojas
    /// 02.Nov.2011
    /// </summary>
    /// <param name="inc">Datos de la incidencia</param>
    /// <param name="resp">lista de responsables de solucion</param>
    /// <param name="client">lista de clientes afectados</param>
    /// <param name="service">lista de servicios afectados</param>
    /// <param name="userName">Nombre del usuario que realiza la inserción</param>
    /// <returns>estado de la inserción</returns>
    public Boolean InsertIncidence(Incidence inc, SolutionResponsible[] resp, AffectedClient[] client, AffectedService[] service, attachment[] attach, string userName)
    {
        String sql;
        int res, res2;
        Boolean result = false;
        decimal cod_incidence = 0;
        decimal cod_incidence_log = 0;
       // attachment[] attach,
        #region Creacion de la consulta


        /* XOLO S.A -  16/10/2014
         * Se agrega el campo FOLIO_OT al insert de la consulta
        sql = "DECLARE\n" +
              "        cod_incidence DECIMAL;\n" +
              "        cod_incidence_log DECIMAL;\n" +
              "        message VARCHAR2(500);\n" +
              "BEGIN\n" +
              "		SELECT COD_INCIDENCE_SEQ.NEXTVAL INTO cod_incidence FROM DUAL;\n" +
              "		INSERT INTO IN_INCIDENCES (COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",START_DATE, END_DATE, SCRIPT, TYPOLOGY,IN_COUNTRY_PK)\n" +
              String.Format("VALUES (cod_incidence, {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', TO_DATE('{7}','ddmmyyyyhh24miss') , TO_DATE({8},'ddmmyyyyhh24miss'), '{9}', '{10}',{11});",
                  inc.CodLevel, inc.CodMotive, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                  inc.Monitoring, inc.StartDate.ToString("ddMMyyyyHHmmss"), inc.EndDate.HasValue ? "'"+inc.EndDate.Value.ToString("ddMMyyyyHHmmss") +"'" : "NULL", inc.Script, inc.Typology, inc.IdCountry );
        */

        DataTable dtIncidences = new Incidence().GetIncidentSeq();

        foreach (DataRow row in dtIncidences.Rows)
        {
           cod_incidence = (decimal)row["INCIDENTSEQ"];
        }

        sql = "DECLARE\n" +
             // "        cod_incidence DECIMAL;\n" +
              "        cod_incidence_log DECIMAL;\n" +
              "        message VARCHAR2(500);\n" +
              "BEGIN\n" +
            //  "		SELECT COD_INCIDENCE_SEQ.NEXTVAL INTO cod_incidence FROM DUAL;\n" +
              "		INSERT INTO IN_INCIDENCES (COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",START_DATE, END_DATE, SCRIPT, TYPOLOGY,IN_COUNTRY_PK,FOLIO_OT,SUBJECT,MAINTENANCE)\n" +
              String.Format("VALUES ({14}, {0}, {1}, {2}, {3}, '{4}', '{5}', :Monitoring, TO_DATE('{6}','ddmmyyyyhh24miss') , TO_DATE({7},'ddmmyyyyhh24miss'), '{8}', '{9}',{10},'{11}','{12}','{13}');",
                  inc.CodLevel, inc.CodMotive, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                  inc.StartDate.ToString("ddMMyyyyHHmmss"), inc.EndDate.HasValue ? "'" + inc.EndDate.Value.ToString("ddMMyyyyHHmmss") + "'" : "NULL", inc.Script, inc.Typology, inc.IdCountry, inc.Folio_OT, inc.Subject, inc.Maintenance, cod_incidence);
       
        foreach (AffectedClient c in client)
        {
            if (c != null)
            {
                if (c.CodAffectedClient != 0)
                {
                    sql += "INSERT INTO IN_INC_AFFECTED_CLIENTS (COD_AFFECTED_CLIENT, COD_INCIDENCE)\n" +
                    String.Format("VALUES ({0}, {1});\n", c.CodAffectedClient, cod_incidence);
                }
            }
        }

        foreach (SolutionResponsible r in resp)
        {
            if (r != null)
            {
                if (r.CodSolutionResponsible != 0)
                {
                    sql += "INSERT INTO IN_INC_SOLUTION_RESPONSIBLES (COD_SOLUTION_RESPONSIBLE, COD_INCIDENCE)\n" +
                    String.Format("VALUES ({0}, {1});\n", r.CodSolutionResponsible, cod_incidence);
                }
            }
        }      
       

        Decimal receivedCalls = 0;
        foreach (AffectedService s in service)
        {
            if (s != null)
            {
                if (s.Name != string.Empty)
                {
                    sql += "INSERT INTO IN_AFFECTED_SERVICES (COD_INCIDENCE, RECEIVED_CALLS,RECORD_DATE,AFFECTED_SERVICE)\n" +
                    String.Format("VALUES ({0}, {1}, sysdate, '{2}');\n", cod_incidence, s.ReceivedCalls, s.Name);
                    receivedCalls += s.ReceivedCalls;
                }
            }
        }

        for (int i = 1; i <= inc.codMotive; i++) //Ingresar en la tabla de LOGS dependiendo del motivo ingresado
        {
            /*XOLO S.A. 16/10/2014 
             * Se ingresa valor del campo FOLIO_OT al insert
             */
            sql += "\nSELECT COD_INCIDENCE_LOG_SEQ.NEXTVAL INTO cod_incidence_log FROM DUAL;\n" +
             "\nINSERT INTO IN_INCIDENCE_LOGS (COD_INCIDENCE_LOG,COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",LOG_DATE, USERNAME, IS_LEVEL_CHANGE_LOG,RECEIVED_CALLS,SCRIPT,TYPOLOGY,CRITICALITY,IN_COUNTRY_PK, FOLIO_OT,SUBJECT,MAINTENANCE)\n" +
                  String.Format("VALUES (cod_incidence_log,{15},{0},{1},{2},{3},'{4}','{5}',:Monitoring,sysdate,'{6}','S',{7},'{8}','{9}',{10},{11},'{12}','{13}','{14}');",
                      inc.CodLevel, i, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                       userName, receivedCalls, inc.Script, inc.Typology, inc.Critical, inc.IdCountry, inc.Folio_OT,inc.Subject, inc.Maintenance,cod_incidence);
        }
        // sql += "COMMIT;\nSELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(cod_incidence_log) INTO message FROM DUAL; ";
        sql += "COMMIT;\n ";



        sql += "END;";

        #endregion

        try
        {
            res = ConfigurationTool.ExecQueryParam(sql,inc.Monitoring);
             
            foreach (attachment at in attach)
            {

                if (at != null)
                {
                    if (at.DocName != "")
                    {
                        attachment actAttach = new attachment();

                        actAttach.DocName = at.DocName;
                        actAttach.Docbyte = at.Docbyte;
                        actAttach.CodIncidence = cod_incidence;
                        actAttach.DocExt = at.DocExt;
                        actAttach.DocMime = at.DocMime;
                        actAttach.InsertAttach(actAttach);
                    }
                }
            }

            DataTable dtIncidencesLog = new Incidence().GetCodIncidentLog(cod_incidence);

            foreach (DataRow row in dtIncidencesLog.Rows)
            {
                cod_incidence_log = (decimal)row["COD_INCIDENCE_LOG"];
            }

            string sqlemail = "DECLARE\n" +
                "        cod_incidence_log DECIMAL;\n" +
                "        message VARCHAR2(500);\n" +
                "BEGIN\n" +
                "         SELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(" + cod_incidence_log + ") INTO message FROM DUAL;";
            sqlemail += "END;";
            res2 = ConfigurationTool.ExecQuery(sqlemail);
            
            this.Messages.Message = "Incidencia Ingresada con éxito";
            //this.Messages.Message = sql;
            this.Messages.Status = 1;
            result = true;

        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message;
            //this.Messages.Message = sql;
            this.Messages.Status = 0;
            result = false;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            
        }

        return result;
    }

    /// <summary>
    /// Obtiene la lista de Clientes afectados 
    /// para una incidencia
    /// </summary>
    /// <param name="codIncidence">codigo de la incidencia</param>
    /// <returns></returns>
    public DataTable GetAffectedClients(Decimal codIncidence)
    {
        DataTable clients = new DataTable();
        string query = "SELECT COD_AFFECTED_CLIENT " +
                       "FROM IN_INC_AFFECTED_CLIENTS " +
                       "WHERE COD_INCIDENCE = " + codIncidence;

        clients = ConfigurationTool.GetQueryResult(query);

        return clients;
    }

    /// <summary>
    /// Obtiene la lista de Responsables de Solución
    /// para una incidencia
    /// </summary>
    /// <param name="codIncidence">codigo de la incidencia</param>
    /// <returns></returns>
    public DataTable GetSolutionResponsibles(Decimal codIncidence)
    {
        DataTable responsibles = new DataTable();
        string query = "SELECT COD_SOLUTION_RESPONSIBLE " +
                       "FROM IN_INC_SOLUTION_RESPONSIBLES " +
                       "WHERE COD_INCIDENCE = " + codIncidence;

        responsibles = ConfigurationTool.GetQueryResult(query);

        return responsibles;
    }


    

    /// <summary>
    /// Actualiza la informacion
    /// de una incidencia
    /// Manuel Gutierrez Rojas
    /// 08.Nov.2011
    /// </summary>
    /// <param name="inc">Datos de la incidencia</param>
    /// <param name="resp">lista de responsables de solucion</param>
    /// <param name="client">lista de clientes afectados</param>
    /// <param name="service">lista de servicios afectados</param>
    /// <param name="userName">Nombre del usuario que realiza la modificacion</param>
    /// <param name="levelChanged">Indica si la incidencia ha cambiado de nivel</param>
    /// <param name="isEnd">Determina si la incidencia ha finalizado</param>
    /// <returns>estado</returns>
    /// <remarks>El campo CodIncidence de la estructura inc debe contener un valor</remarks>
    public Boolean UpdateIncidence(Incidence inc, SolutionResponsible[] resp, AffectedClient[] client, AffectedService[] service, attachment[] attach, string userName, bool levelChanged, bool isEnd)
    {
        Decimal codIncidence = inc.CodIncidence;
        String sql;
        int res, res2;
        decimal cod_incidence_log = 0;
        Boolean result = false;

        #region Creacion de la consulta

        sql = "DECLARE\n" +
              "        cod_incidence_log DECIMAL;\n" +
              "        message VARCHAR2(500);\n" +
              "BEGIN\n" +
              "DELETE\n" +
              "FROM IN_INC_SOLUTION_RESPONSIBLES\n" +
              string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence);

        sql += "DELETE\n" +
              "FROM IN_INC_AFFECTED_CLIENTS\n" +
              string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence);

        sql += "DELETE\n" +
              "FROM IN_AFFECTED_SERVICES\n" +
              string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence);      



        foreach (AffectedClient c in client)
        {
            if (c != null)
            {
                if (c.CodAffectedClient != 0)
                {
                    sql += "INSERT INTO IN_INC_AFFECTED_CLIENTS (COD_AFFECTED_CLIENT, COD_INCIDENCE)\n" +
                    String.Format("VALUES ({0}, {1});\n", c.CodAffectedClient, inc.CodIncidence);
                }
            }
        }

        foreach (SolutionResponsible r in resp)
        {
            if (r != null)
            {
                if (r.CodSolutionResponsible != 0)
                {
                    sql += "INSERT INTO IN_INC_SOLUTION_RESPONSIBLES (COD_SOLUTION_RESPONSIBLE, COD_INCIDENCE)\n" +
                    String.Format("VALUES ({0}, {1});\n", r.CodSolutionResponsible, inc.CodIncidence);
                }
            }
        }       

        
        Decimal receivedCalls = 0;
        foreach (AffectedService s in service)
        {
            if (s != null)
            {
                if (s.Name != string.Empty)
                {
                    sql += "INSERT INTO IN_AFFECTED_SERVICES (COD_INCIDENCE, RECEIVED_CALLS,RECORD_DATE,AFFECTED_SERVICE)\n" +
                    String.Format("VALUES ({2}, {0}, sysdate, '{1}');\n", s.ReceivedCalls, s.Name, inc.CodIncidence);
                    receivedCalls += s.ReceivedCalls;
                }
            }
        }
                

        sql += "UPDATE IN_INCIDENCES\n" +
               string.Format("SET COD_SEGMENT = {0}, COD_TYPE = {1}, COD_LEVEL = {2}, DESCRIPTION = '{3}', COD_MOTIVE = {4}, INCIDENCE_CAUSE = '{5}', \"MONITORING\" = :Monitoring, END_DATE = TO_DATE({6},'ddmmyyyyhh24miss'), SCRIPT = '{7}', TYPOLOGY = '{8}', FOLIO_OT='{9}' ,SUBJECT='{10}',MAINTENANCE='{11}'",
                                inc.CodSegment, inc.CodType, inc.CodLevel, inc.Description, inc.CodMotive, inc.IncidenceCause, inc.EndDate.HasValue ? "'" + inc.EndDate.Value.ToString("ddMMyyyyHHmmss") + "'" : "NULL", inc.Script, inc.Typology, inc.Folio_OT, inc.Subject, inc.Maintenance) +
               "\nWHERE COD_INCIDENCE = " + codIncidence + ";\n";

        
        //Ingresar en la tabla de LOGS dependiendo del motivo ingresado

        if (levelChanged)
        {
            sql += "SELECT COD_INCIDENCE_LOG_SEQ.NEXTVAL INTO cod_incidence_log FROM DUAL;\n" +
                "INSERT INTO IN_INCIDENCE_LOGS (COD_INCIDENCE_LOG,COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",LOG_DATE, USERNAME, IS_LEVEL_CHANGE_LOG,RECEIVED_CALLS,SCRIPT,TYPOLOGY,CRITICALITY,IN_COUNTRY_PK, FOLIO_OT,SUBJECT,MAINTENANCE)\n" +
                  String.Format("VALUES (cod_incidence_log,{7},{0},{1},{2},{3},'{4}','{5}',:Monitoring,sysdate,'{6}','S',{8},'{9}','{10}',{11},{12},'{13}','{14}','{15}');\n",
                      inc.CodLevel, inc.codMotive, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                      userName, inc.CodIncidence, receivedCalls, inc.Script, inc.Typology, inc.Critical, inc.IdCountry, inc.Folio_OT, inc.Subject, inc.Maintenance);
            //sql += "\nCOMMIT;\nSELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(cod_incidence_log) INTO message FROM DUAL; ";
            sql += "\nCOMMIT;";
        }
        else
        {
            sql += "SELECT COD_INCIDENCE_LOG_SEQ.NEXTVAL INTO cod_incidence_log FROM DUAL;\n" +
                "INSERT INTO IN_INCIDENCE_LOGS (COD_INCIDENCE_LOG,COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",LOG_DATE, USERNAME, IS_LEVEL_CHANGE_LOG, RECEIVED_CALLS,SCRIPT,TYPOLOGY,CRITICALITY,IN_COUNTRY_PK, FOLIO_OT,SUBJECT,MAINTENANCE)\n" +
                  String.Format("VALUES (cod_incidence_log,{7},{0},{1},{2},{3},'{4}','{5}',:Monitoring,sysdate,'{6}','N',{8},'{9}','{10}',{11},{12},'{13}','{14}','{15}');\n",
                      inc.CodLevel, inc.codMotive, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                      userName, inc.CodIncidence, receivedCalls, inc.Script, inc.Typology, inc.Critical, inc.IdCountry, inc.Folio_OT, inc.Subject, inc.Maintenance);
            //sql += "\nCOMMIT;\nSELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(cod_incidence_log) INTO message FROM DUAL; ";
            sql += "\nCOMMIT;";
        }
        
        sql += "END;";

        #endregion

        try
        {
            res = ConfigurationTool.ExecQueryParam(sql, inc.Monitoring);

            attachment deleteattach = new attachment();
            deleteattach.DeleteAllAttach(inc.CodIncidence);

            foreach (attachment at in attach)
            {

                if (at != null)
                {
                    if (at.DocName != "")
                    {
                        attachment actAttach = new attachment();

                        actAttach.DocName = at.DocName;
                        actAttach.Docbyte = at.Docbyte;
                        actAttach.CodIncidence = codIncidence;
                        actAttach.DocExt = at.DocExt;
                        actAttach.DocMime = at.DocMime;
                        actAttach.InsertAttach(actAttach);
                    }
                }
            }

            DataTable dtIncidencesLog = new Incidence().GetCodIncidentLog(codIncidence);

            foreach (DataRow row in dtIncidencesLog.Rows)
            {
                cod_incidence_log = (decimal)row["COD_INCIDENCE_LOG"];
            }

            string sqlemail = "DECLARE\n" +
                "        message VARCHAR2(500);\n" +
                "BEGIN\n" +
                "         SELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(" + cod_incidence_log + ") INTO message FROM DUAL;";
            sqlemail += "END;";
            res2 = ConfigurationTool.ExecQuery(sqlemail);

            this.Messages.Message = "Incidencia Actualizada con éxito";
            // this.Messages.Message = sql;
            this.Messages.Status = 1;
            result = true;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message;
            //this.Messages.Message = sql;
            this.Messages.Status = 0;
            result = false;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        return result;
    }

    public bool SetTotalCalls(AffectedService[] services, SolutionResponsible[] solResp, attachment[] attach, Decimal codIncidence, Incidence inc, string userName)
    {
        bool result = false;        
        
        //Eliminamos los servicios Afectados y los responsables de la solucion
        string sql = "DECLARE\n" +
                     "   cod_incidence_log DECIMAL;\n" +
                     "   message VARCHAR2(500);\n" +
                     "BEGIN\n" +
                      "DELETE\n" +
                      "FROM IN_AFFECTED_SERVICES\n" +
                      string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence) +
                      "DELETE\n" +
                      "FROM IN_INC_SOLUTION_RESPONSIBLES\n" +
                      string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence);
                     //"DELETE\n" +
                     //"FROM IN_ATTACHMENTS\n" +
                     // string.Format("WHERE COD_INCIDENCE = {0} \n;", codIncidence);          

        //Actualizamos la Incidencia
        sql += "UPDATE IN_INCIDENCES\n" +
                string.Format("SET INCIDENCE_CAUSE='{0}', DESCRIPTION='{1}' , \"MONITORING\" = :Monitoring , SUBJECT = '{2}', MAINTENANCE = '{3}'  ",
                 inc.IncidenceCause, inc.Description, inc.Subject, inc.Maintenance) +
                "\n WHERE COD_INCIDENCE=" + codIncidence + ";\n";

        //Servicios Afectados
        Decimal receivedCalls = 0;
        foreach (AffectedService s in services)
        {
            if (s != null)
            {
                if (s.Name != string.Empty)
                {
                    sql += "INSERT INTO IN_AFFECTED_SERVICES (COD_INCIDENCE, RECEIVED_CALLS,RECORD_DATE,AFFECTED_SERVICE)\n" +
                    String.Format("VALUES ({2}, {0}, sysdate, '{1}');\n", s.ReceivedCalls, s.Name, codIncidence);
                    receivedCalls += s.ReceivedCalls;
                }
            }
        }

       
        //Responsable de la solucion 
        foreach (SolutionResponsible r in solResp)
        {
            if (r != null)
            {
                if (r.CodSolutionResponsible != 0)
                {
                    sql += "INSERT INTO IN_INC_SOLUTION_RESPONSIBLES (COD_SOLUTION_RESPONSIBLE, COD_INCIDENCE)\n" +
                    String.Format("VALUES ({0}, {1});\n", r.CodSolutionResponsible, inc.CodIncidence);
                }
            }
        }
        
        
        //Ingresamos en el Log
        sql += "SELECT COD_INCIDENCE_LOG_SEQ.NEXTVAL INTO cod_incidence_log FROM DUAL;\n" +
                        "INSERT INTO IN_INCIDENCE_LOGS (COD_INCIDENCE_LOG,COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",LOG_DATE, USERNAME, IS_LEVEL_CHANGE_LOG, RECEIVED_CALLS,SCRIPT,TYPOLOGY,CRITICALITY,IN_COUNTRY_PK,FOLIO_OT,SUBJECT,MAINTENANCE)\n" +
                          String.Format("VALUES (cod_incidence_log,{7},{0},{1},{2},{3},'{4}','{5}',:Monitoring,sysdate,'{6}','N',{8},'{9}','{10}',{11},{12},'{13}','{14}','{15}');\n",
                              inc.CodLevel, inc.codMotive, inc.CodSegment, inc.CodType, inc.Description, inc.IncidenceCause,
                              userName, inc.CodIncidence, receivedCalls, inc.Script, inc.Typology, inc.Critical, inc.IdCountry, inc.Folio_OT, inc.Subject, inc.Maintenance);
        sql += "\nCOMMIT;";

        sql += "END;";


        try
        {            
            int res = ConfigurationTool.ExecQueryParam(sql, inc.Monitoring);

            attachment deleteattach = new attachment();
            deleteattach.DeleteAllAttach(inc.CodIncidence);

            foreach (attachment at in attach)
            {

                if (at != null)
                {
                    if (at.DocName != "")
                    {
                        attachment actAttach = new attachment();

                        actAttach.DocName = at.DocName;
                        actAttach.Docbyte = at.Docbyte;
                        actAttach.CodIncidence = codIncidence;
                        actAttach.DocExt = at.DocExt;
                        actAttach.DocMime = at.DocMime;
                        actAttach.InsertAttach(actAttach);

                        //sql += "INSERT INTO IN_ATTACHMENTS (FILE_NAME, ATTACHMENT_FILE, EXTENSION, TIPOMIME, RECORD_DATE, COD_INCIDENCE)\n" +
                        //String.Format("VALUES ('{0}','{1}','{2}','{3}',sysdate, {4});\n", at.DocName, at.Docbyte, at.DocExt, at.DocMime, inc.CodIncidence);

                    }
                }
            }

            this.Messages.Message = "Registro Actualizado con Éxito";
            // this.Messages.Message = sql;
            this.Messages.Status = 1;
            result = true;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message;
            //this.Messages.Message = sql;
            this.Messages.Status = 0;
            result = false;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        return result;
    }

    /// <summary>
    /// Captura la secuencia de la incidencia
    /// </summary>
    /// <returns></returns>
    public DataTable GetIncidentSeq()
    {
        DataTable IncidentSeq = new DataTable();

        string query = "SELECT COD_INCIDENCE_SEQ.NEXTVAL INCIDENTSEQ FROM DUAL";          


        IncidentSeq = ConfigurationTool.GetQueryResult(query);

        return IncidentSeq;
    }

    /// <summary>
    /// Captura el codigo del log de la incidencia
    /// </summary>
    /// <param name="codIncidence"></param>
    /// <returns></returns>
    public DataTable GetCodIncidentLog(Decimal codIncidence)
    {
        DataTable IncidentLog = new DataTable();
        string query = "SELECT MAX(COD_INCIDENCE_LOG) COD_INCIDENCE_LOG " +
                       "FROM IN_INCIDENCE_LOGS " +
                       "WHERE COD_INCIDENCE = " + codIncidence;

        IncidentLog = ConfigurationTool.GetQueryResult(query);

        return IncidentLog;
    }
    #endregion

}
#endregion

#region "SOLUCION"

/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los Envío Solución
/// Autor:XOLO
/// Fecha:12/03/2015
/// </summary>
public class Solucion
{

    #region CONSTRUCTOR DE LA CLASE

    public Solucion()
    {
        messages = new GlobalStructure();
        dtIncidences = new dsIncidentNotification.IN_INCIDENCESDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private Decimal codIncidence;
    private Decimal codSegment;
    private Decimal codType;
    private Decimal codLevel;
    private Decimal codMotive;
    private String description;
    private String monitoring;
    private String incidenceCause;
    private DateTime startDate;
    private DateTime? endDate;
    private String script;
    private String typology;
    private dsIncidentNotification.IN_INCIDENCESDataTable dtIncidences;
    private Decimal idCountry;
    private Decimal critical;
    private String _Folio_OT;

    #endregion


    #region PROPIEDADES

    /// <summary>
    /// Codigo del tipo de 
    /// motivo, ya sea INICIO, SEGUIMIENTO, FIN
    /// </summary>
    public Decimal CodMotive
    {
        get { return codMotive; }
        set { codMotive = value; }
    }

    /// <summary>
    /// Codigo del nivel asociado 
    /// al tipo
    /// </summary>
    public Decimal CodLevel
    {
        get { return codLevel; }
        set { codLevel = value; }
    }

    /// <summary>
    /// Codigo del tipo asociado al 
    /// segmento de la incidencia
    /// </summary>
    public Decimal CodType
    {
        get { return codType; }
        set { codType = value; }
    }

    /// <summary>
    /// Codigo del Segmento en el cual esta la
    /// incidencia
    /// </summary>
    public Decimal CodSegment
    {
        get { return codSegment; }
        set { codSegment = value; }
    }

    /// <summary>
    /// Codigo de la incidencia
    /// </summary>
    public Decimal CodIncidence
    {
        get { return codIncidence; }
        set { codIncidence = value; }
    }

    /// <summary>
    /// Causa de la incidencia
    /// </summary>
    public String IncidenceCause
    {
        get { return incidenceCause; }
        set { incidenceCause = value; }
    }


    /// <summary>
    /// Monitoreo
    /// </summary>
    public String Monitoring
    {
        get { return monitoring; }
        set { monitoring = value; }
    }

    /// <summary>
    /// Descripcion de la incidencia
    /// </summary>
    public String Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Fecha de inicio de la Incidencia
    /// </summary>
    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    /// <summary>
    /// Fecha de fin de la incidencia
    /// </summary>
    public DateTime? EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }

    /// <summary>
    /// Texto que se muestra al cliente 
    /// </summary>
    public String Script
    {
        get { return script; }
        set { script = value; }
    }

    /// <summary>
    /// Tipologia con la que se uincio el caso 
    /// </summary>
    public String Typology
    {
        get { return typology; }
        set { typology = value; }
    }


    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// segmentos
    /// </summary>
    public dsIncidentNotification.IN_INCIDENCESDataTable IncidencesTable
    {
        get { return dtIncidences; }
        set { dtIncidences = value; }
    }


    /// <summary>
    /// Identificador del País seleccionado
    /// </summary>
    public Decimal IdCountry
    {
        get { return idCountry; }
        set { idCountry = value; }
    }

    /// <summary>
    /// Identificador del País seleccionado
    /// </summary>
    public Decimal Critical
    {
        get { return critical; }
        set { critical = value; }
    }

    /// <summary>
    /// Numero de Folio/OT
    /// </summary>
    public String Folio_OT
    {
        get { return _Folio_OT; }
        set { _Folio_OT = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el Segmento y Tipo actual de una Incidencia
    /// </summary>
    /// <param name="codIncidence">Identificador de la incidencia</param>
    /// <returns></returns>
    public DataTable GetSegmentType(decimal codIncidence)
    {
        try
        {
            decimal resp = 0;
            string mySentence = string.Format(" SELECT  MOT.COD_MOTIVE, INC.COD_SEGMENT, INC.COD_TYPE,INC.IN_COUNTRY_PK, MOT.MOTIVE_NAME " +
                                       " FROM         IN_INCIDENCES INC, IN_CAT_MOTIVES MOT " +
                                     " WHERE     INC.COD_MOTIVE = MOT.COD_MOTIVE AND (INC.COD_INCIDENCE = {0}) ", codIncidence);
            DataTable result = new DataTable();

            result = ConfigurationTool.GetQueryResult(mySentence);
            if (result.Rows.Count > 0)
            {
                this.Messages.Message = "Datos Obtenidos Exitosamente";
                this.Messages.Status = 1;

            }
            return result;

        }
        catch (Exception ex)
        {
            this.Messages.Message = "Inconveniente en consulta Nivel Criticidad";
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return null;
        }

    }

    public string GetParam(string paramName)
    {
        string result = string.Empty;
        try
        {
            string mySentence = string.Format(" SELECT PARAMETER_ALIAS,PARAMETER_VALUE FROM IN_PARAMETERS WHERE PARAMETER_ALIAS='{0}' ", paramName);
            DataTable resp = new DataTable();

            resp = ConfigurationTool.GetQueryResult(mySentence);

            if (resp.Rows.Count > 0)
            {
                result = resp.Rows[0]["PARAMETER_VALUE"].ToString();
            }


            return result;
        }
        catch
        {
            return string.Empty;
        }

    }

    #endregion

}

#endregion

#region HISTORIAL

/// <summary>
/// Clase que contiene los métodos 
/// necesarios para realizar las consultas de historial de incidencias
/// Autor:Manuel Gutiérrez Rojas
/// Fecha:10.Nov.2011
/// </summary>
public class IncidenceHistory
{
    #region CONSTRUCTOR DE LA CLASE

    public IncidenceHistory()
    {
        messages = new GlobalStructure();
        dtIncidenceHistory = new dsIncidentNotification.IN_INCIDENCE_LOGSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private Decimal codIncidence;
    private Decimal codIncidenceLog;
    private String motive;
    private String monitoring;
    private String incidenceCause;
    private DateTime logDate;
    private dsIncidentNotification.IN_INCIDENCE_LOGSDataTable dtIncidenceHistory;


    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Codigo del tipo de 
    /// motivo, ya sea INICIO, SEGUIMIENTO, FIN
    /// </summary>
    public String CodMotive
    {
        get { return motive; }
        set { motive = value; }
    }

    /// <summary>
    /// Codigo de la incidencia
    /// </summary>
    public Decimal CodIncidence
    {
        get { return codIncidence; }
        set { codIncidence = value; }
    }

    /// <summary>
    /// Causa de la incidencia
    /// </summary>
    public String IncidenceCause
    {
        get { return incidenceCause; }
        set { incidenceCause = value; }
    }


    /// <summary>
    /// Monitoreo
    /// </summary>
    public String Monitoring
    {
        get { return monitoring; }
        set { monitoring = value; }
    }

    /// <summary>
    /// Fecha de inicio de la Incidencia
    /// </summary>
    public DateTime LogDate
    {
        get { return logDate; }
        set { logDate = value; }
    }


    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// segmentos
    /// </summary>
    public dsIncidentNotification.IN_INCIDENCE_LOGSDataTable IncidenceHistoryTable
    {
        get { return dtIncidenceHistory; }
        set { dtIncidenceHistory = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el Historial de una incidencia
    /// Fecha : 10.Nov.2011
    /// Autor : Manuel Gutiérrez Rojas 
    /// </summary>
    /// <param name="codIncidence"> codigo de la incidencia </param>
    public DataTable GetIncidenceHistory(Decimal codIncidence)
    {
        DataTable dtHistory = new DataTable();

        try
        {
            dsIncidentNotificationTableAdapters.IN_INCIDENCE_LOGSTableAdapter taHistory = new dsIncidentNotificationTableAdapters.IN_INCIDENCE_LOGSTableAdapter();
            taHistory.FillByCodIncidence(IncidenceHistoryTable, codIncidence);
            dtHistory = IncidenceHistoryTable;
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }

        return dtHistory;
    }

    /// <summary>
    /// Obtine la informacion para 
    /// la Bitacora de incidencias
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha : 14.Nov.2011
    /// </summary>
    /// <param name="startDate">Fecha de inicio del Rango</param>
    /// <param name="endDate">Fecha de fin del Rango</param>
    /// <returns></returns>
    public DataTable IncidentLog(DateTime startDate, DateTime endDate)
    {
        DataTable dtIncidentLog = new DataTable();
        try
        {

        }
        catch (Exception ex)
        {

        }

        return dtIncidentLog;
    }

    #endregion
}


#endregion

#region PARAMETROS
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// Parametros
/// Autor: Xolo
/// Fecha:04.Agost.2015
/// </summary>
public class Parameters
{
    #region CONSTRUCTOR DE LA CLASE

    public Parameters()
    {
        messages = new GlobalStructure();
        dtParameter = new dsIncidentNotification.IN_PARAMETERSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string valor;
    private string alias;
    private decimal codParameter;    

    private dsIncidentNotification.IN_PARAMETERSDataTable dtParameter;

    #endregion

    #region PROPIEDADES

    public string Valor
    {
        get { return valor; }
        set { valor = value; }
    }

    public string Alias
    {
        get { return alias; }
        set { alias = value; }
    }


    
    public decimal CodParameter
    {
        get { return codParameter; }
        set { codParameter = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    public dsIncidentNotification.IN_PARAMETERSDataTable ParameterTable
    {
        get { return dtParameter; }
        set { dtParameter = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de parametros
    /// Fecha : 04.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetParameter(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter  IN_PARAMETERSTableAdapter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            IN_PARAMETERSTableAdapter.FillType(this.ParameterTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la información de un Parametro
    /// Fecha : 04.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codParameterinput">código del cliente</param>
    public void EditParameter(Decimal codParameterinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter IN_PARAMETERSTableAdapter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            IN_PARAMETERSTableAdapter.FillByCodParameter(ParameterTable, codParameterinput);

            Alias = ParameterTable[0].PARAMETER_ALIAS;
            Valor = ParameterTable[0].PARAMETER_VALUE;
            codParameter = ParameterTable[0].COD_PARAMETER;
            

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un nuevo Parametro
    /// Fecha : 04.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="info">informacion de Parametro</param>
    public void InsertParameter(Parameters info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter IN_PARAMETERSTableAdapter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            IN_PARAMETERSTableAdapter.InsertParameter(info.Alias.ToString(), info.Valor.ToString());

            this.Messages.Message = "Parametro Ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza la información de un Parametro
    /// Fecha : 04.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="info"></param>
    /// <param name="codParameter"></param>
    public void UpdateParameter(Parameters info, Decimal codParameterinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter IN_PARAMETERSTableAdapter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            IN_PARAMETERSTableAdapter.UpdateParameter(info.Alias.ToString(), info.Valor.ToString(), codParameterinput);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obetiene si Existe Código   
    /// </summary>
    /// <param name="codParameterInput">Codigo de Parametro</param>
    /// <returns>Existencia de Codigo</returns>
    public Int32 ExistCodigo(Decimal codParameterInput)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter taParameter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            counter = Convert.ToInt32(taParameter.ExistCodigo(codParameterInput));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }


    /// <summary>
    /// Obetiene si Existe Alias   
    /// </summary>
    /// <param name="codAliasInput">Codigo de Parametro</param>
    /// <returns>Existencia de Alia</returns>
    public Int32 ExistAlia(String AliasInput)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter taParameter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            counter = Convert.ToInt32(taParameter.ExistAlias(AliasInput));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }

    /// <summary>
    /// Obetiene valor de ´parametro segun el Alias   
    /// </summary>
    /// <param name="AliasInput">Codigo de Parametro</param>
    /// <returns>Existencia de Alia</returns>
    public String GetValueParameter(String AliasInput)
    {
        String ValueParameter = "";
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter taParameter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            ValueParameter = Convert.ToString(taParameter.GetParameterValueByAlias(AliasInput));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return ValueParameter;
    }
   
    #endregion
}
#endregion

#region CORREO MANTENIMIENTO

/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// los Correos de Mantenimiento
/// Autor: Xolo
/// Fecha:06.Agost.2015
/// </summary>
public class EmailMaintenance
{ 

    #region CONSTRUCTOR DE LA CLASE

    public EmailMaintenance()
    {
        messages = new GlobalStructure();        
        dtEmailMaintenances = new dsIncidentNotification.IN_EMAIL_MAINTENANCES_COUNTRYDataTable(); 
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;    
    private string status;   
    private string email;
    private decimal codEmailMaintenance;
    private DateTime recordDate;

    private dsIncidentNotification.IN_EMAIL_MAINTENANCES_COUNTRYDataTable dtEmailMaintenances;
    private decimal idCountryR;
    private dsIncidentNotification.IN_EMAIL_MAINTENANCESDataTable dtEmailMaintEdit;

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Estado del Correo de mantenimiento 
    /// Activo = 1, Inactivo = 0 
    /// </summary>
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    /// <summary>
    /// Correo electrónico 
    /// </summary>
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    
    /// <summary>
    /// Fecha del registro
    /// </summary>
    public DateTime RecordDate
    {
        get { return recordDate; }
        set { recordDate = value; }
    }

    /// <summary>
    /// Id del Correo de mantenimiento
    /// </summary>
    public decimal CodEmailMaintenance
    {
        get { return codEmailMaintenance; }
        set { codEmailMaintenance = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    /// <summary>
    /// Tabla con los datos de los
    /// responsables de solución
    /// </summary>
    public dsIncidentNotification.IN_EMAIL_MAINTENANCES_COUNTRYDataTable EmailMaintenanceTable
    {
        get { return dtEmailMaintenances; }
        set { dtEmailMaintenances = value; }
    }


    /// <summary>
    /// Identificador del País del responsable
    /// </summary>
    public decimal IdCountryR
    {
        get { return idCountryR; }
        set { idCountryR = value; }
    }


    /// <summary>
    /// Tabla con los datos de los
    /// correo de mantenimiento para edición
    /// </summary>
    public dsIncidentNotification.IN_EMAIL_MAINTENANCESDataTable EmailMaintEdit
    {
        get { return dtEmailMaintEdit; }
        set { dtEmailMaintEdit = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene el listado de Correo de mantenimiento
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetEmailMaintenance(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter IN_EMAIL_MAINTENANCESTableAdapter = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter();
            IN_EMAIL_MAINTENANCESTableAdapter.Fill(EmailMaintenanceTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene el listado de Correo de mantenimiento
    /// Según país
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="filter">filtro de busqueda</param>
    public void GetEmailMaintenanceByCountry(Decimal idCountry)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter IN_EMAIL_MAINTENANCESTableAdapter = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter();
            IN_EMAIL_MAINTENANCESTableAdapter.FillByCountry(EmailMaintenanceTable, idCountry);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obtiene la información de un Correo de
    /// mantenimiento
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codSolutionResponsible">código del responsable</param>
    public void EditEmailMaintenance(Decimal codEmailMaintenanceinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter taEmailMaintenance = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCES_COUNTRYTableAdapter();
            taEmailMaintenance.FillByCodEmailMaintenance(EmailMaintenanceTable, codEmailMaintenanceinput);



            Email = EmailMaintenanceTable[0].EMAIL;
            Status = EmailMaintenanceTable[0].RECORD_STATUS;
            RecordDate = EmailMaintenanceTable[0].RECORD_DATE;
            CodEmailMaintenance = EmailMaintenanceTable[0].COD_EMAIL_MAINTENANCE;
            IdCountryR = EmailMaintenanceTable[0].IN_COUNTRY_PK;

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Ingresa un Correo de Mantenimiento
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="info">informacion del responsable de solución</param>
    public void InsertEmailMaintenance(EmailMaintenance info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter IN_EMAIL_MAINTENANCESTableAdapter = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter();
            IN_EMAIL_MAINTENANCESTableAdapter.InsertEmailMaintenance( info.Email, info.Status, info.IdCountryR);

            this.Messages.Message = "Correo Mantenimiento ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Actualiza la información de un 
    /// Correo de Mantenimiento
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="info">información modificada del responsable</param>
    /// <param name="codEmailMaintenanceinput">código del responsable</param>
    public void UpdateEmailMaintenance(EmailMaintenance info, Decimal codEmailMaintenanceinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter IN_EMAIL_MAINTENANCESTableAdapter = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter();
            IN_EMAIL_MAINTENANCESTableAdapter.UpdateEmailMaintenance(info.Email, info.Status, info.IdCountryR, codEmailMaintenanceinput);

            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Obetiene si Existe Alias   
    /// </summary>
    /// <param name="codAliasInput">Codigo de Parametro</param>
    /// <returns>Existencia de Alia</returns>
    public Int32 ExistEmailCountry(String EmailInput, Decimal CountryInput)
    {
        Int32 counter = 0;
        try
        {
            dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter taEmailCountry = new dsIncidentNotificationTableAdapters.IN_EMAIL_MAINTENANCESTableAdapter();
            counter = Convert.ToInt32(taEmailCountry.ExistEmailPais(EmailInput, CountryInput));

            this.Messages.Message = "Información obtenida con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        return counter;
    }

    #endregion
}

#endregion

#region "ADJUNTAR ARCHIVOS A INCIDENCIAS"
/// <summary>
/// Clase que contiene los métodos para las acciones de 
/// Adjuntar
/// Autor: Xolo
/// Fecha:17.Agost.2015
/// </summary>
public class attachment
{
    #region CONSTRUCTOR DE LA CLASE

    public attachment()
    {
        messages = new GlobalStructure();          
        dtAttach = new dsIncidentNotification.IN_ATTACHMENTSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string docname;
    private byte[] docbyte;    
    private decimal codIncidence;
    private string docext;
    private string docmime;
    private decimal codAttach;
    private dsIncidentNotification.IN_ATTACHMENTSDataTable dtAttach;
    #endregion

    #region PROPIEDADES

    public string DocName
    {
        get { return docname; }
        set { docname = value; }
    }

    public byte[] Docbyte
    {
        get { return docbyte; }
        set { docbyte = value; }
    }   

    public decimal CodIncidence
    {
        get { return codIncidence; }
        set { codIncidence = value; }
    }

    public string DocExt
    {
        get { return docext; }
        set { docext = value; }
    }

    public string DocMime
    {
        get { return docmime; }
        set { docmime = value; }
    }

    public decimal CodAttach
    {
        get { return codAttach; }
        set { codAttach = value; }
    }
    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    public dsIncidentNotification.IN_ATTACHMENTSDataTable AttachTable
    {
        get { return dtAttach; }
        set { dtAttach = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Obtiene la información de un Attach
    /// Fecha : 18.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codIncidentinput">código del Incidente</param>
    public void EditAttach(Decimal codIncidentinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter  IN_ATTACHMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter();
            IN_ATTACHMENTSTableAdapter.FillByCodeIncidence(AttachTable, codIncidentinput);

            //DocName = AttachTable[0].FILE_NAME;
            //AttachBinary = AttachTable[0].ATTACHMENT_FILE;
            //CodAttach = AttachTable[0].COD_ATTACHMENT;
            //CodIncidence = AttachTable[0].COD_INCIDENCE;          


            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Ingresa un nuevo Attach
    /// Fecha : 18.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="info">informacion de Attachment</param>
    public void InsertAttach(attachment info)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter IN_ATTACHMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter();
            IN_ATTACHMENTSTableAdapter.Insert(0, info.CodIncidence, info.DocName.ToString(), DateTime.Now, info.Docbyte, info.DocExt, info.DocMime);

            
            this.Messages.Message = "Archivos Ingresado con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Borra archivo adjunto ingresado en la tabla para el incidente indicado
    /// Fecha : 18.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codIncidentinput">código del Incidente</param>
    /// <param name="nameFileInput">Nombre del Archivo adjunto</param>
    public void DeleteAttach(Decimal codIncidentinput, String nameFileInput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter IN_ATTACHMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter();
            IN_ATTACHMENTSTableAdapter.DeleteAttachment(codIncidentinput,nameFileInput);     

            this.Messages.Message = "Archivo Adjunto Borrado Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Borra lista de adjuntos ingresados en la tabla para el incidente indicado
    /// Fecha : 18.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codIncidentinput">código del Incidente</param>    
    public void DeleteAllAttach(Decimal codIncidentinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter IN_ATTACHMENTSTableAdapter = new dsIncidentNotificationTableAdapters.IN_ATTACHMENTSTableAdapter();
            IN_ATTACHMENTSTableAdapter.DeleteAllAttachByIncindence(codIncidentinput);

            this.Messages.Message = "Dato Borrado Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    #endregion
}
#endregion

#region HTMLEditor
public class HTMLEditor
{
    private System.Data.OracleClient.OracleConnection connOracle;
    private System.Data.OracleClient.OracleDataReader rstOracle;
    private System.Data.OracleClient.OracleCommand sqlCommandOracle;
    private System.Data.OracleClient.OracleTransaction txn;
    private System.Data.OracleClient.OracleLob clob;

    #region CONSTRUCTOR DE LA CLASE

    public HTMLEditor()
    {
        messages = new GlobalStructure();
        dtHTML = new dsIncidentNotification.IN_HTML_NOTIFICATIONSDataTable();
    }

    #endregion

    #region VARIABLES

    private GlobalStructure messages;
    private string content;
    private string clasification;
    private string description;
    private string type;
    private decimal status;
    private decimal codHtml;
    private string strclasificacion;
    private string strtipo;
    
    private dsIncidentNotification.IN_HTML_NOTIFICATIONSDataTable dtHTML;

    #endregion

    #region PROPIEDADES

   

    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    public string Clasification
    {
        get { return clasification; }
        set { clasification = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    public decimal Status
    {
        get { return status; }
        set { status = value; }
    }

    public string descripcionClasificacion
    {
        get { return strclasificacion; }
        set { strclasificacion = value; }
    }

    public string descripcionTipo
    {
        get { return strtipo; }
        set { strtipo = value; }
    }

    public decimal CodHTML
    {
        get { return codHtml; }
        set { codHtml = value; }
    }

    public GlobalStructure Messages
    {
        get { return messages; }
        set { messages = value; }
    }

    public dsIncidentNotification.IN_HTML_NOTIFICATIONSDataTable HtmlTable
    {
        get { return dtHTML; }
        set { dtHTML = value; }
    }
    
    #endregion


    /// <summary>
    /// Prueba para Insert
    /// </summary>
    /// <param name="co">Contenido</param>
    /// <returns>Respuesta</returns>
    /*public string InsertHtml(string ContentHtml)
    {
        string res = string.Empty;
        int res2;
        try
        {
            string query = "INSERT INTO IN_TESTHTML (CONTENT_HTML) " +
                           "VALUES('" + ContentHtml + "')";
            //query = query + "";

            res2 = ConfigurationTool.ExecQuery(query);

            this.Messages.Message = "Registro Ingresado con éxito";
            //this.Messages.Message = sql;
            this.Messages.Status = 1;
            res = res2.ToString();
        }
        catch (Exception ex)
        {
            res = "-1" + ex.Message + "<---::::-->" + ex.StackTrace;
        }

        return res;
    }*/

    /// <summary>
    /// Obtiene el listado de los registros para HTML
    /// </summary>
    /// <returns></returns>
    /*public DataTable GetListHtmlContent()
    {
        DataTable dtResult = new DataTable();
        try
        {
            //Consulta a ejecutar
            string query = "SELECT COD_HTML_PK,DESCRIPTION_HTML,CONTENT_HTML, " +
                           " CASE WHEN CLASIFICATION='S' THEN 'Mantenimiento' ELSE 'Incidencia' END CLASIFICATION," +
                           " CASE WHEN TYPE_FORMAT='1' THEN 'Formato 1' ELSE 'Formato 2' END TIPO," +
                           " STATUS_HTML " +
                             " FROM IN_HTML_NOTIFICATIONS";

            //Método para procesar la consulta
            dtResult = ConfigurationTool.GetQueryResult(query);

        }
        catch
        {
            dtResult = null;
        }

        return dtResult;
    }*/

    public void GetHtml(string filter)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter IN_HTML_NOTIFICATIONSTableAdapter = new dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter();
            IN_HTML_NOTIFICATIONSTableAdapter.Fill(HtmlTable, filter);
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /// <summary>
    /// Obtiene los datos de un registro HTML, tabla IN_HTML_NOTIFICATIONS
    /// </summary>
    /// <param name="codHTML">Identificador del Registro</param>
    /// <returns>Datos</returns>
    /*public String GetHtmlData(decimal codHTML)
    {
        try
        {
            string resp = string.Empty;
            string mySentence = string.Format("SELECT COD_HTML_PK,DESCRIPTION_HTML,CONTENT_HTML,CLASIFICATION,TYPE_FORMAT,STATUS_HTML "+
                                                " FROM IN_HTML_NOTIFICATIONS " +
                                "WHERE COD_HTML_PK = {0}", codHTML);

            DataTable result = new DataTable();

            result = ConfigurationTool.GetQueryResult(mySentence);
            if (result.Rows.Count > 0)
            {
                //dtTypes.Columns["COD_TYPE"].ColumnName;
                resp = result.Rows[0]["CONTENT_HTML"].ToString();
            }
            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
            return resp;

        }
        catch (Exception ex)
        {
            this.Messages.Message = "Inconveniente en consulta Nivel Criticidad";
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
            return "-1";
        }

    }*/

    ///<summary>
    ///Actualizar
    ///</summary>

    public void UpdateHtml(string CONTENTHTML, string DESCRIP, Decimal COD)
    {
        OracleConnection oracleCon = new OracleConnection();

        try
        {

            String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
            oracleCon.ConnectionString = stringConection;

            oracleCon.Open();
           /* String query = "call IN_OPERATIONS_API.INSERT_HTML('" + CONTENTHTML + "', '" + DESCRIP + "', " + COD + ")";
            OracleCommand commandOracle = new OracleCommand(query, oracleCon);
            commandOracle.ExecuteOracleScalar().ToString();*/

            OracleCommand cmd = new OracleCommand("IN_OPERATIONS_API.INSERT_HTML", oracleCon);
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter pCONTENTHTML = new OracleParameter("CONTENTHTML", OracleType.NClob);
            pCONTENTHTML.Value = CONTENTHTML;
            cmd.Parameters.Add(pCONTENTHTML);
            OracleParameter pDESCRIP = new OracleParameter("DESCRIP", OracleType.VarChar);
            pDESCRIP.Value = DESCRIP;
            cmd.Parameters.Add(pDESCRIP);
            OracleParameter pCOD = new OracleParameter("COD", OracleType.Number);
            pCOD.Value = COD;
            cmd.Parameters.Add(pCOD);
            cmd.ExecuteNonQuery();
            oracleCon.Close();

            /*dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter IN_HTML_NOTIFICATIONSTableAdapter = new dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter();
            IN_HTML_NOTIFICATIONSTableAdapter.UpdateHtml(CONTENTHTML, DESCRIP, COD);*/
            //IN_HTML_NOTIFICATIONSTableAdapter.UpdateHtml(content, desc, codHtmlInput);
            /*String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
            oracleCon.ConnectionString = stringConection;
            oracleCon.Open();
            String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
            Oracle.DataAccess.Client.OracleConnection oraConn2 = new Oracle.DataAccess.Client.OracleConnection(stringConection);
            //oraConn2 = new Oracle.DataAccess.Client.OracleConnection(stringConection);
            oraConn2.Open();

            //OracleConnection Conex = new OracleConnection(ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString);
            //Oracle.DataAccess.Client.OracleConnection oraConn2 = null;
            OracleClob clob = new OracleClob(oraConn2, false, false);
            string str2 = CONTENTHTML;
            clob.Write(str2.ToCharArray(), 0, str2.Length);
            Oracle.DataAccess.Client.OracleCommand cmd2 = null;
            cmd2 = new Oracle.DataAccess.Client.OracleCommand("IN_OPERATIONS_API.UPDATE_TBLNOTIF", oraConn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("CONTENTHTML", Oracle.DataAccess.Client.OracleDbType.Clob, clob, System.Data.ParameterDirection.Input);
            cmd2.Parameters.Add("DESCRIP", Oracle.DataAccess.Client.OracleDbType.Varchar2, DESCRIP, System.Data.ParameterDirection.Input);
            cmd2.Parameters.Add("COD", Oracle.DataAccess.Client.OracleDbType.Double, COD, System.Data.ParameterDirection.Input);
            cmd2.ExecuteNonQuery();
            oraConn2.Close();*/


            this.Messages.Message = "Información actualizada con éxito";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
        
    }


    /// <summary>
    /// Obtiene la información de un Correo de
    /// mantenimiento
    /// Fecha : 06.Agost.2015
    /// Autor : Xolo
    /// </summary>
    /// <param name="codSolutionResponsible">código del responsable</param>
    public void EditHTML(Decimal codInput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter taHTML = new dsIncidentNotificationTableAdapters.IN_HTML_NOTIFICATIONSTableAdapter();
            taHTML.FillByCode(HtmlTable, codInput);

            Content = HtmlTable[0].CONTENT_HTML;
            CodHTML = HtmlTable[0].COD_HTML_PK;
            Clasification = HtmlTable[0].CLASIFICATION;
            Type = HtmlTable[0].TYPE_FORMAT;
            Description = HtmlTable[0].DESCRIPTION_HTML;
            Status = HtmlTable[0].STATUS_HTML;
            descripcionClasificacion = HtmlTable[0].STRCLASIFICATION;
            descripcionTipo = HtmlTable[0].STRTYPE;

            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }


    /*public void LoadHtmlByCod(Decimal codParameterinput)
    {
        try
        {
            dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter IN_PARAMETERSTableAdapter = new dsIncidentNotificationTableAdapters.IN_PARAMETERSTableAdapter();
            IN_PARAMETERSTableAdapter.FillByCodParameter(ParameterTable, codParameterinput);

            Alias = ParameterTable[0].PARAMETER_ALIAS;
            Valor = ParameterTable[0].PARAMETER_VALUE;
            codParameter = ParameterTable[0].COD_PARAMETER;


            this.Messages.Message = "Datos Obtenidos Exitosamente";
            this.Messages.Status = 1;
        }
        catch (Exception ex)
        {
            this.Messages.Message = ex.Message.ToString();
            this.Messages.Status = 0;
            SafetyPad.SetLogRecord("IncidentNotification.cs", ex.ToString());
        }
    }*/


}

#endregion

