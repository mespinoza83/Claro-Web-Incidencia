using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ni.Com.Xolo.Safetypad.Ws.Wrappers;
using Ni.Com.Xolo.Safetypad.Ws.Interface;

public partial class AppControls_SiteMap : System.Web.UI.UserControl
{
    /// <summary>
    /// Evento que se ejecuta en la carga de la página
    /// Autor: Ervin Isaba
    /// Fecha:07/02/2011
    /// </summary>
    /// <param name="sender">Objecto que levanta el evento</param>
    /// <param name="e">Argumentos del evento</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SafetypadWS wsInterface = new SafetypadWS();
            //Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));
            //String siteMapPath = wsInterface.GetMapSite(scm.ApplicationCode, Page.Request.Url.AbsolutePath);
            //CreateSiteMap(siteMapPath);
        }
    }

    /// <summary>
    /// Crea el sitemap de la aplicación con la ruta obtenida.
    /// Autor: Ervin Isaba
    /// Fecha:07/02/2011
    /// </summary>
    /// <param name="siteMapPath">Cadena que contiene la ruta con la que se creará el sitemap</param>
    protected void CreateSiteMap(String siteMapPath)
    {
        liSiteMapPath.Text = siteMapPath.Replace(" / ", "&raquo;").Replace(" /", "");
        //liSiteMapLocation.Text = siteMapPath;
    }
}