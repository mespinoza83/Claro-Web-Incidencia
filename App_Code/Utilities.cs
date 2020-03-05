using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities{
    
    
    public Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Envia Mensaje de confirmacion.
    /// Fecha: 28-08-2015
    /// Realizado por: XOLO S.A
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="strMessage"></param>
    public static void CreateConfirmBox(Button btn, String strMessage)
    {
        btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");
    }
       
	
}