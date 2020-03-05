using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;

public partial class AppPages_Parameters : System.Web.UI.Page
{

    #region PROPIEDADES
    private List<ParameterAsunto> ListaValoresAsunto
    {
        get { return (List<ParameterAsunto>) this.Session["ListaValoresAsunto"]; }
        set { this.Session["ListaValoresAsunto"] = value; }
    }
    #endregion

    #region METODOS DE CARGA DE LA PAGINA

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ListaValoresAsunto = new List<ParameterAsunto>();
            SetValidators();
            ClearFields();
            Load_Parameters();
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

        btnSearchParameter.Enabled = SafetyPad.IsAllowed("Buscar");
        btnShowAllParameter.Enabled = SafetyPad.IsAllowed("MostrarTodos");
        btnSave.Enabled = SafetyPad.IsAllowed("Guardar");
        gvParameters.Visible = SafetyPad.IsAllowed("Consultar");
        btnNewParameter.Enabled = SafetyPad.IsAllowed("Agregar");          
              
    }


    /// <summary>
    /// Coloca las validaciones en los campos correspondientes
    /// </summary>
    private void SetValidators()
    {
        tbCodigo.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Number);
        tbAlias.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);
        tbValor.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);

        tbCodIncidencia.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);      
        tbOrdenCodIncidencia.SetCustomValidator("12345");
       
        tbPais.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);
        tbOrdenPais.SetCustomValidator("12345");

        tbTipo.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);
        tbOrdenTipo.SetCustomValidator("12345");

        tbSegmento.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);
        tbOrdenSegmento.SetCustomValidator("12345");

        tbNivel.SetDefinedValidator(Text_EditField.ExpressionTypeEnum.Parameter);
        tbOrdenNivel.SetCustomValidator("12345");


    }

    /// <summary>
    /// Limpia todos los campos del Formulario
    /// </summary>
    private void ClearFields()
    {
        tbCodigo.Text = string.Empty;
        tbAlias.Text = string.Empty;
        tbValor.Text = string.Empty;
        tbSeachParameter.Text = string.Empty;

    }

    /// <summary>
    /// Evalua si hay Orden repetido al ingresar Valor en el parametro Asunto
    /// </summary>
    private Int32 ValidarOrdenAsunto()
    {

        Int32 Counter =0;

        if (tbOrdenCodIncidencia.Text == "" && tbOrdenNivel.Text == "" && tbOrdenPais.Text == "" && tbOrdenSegmento.Text == "" && tbOrdenTipo.Text == "")
        {
            Counter = 2;
        }

        if (tbOrdenCodIncidencia.Text != "")
        {
            if (tbOrdenCodIncidencia.Text == tbOrdenNivel.Text || tbOrdenCodIncidencia.Text == tbOrdenPais.Text || tbOrdenCodIncidencia.Text == tbOrdenSegmento.Text || tbOrdenCodIncidencia.Text == tbOrdenTipo.Text)
            {
                Counter = 1;
            }            
        }

        if (tbOrdenNivel.Text != "")
        {
            if (tbOrdenNivel.Text == tbOrdenCodIncidencia.Text || tbOrdenNivel.Text == tbOrdenPais.Text || tbOrdenNivel.Text == tbOrdenSegmento.Text || tbOrdenNivel.Text == tbOrdenTipo.Text)
            {
                Counter = 1;
            }            
        }

        if (tbOrdenPais.Text != "")
        {
            if (tbOrdenPais.Text == tbOrdenCodIncidencia.Text || tbOrdenPais.Text == tbOrdenNivel.Text || tbOrdenPais.Text == tbOrdenSegmento.Text || tbOrdenPais.Text == tbOrdenTipo.Text)
            {
                Counter = 1;
            }            
        }

        if (tbOrdenSegmento.Text != "")
        {
            if (tbOrdenSegmento.Text == tbOrdenCodIncidencia.Text || tbOrdenSegmento.Text == tbOrdenNivel.Text || tbOrdenSegmento.Text == tbOrdenPais.Text || tbOrdenSegmento.Text == tbOrdenTipo.Text)
            {
                Counter = 1;
            }           
        }

        if (tbOrdenTipo.Text != "")
        {
            if (tbOrdenTipo.Text == tbOrdenCodIncidencia.Text || tbOrdenTipo.Text == tbOrdenNivel.Text || tbOrdenTipo.Text == tbOrdenPais.Text || tbOrdenTipo.Text == tbOrdenSegmento.Text)
            {
                Counter = 1;
            }            
        }
        

        return Counter;

    }

    /// <summary>
    /// Carga la vista de Clientes Afectados
    /// </summary>
    private void Load_Parameters()
    {

        mvParameters.ActiveViewIndex = 0;
        TitleSubtitle1.SetTitle("Parametros");
        Fill_gvParameters(string.Empty, false);
    }

    /// <summary>
    /// Evento de cambio de pagina en 
    /// gvParameters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvParameters_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvParameters.PageIndex = e.NewPageIndex;
        Fill_gvParameters(tbSeachParameter.Text, true);
    }

    #endregion

    #region PARAMETROS

    /// <summary>
    /// Carga la informacion de los Parametros
    /// en gvParameters con un filtro de busqueda
    /// Autor: Xolo
    /// Fecha: 04.Agost.2015
    /// </summary>
    /// <param name="searchFilter">filtro de busqueda</param>
    /// <param name="showErrorMessage">muestra mensaje de error en caso de no haber datos</param>
    private void Fill_gvParameters(string searchFilter, bool showErrorMessage)
    {
        Parameters result = new Parameters();
        result.GetParameter(searchFilter);

        if (result.Messages.Status == 0)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else
        {
            if (result.ParameterTable.Count <= 0 && showErrorMessage == true)
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/advertencia_32.png";
                wucMessageControl.Message = "No se encontraron datos";
                wucMessageControl.ShowPopup();
            }
        }

        gvParameters.DataSource = result.ParameterTable;
        gvParameters.DataBind();
           }
    

    /// <summary>
    /// Click al boton Nuevo Cliente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewParameter_Click(object sender, EventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Insert;
        ClearFields();
        tbCodigo.Text = "0";
        tbCodigo.Enable = false;
        tbAlias.Enable = true;
        btnSave.Attributes.Remove("onclick");
        btnNewParameter_ModalPopupExtender.Enabled = true;
        btnNewParameter_ModalPopupExtender.Show();
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
            ConfigurationTool.Command action = (ConfigurationTool.Command)Session["Action"];
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

        mvParameters.ActiveViewIndex = 0;
        btnNewParameter_ModalPopupExtender.Enabled = false;
        Load_Parameters();        
    }

    /// <summary>
    /// Click al bonton cancelar en el panel PopUp Asunto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelAsunto_Click(object sender, EventArgs e)
    {

        mvParameters.ActiveViewIndex = 0;
        btnAsunto_ModalPopupExtender.Enabled = false;
        Load_Parameters();
    }

    /// <summary>
    /// Click al Boton guardar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveAsunto_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

           Int32 valid = ValidarOrdenAsunto();

           if (valid == 1)
           {
               lblOrdenRepetido.Text = "Orden Repetido";
               btnAsunto_ModalPopupExtender.Enabled = true;
               btnAsunto_ModalPopupExtender.Show();
           }
           else if (valid == 2)
           {
               lblOrdenRepetido.Text = "Ingrese al menos un Orden";
               btnAsunto_ModalPopupExtender.Enabled = true;
               btnAsunto_ModalPopupExtender.Show();
           }
           else
           {    
               if (tbOrdenCodIncidencia.Text != "")
               {
                   ParameterAsunto Asunto = new ParameterAsunto();
                   Asunto.Name = tbCodIncidencia.Text;
                   Asunto.Orden = Convert.ToDecimal(tbOrdenCodIncidencia.Text);
                   ListaValoresAsunto.Add(Asunto);
               }

               if (tbOrdenPais.Text != "")
               {
                   ParameterAsunto Asunto = new ParameterAsunto();
                   Asunto.Name = tbPais.Text;
                   Asunto.Orden = Convert.ToDecimal(tbOrdenPais.Text);
                   ListaValoresAsunto.Add(Asunto);
               }


               if (tbOrdenTipo.Text != "")
               {
                   ParameterAsunto Asunto = new ParameterAsunto();
                   Asunto.Name = tbTipo.Text;
                   Asunto.Orden = Convert.ToDecimal(tbOrdenTipo.Text);
                   ListaValoresAsunto.Add(Asunto);
               }


               if (tbOrdenSegmento.Text != "")
               {
                   ParameterAsunto Asunto = new ParameterAsunto();
                   Asunto.Name = tbSegmento.Text;
                   Asunto.Orden = Convert.ToDecimal(tbOrdenSegmento.Text);
                   ListaValoresAsunto.Add(Asunto);
               }

               if (tbOrdenNivel.Text != "")
               {
                   ParameterAsunto Asunto = new ParameterAsunto();
                   Asunto.Name = tbNivel.Text;
                   Asunto.Orden = Convert.ToDecimal(tbOrdenNivel.Text);
                   ListaValoresAsunto.Add(Asunto);
               }

               lblOrdenRepetido.Text = "";
               UpdateAsunto();
           }
            
           
        }
    }

    /// <summary>
    /// Click al boton de edición en gv
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEditParameter_Click(object sender, ImageClickEventArgs e)
    {
        Session["Action"] = ConfigurationTool.Command.Update;

        //Obtner la fila donde se activo el evento
        GridViewRow gvRow = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int rowIndex;

        rowIndex = gvRow.RowIndex;
        tbCodigo.Enable = false;
        tbAlias.Enable = false;
        mvParameters.ActiveViewIndex = 0;
        EditParameter(rowIndex);
        Utilities.CreateConfirmBox(btnSave, "Esta seguro de realizar cambio?");
        Utilities.CreateConfirmBox(btnSaveAsunto, "Esta seguro de realizar cambio?");
    }

    /// <summary>
    /// Carga la informacion de la fila seleccionada en el panelPopup
    /// </summary>
    /// <param name="rowIndex">fila Seleccionada</param>
    private void EditParameter(int rowIndex)
    {
        //ObtEner el código del cliente afectado

        Session["COD_PARAMETER"] = gvParameters.DataKeys[rowIndex]["COD_PARAMETER"].ToString();

        //Cargar la información del cliente
        Parameters result = new Parameters();
        result.EditParameter(Convert.ToDecimal(Session["COD_PARAMETER"]));

        if (result.Messages.Status == 0) //Si hay un error
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = result.Messages.Message;
            wucMessageControl.ShowPopup();
        }
        else //Si el dato es correcto
        {
            string vAsunto;
            tbAlias.Text = result.Alias;
            tbValor.Text = result.Valor;
            tbCodigo.Text = result.CodParameter.ToString();
            hdfCodigo.Value = result.CodParameter.ToString();
            hdfAlias.Value = result.Alias;
            vAsunto=result.Valor;

            if (tbAlias.Text == "ASUNTO")
            {
                string[] valorAsunto = vAsunto.Split(new string[] { " || ' - ' || " }, StringSplitOptions.None);

                cargarvalAsunto(valorAsunto);             

                btnAsunto_ModalPopupExtender.Enabled = true;
                btnAsunto_ModalPopupExtender.Show();
            }
            else
            {
                btnNewParameter_ModalPopupExtender.Enabled = true;
                btnNewParameter_ModalPopupExtender.Show();
            }
        }


    }

    /// <summary>
    /// Carga la información de los campos cuando el Alias es ASUNTO
    /// </summary>
    /// <param name="valor"></param>
    private void cargarvalAsunto(string[] valor)
    {
        if (valor.Length > 0)
        {

            for (int i = 0; i <= valor.Length - 1; i++ )
                if (tbCodIncidencia.Text == valor[i])
                {
                    tbOrdenCodIncidencia.Text = Convert.ToString(i+1);
                }
                else if (tbNivel.Text == valor[i])
                {
                    tbOrdenNivel.Text = Convert.ToString(i + 1); ;
                }
                else if (tbPais.Text == valor[i])
                {
                    tbOrdenPais.Text = Convert.ToString(i + 1); ;
                }
                else if (tbSegmento.Text == valor[i])
                {

                    tbOrdenSegmento.Text = Convert.ToString(i + 1); ;
                }
                else if (tbTipo.Text == valor[i])
                {
                    tbOrdenTipo.Text = Convert.ToString(i + 1); ;
                }           

        } 
    }

    #endregion

    #region BUSQUEDA

    protected void btnSearchSegment_Click(object sender, EventArgs e)
    {
        Fill_gvParameters(tbSeachParameter.Text, true);
    }

    protected void btnShowAllSegment_Click(object sender, EventArgs e)
    {
        Fill_gvParameters(string.Empty, true);
        tbSeachParameter.Text = string.Empty;
    }

    #endregion

    #region METODOS DE INSERTAR Y ACTUALIZAR

    /// <summary>
    /// Inserta un nuevo Parametro
    /// Fecha: 04.Agost.2015
    /// Autor: Xolo
    /// </summary>
    private void Insert()
    {
        Parameters newParameter= new Parameters();

       // newParameter.CodParameter = Convert.ToDecimal(tbCodigo.Text);
        newParameter.Alias=tbAlias.Text;
        newParameter.Valor=tbValor.Text;
        

        try
        {

            //if (newParameter.ExistCodigo(Convert.ToDecimal(tbCodigo.Text)) > 0)
            //{

            //    wucMessageControl.Title = "Error";
            //    wucMessageControl.Image = "../include/imagenes/error_32.png";
            //    wucMessageControl.Message = "Ya existe Código";
            //    wucMessageControl.ShowPopup();
            //    ClearFields();
            //}
            //else 
            if (newParameter.ExistAlia(tbAlias.Text) > 0)
             {
                 wucMessageControl.Title = "Error";
                 wucMessageControl.Image = "../include/imagenes/error_32.png";
                 wucMessageControl.Message = "Ya existe un parámetro con el mismo nombre";
                 wucMessageControl.ShowPopup();
                 ClearFields();
             }
             else
             {

                 newParameter.InsertParameter(newParameter);

                 if (newParameter.Messages.Status == 1)
                 {
                     wucMessageControl.Title = "Mensaje";
                     wucMessageControl.Image = "../include/imagenes/info_32.png";
                 }
                 else
                 {
                     wucMessageControl.Title = "Error";
                     wucMessageControl.Image = "../include/imagenes/error_32.png";
                 }

                 wucMessageControl.Message = newParameter.Messages.Message;
                 wucMessageControl.ShowPopup();
                 //Registrar las pistas de Auditoria 
                 //InsertAffectedClientAudit();

                 btnNewParameter_ModalPopupExtender.Enabled = false;

                 Load_Parameters();
             }
        }
        catch (Exception ex)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.ShowPopup();

            SafetyPad.SetLogRecord("Parameters.aspx.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Actualiza un Parametro
    /// Fecha: 04.Agost.2015
    /// Autor: Xolo
    /// </summary>
    private void Update()
    {
        Parameters updParameter = new Parameters();

        updParameter.CodParameter = Convert.ToDecimal(tbCodigo.Text);
        updParameter.Alias = tbAlias.Text;
        updParameter.Valor = tbValor.Text;
        
        try
        {
            decimal codParameter = Convert.ToDecimal(tbCodigo.Text);
            decimal codParameterAct = Convert.ToDecimal(Session["COD_PARAMETER"]);
            
            if (updParameter.ExistCodigo(codParameter) > 0 && hdfCodigo.Value != tbCodigo.Text.Trim())
            {

                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = "Ya existe un parámetro con el mismo código";
                wucMessageControl.ShowPopup();
                ClearFields();
            }
            else if (updParameter.ExistAlia(tbAlias.Text) > 0 && hdfAlias.Value != tbAlias.Text.Trim())
            {
                wucMessageControl.Title = "Error";
                wucMessageControl.Image = "../include/imagenes/error_32.png";
                wucMessageControl.Message = "Ya existe un parámetro con el mismo nombre";
                wucMessageControl.ShowPopup();
                ClearFields();
            }
            else
            {
                
                    updParameter.UpdateParameter(updParameter, codParameterAct);

                    if (updParameter.Messages.Status == 1)
                    {
                        wucMessageControl.Title = "Mensaje";
                        wucMessageControl.Image = "../include/imagenes/info_32.png";
                    }
                    else
                    {
                        wucMessageControl.Title = "Error";
                        wucMessageControl.Image = "../include/imagenes/error_32.png";
                    }

                    wucMessageControl.Message = updParameter.Messages.Message;
                    wucMessageControl.ShowPopup();
                    //Registrar las pistas de Auditoria 
                    //UpdateAffectedClientAudit();

                    btnNewParameter_ModalPopupExtender.Enabled = false;

                    Load_Parameters();                
            }
        }
        catch (Exception ex)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.ShowPopup();

            SafetyPad.SetLogRecord("Parameters.aspx.cs", ex.ToString());
        }
    }

    /// <summary>
    /// Actualizar Valor de Parametro Asunto
    /// Fecha: 05.Agost.2015
    /// Autor: Xolo
    /// </summary>
    private void UpdateAsunto()
    {
        Parameters updParameter = new Parameters();
        string ValorFinal = "";

        updParameter.CodParameter = Convert.ToDecimal(Session["COD_PARAMETER"]);
        updParameter.Alias = "ASUNTO";

        foreach (ParameterAsunto Item in ListaValoresAsunto.OrderBy(x => x.Orden).ToList()) //.OrderBy(x => x.Orden).ToList()
        {
            if (ValorFinal == "")
            {
                ValorFinal = Item.Name;
            }
            else
            {
                ValorFinal = ValorFinal + " || ' - ' || " + Item.Name;
            }
        }
        updParameter.Valor = ValorFinal;

        try
        {
            decimal codParameterAct = Convert.ToDecimal(Session["COD_PARAMETER"]);
            
            updParameter.UpdateParameter(updParameter, codParameterAct);

                if (updParameter.Messages.Status == 1)
                {
                    wucMessageControl.Title = "Mensaje";
                    wucMessageControl.Image = "../include/imagenes/info_32.png";
                }
                else
                {
                    wucMessageControl.Title = "Error";
                    wucMessageControl.Image = "../include/imagenes/error_32.png";
                }

                wucMessageControl.Message = updParameter.Messages.Message;
                wucMessageControl.ShowPopup();
                //Registrar las pistas de Auditoria 
                //UpdateAffectedClientAudit();

                btnAsunto_ModalPopupExtender.Enabled = false;

                ListaValoresAsunto.Clear();
                Load_Parameters();
            
        }
        catch (Exception ex)
        {
            wucMessageControl.Title = "Error";
            wucMessageControl.Image = "../include/imagenes/error_32.png";
            wucMessageControl.Message = ex.Message.ToString();
            wucMessageControl.ShowPopup();

            SafetyPad.SetLogRecord("Parameters.aspx.cs", ex.ToString());
        }


    }

    #endregion

    #region PISTAS DE AUDITORIA

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de actualización de un responsable de solución
    /// </summary>
    private void UpdateParameters()
    {
        String[] fieldNames = { "COD_PARAMETER", "PARAMETER_ALIAS", "PARAMETER_VALUE" };
        String[] fieldValues = { tbCodigo.Text, tbAlias.Text, tbValor.Text };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_PARAMETERS", "U");
    }

    /// <summary>
    /// Registra las pistas de auditoria 
    /// de inserción de un responsable de solución
    /// </summary>
    private void InsertParameters()
    {
        String[] fieldNames = { "COD_PARAMETER", "PARAMETER_ALIAS", "PARAMETER_VALUE" };
        String[] fieldValues = { tbCodigo.Text, tbAlias.Text, tbValor.Text };

        SafetyPad.RegAuditTrail(SafetyPad.GetUserLogin(), fieldNames, fieldValues, "IN_PARAMETERS", "C");
    }

    #endregion


   


}