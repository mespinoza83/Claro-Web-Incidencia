<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="HtmlEditor.aspx.cs" Inherits="AppPages_HtmlEditor" %>
<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%--<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>




<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
 <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />

<asp:Panel ID="pnlLoad" runat="server">
<table width="80%">
    <tr>
         <td colspan="2">
        
        </td>
       
    </tr>
    <tr>
         <td style="width:10%">
            <asp:Label ID="lblClasif" runat="server" Text="Seleccione la Clasificación:"></asp:Label>
        </td>
        <td style="width:30%">
            <asp:DropDownList ID="ddlClasif" runat="server" AutoPostBack="true"
                onselectedindexchanged="ddlClasif_SelectedIndexChanged">
                <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                <asp:ListItem Value="M">Mantenimiento</asp:ListItem>
                <asp:ListItem Value="I">Incidencia</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
        <tr>
         <td>
        
        </td>
        <td>
        </td>
    </tr>
    <tr>
         <td colspan="2">
         <asp:GridView ID="grvHtml" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" DataKeyNames="COD_HTML_PK,DESCRIPTION_HTML,CLASIFICATION,TYPE_FORMAT,STATUS_HTML,STRCLASIFICATION,STRTYPE" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" 
                            onpageindexchanging="grvParameters_PageIndexChanging" >
                            <Columns>       
                                 
                                <asp:BoundField DataField="COD_HTML_PK" HeaderText="Identificador" />                    
                                <asp:BoundField DataField="DESCRIPTION_HTML" HeaderText="Descripción" 
                                    ItemStyle-HorizontalAlign="Left">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STRCLASIFICATION" HeaderText="Clasificación" 
                                    ItemStyle-HorizontalAlign="Left">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STRTYPE" HeaderText="Tipo" 
                                    ItemStyle-HorizontalAlign="Left">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditReg" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditReg_Click" 
                                            CausesValidation="False" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>


        </td>
     </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
         <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
         <td>
        </td>
        <td>
        </td>
    </tr>
</table>
</asp:Panel>

<asp:Panel ID="pnlEditor" runat="server">
 <table width="80%">
    <tr>
        <td style="width:10%" ><asp:Label ID="lblDescript" runat="server" Text="Descripción"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDescript" runat="server" Rows="5" TextMode="MultiLine" Width="131px"></asp:TextBox>
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td style="width:10%"><asp:Label ID="lblClas" runat="server" Text="Clasificación"></asp:Label>
        </td>
        <td><asp:TextBox ID="txtClas" runat="server" Enabled="false"></asp:TextBox>
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td style="width:10%"><asp:Label ID="lblType" runat="server" Text="Tipo" Enabled="false"></asp:Label>
        </td>
        <td><asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <%--<CKEditor:CKEditorControl ID="CKEdHtml" runat="server" Height="400" BasePath="~/ckeditor">
		        Escriba su texto aquí
		        </CKEditor:CKEditorControl>--%>
                <HTMLEditor:Editor runat="server" Width="100%" ID="txtContenido" Height="350px" />
        </td>
        
    </tr>
    <tr>
        <td>
         <asp:Button ID="btnSave" runat="server" 
                                        onclick="btnSave_Click" SkinID="btnAceptX" Text="Guardar" Width="75px" />
        </td>
        <td>
        <asp:Button ID="btnLoad" runat="server" 
                                         SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                onclick="btnCancel_Click" />
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td>
            <%--<asp:TextBox ID="txtTest" runat="server"></asp:TextBox>--%>

        </td>
        <td>
        </td>
         <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
        </td>
         <td>
        </td>
    </tr>
</table>
   </asp:Panel>
</asp:Content>

