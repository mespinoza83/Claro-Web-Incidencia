<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="AffectedClients.aspx.cs" Inherits="AppPages_AffectedClients"  EnableEventValidation="true" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />
<asp:MultiView runat = "server" ID = "mvAffectedClients">
<asp:View ID="vwClients" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" width="60">
                        Búsqueda:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbSeachClient" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchClient" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchSegment_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllClient" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllSegment_Click" CausesValidation="False" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNewClient" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewClient_Click" CausesValidation="False" />
                        <asp:ModalPopupExtender ID="btnNewClient_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEdit" 
                            TargetControlID="btnNewClient" BackgroundCssClass="FondoAplicacion">
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
                            LISTADO DE CLIENTES AFECTADOS</asp:Panel>
                        <asp:GridView ID="gvAffectedClients" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" DataKeyNames="COD_AFFECTED_CLIENT,RECORD_STATUS" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" 
                            onpageindexchanging="gvAffectedClients_PageIndexChanging" >
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
                                <asp:BoundField DataField="NAME" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditClient" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditAffectedClient_Click" 
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

             <asp:Panel ID="pnlEdit" runat="server" CssClass="CajaDialogo" Width="290px" Style="display: none;">
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                CLIENTES AFECTADOS</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : right">
                            Nombre:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbName" runat="server" IsRequired="True" MaxLength="50" 
                                Width="200" />
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
                            <asp:Button ID="btnSave" runat="server" 
                                onclick="btnSave_Click" SkinID="btnAceptX" Text="Guardar" Width="75px" />
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

