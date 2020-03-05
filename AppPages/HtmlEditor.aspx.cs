using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppPages_HtmlEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TitleSubtitle1.SetSubTitleVisible = false;
            //CKEdHtml.config.uiColor = "#0099FF"; //"#006666"; /*"#BFEE62";*/
            //CKEdHtml.config.allowedContent = true;
            //CKEdHtml.config.language = "es";
            TitleSubtitle1.SetTitle("Administración HTML de Notificaciones"); //Poniendo Título a la página
            txtClas.Attributes.Add("readonly", "true"); //Declarando controles como de sólo lectura
            txtType.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                LoadGrid();
                pnlEditor.Visible = false;
            }
        }
        catch(Exception ex)
        {
           
            TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
            TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Session["COD_INCIDENCE"] = grvHtml.DataKeys[rowIndex]["COD_HTML_PK"].ToString();
            TitleSubtitle1.SetSubTitleVisible = false;
            HTMLEditor incidence = new HTMLEditor();
            string strHtml = txtContenido.Content;
            //strHtml = strHtml.Replace("&","|||");
            //strHtml = "'" + strHtml + "'";
            string strDesc = txtDescript.Text;
            decimal idHtml = Convert.ToDecimal(Session["COD_INCIDENCE"]);
            string id2 = Convert.ToString(idHtml);
            int id3 = Convert.ToInt32(id2);
            //preCKEditorData.InnerText = strHtml;
            incidence.UpdateHtml(strHtml, strDesc, idHtml);
            if (incidence.Messages.Status == 1)
            {
                wucMessageControl.Title = "Mensaje";
                wucMessageControl.Image = "../include/imagenes/info_32.png";
            }
            else
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
            }

            wucMessageControl.Message = incidence.Messages.Message;
            wucMessageControl.ShowPopup();
            Fill_gvEmail(ddlClasif.Text, true);
            pnlEditor.Visible = false;
            pnlLoad.Visible = true;

        }
        catch (Exception ex)
        {
            TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
            TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
 
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            TitleSubtitle1.SetSubTitleVisible = false;
            pnlLoad.Visible = true;
            //CKEdHtml.Text = "";
            txtClas.Text = String.Empty;
            txtDescript.Text = String.Empty;
            txtType.Text = String.Empty;
            pnlEditor.Visible = false;
        }
        catch(Exception ex)
        {
            TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
            TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
        }
    }
    protected void ibtnEditReg_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["Action"] = ConfigurationTool.Command.Update;

            //Obtner la fila donde se activo el evento
            GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
            int rowIndex;

            rowIndex = gvRow.RowIndex;
            /*tbCodigo.Enable = false;
            tbAlias.Enable = false;*/
            pnlEditor.Visible = true;
            EditHTML(rowIndex);
            pnlLoad.Visible = false;
            Utilities.CreateConfirmBox(btnSave, "Esta seguro de realizar cambio?");
            //Utilities.CreateConfirmBox(btnSaveAsunto, "Esta seguro de realizar cambio?");
        }
        catch (Exception ex)
        {
            TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
            TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
        }
    }
    protected void grvParameters_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    /// <summary>
    /// Carga los datos del Grid
    /// </summary>
    protected void LoadGrid()
    {
        try
        {
            HTMLEditor incidence = new HTMLEditor();
            DataTable dtHtmlList = new DataTable();

            Fill_gvEmail(ddlClasif.Text, true);

            /*grvHtml.DataSource = dtHtmlList;
            grvHtml.DataBind();*/
        }
        catch (Exception ex)
        {
            //TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
           // TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
        }

    }

    /// <summary>
    /// Carga los datos en los controles
    /// </summary>
    /// <param name="rowIndex">Registro a Buscar</param>
    protected void EditHTML(int rowIndex)
    {

      
        //Guardar el Código del Responsable a editar
        Session["COD_INCIDENCE"] = grvHtml.DataKeys[rowIndex]["COD_HTML_PK"].ToString();

        //Cargar la información del responsable
        HTMLEditor result = new HTMLEditor();
        result.EditHTML(Convert.ToDecimal(Session["COD_INCIDENCE"]));
        txtDescript.Text = result.Description;
        txtClas.Text = result.descripcionClasificacion;
        txtType.Text = result.descripcionTipo;
        txtContenido.Content = result.Content;
        //txtContenido.Content = result.Content.Replace("'","");
        //CKEdHtml.Text = result.Content.Replace("|||", "&").Replace("'", "");
        //CKEdHtml.Text = result.Content;
        //CKEdHtml.Text = result.Content;

       /* //ObtEner el código del registro afectado
        Session["COD_HTML"] = grvHtml.DataKeys[rowIndex]["COD_HTML_PK"].ToString();

        //Cargar la información del registro
        HTMLEditor result = new HTMLEditor();
        CKEdHtml.Text = result.GetHtmlData(Convert.ToDecimal(Session["COD_HTML"]));
        txtDescript.Text = grvHtml.DataKeys[rowIndex]["DESCRIPTION_HTML"].ToString();
        txtClas.Text = grvHtml.DataKeys[rowIndex]["CLASIFICATION"].ToString();
        txtType.Text = grvHtml.DataKeys[rowIndex]["TIPO"].ToString();*/

    }

    /// <summary>
    /// Llena el grid con la informacion de los Correos
    /// de mantenimiento
    /// </summary>
    /// <param name="searchFilter">filtro de Busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvEmail(string searchFilter, bool showErrorMessage)
    {
        HTMLEditor result = new HTMLEditor();
        result.GetHtml(searchFilter);

        /*if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.HtmlTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }*/

        grvHtml.DataSource = result.HtmlTable;
        grvHtml.DataBind();

        //setStatusImage();
    }

    

    protected void ddlClasif_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HTMLEditor incidence = new HTMLEditor();
            DataTable dtHtmlList = new DataTable();

            Fill_gvEmail(ddlClasif.Text, true);

           /* grvHtml.DataSource = dtHtmlList;
            grvHtml.DataBind();*/
        }
        catch (Exception ex)
        {
            //TitleSubtitle1.SetSubtitle("Ocurrió un inconveniente");
            // TitleSubtitle1.SetSubTitleVisible = true;
            SafetyPad.SetLogRecord("HtmlEditor.aspx.cs", ex.ToString());
        }
    }



}