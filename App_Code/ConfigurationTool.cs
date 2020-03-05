using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary Description for ConfigurationTool
/// </summary>
public class ConfigurationTool
{
	public ConfigurationTool()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum hierarchy { Segment = 1, Type = 2, Level = 3 };

    public enum Command { Insert = 1, Update = 2 };


    #region METODOS

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 29/07/2011
    /// Descripción: Función que retorna los resultados de una consulta en un DataTable
    /// </summary>
    /// <param name="query">Consulta de Base de Datos</param>
    /// <returns>Resultados de una consulta en un DataTable</returns>
    public static DataTable GetQueryResult(String query)
    {
        String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
        OracleConnection oracleConnection = new OracleConnection(stringConection);
        OracleCommand command = new OracleCommand(query, oracleConnection);//Crear la consulta
        OracleDataAdapter adapter = new OracleDataAdapter(command);//Establecer la consulta al adaptador
        DataTable source = new DataTable();
        adapter.Fill(source);//Llenar la tabla
        return source;//Retornar la tabla con el resultado de la consulta
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 29/07/2011
    /// Descripción: Función que retorna los resultados de una consulta en un DataTable
    /// </summary>
    /// <param name="query">Consulta de Base de Datos</param>
    /// <returns>Resultados de una consulta en un DataTable</returns>
    public static int ExecQuery(String query)
    {
        String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
        OracleConnection oracleConnection = new OracleConnection(stringConection);
        OracleCommand command = new OracleCommand(query, oracleConnection);//Crear la consulta
        OracleTransaction trans = null;
        int res;

        try
        {
            oracleConnection.Open();
            trans = oracleConnection.BeginTransaction();
            command.Transaction = trans;
            res = command.ExecuteNonQuery();
            trans.Commit();
        }
        catch (Exception ex)
        {
            SafetyPad.SetLogRecord("ConfigurationTool.cs", ex.ToString());
            if (trans != null) trans.Rollback();
            throw ex;
        }
        finally
        {
            if(oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();
        }
        return res;
    }

    /// <summary>
    /// Autor: XOLO S.A.
    /// Fecha: 24/08/2015
    /// Descripción: Función que retorna los resultados de una consulta en un DataTable usando parametro CLOB   /// </summary>
    /// <param name="query"></param>
    /// <param name="strclob"></param>
    /// <returns></returns>

    public static int ExecQueryParam(String query, String strclob)
    {
        String stringConection = ConfigurationManager.ConnectionStrings["cnxAPP_APLICACIONES"].ConnectionString;
        OracleConnection oracleConnection = new OracleConnection(stringConection);
        OracleCommand command = new OracleCommand(query, oracleConnection);//Crear la consulta
        command.Parameters.Clear();
        command.Parameters.Add("Monitoring", OracleType.Clob).Value = strclob;
        OracleTransaction trans = null;
        int res;

        try
        {
            oracleConnection.Open();
            trans = oracleConnection.BeginTransaction();
            command.Transaction = trans;
            res = command.ExecuteNonQuery();
            trans.Commit();
        }
        catch (Exception ex)
        {
            SafetyPad.SetLogRecord("ConfigurationTool.cs", ex.ToString());
            if (trans != null) trans.Rollback();
            throw ex;
        }
        finally
        {
            if (oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();
        }
        return res;
    }


    #endregion

}