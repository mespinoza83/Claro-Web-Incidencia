using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UsersControls_wucMessage : System.Web.UI.UserControl
{
    //Declaracion de eventos publicos 
    public event OkEventHandler Ok;
    public delegate void OkEventHandler();

    #region METODO MENSAJE
    public string Message
    {
        get { return this.lblMensaje.Text.Trim(); }
        set { this.lblMensaje.Text = value; }
    }
    #endregion

    #region METODO TITULO MENSAJE
    public string Title
    {
         get { return this.lblMsgConfirmacion.Text ; }
        set { this.lblMsgConfirmacion.Text = value; }
    }
    #endregion

    #region METODO IMAGE
    public string Image
    {
        set { this.imgMessage.ImageUrl = value; }
    }
    #endregion

    #region METODO DEL CONTROL IDPOPUP
    public string ControlIDPopup
    {
        set { this.mpeMsgConfirmacion.TargetControlID = value; }
    }
    #endregion

    #region MEDOTO SHOW POPUP
    public void ShowPopup()
    {
        this.mpeMsgConfirmacion.Show();
    }
    #endregion

    #region PAGE LOAD DEL CONTROL
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion 

    #region AL DAR CLIC EN EL BOTON ACEPTAR
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        if (Ok != null)
        {
            Ok();
        }
    }
    #endregion
}
