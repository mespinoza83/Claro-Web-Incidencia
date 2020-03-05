  //***************************************************************************//
  //***************************************************************************//
  //****************** FUNCIONES UTILITARIAS EN JAVASCRIPT ********************//
  //***************************************************************************//
  //***************************************************************************//

  var sBrowser;
  var sVersion;

  //Para todos (saber el tipo de navegador)
  function setBrowserType() {
      var aBrowFull = new Array("opera","msie","netscape","gecko","mozilla");
      var aBrowVers = new Array("opera","msie","netscape","rv","mozilla");
      var aBrowAbrv = new Array("op","ie","ns","mo","ns");
      var sInfo = navigator.userAgent.toLowerCase();

      sBrowser = "";
      for (var i = 0; i < aBrowFull.length; i++) {
          if ((sBrowser == "") && (sInfo.indexOf(aBrowFull[i]) != -1)) {
              sBrowser = aBrowAbrv[i];
              sVersion = String(parseFloat(sInfo.substr(sInfo.indexOf(aBrowVers[i]) + aBrowVers[i].length + 1)));
          }
      }
  }

  //Función para validar que la fecha de inicio sea menor a la fecha final
  //Formato para las fechas dd/mm/yyyy
  function validarFechasInicioFinal(IdfechaIni,IdfechaFin,frm){
    var eleFechaInicio = null;
    var eleFechaFinal = null;
       
    for(var i = 0; i < frm.length; i++){
        var ele = frm.elements[i];    

        if (ele.id.indexOf(IdfechaIni) != -1)
            eleFechaInicio = ele;
        if (ele.id.indexOf(IdfechaFin) != -1)
            eleFechaFinal = ele;
        
        if (eleFechaInicio != null && eleFechaFinal != null)
            break;
    }

    var cad1 = eleFechaInicio.value.split('/');
    var cad2 = eleFechaFinal.value.split('/');                             

    var numFecInicio = cad1[2] + cad1[1] + cad1[0];
    var numFecFinal = cad2[2] + cad2[1] + cad2[0];

    // Se modifico para que acepte la misma fecha.
    if (parseInt(numFecInicio) > parseInt(numFecFinal)){
        alert('La fecha de inicio debe ser menor a la fecha final.');
        return false;
    }
    return true;
  }
  
  //Función para mostrar el calendario
  function mostrarCalendario(obj,id,frm){
    for(var i = 0; i < frm.length; i++){
      var ele = frm.elements[i];    

      if (ele.id.indexOf(id) != -1){
          popUpCalendar(obj,ele,'dd/mm/yyyy');
          break;
      }
    }        
  }
  
  //No permite ingresar nada, para mantener la fecha ingresada por el Calendario.
  function validarCalendario(){
      return false;
  }
  
  function acceptnum(evt,nav4){ 
    // note: backspace = 8, enter = 13, '0' = 48, '9' = 57 
    var key = nav4 ? evt.which : evt.keycode; 
    return (key <= 13 || (key >= 48 && key <= 57));
  } 

  //=========================================================
  // Funcion generica para validar caracteres introducidos
  //=========================================================
  function validacion(input,funcion){
      setBrowserType();

      if (funcion == "validarSoloNumero") {
          input.onkeypress = validarSoloNumero;
      }
  }

  //Solo permite numero
  function validarSoloNumero(evento){
    var key;
    var keyCode;

    if (sBrowser == "ie") { //Internet Explorer
        key=window.event.keyCode
    } else { //Mozilla Firefox y otros
        key=evento.which;
        keyCode=evento.keyCode;
        //Fin, Inicio, Izquierda, Derecha, Delete, BackSpace, Tab
        if (keyCode == 35 | keyCode == 36 | keyCode == 37 | keyCode == 39 | keyCode == 46 | keyCode == 8 | keyCode == 9)
            key=keyCode;
    }

    if ((key < 48 || key > 57) & key != 35 & key != 36 & key != 37 & key != 39 & key != 46 & key != 8 & key != 9) {
      if (sBrowser == "ie") { window.event.keyCode=0; }
      else { return false; }
    }
  }

  //Solo permite número, coma y punto
  function validarCamposMonto(){
      var key=window.event.keyCode;
      if ((key < 48 || key > 57) & (key != 46) & (key != 44)){
          window.event.keyCode=0;
      }
  }

  //Solo permite números, letras y guión.
  function validarIdentificacion(){
      var key=window.event.keyCode;
    if (key < 48 || key > 57) {
      if (key < 65 || key > 90) {
          if (key < 97 || key > 122) {
            if (key != 45) {
              window.event.keyCode=0;
            }
          }
      }
    }
  }
  
  //Solo permite número y guión
  function validarNumeroCuenta(){
      var key=window.event.keyCode;
      if ((key < 48 || key > 57) & (key != 45)){
          window.event.keyCode=0;
      }
  }

  // Solo permite letras.
  function validarSoloLetras()
  {
    var key=window.event.keyCode;
    if (key < 65 || key > 90) {
          if (key < 97 || key > 122) {
            window.event.keyCode=0;
          }
    }
  }
  
  // Solo permite números y letras
  function validarSoloNumerosLetras() 
  { 
    var key=window.event.keyCode;
    if (key < 48 || key > 57) {
      if (key < 65 || key > 90) {
          if (key < 97 || key > 122) {
            window.event.keyCode=0;
          }
      }
    }
  }
  
  // Funciones que evitan el doble submit.   
  var statSend = false;
  function checkSubmit(mensaje) {
      if (!statSend) {
          statSend = true;
          return true;
      } else {
          abrirVentanaAlert("Seguridad", 270, 120, mensaje);
          return false;
      }
  }
  
  function checkComboSubmit(combo) {
      if (!statSend) {
        combo.form.submit();
        statSend = true;
        combo.disabled = true;      
      }
  }

  // Inicializa la variable de verificación del Submit.
  function initSubmit(){
      statSend = false;
  }
  
  // Función para recuperar el objeto que hace referencia al control en el formulario
  function RetornarObjControl(nombre, frm) {
      for(var i = 0; i < frm.length; i++) {
          var ele = frm.elements[i];    

          if (ele.id.indexOf(nombre) != -1) {
              return ele;
          }	              
      }
      return null;
  }  
  
  // Funcion para validar si se habilitara el Autocompletado del navegador.
  function AutoComplete(est){
      var estado = "on";
      
      //Si se manda el estado se setea
      if (est != null){
          estado = est;
      }
      document.forms[0].setAttribute('autocomplete', "'" + estado + "'");
  }

  /*********** UTILIZADO PARA MOSTRAR EL MENSAJE DEL DOBLE SUBMIT *************/

  var ventana = null;
    
  function abrirVentanaAlert(titulo, ancho, alto, mensaje) {
      var contextPath = '/safetypad';
      var src = contextPath + '/include/imagenes/';
      var over = 'onMouseOver="this.src=\'' + src + 'cerrarover.gif\'"';
      var out = 'onMouseOut="this.src=\'' + src + 'cerrar.gif\'"';
      var onClick = 'onclick="cerrar();"';
      var atributos = 'alt="Cerrar" title="Cerrar ventana" style="cursor: pointer;"';
      var cerrar = '<img src="' + src + 'cerrar.gif" ' + atributos + ' ' + onClick + ' ' + over + ' ' + out + ' />';
      var tabla = ' border="0" cellspacing="0" cellpadding="0"';
      var texto = 'font-family: Arial, Verdana, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #ffffff;';

      var superiorIzquierda = 'style="width: 15px; height: 35px; cursor: default; background-image: url(' + src + 'superior-izquierda.png);"';
      var superior = 'id="draggable" style="width: ' + (ancho - 51) + 'px; height: 35px; cursor: default; background-image: url(' + src + 'superior.png); ' + texto + '"';
      var celdaImagen = 'style="width: 15px; height: 35px; background-image: url(' + src + 'superior.png); ' + texto + '"';
      var superiorDerecha = 'style="width: 20px; height: 35px; cursor: default; background-image: url(' + src + 'superior-derecha.png);"';
      var izquierda = 'style="width: 30px; height: ' + (alto - 75) + 'px; cursor: default; background-image: url(' + src + 'izquierdaAlert.png);"';
      var centro = 'style="width: ' + (ancho - 22) + 'px; height: ' + (alto - 75) + 'px;cursor: default;font-family: Arial, Verdana, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000;background-image: url(' + src + 'centro.png);"';
      var derecha = 'style="width: 19px; height: ' + (alto - 75) + 'px; cursor: default; background-image: url(' + src + 'derecha.png);"';
      var inferiorIzquierda = 'style="width: 15px; height: 40px; cursor: default; background-image: url(' + src + 'inferior-izquierda.png);"';
      var inferior = 'style="width: ' + (ancho - 36) + 'px; height: 40px; cursor: default; background-image: url(' + src + 'inferior.png);"';
      var inferiorDerecha = 'style="width: 20px; height: 40px; cursor: default; background-image: url(' + src + 'inferior-derecha.png);"';
      var msg_info = 'style="width: 40px; height: ' + (alto - 75) + 'px; cursor: default;background: url(' + src + 'msgInfoAlert.png);background-repeat: no-repeat;"';

      var html = ''
      + '<table' + tabla + '>'
      + '<tr><td><table' + tabla + '>'
      + '<tr><td ' + superiorIzquierda + '>&nbsp;</td>'
      + '<td ' + superior + '>' + titulo + '</td>'
      + '<td ' + celdaImagen + '>' + cerrar + '</td>'
      + '<td ' + superiorDerecha + '>&nbsp;</td>'
      + '</tr></table></td></tr><tr><td>'
      + '<table border="0" ' + tabla + '>'
      + '<tr><td ' + izquierda + '>&nbsp;</td>'
      + '<td align="right" ' + msg_info + '>&nbsp;</td>'
      + '<td align="center" ' + centro + '>' + mensaje + '</td>'
      + '<td ' + derecha + '>&nbsp;</td>'
      + '</tr></table></td></tr><tr><td>'
      + '<table ' + tabla + '>'
      + '<tr><td ' + inferiorIzquierda +'>&nbsp;</td>'
      + '<td ' + inferior + '>&nbsp;</td>'
      + '<td ' + inferiorDerecha + '>&nbsp;</td>'
      + '</tr></table></td></tr></table>';

      mostrarVentana(html, ancho, alto);      
  }   

  function mostrarVentana(html, ancho, alto) {   
      var top = 0;
      var left = 0;
      
      top = (screen.height / 2) - (alto / 2) - 100;                     
      left = (screen.width / 2) - (ancho / 2);

      ventana = document.createElement("DIV");
      document.body.appendChild(ventana);
      ventana.style.display = "inline";
      ventana.style.position = "absolute";
      ventana.style.zIndex = 100000;
      ventana.style.width = ancho + "px";
      ventana.style.height = alto + "px";
      ventana.style.top = top + "px";
      ventana.style.left = left + "px";
      ventana.className = "ventana-modal-ventana";
      ventana.innerHTML = html;
  }

  function cerrar() {
      ventana.style.display = "none";
  }  
