<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeFile="Example.aspx.cs" Inherits="AppPages_Example" %>

<%@ Register Src="../AppControls/CustomControls/wucText_Edit.ascx" TagName="wucText_Edit"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">

    <asp:Panel ID="pnlLevelInformation" runat="server">
        <table width="100%">
            <tr>
                <td colspan="4" style="text-align: right">
                    <asp:Panel ID="Panel5" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                        Datos Generales</asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120">
                    &nbsp;
                </td>
                <td width="120">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    Segmento:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSegment" runat="server" Width="210px" AutoPostBack="True" CssClass = "buttonNormal">
                    </asp:DropDownList>
                </td>
                <td width="120">
                    &nbsp;</td>
                <td width="120">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    Tipo:
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="210px" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <uc1:wucText_Edit ID="wucText_Edit1" runat="server" IsRequired="True" 
                        MaxLength="150" TextMode="SingleLine" Width="200" />
                </td>
                <td style="text-align: right" width="120">
                    &nbsp;</td>
                <td width="120">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    Nivel:
                </td>
                <td>
                    <%--<uc3:wucText_Edit ID="tbLevel" runat="server" Enable="False" IsRequired="True" 
                                                TextMode="SingleLine" Width="200" />--%>
                    <br />
                    <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Text="No existen Niveles para este tipo"
                        Visible="False"></asp:Label>
                </td>
                <td style="text-align: right" width="120">
                    &nbsp;</td>
                <td width="120">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    Motivo:
                </td>
                <td>
                    <asp:DropDownList ID="ddlMotive" runat="server" Width="210px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right" width="120">
                    &nbsp;
                </td>
                <td width="120">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right" width="120">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120">
                    &nbsp;
                </td>
                <td width="120">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:RoundedCornersExtender ID="pnlLevelInformation_RoundedCornersExtender" runat="server"
        BorderColor="SlateGray" Enabled="True" Radius="3" TargetControlID="pnlLevelInformation">
    </asp:RoundedCornersExtender>
</asp:Content>
