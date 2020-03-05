using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using Ni.Com.Xolo.Safetypad.Ws.Interface;
using Ni.Com.Xolo.Safetypad.Ws.Wrappers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// Descripcion: inicializa la clase AuditoryTrails.
/// Autor: C. Peralta.
/// Fecha: 04/07/2011.
/// Nombre del Proyecto: Certificados de Regalos.
/// </summary>
public class AuditoryTrails
{
    /// <summary>
    /// Instanciamos la clase  y creación de variables.
    /// Autor: AJA.
    /// Fecha: 04/07/2011.
    /// </summary>
    private SafetypadWS wsModSeg;
    public AuditoryTrails()
    {

        //
        // TODO: Add constructor logic here
        //
    }
    
    /// <summary>
    /// Obtiene el nombre del servidor o la ip del servidor segun el flag
    /// Autor: AJA
    /// Fecha: 14/06/2011
    /// </summary>
    /// <param name="flag">True para el nombre del servidor, false para el Ip del servidor</param>
    /// <returns>Nombre o Ip de servidor segun flag</returns>
    public String GetServerName(bool flag)
    {
        String webServerName = "";
        if (flag)
            webServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        else
            webServerName = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        return webServerName;
    }

    /// <summary>
    /// Obtiene el ip del Cliente
    /// Autor: AJA
    /// Fecha: 14/06/2011
    /// </summary>
    /// <returns>String con Ip de Cliente</returns>
    public String GetIpClient()
    {

        String varIp = "";
        varIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (varIp == null || varIp.Length == 0)
            return HttpContext.Current.Request.UserHostAddress;
        else
            return varIp;

    }

    /// <summary>
    /// Obtiene el objeto Xml armado
    /// Autor: AJA
    /// Fecha: 14/06/2011
    /// </summary>
    /// <param name="dbObject">Objeto afectado</param>
    /// <param name="fieldNames">Nombre de campos</param>
    /// <param name="fieldValues">Valores de campos</param>
    /// <returns>XML</returns>
    public String GetXML(String dbObject, String[] fieldNames, String[] fieldValues)
    {
        StringBuilder xml = new StringBuilder();
        xml.Append("<Log>");
        xml.Append("<Objeto>");
        xml.Append(dbObject);
        xml.Append("</Objeto>");
        xml.Append("<Campos>");

        for (int i = 0; i < fieldNames.Length; i++)
        {
            xml.Append("<Campo>");
            xml.Append("<Nombre>");
            xml.Append(fieldNames[i]);
            xml.Append("</Nombre>");
            xml.Append("<Valor>");
            xml.Append(fieldValues[i]);
            xml.Append("</Valor>");
            xml.Append("</Campo>");
        }

        xml.Append("</Campos>");
        xml.Append("</Log>");

        return xml.ToString();
    }

}