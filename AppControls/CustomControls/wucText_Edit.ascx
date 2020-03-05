<%@ Control Language="C#" CodeFile="wucText_Edit.ascx.cs" Inherits="Text_EditField" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="/WebApp_NotificacionIncidencias/App_Themes/Skin/EstilosAjax.css" rel="stylesheet" type="text/css" />
<%--<script src="../../include/js/LogitudTextArea.js" type="text/javascript"></script>--%>
<script language="javascript" type="text/javascript">
    function ValidateMaxLength(Obj, max) {
        if (max == 0) return true;
        if (Obj.value.length == max) return false;
        return true;
    }

    function StopMessage(text, cant) {

        if (text.value.length > cant) {

            alert("Su mensaje fue recortado a " + cant + " caracteres\nya que sobrepasaba el limite");

            return text.value = text.value.substring(0, cant);

        }
    }


    var sBrowser;
    var sVersion;
    var longitud;

    //Para todos (saber el tipo de navegador)
    function setBrowserType() {
        var aBrowFull = new Array("opera", "msie", "netscape", "gecko", "mozilla");
        var aBrowVers = new Array("opera", "msie", "netscape", "rv", "mozilla");
        var aBrowAbrv = new Array("op", "ie", "ns", "mo", "ns");
        var sInfo = navigator.userAgent.toLowerCase();

        sBrowser = "";
        for (var i = 0; i < aBrowFull.length; i++) {
            if ((sBrowser == "") && (sInfo.indexOf(aBrowFull[i]) != -1)) {
                sBrowser = aBrowAbrv[i];
                sVersion = String(parseFloat(sInfo.substr(sInfo.indexOf(aBrowVers[i]) + aBrowVers[i].length + 1)));
            }
        }
    }

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
    </script>

<asp:TextBox ID="tbTextEdit" Style="z-index:500;" TabIndex="3001" runat="server" Width="150px" ></asp:TextBox>

<asp:TextBoxWatermarkExtender ID="tbTextEdit_TextBoxWatermarkExtender" 
    runat="server" Enabled="True" TargetControlID="tbTextEdit" 
    WatermarkText="correoelectronico@domain.com" WatermarkCssClass="watermark_style">
</asp:TextBoxWatermarkExtender>

<asp:FilteredTextBoxExtender ID="tbTextEdit_FilteredTextBoxExtender" 
    runat="server" Enabled="True" TargetControlID="tbTextEdit">
</asp:FilteredTextBoxExtender>

<asp:RequiredFieldValidator runat="server" ID="rfvTextEdit" 
    ControlToValidate="tbTextEdit" Display="None" 
    ErrorMessage="El campo es requerido." />
<asp:RegularExpressionValidator runat="server" ID="revTextEdit" 
    ControlToValidate="tbTextEdit" Display="None" 
    ErrorMessage="El dato introducido no es válido." 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
<asp:ValidatorCalloutExtender ID="revTextEdit_ValidatorCalloutExtender" 
    runat="server" Enabled="True" TargetControlID="revTextEdit">
                        <Animations>
                        <OnShow>                                    
                        <Sequence>   
                        <HideAction Visible="true" /> 
                        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
                        </Sequence>
                        </OnShow>
                        <OnHide>
                        <Sequence>    
                        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
                        <HideAction Visible="false" />
                        </Sequence>
                        </OnHide>
                    </Animations>

</asp:ValidatorCalloutExtender>
<asp:ValidatorCalloutExtender ID="rfvTextEdit_ValidatorCalloutExtender" 
    runat="server" Enabled="True" TargetControlID="rfvTextEdit">

                        <Animations>
                        <OnShow>                                    
                        <Sequence>   
                        <HideAction Visible="true" /> 
                        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
                        </Sequence>
                        </OnShow>
                        <OnHide>
                        <Sequence>    
                        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
                        <HideAction Visible="false" />
                        </Sequence>
                        </OnHide>
                    </Animations>

</asp:ValidatorCalloutExtender>


