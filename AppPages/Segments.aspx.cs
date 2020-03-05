using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data;
using System.Drawing;

public partial class AppPages_Segments : System.Web.UI.Page
{

    #region METODOS DE CARGA DE LA PAGINA
    //Crear un diccionario con la lista de correos
    Dictionary<Decimal, String> dicEmails = new Dictionary<decimal, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (hfColor.Value.Length > 0 )
        {
            // pnlColor.Style["background-color"] = "#"+hfColor.Value;
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", " document.getElementById(\"ctl00_Body_ctl00_TabPanel1_pnlColor\").style.color = \"#" + hfColor.Value + "\"", true);
            tbLevelColor.Text = hfColor.Value;
            Session["COLOR"] = false;
        }

        if (!IsPostBack)
        {
            mvSegments.ActiveViewIndex = 0;
            SetSegmentsValidatorFields();
            LoadSegmentsStates();
            SetUserPermissions();
            Load_Segments();
            //Load_Levels(); //--> Quitar
        }
    }

    /// <summary>
    /// Administra los permisos del
    /// usuario en sesion
    /// </summary>
    private void SetUserPermissions()
    {
        btnSearchSegment.Enabled = SafetyPad.IsAllowed("Buscar");
        btnShowAllSegment.Enabled = SafetyPad.IsAllowed("MostrarTodos");
        btnSaveSegment.Enabled = SafetyPad.IsAllowed("Guardar");
        gvSegment.Visible = SafetyPad.IsAllowed("Consultar");
        btnNewSegment.Enabled = SafetyPad.IsAllowed("Agregar");
        gvSegment.Enabled = SafetyPad.IsAllowed("Agregar");
    }

    /// <summary>
    /// Agrega el tipo de validacion para los campos
    /// requeridos
    /// </summary>
    private void SetSegmentsValidatorFields()
    {
        tbSegmentName.SetCustomValidator("1234567890qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM.,-ÁÉÍÓÚáéíóú-+*/¿?{}{}.$!¡|%#;=:ñÑüÜ\"'_()& ");
        tbSegmentDescription.SetCustomValidator("1234567890qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM.-ÁÉÍÓÚáéíóú-+*/¿?{}{}.$!¡|%#;=:ñÑüÜ\"'_()&, ");
        
        //tbSegmentName.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
        //tbSegmentDescription.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
    }


    /// <summary>
    /// Carga los Estados en ddlStates
    /// </summary>
    private void LoadSegmentsStates()
    {
        DataTable dt = new DataTable("STATES");
        dt.Columns.Add("RECORD_STATUS");
        dt.Columns.Add("VALUE");

        dt.Rows.Add("Activo", "1");
        dt.Rows.Add("Inactivo", "0");

        ddlStatusSegment.DataSource = dt;
        ddlStatusSegment.DataTextField = dt.Columns["RECORD_STATUS"].ColumnName;
        ddlStatusSegment.DataValueField = dt.Columns["VALUE"].ColumnName;
        ddlStatusSegment.DataBind();
    }

    /// <summary>
    /// Limpia todos los campos del Formulario
    /// </summary>
    private void ClearSegmentsFields()
    {
        //Campos en la vista de Segmentos
        tbSegmentDescription.Text = string.Empty;
        tbSegmentName.Text = string.Empty;
        tbSearchSegment.Text = string.Empty;
        ddlStatusSegment.SelectedIndex = 0;

        //Campos en la vista de Niveles
        tbLevelColor.Text = string.Empty;
        tbLevelDescription.Text = string.Empty;
        tbLevelName.Text = string.Empty;
        tbLevelWaitTime.Text = string.Empty;
        //ddlLevelState.SelectedIndex = 0;
        tbNewMail.Text = string.Empty;
        listMails.Items.Clear();
        lblMessageSegm.Text = string.Empty;

        //Limpiando Países
        ckbCountries.ClearSelection();
        

    }

    #endregion

    #region SEGMENTOS


    /// <summary>
    /// Carga la información de los Segmentos 
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha: 28.Sep.2011
    /// </summary>
    /// <param name="searchFilter"></param>
    private void Load_Segments()
    {
        mvSegments.ActiveViewIndex = 0;
        pnlLevelCountry.Visible = false;
        pnlTypes.Visible = false;
        TitleSubtitle1.SetTitle("Administración de Segmentos");

        fill_gvSegment(string.Empty, false);
    }

    /// <summary>
    /// Carga la información de los Segmentos 
    /// en gvSegments con un filtro de busqueda
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha: 28.Sep.2011
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void fill_gvSegment(string searchFilter, bool showErrorMessage)
    {
        Segment result = new Segment();
        result.GetSegments(searchFilter);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.SegmentsTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvSegment.DataSource = result.SegmentsTable;
        gvSegment.DataBind();

        //Cargar la imagen del estado correspondiente
        setSegmentImage();
    }

    /// <summary>
    /// Coloca la imagen del Estado de un Segmento en 
    /// gvSegment
    /// </summary>
    private void setSegmentImage()
    {
        string status = string.Empty;

        for (int i = 0; i < gvSegment.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvSegment.Rows[i];

            status = gvSegment.DataKeys[i]["RECORD_STATUS"].ToString();

            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

        }
    }

    /// <summary>
    /// Click al boton Nuevo, en la vista de segmentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewSegment_Click(object sender, EventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Insert;
        lbTitleSegment.Text = "SEGMENTO";
        ClearSegmentsFields();
        btnNewSegment_ModalPopupExtender.Enabled = true;
        btnNewSegment_ModalPopupExtender.Show();
      
	
    }

    /// <summary>
    /// Click al boton de Edicion de Segmentos en 
    /// gvSegment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditSegment_Click(object sender, ImageClickEventArgs e)
    {
        lblMessageSegm.Text = string.Empty;
        lbTitleSegment.Text = "SEGMENTO";
        Session["Action"] = ConfigurationTool.Command.Update;
        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;

        mvSegments.ActiveViewIndex = 0;
        EditSegment(rowIndex, ConfigurationTool.hierarchy.Segment);
        tbSegmentName.Focus();
    }

    /// <summary>
    /// Click al boton Guardar en 
    /// el panel de Segmentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveSegment_Click(object sender, EventArgs e)
    {
       bool mantPop = false;
        if (Page.IsValid)
        {
            lblMessageSegm.Text = string.Empty;
            ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];

            if (action == ConfigurationTool.Command.Insert)
            {

                //Revisando que se haya seleccionado por lo menos un país para el segmento
                if (ckbCountries.SelectedIndex != -1 && ckbCountries.SelectedIndex != null)
                {
                    if (tbSegmentName.Text.ToString().Length == 0 || tbSegmentDescription.Text.ToString().Length == 0)
                    {
                        lblMessageSegm.Text = "Todos los datos son requeridos";
                        mantPop = true;
                    }
                    else
                        Insert(ConfigurationTool.hierarchy.Segment);
                }
                else
                {

                    lblMessageSegm.Text = "Seleccione por lo menos un país";
                     mantPop = true;
                    
                }
            }
            else
            {
                if (tbSegmentName.Text.ToString().Length == 0 || tbSegmentDescription.Text.ToString().Length == 0)
                { lblMessageSegm.Text = "Todos los datos son requeridos";
                mantPop = true;
                }
                else
                    Update(ConfigurationTool.hierarchy.Segment, ref mantPop);
            }
        }
        if (mantPop)
        {
            btnNewSegment_ModalPopupExtender.Enabled = true;
            btnNewSegment_ModalPopupExtender.Show();
            //ClearSegmentsFields();
        }
        else
            btnNewSegment_ModalPopupExtender.Enabled = false;
    }

    /// <summary>
    /// Click al boton cancelar
    /// en el panel de Segmentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelSegment_Click(object sender, EventArgs e)
    {
        btnNewSegment_ModalPopupExtender.Enabled = false;
        mvSegments.ActiveViewIndex = 0;
    }

    /// <summary>
    /// Click al boton de Agregar Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddSegment_Click(object sender, ImageClickEventArgs e)
    {
        //Obtner la fila donde se activo el evento
       /* GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;
        rowIndex = gvRow.RowIndex;
        
        //Agregando para llamar el nivel y no al tipo

        Session["COD_SEGMENT"] = gvSegment.DataKeys[rowIndex]["COD_SEGMENT"];
        Session["strSegment"] = gvSegment.DataKeys[rowIndex]["SEGMENT_NAME"];

        
        Load_Types();
        

       // ibtnAddType_Click( sender,  e);

        /*

        
        Session["COD_SEGMENT"] = gvSegment.DataKeys[rowIndex]["COD_SEGMENT"];
        Session["strSegment"] = gvSegment.DataKeys[rowIndex]["SEGMENT_NAME"];

        SetTypesValidatorFields();
        LoadTypesStates();
        Load_Types();*/

        //Agregando por pruebas

        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;


        rowIndex = gvRow.RowIndex;
        Session["COD_SEGMENT"] = gvSegment.DataKeys[rowIndex]["COD_SEGMENT"];
        Session["strSegment"] = gvSegment.DataKeys[rowIndex]["SEGMENT_NAME"];

        SetTypesValidatorFields();
        LoadTypesStates();
        Load_Types();
    }


    #endregion

    #region TIPOS

    /// <summary>
    /// Carga los Tipos asociados a un determinado 
    /// segmento
    /// </summary>
    private void Load_Types()
    {
        mvSegments.ActiveViewIndex = 1;
        TitleSubtitle1.SetTitle("Administración Tipos");
        

        fill_gvTypes(string.Empty, false);
    }

    /// <summary>
    /// Carga la informacion de los países 
    /// en gvTypes con un filtro de busqueda
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 18.Oct.2011
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void fill_gvTypes(string searchFilter, bool showErrorMessage)
    {
       /* Decimal codSegment = Convert.ToDecimal(Session["COD_SEGMENT"]); //Segmento al cual estan asociados los tipos

        /*Type result = new Type();
        result.GetTypes(searchFilter, codSegment);
        */
       /* Level result = new Level();
        DataTable dtCountries = new DataTable();
        dtCountries = result.GetCountriesBySegment(decimal.Parse(Session["COD_SEGMENT"].ToString()));

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }*/
        /*else
        {
            /*if (result.TypesTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }*/
        /*}*/

       /* grvCountries.DataSource = dtCountries;
        grvCountries.DataBind();
        */
        //gvTypes.DataSource = result.TypesTable;

       // gvTypes.DataSource = dtCountries; //result.GetCountriesBySegment(decimal.Parse(Session["COD_SEGMENT"].ToString())); ;
        //gvTypes.DataBind();

        //Cargar la imagen del estado correspondiente
        //setTypeImage();
        lblSegmentSelect.Text = Session["strSegment"].ToString();
        Decimal codSegment = Convert.ToDecimal(Session["COD_SEGMENT"]); //Segmento al cual estan asociados los tipos

        Type result = new Type();
        result.GetTypes(searchFilter, codSegment);

        Level resultCou = new Level();
        DataTable dtCountries = new DataTable();
        dtCountries = resultCou.GetCountriesBySegment(decimal.Parse(Session["COD_SEGMENT"].ToString()));


      if (resultCou.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = resultCou.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.Messages.Status == 0)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = result.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else
            {
                if (result.TypesTable.Count <= 0 && showErrorMessage == true)
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                    wucMessageControl.Message = "No se encontraron datos";
                    wucMessageControl.ShowPopup();
                }
            }
        }


        /*gvTypes.DataSource = result.TypesTable;
        gvTypes.DataBind();
        */
        grvCountries.DataSource = dtCountries;
        grvCountries.DataBind();

        //Cargar la imagen del estado correspondiente
        //setTypeImage();

    }


    /// <summary>
    /// Carga la informacion de los Tipos según el país seleccionado 
    /// en gvTypes con un filtro de busqueda
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 18.Oct.2011
    /// Modificado en fecha: 04/07/2014
    /// Xolo
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void fill_grvTypes(string searchFilter, bool showErrorMessage)
    {
        

        Decimal codSegment = Convert.ToDecimal(Session["COD_SEGMENT"]); //Segmento al cual estan asociados los tipos
        decimal idCountr = Convert.ToDecimal(Session["ID_COUNTRY"]); //Identificador del país al cual estan asociados los tipos

        Type result = new Type();
        result.GetTypesByCountry(searchFilter, codSegment, idCountr);

        //Level resultCou = new Level();
        //DataTable dtCountries = new DataTable();
        //dtCountries = resultCou.GetCountriesBySegment(decimal.Parse(Session["COD_SEGMENT"].ToString()));


        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.TypesTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvTypes.DataSource = result.TypesTable;
        gvTypes.DataBind();
        
        //grvCountries.DataSource = dtCountries;
        //grvCountries.DataBind();

        //Cargar la imagen del estado correspondiente
        setTypeImage();

    }


    /// <summary>
    /// Coloca la imagen del Estado de un Tipo en 
    /// gvTypes
    /// </summary>
    private void setTypeImage()
    {
        mvSegments.ActiveViewIndex = 1;
        string status = string.Empty;

        for (int i = 0; i < gvTypes.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvTypes.Rows[i];

            status =  gvTypes.DataKeys[i]["RECORD_STATUS"].ToString();

            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

        }
    }

    /// <summary>
    /// Click al boton de Supervisores en la vista de Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSegment_Click(object sender, EventArgs e)
    {
        mvSegments.ActiveViewIndex = 0;
        Load_Segments();
    }

    /// <summary>
    /// Click al boton Nuevo, en la vista de Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewType_Click(object sender, EventArgs e)
    {
        ClearFieldsTypes();
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearFieldsTypes();
        ClearSegmentsFields();
btnNewType_ModalPopupExtender.Enabled = true;
        btnNewType_ModalPopupExtender.Show();

    }

    /// <summary>
    /// Limpia todos los campos del Formulario
    /// de tipos
    /// </summary>
    private void ClearFieldsTypes()
    {
        tbTypeDescription.Text = string.Empty;
        tbTypeName.Text = string.Empty;
        //tbSearchType.Text = string.Empty;
        lbTitleType.Text = "TIPOS";
    }

    /// <summary>
    /// Agrega el tipo de validacion para los campos
    /// requeridos
    /// </summary>
    private void SetTypesValidatorFields()
    {
	
        tbTypeName.SetCustomValidator("1234567890qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM.,-ÁÉÍÓÚáéíóú-+*/¿?{}{}.$!¡|%#;=:ñÑüÜ\"'_()& ");
        tbTypeDescription.SetCustomValidator("1234567890qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM.,-ÁÉÍÓÚáéíóú-+*/¿?{}{}.$!¡|%#;=:ñÑüÜ\"'_()& ");

        //tbTypeName.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
        //tbTypeDescription.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
    }


    /// <summary>
    /// Carga los Estados en ddlStates
    /// </summary>
    private void LoadTypesStates()
    {
        DataTable dt = new DataTable("STATES");
        dt.Columns.Add("RECORD_STATUS");
        dt.Columns.Add("VALUE");

        dt.Rows.Add("Activo", "1");
        dt.Rows.Add("Inactivo", "0");

        ddlStatusType.DataSource = dt;
        ddlStatusType.DataTextField = dt.Columns["RECORD_STATUS"].ColumnName;
        ddlStatusType.DataValueField = dt.Columns["VALUE"].ColumnName;
        ddlStatusType.DataBind();

        ddlLevelState.DataSource = dt;
        ddlLevelState.DataTextField = dt.Columns["RECORD_STATUS"].ColumnName;
        ddlLevelState.DataValueField = dt.Columns["VALUE"].ColumnName;
        ddlLevelState.DataBind();
    }

    /// <summary>
    /// Click al boton de Edicion en el grid de Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditType_Click(object sender, ImageClickEventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Update;
        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        mvSegments.ActiveViewIndex = 1;

        EditSegment(rowIndex, ConfigurationTool.hierarchy.Type);
    }


    /// <summary>
    /// Click al boton para añadir niveles a los Tipos
    /// en gvTypes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddType_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        Session["COD_TYPE"] = gvTypes.DataKeys[rowIndex]["COD_TYPE"];
        Session["strType"] = gvTypes.DataKeys[rowIndex]["TYPE_NAME"];
        LoadTypesStates();
        Load_Levels();

        mvSegments.ActiveViewIndex = 2;

    }

    /// <summary>
    /// Click al boton guardar en la vista de
    /// tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveType_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool mantPop = false;
			lblMessageTy.Text = string.Empty;
            ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
            if (tbTypeName.Text.ToString().Length == 0 || tbTypeDescription.Text.ToString().Length == 0)
            {
                mantPop = true;
                lblMessageTy.Text = "Nombre y Descripción son requeridos";
            }
            else
            {
                if (action == ConfigurationTool.Command.Insert)
                    Insert(ConfigurationTool.hierarchy.Type);
                else
                    Update(ConfigurationTool.hierarchy.Type, ref mantPop);
            }

            if (mantPop)
            {
                btnNewType_ModalPopupExtender.Enabled = true;
                btnNewType_ModalPopupExtender.Show();
            }
            else
                btnNewType_ModalPopupExtender.Enabled = false;
           
            //btnNewType_ModalPopupExtender.Enabled = false;
        }
    }

    /// <summary>
    /// click al boton cancelar en la 
    /// vista de tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelType_Click(object sender, EventArgs e)
    {
        //mvSegments.ActiveViewIndex = 0;
		lblMessageTy.Text = string.Empty;
        btnNewType_ModalPopupExtender.Enabled = false;
    }



    #endregion

    #region METODOS PARA  INSERTAR Y ACTUALIZAR

    /// <summary>
    /// Carga la informacion de un Segmento,Tipo o Nivel y la
    /// coloca en el panel de Edición
    /// </summary>
    /// <param name="rowIndex">indice de la fila en el grid</param>
    /// <param name="rol">rol a editar</param>
    private void EditSegment(int rowIndex, ConfigurationTool.hierarchy rol)
    {
        //Leer la informacion y Colocarla

        if (rol == ConfigurationTool.hierarchy.Segment)
        {
            //Obtener el código del segmento
            Session["COD_SEGMENT"] = gvSegment.DataKeys[rowIndex]["COD_SEGMENT"].ToString();
            
            
            //Cargar la información 
            Segment result = new Segment();
            result.EditSegment(Convert.ToDecimal(Session["COD_SEGMENT"]));

            if (result.Messages.Status == 0) //Si hay un error
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = result.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else //Si el dato es correcto
            {
                tbSegmentName.Text = result.Name;
                tbSegmentDescription.Text = result.Description;
                ddlStatusSegment.SelectedValue = gvSegment.DataKeys[rowIndex]["RECORD_STATUS"].ToString();
                ckbCountries.ClearSelection();
                if (!string.IsNullOrEmpty(result.Countries))
                {
                    string[] countryIds = result.Countries.Split('|');

                    //foreach (DataRow dr in dt.Rows)
                    //{//por cada item en el checklist
                    foreach (ListItem lItem in ckbCountries.Items)
                    {//si se tiene registrado el tipo de consulta correspondiente al item, lo seleccionamos
                        foreach (var item in countryIds)
                        {
                            if (lItem.Value == item.ToString())
                            {
                                lItem.Selected = true;
                                break;
                            }
                        }
                    }
                }
                //}

                btnNewSegment_ModalPopupExtender.Enabled = true;
                btnNewSegment_ModalPopupExtender.Show();
            }
        }

        if (rol == ConfigurationTool.hierarchy.Type)
        {
            //Obtener el código del tipo
            Session["COD_TYPE"] = gvTypes.DataKeys[rowIndex]["COD_TYPE"].ToString();
                
            //Cargar la información
            Type result = new Type();
            result.EditType(Convert.ToDecimal(Session["COD_TYPE"]));

            if (result.Messages.Status == 0) //Si hay un error
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = result.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else //Si el dato es correcto
            {
                tbTypeName.Text = result.Name;
                tbTypeDescription.Text = result.Description;
                ddlStatusType.SelectedValue = gvTypes.DataKeys[rowIndex]["RECORD_STATUS"].ToString();
                lbTitleType.Text = "TIPOS";
               btnNewType_ModalPopupExtender.Enabled = true;
               btnNewType_ModalPopupExtender.Show();
            }
        }

        if (rol == ConfigurationTool.hierarchy.Level)
        {
            //Obtener el código del nivel
            Session["COD_LEVEL"] = gvLevels.DataKeys[rowIndex]["COD_LEVEL"].ToString();
            Session["SELECTED_COLOR"] = gvLevels.DataKeys[rowIndex]["LEVEL_COLOR"].ToString();

            Level result = new Level();
            result.EditLevel(Convert.ToDecimal(Session["COD_LEVEL"]));

            if (result.Messages.Status == 0) //Si hay un error
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = result.Messages.Message;
                wucMessageControl.ShowPopup();
            }
            else //Si el dato es correcto
            {
                //Cargar la información que no es escrita x el usuario
                Level level = new Level();
                
                tbLevelName.Text = result.Name;
                tbLevelDescription.Text = result.Description;
                tbSequence.Text = result.Sequence.ToString();
                tbLevelWaitTime.Text = result.WaitTime.ToString();
                tbLevelColor.Text = result.Color;
                hfColor.Value = result.Color;
                ddlLevelState.SelectedValue = result.Status;
                //lbColor
                dicEmails.Clear();
                Session["dtEmails"] = result.LevelEmailsTable; 
                Session["EmailsList"] = (Dictionary<Decimal, String>)dicEmails;

                dicEmails.Clear();

                foreach (DataRow row in result.LevelEmailsTable)
                {
                    dicEmails.Add(Convert.ToDecimal(row["COD_LEVEL_EMAIL"]), row["EMAIL"].ToString());
                }
                Session["EmailsList"] = dicEmails;

                listMails.DataSource = result.LevelEmailsTable;
                listMails.DataValueField = result.LevelEmailsTable.COD_LEVEL_EMAILColumn.ColumnName;
                listMails.DataTextField = result.LevelEmailsTable.EMAILColumn.ColumnName;
                listMails.DataBind();

                //gvLevels.Rows[i].Cells[4].Style["background-color"] = String.Format("#{0:X}", 0xFFFFFF & Color.Tan.ToArgb());
                //gvLevels.Rows[i].Cells[4].Style["background-color"] = "#"+ color;
            }

        }



    }

    /// <summary>
    /// Inserta un Nuevo Segmento 
    /// o tipo
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha:  18.Oct.2011
    /// </summary>
    /// <param name="hierarchy">Nivel en la jerarquia de Segmentos</param>
    private void Insert(ConfigurationTool.hierarchy hierarchy)
    {
        if (hierarchy == ConfigurationTool.hierarchy.Segment) //Insertar un Segmento
        {
            Segment newSegment = new Segment();
            newSegment.Name = tbSegmentName.Text;
            newSegment.Description = tbSegmentDescription.Text;
            newSegment.Status = ddlStatusSegment.SelectedValue;

            //Verificando los países seleccionados
            string countries = string.Empty;

            int counter = 0;

            foreach (ListItem item in ckbCountries.Items)
            {
                if (item.Selected) //Verificando si el item está seleccionado
                {
                    counter += 1;
                    //Agregando a la Cadena
                    if (counter == 1)
                     countries = item.Value.ToString();
                    else
                        countries = countries + "|" + item.Value.ToString();
                }

            }
            newSegment.Countries = countries; 

            try
            {
                newSegment.InsertSegment(newSegment);

                if (newSegment.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = newSegment.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                InsertSegmentAudit(ConfigurationTool.hierarchy.Segment);

                btnNewSegment_ModalPopupExtender.Enabled = false;

                Load_Segments();
            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString();
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("AffectedClients.aspx.cs", ex.ToString());
            }
        }

        if (hierarchy == ConfigurationTool.hierarchy.Type) //Insertar un Tipo
        {
            Type newType = new Type();
            newType.Name = tbTypeName.Text;
            newType.Description = tbTypeDescription.Text;
            newType.Status = ddlStatusType.SelectedValue;
            newType.CodSegment = Convert.ToDecimal(Session["COD_SEGMENT"]);
            newType.IdCountry = Convert.ToDecimal(Session["ID_COUNTRY"]);

            try
            {
                newType.InsertType(newType);

                if (newType.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = newType.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                InsertSegmentAudit(ConfigurationTool.hierarchy.Type);
                ClearFieldsTypes();

                //btnNewType_ModalPopupExtender.Enabled = false;

                //Load_Types();
                fill_grvTypes(string.Empty, false);
            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString();
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("AffectedClients.aspx.cs", ex.ToString());
            }
        }
    }


   

    /// <summary>
    /// Actualiza los datos de un Segmento 
    /// o tipo
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha:  18.Oct.2011
    /// </summary>
    /// <param name="hierarchy">Nivel en la jerarquia de Segmentos</param>
    private void Update(ConfigurationTool.hierarchy hierarchy, ref bool mantPop)
    {
        if (hierarchy == ConfigurationTool.hierarchy.Segment) //Actualizar un Segmento
        {

            Segment updSegment = new Segment();
            updSegment.Name = tbSegmentName.Text;
            updSegment.Description = tbSegmentDescription.Text;
            updSegment.Status = ddlStatusSegment.SelectedValue;

            try
            {
                
                Decimal codSegment = Convert.ToDecimal(Session["COD_SEGMENT"]);
                if (updSegment.GetUnfinishedIncidents(codSegment) > 0 && ddlStatusSegment.SelectedValue=="0")
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                    wucMessageControl.Message = "El segmento no puede ser dado de baja</br>Aún hay incidencias Activas para el";
                    wucMessageControl.ShowPopup();
                }
                else
                {

                    if (ckbCountries.SelectedIndex != -1 && ckbCountries.SelectedIndex != null)
                    {
                        //Verificando los países seleccionados
                        string countries = string.Empty;

                        int counter = 0;

                        foreach (ListItem item in ckbCountries.Items)
                        {
                            if (item.Selected) //Verificando si el item está seleccionado
                            {
                                counter += 1;
                                //Agregando a la Cadena
                                if (counter == 1)
                                    countries = item.Value.ToString();
                                else
                                    countries = countries + "|" + item.Value.ToString();
                            }

                        }
                        updSegment.Countries = countries; 


                        updSegment.UpdateSegment(updSegment, codSegment);

                        if (updSegment.Messages.Status == 1)
                        {
                            wucMessageControl.Title = "Mensaje";
                            wucMessageControl.Image = "../include/imagenes/info_32.png";
                        }
                        else
                        {
                            wucMessageControl.Title = "Error";
                            wucMessageControl.Image = "../include/imagenes/error_32.png";
                        }

                        wucMessageControl.Message = updSegment.Messages.Message;
                        wucMessageControl.ShowPopup();
                        //Registrar las pistas de Auditoria 
                        UpdateSegmentAudit(ConfigurationTool.hierarchy.Segment);

                        btnNewSegment_ModalPopupExtender.Enabled = false;

                        Load_Segments();
                    }
                    else
                    {
                        lblMessageSegm.Text = "Seleccione por lo menos un país";
                        mantPop = true;
                    }


                }
            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString();
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("AffectedClients.aspx.cs", ex.ToString());
            }
        }

        if (hierarchy == ConfigurationTool.hierarchy.Type) //Actualizar un Tipo
        {
            Type updType = new Type();
            updType.Name = tbTypeName.Text;
            updType.Description = tbTypeDescription.Text;
            updType.Status = ddlStatusType.SelectedValue;

            try
            {
                Decimal codType = Convert.ToDecimal(Session["COD_TYPE"]);
                if (updType.GetUnfinishedIncidents(codType) > 0 && ddlStatusType.SelectedValue == "0")
                {

                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                    wucMessageControl.Message = "El Tipo no puede ser dado de baja</br>Aún hay incidencias Activas para él";
                    wucMessageControl.ShowPopup();
                }
                else
                {
                    updType.UpdateType(updType, codType);

                    if (updType.Messages.Status == 1)
                    {
                        wucMessageControl.Title = "Mensaje";
                        wucMessageControl.Image = "../include/imagenes/info_32.png";
                    }
                    else
                    {
                        wucMessageControl.Title = "Error";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                    }

                    wucMessageControl.Message = updType.Messages.Message;
                    wucMessageControl.ShowPopup();
                    //Registrar las pistas de Auditoria 
                    UpdateSegmentAudit(ConfigurationTool.hierarchy.Type);

                    //btnNewType_ModalPopupExtender.Enabled = false;

                    //Load_Types();
                    fill_grvTypes(string.Empty, false);
                    ClearFieldsTypes();
                    
                }
            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString();
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("AffectedClients.aspx.cs", ex.ToString());
            }
        }
    }

   





    #endregion

    #region BUSQUEDA

    /// <summary>
    /// Realiza la busqueda de Segmentos con el filtro insertado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchSegment_Click(object sender, EventArgs e)
    {
        fill_gvSegment(tbSearchSegment.Text, true);
    }

    /// <summary>
    /// Muestra todos los Segmentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAllSegment_Click(object sender, EventArgs e)
    {
        fill_gvSegment(string.Empty, true);
        tbSearchSegment.Text = string.Empty;
    }

    /// <summary>
    /// Busqueda de Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchType_Click(object sender, EventArgs e)
    {
        fill_grvTypes(tbSearchType.Text, true);
    }

    /// <summary>
    /// Muestra todos los Tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAllTypes_Click(object sender, EventArgs e)
    {
        fill_grvTypes(string.Empty, true);
        tbSearchType.Text = string.Empty;
    }


    /// <summary>
    /// Busqueda de Niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchLevel_Click(object sender, EventArgs e)
    {
        fill_gvLevels(tbSearchLevel.Text, true);
    }

    /// <summary>
    /// Muestra Todos los Niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAllLevels_Click(object sender, EventArgs e)
    {
        fill_gvLevels(string.Empty, true);
        tbSearchLevel.Text = string.Empty;
    }
    #endregion

    #region NIVELES

    /// <summary>
    /// Carga la informacion de los niveles de un tipo determinado
    /// </summary>
    private void Load_Levels()
    {
        //mvSegments.ActiveViewIndex = 1;
        pnlLevelCountry.Visible = true;
        TitleSubtitle1.SetTitle("Administración de Niveles");
        SetLevelValidators();
        fill_gvLevels(string.Empty, false);
        lblCountryText.Text = Session["strCountry"].ToString() + " - " + Session["strType"].ToString();
    }

    private void SetLevelValidators()
    {
        tbNewMail.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Email);
        tbEditEmail.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Email);
        tbLevelWaitTime.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Integer);
    }

    /// <summary>
    /// Carga la informacion de los Niveles
    /// en gvLevels con un filtro de busqueda
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 20.Oct.2011
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void fill_gvLevels(string searchFilter, bool showErrorMessage)
    {
        //Obtener el codigo del tipo al que estan asociados los niveles
        Decimal codType = Convert.ToDecimal(Session["COD_TYPE"]);

        //Obteniendo Código de Segmento
        Decimal codSegment = Convert.ToDecimal(Session["COD_SEGMENT"]);
        decimal idCountry = Convert.ToDecimal(Session["ID_COUNTRY"]);
        //Decimal codType = 3;
        Level result = new Level();
        //result.GetLevels(searchFilter, codSegment, idCountry);
        result.GetLevels(searchFilter, codType, idCountry);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            Session["dtLevels"] = result.LevelsTable;
            if (result.LevelsTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvLevels.DataSource = result.LevelsTable;
        gvLevels.DataBind();

        //Cargar la imagen del estado correspondiente
        setLevelImage();
    }


    /// <summary>
    /// Coloca la imagen del estado de un nivel en
    /// gvLevels
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha:20.Oct.2011
    /// </summary>
    private void setLevelImage()
    {
        string status = string.Empty;
        string color = string.Empty;

        for (int i = 0; i < gvLevels.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvLevels.Rows[i];

            status = gvLevels.DataKeys[i]["RECORD_STATUS"].ToString();//Obtener el estado de sincronización del panel


            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

            color = gvLevels.DataKeys[i]["LEVEL_COLOR"].ToString();
            //string s = "<div style = \"background-color:#" + color + "\">&nbsp;</div>";

            Panel pnl = (Panel)selectedRow.FindControl("pnlColor");
            //pnl.Attributes["style"] = "\"background-color:#" + color + "\"";
            pnl.BackColor = Color.FromArgb(Convert.ToInt32(color, 16));
            //pnl.Controls.Add(new LiteralControl(s));
            //pnl.Style["background-color"] = "#" + color;
            //gvLevels.Rows[i].Cells[4].Style["background-color"] = String.Format("#{0:X}", 0xFFFFFF & Color.Tan.ToArgb());
            //gvLevels.Rows[i].Cells[4].Style["background-color"] = "#"+ color;

        }

    }

    /// <summary>
    /// Click al boton de Tipos para regresar
    /// en la vista de Niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTypes_Click(object sender, EventArgs e)
    {
        //mvSegments.ActiveViewIndex = 1;
        pnlLevelCountry.Visible = false;
        Load_Types();
		fill_grvTypes(string.Empty, false);
    }

    /// <summary>
    /// click al boton nuevo en la vista de niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewLevel_Click(object sender, EventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearSegmentsFields();
        mvSegments.ActiveViewIndex = 3;
        TitleSubtitle1.SetTitle("Administración de Niveles");
        tbLevelColor_ColorPickerExtender.Enabled = true;
        //Cargar la información que no es escrita x el usuario
        Level level = new Level();
        //tbSequence.Text = level.GetSequence(Convert.ToDecimal(Session["COD_SEGMENT"].ToString()), Convert.ToDecimal(Session["ID_COUNTRY"])).ToString();
        tbSequence.Text = level.GetSequence(Convert.ToDecimal(Session["COD_TYPE"].ToString()), Convert.ToDecimal(Session["ID_COUNTRY"])).ToString();
        tbLevelName.Text = "Nivel " + tbSequence.Text;
        tbLevelDescription.Text = Session["strSegment"].ToString() + "-" + Session["strCountry"].ToString() + "-" + Session["strType"].ToString() + "-" + "Nivel " + tbSequence.Text;

    }


    /// <summary>
    /// Click al boton de Edición en el el grid de Niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditLevel_Click(object sender, ImageClickEventArgs e)
    {
        Session["COLOR"] = true;
        Session["Action"] = ConfigurationTool.Command.Update;
        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        tbLevelColor_ColorPickerExtender.Enabled = true;

        mvSegments.ActiveViewIndex = 3;
        TitleSubtitle1.SetTitle("Administración de Niveles");

        rowIndex = gvRow.RowIndex;
        EditSegment(rowIndex, ConfigurationTool.hierarchy.Level);
    }

    /// <summary>
    /// Click al boton cancelar en la vista de niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelLevelEdit_Click(object sender, EventArgs e)
    {
        mvSegments.ActiveViewIndex = 2;
        Load_Levels();
    }

    #endregion

    #region PANEL DE CORREOS

    /// <summary>
    /// Click al botón de añadir un
    /// correo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddMail_Click(object sender, ImageClickEventArgs e)
    {
        if (tbNewMail.Text.Length > 0)
        {
            Session["COLOR"] = true;
            ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
            Dictionary<Decimal, String> emailsList = new Dictionary<decimal,string>();
            if(action== ConfigurationTool.Command.Update)
               emailsList = (Dictionary<Decimal, String>)Session["EmailsList"];
            bool exist = false; //Determina si el correo ya existe en la lista
            //Revisar que el email no exista en la lista
            if (emailsList.Count > 0)
            {
                foreach (String email in emailsList.Values)
                {
                    if (email == tbNewMail.Text)
                        exist = true;
                }
            }
            
            //Ni en la lista temporal
            for (int i = 0; i < listMails.Items.Count; i++)
            {
                string email = listMails.Items[i].Text;
                if (email == tbNewMail.Text)
                    exist = true;
            }

            if (exist) //Mostrar mensaje de error
            {
                wucMessageControl.Title = "Advertencia";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "El correo ya existe en la lista";
                wucMessageControl.ShowPopup();
            }
            else
            {
                listMails.Items.Add(tbNewMail.Text);
                tbNewMail.Text = string.Empty;
            }
        }
    }

    /// <summary>
    /// Click al botón de edición de un correo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditMail_Click(object sender, ImageClickEventArgs e)
    {
        Session["COLOR"] = true;
        if (listMails.Items.Count > 0 && listMails.SelectedIndex >= 0)
        {
            ibtnEditMail_ModalPopupExtender.Enabled = true;
            ibtnEditMail_ModalPopupExtender.Show();

            Session["COD_LEVEL_EMAIL"] = listMails.SelectedItem.Value;
            Session["Email"] = listMails.SelectedItem.Text;
            Session["EmailIndex"] = listMails.SelectedIndex;

            tbEditEmail.Text = listMails.SelectedItem.Text;
        }

        else if (listMails.SelectedIndex < 0)
        {
            wucMessageControl.Title = "Advertencia";
            wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
            wucMessageControl.Message = "Seleccione un elemento";
            wucMessageControl.ShowPopup();
        }

        else
        {
            wucMessageControl.Title = "Advertencia";
            wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
            wucMessageControl.Message = "No hay datos";
            wucMessageControl.ShowPopup();
        }
    }

    /// <summary>
    /// click al botón de eliminar correo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnDeleteMail_Click(object sender, ImageClickEventArgs e)
    {
        Session["COLOR"] = true;
        if (listMails.Items.Count > 0 && listMails.SelectedIndex >= 0)
        {
            Dictionary<Decimal, String> emailsList = (Dictionary<Decimal, String>)Session["EmailsList"];
            //Borrar el elemento de la lista, sin importar si era o no 
            //de la lista inicial
            //Session["EmailIndex"] = listMails.SelectedIndex;

            Decimal cod_level_email;

            try//Si estaba en el diccionario, era un numero y se puede convertir
            {
                cod_level_email = Decimal.Parse(listMails.SelectedItem.Value);

                emailsList.Remove(cod_level_email);
                listMails.Items.RemoveAt(listMails.SelectedIndex);
            }
            catch //Si no estaba en el diccionario
            {
                listMails.Items.RemoveAt(listMails.SelectedIndex);
            }
            Session["EmailsList"] = emailsList;
        }
        else if (listMails.SelectedIndex < 0)
        {
            wucMessageControl.Title = "Advertencia";
            wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
            wucMessageControl.Message = "Seleccione un elemento";
            wucMessageControl.ShowPopup();
        }
        else
        {
            wucMessageControl.Title = "Advertencia";
            wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
            wucMessageControl.Message = "No hay datos";
            wucMessageControl.ShowPopup();
        }


        ibtnEditMail_ModalPopupExtender.Enabled = false;
    }

    /// <summary>
    /// click al boton de aceptar la 
    /// edicion de un correo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAcceptEdit_Click(object sender, EventArgs e)
    {
        if (tbEditEmail.Text.Length > 0)
        {

            int index = (Int32)Session["EmailIndex"];
            string code = Session["COD_LEVEL_EMAIL"].ToString();
            string text = tbEditEmail.Text;

            Dictionary<Decimal, String> emailsList = (Dictionary<Decimal, String>)Session["EmailsList"];
            //Revisar si el correo esta en el diccionario, si es asi contiene un codigo numerico
            try 
            {
                emailsList[Convert.ToDecimal(Session["COD_LEVEL_EMAIL"])] = tbEditEmail.Text;

                listMails.Items[index].Value = code;
                listMails.Items[index].Text = text;
            }
            catch //Si falla, es por que la llave en la lista es texto, por tanto actualizar directamente en la lista
            {
                listMails.Items[listMails.SelectedIndex].Value = code;
                listMails.Items[listMails.SelectedIndex].Text = text; 
            }
            DataBind();
            //Session["EmailIndex"] = listMails.SelectedIndex;
            tbEditEmail.Text = string.Empty;
        }
        ibtnEditMail_ModalPopupExtender.Enabled = false;
    }

    /// <summary>
    /// click al botón cancelar en la edicion de 
    /// correos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelEdit_Click(object sender, EventArgs e)
    {
        ibtnEditMail_ModalPopupExtender.Enabled = false;
        tbEditEmail.Text = string.Empty;
    }


    #endregion

    /// <summary>
    /// Click al boton Guardar en la vista de niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveLevel_Click(object sender, EventArgs e)
    {
        ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
		wucMessageControl.Message = string.Empty;

        if (action == ConfigurationTool.Command.Insert)
            InsertLevel();
        else
            UpdateLevel();
    }


    /// <summary>
    /// Inserta un Nuevo Nivel
    /// </summary>
    private void InsertLevel()
    {
        DataTable dtLevels = (DataTable)Session["dtLevels"];
        bool canInsert = true;
        Decimal codLevel=0;

        Level newLevel = new Level();
        newLevel.CodType = Convert.ToDecimal(Session["COD_TYPE"]);
        //newLevel.Color = tbLevelColor.Text;
        newLevel.Color = hfColor.Value;
        newLevel.Description = tbLevelDescription.Text;
        newLevel.Status = ddlLevelState.SelectedValue;
        newLevel.Name = tbLevelName.Text;
        newLevel.WaitTime = Convert.ToDecimal(tbLevelWaitTime.Text);
        newLevel.CodCountry = Convert.ToDecimal(Session["ID_COUNTRY"].ToString());
        newLevel.CodSegm = Convert.ToDecimal(Session["COD_SEGMENT"].ToString());

        //Comprobar si el color no esta repetido
        if (dtLevels.Rows.Count > 0)
        {
            foreach (DataRow row in dtLevels.Rows)
            {
                if (row["LEVEL_COLOR"].ToString() == tbLevelColor.Text)
                    canInsert = false;
            }
        }

        if (canInsert)
        {

            try
            {
                codLevel = newLevel.InsertLevel(newLevel);

                if (newLevel.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                //tbLevelColor_ColorPickerExtender.Enabled = false;

                wucMessageControl.Message = newLevel.Messages.Message;
                wucMessageControl.ShowPopup();
               
                //Registrar las pistas de Auditoria 
                InsertSegmentAudit(ConfigurationTool.hierarchy.Level);
                if (newLevel.Messages.Status == 1)
                {
                    Session["COD_LEVEL"] = codLevel;
                    ManageEmailList();
                    Load_Levels();
					Session["Action"] = ConfigurationTool.Command.Update;
                    Session["SELECTED_COLOR"] = hfColor.Value;
					
					Level result = new Level();
                    result.EditLevel(Convert.ToDecimal(Session["COD_LEVEL"]));

                    if (result.Messages.Status == 0) //Si hay un error
                    {
                        wucMessageControl.Title = "Error";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                        wucMessageControl.Message = result.Messages.Message;
                        wucMessageControl.ShowPopup();
                    }
                    else //Si el dato es correcto
                    {
                        //Cargar la información que no es escrita x el usuario
                        Level level = new Level();

                        tbLevelName.Text = result.Name;
                        tbLevelDescription.Text = result.Description;
                        tbSequence.Text = result.Sequence.ToString();
                        tbLevelWaitTime.Text = result.WaitTime.ToString();
                        tbLevelColor.Text = result.Color;
                        ddlLevelState.SelectedValue = result.Status;
                        dicEmails.Clear();
                        Session["dtEmails"] = result.LevelEmailsTable;
                        Session["EmailsList"] = (Dictionary<Decimal, String>)dicEmails;

                        dicEmails.Clear();

                        foreach (DataRow row in result.LevelEmailsTable)
                        {
                            dicEmails.Add(Convert.ToDecimal(row["COD_LEVEL_EMAIL"]), row["EMAIL"].ToString());
                        }
                        Session["EmailsList"] = dicEmails;

                        listMails.DataSource = result.LevelEmailsTable;
                        listMails.DataValueField = result.LevelEmailsTable.COD_LEVEL_EMAILColumn.ColumnName;
                        listMails.DataTextField = result.LevelEmailsTable.EMAILColumn.ColumnName;
                        listMails.DataBind();
                                               
                    }
                }

            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString() + "-::::-" + ex.StackTrace.ToString() ;
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("Segments.aspx.cs", ex.ToString());
            }
        }
        else
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = "Ese color ya esta siendo usado, intente con otro";
            wucMessageControl.ShowPopup();
        }

    }

    /// <summary>
    /// Actualiza la informacion de 
    /// un nivel
    /// </summary>
    private void UpdateLevel()
    {
        string selectedColor = Session["SELECTED_COLOR"].ToString();
        DataTable dtLevels = (DataTable)Session["dtLevels"];
        bool canInsert = true;

        Level updLevel = new Level();
        updLevel.Color = tbLevelColor.Text;
        updLevel.Status = ddlLevelState.SelectedValue;
        updLevel.WaitTime = Convert.ToDecimal(tbLevelWaitTime.Text);

        //Comprobar si el color no esta repetido
        foreach (DataRow row in dtLevels.Rows)
        {
            if (row["LEVEL_COLOR"].ToString() == tbLevelColor.Text && tbLevelColor.Text != selectedColor)
                canInsert = false;
        }

        if (canInsert)
        {
            try
            {
                updLevel.UpdateLevel(updLevel, Convert.ToDecimal(Session["COD_LEVEL"]));

                if (updLevel.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                //tbLevelColor_ColorPickerExtender.Enabled = false;
                wucMessageControl.Message = updLevel.Messages.Message;
                wucMessageControl.ShowPopup();
                
                //Registrar las pistas de Auditoria 
                UpdateSegmentAudit(ConfigurationTool.hierarchy.Level);
                if (updLevel.Messages.Status == 1)
                {
                    ManageEmailList();
                    Load_Levels(); 
                }
                    

            }
            catch (Exception ex)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = ex.Message.ToString() + "-::::-" + ex.StackTrace.ToString() ;
                wucMessageControl.ShowPopup();

                SafetyPad.SetLogRecord("Segments.aspx.cs", ex.ToString());
            }
        }
        else
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = "Ese color ya esta siendo usado, intente con otro";
            wucMessageControl.ShowPopup();
        }
    }

    /// <summary>
    /// Realiza las acciones de insertar, actualizar y eliminar 
    /// correos de la lista asociada a un nivel
    /// </summary>
    private void ManageEmailList()
    {
        ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
        Decimal codLevel = Convert.ToDecimal(Session["COD_LEVEL"]);

        //La lista de Correos es nueva, insertar todos los datos ingresados
        if (action == ConfigurationTool.Command.Insert)
        {
            int counter = listMails.Items.Count;

            foreach (ListItem item in listMails.Items)
            {
                Email email = new Email();
                email.InsertEmail(item.Text, codLevel);

                if (email.Messages.Status == 1)
                    counter--;
            }

            /*if (counter == 0)
            {
                wucMessageControl.Title = "Mensaje";
                wucMessageControl.Image = "../include/imagenes/info_32.png";
                wucMessageControl.Message = "Lista de correos ingresada con éxito";
            }
            else
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "Lista de correos ingresada con errores";
            }*/

            //wucMessageControl.ShowPopup();
        }

        //Si la acción es actualizar, verificar los elementos que existen en la BD
        //y si no estan en la interfaz eliminarlos o modificarlos
        if (action == ConfigurationTool.Command.Update)
        {
            Email email = new Email();
            DataTable dtEmails = (DataTable)Session["dtEmails"]; //Tabla con los correos originales

            Dictionary<Decimal, String> emailsList = (Dictionary<Decimal, String>)Session["EmailsList"]; //Lista con los correos originales, menos los eliminados
            Decimal i;
            String mail;

            Decimal[] deleteCodes = new Decimal[dtEmails.Rows.Count];
            foreach (DataRow dr in dtEmails.Rows)
            {
                i = Convert.ToDecimal(dr["COD_LEVEL_EMAIL"]);

                if (emailsList.Keys.Contains(i))
                {
                    email.UpdateEmail(emailsList[i].ToString(), i);
                }
                else
                {
                    email.DeleteEmail(i);
                }
            }

            //Ingresar los Correos que no estaban en la lista inicial
            for (int c = 0; c < listMails.Items.Count; c++)
            {
                mail = listMails.Items[c].Text;
                if (!emailsList.Values.Contains(mail))
                {
                    email.InsertEmail(mail, Convert.ToDecimal(Session["COD_LEVEL"]));
                }
            }
        }


    }


    #region PISTAS DE AUDITORIA

    private void InsertSegmentAudit(ConfigurationTool.hierarchy rol)
    {
        if (rol == ConfigurationTool.hierarchy.Segment)
        {
            String[] fieldNames = { "SEGMENT_NAME", "DESCRIPTION", "RECORD_STATUS" };
            String[] fieldValues = { tbSegmentName.Text, tbSegmentDescription.Text, ddlStatusSegment.SelectedValue };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_SEGMENTS", "C");
        }

        if (rol == ConfigurationTool.hierarchy.Type)
        {

            String[] fieldNames = { "COD_SEGMENT", "TYPE_NAME", "DESCRIPTION", "RECORD_STATUS", "RECORD_DATE" };
            String[] fieldValues = { Session["COD_SEGMENT"].ToString(), tbTypeName.Text, tbTypeDescription.Text, ddlStatusType.SelectedValue, DateTime.Now.ToString() };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_TYPES", "C");
        }

        if (rol == ConfigurationTool.hierarchy.Level)
        {
            decimal sessionCodType = 0;
            if (Session["COD_TYPE"] != null)
                sessionCodType = decimal.Parse(Session["COD_TYPE"].ToString());
            
            String[] fieldNames = { "COD_TYPE", "LEVEL_NAME", "DESCRIPTION", "WAIT_TIME", "LEVEL_COLOR", "RECORD_STATUS", "RECORD_DATE" };
            String[] fieldValues = { sessionCodType.ToString(), tbLevelName.Text, tbLevelDescription.Text, tbLevelWaitTime.Text, tbLevelColor.Text, ddlLevelState.SelectedValue, DateTime.Now.ToString() };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_LEVELS", "C");
        }
    }

    private void UpdateSegmentAudit(ConfigurationTool.hierarchy rol)
    {
        if (rol == ConfigurationTool.hierarchy.Segment)
        {
            String[] fieldNames = { "SEGMENT_NAME", "DESCRIPTION", "RECORD_STATUS" };
            String[] fieldValues = { tbSegmentName.Text, tbSegmentDescription.Text, ddlStatusSegment.SelectedValue };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_SEGMENTS", "U");
        }
        if (rol == ConfigurationTool.hierarchy.Type)
        {

            String[] fieldNames = { "COD_SEGMENT", "TYPE_NAME", "DESCRIPTION", "RECORD_STATUS", "RECORD_DATE" };
            String[] fieldValues = { Session["COD_SEGMENT"].ToString(), tbTypeName.Text, tbTypeDescription.Text, ddlStatusType.SelectedValue, DateTime.Now.ToString() };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_TYPES", "U");
        }
        if (rol == ConfigurationTool.hierarchy.Level)
        {
            decimal sessionCodType = 0;
            if (Session["COD_TYPE"] != null)
                sessionCodType = decimal.Parse(Session["COD_TYPE"].ToString());
            String[] fieldNames = { "COD_TYPE", "LEVEL_NAME", "DESCRIPTION", "WAIT_TIME", "LEVEL_COLOR", "RECORD_STATUS", "RECORD_DATE" };
            String[] fieldValues = { sessionCodType.ToString(), tbLevelName.Text, tbLevelDescription.Text, tbLevelWaitTime.Text, tbLevelColor.Text, ddlLevelState.SelectedValue, DateTime.Now.ToString() };

            SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_LEVELS", "U");
        }
    }

    #endregion

    /// <summary>
    /// Cambio de pagina en el grid de segmentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSegment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSegment.PageIndex = e.NewPageIndex;
        fill_gvSegment(tbSearchSegment.Text, false);
    }


    /// <summary>
    /// Cambio de pagina en el grid de tipos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTypes.PageIndex = e.NewPageIndex;
        fill_grvTypes(tbSearchType.Text, false);
    }

    /// <summary>
    /// Cambio de pagina en el grid de Niveles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLevels_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLevels.PageIndex = e.NewPageIndex;
        fill_gvLevels(tbSearchLevel.Text, false);
    }


    protected void imbAddLevel_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        /*Session["COD_TYPE"] = gvTypes.DataKeys[rowIndex]["COD_TYPE"];*/
        Session["strCountry"] = grvCountries.DataKeys[rowIndex]["NAME_COUNTRY"];
        Session["ID_COUNTRY"] = grvCountries.DataKeys[rowIndex]["IN_COUNTRY_PK"];
        //LoadTypesStates();
        //Load_Levels();

        fill_grvTypes(string.Empty, false);

        //mvSegments.ActiveViewIndex = 1;
        //pnlLevelCountry.Visible = true;
        pnlTypes.Visible = true;
        //pnlTitleLevel.GroupingText = Session["strCountry"].ToString();
        lblTextCountry.Text = Session["strCountry"].ToString(); ;
        lblCountryText.Text = Session["strCountry"].ToString();
    }
    protected void btnCanType_Click(object sender, EventArgs e)
    {
        pnlTypes.Visible = false;
    }
}
