using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppPages_Operations : System.Web.UI.Page
{
    DataView dv;
    string codLevelChg;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDown();
            hdfEdit.Value = SafetyPad.GetParameterXSA_CUENTA(2); //"EDITAR_SEGM";//
            hdfEditRol.Value = SafetyPad.IsAllowed("Editar").ToString(); //"true"; //
            //lblMessage.Text = "Valor Parametro 2 cuenta: "+ hdfEdit.Value + " ::::::::  Valor Rol: " + hdfEditRol.Value;
        }
    }

    #region Eventos

    /// <summary>
    /// Al cambiar el DropDownList Incidente, establecer estado y cargar el GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboRelDescription_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblState.Text = "";
    }

    /// <summary>
    /// Limpia el contenido del TextBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cleanButton_Click(object sender, EventArgs e)
    {
        txtIncident.Text = "";
        txtComment.Text = "";
        trState.Visible = false;
        trSegment.Visible = false;
         trType.Visible=false;
         //trStatus.Visible = false;
        grvOperations.Visible = false;
    }

     /// <summary>
    /// Enviar comentario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void sendButton_Click(object sender, EventArgs e)
    {
        try
        {
            String sql, strComment, segmentType=string.Empty, codLevels=string.Empty;
            string codSegm= string.Empty, codType=string.Empty, codLevel=string.Empty, seqLevel=string.Empty,seqLevelT=string.Empty; //,codMot=string.Empty;
            bool edit = false;
            //vfin = false;
            Level resultLevel = new Level();
            int res;
            DataView dt = new DataView();

            strComment = txtComment.Text.Replace("'", "''");


            if(strComment.Equals(""))
            {
                wucMessageControl.Message = "Debe de proveer un comentario";
                wucMessageControl.ShowPopup();
            }
            else
            {
                
                
                /*Obtener las filas para insertar en el Logs*/
                dsIncidentNotification.OperationsLogsDataTable dtIncidentLogs = new dsIncidentNotification.OperationsLogsDataTable();
                dsIncidentNotificationTableAdapters.OperationsLogsTableAdapter taIncidentLogs = new dsIncidentNotificationTableAdapters.OperationsLogsTableAdapter();

                taIncidentLogs.FillByCodIncidence(dtIncidentLogs, Convert.ToInt16(hidIncidentNumber.Value));

                /*Obtener código de motivo de Validación*/
                dsIncidentNotification.IN_CAT_MOTIVESDataTable dtMotive = new dsIncidentNotification.IN_CAT_MOTIVESDataTable();
                dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter taMotive = new dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter();


                /**Segmento de código para verificar si se hace modificación en segmentos y tipos*/
                //if (trSegment.Visible == true && trType.Visible == true && trStatus.Visible == true)
                if (trSegment.Visible == true && trType.Visible == true )
                {
                    
                    segmentType = string.Format(",COD_SEGMENT={0},COD_TYPE={1} ", ddlSegment.SelectedValue, ddlType.SelectedValue);
                    codSegm = ddlSegment.SelectedValue;
                    codType = ddlType.SelectedValue;

                    //El id de la secuencia del nivel del registro actual
                    resultLevel.GetLevelRec(decimal.Parse(dtIncidentLogs[0]["COD_LEVEL"].ToString()));
                    seqLevel = resultLevel.LevelsTable.Rows[0]["LEVEL_SEQUENCE"].ToString();
                    codLevels = resultLevel.LevelsTable.Rows[0]["COD_LEVEL"].ToString(); //código del nivel 

                    //Niveles del tipo seleccionado
                    resultLevel.GetLevelsType(decimal.Parse(ddlType.SelectedValue.ToString()));
                    foreach (DataRow item in resultLevel.LevelsTable.Rows)
                    {

                        seqLevelT = item["LEVEL_SEQUENCE"].ToString();
                        if (seqLevel == seqLevelT)
                            break;
                    }

                    if (seqLevelT != seqLevel)//(seqLevelT != seqLevel)                                   
                        codLevel = codLevelChg;                    
                    else
                      codLevel = resultLevel.GetCodLevelTypeLevel(decimal.Parse(ddlType.SelectedValue.ToString()), decimal.Parse(seqLevel) ).ToString();
                    
                    //codLevel= codLevelChg;
                    /*if (ddlStatus.SelectedItem.Text.ToString().ToUpper().Contains("FIN"))
                        vfin = true;
                    */
                    edit = true;
                }
                

                

                taMotive.FillByOperations(dtMotive);
                decimal criticalityLevel = 0;
                if ( !string.IsNullOrEmpty(dtIncidentLogs[0]["CRITICALITY"].ToString()))
                    criticalityLevel =decimal.Parse(dtIncidentLogs[0]["CRITICALITY"].ToString());

                sql = "DECLARE\n" +
                  "        cod_incidence_log DECIMAL;\n" +
                  "        message VARCHAR2(500);\n" +
                  "BEGIN\n";

                /* sql += "UPDATE IN_INCIDENCES\n" +
                  /*   string.Format("SET COD_MOTIVE = {0}, \"MONITORING\" = '{1}'" /*+ ((vfin) ? " ,END_DATE=sysdate ": " "), ((edit) ? ddlStatus.SelectedValue : dtMotive.Rows[0][0], strComment) + segmentType + ((edit) ? string.Format(",COD_LEVEL={0}", codLevel) : "") +
                 "\nWHERE COD_INCIDENCE = " + dtIncidentLogs[0]["COD_INCIDENCE"] + ";\n";*/

                   sql += "UPDATE IN_INCIDENCES\n" +
                     string.Format("SET COD_MOTIVE = {0}, \"MONITORING\" = :Monitoring", dtMotive.Rows[0][0]) + segmentType + ((edit) ? string.Format(",COD_LEVEL={0}", codLevel) : "") +
                 "\nWHERE COD_INCIDENCE = " + dtIncidentLogs[0]["COD_INCIDENCE"] + ";\n";


                sql += "SELECT COD_INCIDENCE_LOG_SEQ.NEXTVAL INTO cod_incidence_log FROM DUAL;\n" +
                            "INSERT INTO IN_INCIDENCE_LOGS (COD_INCIDENCE_LOG,COD_INCIDENCE, COD_LEVEL, COD_MOTIVE, COD_SEGMENT, COD_TYPE, DESCRIPTION,INCIDENCE_CAUSE, \"MONITORING\",LOG_DATE, USERNAME, IS_LEVEL_CHANGE_LOG, RECEIVED_CALLS, SCRIPT, TYPOLOGY,CRITICALITY,IN_COUNTRY_PK,SUBJECT,MAINTENANCE)\n" +
                              String.Format("VALUES (cod_incidence_log,{7},{0},{1},{2},{3},'{4}','{5}',:Monitoring,sysdate,'{6}','N',{8},'{9}','{10}',{11},{12},'{13}','{14}');\n",
                              (edit) ? codLevel : dtIncidentLogs[0]["COD_LEVEL"], /*((edit)? ddlStatus.SelectedValue:*/ dtMotive.Rows[0][0].ToString().Replace("'", "''"), (edit) ? codSegm : dtIncidentLogs[0]["COD_SEGMENT"], (edit) ? codType : dtIncidentLogs[0]["COD_TYPE"], dtIncidentLogs[0]["DESCRIPTION"].ToString().Replace("'", "''"), dtIncidentLogs[0]["INCIDENCE_CAUSE"].ToString().Replace("'", "''"),
                              SafetyPad.GetUserLogin(), dtIncidentLogs[0]["COD_INCIDENCE"], dtIncidentLogs[0]["RECEIVED_CALLS"], dtIncidentLogs[0]["SCRIPT"].ToString().Replace("'", "''"), dtIncidentLogs[0]["TYPOLOGY"].ToString().Replace("'", "''"), criticalityLevel, dtIncidentLogs[0]["IN_COUNTRY_PK"], dtIncidentLogs[0]["SUBJECT"], dtIncidentLogs[0]["MAINTENANCE"]);
                
                sql += "\nCOMMIT;\nSELECT IN_OPERATIONS_API.INCIDENCE_REPORT_SENT_FUN(cod_incidence_log) INTO message FROM DUAL; ";
                sql += "\nEND;";

                try
                {
                    res = ConfigurationTool.ExecQueryParam(sql, strComment);
                    taMotive.FillByOperations (dtMotive);
                    /*taMotive.FillByCodMotive(dtMotive,decimal.Parse (ddlStatus.SelectedValue.ToString()));*/
                    lblState.Text = dtMotive.Rows[0][1].ToString();
                    populateGridComments(Convert.ToInt16(hidIncidentNumber.Value));
                    //txtIncident_TextChanged(null, null);
                    wucMessageControl.Message = "La notificaci&oacute;n se realiz&oacute; correctamente!";
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                    wucMessageControl.ShowPopup();
                     txtComment.Text= String.Empty ;
                     /*if (vfin)
                     {
                         ddlSegment.Enabled = false;
                         ddlType.Enabled = false;
                         //ddlStatus.Enabled = false;
                         txtComment.Enabled = false;
                         sendButton.Enabled = false;
                     }*/
                    
                }
                catch (Exception ex)
                {
                    wucMessageControl.Message = ex.Message;
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                    SafetyPad.SetLogRecord("Operations.aspx.cs", ex.ToString());
                    wucMessageControl.ShowPopup();
                }
            }
        }
        catch (Exception ex)
        {
            SafetyPad.SetLogRecord("Operations.aspx.cs", ex.ToString());
        }
    }

    protected void grvOperations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvOperations.PageIndex = e.NewPageIndex;
        populateGridComments(Convert.ToInt16(hidIncidentNumber.Value));
    }

    protected void txtIncident_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int16 l_noincident = Convert.ToInt16(((TextBox)sender).Text.Split('-')[0]);
            Solucion result = new Solucion();
            Segment resp = new Segment();
            Level resultLevel = new Level();
            DataTable myResult = new DataTable();
            String seqLevelT = string.Empty;
            decimal codSegm = 0;
            decimal codType = 0;

            dsIncidentNotification.IN_CAT_MOTIVESDataTable dtMotives = new dsIncidentNotification.IN_CAT_MOTIVESDataTable();
            dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter taMotives = new dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter();

            taMotives.FillByIncidence(dtMotives, l_noincident);

            hidIncidentNumber.Value = l_noincident.ToString();

            if (dtMotives.Rows.Count > 0)
            {
                trState.Visible = true;
                trSegment.Visible = true;
                trType.Visible = true;
                //trStatus.Visible = true;
              
                //Se obtiene los valores, código segmento, código tipo y país.
               myResult= result.GetSegmentType(decimal.Parse(hidIncidentNumber.Value));
               if (myResult != null)
               {
                   //se guardan en variables
                   hdfCountry.Value = myResult.Rows[0]["IN_COUNTRY_PK"].ToString();
                   codSegm = decimal.Parse(myResult.Rows[0]["COD_SEGMENT"].ToString());
                   codType = decimal.Parse(myResult.Rows[0]["COD_TYPE"].ToString());

                   //Se obtienene solamente los segmentos según el país de la incidencia
                   resp.GetSegmentsByCountryAct(decimal.Parse(hdfCountry.Value));

                   //Se llena el combo de segmentos
                   ddlSegment.DataSource = resp.SegmentsTable.Select("RECORD_STATUS = 1"); ;
                   ddlSegment.DataValueField = resp.SegmentsTable.COD_SEGMENTColumn.ColumnName;
                   ddlSegment.DataTextField = resp.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;

                   

                   //Se le asigna el valor del segmento al combo
                   ddlSegment.SelectedValue = codSegm.ToString();
                   ddlSegment_SelectedIndexChanged(null, null); //Se cargan los tipos según el segmento seleccionado
                   try
                   {
                       ddlType.SelectedValue = codType.ToString(); //Se asigna el valor del tipo
                   }
                   catch
                   { }
                   if(String.IsNullOrEmpty( ddlType.SelectedValue.ToString()))
                       codLevelChg = resultLevel.GetCodLevelType(decimal.Parse(ddlType.SelectedValue)).ToString();
                   else
                       codLevelChg = "0";
                                                       

               }

                //Verificar si están todas las configuraciones para mostrar los datos de Segmento y Tipo
                VerifyEditSegmentType();

                lblState.Text = dtMotives.Rows[0][1].ToString();
                populateGridComments(l_noincident);


                if (lblState.Text.Trim().Contains("FIN"))
                {
                    txtComment.Enabled = false;
                    sendButton.Enabled = false;
                    ddlSegment.Enabled = false;
                    ddlType.Enabled = false;
                    //trStatus.Visible = false;
                }
                else
                {
                    txtComment.Enabled = true;
                    sendButton.Enabled = true;
                    ddlSegment.Enabled = true;
                    ddlType.Enabled = true;
                   // trStatus.Visible = true;
                }
            }
            else
                trState.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    /// <summary>
    /// Método para ordenamiento por campos de los Grids
    /// </summary>
    /// <param name="sortDirection"></param>
    /// <returns></returns>
    public string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string m_SortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                m_SortDirection = "ASC";
                break;

            case SortDirection.Descending:
                m_SortDirection = "DESC";
                break;
        }
        return m_SortDirection;
    }

    #endregion

    #region Métodos

    [WebMethod]
    public static string[] GetCompletionList(string prefixText, int count)
    {
        string strParam;
        dsIncidentNotification.OperationIncidentsDataTable dtIncidents = new dsIncidentNotification.OperationIncidentsDataTable();
        List<string> incidentList = new List<string>();

        try
        {
            strParam = (!prefixText.Trim().ToUpper().Equals("TODO") ? prefixText.ToUpper() : "");
            dsIncidentNotificationTableAdapters.OperationIncidentsTableAdapter taIncidents = new dsIncidentNotificationTableAdapters.OperationIncidentsTableAdapter();
            taIncidents.FillByParamC(dtIncidents, strParam);

            if (dtIncidents.Rows.Count > 0)
            {
                foreach (DataRow dR in dtIncidents.Rows)
                {
                    incidentList.Add(dR[0].ToString());
                }
            }
        }
        catch (Exception ex)
        {

        }

        return incidentList.ToArray();
    }

    private void populateGridComments(Int16 i_noincident)
    {
        dsIncidentNotification.OperationsGridDataTable dtGrid = new dsIncidentNotification.OperationsGridDataTable();
        dsIncidentNotificationTableAdapters.OperationsGridTableAdapter taGrid = new dsIncidentNotificationTableAdapters.OperationsGridTableAdapter();

        taGrid.Fill(dtGrid, i_noincident);

        if (dtGrid.Rows.Count > 0)
        {
            grvOperations.Visible = true;
            grvOperations.DataSource = dtGrid;
            grvOperations.DataBind();
        }
        else
        {
            grvOperations.Visible = false;
        }
    }

    #endregion


    #region Añadido
    /// <summary>
    /// Cargar los combos de Segmentos y Tipos
    /// Esto en caso que se carguen cuando es fin
    /// </summary>
    protected void LoadDropDown()
    {
        try
        {
            //Lista de Segmentos
            Segment segmentList = new Segment();
            segmentList.GetSegments(string.Empty);
            ddlSegment.DataSource = segmentList.SegmentsTable.Select("RECORD_STATUS = 1");
            ddlSegment.DataValueField = segmentList.SegmentsTable.COD_SEGMENTColumn.ColumnName;
            ddlSegment.DataTextField = segmentList.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;
            ddlSegment.DataBind();
            /*try
            {
                ddlSegment.SelectedValue =result.CodSegment.ToString();
            }
            catch
            {
                ddlSegment.DataSource = segmentList.SegmentsTable;
                ddlSegment.DataValueField = segmentList.SegmentsTable.COD_SEGMENTColumn.ColumnName;
                ddlSegment.DataTextField = segmentList.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;
                ddlSegment.DataBind();
                ddlSegment.SelectedValue = result.CodSegment.ToString();
            }*/
            Type typeList = new Type();
            ddlType.Items.Clear();
            typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
            ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
            ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
            ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
            ddlType.DataBind();
            /*if (typeList.TypesTable.Rows.Count > 0)
            {
                try
                {
                    ddlType.DataBind();
                    ddlType.SelectedValue = hdfType.Value; //result.CodType.ToString();
                }
                catch
                {
                    typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
                    ddlType.DataSource = typeList.TypesTable;
                    ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
                    ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
                    if (typeList.TypesTable.Rows.Count > 0)
                    {
                        ddlType.DataBind();
                        ddlType.SelectedValue = hdfType.Value; //result.CodType.ToString();
                    }

                }
            }*/

            /*Para estados*/
            /*dsIncidentNotification.IN_CAT_MOTIVESDataTable dtMotive = new dsIncidentNotification.IN_CAT_MOTIVESDataTable();
            dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter taMotive = new dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter();
            taMotive.FillByOperations(dtMotive);
            
            ddlStatus.DataSource = dtMotive;
            ddlStatus.DataValueField = dtMotive.COD_MOTIVEColumn.ColumnName;
            ddlStatus.DataTextField = dtMotive.MOTIVE_NAMEColumn.ColumnName;

            
            ddlStatus.DataBind();*/


        }
        catch (Exception ex)
        {
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.ShowPopup();
        }

    }

    protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Level resultLevel = new Level();
            Type typeList = new Type();
            ddlType.Items.Clear();
            typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
            ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
            ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
            ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
            ddlType.DataBind();
            

        }
        catch
        { }

    }

    /// <summary>
    /// Verifica los permisos para hacer cambios en el segmento y tipo de incidencia
    /// en caso que no tengan permiso no se muestran los campos
    /// </summary>
    protected void VerifyEditSegmentType()
    {
        try
        {
            Solucion resp = new Solucion();
            string valueParam=string.Empty;
            valueParam = resp.GetParam("EDITAR_SEGM_OP");
            //lblMessage.Text = lblMessage.Text + " ::::: Valor Parámetro: " + valueParam;
            if (valueParam == "1")
            {
                //  lblMessage.Text = lblMessage.Text + " Pasó por el Parámetro ";
                if (hdfEdit.Value.ToString().ToUpper().Equals("EDITAR_SEGM"))
                {
                    //    lblMessage.Text = lblMessage.Text + " :::: Pasó por hdfEdit ";
                    if (hdfEditRol.Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        //lblMessage.Text = lblMessage.Text + " ::: Pasó por hdfEditRol ";
                        applyVisible(true);
                    }
                    else
                        applyVisible(false);
                }
                else
                    applyVisible(false);

            }
            else
                applyVisible(false);

        }
        catch
        { }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Level resultLevel = new Level();
        codLevelChg = resultLevel.GetCodLevelType(decimal.Parse(ddlType.SelectedValue)).ToString();
    }

    #endregion

    /// <summary>
    /// Para indicar si las opciones serán visibles o no
    /// </summary>
    /// <param name="opt">boleano, verdadero o falso</param>
    protected void applyVisible(bool opt)
    {
        trSegment.Visible = opt;
        trType.Visible = opt;
        //trStatus.Visible = opt;
    }

   
}
