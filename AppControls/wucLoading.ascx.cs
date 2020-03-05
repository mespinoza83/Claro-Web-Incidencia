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

public partial class UsersControls_wucLoading : System.Web.UI.UserControl
{
    //Declaracion de eventos publicos 
    public event OkEventHandler Ok;
    public delegate void OkEventHandler();

    #region METODO DEL CONTROL IDPOPUP
    //public string ControlIDPopup
    //{
    //    set { this.mpeLoading.TargetControlID = value; }
    //}
    #endregion

    #region MEDOTO SHOW POPUP
    //public void ShowPopup()
    //{
    //    this.mpeLoading.Show();
    //}
    #endregion

    #region MEDOTO HIDE POPUP
    //public void HidePopup()
    //{
    //    this.mpeLoading.Hide();
    //}
    #endregion

    #region PAGE LOAD DEL CONTROL
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxControlToolkit.ModalPopupExtender mpe = (AjaxControlToolkit.ModalPopupExtender)UpdateProgress1.FindControl("pnlLoading_ModalPopupExtender");
        mpe.Enabled = true;
        mpe.Show();
    }
    #endregion 
    
   
}
