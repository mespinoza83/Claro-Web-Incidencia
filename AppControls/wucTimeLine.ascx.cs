using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppControls_TimeLine : System.Web.UI.UserControl
{
    List<Control> controlList;

    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dtInfo = new DataTable();
        //dtInfo.Columns.Add("Nivel");
        //dtInfo.Columns.Add("Llamadas");
        //dtInfo.Columns.Add("Comentario");
        //dtInfo.Columns.Add("Hora");

        if (!IsPostBack)
        {
            controlList = new List<Control>();
        }
        else
        {
            controlList = (List<Control>)Session["Time_Line"];
            if (controlList == null)
                controlList = new List<Control>();
            else
            {
                pnlContainer.Controls.Clear();
                foreach (Control c in controlList)
                    pnlContainer.Controls.Add(c);
            }
        }

        //// dtInfo.Rows.Add("1", "30", "Soporte Revisando", "27/09/2011 21:00:00");
        //dtInfo.Rows.Add("1", "30", "Soporte Revisando", "27/09/2011 1:45:45");
        //dtInfo.Rows.Add("1", "30", "Soporte Revisando", "27/09/2011 1:47:41");
        //dtInfo.Rows.Add("2", "283", "Revisar Solución", "27/09/2011 2:15:00");
        //dtInfo.Rows.Add("3", "1087", "Solucionado, esperando causa de incidencia", "27/09/2011 3:00:12");
        ////dtInfo.Rows.Add("4", "1200", "Prueba de Otro dia", "30/11/2011 4:00:21");
    }

    /// <summary>
    /// Metodo para dibujar la linea de tiempo con los datos
    /// del dataTable
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 27.Sep.2011
    /// </summary>
    /// <param name="table">informacion con los datos de los eventos</param>
    public void CreateTimeline(DataTable table)
    {
        Session["levels"] = string.Empty;

        String s; // Cadena que contendra todos los controles dinamicos
        int counter = 0; //contador de las filas
        string lastRowDate = string.Empty;

        System.Web.UI.WebControls.Image img;

        //Ordenar los Resultados por Hora
        table.DefaultView.Sort = "LOG_DATE ASC";
        controlList.Clear();
        s = "<table width=\"100%\">";
        s += "<tr>";
        //pnlContainer.Controls.Add(new LiteralControl(s)); //Crear tabla para incluir los componentes
        controlList.Add(new LiteralControl(s));

        Decimal eventCount = table.DefaultView.ToTable().Rows.Count;
        //foreach (DataRow row in table.DefaultView.ToTable().Rows)
        
        if (eventCount > 0)
        {
            for (counter = 0; counter < table.DefaultView.ToTable().Rows.Count; counter++)
            {
                DataTable dt = table.DefaultView.ToTable();
                //Variables para los contenidos
                /*string rowLevel = row["Nivel"].ToString();
                string rowCalls = row["Llamadas"].ToString();
                string rowComment = row["Comentario"].ToString();
                string rowDate = row["Hora"].ToString();*/

                string rowLevel = dt.Rows[counter]["LEVEL_NAME"].ToString();
                string rowCalls = dt.Rows[counter]["RECEIVED_CALLS"].ToString();
                string rowComment = dt.Rows[counter]["MONITORING"].ToString();
                string rowDate = dt.Rows[counter]["LOG_DATE"].ToString();
                string rowColor = dt.Rows[counter]["LEVEL_COLOR"].ToString();
                string motiveName = dt.Rows[counter]["MOTIVE_NAME"].ToString();
                string nextRowDate = string.Empty;


                if (counter + 1 < dt.Rows.Count && counter != 0)
                    nextRowDate = dt.Rows[counter + 1]["LOG_DATE"].ToString();
                else
                    if (counter == 0 && dt.Rows.Count>2)
                        nextRowDate = dt.Rows[1]["LOG_DATE"].ToString(); //TODO PROBLEMA CUANDO ES CERO 0

                long dateDiff = 0;//Diferencia entre dos fechas


                if (counter > 0 && counter + 1 < eventCount)
                {
                    //dateDiff = DateTimeExtension.DateDiff(DateInterval.Minute, Convert.ToDateTime(lastRowDate), Convert.ToDateTime(rowDate));
                    dateDiff = DateTimeExtension.DateDiff(DateInterval.Minute, Convert.ToDateTime(rowDate), Convert.ToDateTime(nextRowDate));
                    lastRowDate = rowDate;
                }
                else
                {
                    if (dt.Rows.Count > 2)
                    {
                        dateDiff = DateTimeExtension.DateDiff(DateInterval.Minute, Convert.ToDateTime(dt.Rows[1]["LOG_DATE"].ToString()), Convert.ToDateTime(dt.Rows[0]["LOG_DATE"].ToString()));

                        lastRowDate = rowDate;
                    }
                }

                if (dateDiff < 0)
                    dateDiff = dateDiff * (-1);

                //DateTime t1 = DateTime.ParseExact(rowDate, @"dd\/MM\/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                //width : 30px ;
                s += "<td style = \"text-align:center; border-color: Black; ";
                //s += string.Format("border-width:1px; border-style: dashed; font-size: 9px; width:50px \"> {0}", rowDate);

                //Obtener el Color Actual de Fondo
                Int32[] backColor = new Int32[3]; 
                if (rowColor.Length >= 6)
                {
                    backColor[0] = Convert.ToInt32(rowColor.Substring(0, 2), 16);
                    backColor[1] = Convert.ToInt32(rowColor.Substring(2, 2), 16);
                    backColor[2] = Convert.ToInt32(rowColor.Substring(4, 2), 16);
                }
                else
                {
                    backColor[0] = 0;
                    backColor[1] = 0;
                    backColor[2] = 0; 
                }

                //Invertir el color del fondo, para colocarlo en el color de texto
                Int32[] rgbTextColor = new Int32[3];
                string color = string.Empty;
                if (backColor.Length == 3)
                {
                   /* rgbTextColor[0] = backColor[0] - 256;
                    if (rgbTextColor[0] < 0)
                        rgbTextColor[0] *= -1;

                    rgbTextColor[1] = backColor[1] - 256;
                    if (rgbTextColor[1] < 0)
                        rgbTextColor[1] *= -1;

                    rgbTextColor[2] = backColor[2] - 256;
                    if (rgbTextColor[2] < 0)
                        rgbTextColor[2] *= -1;*/

                    int colorScale = 0;

                    if (backColor[0] < 128)
                        colorScale++;
                    if (backColor[1] < 128)
                        colorScale++;
                    if(backColor[2] < 128)
                        colorScale++;

                    if (colorScale < 2)
                        color = "white";
                    else
                        color = "black";

                }
                else
                {
                    rgbTextColor[0] = 0;
                    rgbTextColor[1] = 0;
                    rgbTextColor[2] = 0;
                }



               // Int32 backColor = int.Parse(rowColor, System.Globalization.NumberStyles.HexNumber) - 256;
                //string textColor = backColor < 128 ?  "black" : "white" ;
                //s += string.Format("border-width:1px; border-style: groove; background-color: #{1}; color: rgb({2},{3},{4}); font-size: 9px; width:50px \"> {0}", rowDate.Replace(" ", "<br />"), rowColor, rgbTextColor[0], rgbTextColor[1], rgbTextColor[2]);
                s += string.Format("border-width:1px; border-style: groove; background-color: #{1}; color: {2}); font-size: 9px; width:50px \"> {0}", rowDate.Replace(" ", "<br />"), rowColor,color);
                s += "<br /> ";
                img = new Image();
                if (motiveName != "FIN")
                    img.ImageUrl = SetEventImage(rowLevel);
                else
                    img.ImageUrl = "../include/imagenes/b01.png";

                img.ToolTip = string.Format("{0}\nLlamadas: {1}\n{2}", rowLevel, rowCalls, rowComment);

                //pnlContainer.Controls.Add(new LiteralControl(s));
                //pnlContainer.Controls.Add(img);
                controlList.Add(new LiteralControl(s));
                controlList.Add(img);

                s = "</td>";
                //pnlContainer.Controls.Add(new LiteralControl(s));
                controlList.Add(new LiteralControl(s));

                if (counter + 1 < table.DefaultView.ToTable().Rows.Count)
                {

                    //pnlContainer.Controls.Add(new LiteralControl(s));
                    controlList.Add(new LiteralControl(s));
                    img = new Image(); //Imagen de Division del tiempo
                    img.ID = "img" + counter;

                    img.ImageUrl = "../include/imagenes/arrow_med.png";
                    img.Height = 2;
                    //pnlContainer.Controls.Add(img);

                    /*Crear Escalas relativas a los tiempos para crear las divisiones*/
                    Int32 timeLength = dateDiff.ToString().Length;
                    long tempResult;

                    //Validar si los eventos tienen la misma hora
                    //if(dateDiff == 0)


                    if (timeLength == 1 && dateDiff != 0)
                    {
                        dateDiff *= 1;
                    }
                    else if (timeLength == 2)
                    {
                        //tempResult = dateDiff / 10;
                        dateDiff = ((dateDiff / 2) + 10);
                    }
                    else if (timeLength == 3)
                    {
                        tempResult = dateDiff / 10;
                        dateDiff = ((dateDiff * 2) * tempResult);
                    }
                    else if (timeLength == 4)
                    {
                        tempResult = dateDiff / 100;
                        dateDiff = ((dateDiff * 1) * tempResult);
                    }
                    else if (timeLength > 4)
                    {
                        dateDiff /= timeLength * 100;
                    }


                    if (counter >= 0 && dateDiff >= 0)
                    {
                        //if (counter == 1)
                        //{
                        //    Image imgTemp = (Image)pnlContainer.FindControl("img0");
                        //    imgTemp.Width = Unit.Pixel(Convert.ToInt32(dateDiff));
                        //}

                        if (dateDiff == 0)
                            s = "<td style= \" background-repeat: repeat-x; background-position: left center; background-image: url('../include/imagenes/arrow_med.png');width:1px;\">";
                        else
                            s = "<td style= \" background-repeat: repeat-x; background-position: left center; background-image: url('../include/imagenes/arrow_med.png');" + string.Format("width:{0}px;\">", dateDiff);

                        //pnlContainer.Controls.Add(new LiteralControl(s));
                        controlList.Add(new LiteralControl(s));
                    }

                    if (counter > 0 && dateDiff < 0)
                    {
                        img.Width = Unit.Pixel(Convert.ToInt32(dateDiff * (-1) / 50));

                        if (counter == 1)
                        {
                            //Image imgTemp = (Image)pnlContainer.FindControl("img0");
                            //imgTemp.Width = Unit.Pixel(Convert.ToInt32(dateDiff));
                        }
                    }

                    if (counter == 0)
                    {
                        //Image imgTemp = (Image)pnlContainer.FindControl("img0");
                        //imgTemp.Width = Unit.Pixel(20);
                    }


                    s = "</td>";
                    //pnlContainer.Controls.Add(new LiteralControl(s));
                    controlList.Add(new LiteralControl(s));


                    s = "<td style = \" width:1px \"> ";
                    //pnlContainer.Controls.Add(new LiteralControl(s));
                    controlList.Add(new LiteralControl(s));
                    Image imgEnd = new Image(); //Imagen del Final de la linea
                    imgEnd.ImageUrl = "../include/imagenes/arrow_end_2.png";
                    //img.Height = 2;
                    if (img.Width != Unit.Pixel(0))
                        controlList.Add(imgEnd);
                        //pnlContainer.Controls.Add(imgEnd);
                    s = "</td>";
                    //pnlContainer.Controls.Add(new LiteralControl(s));
                    controlList.Add(new LiteralControl(s));
                }

            }
            s += "<td>";
            s += "</td>";
            s += "</tr>";
            s += "</table>";
            //pnlContainer.Controls.Add(new LiteralControl(s));
            controlList.Add(new LiteralControl(s));
            pnlContainer.Controls.Clear();
            foreach (Control c in controlList)
                pnlContainer.Controls.Add(c);
        }
        Session["Time_Line"] = controlList;
    }

    /// <summary>
    /// Coloca la imagen en el evento, dependiendo de la categoria del mismo
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 27.Sep.2011
    /// </summary>
    /// <param name="level">Nivel ingresado para el evento</param>
    /// <rereturns></rereturns>
    private string SetEventImage(string level)
    {
        if (!Session["levels"].ToString().Contains(level)) //Si el nivel no ha sido registrado
        {
            Session["levels"] += level;
            return "../include/imagenes/b03.png";
        }
        else
        {
            return "../include/imagenes/b02.png";
        }
    }
}