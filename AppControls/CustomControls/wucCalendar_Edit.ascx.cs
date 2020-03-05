using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calendar_Edit : System.Web.UI.UserControl
{
    #region VARIABLES

    //Cadena de la Fecha por defecto
    private string strDefaultDate = "01/01/1950";

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ////Colocar la fecha por defecto si los valores no fueron afectados desde el Diseñador
            //if (tbTextEdit.Text != strDefaultDate)
            //{
            //    DefaultDate = Convert.ToDateTime(tbTextEdit.Text);
            //    SelectedDate = Convert.ToDateTime(tbTextEdit.Text);
            //}

            //if (tbTextEdit.Text != strDefaultDate)
            //{
            //    DefaultDate = Convert.ToDateTime(strDefaultDate);
            //    SelectedDate = Convert.ToDateTime(strDefaultDate);
            //}
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Coloca la fecha por defecto del control
    /// Autor: Manuel Gutierrez Rojas
    /// Fecha: 25.08.2011
    /// </summary>
    /// <param name="day">dia</param>
    /// <param name="month">mes</param>
    /// <param name="year">año</param>
    public void setDate(int day, int month, int year)
    {
        string date = day + "/" + month + "/" + year;

        tbTextEdit.Text = date;
        tbTextEdit_CalendarExtender.SelectedDate = Convert.ToDateTime(date);
    }

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Ancho del Control Text Box, por defecto tiene 150 px
    /// </summary>
    public Unit Width
    {
        get { return tbTextEdit.Width; }
        set { tbTextEdit.Width = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Deshabilidar o habilitar el control TextBox
    /// </summary>
    public bool Enable
    {
        get { return tbTextEdit.Enabled; }
        set { tbTextEdit.Enabled = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Determina si el Valor relacionado al control es requerido
    /// </summary>
    public bool IsRequired
    {
        get { return rfvTextEdit.Enabled; }
        set { rfvTextEdit.Enabled = value; }
    }

    /// <summary>
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 22/08/2011
    /// Descripcion: Texto asociado al Control
    /// </summary>
    public string Text
    {
        get { return tbTextEdit.Text; }
        set { tbTextEdit.Text = value; }
    }

    /// <summary>
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 23/08/2011
    /// Descripcion: Fecha seleccionada desde el Control
    /// </summary>
    public DateTime SelectedDate
    {
        get { return Convert.ToDateTime(tbTextEdit_CalendarExtender.SelectedDate); }
        set
        {
            tbTextEdit_CalendarExtender.SelectedDate = value;
            // tbTextEdit.Text = value.ToShortDateString();
        }
    }

    /// <summary>
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 25/08/2011
    /// Descripcion: Fecha por defecto del Control
    /// </summary>
    public DateTime DefaultDate
    {
        get
        {
            String strDate = strDefaultDate;
            return Convert.ToDateTime(strDate);
        }
        set
        {
            tbTextEdit.Text = value.ToShortDateString();
            tbTextEdit_CalendarExtender.SelectedDate = value;
        }
    }


    #endregion
}