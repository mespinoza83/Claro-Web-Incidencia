using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ni.Com.Xolo.Safetypad.Ws.Interface;
using System.Text;

/// <summary>
/// Descripcion: Clase fachada que contiene métodos publicos para el uso del web service SaftyPad
/// Autor: Ervin Isaba
/// Fecha: 05/04/2010
/// Version: 1.0
/// </summary>
public class SafetyPad
{
	/// <summary>
	/// Descripcion: Inicializa la clase SafetyPad
    /// Autor: Ervin Isaba
    /// Fecha: 05/04/2010
    /// Version: 1.0
	/// </summary>
    public SafetyPad()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Descripcion: Determina si una accion es permitida para el usuario logeado en el sistema
    /// Autor: Ervin Isaba
    /// Fecha: 05/04/2010
    /// Version: 1.0
    /// </summary>
    /// <param name="action">Accion a evaluar</param>
    /// <returns>True si la accion es permitida para el usuario.</returns>
    public static bool IsAllowed(String action)
    {
        try
        {
            HttpContext page = HttpContext.Current;

            SafetypadWS wsInterface = new SafetypadWS();
            Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));
            return wsInterface.IsActionAllowed(page.Session.SessionID, scm.ApplicationName, page.Request.Url.AbsolutePath, action);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Registra una marca de auditoria
    /// Autor: Ervin Isaba
    /// Fecha: 05/04/2010
    /// Version: 1.0
    /// </summary>
    /// <param name="username">Usuario que realiza el registro de auditoria</param>
    /// <param name="fieldNames">Nombre de campos a afectar</param>
    /// <param name="fieldValues">Nombre de valores correspondientes a los campos a afectar</param>
    /// <param name="dbObject">Nombre de tabla a afectar</param>
    /// <param name="crud">C-Crear,  R-Read,  U-Actualizar,  D-Borrar,  A-Access</param>
    public static void RegAuditTrail(String username, String[] fieldNames, String[] fieldValues, String dbObject, String crud)
    {
        try
        {
            HttpContext page = HttpContext.Current;

            SafetypadWS wsInterface = new SafetypadWS();
            Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));
            wsInterface.RegistreAuditTrailEntryByResource(scm.ApplicationCode, scm.ApplicationName, page.Request.Url.AbsolutePath, username, GetIpClient(), page.Session.SessionID, GetServerName(false), crud, GetXML(dbObject, fieldNames, fieldValues), GetServerName(true));
        }
        catch (Exception)
        {

        }      
    }
    
    /// <summary>
    /// Retorna el nombre login del usuario.
    /// Autor: Ervin Isaba
    /// Fecha: 05/04/2010
    /// Version: 1.0
    /// </summary>
    /// <returns>Nombre de login de usuario</returns>
    public static String GetUserLogin()
    {
        try
        {
            SafetypadWS wsInterface = new SafetypadWS();
            String login = "";

            Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));
                     
            var application = wsInterface.GetSessionUser(HttpContext.Current.Session.SessionID, scm.ApplicationCode);
            login = application.Login;
                        
            
            return login;
        }
        catch 
        {
            return "";            
        }
    }

    /// <summary>
    /// Registra las excepciones de la aplicacion en el modulo de seguridad
    /// Autor: Ervin Isaba
    /// Fecha: 05/04/2010
    /// Version: 1.0
    /// </summary>
    /// <param name="source">Nombre de fuente o archivo donde ocurre la excepción</param>
    /// <param name="stackTrace">Pila de la excepción ej.(ex.ToString())</param>
    public static void SetLogRecord(String source, String stackTrace)
    {
        try
        {
            // Establecemos el web service del modulo de seguridad 
            SafetypadWS wsInterface = new SafetypadWS();

            //Declaramos los objetos para la manipulacion de los datos que regresa el modulo de seguridad 
            Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));

            // Registramos el error 
            wsInterface.RegistreException(HttpContext.Current.Session.SessionID, scm.ApplicationName, scm.ApplicationName + "_" + source, DateTime.Now, stackTrace, "", HttpContext.Current.Request.Browser.Browser.ToString(), HttpContext.Current.Request.Url.ToString());
        }
        catch 
        {
        
        }
    }

    /// <summary>
    /// Obtiene el nombre del servidor o la ip del servidor segun el flag
    /// Autor: Ervin Isaba
    /// Fecha:23/08/2010
    /// </summary>
    /// <param name="flag">True para el nombre del servidor, false para el Ip del servidor</param>
    /// <returns>Nombre o Ip de servidor segun flag</returns>
    private static String GetServerName(bool flag)
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
    /// Autor: Ervin Isaba
    /// Fecha:23/08/2010
    /// </summary>
    /// <returns>String con Ip de Cliente</returns>
    private static String GetIpClient()
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
    /// Autor: Ervin Isaba
    /// Fecha:23/08/2010
    /// </summary>
    /// <param name="dbObject">Objeto afectado</param>
    /// <param name="fieldNames">Nombre de campos</param>
    /// <param name="fieldValues">Valores de campos</param>
    /// <returns>XML</returns>
    private static String GetXML(String dbObject, String[] fieldNames, String[] fieldValues)
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


    /// <summary>
    /// Obtiene el valor del Parametro de la seguridad
    /// </summary>
    /// <param name="parametro">1 - Parametro1 / 2 - Parametro2 / 3 - Parametro3</param>
    /// <returns>-1 No encontrado o Error / Valor del Parametro </returns>
    public static String GetParameterXSA_CUENTA(int parametro)
    {
        string result = "-1";
        try
        {            
            SafetypadWS wsInterface = new SafetypadWS();
            Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager scm = ((Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)(System.Configuration.ConfigurationManager.GetSection("safetypad-client")));

            var Data= wsInterface.GetAccount(HttpContext.Current.Session.SessionID, scm.ApplicationName);


            if (parametro == 1)
            {
                result = Data.Parametro1.ToUpper();
            }
            else if (parametro == 2)
            {
                result = Data.Parametro2;
            }
            else if (parametro == 3)
            {
                result = Data.Parametro3;
            }
            else
            {
                result = "-1";
            }                                        
        }
        catch (Exception ex)
        {
            result = "-1";         
        }
        return result;
    }
}
