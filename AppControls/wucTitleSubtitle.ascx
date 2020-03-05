<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucTitleSubtitle.ascx.cs" Inherits="AppControls_TitleSubtitle" %>
<table width="100%">
        <tr>
            <td align="left" class="rich-panel">
                <p class="rich-panel-header">
                    <strong>
                        <asp:Label ID="lbTitle" runat="server" Text="Título del formulario"></asp:Label></strong></p>
            </td>
        </tr>
        <tr>
            <td>
               <asp:Label ID="lbSubtitle" runat="server" 
                    Text="Subtítulo o instrucciones del formulario" SkinID="lblTittle" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>