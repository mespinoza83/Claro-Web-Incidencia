<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeFile="Segments.aspx.cs" Inherits="AppPages_Segments" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register src="../AppControls/wucTitleSubtitle.ascx" tagname="wucTitleSubtitle" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../AppControls/wucMessage.ascx" tagname="wucMessageControl" tagprefix="uc2" %>

<%@ Register src="../AppControls/CustomControls/wucText_Edit.ascx" tagname="wucText_Edit" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

    <style type="text/css">
        .style1
        {
            text-align: right;
        }
		
		#contenido label {
		display: inline !important;
		float: none  !important;
		}
		
		
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />
    <asp:ObjectDataSource ID="odsCountries" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataStatus" 
        
        TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter"></asp:ObjectDataSource>
		
		
			
    <asp:MultiView  ActiveViewIndex="-1" runat="server" ID="mvSegments">
        <asp:View ID="vSegment" runat="server">
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
                        <asp:TextBox ID="tbSearchSegment" SkinID="TextBoxDefault" runat="server" Width="150px" 
                            ></asp:TextBox>
                        <asp:Button ID="btnSearchSegment" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchSegment_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllSegment" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllSegment_Click" CausesValidation="False" />
                    </td>
                    <td align="right">
                         <asp:Button ID="btnNewSegment" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewSegment_Click" CausesValidation="False" />
                        <asp:ModalPopupExtender ID="btnNewSegment_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEditSegment" 
                            TargetControlID="btnNewSegment" BackgroundCssClass="FondoAplicacion">
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
                            LISTADO DE SEGMENTOS</asp:Panel>
                        <asp:GridView ID="gvSegment" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" DataKeyNames="COD_SEGMENT,SEGMENT_NAME,RECORD_STATUS" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" onpageindexchanging="gvSegment_PageIndexChanging" >
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
                                <asp:BoundField DataField="SEGMENT_NAME" HeaderText="Nombre del Segmento" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Descripcion" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditSegment" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditSegment_Click" 
                                            CausesValidation="False" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnAddSegment" runat="server" ImageUrl="~/include/imagenes/ico_add.png"
                                            ToolTip="Añadir Tipos" onclick="ibtnAddSegment_Click" 
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

			<asp:Panel ID="pnlEditSegment"  DefaultButton="btnSaveSegment" runat="server" CssClass="CajaDialogo" Width="290px" Style="display: none;"> 
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                <asp:Label ID="lbTitleSegment" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                   <tr>
                       <%-- <td width="50" style = "text-align : right"  colspan="2">--%>
                       <td colspan="2">
                         <asp:Panel ID="pnlMesg" runat="server" HorizontalAlign="Center" >
                            <asp:Label ID="lblMessageSegm" runat="server" ForeColor="#FF3300"></asp:Label>
                          
                            </asp:Panel>
                        </td>
                        
                    </tr>
                    <tr>
                        <td width="50" style = "text-align : right">
                            Nombre:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbSegmentName"  runat="server" IsRequired="True" MaxLength="50" 
                                Width="200" TextMode="SingleLine" />
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
                            Descripción:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbSegmentDescription" runat="server" IsRequired ="True" 
                                MaxLength = "200" Width = "210" TextMode ="MultiLine" />
                             
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
                            <asp:DropDownList ID="ddlStatusSegment"  runat="server" Width="210px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td width="50" style = "text-align : right">
                            País:
                        </td>
                        <td>                            
                            <asp:CheckBoxList ID="ckbCountries"  runat="server" CssClass="chkbox"
							 DataSourceID="odsCountries" TextAlign="Right"
                                DataTextField="NAME_CODE" DataValueField="IN_COUNTRY_PK" 
								 >
                            </asp:CheckBoxList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                        
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveSegment"  runat="server" 
                                onclick="btnSaveSegment_Click" SkinID="btnAceptX" Text="Guardar" 
                                Width="75px" />
                            <asp:Button ID="btnCancelSegment"  runat="server" CausesValidation="False" 
                                SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                                onclick="btnCancelSegment_Click" />
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

         <asp:View ID="vTypes" runat="server">
            <table width="100%">
                <%--<tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>--%>
                <tr>
                     <td align="lefth" width="450">
                        <asp:Label ID="lblSegmentSelect" runat="server" Font-Bold="True" 
                            Font-Size="Medium" ForeColor="#0099FF"></asp:Label>
                    </td>
                    <%--
                    <td align="left">
                        <asp:TextBox ID="tbSearchType" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchType" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchType_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllTypes" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllTypes_Click" CausesValidation="False" />
                    </td>--%>
                    <td align="right">
                        <asp:Button ID="btnSegment" runat="server" Text="Segmentos" Width="100px" 
                            onclick="btnSegment_Click" CausesValidation="False" />
                      <%--  <asp:Button ID="btnNewType" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewType_Click" CausesValidation="False" />
                            <asp:ModalPopupExtender ID="btnNewType_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEditType" 
                            TargetControlID="btnNewType" BackgroundCssClass="FondoAplicacion">
                        </asp:ModalPopupExtender>--%>
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
                        <asp:Panel ID="Panel1" runat="server" SkinID="pnlTittleGrid" Width="200px" Style="text-align: center">
                            SELECCIONE EL PAIS</asp:Panel>
                            <asp:Panel ID="pnlCountries" runat="server" Width="222px" Style="text-align: center">
                            <asp:GridView ID="grvCountries" runat="server" Width="200px" 
                            DataKeyNames="IN_COUNTRY_PK,NAME_COUNTRY,SEGMENT_NAME,COUNTRY_STATUS" 
                            EmptyDataText="No hay datos que mostrar" AutoGenerateColumns="False" 
                            EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField DataField="NAME_COUNTRY" HeaderText="País" />
                                <asp:TemplateField HeaderText="Nivel">
                                 <ItemTemplate>
                                        <asp:ImageButton ID="imbAddLevel" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Agregar Tipos" onclick="imbAddLevel_Click" 
                                            CausesValidation="False" />
                                    </ItemTemplate>                               

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                           </asp:Panel>
                        
                        <asp:Panel ID="pnlTypes" runat="server"  Width="100%" >
                           <asp:Panel ID="pnlMsgType" runat="server" CssClass="pnlTittleGrid"  Width="100%" >
                            <br />
                            <asp:Label ID="lblTextCountry" runat="server" Font-Bold="True"></asp:Label>
                            <hr style="border-color: #0099FF; background-color: #0099FF" />
                        </asp:Panel>
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
                        <asp:TextBox ID="tbSearchType" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchType" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchType_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllTypes" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllTypes_Click" CausesValidation="False" />
                           
                    </td>
                    <td align="right">
                           <asp:Button ID="btnCanType" runat="server" CausesValidation="False" 
                                Text="Cancelar" Width="75px" onclick="btnCanType_Click" 
                                 />
                          <asp:Button ID="btnNewType" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewType_Click" CausesValidation="False" />
                            <asp:ModalPopupExtender ID="btnNewType_ModalPopupExtender" runat="server" 
                            DynamicServicePath="" Enabled="False" PopupControlID="pnlEditType" 
                            TargetControlID="btnNewType" BackgroundCssClass="FondoAplicacion">
                        </asp:ModalPopupExtender>
                    </td>
                     
                </tr>
                   <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>          
            </table>
            <asp:Panel ID="pnlgrvTypes" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                            LISTADO DE TIPOS</asp:Panel>
<asp:GridView ID="gvTypes" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" DataKeyNames="COD_TYPE,TYPE_NAME,RECORD_STATUS" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" onpageindexchanging="gvTypes_PageIndexChanging" >
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
                                <asp:BoundField DataField="TYPE_NAME" HeaderText="Nombre del Tipo" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Descripcion" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditType" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditType_Click" CausesValidation="False" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agregar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnAddType" runat="server" ImageUrl="~/include/imagenes/ico_add.png"
                                            ToolTip="Añadir Niveles" onclick="ibtnAddType_Click" 
                                            CausesValidation="False" style="height: 16px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        </asp:Panel>
                        
                    </td>
                </tr>
                <caption>
                 
                </caption>
            </table>
            <table width="100%">
                <tr>
                    <td>
                  
            </td>
            </tr>
            </table>
            <asp:Panel ID="pnlEditType" runat="server" CssClass="CajaDialogo" Width="290px" Style="display: none;" >
            <%--Style="display: none;"--%>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel8" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                <asp:Label ID="lbTitleType" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2" style = "text-align : Center" >
                        <asp:Label ID="lblMessageTy" runat="server" ForeColor="#FF3300" Text=""></asp:Label>
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
                            <uc3:wucText_Edit ID="tbTypeName" runat="server" IsRequired="True" MaxLength="50" 
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
                            Descripción:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbTypeDescription" runat="server" IsRequired="True" 
                                Width="210" TextMode="MultiLine" MaxLength="200"/>
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
                            <asp:DropDownList ID="ddlStatusType" runat="server" Width="210px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveType" runat="server"  
                                onclick="btnSaveType_Click" SkinID="btnAceptX" Text="Guardar" 
                                Width="75px" />
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                                onclick="btnCancelType_Click" />
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

        <asp:View ID="vLevels" runat="server">
             <asp:Panel ID="pnlLevelCountry" runat="server"  Width="100%" >
                           <asp:Panel ID="pnlTitleLevel" runat="server" CssClass="pnlTittleGrid"  Width="100%" >
                            <br />
                            <asp:Label ID="lblCountryText" runat="server" Font-Bold="True"></asp:Label>
                            <hr style="border-color: #0099FF; background-color: #0099FF" />
                        </asp:Panel>
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
                             <asp:TextBox ID="tbSearchLevel" runat="server" Width="150px"></asp:TextBox>
                        <asp:Button ID="btnSearchLevel" runat="server" Text="Buscar"
                            Width="100px" onclick="btnSearchLevel_Click" CausesValidation="False" />
                        <asp:Button ID="btnShowAllLevels" runat="server" Text="Mostrar Todos" 
                            Width="100px" onclick="btnShowAllLevels_Click" CausesValidation="False" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnTypes" runat="server" Text="Tipos" Width="100px" 
                            onclick="btnTypes_Click" CausesValidation="False" />
                        <asp:Button ID="btnNewLevel" runat="server" Text="Nuevo"
                            Width="100px" onclick="btnNewLevel_Click" CausesValidation="False" />
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
                        <asp:Panel ID="Panel4" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                            LISTADO DE NIVELES</asp:Panel>
                        <asp:GridView ID="gvLevels" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" 
                            DataKeyNames="COD_LEVEL,RECORD_STATUS,LEVEL_COLOR" EmptyDataText="No hay datos que mostrar"
                            AllowPaging="True" onpageindexchanging="gvLevels_PageIndexChanging" >
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
                                <asp:BoundField DataField="LEVEL_NAME" HeaderText="Nombre del Nivel" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Descripcion" />
                                <asp:BoundField DataField="WAIT_TIME" HeaderText="Tiempo de Espera (min)" />
                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlColor" runat="server">
                                            &nbsp;
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditLevel" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" onclick="ibtnEditLevel_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

            </asp:Panel>
        </asp:View>


        <asp:View runat ="server" ID="vEditLevels">
        <asp:TabContainer runat = "server" ActiveTabIndex="1" Width = "100%" 
                CssClass="ajax__myTab">
        <asp:TabPanel ID = "TabPanel1" runat="server" HeaderText="Información">
        <ContentTemplate>
        <table width="100%">
        <tr>
        <td width="30%">
            &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="30%">
                &nbsp;</td>
        </tr>
            <tr>
                <td width="30%">
                </td>
                <td>
                    <asp:Panel ID="pnlLevelInformation" style="border: solid 1px SlateGray;" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="text-align: right">
                                    <asp:Panel ID="Panel5" runat="server" SkinID="pnlTittleGrid" 
                                        style="text-align: center">
                                        Información del Nivel</asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Nombre:</td>
                                <td>
                                    <asp:TextBox ID="tbLevelName" runat="server" ReadOnly="True" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Secuencia:</td>
                                <td>
                                    <asp:TextBox ID="tbSequence" runat="server" ReadOnly="True" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Descripción:</td>
                                <td>
                                    <asp:TextBox ID="tbLevelDescription" runat="server" SkinID="xolotextarea" 
                                        TextMode="MultiLine" Width="200px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Tiempo de Espera(min):</td>
                                <td>
                                    <uc3:wucText_Edit ID="tbLevelWaitTime" runat="server" MaxLength = "4" IsRequired="True" 
                                        Width="200" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Color:</td>
                                <td>
                                    <asp:TextBox ID="tbLevelColor" runat="server"  
                                        Width="200px" ReadOnly="True"></asp:TextBox>
                                    <asp:ColorPickerExtender ID="tbLevelColor_ColorPickerExtender" runat="server" 
                                        Enabled="True" SampleControlID="pnlColor"  PopupPosition = "TopRight"
                                        TargetControlID="tbLevelColor" 
                                        OnClientColorSelectionChanged="colorChanged" >
                                    </asp:ColorPickerExtender>
                                    <asp:HiddenField ID="hfColor" runat="server" />
                                    <br />
                                    <asp:Panel ID="pnlColor" runat="server" Height="10px" Width="210px">
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    Estado:</td>
                                <td>
                                    <asp:DropDownList ID="ddlLevelState" runat="server" Width="210px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="120">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td width="30%">
                </td>
            </tr>
            <tr>
                <td width="30%">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td width="30%">
                    &nbsp;</td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel ID = "TabPanel2" runat="server" HeaderText="Correos">
        <ContentTemplate>
        <table width="100%">
        <tr>
        <td width="30%">
            &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="30%">
                &nbsp;</td>
        </tr>
            <tr>
                <td width="30%">
                </td>
                <td>
                    <asp:Panel ID="pnlMailList" runat="server" style="border: solid 1px SlateGray;">
                         <table width="100%">
                            <tr>
                                <td colspan="3" style="text-align: right">
                                    <asp:Panel ID="Panel7" runat="server" SkinID="pnlTittleGrid" 
                                        style="text-align: center">
                                        Lista de correos asociados</asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="20%">
                                    &nbsp;</td>
                                <td style="text-align: left" width="230">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right" width="20%">
                                    Añadir:</td>
                                <td style="text-align: left" width="238">
                                    <uc3:wucText_Edit ID="tbNewMail" runat="server" MaxLength="100" Width="200" />
                                    <asp:ImageButton ID="ibtnAddMail" runat="server" 
                                        ImageUrl="~/include/imagenes/ico_guardar.gif" ToolTip="Guardar Correo" 
                                        CausesValidation="False" onclick="ibtnAddMail_Click" />
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            </table>
                            <table>
                            <tr>
                            <td style = "width:20%" class="style1">Correos:</td>
                                <td colspan="2" rowspan="3" style="text-align: left" width="230px">
                                    <asp:ListBox ID="listMails" runat="server" Width="210px">
                                    </asp:ListBox>
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <asp:ImageButton ID="ibtnEditMail" runat="server" 
                                        ImageUrl="~/include/imagenes/application-edit-icon.png" 
                                        ToolTip="Editar Correo" CausesValidation="False" 
                                        onclick="ibtnEditMail_Click" />
                                    <asp:ModalPopupExtender ID="ibtnEditMail_ModalPopupExtender" runat="server" 
                                        DynamicServicePath="" Enabled="False" TargetControlID="ibtnEditMail" 
                                        BackgroundCssClass="FondoAplicacion" PopupControlID="pnlEditEmail">
                                    </asp:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                            <td style = "width:20%" class="style1"></td>
                                <td colspan="2" style="text-align: left" width="230px">
                                    <asp:ImageButton ID="ibtnDeleteMail" runat="server" 
                                        ImageUrl="~/include/imagenes/ico_delete.png" ToolTip="Borrar Correo" 
                                        CausesValidation="False" onclick="ibtnDeleteMail_Click" />
                                </td>
                            </tr>
                            <tr>
                            <td style = "width:20%" class="style1"></td>
                                <td colspan="2" style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                                <tr>
                                    <td class="style1" style="width: 20%">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td width="30%">
                </td>
            </tr>
            <tr>
                <td width="30%">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td width="30%">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:Panel ID="pnlEditEmail" runat="server" CssClass="CajaDialogo" Width="290px" Style = "display:none">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel9" runat="server" HorizontalAlign="Center" SkinID="pnlTittleGrid">
                                <asp:Label ID="Label1" runat="server" style="font-weight: 700">EMAIL</asp:Label>
                            </asp:Panel>
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
                            Correo:</td>
                        <td>
                            <uc3:wucText_Edit ID="tbEditEmail" runat="server" MaxLength="100" 
                                TextMode="SingleLine" Width="200" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnAcceptEdit" runat="server" 
                                onclick="btnAcceptEdit_Click" SkinID="btnAceptX" Text="Guardar" 
                                Width="75px" />
                            <asp:Button ID="btnCancelEdit" runat="server" CausesValidation="False" 
                                SkinID="btnCancelX" Text="Cancelar" Width="75px" 
                                onclick="btnCancelEdit_Click" />
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
        </ContentTemplate>
        </asp:TabPanel>
        </asp:TabContainer>
 
        <table width ="100%">
        <tr>
        <td width="30%">
        </td>
            <td style="text-align: center">
                <asp:Button ID="btnSaveLevel" runat="server" Text="Guardar" 
                    onclick="btnSaveLevel_Click" />
                <asp:Button ID="btnCancelLevelEdit" runat="server" CausesValidation="False" 
                    onclick="btnCancelLevelEdit_Click" Text="Cancelar" />
            </td>
            <td width="30%">
            </td>
        </tr>
        </table>

        </asp:View>

    </asp:MultiView>
    <script type="text/javascript">
        function colorChanged(sender) {
//            sender.get_element().style.background =
            //       "#" + sender.get_selectedColor();
            document.getElementById('ctl00_Body_ctl00_TabPanel1_tbLevelColor').value = sender.get_selectedColor();
            document.getElementById('ctl00_Body_ctl00_TabPanel1_hfColor').value = sender.get_selectedColor();
        }

        
    </script>
</asp:Content>
