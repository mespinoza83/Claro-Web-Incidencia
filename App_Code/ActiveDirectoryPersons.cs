using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

/// <summary>
/// Autor: Olonyl Rocha Landeros
/// Fecha: 13/06/2011
/// Descripcion: Métodos utilizados para obtener la información Personal de los Usuarios del ActiveDirectory
/// </summary>
public class ActiveDirectoryPerson
{

    #region VARIABLES

    private DirectoryEntry activeDirectoryServer;//ActiveDirectory
    private enum RequiredProperties { samaccountname = 1, displayname, mail, telephonenumber };//Información que va ser mostrada

    #endregion

    #region CONSTRUCTORES

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Constructor de la Clase,crea una conexión al ActiveDirectory del domino al que esta conectado la máquina
    /// </summary>
    public ActiveDirectoryPerson()
    {
        //Autenticación al ActiveDirectory por autenticación de Dominio
        activeDirectoryServer = new DirectoryEntry();
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Constructor de la Clase,crea una conexión al ActiveDirectory de un dominio especificado
    /// </summary>
    /// <param Name="path">Direccion de ActiveDirectory</param>
    /// <param Name="userName">Nombre del Usuario</param>
    /// <param Name="password">Contraseña del Servidor</param>
    public ActiveDirectoryPerson(string path, string userName, string password)
    {
        //Autenticación al ActiveDirectory por medio de Usuario 
        activeDirectoryServer = new DirectoryEntry(path, userName, password);
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Función utilizada para retornar la información Personal de los Usuarios del ActiveDirectory
    /// </summary>
    /// <param name="userName">Nombre del Usauario que va a ser buscado, si se deja en blanco los retorna a todos</param>
    /// <returns>Retornar la información Personal de los Usuarios del ActiveDirectory</returns>
    public List<ActiveDirectoryPersonInformation> GetUsersInformation(string userName)
    {
        //Objeto utilizado para realizar una busqueda personalizada en el ActiveDirectory
        DirectorySearcher search = new DirectorySearcher(activeDirectoryServer);
        //Clase utilizada para almacenar la información personal de los usuarios del ActiveDirectory
        ActiveDirectoryPersonInformation personInformation;
        //Lista de Usuarios del ActiveDirectory con su respetiva información
        List<ActiveDirectoryPersonInformation> listPersonInformation = new List<ActiveDirectoryPersonInformation>();
        //Lista de Propiedades que van ser mostradas
        string[] properties = Enum.GetNames(new RequiredProperties().GetType());

        try
        {
            /*REALIZAR FILTROS DE BUSQUEDA**/
            search.PropertiesToLoad.AddRange(properties);//Definir propiedades que van a ser mostradas
            search.Filter = string.Format("(&(objectCategory=person)(objectclass=User)(|(samaccountname=*{0}*)(displayname=*{0}*)(mail=*{0}*)(telephonenumber=*{0}*)))", userName);//Filtrar información
            search.Filter = search.Filter.Replace("**", "*");//Esto es requerido para cuando no se especifica ningun filtro
            /*============================*/

            //Obtener todos los resultados con los filtros especificados anteriormente
            SearchResultCollection results = search.FindAll();
            // Recorrer los resultados obtenidos para sustraer la información de los Usuarios
            foreach (SearchResult result in results)
            {
                ResultPropertyCollection fields = result.Properties;//Coleccion de Propiedades retornadas
                personInformation = new ActiveDirectoryPersonInformation();//Inicializar objeto que contiene la información del Usuario del ActiveDirectory
                //Recorrer el arreglo de Propiedades que van a ser mostradas y recuperar la información para cada Usuario retornado en la búsqueda
                foreach (string property in properties)
                {
                    //Veroficar si la propiedad existe para este Usuario
                    if (result.Properties[property].Count > 0)
                    {
                        if (property == RequiredProperties.displayname.ToString())
                            personInformation.FullName = result.Properties[property][0].ToString();
                        if (property == RequiredProperties.mail.ToString())
                            personInformation.Email = result.Properties[property][0].ToString();
                        if (property == RequiredProperties.samaccountname.ToString())
                            personInformation.UserName = result.Properties[property][0].ToString();
                        if (property == RequiredProperties.telephonenumber.ToString())
                            personInformation.Telephone = result.Properties[property][0].ToString();
                    }
                }
                listPersonInformation.Add(personInformation);

            }

        }

        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return listPersonInformation;
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Crea una conexión al ActiveDirectory de un dominio especificado
    /// </summary>
    /// <param Name="path">Direccion de ActiveDirectory</param>
    /// <param Name="userName">Nombre del Usuario</param>
    /// <param Name="password">Contraseña del Servidor</param>
    public void SetCustomConnection(string path, string userName, string password)
    {
        //Autenticación al ActiveDirectory por medio de Usuario 
        activeDirectoryServer = new DirectoryEntry(path, userName, password);
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion:Crea  conexión al ActiveDirectory del domino al que esta conectado la máquina
    /// </summary>
    public void SetDefaultConnection(string path, string userName, string password)
    {
        //Autenticación al ActiveDirectory por autenticación de Dominio
        activeDirectoryServer = new DirectoryEntry();
    }

    #endregion

}

/// <summary>
/// Autor: Olonyl Rocha Landeros
/// Fecha: 13/06/2011
/// Descripcion: Clase que contiene la las Propiedades de los Usuarios del ActiveDirectory
/// </summary>
public class ActiveDirectoryPersonInformation
{

    #region VARIABLES

    private string fullName = string.Empty;//Nombre completo del Usuario
    private string userName = string.Empty;//Nombre de Usuario
    private string email = string.Empty;//Email del Usuario 
    private string telephone = string.Empty;//Número de teléfono

    #endregion

    #region PROPIEDADES

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Nombre Completo del Usuario en el ActiveDirectory
    /// </summary>
    public string FullName
    {
        get { return fullName; }
        set { fullName = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Nombre de Usuario en el ActiveDirectory
    /// </summary>
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 13/06/2011
    /// Descripcion: Email de Usuario en el ActiveDirectory
    /// </summary>
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 29/06/2011
    /// Descripcion: Número del Teléfono del Usuario en el ActiveDirectory
    /// </summary>
    public string Telephone
    {
        get { return telephone; }
        set { telephone = value; }
    }

    #endregion
}