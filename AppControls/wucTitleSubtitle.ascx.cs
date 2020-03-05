using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppControls_TitleSubtitle : System.Web.UI.UserControl
{

    /// <summary>
    /// Establece un subtitulo a la barra de encabezado.
    /// Autor: Ervin Isaba
    /// Fecha:07/02/2011
    /// </summary>
    /// <param name="subtitle">Texto a escribir en la barra de encabezado</param>
    public void SetTitle(String title)
    {
        lbTitle.Text = title.ToUpper();
    }

    /// <summary>
    /// Establece un subtitulo a la barra de encabezado.
    /// Autor: Ervin Isaba
    /// Fecha:07/02/2011
    /// </summary>
    /// <param name="subtitle">Texto a escribir en la barra de encabezado</param>
    public void SetSubtitle(String subtitle)
    {
        lbSubtitle.Text = subtitle;
    }

    public bool SetSubTitleVisible
    {
        set { lbSubtitle.Visible = value; }
    }

}