using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;


[Serializable()]
public class AttachDocIncident
{
    private string attachname;
    private byte[] docbinary;
    private string extesion;
    private string mimetype;

    public string AttachName
    {
        get { return attachname; }
        set { attachname = value; }
    }

    public byte[] DocBinary
    {
        get { return docbinary; }
        set { docbinary = value; }
    }

    public string Extesion
    {
        get { return extesion; }
        set { extesion = value; }
    }

    public string Mimetype
    {
        get { return mimetype; }
        set { mimetype = value; }
    }
        
}