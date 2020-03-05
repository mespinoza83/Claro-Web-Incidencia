<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="EmailMaintenances.aspx.cs" Inherits="AppPages_EmailMaintenances" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />

    <asp:MultiView runat = "server" ID = "mvEmailMaintenance">
        <asp:View ID="vwEmailMaintenance" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Búsqueda:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbSeachEmail" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchEmail" runat="server" Text="Buscar"
                            Width="100px" CausesValidation="False" 
                            onclick="btnSearchEmail_Click" />
                        <asp:Button ID="btnShowAllEmail" runat="server" Text="Mostrar Todos" 
                            Width="100px" CausesValidation="False" 
                            onclick="btnShowAllEmail_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNewEmail" runat="server" Text="Nuevo"
                            Width="100px" CausesValidation="False" onclick="btnNewEmail_Click" />
                        <asp:ModalPopupExtender ID="btnNewEmail_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEdit" 
                            TargetControlID="btnNewEmail" BackgroundCssClass="FondoAplicacion">
                        </asp:ModalPopupExtender>
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
                        <asp:Panel ID="Panel3" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                            LISTADO DE CORREOS MANTENIMIENTO</asp:Panel>
                        <asp:GridView ID="gvEmail" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" 
                            DataKeyNames="COD_EMAIL_MAINTENANCE,RECORD_STATUS" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" onpageindexchanging="gvResponsibles_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAct" runat="server" ImageUrl="~/Images/icons/02.png"
                                            ToolTip="Activo" />
                                        <asp:Image ID="imgDes" runat="server" ImageUrl="~/Images/icons/09.png"
                                            ToolTip="Inactivo" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>                                
                                <asp:BoundField DataField="EMAIL" HeaderText="Correo" />
                                <asp:BoundField DataField="NAME_COUNTRY" HeaderText="País" />                                
                                 <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditEmail" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditEmail_Click" 
                                            CausesValidation="False" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

             <asp:Panel ID="pnlEdit" runat="server" CssClass="CajaDialogo" Width="300px" Style="display:none;">
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                CORREO MANTENIMIENTO</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width="240" colspan="2" style = "text-align : center">
                         <asp:Label ID="lblMessageSegm" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                       <tr>
                        <td width="50" style = "text-align : right">
                        País:
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlCountries" Width="210px" runat="server"  
                                DataTextField="NAME_CODE" DataValueField="IN_COUNTRY_PK" OnDataBound="ddlCountries_DataBound" > 
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsCountries" runat="server" 
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataStatus" 
                                TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                            </asp:ObjectDataSource>
                               <asp:ObjectDataSource ID="odsAllCountries" runat="server"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByAll" 
                                TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                     <tr>
                        <td width="50" colspan="2" style = "text-align : right">
                            &nbsp;</td>
                      
                    </tr>                    
                    <tr>
                        <td width="50" style = "text-align : right">
                            E-mail:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbEmail" runat="server" IsRequired="True" 
                                Width="200" TextMode="SingleLine" MaxLength="100" />
                            <asp:HiddenField ID="hdfEmail" runat="server" />  
                        </td>
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>                    
                    <tr>
                        <td width="50" style = "text-align : right">
                            Estado:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="210px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" SkinID="btnAceptX" Text="Guardar" 
                                Width="75px" onclick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                                onclick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="50">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

</asp:Content>

