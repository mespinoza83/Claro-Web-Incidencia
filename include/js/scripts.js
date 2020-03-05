// JavaScript Document
function Limpiar(frm)
{    
	for (i = 0;i < frm.elements.length; i++) 
	{	    
		if(frm.elements[i].type == "text") 
		{		  
		  if(!frm.elements[i].disabled)
		   frm.elements[i].value = "";
		}
		else if(frm.elements[i].type == "checkbox") 
		  frm.elements[i].checked = false; 
//		//else if(frm.elements[i].type == "select-one")
//		  if(frm.elements[i].options.lenght > 0)
//		   frm.elements[i].options[0].selected = true; 
		else if(frm.elements[i].type == "textarea")
		{
		  if(!frm.elements[i].disabled)
		    frm.elements[i].value = "";
		}
   } 
}

//Script que elimina la Cookie
function EliminaCook(cook)
{
	var expira = new Date();
 	expira.setTime(expira.getTime() - 1000);
	for(var i = 0; i < cook.length; i++) 		
	 document.cookie = cook[i] + "=" + escape("") + ";expires=" + expira.toGMTString();
}
	
//Escribe si el valor de una variable Cookie
function CookieValor(variable,valor)
{
 	var expira = new Date();
 	expira.setTime(expira.getTime() + 7 * 24 * 60 * 60 * 1000);
	document.cookie = variable + "=" + escape(valor) + ";expires=" + expira.toGMTString();
}
	
//Verifica si existe una variable Cookie
function ExisteVariable(variable)
{
	var index = document.cookie.indexOf(variable + "=");
	if (index == -1) return false;
		else return true;
}

//Establece a decimales la entrada de un textbox
function decimales(txtBox)
{    
    if(txtBox.value.indexOf('.') != -1 && txtBox.value.substring(txtBox.value.indexOf('.') + 1,txtBox.value.length).length > 2)
       document.getElementById(txtBox.id).value = txtBox.value.substring(0,txtBox.value.indexOf('.') + 3);    
}

//Lee el valor de una variable Cookie
function LeeValorVariable(variable)
{
	var ckObj = document.cookie;
	var	index = ckObj.indexOf(variable + "=");
	if (index != -1)
	{ //Si la variable existe
		index = ckObj.indexOf( "=", index ) + 1;
		var finStr = ckObj.indexOf( ";",index);
		if (finStr == -1) 
			finStr = ckObj.length;
		return unescape(ckObj.substring(index, finStr));
	}
}

function getParameter(parameter)
{
   // Obtiene la cadena completa de URL
   var url = location.href;
   /* Obtiene la posicion donde se encuentra el signo ?, 
      ahi es donde empiezan los parametros */
   var index = url.indexOf("?");
   /* Obtiene la posicion donde termina el nombre del parametro
      e inicia el signo = */
   index = url.indexOf(parameter,index) + parameter.length;
   /* Verifica que efectivamente el valor en la posicion actual 
      es el signo = */ 
   if (url.charAt(index) == "=")
   {
     // Obtiene el valor del parametro
     var result = url.indexOf("&",index);
     if (result == -1){result=url.length;};
     // Despliega el valor del parametro
       return(url.substring(index + 1,result));
   }
}

function SelectAllCheckboxes(spanChk)
{
   // Added as ASPX uses SPAN for checkbox
   var oItem = spanChk.children;
   var theBox= (spanChk.type=="checkbox") ? 
        spanChk : spanChk.children.item[0];
   xState=theBox.checked;
   elm=theBox.form.elements;

   for(i=0;i<elm.length;i++)
     if(elm[i].type=="checkbox" && 
              elm[i].id!=theBox.id)
     {
       //elm[i].click();
       //if(elm[i].checked!=xState)
       //  elm[i].click();
       elm[i].checked = xState;
     }
 }
 
function AceptaSoloNumeros(e)
    {
        var key;
        if(window.event) // IE
        {
            key = e.keyCode;
        }
        else if(e.which) // Netscape/Firefox/Opera
        {
            key = e.which;
        }    
        if (key < 48 || key > 57)
        {
            return false;
        }else
        {
            return true;
        }
 }

function MostrarLabel() {
    setTimeout("OcultarLabel()", 2000);
    var msj = document.getElementById("lblMensaje");
    msj.style.visibility = "visible";
}

function OcultarLabel() {
        var msj = document.getElementById("lblMensaje");
        msj.style.visibility = "hidden";
}

		