<%@ Page Language="C#" AutoEventWireup="true" CodeFile="no_java.aspx.cs" Inherits="no_java" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <table align="center" style="width:80%;border:0px;margin:20px auto 20px auto; padding:0px;background:#FFFFFF" cellpadding="0" cellspacing="0">
                <tr>
		            <td><br /><br /><br />
		            </td>
		        </tr>
                <tr>
                    <td valign="top" align="center">
                        <asp:Image ID="imbAdvertencia" runat="server" 
                            ImageUrl="~/include/imagenes/Advertencia.gif" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center"><br /><br /><br />
                        <div style="vertical-align:top;min-height:200px;background-color:#FFFFFF;padding:0px 10px 10px 10px;">
                             <h1>Se requiere JavaScript para navegar en la aplicación web</h1>
                             <p>Este explorador web no admite JavaScript o las secuencias de comandos están bloqueadas.<br />
                            <br />Para averiguar si el explorador admite JavaScript o para permitir las secuencias de comandos, consulte la ayuda en pantalla del explorador.</p>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/AppPages/HomePage.aspx">Regresar a Inicio</asp:HyperLink>
                    </td>
                </tr>	
            </table>
    </form>
</body>
</html>
