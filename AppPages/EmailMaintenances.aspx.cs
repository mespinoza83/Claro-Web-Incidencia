using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;


public partial class AppPages_EmailMaintenances : System.Web.UI.Page
{

    #region METODOS DE CARGA DE LA PAGINA
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setValidatorFiels();
            ClearFields();
            Load_ddlValues();
            Load_SolutionResponsibles();
            ddlCountries.DataSourceID = odsCountries.ID;
            odsCountries.DataBind();
            ddlCountries.DataBind();
            SetUserPermissions();            
        }
    }

    /// <summary>
    /// Metodo para establecer las acciones
    /// del usuario en sesion
    /// </summary>
    private void SetUserPermissions()
    {

        btnSearchEmail.Enabled = SafetyPad.IsAllowed("Buscar");
        btnShowAllEmail.Enabled = SafetyPad.IsAllowed("MostrarTodos");
        btnSave.Enabled = SafetyPad.IsAllowed("Guardar");
        gvEmail.Visible = SafetyPad.IsAllowed("Consultar");
        btnNewEmail.Enabled = SafetyPad.IsAllowed("Agregar");   
       
    }


    private void Load_ddlValues()
    {
        DataTable dtStatus = new DataTable();
        dtStatus.Columns.Add("RECORD_STATUS");
        dtStatus.Columns.Add("VALUE");

        dtStatus.Rows.Add("Activo", "1");
        dtStatus.Rows.Add("Inactivo", "0");

        ddlStatus.DataSource = dtStatus;
        ddlStatus.DataTextField = dtStatus.Columns["RECORD_STATUS"].ColumnName;
        ddlStatus.DataValueField = dtStatus.Columns["VALUE"].ColumnName;
        ddlStatus.DataBind();
    }

    /// <summary>
    /// Coloca las validaciones en los campos requeridos
    /// </summary>
    private void setValidatorFiels()
    {        
        tbEmail.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Email);  
    }

    /// <summary>
    /// Carga de los Datos de los Correos de Mantenimiento
    /// </summary>
    private void Load_SolutionResponsibles()
    {

        mvEmailMaintenance.ActiveViewIndex = 0;
        TitleSubtitle1.SetTitle("RESPONSABLES DE SOLUCIÓN");        
        Fill_gvEmail(string.Empty, false);
    }

    /// <summary>
    /// Limpia el contenido de los campos en el 
    /// formulario
    /// </summary>
    private void ClearFields()
    {
        tbEmail.Text = string.Empty;        
        ddlStatus.SelectedIndex = 0;
        lblMessageSegm.Text = string.Empty;
        //ddlCountries.SelectedValue = "-1";
        ddlCountries.DataSourceID = odsCountries.ID;
        odsCountries.DataBind();
        ddlCountries.DataBind();
    }

    /// <summary>
    /// Evento de cambio de pagina en 
    /// gvEmail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResponsibles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        gvEmail.PageIndex = e.NewPageIndex;
        Fill_gvEmail(tbSeachEmail.Text, true);
    }

    protected void ddlCountries_DataBound(object sender, EventArgs e)
    {
        ddlCountries.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
    }


    #endregion

    #region CORREO MANTENIMIENTO

    /// <summary>
    /// Llena el grid con la informacion de los Correos
    /// de mantenimiento
    /// </summary>
    /// <param name="searchFilter">filtro de Busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvEmail(string searchFilter, bool showErrorMessage)
    {
        EmailMaintenance result = new EmailMaintenance();
        result.GetEmailMaintenance(searchFilter);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.EmailMaintenanceTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvEmail.DataSource = result.EmailMaintenanceTable;
        gvEmail.DataBind();

        setStatusImage();
    }

    /// <summary>
    /// Coloca la imagen correspondiente al estado en 
    /// gvEmail
    /// </summary>
    private void setStatusImage()
    {
        string status = string.Empty;

        for (int i = 0; i < gvEmail.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvEmail.Rows[i];

            status = gvEmail.DataKeys[i]["RECORD_STATUS"].ToString();

            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

        }
    }


    /// <summary>
    /// Click al boton de Edicion en el grid de
    /// Correo de Mantenimiento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditEmail_Click(object sender, ImageClickEventArgs e)
    {
        lblMessageSegm.Text = string.Empty;
        Session["Action"] = ConfigurationTool.Command.Update;

        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        
        mvEmailMaintenance.ActiveViewIndex = 0;
        EditEmailMaintenance(rowIndex);
        Utilities.CreateConfirmBox(btnSave, "Esta seguro de realizar cambio?");
    }


    /// <summary>
    /// Click al boton Nuevo Correo de Mantenimiento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnNewEmail_Click(object sender, EventArgs e)
    {
        lblMessageSegm.Text = string.Empty;
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearFields();
        btnSave.Attributes.Remove("onclick");
        btnNewEmail_ModalPopupExtender.Enabled = true;
        btnNewEmail_ModalPopupExtender.Show();
    }


    /// <summary>
    /// Carga la información de un Correo de Mantenimiento para su edición
    /// </summary>
    /// <param name="rowIndex">fila seleccionada</param>
    private void EditEmailMaintenance(int rowIndex)
    {
        //Guardar el Código del Responsable a editar
        Session["COD_EMAIL_MAINTENANCE"] = gvEmail.DataKeys[rowIndex]["COD_EMAIL_MAINTENANCE"].ToString();

        //Cargar la información del responsable
        EmailMaintenance result = new EmailMaintenance();
        result.EditEmailMaintenance(Convert.ToDecimal(Session["COD_EMAIL_MAINTENANCE"]));

        if (result.Messages.Status == 0) //Si hay un error
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else //Si el dato es correcto
        {
            tbEmail.Text = result.Email;
            hdfEmail.Value = result.Email;
            ddlStatus.SelectedValue = gvEmail.DataKeys[rowIndex]["RECORD_STATUS"].ToString();

            try
            {
                ddlCountries.SelectedValue = result.IdCountryR.ToString();
            }
            catch
            {
                ddlCountries.DataSourceID = odsAllCountries.ID;
                odsAllCountries.DataBind();
                ddlCountries.DataBind();
                try
                {
                    ddlCountries.SelectedValue = "4";
                }
                catch
                {
                    ddlCountries.SelectedValue = "1";
                }
            }
            
            btnNewEmail_ModalPopupExtender.Enabled = true;
            btnNewEmail_ModalPopupExtender.Show();
        }
    }


    /// <summary>
    /// Click al boton cancelar en el Panel PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        mvEmailMaintenance.ActiveViewIndex = 0;
        btnNewEmail_ModalPopupExtender.Enabled = false;
        ddlCountries.DataSourceID = odsCountries.ID;
        odsCountries.DataBind();
        ddlCountries.DataBind();
    }

    /// <summary>
    /// Click al boton guardar en el panel PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
         
            lblMessageSegm.Text = string.Empty;
            EmailMaintenance ExistEmail = new EmailMaintenance();
            if (Page.IsValid)
            {
                if (ddlCountries.SelectedValue != "-1")
                {
                    if (tbEmail.Text.ToString().Length == 0)
                    {
                        lblMessageSegm.Text = "Todos los datos son requeridos";
                        if (tbEmail.Text.ToString().Length == 0)
                            lblMessageSegm.Text = lblMessageSegm.Text + ", incluya email";
                        btnNewEmail_ModalPopupExtender.Enabled = true;
                        btnNewEmail_ModalPopupExtender.Show();
                    }
                    else if (ExistEmail.ExistEmailCountry(tbEmail.Text.Trim(), decimal.Parse(ddlCountries.SelectedValue)) > 0 && hdfEmail.Value != tbEmail.Text)
                    {
                        lblMessageSegm.Text = "País y correo ya existen";
                        btnNewEmail_ModalPopupExtender.Enabled = true;
                        btnNewEmail_ModalPopupExtender.Show();
                    }
                    else
                    {
                        ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
                        if (action == ConfigurationTool.Command.Insert)
                            Insert();
                        else
                            Update();
                        ddlCountries.DataSourceID = odsCountries.ID;
                        odsCountries.DataBind();
                        ddlCountries.DataBind();
                    }

                }
                else
                {
                    lblMessageSegm.Text = "Seleccione un país";
                    btnNewEmail_ModalPopupExtender.Enabled = true;
                    btnNewEmail_ModalPopupExtender.Show();

                }
            }
        
    }

    #endregion

    #region METODOS DE INSERTAR Y ACTUALIZAR

    /// <summary>
    /// Inserta un nuevo Correo de mantenimiento
    /// Autor: Xolo
    /// Fecha: 06.Agost.2015
    /// </summary>
    private void Insert()
    {
        EmailMaintenance newEmail= new EmailMaintenance();
        newEmail.Email = tbEmail.Text;
        newEmail.Status = ddlStatus.SelectedValue;
        newEmail.IdCountryR = decimal.Parse(ddlCountries.SelectedValue);

        try
        {
                 newEmail.InsertEmailMaintenance(newEmail);

                if (newEmail.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = newEmail.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                InsertEmailMaintAudit();

                btnNewEmail_ModalPopupExtender.Enabled = false;

                Load_SolutionResponsibles();            
        }
        catch (Exception ex)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.ShowPopup();

            SafetyPad.SetLogRecord("SolutionResponsibles.aspx.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Actualiza la información de un 
    /// Correo de mantenimiento
    /// Autor: Xolo
    /// Fecha: 06.Agost.2015
    /// </summary>
    private void Update()
    {
        EmailMaintenance updEmail = new EmailMaintenance();       
        updEmail.Email = tbEmail.Text;
        updEmail.Status = ddlStatus.SelectedValue;
        updEmail.IdCountryR = decimal.Parse(ddlCountries.SelectedValue);



        try
        {
            Decimal codEmailMaint = Convert.ToDecimal(Session["COD_EMAIL_MAINTENANCE"]);        
           
                updEmail.UpdateEmailMaintenance(updEmail, codEmailMaint);

                if (updEmail.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = updEmail.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                UpdateEmailMaintAudit();

                btnNewEmail_ModalPopupExtender.Enabled = false;

                Load_SolutionResponsibles();
        }
        catch (Exception ex)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.ShowPopup();

            SafetyPad.SetLogRecord("SolutionResponsibles.aspx.cs", ex.ToString());
        }
    }
    #endregion

    #region BUSQUEDA

    /// <summary>
    /// Click al boton de Busqueda
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnSearchEmail_Click(object sender, EventArgs e)
    {
        Fill_gvEmail(tbSeachEmail.Text, true);
    }


    /// <summary>
    /// Click al boton mostrar todos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnShowAllEmail_Click(object sender, EventArgs e)
    {
        Fill_gvEmail(string.Empty, true);
        tbSeachEmail.Text = string.Empty;
    }

    #endregion

    #region PISTAS DE AUDITORIA

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de actualización de un responsable de solución
    /// </summary>
    private void UpdateEmailMaintAudit()
    {
        String[] fieldNames = { "EMAIL", "RECORD_STATUS" };
        String[] fieldValues = {  tbEmail.Text, ddlStatus.SelectedValue };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_EMAIL_MAINTENANCES", "U");
    }

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de inserción de un responsable de solución
    /// </summary>
    private void InsertEmailMaintAudit()
    {
        String[] fieldNames = { "EMAIL", "RECORD_STATUS" };
        String[] fieldValues = { tbEmail.Text, ddlStatus.SelectedValue };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_EMAIL_MAINTENANCES", "C");
    }

    #endregion
}