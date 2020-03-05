<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Operations.aspx.cs" Inherits="AppPages_Operations" MasterPageFile="~/MasterPages/Default.master" %>

<%@ Register Src="../AppControls/wucMessage.ascx" TagName="wucMessageControl" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <asp:Panel ID="pnlLevelInformation" runat="server">    
        <table style="padding-left:40px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td colspan="4" style="text-align: right">
                    <asp:Panel ID="Panel5" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                        Soluci&oacute;n del Incidente</asp:Panel>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                 <td>Incidencia</td>
                 <td>
                     <table cellpadding="0" cellspacing="0" border="0">
                       <tr>
                               <td><asp:TextBox ID="txtIncident" CssClass="bordes" MaxLength="358" Width="400" 
                                       runat="server" ontextchanged="txtIncident_TextChanged"></asp:TextBox>
                                   <asp:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtIncident" ServiceMethod="GetCompletionList" MinimumPrefixLength="1" CompletionInterval="200" runat="server" OnClientItemSelected="AutoCompleteEx_OnClientItemSelected">
                                   </asp:AutoCompleteExtender></td>
                               <td>&nbsp;*</td>
                       </tr>
                     </table>
                     <asp:HiddenField ID="hdfEdit" runat="server" />
                     <asp:HiddenField ID="hdfEditRol" runat="server" />
                 </td>
            </tr>
            <tr id="trState" visible="false" runat="server">
                 <td>Estado Actual:  </td>
                 <td style=" font-weight:bold " ><asp:Label ID="lblState" runat="server"></asp:Label><asp:HiddenField ID="hidIncidentNumber" runat="server" /><asp:HiddenField ID="hdfCountry" runat="server" /></td>
            </tr>
             <tr id="trSegment" visible="false" runat="server">
                 <td>Segmento </td>
                 <td><asp:DropDownList ID="ddlSegment" runat="server" Width="210px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged">
                                            </asp:DropDownList></td>
            </tr>
             <tr id="trType" visible="false" runat="server">
                 <td>Tipo </td>
                 <td><asp:DropDownList ID="ddlType" runat="server" Width="210px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList></td>
            </tr>
   <%--          <tr id="trStatus" visible="false" runat="server">
                 <td>Estado</td>
                 <td><asp:DropDownList ID="ddlStatus" runat="server" Width="210px">
                                            </asp:DropDownList>
                 </td>
            </tr>--%>
            <tr>
                 <td style="text-align: left; vertical-align:top">Comentario</td>
                 <td>
                     <table cellpadding="0" cellspacing="0" border="0">
                        <tr style="padding-top:5px">                            
                            <td align="left">
                                  <asp:TextBox ID="txtComment" Rows="10" Columns="100" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td width="0">&nbsp;</td>
                            <td width="0">&nbsp;</td>
                        </tr>
                        <tr>                            
                            <td align="right">
                                <%--<asp:Label ID="lblMessage" runat="server"></asp:Label>--%>
                                <asp:Button ID="sendButton" Text="Enviar Comentario" runat="server" Enabled="false" onclick="sendButton_Click" OnClientClick="this.disabled=true" UseSubmitBehavior="False" /><asp:Button ID="cleanButton" Text="Limpiar" runat="server" onclick="cleanButton_Click" /></td>
                        </tr>
                     </table>
                 </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                 <td></td>
                 <td><asp:UpdatePanel ID="UpdatePanel1" runat="server">                  
                               <ContentTemplate>
                                    <asp:GridView ID="grvOperations" runat="server" AutoGenerateColumns="False" 
                                                    BorderColor="#C5D9EB" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Font-Size="12px" AllowPaging="True" Width="100%" OnPageIndexChanging="grvOperations_PageIndexChanging">
                                                    <Columns>                                                    
                                                        <asp:BoundField DataField="LEVEL_NAME" HeaderText="NIVEL" SortExpression="LEVEL_NAME" ItemStyle-Width="90" ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="USERNAME" HeaderText="USUARIO" SortExpression="USERNAME" ItemStyle-Width="90" ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="LOG_DATE" HeaderText="FECHA" SortExpression="LOG_DATE" ItemStyle-Width="150" ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="MONITORING" HeaderText="COMENTARIO" SortExpression="MONITORING" ItemStyle-Width="500" ItemStyle-HorizontalAlign="center"/>                                                        
                                                    </Columns>
                                                    <RowStyle ForeColor="#330099" />
                                                                            <FooterStyle BackColor="#EEEEEE" ForeColor="#330099" />
                                                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    </asp:GridView>
                              </ContentTemplate>
                             </asp:UpdatePanel></td>
            </tr> 
        </table>
      
        <br/>
        <br/>
      
        
      
    </asp:Panel>
</asp:Content>
