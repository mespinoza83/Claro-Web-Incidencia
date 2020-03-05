<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucLoading.ascx.cs" Inherits="UsersControls_wucLoading" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="/WebApp_NotificacionIncidencias/include/css/rich_faces.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="modalBackground">                     
                        <asp:Panel ID="pnlLoading" runat="server" CssClass="modalPopup" >
                            <asp:Label ID="lblLoadingn" runat="server" Font-Bold="True" SkinID="xololabel" Text="Procesando, favor espere...."></asp:Label>
                            <br />
                            <asp:Image ID="imgLoading" runat="server" ImageUrl="~/include/imagenes/ajax-loader.gif" />
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="pnlLoading_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="FondoAplicacion" DynamicServicePath="" Enabled="False" 
                            PopupControlID="pnlLoading" TargetControlID="pnlLoading">
                        </cc1:ModalPopupExtender>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
</table>
