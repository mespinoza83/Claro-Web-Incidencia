<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCalendar_Edit.ascx.cs" Inherits="Calendar_Edit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="Controls.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function compareDates(selectedDate) {
        if (selectedDate > getDate())
            alert("Fecha no valida");
    }
</script>
<asp:TextBox ID="tbTextEdit" runat="server" Width="150px"></asp:TextBox>

<%--OnClientDateSelectionChanged = "compareDates(document.getElementById('tbTextEdit').value);"--%>

<asp:CalendarExtender ID="tbTextEdit_CalendarExtender" runat="server" 
    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="tbTextEdit" 
    TargetControlID="tbTextEdit">
</asp:CalendarExtender>
<asp:MaskedEditExtender ID="tbTextEdit_MaskedEditExtender" runat="server" 
    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
    Mask="99/99/9999" MaskType="Date" TargetControlID="tbTextEdit" ErrorTooltipEnabled="False">
</asp:MaskedEditExtender>

<asp:TextBoxWatermarkExtender ID="tbTextEdit_TextBoxWatermarkExtender" 
    runat="server" Enabled="True" TargetControlID="tbTextEdit" 
    WatermarkText="dd/mm/yyyy" WatermarkCssClass="watermark_style">
</asp:TextBoxWatermarkExtender>

<asp:RequiredFieldValidator runat="server" ID="rfvTextEdit" 
    ControlToValidate="tbTextEdit" Display="None" 
    ErrorMessage="El campo es requerido." />
<asp:RegularExpressionValidator runat="server" ID="revTextEdit" 
    ControlToValidate="tbTextEdit" Display="None" Enabled = "false"
    ErrorMessage="La fecha introducida no es válida." 
    
    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />
<asp:ValidatorCalloutExtender ID="revTextEdit_ValidatorCalloutExtender" 
    runat="server" Enabled="True" TargetControlID="revTextEdit">
</asp:ValidatorCalloutExtender>
<asp:ValidatorCalloutExtender ID="rfvTextEdit_ValidatorCalloutExtender" 
    runat="server" Enabled="True" TargetControlID="rfvTextEdit">
</asp:ValidatorCalloutExtender>