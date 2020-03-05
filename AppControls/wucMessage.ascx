<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMessage.ascx.cs" Inherits="UsersControls_wucMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="/WebApp_NotificacionIncidencias/include/css/rich_faces.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Skin/EstilosAjax.css" rel="stylesheet" type="text/css" />
<asp:Label ID="lblPopUp" runat="server" Text=""></asp:Label>
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="left">
            <asp:Panel ID="pnlMsgConfirmacion" runat="server" BackColor="White" BorderWidth="1px" CssClass="CajaDialogo" Style="display: none; width:350px;">
                <asp:Panel ID="pnlDragMsgConfirmacion" runat="server" Style="cursor: move;" SkinID="pnlTittleGrid">
                    <asp:Label ID="lblMsgConfirmacion" runat="server" Font-Bold="true" SkinID="xololabel"
                        Text="Mensaje"></asp:Label>
                </asp:Panel>
                <br />
                <div style="overflow: auto; ">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Image ID="imgMessage" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" CssClass="lbl_message" Width="100%"></asp:Label>
                            <br />
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                    </table>
                 </div>
                    <table width="100%>
                    <tr>
                        <td align="right">
                            <caption>
                                &nbsp;
                                </td>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnAccept" runat="server" CausesValidation="False" 
                                            OnClick="btnAccept_Click" SkinID="btnAceptX" Text="ACEPTAR" ToolTip="Aceptar" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </caption>
                </table>
        
            </asp:Panel>
            <cc1:ModalPopupExtender ID="mpeMsgConfirmacion" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="False" PopupControlID="pnlMsgConfirmacion" PopupDragHandleControlID="pnlDragMsgConfirmacion"
                Enabled="True" DynamicServicePath="" TargetControlID="lblPopUp">
            </cc1:ModalPopupExtender>
        </td>
    </tr>
</table>
