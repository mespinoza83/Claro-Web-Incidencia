using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Collections;

public partial class AppPages_Incidents : System.Web.UI.Page
{
    #region PROPIEDADES
    private List<AttachDocIncident> ListaAttachDoc
    {
        get { return (List<AttachDocIncident>)this.Session["ListaAttachDoc"]; }
        set { this.Session["ListaAttachDoc"] = value; }
    }
    #endregion
    
    #region "Variables"
    DataTable dtAffectedServices = new DataTable();
    private int  cantCall;      
    #endregion

    #region "Persistencia"
    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            object[] allStates = (object[])savedState;
            if (allStates[0] != null)
            {
                base.LoadViewState(allStates[0]);
            }            
            if (allStates[1] is int)
            {
                this.cantCall = (int)allStates[1];
            }            
        }
    }
    protected override object SaveViewState()
    {
        object[] allStates = new object[2];
        allStates[0] = base.SaveViewState();
        allStates[1] = this.cantCall;        
        return allStates;
    }
    #endregion

    #region "LOAD"

    /// <summary>
    /// Load de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                this.ListaAttachDoc = new List<AttachDocIncident>();
                //Obtenemos el listado de Codigos de Paises que tiene permitido 
                //el usuario conectado
                hdfCodigosPaises.Value = SafetyPad.GetParameterXSA_CUENTA(1);
                cantCall = 0;
                Session["FileUpload1"] = null;
                document_attachment_GetAll();        
                TitleSubtitle1.SetTitle("INCIDENCIAS");
                mvIncidents.ActiveViewIndex = 0;
                SetValidators();
                SetUserPermissions();
                Fill_dropDrowList(false);
                FillCbxLists();
                Load_Incidents();
                hdfVal.Value = "0";
                hdfLoad.Value = "1";
                ftbetbMonitoring.ValidChars = ftbetbMonitoring.ValidChars + System.Environment.NewLine;
            }
            else 
            {

                if (this.Session["FileUpload1"] == null && FileUpload1.HasFile) 
                {
                    this.Session["FileUpload1"] = FileUpload1;
                    txtFile.Text = FileUpload1.FileName;
                }
                else if (this.Session["FileUpload1"] != null && !FileUpload1.HasFile)
                {
                    FileUpload1 = (FileUpload)Session["FileUpload1"];
                    txtFile.Text = FileUpload1.FileName;
                }
                else if(FileUpload1.HasFile)
                {
                    Session["FileUpload1"] = FileUpload1;
                    txtFile.Text = FileUpload1.FileName;
                }            
             }
        }
        catch (Exception ex)
        {
                        
        }        
    }

    private void SetUserPermissions()
    {

        pnlAttach.Visible = SafetyPad.IsAllowed("SubirArchivos");       

    }
    
    #endregion
       
    #region CARGA DE LA PAGINA
    
    private void SetValidators()
    {
        Session["IsEndedInc"] = false;

        //tbDescription.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.BothCase);
        tbLevel.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.None);
    }

    /// <summary>
    /// Borra el contenido de los campos de
    /// la pantalla de incidencias
    /// </summary>
    private void ClearFields()
    {
        lblState.Text = "";
        tbMonitoring.Text = "";
        tbIncidentCause.Text = "";
		txtFolioOT.Text = string.Empty;
        tbLevel.Text = string.Empty;
        tbDescription.Text = string.Empty;
        foreach (ListItem item in cbxlistAfecctedClients.Items)
            item.Selected = false;
        foreach (ListItem item in cbxListResponsibles.Items)
            item.Selected = false;
			
        // Borramos los campos Mensaje (Script), Tipologia y Asunto 
        this.txtTypology.Text = string.Empty;        
        this.txtScript.Text = string.Empty;
        this.txtSubject.Text = string.Empty;
        cbxMaintenance.Checked = false;
        this.lblMessageAttach.Text = "";
        lbMessage.Visible = false;
        lbMessageddlType.Visible = false;
        Session.Remove("FileUpload1");
        Session.Remove("ListaAttachDoc");
       // document_attachment_GetAll();

        //Fill_dropDrowList(false);
        ddlCountry.DataSourceID = odsCountry.ID;
        odsCountry.DataBind();
        ddlCountry.DataBind();
        ddlCountry.SelectedValue = "-1";
        //ddlCountry_SelectedIndexChanged(null, null);        
		ddlCountry.Enabled = true;
    }

    /// <summary>
    /// Carga la informacion en los dropdownList de
    /// la pantalla de Incidencias
    /// </summary>
    private void Fill_dropDrowList(bool withCountry)
    {
        try
        {
            wucMessageControl.Visible = true;
            ddlSegment.Enabled = true;
            
            //Verifica si se cargan los datos al cambiar de país
            if (!withCountry)
            {
                ddlCountry.DataSourceID = odsCountry.ID;
                odsCountry.DataBind();
                ddlCountry.DataBind();
            }

                        
            //Lista de Segmentos
            Segment segmentList = new Segment();
            segmentList.GetSegments(string.Empty);

            if (!withCountry)
            {
                ddlSegment.DataSource = segmentList.SegmentsTable.Select("RECORD_STATUS = 1");
                ddlSegment.DataValueField = segmentList.SegmentsTable.COD_SEGMENTColumn.ColumnName;
                ddlSegment.DataTextField = segmentList.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;
                ddlSegment.DataBind();
            }
            else
            {
                segmentList.GetSegmentsByCountryAct(Convert.ToDecimal(ddlCountry.SelectedValue));
                ddlSegment.DataSource = segmentList.SegmentsTable.Select("RECORD_STATUS = 1"); 
                ddlSegment.DataValueField = segmentList.SegmentsTable.COD_SEGMENTColumn.ColumnName;
                ddlSegment.DataTextField = segmentList.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;
                ddlSegment.DataBind();
            }


            if (ddlSegment.Items.Count > 0)
            {
                //ddlSegment.SelectedIndex = 0;
                ddlSegment.Enabled = true;
                //hdfValue.Value = ddlSegment.SelectedValue;
                //Lista de tipos
                Type typeList = new Type();
                //typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
                if (string.IsNullOrEmpty(hdfValue.Value))
                    hdfValue.Value = "-1";
                typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
                if (!withCountry)
                {
                    ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
                    ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
                    ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
                    ddlType.DataBind();
                }
                else
                {
                    /* Validamos si aun no se ha seleccionado un Segmento, procederemos a cargar los tipos del primer segmenteo */
                    if(Convert.ToDecimal(hdfValue.Value).Equals(-1))
                        typeList.GetTypesByCountry(string.Empty, decimal.Parse(ddlSegment.Items[0].Value.ToString()), decimal.Parse(ddlCountry.SelectedValue));
                    else
                        typeList.GetTypesByCountry(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue), decimal.Parse(ddlCountry.SelectedValue));
                    
                    //ddlType.DataSource = typeList.TypesTable;
                    ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
                    ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
                    ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
                    ddlType.DataBind();
                }

                if (ddlType.Items.Count > 0)
                {
                    ddlType.SelectedIndex = 0;
                    ddlType.Enabled = true;

                    Level level = new Level();
                    DataTable dtLevels = level.getLessSequence(Convert.ToDecimal(ddlType.SelectedValue));
                    if (dtLevels.Rows.Count > 0)
                    {
                        tbLevel.Text = dtLevels.Rows[0]["LEVEL_NAME"].ToString();
                        Session["COD_LEVEL"] = dtLevels.Rows[0]["COD_LEVEL"].ToString();
                        lbMessage.Visible = false;
                    }
                    else
                    {
                        lbMessage.Visible = true;
                        wucMessageControl.Message = "No hay Niveles asociados al Tipo seleccionado.";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                        wucMessageControl.ShowPopup();
                        tbLevel.Enable = false;
                    }
                }
                else
                {
                    if (hdfLoad.Value == "0")
                    {
                        ddlType.Enabled = false;
                        tbLevel.Enable = false;
                        tbLevel.Text = "";
                        lbMessageddlType.Visible = true;
                        wucMessageControl.Message = "No hay Tipos asociados al Segmento seleccionado.";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                        wucMessageControl.ShowPopup();
                    }
                }
            }
            else
            {
                ddlSegment.Enabled = false;
                ddlType.Enabled = false;
                
                tbLevel.Enable = false;
                ddlType.DataSource = null;
                ddlType.DataBind();
                ddlType.Items.Clear();
                if (hdfEdit.Value == "0")
                {
                    if (Session["isIncNuew"] != null)
                    {
                        if (Session["isIncNuew"] == "0")
                        {
                            wucMessageControl.Message = "No hay información de Segmentos para mostrar.";
                            wucMessageControl.Image = "../include/imagenes/error_32.png";
                            wucMessageControl.ShowPopup();
                        }
                    }
                }
                if (ddlCountry.SelectedValue == "-1")
                {
                    if (hdfEdit.Value == "0")
                    {
                        if (Session["isIncNuew"] != null)
                        {
                            if (Session["isIncNuew"] == "0")
                            {
                                wucMessageControl.Message = "Seleccione un país para mostrar segmentos y Tipos.";
                                wucMessageControl.Image = "../include/imagenes/error_32.png";
                                wucMessageControl.ShowPopup();
                                wucMessageControl.Visible = false;
                            }
                        }
                    }
                }
            }

            //Lista de Motivos
            Motive motiveList = new Motive();
            motiveList.GetMotives();

            ddlMotive.DataSource = motiveList.MotivesTable;
            ddlMotive.DataValueField = motiveList.MotivesTable.COD_MOTIVEColumn.ColumnName;
            ddlMotive.DataTextField = motiveList.MotivesTable.MOTIVE_NAMEColumn.ColumnName;
            ddlMotive.DataBind();

            if (ddlMotive.Items.Count > 0)
            {
                ddlMotive.SelectedIndex = 0;
                ddlMotive.Enabled = true;
            }
            else
            {
                ddlMotive.Enabled = false;

                wucMessageControl.Message = "No hay información de Motivos para mostrar.";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.ShowPopup();
            }
        }
        catch (Exception err)
        {
            wucMessageControl.Message = err.Message.ToString();
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.ShowPopup();
        }
    }

    /// <summary>
    /// carga la informacion en las
    /// listas de checkbox 
    /// </summary>
    private void FillCbxLists()
    {
        //Lista de clientes Afectados
        AffectedClient affectedClients = new AffectedClient();
        affectedClients.GetAffectedClients(string.Empty);
        hdfValueR.Value = "1";
        if (affectedClients.Messages.Status == 1) //cargar los datos en la lista
        {
            cbxlistAfecctedClients.DataSource = affectedClients.AffectedClientsTable.Select("RECORD_STATUS = 1");
            cbxlistAfecctedClients.DataValueField = affectedClients.AffectedClientsTable.COD_AFFECTED_CLIENTColumn.ColumnName;
            cbxlistAfecctedClients.DataTextField = affectedClients.AffectedClientsTable.NAMEColumn.ColumnName;
            cbxlistAfecctedClients.DataBind();
        }
                       
        //Responsables de solucion
       
            SolutionResponsible responsibles = new SolutionResponsible();
            //responsibles.GetSolutionResponsibles(string.Empty);
            responsibles.GetSolutionResponsiblesByCountry(decimal.Parse(ddlCountry.SelectedValue));

            if (responsibles.Messages.Status == 1)
            {
                cbxListResponsibles.DataSource = responsibles.SolutionResponsiblesTable.Select("RECORD_STATUS = 1");
                cbxListResponsibles.DataValueField = responsibles.SolutionResponsiblesTable.COD_SOLUTION_RESPONSIBLEColumn.ColumnName;
                cbxListResponsibles.DataTextField = responsibles.SolutionResponsiblesTable.NAMEColumn.ColumnName;
                cbxListResponsibles.DataBind();
            }

            if (responsibles.SolutionResponsiblesTable.Rows.Count < 1)
            {
                if (ddlCountry.SelectedValue != "-1")
                {
                    hdfValueR.Value = "0";
                    /*if (hdfIn.Value != "1")
                    {*/
                    wucMessageControl.Message = "No hay Responsables Configurados para el país seleccionado";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                    wucMessageControl.ShowPopup();
                    //}

                }
            }
        
       

    }

    #endregion

    #region INCIDENCIAS

    /// <summary>
    ///  Carga la informacion de la vista de 
    ///  Incidencias
    /// </summary>
    private void Load_Incidents()
    {
        mvIncidents.ActiveViewIndex = 0;
        TitleSubtitle1.SetTitle("INCIDENCIAS");
        //ddlMotive.SelectedItem.Text = string.Empty;
        Fill_gvIncidents(string.Empty,-1, false);
    }

    /// <summary>
    /// Carga la informacion de las incidencias
    /// con un filtro de busqueda
    /// Manuel Gutierrez Rojas
    /// 28.Oct.2011
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="IN_COUNTRY_PK">Filtro Pais</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvIncidents(string searchFilter, int IN_COUNTRY_PK, bool showErrorMessage)
    {
        Incidence result = new Incidence();
        result.GetIncidences(searchFilter, IN_COUNTRY_PK);            
                           

        if (wucMessageControl.Visible == false)
            wucMessageControl.Visible = true;

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.IncidencesTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvIncidentMain.DataSource = result.IncidencesTable;
        gvIncidentMain.DataBind();
    }
       
    /// <summary>
    /// Obtiene la informacion de 
    /// una incidencia en especifico y la coloca e
    /// en el formulario
    /// </summary>
    /// <param name="rowIndex">indice de la fila seleccionada</param>
    private void EditIncident(int rowIndex)
    {
        //System.Threading.Thread.Sleep(60000);
            //Obtener el código de la incidencia
            ddlCountry.Enabled = true;
            Session["COD_INCIDENCE"] = gvIncidentMain.DataKeys[rowIndex]["COD_INCIDENCE"].ToString();
            Session["DESC_MOTIVE"] = gvIncidentMain.DataKeys[rowIndex]["MOTIVE_NAME"].ToString();
            Decimal codIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);
            string strMotive = Session["DESC_MOTIVE"].ToString();
            decimal statusValue = 0;
            document_attachment_GetAll();

            //Cargar la información 
            Incidence result = new Incidence();
            result.EditIncidence(codIncidence);

            if (result.Messages.Status == 0) //Si hay un error
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = result.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else //Si el dato es correcto
            {
                hdfValue.Value = result.CodSegment.ToString();
                hdfType.Value = result.CodType.ToString();
                dsIncidentNotification.IN_CAT_MOTIVESDataTable dtMotives = new dsIncidentNotification.IN_CAT_MOTIVESDataTable();
                dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter taMotives = new dsIncidentNotificationTableAdapters.IN_CAT_MOTIVESTableAdapter();
                taMotives.FillByCodMotive(dtMotives, result.CodMotive);

                mvIncidents.ActiveViewIndex = 1;

                //INFORMACION GENERAL
                hdfIn.Value = "1";
                if (lblState.Text.ToString().ToUpper().Equals("FIN"))
                {
                    LoadDropDown();
                }
                else
                {
                    Fill_dropDrowList(true);
                    ddlCountry.SelectedValue = result.IdCountry.ToString();
                    //hdfIn.Value = "1";
                    ddlCountry_SelectedIndexChanged(null, null);
                    //Revisar si la relación del segmento-país está activo
                    statusValue = result.GetStatusCountrySegm(result.IdCountry, result.CodSegment);
                    if (statusValue == 0)
                        LoadDropDown();
                    else
                    {
                        ddlSegment.SelectedValue = result.CodSegment.ToString();
                        Type typeList = new Type();
                        //typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
                        ddlType.Items.Clear();
                        try
                        {
                            typeList.GetTypesByCountry(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue), decimal.Parse(ddlCountry.SelectedValue));
                        }
                        catch
                        {
                            typeList.GetTypes(string.Empty, -1);
                        }
                        ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
                        ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
                        ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
                        if (typeList.TypesTable.Rows.Count > 0)
                        {
                            ddlType.DataBind();
                            ddlType.SelectedValue = result.CodType.ToString();
                        }
                    }

                }
                lblState.Text = dtMotives.Rows[0][1].ToString();
                ListItem ddlItem = ddlMotive.Items.FindByValue("1");
                if (!strMotive.ToUpper().Contains("VALIDA"))
                    ddlMotive.SelectedValue = result.CodMotive.ToString();
                if (ddlItem != null)
                    ddlMotive.Items.Remove(ddlItem);
                tbDescription.Text = result.Description;
                decimal criticalValue = result.GetCritical(codIncidence);
                if (criticalValue == 1)
                    ddlCriticality.SelectedValue = criticalValue.ToString();
                else
                    ddlCriticality.SelectedValue = "3";
                //if (criticalValue == 0 || criticalValue == -1)
                //    ddlCriticality.SelectedValue = "1";
                //else
                //    ddlCriticality.SelectedValue = criticalValue.ToString();

                Level lev = new Level();
                lev.EditLevel(result.CodLevel);
                if (lev.LevelsTable.Rows.Count > 0)
                {
                    //tbLevel.Text = result.CodLevel.ToString();
                    tbLevel.Text = lev.Name;
                    Session["FIRST_COD_LEVEL"] = result.CodLevel;
                    Session["COD_LEVEL"] = result.CodLevel;
                    lbMessage.Visible = false;
                }


                tbMonitoring_rfv.Enabled = true;
                tbMonitoring.Text = result.Monitoring; //string.Empty;
                tbIncidentCause.Text = result.IncidenceCause;

                //Campo FOlIO/OT
                txtFolioOT.Text = result.Folio_OT;

                this.txtScript.Text = result.Script;
                this.txtTypology.Text = result.Typology;

                this.txtSubject.Text = result.Subject;

                if (result.Maintenance != string.Empty)
                    this.cbxMaintenance.Checked = true;
                else
                    this.cbxMaintenance.Checked = false;



                //SERVICIOS AFECTADOS
                AffectedService services = new AffectedService();
                services.GetAffectedServices(codIncidence);

                dtAffectedServices = new DataTable();
                dtAffectedServices.Columns.Add("COD_AFFECTED_SERVICE");
                dtAffectedServices.Columns.Add("AFFECTED_SERVICE");
                dtAffectedServices.Columns.Add("RECEIVED_CALLS");
                foreach (DataRow row in services.ServicesTable.Rows)
                    dtAffectedServices.Rows.Add(row["COD_AFFECTED_SERVICE"], row["AFFECTED_SERVICE"], row["RECEIVED_CALLS"]);
                Session["dtAffectedServices"] = dtAffectedServices;
                gvServices.DataSource = dtAffectedServices;
                gvServices.DataBind();
                cantCall =0;
                for (int i = 0; i < gvServices.Rows.Count; i++)
                {
                    TextBox tbCalls = (TextBox)gvServices.Rows[i].FindControl("tbCalls");
                    tbCalls.Text = dtAffectedServices.Rows[i]["RECEIVED_CALLS"].ToString();
                    cantCall = cantCall + Convert.ToInt32(tbCalls.Text);
                }

                //CLIENTES AFECTADOS
                DataTable dtAffectedClients = new Incidence().GetAffectedClients(codIncidence);

                foreach (DataRow row in dtAffectedClients.Rows)
                {
                    ListItem item = cbxlistAfecctedClients.Items.FindByValue(row["COD_AFFECTED_CLIENT"].ToString());
                    if (item != null)
                        item.Selected = true;
                }

                //RESPONSABLES DE SOLUCION
                DataTable dtSolutionResp = new Incidence().GetSolutionResponsibles(codIncidence);

                foreach (DataRow row in dtSolutionResp.Rows)
                {
                    ListItem item = cbxListResponsibles.Items.FindByValue(row["COD_SOLUTION_RESPONSIBLE"].ToString());
                    if (item != null)
                        item.Selected = true;
                }


                //ADJUNTOS

                CargarAdjuntos();

                //HISTORIAL DE LA INCIDENCIA

                pnlHistory.Visible = true;
                TabContainer.ActiveTabIndex = 0;

                IncidenceHistory history = new IncidenceHistory();
                DataTable dthistory = history.GetIncidenceHistory(codIncidence);
                Session["dthistory"] = dthistory;
                gvIncidents.DataSource = dthistory;
                gvIncidents.DataBind();

                timeLine.CreateTimeline(dthistory);


                if (ddlMotive.SelectedItem.Text == "FIN" || lblState.Text.ToString().ToUpper().Equals("FIN")) //Si la incidencia ha concluido, deshabilitar todos los campos
                {
                    Session["IsEndedInc"] = true;
                    ddlMotive.SelectedItem.Text = "FIN";
                    //Fill_dropDrowList(false);
                    //LoadDropDown();
                    //ddlCriticality.Enabled = false; 
                    ddlSegment.Enabled = false;
                    ddlCountry.Enabled = false;
                    ddlType.Enabled = false;
                    ddlMotive.Enabled = false;
                   // tbDescription.Enabled = false;
                    tbMonitoring.Enabled = true;
                   // tbMonitoring_rfv.Enabled = false;
                    //tbIncidentCause.Enabled = false;
                    tbAddService.Enabled = false;
                    tbCallNumber.Enabled = false;
                    ibtnAddService.Enabled = false;
                    gvServices.Enabled = true;
                    for (int i = 0; i < gvServices.Rows.Count; i++)
                    {
                        ImageButton ibtn = new ImageButton();
                        GridViewRow selectedRow = gvServices.Rows[i];

                        ibtn = (ImageButton)selectedRow.FindControl("ibtnDelete");
                        ibtn.Enabled = false;
                    }

                    txtFile.Enabled = false;
                    FileUpload1.Enabled = false;
                    ibtUpload.Enabled = false;

                    gvAttach.Enabled = true;
                    for (int i = 0; i < gvAttach.Rows.Count; i++)
                    {
                        ImageButton ibtn = new ImageButton();
                        GridViewRow selectedRow = gvAttach.Rows[i];

                        ibtn = (ImageButton)selectedRow.FindControl("ibtnDeleteAttach");
                        ibtn.Enabled = false;

                    }

                        if (ddlSegment.Items.Count < 1)
                            LoadDropDown();

                    cbxlistAfecctedClients.Enabled = false;
                  //  cbxListResponsibles.Enabled = false;
                    btnAccept.Enabled = true;
                }
                else
                {
                    Session["IsEndedInc"] = false;
                    ddlSegment.Enabled = true;
                    ddlType.Enabled = true;
                    ddlMotive.Enabled = true;
                    tbDescription.Enabled = true;
                    tbMonitoring.Enabled = true;
                    tbMonitoring_rfv.Enabled = true;
                    tbIncidentCause.Enabled = true;
                    ibtnAddService.Enabled = true;
                    gvServices.Enabled = true;

                    txtFile.Enabled = true;
                    FileUpload1.Enabled = true;
                    ibtUpload.Enabled = true;
                    gvAttach.Enabled = true;

                    cbxlistAfecctedClients.Enabled = true;
                    cbxListResponsibles.Enabled = true;
                    btnAccept.Enabled = true;
                }
            }

            hdfEdit.Value = "0";
            gvServices.DataBind();
        //gvServices_RowDataBound(null, null);
    }

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
            try
            {
                ddlSegment.SelectedValue = hdfValue.Value; //result.CodSegment.ToString();
            }
            catch
            {
                ddlSegment.DataSource = segmentList.SegmentsTable;
                ddlSegment.DataValueField = segmentList.SegmentsTable.COD_SEGMENTColumn.ColumnName;
                ddlSegment.DataTextField = segmentList.SegmentsTable.SEGMENT_NAMEColumn.ColumnName;
                ddlSegment.DataBind();
                ddlSegment.SelectedValue = hdfValue.Value; //result.CodSegment.ToString();
            }
            Type typeList = new Type();
            ddlType.Items.Clear();
            typeList.GetTypes(string.Empty, Convert.ToDecimal(ddlSegment.SelectedValue));
            ddlType.DataSource = typeList.TypesTable.Select("RECORD_STATUS = 1");
            ddlType.DataValueField = typeList.TypesTable.COD_TYPEColumn.ColumnName;
            ddlType.DataTextField = typeList.TypesTable.TYPE_NAMEColumn.ColumnName;
            if (typeList.TypesTable.Rows.Count > 0)
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
            }
        
        }
        catch (Exception ex)
        {
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.ShowPopup();
        }

    }

    /// <summary>
    /// Cambio de indice en ddlSegment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTypes;

        Type type = new Type();
        dtTypes = type.GetAssignedLevelsTypesCountr(Convert.ToDecimal(ddlSegment.SelectedValue), Convert.ToDecimal(ddlCountry.SelectedValue) );

        if (dtTypes.Rows.Count > 0)
        {
            ddlType.Enabled = true;
            ddlType.DataSource = dtTypes.Select ("RECORD_STATUS = 1");
            ddlType.DataValueField = dtTypes.Columns["COD_TYPE"].ColumnName;
            ddlType.DataTextField = dtTypes.Columns["TYPE_NAME"].ColumnName;
            ddlType.DataBind();
        }
        else
        {
            ddlType.Items.Clear();
            ddlType.DataSource = null;
            ddlType.Enabled = false;
            ddlType.DataBind();
        }

        Level level = new Level();
        DataTable dtLevels = new DataTable();

        if (ddlType.Items.Count > 0)
        {
            lbMessageddlType.Visible = false;
            dtLevels = level.getLessSequence(Convert.ToDecimal(ddlType.SelectedValue));

            if (dtLevels.Rows.Count > 0)
            {
                tbLevel.Text = dtLevels.Rows[0]["LEVEL_NAME"].ToString();
                Session["COD_LEVEL"] = dtLevels.Rows[0]["COD_LEVEL"].ToString();
                lbMessage.Visible = false;
            }
            else
            {
                lbMessage.Visible = true;
                tbLevel.Text = string.Empty;
            }
        }
        else
        {
            tbLevel.Text = string.Empty;
            lbMessageddlType.Visible = true;

            /*wucMessageControl.Title = "Advertencia";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = "No hay tipos para este segmento";
            wucMessageControl.ShowPopup(); */
        }
    }

    /// <summary>
    /// Cambio de indice en ddlType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Level level = new Level();
        DataTable dtLevels = level.getLessSequence(Convert.ToDecimal(ddlType.SelectedValue));
        if (dtLevels.Rows.Count > 0)
        {
            tbLevel.Text = dtLevels.Rows[0]["LEVEL_NAME"].ToString();
            Session["COD_LEVEL"] = dtLevels.Rows[0]["COD_LEVEL"].ToString();
            lbMessage.Visible = false;
        }
        else
        {
            lbMessage.Visible = true;
            tbLevel.Text = string.Empty;
        }
    }

    #endregion
   
    #region "Filtro de Busqueda"

    /// <summary>
    /// Buscar 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchIncident_Click(object sender, EventArgs e)
    {        
        Fill_gvIncidents(tbSeachClient.Text, Convert.ToInt32(ddlCountryFilter.SelectedValue),false);
    }

    /// <summary>
    /// Boton Mostrar Todos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAllIncident_Click(object sender, EventArgs e)
    {        
        Fill_gvIncidents(string.Empty,-1, false);
        tbSeachClient.Text = string.Empty;
        ddlCountryFilter.ClearSelection();
    }

    #endregion

    #region "Botones"

    /// <summary>
    /// Boton Agregar Nueva incidencia
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewIncident_Click(object sender, EventArgs e)
    {
        mvIncidents.ActiveViewIndex = 1;        
        Session["isIncNuew"] = "1";
        ClearFields();
        this.ListaAttachDoc = new List<AttachDocIncident>();
        Load_SecondView();
        hdfEdit.Value = "0";
        Session["isIncNuew"] = "0";
        if (wucMessageControl.Visible == false)
            wucMessageControl.Visible = true;
    }

    /// <summary>
    /// Click al boton Aceptar en la
    /// pantalla de edicion de Incidencias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        hdfCont.Value = "1";
        ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
        GetInformation(action);
        hdfVal.Value = "0";
        if (hdfCont.Value == "1")
        {
            Fill_dropDrowList(false);
            lblState.Text = string.Empty;
        }
    }

    /// <summary>
    /// Boton Cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mvIncidents.ActiveViewIndex = 0;
        hdfLoad.Value = "1";
        Load_Incidents();
        ddlMotive.SelectedIndex = 1;
        FillCbxLists();
        SetValidators();
        Fill_dropDrowList(false);
        lblState.Text = string.Empty;
        if (ddlCountry.Enabled == false)
            ddlCountry.Enabled = true;
        //FillCbxLists();
        //Load_Incidents();
        if (ddlCriticality.Enabled == false)
            ddlCriticality.Enabled = true;
        wucMessageControl.Visible = false;

    }

    #endregion

    #region "Metodos"

    /// <summary>
    /// Carga la vista de Edicion de una 
    /// incidencia para la insercion de una nueva
    /// </summary>
    private void Load_SecondView()
    {
        //Activar todos los campos para la edicion
        ddlSegment.Enabled = true;
        ddlType.Enabled = true;
        ddlMotive.Enabled = true;
        tbDescription.Enabled = true;
        tbMonitoring.Enabled = true;
        tbMonitoring_rfv.Enabled = false;
        tbIncidentCause.Enabled = true;
        tbAddService.Enabled = true;
        tbCallNumber.Enabled = true;
        ibtnAddService.Enabled = true;
        gvServices.Enabled = true;

        txtFile.Enabled = true;
        FileUpload1.Enabled = true;
        ibtUpload.Enabled = true;
        gvAttach.Enabled = true;

        cbxlistAfecctedClients.Enabled = true;
        cbxListResponsibles.Enabled = true;
        btnAccept.Enabled = true;
        document_attachment_GetAll();
        Session["Action"] = ConfigurationTool.Command.Insert;

        //Cargar el nivel mas bajo de la incidencia seleccionada
        Fill_dropDrowList(true);

        //Crear un nuevo dataTable para guardar los servicios afectados
        Session["SERVICES_COUNTER"] = 0;
        dtAffectedServices = new DataTable();
        dtAffectedServices.Columns.Add("COD_AFFECTED_SERVICE");
        dtAffectedServices.Columns.Add("AFFECTED_SERVICE");
        dtAffectedServices.Columns.Add("RECEIVED_CALLS");
        Session["dtAffectedServices"] = dtAffectedServices;
        gvServices.DataSource = dtAffectedServices;
        gvServices.DataBind();

        //Ocultar el panel de Historial
        pnlHistory.Visible = false;
    }

    /// <summary>
    /// Obtiene la informacion ingresadaw
    /// luego invoca a la funcion para insertarla
    /// </summary>
    /// <param name="action">La accion a realizar</param>
    private void GetInformation(ConfigurationTool.Command action)
    {

        if (ddlType.Items.Count > 0)
        {
            if (Convert.ToBoolean(Session["IsEndedInc"])) //Si es una incidencia concluida entonces guardar el total de las llamadas
            {
                Decimal codIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);
                DataTable dtAffectedServices = (DataTable)Session["dtAffectedServices"]; //Tabla que contiene la informacion de los servicios afectados

                AffectedService[] services = new AffectedService[dtAffectedServices.Rows.Count];
                int i = 0;
                Decimal incomingCalls = 0;
                foreach (AffectedService s in services)
                {
                    services[i] = new AffectedService();

                    GridViewRow selectedRow = gvServices.Rows[i];

                    TextBox tbCalls = (TextBox)selectedRow.FindControl("tbCalls");
                    if (!string.IsNullOrEmpty(tbCalls.Text))
                    {
                        incomingCalls += Convert.ToDecimal(tbCalls.Text);
                    }
                    services[i].Name = dtAffectedServices.Rows[i]["AFFECTED_SERVICE"].ToString();

                    if (!string.IsNullOrEmpty(tbCalls.Text))
                    {
                        services[i].ReceivedCalls = Convert.ToDecimal(tbCalls.Text);
                    }
                    else
                    {
                        services[i].ReceivedCalls = 0;
                    }
                    i++;
                }

                Incidence incidence = new Incidence();

                incidence.CodIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);
                incidence.CodLevel = Convert.ToDecimal(Session["COD_LEVEL"]);
                incidence.CodType = Convert.ToDecimal(ddlType.SelectedValue);
                incidence.CodMotive = Convert.ToDecimal(ddlMotive.SelectedValue);
                if (ddlMotive.SelectedItem.Text == "FIN")
                    incidence.EndDate = DateTime.Now;
                incidence.CodSegment = Convert.ToDecimal(ddlSegment.SelectedValue);
                incidence.Description = tbDescription.Text.Replace("'","''");

                incidence.IncidenceCause = tbIncidentCause.Text == string.Empty ? "PENDIENTE" : tbIncidentCause.Text.Replace("'", "''");
                incidence.Critical = decimal.Parse(ddlCriticality.SelectedValue);

                //XOLO S.A 
                //incidence.Monitoring = tbMonitoring.Text == string.Empty ? string.Empty : tbIncidentCause.Text;
                incidence.Monitoring = tbMonitoring.Text.Replace("'", "''"); 
                incidence.StartDate = DateTime.Now;
                incidence.Script = this.txtScript.Text.Replace("'", "''");
                incidence.Typology = this.txtTypology.Text.Replace("'", "''");
                incidence.Subject=this.txtSubject.Text.Replace("'", "''");
                if (cbxMaintenance.Checked == true) 
                    incidence.Maintenance = "S";
                else
                    incidence.Maintenance = string.Empty;

                //Campo Nuevo Folio OT
                incidence.Folio_OT = txtFolioOT.Text.Trim();


                #region Responsables de solucion

                    SolutionResponsible[] SolResp = new SolutionResponsible[cbxListResponsibles.Items.Count];

                    int contRespon = 0;
                    foreach (ListItem item in cbxListResponsibles.Items)
                    {
                        SolResp[contRespon] = new SolutionResponsible();
                        if (item.Selected)
                        {
                            SolResp[contRespon].CodSolutionResponsible = Convert.ToDecimal(item.Value);
                            contRespon++;
                        }
                    }             

                #endregion               

                #region Archivos Adjuntos a Insertar

                attachment[] attachFiles = new attachment[ListaAttachDoc.Count];

                    if (this.ListaAttachDoc.Count != 0)
                    {
                            i = 0;

                            foreach (AttachDocIncident item in ListaAttachDoc)
                            {
                                attachFiles[i] = new attachment();

                                attachFiles[i].DocName = item.AttachName;
                                attachFiles[i].Docbyte = item.DocBinary;
                                attachFiles[i].DocExt = item.Extesion;
                                attachFiles[i].DocMime = item.Mimetype;

                                i++;
                            }

                    }
                #endregion
               

                //bool isEnd = false;
                    if (incidence.SetTotalCalls(services, SolResp, attachFiles, codIncidence, incidence, SafetyPad.GetUserLogin()))  
                    {
                        //lblState.Text = ddlMotive.Text; 
                        //wucMessageControl.Message = "Incidencia Ingresada con éxito";
                        
                        wucMessageControl.Title = "Mensaje";
                        wucMessageControl.Image = "../include/imagenes/info_32.png";
                        mvIncidents.ActiveViewIndex = 0;
                        InsertIncidentAudit();
                        Load_Incidents();

                    }
                    else
                    {
                        wucMessageControl.Title = "Error";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                        hdfCont.Value = "0";
                    }
                wucMessageControl.Message = incidence.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else
            {
                #region Informacion de la incidencia

                bool changeLevel = false;

                Incidence inc = new Incidence(); //Informacion de la incidencia

                inc.CodIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);
                inc.CodLevel = Convert.ToDecimal(Session["COD_LEVEL"]);

                if (inc.CodLevel != Convert.ToDecimal(Session["FIRST_COD_LEVEL"]))
                    changeLevel = true;
                else
                    changeLevel = false;

                inc.CodType = Convert.ToDecimal(ddlType.SelectedValue);
                inc.CodMotive = Convert.ToDecimal(ddlMotive.SelectedValue);
                if (ddlMotive.SelectedItem.Text == "FIN")
                    inc.EndDate = DateTime.Now;
                inc.CodSegment = Convert.ToDecimal(ddlSegment.SelectedValue);
                inc.Description = tbDescription.Text.Replace("'", "''");
                inc.IncidenceCause = tbIncidentCause.Text == string.Empty ? "PENDIENTE" : tbIncidentCause.Text.Replace("'", "''");
                inc.Monitoring = tbMonitoring.Text == string.Empty ? string.Empty : tbIncidentCause.Text;
                inc.Monitoring = tbMonitoring.Text.Replace("'", "''"); ;
                inc.StartDate = DateTime.Now;
                inc.Script = this.txtScript.Text.Replace("'", "''");
                inc.Typology = this.txtTypology.Text.Replace("'", "''"); 
                inc.IdCountry = decimal.Parse(ddlCountry.SelectedValue);
                inc.Critical = decimal.Parse(ddlCriticality.SelectedValue);
                inc.Folio_OT = txtFolioOT.Text.Trim();
                inc.Subject = this.txtSubject.Text.Replace("'", "''");

                if (cbxMaintenance.Checked == true) 
                    inc.Maintenance = "S";
                else
                    inc.Maintenance = string.Empty;

                bool isEnd = false;
                if (ddlMotive.SelectedItem.Text != "FIN")
                {
                    inc.EndDate = null;
                    isEnd = true;
                }
                else
                    inc.EndDate = DateTime.Now;

                #endregion

                #region Servicios afectados

                DataTable dtAffectedServices = (DataTable)Session["dtAffectedServices"]; //Tabla que contiene la informacion de los servicios afectados

                AffectedService[] services = new AffectedService[dtAffectedServices.Rows.Count];
                int i = 0;
                Decimal incomingCalls = 0;
                foreach (AffectedService s in services)
                {
                    services[i] = new AffectedService();

                    GridViewRow selectedRow = gvServices.Rows[i];

                    TextBox tbCalls = (TextBox)selectedRow.FindControl("tbCalls");
                    if (!string.IsNullOrEmpty(tbCalls.Text))
                    {
                        incomingCalls += Convert.ToDecimal(tbCalls.Text);
                    }
                    services[i].Name = dtAffectedServices.Rows[i]["AFFECTED_SERVICE"].ToString();
                    if (!string.IsNullOrEmpty(tbCalls.Text))
                    {
                        services[i].ReceivedCalls = Convert.ToDecimal(tbCalls.Text);
                    }
                    else
                    {
                        services[i].ReceivedCalls = 0;
                    }
                    i++;
                }

                #endregion

                #region Responsables de solucion

                SolutionResponsible[] resp = new SolutionResponsible[cbxListResponsibles.Items.Count];

                i = 0;

                foreach (ListItem item in cbxListResponsibles.Items)
                {
                    resp[i] = new SolutionResponsible();
                    if (item.Selected)
                    {
                        resp[i].CodSolutionResponsible = Convert.ToDecimal(item.Value);
                        i++;
                    }
                }

                #endregion

                #region Clientes afectados

                AffectedClient[] client = new AffectedClient[cbxlistAfecctedClients.Items.Count];

                i = 0;

                foreach (ListItem item in cbxlistAfecctedClients.Items)
                {
                    client[i] = new AffectedClient();
                    if (item.Selected)
                    {
                        client[i].CodAffectedClient = Convert.ToDecimal(item.Value);
                        i++;
                    }
                }

                #endregion

                #region Archivos Adjuntos a Insertar

                attachment[] attachFiles = new attachment[ListaAttachDoc.Count];

                if (this.ListaAttachDoc.Count != 0)
                {
                     i = 0;

                     foreach (AttachDocIncident item in ListaAttachDoc)
                     {
                         attachFiles[i] = new attachment();

                         attachFiles[i].DocName = item.AttachName;
                         attachFiles[i].Docbyte = item.DocBinary;
                         attachFiles[i].DocExt = item.Extesion;
                         attachFiles[i].DocMime = item.Mimetype;

                         i++;
                     }

                }
                #endregion

                #region Realizar la accion solicitada

                if (tbLevel.Text != string.Empty)
                {

                    //if (cbxListResponsibles.DataSource == null)
                    if (hdfValueR.Value == "0")
                    {
                        wucMessageControl.Message = "No Hay Responsables Configurados para el país seleccionado";
                        wucMessageControl.Title = "Mensaje";
                        wucMessageControl.Image = "../include/imagenes/info_32.png";
                        wucMessageControl.ShowPopup();
                        hdfCont.Value = "0";
                    }
                    else
                    {
                        if (cbxlistAfecctedClients.SelectedValue == "-1" || string.IsNullOrEmpty(cbxListResponsibles.SelectedValue))
                        {
                            wucMessageControl.Message = "No Ha Seleccionado Responsable de Solución ";
                            wucMessageControl.Title = "Mensaje";
                            wucMessageControl.Image = "../include/imagenes/info_32.png";
                            wucMessageControl.ShowPopup();
                            hdfCont.Value = "0";
                        }
                        else
                        {
                            if (action == ConfigurationTool.Command.Insert)
                            {
                                //Invocar al metodo para insertar la incidencia
                                if (inc.InsertIncidence(inc, resp, client, services, attachFiles, SafetyPad.GetUserLogin()))
                                {
                                    //lblState.Text = ddlMotive.Text; 
                                    
                                    wucMessageControl.Message = "Incidencia Ingresada con éxito";
                                    wucMessageControl.Title = "Mensaje";
                                    wucMessageControl.Image = "../include/imagenes/info_32.png";

                                    mvIncidents.ActiveViewIndex = 0;
                                    InsertIncidentAudit();
                                    Load_Incidents();
                                }
                                else
                                {
                                    wucMessageControl.Title = "Error";
                                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                                    hdfCont.Value = "0";
                                }
                                wucMessageControl.Message = inc.Messages.Message;
                                wucMessageControl.ShowPopup();
                            }

                            if (action == ConfigurationTool.Command.Update)
                            {
                                //Invocar al metodo para actualizar la incidencia
                                if (inc.UpdateIncidence(inc, resp, client, services, attachFiles, SafetyPad.GetUserLogin(), changeLevel, isEnd))
                                { 
                                    //lblState.Text = ddlMotive.Text;
                                    
                                    ddlCountry.Enabled = true;
                                    wucMessageControl.Message = "Incidencia Actualizada con éxito";
                                    wucMessageControl.Title = "Mensaje";
                                    wucMessageControl.Image = "../include/imagenes/info_32.png";

                                    mvIncidents.ActiveViewIndex = 0;
                                    //UpdateIncidentAudit();
                                    Load_Incidents();
                                }
                                else
                                {
                                    wucMessageControl.Title = "Error";
                                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                                    hdfCont.Value = "0";
                                }

                                wucMessageControl.Message = inc.Messages.Message;
                                wucMessageControl.ShowPopup();

                            }
                        }
                    }
                }
                else
                {
                    wucMessageControl.Message = "No existe un nivel asociado,<br/> no se puede insertar la incidencia";
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                    wucMessageControl.ShowPopup();
                    hdfCont.Value = "0";
                }
                #endregion
            }
        }
        else
        {
            wucMessageControl.Message = "No existe un tipo asociado,<br/> no se puede insertar la incidencia";
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.ShowPopup();
            hdfCont.Value = "0";
        }
    }

    #endregion

    #region  SECCION DE SERVICIOS AFECTADOS

    /// <summary>
    /// Agrega un Servicio afectado al grid 
    /// en la 2da sección
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddService_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(tbAddService.Text))
        {
            DataTable dt = null;
            Int32 counter = 0;
            bool insert = true;
            counter = Convert.ToInt32(Session["SERVICES_COUNTER"]);

            dt = (DataTable)Session["dtAffectedServices"];

            for (int i = 0; i < gvServices.Rows.Count; i++)
            {

                if (((Label)gvServices.Rows[i].FindControl("lblServicio")).Text == dt.Rows[i]["AFFECTED_SERVICE"].ToString())
                {
                    if (!string.IsNullOrEmpty(((TextBox)gvServices.Rows[i].FindControl("tbCalls")).Text))
                    {
                        dt.Rows[i]["RECEIVED_CALLS"] = ((TextBox)gvServices.Rows[i].FindControl("tbCalls")).Text;
                    }
                    else
                        dt.Rows[i]["RECEIVED_CALLS"] = "0";

                }
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row["AFFECTED_SERVICE"].ToString().ToUpper() == tbAddService.Text.ToUpper())
                    insert = false;
            }

            if (insert)
                dt.Rows.Add(0, tbAddService.Text, tbCallNumber.Text);
            else
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "El servicio ya existe para<br />Esta incidencia";
                wucMessageControl.ShowPopup();
            }

            gvServices.DataSource = dt;
            gvServices.DataBind();
            cantCall = 0;
            for (int i = 0; i < gvServices.Rows.Count; i++)
            {
                TextBox tbCalls = (TextBox)gvServices.Rows[i].FindControl("tbCalls");

                if (!string.IsNullOrEmpty((dt.Rows[i]["RECEIVED_CALLS"].ToString())))
                {
                    tbCalls.Text = dt.Rows[i]["RECEIVED_CALLS"].ToString();
                    cantCall += Convert.ToInt32(tbCalls.Text);
                }
                else
                {
                    tbCalls.Text = "0";
                    cantCall += 0;
                }
                Label lblCantTotal = (Label)gvServices.FooterRow.FindControl("lblCantTotal");
                if (lblCantTotal != null)
                {
                    lblCantTotal.Text = cantCall.ToString();
                    gvServices.ShowFooter = true;
                }
            }

            Session["SERVICES_COUNTER"] = counter + 1;
            Session["dtAffectedServices"] = dt;
            tbAddService.Text = string.Empty;
            tbCallNumber.Text = "0";

        }

        else
        {
            int totals = 0;
            for (int i = 0; i < gvServices.Rows.Count; i++)
            {   
                Label lblCantTotal = (Label)gvServices.FooterRow.FindControl("lblCantTotal");
                if (lblCantTotal != null)
                {
                    if (!string.IsNullOrEmpty(((TextBox)gvServices.Rows[i].FindControl("tbCalls")).Text))
                    {
                        totals += Convert.ToInt32(((TextBox)gvServices.Rows[i].FindControl("tbCalls")).Text);
                    }
                    else
                    {
                        ((TextBox)gvServices.Rows[i].FindControl("tbCalls")).Text = "0";
                    }
                    lblCantTotal.Text = totals.ToString();
                    gvServices.ShowFooter = true;
                }
                
            }       
        }
        
    }

    /// <summary>
    /// Elimina una fila de servicios afectados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;

        DataTable dt = (DataTable)Session["dtAffectedServices"];
        dt.Rows.RemoveAt(rowIndex);
        Session["SERVICES_COUNTER"] = gvServices.Rows.Count -1;

        gvServices.DataSource = dt;
        gvServices.DataBind();

        cantCall = 0;
        for (int i = 0; i < gvServices.Rows.Count; i++)
        {
            TextBox tbCalls = (TextBox)gvServices.Rows[i].FindControl("tbCalls");
            tbCalls.Text = dt.Rows[i]["RECEIVED_CALLS"].ToString();
            cantCall += Convert.ToInt32(tbCalls.Text);
            Label lblCantTotal = (Label)gvServices.FooterRow.FindControl("lblCantTotal");
            if (lblCantTotal != null)
            {
                lblCantTotal.Text = cantCall.ToString();
                gvServices.ShowFooter = true;
            }
        }       


    }
    #endregion
  
    #region "Grids"

    #region "Listado de  Incidencias"

    /// <summary>
    /// Click al boton de edicion
    /// de una Incidencia
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditClient_Click(object sender, ImageClickEventArgs e)
    {

        ClearFields();
        this.ListaAttachDoc = new List<AttachDocIncident>();
        Session["Action"] = ConfigurationTool.Command.Update;
        Session["isIncNuew"] = "0";

        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        hdfEdit.Value = "1";
        hdfLoad.Value = "0";
        //mvIncidents.ActiveViewIndex = 1;
        if (wucMessageControl.Visible == false)
            wucMessageControl.Visible = true;
        EditIncident(rowIndex);
    }

    /// <summary>
    /// Paginacion listado de incidencias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvIncidentMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidentMain.PageIndex = e.NewPageIndex;
        Fill_gvIncidents(string.Empty, -1, false);
    }

    /// <summary>
    /// Paginacion listado de incidencias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvIncidents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        gvIncidents.DataSource = Session["dthistory"];
        gvIncidents.DataBind();
    }

    #endregion


    #region "Listado de Servicios"

    /// <summary>
    /// Paginacion listado de servicios
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServices.PageIndex = e.NewPageIndex;
        gvServices.DataSource = Session["dtAffectedServices"];
        gvServices.DataBind();
    }

    /// <summary>
    /// row data bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtcant = (TextBox)e.Row.FindControl("tbCalls");
                txtcant.Attributes.Add("onkeyup", "changeTotal();");                
                ImageButton imb = (ImageButton)e.Row.FindControl("ibtnDelete");
                if (Session["IsEndedInc"] != null) //si tiene la session
                {
                    if (Convert.ToBoolean(Session["IsEndedInc"]))//si es una INCIDENCIA Concluida                    
                        imb.Enabled = false;
                    else
                        imb.Enabled = true;
                }
                else //No tiene la session
                {
                    imb.Enabled = true;
                }
            }            
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblCantTotal = (Label)e.Row.FindControl("lblCantTotal");
                lblCantTotal.Text = cantCall.ToString();
                gvServices.ShowFooter = true;

                for (int i = 0; i < gvServices.Rows.Count; i++)
                {
                    TextBox tbCalls = (TextBox)gvServices.Rows[i].FindControl("tbCalls");
                    tbCalls.Text = dtAffectedServices.Rows[i]["RECEIVED_CALLS"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #endregion

    #region PISTAS DE AUDITORIA

    /// <summary>
    /// Registra las pistas de Auditoria para la insercion de una Incidencia
    /// </summary>
    private void InsertIncidentAudit()
    {
        String[] fieldNames = { "COD_LEVEL", "COD_MOTIVE", "COD_SEGMENT", "COD_TYPE", "DESCRIPTION", "INCIDENCE_CAUSE", "MONITORING", "START_DATE" };
        String[] fieldValues = { Session["COD_LEVEL"].ToString(), ddlSegment.SelectedValue, ddlType.SelectedValue, tbIncidentCause.Text.Replace("'","''"), tbMonitoring.Text.Replace("'", "''"), DateTime.Now.ToShortDateString() };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_INCIDENCES", "C"); 
    }

    /// <summary>
    /// Registra las pistas de Auditoria para la actualizacion de una Incidencia
    /// </summary>
    private void UpdateIncidentAudit()
    {
        String[] fieldNames = { "COD_LEVEL", "COD_MOTIVE", "COD_SEGMENT", "COD_TYPE", "DESCRIPTION", "INCIDENCE_CAUSE", "MONITORING" };
        String[] fieldValues = { Session["COD_LEVEL"].Equals(null) ? "" : Session["COD_LEVEL"].ToString(), ddlMotive.SelectedValue, ddlSegment.SelectedValue, ddlType.SelectedValue, tbDescription.Text.Replace("'", "''"), tbIncidentCause.Text.Replace("'", "''"), tbMonitoring.Text.Replace("'", "''") };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_INCIDENCES", "U");
    }

    #endregion
 
    #region "Combos"
    
    /// <summary>
    /// Combo Filtro pais
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountryFilter_DataBound(object sender, EventArgs e)
    {
        ddlCountryFilter.Items.Insert(0, new ListItem("Seleccione", "-1"));
    }

    /// <summary>
    /// Combo Motivo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMotive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMotive.SelectedItem.Text == "SEGUIMIENTO" || ddlMotive.SelectedItem.Text == "FIN")
        {
            tbMonitoring_rfv.Enabled = true;
        }
        else
            tbMonitoring_rfv.Enabled = false;
    }
   
    /// <summary>
   /// Al cambiar el combo Pais
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdfLoad.Value = "0";
            lbMessage.Visible = false;
            lbMessageddlType.Visible = false;
            Fill_dropDrowList(true);
            FillCbxLists();

        }
        catch { }
    }

   
    /// <summary>
    /// Al llenar combo Pais
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_DataBound(object sender, EventArgs e)
    {
        ddlCountry.Items.Insert(0, new ListItem("Seleccione", "-1"));
    }

    #endregion

    #region "ARCHIVOS ADJUNTOS"
    /// <summary>
    /// Carga Archivos Adjuntos cuando se edita.
    /// </summary>
    private void CargarAdjuntos()
    {
        attachment attachdata = new attachment();
        Decimal codIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);

        attachdata.EditAttach(codIncidence);

        if (attachdata.Messages.Status == 0) //Si hay un error
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = attachdata.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
           foreach (dsIncidentNotification.IN_ATTACHMENTSRow item in attachdata.AttachTable)
            {
                AttachDocIncident attachDoc = new AttachDocIncident();

                attachDoc.AttachName = item.FILE_NAME;
                attachDoc.DocBinary = item.ATTACHMENT_FILE;
                attachDoc.Extesion = item.EXTENSION;
                attachDoc.Mimetype = item.TIPOMIME;

                this.ListaAttachDoc.Add(attachDoc);
            }

            document_attachment_GetAll();
        
        }
    
    }

    /// <summary>
    /// Valida si el nombre del archivo adjunto se repite.
    /// </summary>
    /// <param name="nombreAttach"></param>
    /// <returns></returns>
    private Boolean NombreRepetidoAttach(string nombreAttach)
    {

        Boolean result = false;

        if (ListaAttachDoc.Count > 0)
        { 
            foreach (AttachDocIncident item in ListaAttachDoc)
            {
                if (item.AttachName.Trim() == nombreAttach.Trim())
                {                                   
                    result = true;
                    return result;
                }
                else result = false;
            }
        }

        return result;
    }

    /// <summary>
    /// Carga grid de los datos adjuntos.
    /// </summary>
    private void document_attachment_GetAll()
    {
        if (this.ListaAttachDoc.Count != 0)
        {
           // var carga = (from p in ListaAttachDoc select p).ToList();

            gvAttach.DataSource = ListaAttachDoc.OrderBy(x => x.AttachName).ToList();
            gvAttach.DataBind();
        }
        else 
        {
            gvAttach.DataSource = null;
            gvAttach.DataBind();
        }
    }

    /// <summary>
    /// Convierte bytes a megabytes
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private double ConvertBytesToMegabytes(long bytes)
    {
        return (bytes / 1024f) / 1024f;
    }

/// <summary>
/// Devuelve la sumatoria en MG de los archivos adjuntos.
/// </summary>
/// <param name="SizeFileUpload"></param>
/// <returns></returns>
    private double TotalMgSize(double SizeFileUpload)
    {

        long Total = 0;
        Double TotalMega = 0;

        if (ListaAttachDoc.Count > 0)
        {
            foreach (AttachDocIncident item in ListaAttachDoc)
            {
                Total = Total + item.DocBinary.LongLength;            
            }

            TotalMega = ConvertBytesToMegabytes(Total);
            TotalMega = TotalMega + SizeFileUpload;

            return TotalMega;
        }

        return TotalMega;
    
    }

    /// <summary>
    /// Carga Archivo Adjunto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtUpload_Click(object sender, ImageClickEventArgs e)
    {
        lblMessageAttach.Text = "";

        if (FileUpload1.HasFile)
        {
            string nomArchivo = Path.GetFileName(FileUpload1.PostedFile.FileName).ToLower();
            string extArchivo = Path.GetExtension(nomArchivo);
            string mimeArchivo = GetMimeType(extArchivo);

            long sizeinfobyte = FileUpload1.PostedFile.ContentLength;
            double sizeinfomegabyte = ConvertBytesToMegabytes(sizeinfobyte);
            if (NombreRepetidoAttach(nomArchivo))
            {
                lblMessageAttach.Text = "El nombre del archivo a adjuntar ya existe.";
            }
            else
            {

                Parameters getparameter = new Parameters();
                double parameterSizeAttach = Convert.ToDouble(getparameter.GetValueParameter("MAX_TAMAÑO_ADJUNTO"));
                double SizeAllFile = TotalMgSize(sizeinfomegabyte);
                                
                if (sizeinfomegabyte <= parameterSizeAttach)
                {
                    if (SizeAllFile <= parameterSizeAttach)
                    {
                        using (Stream fs = FileUpload1.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                Byte[] Docbyte = br.ReadBytes((Int32)fs.Length);
                                AttachDocIncident DocIncident = new AttachDocIncident();
                                DocIncident.AttachName = nomArchivo;
                                DocIncident.DocBinary = Docbyte;
                                DocIncident.Extesion = extArchivo;
                                DocIncident.Mimetype = mimeArchivo;

                                this.ListaAttachDoc.Add(DocIncident);
                                txtFile.Text = "Click Aquí...";
                                Session.Remove("FileUpload1");
                            }
                        }
                    }
                    else
                    {
                        lblMessageAttach.Text = "Cantidad de Carga de los archivos es mayor a " + parameterSizeAttach + "MB";
                    }
                }
                else
                {
                    lblMessageAttach.Text = "Tamaño de Archivo es mayor a " + parameterSizeAttach + "MB";
                }
            }
        }
        else 
        {
            lblMessageAttach.Text = "Seleccione el archivo que desea Cargar.";
        }

        document_attachment_GetAll();

    }

    /// <summary>
    /// Descarga Archivo Adjunto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton img = (ImageButton)sender;
            string DocNombre="";
            byte[] bytefile;
            DocNombre = img.CommandArgument;

            if (DocNombre != null)
            {
                var consulta = (from p in ListaAttachDoc where p.AttachName.ToLower() == DocNombre.ToLower() select p).SingleOrDefault();

                bytefile = (byte[])consulta.DocBinary;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + DocNombre);
                Response.BinaryWrite(bytefile);
                Response.Flush();                          
            }

            document_attachment_GetAll();
            
        }
        catch (Exception ex)
        {
            lblMessageAttach.Text = "Error al Descargar Archivo.";
        }
        finally
        {
            Response.End();
        }

    }

    /// <summary>
    /// Elimina Archivo Adjunto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnDeleteAttach_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessageAttach.Text = "";
            ImageButton img = (ImageButton)sender;
            string DocNombre = "";
            DocNombre = img.CommandArgument.ToString();
            Decimal codIncidence = Convert.ToDecimal(Session["COD_INCIDENCE"]);
            AttachDocIncident docattach = new AttachDocIncident();
            if (codIncidence == 0)
            {
                for (int i = 0; i < ListaAttachDoc.Count; i++)
                {
                    if (ListaAttachDoc[i].AttachName.ToLower().Contains(DocNombre.ToLower()))
                    {
                        ListaAttachDoc.RemoveAt(i);
                        i--;
                    }
                }
                document_attachment_GetAll();

            }
            else
            {
                attachment BorrarAttach = new attachment();

                BorrarAttach.DeleteAttach(codIncidence, DocNombre);

                if (ListaAttachDoc.Count > 0)
                {
                    var deletefile = (from p in ListaAttachDoc where p.AttachName == DocNombre select p).SingleOrDefault();
                    ListaAttachDoc.Remove(deletefile);
                }

                if (BorrarAttach.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = BorrarAttach.Messages.Message;
                wucMessageControl.ShowPopup();

                document_attachment_GetAll();

            }

        }
        catch (Exception ex)
        {
            lblMessageAttach.Text = "Error al Eliminar Archivo.";
        }

    }

    /// <summary>
    /// Captura el tipo mime del archivo a adjuntar
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string GetMimeType(string extension)
    {
        if (extension == null)
            throw new ArgumentNullException("extension");

        if (extension.StartsWith("."))
            extension = extension.Substring(1);


        switch (extension.ToLower())
        {
            #region Big freaking list of mime types

            case "7z": return "application/x-7z-compressed";
            case "bmp": return "image/bmp";            
            case "config": return "application/xml";            
            case "csv": return "text/csv";            
            case "doc": return "application/msword";            
            case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";            
            case "gif": return "image/gif";
            case "gz": return "application/x-gzip";
            case "htm": return "text/html";
            case "html": return "text/html";            
            case "ico": return "image/x-icon";            
            case "jpe": return "image/jpeg";
            case "jpeg": return "image/jpeg";
            case "jpg": return "image/jpeg";          
            case "pic": return "image/pict";
            case "rar": return "application/octet-stream";
            case "tex": return "application/x-tex";
            case "txt": return "text/plain";
            case "tgz": return "application/x-compressed";
            case "xlm": return "application/vnd.ms-excel";
            case "xls": return "application/vnd.ms-excel";            
            case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";           
            case "z": return "application/x-compress";
            case "zip": return "application/x-zip-compressed";
            #endregion
            default: return "application/octet-stream";
        }
    }

    #endregion 

}