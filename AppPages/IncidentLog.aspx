<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="IncidentLog.aspx.cs" Inherits="IncidentLog"  EnableEventValidation="true" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="../AppControls/CustomControls/wucCalendar_Edit.ascx" tagname="wucCalendar_Edit" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />
<asp:MultiView runat = "server" ID = "mvAffectedClients" ActiveViewIndex="0">
    <asp:View ID="vwClients" runat="server">
        <table width="100%">
            <tr>
                <td colspan="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="70">
                    Fecha inicio:</td>
                <td align="left">
                    <uc4:wucCalendar_Edit ID="tbStartDate" runat="server" IsRequired="True" />
                </td>
                <td align="left">
                    Fecha fin:</td>
                <td align="left">
                    <uc4:wucCalendar_Edit ID="tbEndDate" runat="server" IsRequired="True" />
                </td>
                <td align="left">
                    País:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCountry" Width="140px" runat="server"
                         DataTextField="NAME_CODE" DataValueField="IN_COUNTRY_PK"
                                                ondatabound="ddlCountry_DataBound" DataSourceID="odsCountries" 
                        >
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsCountries" runat="server"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataStatus" 
                        TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                                            </asp:ObjectDataSource>
                </td>
                <td align="right">
                    <asp:Button ID="btnGenerate" runat="server" CssClass="buttonNormal" 
                        onclick="btnGenerate_Click" Text="Generar" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="vwReport" runat="server">
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnBack" runat="server" CssClass="buttonNormal" 
                        onclick="btnBack_Click" Text="Regresar" />
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="rvIncidentLog" runat="server" Width="100%" Height="5%" 
                        ShowDocumentMapButton="False" >
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:View>
        </asp:MultiView>
</asp:Content>

