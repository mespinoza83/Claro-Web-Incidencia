<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="SolutionResponsibles.aspx.cs" Inherits="AppPages_SolutionResponsibles" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />
<asp:MultiView runat = "server" ID = "mvSolutionResponsibles">
<asp:View ID="vwResponsibles" runat="server">
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
                        <asp:TextBox ID="tbSeachResponsible" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchResponsible" runat="server" Text="Buscar"
                            Width="100px" CausesValidation="False" 
                            onclick="btnSearchResponsible_Click" />
                        <asp:Button ID="btnShowAllResponsible" runat="server" Text="Mostrar Todos" 
                            Width="100px" CausesValidation="False" 
                            onclick="btnShowAllResponsible_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNewResponsible" runat="server" Text="Nuevo"
                            Width="100px" CausesValidation="False" onclick="btnNewResponsible_Click" />
                        <asp:ModalPopupExtender ID="btnNewResponsible_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEdit" 
                            TargetControlID="btnNewResponsible" BackgroundCssClass="FondoAplicacion">
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
                            LISTADO DE RESPONSABLES DE SOLUCIÓN</asp:Panel>
                        <asp:GridView ID="gvResponsibles" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" 
                            DataKeyNames="COD_SOLUTION_RESPONSIBLE,RECORD_STATUS" EmptyDataText="No hay datos que mostrar"
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
                                <asp:BoundField DataField="NAME" 
                                    HeaderText="Nombre del Responsable" />
                                <asp:BoundField DataField="EMAIL" HeaderText="Correo" />
                                <asp:BoundField DataField="NAME_COUNTRY" HeaderText="País" />
                                <asp:BoundField DataField="AREA" HeaderText="Área" />
                                 <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditResponsible" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditResponsible_Click" 
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

             <asp:Panel ID="pnlEdit" runat="server" CssClass="CajaDialogo" Width="300px" Style="display: none;">
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                RESPONSABLES DE SOLUCIÓN</asp:Panel>
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
                                DataTextField="NAME_CODE" DataValueField="IN_COUNTRY_PK"
                               > 
                            </asp:DropDownList><%--ondatabound="ddlCountries_DataBound"--%>
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
                            Nombre:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbName" runat="server" IsRequired="True" MaxLength="60" 
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
                            E-mail:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbEmail" runat="server" IsRequired="True" 
                                Width="200" TextMode="SingleLine" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align : right" width="50">
                            Area:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbArea" runat="server" IsRequired="True" 
                                TextMode="SingleLine" Width="200" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align : right" width="50">
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


