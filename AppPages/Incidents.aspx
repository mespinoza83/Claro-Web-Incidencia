<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeFile="Incidents.aspx.cs" Inherits="AppPages_Incidents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../AppControls/wucMessage.ascx" TagName="wucMessageControl" TagPrefix="uc2" %>
<%@ Register Src="../AppControls/CustomControls/wucText_Edit.ascx" TagName="wucText_Edit"
    TagPrefix="uc3" %>
<%@ Register Src="../AppControls/wucTitleSubtitle.ascx" TagName="wucTitleSubtitle"
    TagPrefix="uc1" %>
<%@ Register Src="../AppControls/wucTimeLine.ascx" TagName="TimeLine" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
<script language="javascript">

    //Funcion que cambia el total de los servicios 
    //afectados
    function changeTotal() {
        var txtTotal = 0.00;
        //Tomamos la grid de servicios afectados
        var gv = document.getElementById("<%=gvServices.ClientID%>");
        //Tomamos todos los controles de tipo input
        var rbs = gv.getElementsByTagName("input");

        //Recorremos los controles
        for (var i = 0; i < rbs.length; i++) {
            //si son cajas de texto 
            if (rbs[i].type == "text") {
                if (rbs[i].value) {
                    txtTotal = parseFloat(txtTotal) + parseFloat(rbs[i].value);
                }
            }
        }
		var rbs2 = gv.getElementsByTagName("span");
		for (var i = 0; i < rbs2.length; i++) {
            //si son cajas de texto 
            
                if (rbs2.length ==(i+1) ) 
				{
					//Evaluamos si es internet explorer              
            if (navigator.appName.indexOf("Explorer") != -1) {
                //cambiamos el total actual por el total calculado
                
                rbs2[i].innerText = txtTotal;                
            }
            else {
                //cambiamos el total actual por el total calculado
                                
                rbs2[i].innerHTML = txtTotal;   
            }

                
            }
			}
    }

    function File_OnChange(sender) {
        val = sender.value.split('\\');
        document.getElementById('<%= txtFile.ClientID %>').value = val[val.length - 1];
    }
</script>
<style type="text/css">
    .spanFu .txt { width: 150px; height: 20px; text-align:center }
    .spanFu { position: relative; overflow: hidden; vertical-align: top; }
    .fu { z-index: 1; width: 100px; height: 24px; position: absolute; top: 0px;
        left: 5px; filter: alpha(opacity=0); opacity: .0; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <uc2:wucMessageControl ID="wucMessageControl" runat="server" />
    <uc1:wucTitleSubtitle ID="TitleSubtitle1" runat="server" />
    <asp:MultiView runat="server" ID="mvIncidents" ActiveViewIndex="0">
        <asp:View ID="vwIncidentList" runat="server">
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
                        <asp:TextBox ID="tbSeachClient" runat="server" Width="150px"></asp:TextBox>&nbsp;
                        <asp:DropDownList ID="ddlCountryFilter" Width="140px" runat="server" DataTextField="NAME_CODE"
                            DataValueField="IN_COUNTRY_PK" DataSourceID="odsCountries" OnDataBound="ddlCountryFilter_DataBound">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsCountries" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetDataByCodesCountry" 
                            TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdfCodigosPaises" DefaultValue="-1" 
                                    Name="Codigos_Paises" PropertyName="Value" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:HiddenField ID="hdfCodigosPaises" runat="server" />
                        <asp:Button ID="btnSearchIncident" runat="server" Text="Buscar" Width="100px" CausesValidation="False"
                            OnClick="btnSearchIncident_Click" />
                        <asp:Button ID="btnShowAllIncident" runat="server" Text="Mostrar Todos" Width="100px"
                            CausesValidation="False" OnClick="btnShowAllIncident_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNewIncident" runat="server" Text="Nuevo" Width="100px" CausesValidation="False"
                            OnClick="btnNewIncident_Click" />
                        <asp:ModalPopupExtender ID="btnNewIncident_ModalPopupExtender" runat="server" DynamicServicePath=""
                            Enabled="False" PopupControlID="pnlEdit" TargetControlID="btnNewIncident" BackgroundCssClass="FondoAplicacion">
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
                            LISTADO DE INCIDENCIAS</asp:Panel>
                        <asp:GridView ID="gvIncidentMain" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" EmptyDataText="No hay datos que mostrar" AllowPaging="True" DataKeyNames="COD_INCIDENCE,MOTIVE_NAME"
                            OnPageIndexChanging="gvIncidentMain_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="COD_INCIDENCE" HeaderText="<%$ Resources: IncidentLabel, IncidentCode %>" />
                                <asp:BoundField DataField="NAME_COUNTRY" HeaderText="<%$ Resources: IncidentLabel, CountryName %>" />
                                <asp:BoundField DataField="START_DATE" HeaderText="<%$ Resources: IncidentLabel, StartDate %>" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="<%$ Resources: IncidentLabel, Description %>" />
                                <asp:BoundField DataField="SEGMENT_NAME" HeaderText="<%$ Resources: IncidentLabel, SegmentName %>" />
                                <asp:BoundField DataField="TYPE_NAME" HeaderText="<%$ Resources: IncidentLabel, TypeName %>" />
                                <asp:BoundField DataField="LEVEL_NAME"  HeaderText="<%$ Resources: IncidentLabel, LevelName %>" />
                                <asp:BoundField DataField="MOTIVE_NAME" HeaderText="<%$ Resources: IncidentLabel, State %>" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEditClient" runat="server" ImageUrl="~/include/imagenes/application-edit-icon.png"
                                            ToolTip="Editar" CausesValidation="False" OnClick="ibtnEditClient_Click" 
                                            style="width: 16px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="vwEditInicident">
            <asp:Panel runat="server" ID="pnlContainer">
                <table width="100%">
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlLevelInformation" Style="border: solid 1px SlateGray;" runat="server">
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
                                            <asp:Label ID="lblCountry" runat="server" Text="<%$ Resources: IncidentLabel, CountryName %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCountry" Width="210px" runat="server" AutoPostBack="True"
                                                DataTextField="NAME_CODE" DataValueField="IN_COUNTRY_PK" OnDataBound="ddlCountry_DataBound"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsCountry" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetDataByCodesCountryActive" 
                                                TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="hdfCodigosPaises" DefaultValue="-1" 
                                                        Name="Codigos_Paises" PropertyName="Value" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <%--<asp:ObjectDataSource ID="odsCountryAll" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetDataByCodesCountryAll" 
                                                TypeName="dsIncidentNotificationTableAdapters.IN_COUNTRIES_SELECTTableAdapter">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="hdfCodigosPaises" DefaultValue="-1" 
                                                        Name="Codigos_Paises" PropertyName="Value" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>--%>
                                            <asp:RequiredFieldValidator ID="rqvCountry" runat="server" ControlToValidate="ddlCountry"
                                                Display="None" InitialValue="-1" ErrorMessage="El campo es requerido"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="vceCountry" runat="server" Enabled="True" TargetControlID="rqvCountry">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td style="text-align: right" width="120" valign="top">
                                            <asp:Label ID="lblScript" runat="server" Text="<%$ Resources: IncidentLabel, Script %>"></asp:Label>:
                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScript" runat="server" Height="30px" MaxLength="2000" onFocus="textAreaFocus(this,2000)"
                                                onKeyUp="StopMessage(this,2000)" TextMode="MultiLine" Width="210px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtScript" runat="server" Display="None" ErrorMessage="El campo es requerido"
                                                ControlToValidate="txtScript"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="rfvTxtScript_VCE" runat="server" Enabled="True"
                                                TargetControlID="rfvTxtScript">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:FilteredTextBoxExtender ID="ftbetxtScript" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="txtScript" runat="server"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblSegment" runat="server" Text=" <%$ Resources: IncidentLabel, SegmentName %>"></asp:Label>:                                           
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSegment" runat="server" Width="210px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblDescription" runat="server" Text="<%$ Resources: IncidentLabel, Description %>"></asp:Label>:                                            
                                        </td>
                                        <td width="120">
                                            <asp:TextBox ID="tbDescription" runat="server" Height="30px" onFocus="textAreaFocus(this,2000)"
                                                onKeyUp="StopMessage(this,2000)" TextMode="MultiLine" Width="210px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="tbDescription_rfv" runat="server" ControlToValidate="tbDescription"
                                                Display="None" ErrorMessage="El campo es requerido"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="tbDescription_rfv_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="tbDescription_rfv">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:FilteredTextBoxExtender ID="ftbetbDescription" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="tbDescription" runat="server"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblType" runat="server" Text="<%$ Resources: IncidentLabel, TypeName %>"></asp:Label>:                                           
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlType" runat="server" Width="210px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="lbMessageddlType" runat="server" ForeColor="Red" Text="No existen Tipos para este segmento"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblMonitoring" runat="server" Text="<%$ Resources: IncidentLabel, Monitoring %>"></asp:Label>:                                            
                                        </td>
                                        <td width="120">
                                            <asp:TextBox ID="tbMonitoring" runat="server" Height="30px"  TextMode="MultiLine" Width="210px"></asp:TextBox> 
                                            <%--onFocus="textAreaFocus(this,300)" onKeyUp="StopMessage(this,300)"--%>
                                            <asp:RequiredFieldValidator ID="tbMonitoring_rfv" runat="server" ControlToValidate="tbMonitoring"
                                                Display="None" ErrorMessage="El campo es requerido"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="tbMonitoring_rfv_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="tbMonitoring_rfv">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:FilteredTextBoxExtender ID="ftbetbMonitoring" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="tbMonitoring" runat="server"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblLevel" runat="server" Text="<%$ Resources: IncidentLabel, LevelName %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <uc3:wucText_Edit ID="tbLevel" runat="server" Enable="False" IsRequired="True" TextMode="SingleLine"
                                                Width="200" />
                                            <br />
                                            <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Text="No existen Niveles para este tipo"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblIncidentCause" runat="server" Text=" <%$ Resources: IncidentLabel, IncidentCause %>"></asp:Label>:                                           
                                        </td>
                                        <td width="120">
                                            <asp:TextBox ID="tbIncidentCause" runat="server" Height="30px" onFocus="textAreaFocus(this,2000)"
                                                onKeyUp="StopMessage(this,2000)" TextMode="MultiLine" Width="210px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftbetbIncidentCause" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="tbIncidentCause" runat="server"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblCriticality" runat="server" Text="<%$ Resources: IncidentLabel, Criticality %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCriticality" Width="210px" runat="server">
                                                <asp:ListItem Value="1">Crítico</asp:ListItem>
                                                <asp:ListItem Value="3">No Crítico</asp:ListItem>
                                                <%--<asp:ListItem Value="5">Bajo</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblStateName" runat="server" Text="<%$ Resources: IncidentLabel, State %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lblState" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right" width="120" valign="top">
                                            <asp:Label ID="lblTypology" runat="server" Text="<%$ Resources: IncidentLabel, Typology %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTypology" runat="server" Height="30px" MaxLength="500" onFocus="textAreaFocus(this,500)"
                                                onKeyUp="StopMessage(this,500)" TextMode="MultiLine" Width="210px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTxtTypology" runat="server" ErrorMessage="El campo es requerido"
                                                ControlToValidate="txtTypology" Display="None"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="rfvTxtTypology_VCE" runat="server" Enabled="True"
                                                TargetControlID="rfvTxtTypology">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:FilteredTextBoxExtender ID="ftbetxtTypology" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="txtTypology" runat="server"></asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblMotive" runat="server" Text="<%$ Resources: IncidentLabel, Motive %>"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMotive" runat="server" Width="210px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlMotive_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" width="120" valign="top">
                                            <asp:Label ID="lblSubject" runat="server" Text="<%$ Resources: IncidentLabel, Subject %>"></asp:Label>:
                                        </td>
                                       <td>
                                            <asp:TextBox ID="txtSubject" runat="server" Height="30px" MaxLength="500" onFocus="textAreaFocus(this,500)"
                                                onKeyUp="StopMessage(this,500)" TextMode="MultiLine" Width="210px"></asp:TextBox>                                           
                                            <asp:FilteredTextBoxExtender ID="ftbetxtSubject" FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom" 
                                                ValidChars=" ÁÉÍÓÚáéíóú-+*/¿?{}{},.$!¡|%#;=:ñÑüÜ&quot;'()&_"  TargetControlID="txtSubject" runat="server">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="120">
                                            <asp:Label ID="lblFolioOT" runat="server" Text="<%$ Resources: IncidentLabel, FolioOT %>"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFolioOT" runat="server" Width="150px" onKeyUp="StopMessage(this,16)" onFocus="textAreaFocus(this,16)" MaxLength="16"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftbetxtFolioOT" runat="server"
                                                Enabled="True" TargetControlID="txtFolioOT" ValidChars="0123456789-">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align: right" width="120" valign="middle">
                                            <asp:Label ID="lblMaintenance" runat="server" Text="<%$ Resources: IncidentLabel, Maintenance %>"></asp:Label>:                                            
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="cbxMaintenance" runat="server" CssClass="chkbox" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdfValueR" runat="server" />
                                            <asp:HiddenField ID="hdfEdit" runat="server" />
                                            <asp:HiddenField ID="hdfIn" runat="server" />
                                            <asp:HiddenField ID="hdfValue" runat="server" />
                                            <asp:HiddenField ID="hdfType" runat="server" />
                                            <asp:HiddenField ID="hdfVal" runat="server" />
                                            <asp:HiddenField ID="hdfCont" runat="server" />
                                            <asp:HiddenField ID="hdfLoad" runat="server" />
                                        </td>
                                        <td width="120" valign="top">
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
                        </td>
                        <td width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="background: url('../include/imagenes/backMenuContacto.gif'); width: 100%;">
                    &nbsp;</div>
                <%-- Seccion 2 --%>
                <table width="100%">
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlAffectedServices" Style="border: solid 1px SlateGray;" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" style="text-align: right">
                                            <asp:Panel ID="Panel2" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                Servicios Afectados</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="100">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="100">
                                            <asp:Label ID="lblAddService" runat="server" Text="<%$ Resources: IncidentLabel, Service %>"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbAddService" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="280">
                                            <asp:ImageButton ID="ibtnAddService" runat="server" CausesValidation="False" ImageUrl="~/include/imagenes/ico_add.png"
                                                OnClick="ibtnAddService_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="100">
                                            <asp:Label ID="lblCallNumber" runat="server" Text="<%$ Resources: IncidentLabel, CallNumbers %>"></asp:Label>:                                            
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbCallNumber" runat="server" Text="0" MaxLength="5" Width="50px"
                                                CssClass="centerText" Style="text-align: center;"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="tbCallNumber_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="tbCallNumber" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td width="280">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <%--<asp:Panel ID="Panel1" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                            LISTADO DE SERVICIOS</asp:Panel>--%>
                                            <asp:GridView ID="gvServices" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                Width="100%" EmptyDataText="No hay datos que mostrar" AllowPaging="True" DataKeyNames="COD_AFFECTED_SERVICE"
                                                OnPageIndexChanging="gvServices_PageIndexChanging" ShowFooter="True" OnRowDataBound="gvServices_RowDataBound">
                                                <FooterStyle CssClass="FooterStyleGrid" Font-Bold="True" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources: IncidentLabel, Service %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServicio" runat="server" Text='<%#  Eval("AFFECTED_SERVICE")%>' onchange="changeTotal();" ></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>                                                                                                                
                                                            <asp:Label ID="lblTotal" runat="server" Text="Total" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField HeaderText="Servicio" DataField="AFFECTED_SERVICE"/>--%>
                                                    <asp:TemplateField HeaderText="<%$ Resources: IncidentLabel, Calls %>">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="tbCalls" runat="server" Text="0" MaxLength="5" Width="50px" CssClass="centerText"
                                                                Style="text-align: center;"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="tbCalls_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="tbCalls" ValidChars="0123456789">
                                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCantTotal" runat="server" Text="0" />
                                                        </FooterTemplate>
                                                        <ItemStyle Width="60px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/include/imagenes/ico_delete.png"
                                                                CausesValidation="False" OnClick="ibtnDelete_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="45px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:RoundedCornersExtender ID="pnlContainer_RoundedCornersExtender" runat="server"
                    BorderColor="SlateGray" Enabled="True" Radius="3" TargetControlID="pnlContainer">
                </asp:RoundedCornersExtender>
                <div style="background: url('../include/imagenes/backMenuContacto.gif'); width: 100%;">
                    &nbsp;</div>
                <%--Seccion 3--%>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlAffectedClients" Style="border: solid 1px SlateGray;" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Panel ID="Panel4" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                Clientes Afectados</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBoxList ID="cbxlistAfecctedClients" runat="server" RepeatColumns="3" TextAlign="Left"
                                                Width="600px" CssClass="chkbox" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="background: url('../include/imagenes/backMenuContacto.gif'); width: 100%;">
                    &nbsp;</div>
                <%--Seccion 3--%>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlResponsibles" Style="border: solid 1px SlateGray;" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Panel ID="Panel6" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                Responsables de Solución</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBoxList ID="cbxListResponsibles" runat="server" RepeatColumns="3" TextAlign="Left"
                                                Width="600px" CssClass="chkbox" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="background: url('../include/imagenes/backMenuContacto.gif'); width: 100%;">
                    &nbsp;</div>
                <%--Seccion 4--%>
                <table width="100%">
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlAttach" Style="border: solid 1px SlateGray;" runat="server">
                                <table width="100%">                                   
                                    <tr>
                                        <td style="text-align: right" colspan="3">
                                            <asp:Panel ID="Panel9" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                Adjuntar Archivos</asp:Panel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="text-align: right" width="200">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                        
                                         <td style="text-align: right" width="200">
                                            <asp:Label ID="lblFile" runat="server" Text="Seleccione archivo:"></asp:Label>
                                          </td>
                                        <td>                                       
                                            <span class="spanFu">
                                            <asp:TextBox ID="txtFile" Width="150px" Text="Click aquí..."  runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                        
                                        
                                            <asp:FileUpload ID="FileUpload1" runat="server" onchange="File_OnChange(this)" CssClass="fu"  EnableTheming="false" />
                                            </span>
                                        </td>
                                        <td width="280">
                                             <asp:ImageButton ID="ibtUpload" runat="server" CausesValidation="False" ImageUrl="~/include/imagenes/ico_add.png"
                                               OnClick="ibtUpload_Click" />                                             
                                         </td>
                                    </tr> 
                                    <tr>
                                        
                                        <td colspan="3" style="text-align: left; Color:Red" width="200" >
                                            <b><asp:Label id="lblMessageAttach" ForeColor="Red" runat="server" Text=""></asp:Label></b>
                                        </td>
                                      </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">                                           
                                            <asp:GridView ID="gvAttach" runat="server" AutoGenerateColumns="False" Width="100%" 
                                               EmptyDataText="No hay datos que mostrar" DataKeyNames="AttachName"
                                               HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nombre del Archivo" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("AttachName")%>'></asp:Label>                                                                                                                                                                            
                                                        </ItemTemplate>                                                        
                                                        <ItemStyle Width="80%" />                                                        
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Descargar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnDownload" runat="server" ImageUrl="~/include/imagenes/ico_download.png"
                                                                 CausesValidation="False" OnClick="ibtnDownload_Click" CommandArgument='<%# Eval("AttachName")%>'  />                                                            
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnDeleteAttach" runat="server" ImageUrl="~/include/imagenes/ico_delete.png" 
                                                                 CausesValidation="False" OnClick="ibtnDeleteAttach_Click" CommandArgument='<%# Eval("AttachName")%>'  />                                                            
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>

                            </asp:Panel>
                        </td>
                        <td width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>                
                <div style="background: url('../include/imagenes/backMenuContacto.gif'); width: 100%;">
                    &nbsp;</div>
                <%--Seccion 5--%>
                <table width="100%">
                    <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                        </td>
                        <td>
                            <asp:Panel ID="pnlHistory" Style="border: solid 1px SlateGray;" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Panel ID="Panel7" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                Historial</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="1" Width="670px"
                                                CssClass="ajax__myTab">
                                                <asp:TabPanel ID="tabPanelHistory" runat="server" HeaderText="Historial" Width="100%">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel1" runat="server" SkinID="pnlTittleGrid" Style="text-align: center">
                                                                        HISTORIAL DE INCIDENCIAS</asp:Panel>
                                                                    <asp:GridView ID="gvIncidents" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                        EmptyDataText="No hay datos que mostrar" EnableModelValidation="True" Width="100%"
                                                                        DataKeyNames="COD_INCIDENCE_LOG,COD_INCIDENCE" OnPageIndexChanging="gvIncidents_PageIndexChanging">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="LOG_DATE" HeaderText="<%$ Resources: IncidentLabel, LogDate %>" />
                                                                            <asp:BoundField DataField="LEVEL_NAME" HeaderText="<%$ Resources: IncidentLabel, LevelName %>" />
                                                                            <asp:BoundField DataField="MOTIVE_NAME" HeaderText="<%$ Resources: IncidentLabel, MotiveName %>" />
                                                                            <asp:BoundField DataField="MONITORING" HeaderText="<%$ Resources: IncidentLabel, Comment %>"/>
                                                                            <asp:BoundField DataField="RECEIVED_CALLS" HeaderText="<%$ Resources: IncidentLabel, Calls %>" />
                                                                        </Columns>
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="tabPanelTimeLine" runat="server" HeaderText="Linea de Tiempo" Width="100%">
                                                    <HeaderTemplate>
                                                        Linea de Tiempo
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uc4:TimeLine ID="timeLine" runat="server" />
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td width="15%">
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                &nbsp;
            </div>
            <div style="text-align: center">
                <asp:Button ID="btnAccept" runat="server" Text="Aceptar" OnClick="btnAccept_Click"
                    OnClientClick="return ValidateHdf();" /><%--OnClientClick="this.disabled=true" UseSubmitBehavior="False"--%>
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False"
                    OnClick="btnCancel_Click" />
            </div>
            <div>
                &nbsp;
            </div>
        </asp:View>
    </asp:MultiView>

    <script language="javascript" type="text/javascript">
        



        function textAreaFocus(input, longi) {
            //Conociendo el tipo de navegador
            setBrowserType();
            //Estableciendo longitud
            longitud = longi;

            if (sBrowser == "ie") { //Internet Explorer
                input.onkeypress = keyPressIE;
                input.onkeydown = keyDownIE;
            } else if (sBrowser == "mo") { //Mozilla Firefox
                input.onkeypress = keyPressMO;
            }
        }

        //Para Internet Explorer
        function keyPressIE(evento) {
            if (this.value.length + 1 > longitud) {
                window.event.keyCode = 0;
            }
        }

        //Para Internet Explorer
        function keyDownIE(evento) {
            var whichCode = event.keyCode;
            if (whichCode == 35 || whichCode == 36 || whichCode == 37 || whichCode == 38 || whichCode == 39 || whichCode == 40 || whichCode == 8 || whichCode == 46) { return true; }

            if (this.value.length + 1 > longitud) {
                return false;
            }
            return true;
        }

        //Para FireFox
        function keyPressMO(evento) {
            var key = evento.keyCode;
            if (key == 35 || key == 36 || key == 37 || key == 38 || key == 39 || key == 40 || key == 8 || key == 46) { return true; }

            if (this.value.length + 1 > longitud) {
                return false;
            }
        }

        function StopMessage(text, cant) {

            if (text.value.length > cant) {

                alert("Su mensaje fue recortado a " + cant + " caracteres\nya que sobrepasaba el limite");

                return text.value = text.value.substring(0, cant);

            }
        }

        function ValidateHdf() {

            var myHdfValue = document.getElementById("<%= hdfVal.ClientID %>");
            //        alert(myHdfValue.value);
            if (myHdfValue.value == 0) {
                if (VerifyValidators(null)) {
                    myHdfValue.value = 1;
                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }

        /*verifica los validadores de la pagina, si es false se activan los validadores*/
        function VerifyValidators(ValidationGroup) {
            if (Page_Validators != undefined && Page_Validators != null) {
                Texto_Validaciones = '';
                Hay_Validaciones = false;
                Page_ClientValidate();

                if (Page_IsValid) {
                    return true;
                }
                else {
                    for (var i = 0; i < Page_Validators.length; i++) {
                        var val = Page_Validators[i];
                        var ctrl = document.getElementById(val.controltovalidate);

                        if (ctrl != null && ctrl.style != null) {
                            if (ValidationGroup != null) {
                                if (val.validationGroup == ValidationGroup) {
                                    if (val.enabled == undefined || val.enabled == true) {
                                        return false;
                                    }
                                }
                            }
                            else {
                                if (val.validationGroup == undefined) {
                                    if (val.enabled == undefined || val.enabled == true) {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

    </script>

</asp:Content>
