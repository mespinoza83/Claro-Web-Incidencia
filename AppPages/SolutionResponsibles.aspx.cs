using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppPages_SolutionResponsibles : System.Web.UI.Page
{
    #region METODOS DE CARGA DE LA PAGINA

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setValidatorFiels();
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
        btnSearchResponsible.Enabled = SafetyPad.IsAllowed("Buscar");
        btnShowAllResponsible.Enabled = SafetyPad.IsAllowed("MostrarTodos");
        btnSave.Enabled = SafetyPad.IsAllowed("Guardar");
        gvResponsibles.Visible = SafetyPad.IsAllowed("Consultar");
        btnNewResponsible.Enabled = SafetyPad.IsAllowed("Agregar");
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
        tbArea.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
        tbEmail.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Email);
        tbName.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.LetterAndNumber);
        
    }

    /// <summary>
    /// Carga de los Datos de los Responsables
    /// </summary>
    private void Load_SolutionResponsibles()
    {
        mvSolutionResponsibles.ActiveViewIndex = 0;
        TitleSubtitle1.SetTitle("RESPONSABLES DE SOLUCIÓN");

        Fill_gvSolutionResponsibles(string.Empty,false);
    }

    /// <summary>
    /// Limpia el contenido de los campos en el 
    /// formulario
    /// </summary>
    private void ClearFields()
    {
        tbArea.Text = string.Empty;
        tbEmail.Text = string.Empty;
        tbName.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        lblMessageSegm.Text = string.Empty;
        //ddlCountries.SelectedValue = "-1";
        ddlCountries.DataSourceID = odsCountries.ID;
        odsCountries.DataBind();
        ddlCountries.DataBind();
    }

    #endregion

    #region RESPONSABLES DE SOLUCION

    /// <summary>
    /// Llena el grid con la informacion de los Responsables
    /// de solucion
    /// </summary>
    /// <param name="searchFilter">filtro de Busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvSolutionResponsibles(string searchFilter, bool showErrorMessage)
    {
        SolutionResponsible result = new SolutionResponsible();
        result.GetSolutionResponsibles(searchFilter);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.SolutionResponsiblesTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvResponsibles.DataSource = result.SolutionResponsiblesTable;
        gvResponsibles.DataBind();

        setStatusImage();
    }


    /// <summary>
    /// Coloca la imagen correspondiente al estado en 
    /// gvSolutionResponsibles
    /// </summary>
    private void setStatusImage()
    {
        string status = string.Empty;

        for (int i = 0; i < gvResponsibles.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvResponsibles.Rows[i];

            status = gvResponsibles.DataKeys[i]["RECORD_STATUS"].ToString();

            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgAct")).Visible = (status == "1");
            ((System.Web.UI.WebControls.Image)selectedRow.FindControl("imgDes")).Visible = (status == "0");

        }    
    }
    
    
    /// <summary>
    /// Click al boton de Edicion en el grid de
    /// Responsables de solucion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditResponsible_Click(object sender, ImageClickEventArgs e)
    {
        lblMessageSegm.Text = string.Empty;
        Session["Action"] = ConfigurationTool.Command.Update;

        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        
        mvSolutionResponsibles.ActiveViewIndex = 0;
        EditSolutionResponsible(rowIndex);
    }

    /// <summary>
    /// Click al boton Nuevo Responsable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnNewResponsible_Click(object sender, EventArgs e)
    {
        lblMessageSegm.Text = string.Empty;
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearFields();
        btnNewResponsible_ModalPopupExtender.Enabled = true;
        btnNewResponsible_ModalPopupExtender.Show();
    }

    /// <summary>
    /// Carga la información de un Responsable para su edición
    /// </summary>
    /// <param name="rowIndex">fila seleccionada</param>
    private void EditSolutionResponsible(int rowIndex)
    {
        //Guardar el Código del Responsable a editar
        Session["COD_SOLUTION_RESPONSIBLE"] = gvResponsibles.DataKeys[rowIndex]["COD_SOLUTION_RESPONSIBLE"].ToString();
        
        //Cargar la información del responsable
        SolutionResponsible result = new SolutionResponsible();
        result.EditSolutionResponsible(Convert.ToDecimal(Session["COD_SOLUTION_RESPONSIBLE"]));

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
            tbArea.Text = result.Area;
            tbEmail.Text = result.Email;
            ddlStatus.SelectedValue = gvResponsibles.DataKeys[rowIndex]["RECORD_STATUS"].ToString();

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


            btnNewResponsible_ModalPopupExtender.Enabled = true;
            btnNewResponsible_ModalPopupExtender.Show();
        }
    }

    /// <summary>
    /// Click al boton cancelar en el Panel PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mvSolutionResponsibles.ActiveViewIndex = 0;
        btnNewResponsible_ModalPopupExtender.Enabled = false;
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
        if (Page.IsValid)
        {
            if (ddlCountries.SelectedValue != "-1")
            {
                if (tbName.Text.ToString().Length == 0 || tbEmail.Text.ToString().Length == 0 || tbArea.Text.ToString().Length == 0)
                {
                    lblMessageSegm.Text = "Todos los datos son requeridos";
                    if (tbEmail.Text.ToString().Length == 0)
                        lblMessageSegm.Text = lblMessageSegm.Text + ", incluya email";
                    btnNewResponsible_ModalPopupExtender.Enabled = true;
                    btnNewResponsible_ModalPopupExtender.Show();
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
                btnNewResponsible_ModalPopupExtender.Enabled = true;
                btnNewResponsible_ModalPopupExtender.Show();

            }
        }
    }

    #endregion

    #region METODOS DE INSERTAR Y ACTUALIZAR

    /// <summary>
    /// Inserta un nuevo Responsable de solución
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 18.Oct.2011
    /// </summary>
    private void Insert()
    {
        SolutionResponsible newResponsible = new SolutionResponsible();
        newResponsible.Name = tbName.Text;
        newResponsible.Email = tbEmail.Text;
        newResponsible.Area = tbArea.Text;
        newResponsible.Status = ddlStatus.SelectedValue;
        newResponsible.IdCountryR = decimal.Parse(ddlCountries.SelectedValue);

        try
        {
            newResponsible.InsertSolutionResponsible(newResponsible);

            if (newResponsible.Messages.Status == 1)
            {
                wucMessageControl.Title = "Mensaje";
                wucMessageControl.Image = "../include/imagenes/info_32.png";
            }
            else
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
            }

            wucMessageControl.Message = newResponsible.Messages.Message;
            wucMessageControl.ShowPopup();
            //Registrar las pistas de Auditoria 
            InsertSolutionRespAudit();

            btnNewResponsible_ModalPopupExtender.Enabled = false;

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
    /// responsable de solución
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha: 18.Oct.2011
    /// </summary>
    private void Update()
    {
        SolutionResponsible updResponsible = new SolutionResponsible();
        updResponsible.Name = tbName.Text;
        updResponsible.Email = tbEmail.Text;
        updResponsible.Area = tbArea.Text;
        updResponsible.Status = ddlStatus.SelectedValue;
        updResponsible.IdCountryR = decimal.Parse(ddlCountries.SelectedValue);

        

         try
        {
            Decimal codSolutionResponsible = Convert.ToDecimal(Session["COD_SOLUTION_RESPONSIBLE"]);

            if (updResponsible.GetUnfinishedIncidents(codSolutionResponsible) > 0 && ddlStatus.SelectedValue == "0")
            {

                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = "El Registro no puede ser dado de baja</br>Aún hay incidencias Activas para el";
                wucMessageControl.ShowPopup();
            }
            else
            {

                updResponsible.UpdateAffectedClient(updResponsible, codSolutionResponsible);

                if (updResponsible.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = updResponsible.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                UpdateSolutionRespAudit();

                btnNewResponsible_ModalPopupExtender.Enabled = false;

                Load_SolutionResponsibles();
            }
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
    public void btnSearchResponsible_Click(object sender, EventArgs e)
    {
        Fill_gvSolutionResponsibles(tbSeachResponsible.Text,true);
    }


    /// <summary>
    /// Click al boton mostrar todos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public  void btnShowAllResponsible_Click(object sender, EventArgs e)
    {
        Fill_gvSolutionResponsibles(string.Empty, true);
        tbSeachResponsible.Text = string.Empty;
    }

    #endregion

    #region PISTAS DE AUDITORIA

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de actualización de un responsable de solución
    /// </summary>
    private void UpdateSolutionRespAudit()
    {
        String[] fieldNames = { "NAME", "EMAIL", "AREA", "RECORD_STATUS" };
        String[] fieldValues = { tbName.Text,tbEmail.Text,tbArea.Text, ddlStatus.SelectedValue };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_SOLUTION_RESPONSIBLES", "U"); 
    }

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de inserción de un responsable de solución
    /// </summary>
    private void InsertSolutionRespAudit()
    {
        String[] fieldNames = { "NAME", "EMAIL", "AREA", "RECORD_STATUS" };
        String[] fieldValues = { tbName.Text, tbEmail.Text, tbArea.Text, ddlStatus.SelectedValue };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_SOLUTION_RESPONSIBLES", "C");
    }

    #endregion
    /// <summary>
    /// Evento de cambio de pagina en 
    /// gvResponsibles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResponsibles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvResponsibles.PageIndex = e.NewPageIndex;
        Fill_gvSolutionResponsibles(tbSeachResponsible.Text, true);
    }

    protected void ddlCountries_DataBound(object sender, EventArgs e)
    {
        ddlCountries.Items.Insert(0, new ListItem("Seleccione", "-1"));
    }
}