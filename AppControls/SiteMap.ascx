<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteMap.ascx.cs" Inherits="AppControls_SiteMap" %>
<table cellpadding="0" cellspacing="0" width="100%" border="0">
    <tr>
        <td align="left" height="23" style="padding: 5px 0px 0px 10px; font-size: 11px">
            <%--" /*+ path.Replace(path.Split('/')[path.Split('/').Length - 1], "").Replace(" / ", "&raquo;").Replace(" /", "")*/ + "--%>
            <asp:Literal ID="liSiteMapPath" runat="server"></asp:Literal>
        </td>
    </tr>
   <%-- <tr>
        <td style="padding: 0px 0px 0px 10px; border-bottom: 1px solid #CCCCCC; color: #AAAAAA;
            font-weight: bold; font-size: 13px" width="100%" height="5" align="left" valign="top">
            <asp:Literal ID="liSiteMapLocation" runat="server"></asp:Literal>
        </td>
    </tr>--%>
</table>
