using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ni.Com.Xolo.Safetypad.Ws.Interface;

/// <summary>
/// Descripcion: Contiene las funciones que establecen variables globales y configuración de inicio de la aplicación
/// Autor: Ervin Isaba
/// Fecha: 15/06/2010
/// </summary>
public partial class MasterPages_Default : System.Web.UI.MasterPage
{
    SafetypadWS safetypadWS;
    protected void Page_Load(object sender, EventArgs e)
    {
        SafetypadWS wsInterface = new SafetypadWS();
        safetypadWS = new SafetypadWS();
        Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm =
            (Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)System.Configuration.ConfigurationManager.GetSection("safetypad-client");

        /*Ni.Com.Xolo.Safetypad.Ws.Wrappers.Account var =
            (Ni.Com.Xolo.Safetypad.Ws.Wrappers.Account)safetypadWS.GetAccount(Session.SessionID, scm.ApplicationName);*

        /*lbUsername.Text = var.Login;
        lbSesiones.Text = Session["totalActiveSessions"].ToString();
        lbLastIp.Text = Session["lastLoginIp"].ToString();
        lbLastAccess.Text = Session["lastLoginDate"].ToString();

        lbMenu.Text = Session["menu"].ToString();
        lbResources.Text = Session["resources"].ToString();
        lbYear.Text = DateTime.Now.Year.ToString();
*/
        string Host_Usuario = Request.ServerVariables["AUTH_USER"];
        Session["vs_user_name"] = Host_Usuario;
        Session["vs_user_name"] = Session["vs_user_name"].ToString().ToUpper();
        Session["vs_user_name"] = Session["vs_user_name"].ToString().Replace("TCN\\", "");
        Session["vs_user_name"] = Session["vs_user_name"].ToString().Replace("INTRA\\", "");
        Session["vs_user_name"] = Session["vs_user_name"].ToString().ToUpper();
        Session["v_ip"] = Request.ServerVariables["REMOTE_ADDR"];
    }

    
}
