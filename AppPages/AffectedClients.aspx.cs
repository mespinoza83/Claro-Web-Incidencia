using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppPages_AffectedClients : System.Web.UI.Page
{

    #region METODOS DE CARGA DE LA PAGINA

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetValidators();
            Load_ddlValues();
            ClearFields();
            Load_AffectedClients();

            SetUserPermissions();
        }
    }

    /// <summary>
    /// Metodo para establecer las acciones
    /// del usuario en sesion
    /// </summary>
    private void SetUserPermissions()
    {
        

        string userName = SafetyPad.GetUserLogin();

        btnSearchClient.Enabled = SafetyPad.IsAllowed("Buscar");
        btnShowAllClient.Enabled = SafetyPad.IsAllowed("MostrarTodos");
        btnSave.Enabled = SafetyPad.IsAllowed("Guardar");
        gvAffectedClients.Visible = SafetyPad.IsAllowed("Consultar");
        btnNewClient.Enabled = SafetyPad.IsAllowed("Agregar");
    }


    /// <summary>
    /// Carga la informacion de los estado en ddlStatus
    /// </summary>
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
    /// Coloca las validaciones en los campos correspondientes
    /// </summary>
    private void SetValidators()
    {
        tbName.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
    }

    /// <summary>
    /// Limpia todos los campos del Formulario
    /// </summary>
    private void ClearFields()
    {
        tbName.Text = string.Empty;
        tbSeachClient.Text = string.Empty;

        ddlStatus.SelectedIndex = 0;
    }

    /// <summary>
    /// Carga la vista de Clientes Afectados
    /// </summary>
    private void Load_AffectedClients()
    {
        mvAffectedClients.ActiveViewIndex = 0;
        TitleSubtitle1.SetTitle("Clientes Afectados");
        Fill_gvAffectedClients(string.Empty, false);
    }

    #endregion

    #region CLIENTES AFECTADOS

    /// <summary>
    /// Carga la informacion de los tipos de clientes
    /// en gvAffectedClients con un filtro de busqueda
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 02.Oct.2011
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvAffectedClients(string searchFilter, bool showErrorMessage)
    {
        AffectedClient result = new AffectedClient();
        result.GetAffectedClients(searchFilter);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.AffectedClientsTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvAffectedClients.DataSource = result.AffectedClientsTable;
        gvAffectedClients.DataBind();

        setStatusImage();
    }

    private void setStatusImage()
    {
        string status = string.Empty;

        for (int i = 0; i < gvAffectedClients.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvAffectedClients.Rows[i];

            status = gvAffectedClients.DataKeys[i]["RECORD_STATUS"].ToString();

            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

        }
    }


    /// <summary>
    /// Click al boton Nuevo Cliente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewClient_Click(object sender, EventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearFields();
        btnNewClient_ModalPopupExtender.Enabled = true;
        btnNewClient_ModalPopupExtender.Show();
    }

    /// <summary>
    /// Click al Boton guardar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ConfigurationTool.Command action = (ConfigurationTool.Command) Session["Action"];
            if (action == ConfigurationTool.Command.Insert)
                Insert();
            else
                Update();
        }
    }

    /// <summary>
    /// Click al bonton cancelar en el panel PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mvAffectedClients.ActiveViewIndex = 0;
        btnNewClient_ModalPopupExtender.Enabled = false;
        Load_AffectedClients();
    }

    /// <summary>
    /// Click al boton de edición en gv
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditAffectedClient_Click(object sender, ImageClickEventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Update;

        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        
        mvAffectedClients.ActiveViewIndex = 0;
        EditAffectedClient(rowIndex);
    }

    /// <summary>
    /// Carga la informacion de la fila seleccionada en el panelPopup
    /// </summary>
    /// <param name="rowIndex">fila Seleccionada</param>
    private void EditAffectedClient(int rowIndex)
    {
        //ObtEner el código del cliente afectado
        Session["COD_AFFECTED_CLIENT"] = gvAffectedClients.DataKeys[rowIndex]["COD_AFFECTED_CLIENT"].ToString();
        
        //Cargar la información del cliente
        AffectedClient result = new AffectedClient();
        result.EditAffectedClient(Convert.ToDecimal(Session["COD_AFFECTED_CLIENT"]));

        if (result.Messages.Status == 0) //Si hay un error
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else //Si el dato es correcto
        {
            tbName.Text = result.Name;            
            ddlStatus.SelectedValue = gvAffectedClients.DataKeys[rowIndex]["RECORD_STATUS"].ToString();

            btnNewClient_ModalPopupExtender.Enabled = true;
            btnNewClient_ModalPopupExtender.Show();
        }

        
    }

    #endregion

    #region BUSQUEDA

    protected void btnSearchSegment_Click(object sender, EventArgs e)
    {
        Fill_gvAffectedClients(tbSeachClient.Text, true);
    }

    protected void btnShowAllSegment_Click(object sender, EventArgs e)
    {
        Fill_gvAffectedClients(string.Empty, true);
        tbSeachClient.Text = string.Empty;
    }

    #endregion

    #region METODOS DE INSERTAR Y ACTUALIZAR

    /// <summary>
    /// Inserta un nuevo cliente afectado
    /// Fecha:17.Oct.2011
    /// Autor: Manuel Gutiérrez Rojas
    /// </summary>
    private void Insert()
    {
        AffectedClient newClient = new AffectedClient();
        newClient.Name = tbName.Text;
        newClient.Status = ddlStatus.SelectedValue;

        try
        {
            newClient.InsertAffectedClient(newClient);

            if (newClient.Messages.Status == 1)
            {
                wucMessageControl.Title = "Mensaje";
                wucMessageControl.Image = "../include/imagenes/info_32.png";
            }
            else
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png"; 
            }

            wucMessageControl.Message = newClient.Messages.Message;
            wucMessageControl.ShowPopup();
            //Registrar las pistas de Auditoria 
            //InsertAffectedClientAudit();

            btnNewClient_ModalPopupExtender.Enabled = false;

            Load_AffectedClients();
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


    /// <summary>
    /// Inserta un nuevo cliente afectado
    /// Fecha:17.Oct.2011
    /// Autor: Manuel Gutiérrez Rojas
    /// </summary>
    private void Update()
    {
        AffectedClient updClient = new AffectedClient();
        updClient.Name = tbName.Text;
        updClient.Status = ddlStatus.SelectedValue;

        try
        {
            decimal codAffectedClient = Convert.ToDecimal(Session["COD_AFFECTED_CLIENT"]);

            if (updClient.GetUnfinishedIncidents(codAffectedClient) > 0 && ddlStatus.SelectedValue == "0")
            {

                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = "El Registro no puede ser dado de baja</br>Aún hay incidencias Activas para el";
                wucMessageControl.ShowPopup();
            }

            else
            {
                updClient.UpdateAffectedClient(updClient, codAffectedClient);

                if (updClient.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = updClient.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                //UpdateAffectedClientAudit();

                btnNewClient_ModalPopupExtender.Enabled = false;

                Load_AffectedClients();
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

    #endregion

    #region PISTAS DE AUDITORIA

    /// <summary>
    /// Registra las pistas de Auditoria de 
    /// Inserción
    /// </summary>
    private void InsertAffectedClientAudit()
    {
        String[] fieldNames = { "AFFECTED_CLIENT_NAME", "RECORD_STATUS" };
        String[] fieldValues = {tbName.Text, ddlStatus.SelectedValue};

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_AFFECTED_CLIENTS", "C");
    }

    /// <summary>
    /// Registra las pistas de Auditoria de 
    /// Inserción
    /// </summary>
    private void UpdateAffectedClientAudit()
    {
        String[] fieldNames = { "AFFECTED_CLIENT_NAME", "RECORD_STATUS" };
        String[] fieldValues = { tbName.Text, ddlStatus.SelectedValue };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_AFFECTED_CLIENTS", "U");
    }

    #endregion



    /// <summary>
    /// Evento de cambio de pagina en 
    /// gvAffectedClients
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAffectedClients_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAffectedClients.PageIndex = e.NewPageIndex;
        Fill_gvAffectedClients(tbSeachClient.Text, true);
    }
}