using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

[Serializable()]
public class ParameterAsunto
{   
	
    private string name;
    private decimal orden;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public decimal Orden
    {
        get { return orden; }
        set { orden = value; }
    }

	
}