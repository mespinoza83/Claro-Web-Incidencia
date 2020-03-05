<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Parameters.aspx.cs" Inherits="AppPages_Parameters" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />


    <asp:MultiView runat = "server" ID = "mvParameters">
    <asp:View ID="vwparameter" runat="server">
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
                        <asp:TextBox ID="tbSeachParameter" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchParameter" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchSegment_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllParameter" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllSegment_Click" CausesValidation="False" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNewParameter" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewParameter_Click" CausesValidation="False" />
                        <asp:ModalPopupExtender ID="btnNewParameter_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEdit" 
                            TargetControlID="btnNewParameter" BackgroundCssClass="FondoAplicacion">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="btnAsunto_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEditAsunto" 
                            TargetControlID="btnNewParameter"  BackgroundCssClass="FondoAplicacion">
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
                            LISTADO DE PARAMETROS</asp:Panel>
                        <asp:GridView ID="gvParameters" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" DataKeyNames="COD_PARAMETER" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" 
                            onpageindexchanging="gvParameters_PageIndexChanging" >
                            <Columns>            
                                <asp:BoundField DataField="COD_PARAMETER" HeaderText="Código" />                    
                                <asp:BoundField DataField="PARAMETER_ALIAS" HeaderText="Alias" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="PARAMETER_VALUE" HeaderText="Valor" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="STR_TYPE" HeaderText="Tipo" ItemStyle-HorizontalAlign="Left"/>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditParameter" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditParameter_Click" 
                                            CausesValidation="False" Enabled='<%#Eval("PARAMETER_EDITABLE").ToString().Equals("1")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

             <asp:Panel ID="pnlEdit" runat="server" CssClass="CajaDialogo" Width="290px" Style="display:none;">
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                PARAMETROS</asp:Panel>
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
                            Código:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbCodigo" runat="server" IsRequired="false" MaxLength="50" 
                                Width="50" />
                            <asp:HiddenField ID="hdfCodigo" runat="server" />  
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
                            Alias:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbAlias" runat="server" IsRequired="True" MaxLength="50" 
                                Width="200" />
                            <asp:HiddenField ID="hdfAlias" runat="server" />
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
                            Valor:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbValor" runat="server" IsRequired="True" MaxLength="500" 
                                Width="200" />
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

            <%--Panel Edicion Parametro Asunto--%>

        <asp:Panel ID="pnlEditAsunto" runat="server" CssClass="CajaDialogo" Width="290px" Style="display:none;">
             <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                VALOR PARAMETRO ASUNTO </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                NOMBRE VALOR </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                ORDEN </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : left">
                            <asp:Label ID="lblOrdenRepetido" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc3:wucText_Edit ID="tbCodIncidencia" runat="server" IsRequired="true" MaxLength="50" 
                                Width="200" Text="COD_INCIDENCE" Enable="false" />                             
                        </td>
                        <td>
                            <uc3:wucText_Edit ID="tbOrdenCodIncidencia" runat="server" IsRequired="false" MaxLength="1" 
                                Width="60" />  
                        </td>
                    </tr>
                 <tr>
                        <td width="50" style = "text-align : right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc3:wucText_Edit ID="tbPais" runat="server" IsRequired="true" MaxLength="50" 
                                Width="200" Text="CODE_COUNTRY" Enable="false" />                             
                        </td>
                        <td>
                            <uc3:wucText_Edit ID="tbOrdenPais" runat="server" IsRequired="false" MaxLength="1" 
                                Width="60" />  
                        </td>
                    </tr>
                 <tr>
                        <td width="50" style = "text-align : right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc3:wucText_Edit ID="tbTipo" runat="server" IsRequired="true" MaxLength="50" 
                                Width="200" Text="TYPE_NAME" Enable="false" />                             
                        </td>
                        <td>
                            <uc3:wucText_Edit ID="tbOrdenTipo" runat="server" IsRequired="false" MaxLength="1" 
                                Width="60" />  
                        </td>
                    </tr>
                 <tr>
                        <td width="50" style = "text-align : right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc3:wucText_Edit ID="tbSegmento" runat="server" IsRequired="true" MaxLength="50" 
                                Width="200" Text="SEGMENT_NAME" Enable="false" />                             
                        </td>
                        <td>
                            <uc3:wucText_Edit ID="tbOrdenSegmento" runat="server" IsRequired="false" MaxLength="1" 
                                Width="60" />  
                        </td>
                    </tr>
                 <tr>
                        <td width="50" style = "text-align : right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc3:wucText_Edit ID="tbNivel" runat="server" IsRequired="true" MaxLength="50" 
                                Width="200" Text="LEVEL_NAME" Enable="false" />                             
                        </td>
                        <td>
                            <uc3:wucText_Edit ID="tbOrdenNivel" runat="server" IsRequired="false" MaxLength="1" 
                                Width="60" />  
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveAsunto" runat="server" 
                                onclick="btnSaveAsunto_Click" SkinID="btnAceptX" Text="Guardar" Width="75px" />
                            <asp:Button ID="btnCancelAsunto" runat="server" CausesValidation="False" 
                                SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                                onclick="btnCancelAsunto_Click" />
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

