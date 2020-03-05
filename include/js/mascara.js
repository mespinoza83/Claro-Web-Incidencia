  //***************************************************************************//
  //******************* UTILIZADO PARA LA MASCARA *****************************//
  //******************* Yamil Vanegas (Xolo S.A.) *****************************//
  //***************************************************************************//

  var comodines = "/-";
  var format;
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

  //Para todos
  function maskFocus(input,formato) {
      //Estableciendo el formato
      format = formato;
      //Conociendo el tipo de navegador
      setBrowserType();
      //***** Estableciendo atributos *****
      //Atributo autocomplete, evita auto completar texto
      input.setAttribute("autocomplete", "off");
      //***** Estableciendo eventos *****
      //Evita menú contextual
      //Para Firefox deshabilita el CTRL+C, CTRL+V y CTRL+X
      input.oncontextmenu = function(){return false;};
      if (sBrowser == "ie") { //Internet Explorer
          input.onkeypress = maskKeyPressIE;
          input.onkeydown = maskKeyDownIE;              
      } else if (sBrowser == "mo") { //Mozilla Firefox
          input.onkeypress = maskKeyPressMO;
      }      
      //Si el campo está vacío, escribir caracteres de máscara
      if (input.value.replace(/ /g, '') == '') {          
          var cadena = "";
	  for (var i = 0; i < formato.length; i++) {
              if (comodines.indexOf(formato.substring(i,i + 1)) != -1) {
                  cadena = cadena + formato.substring(i,i + 1);                
              } else {
                  cadena = cadena + "_";
              }
          } 
          input.value = cadena;
      }
  }

  //Para Internet Explorer
  function maskKeyPressIE(evento) {
      var texto = this.value;
      var posicion = obtenerPosicionCursor(this);
      var key = window.event.keyCode;
      var caracter = String.fromCharCode(key);
      var cadenaIndex = "";

      for (var i = 0; i < format.length; i++) {
          if (comodines.indexOf(format.substring(i,i + 1)) != -1) {
              if (cadenaIndex.length > 0)
                  cadenaIndex = cadenaIndex + "/";
              cadenaIndex = cadenaIndex + i;                
          }
      }

      var indices = cadenaIndex.split("/");

      if (checkCaracter(posicion,key)) {
          if (verificarPosicionComodin(indices,posicion)) {
              this.value = texto.substring(0, posicion + 1) + caracter + texto.substring(posicion + 2,texto.length);
              ponerCursorEnPosicion(posicion + 2,this);
              window.event.keyCode = 0;
          } else {
              this.value = texto.substring(0, posicion) + caracter + texto.substring(posicion + 1,texto.length);
              ponerCursorEnPosicion(posicion + 1,this);
              window.event.keyCode = 0;
          }
      } else {
          window.event.keyCode = 0;
      }
  }

  //Para Internet Explorer
  function maskKeyDownIE(evento) {
      var e = event;
      var whichCode = (window.Event) ? e.which : e.keyCode;
      var posicion = obtenerPosicionCursor(this);
      var texto = this.value;
      var cadenaIndex = "";
      var cadenaIndex2 = "";

      if (whichCode == 27) {
          return false;
      }

      if (whichCode == 32) {return false;}

      for (var i = 0; i < format.length; i++) {	  
          if (comodines.indexOf(format.substring(i,i + 1)) != -1) {
              if (cadenaIndex.length > 0)
                  cadenaIndex = cadenaIndex + "/";
              if (cadenaIndex2.length > 0)
                  cadenaIndex2 = cadenaIndex2 + "/";
              cadenaIndex = cadenaIndex + (i + 1);
              cadenaIndex2 = cadenaIndex2 + i;
          }
      }
      
      var indices = (cadenaIndex + "/0").split("/");      

      if (whichCode == 8 && verificarPosicionComodin(indices,posicion)) {
          indices = cadenaIndex.split("/");
          if (verificarPosicionComodin(indices,posicion)) {
              this.value = texto.substring(0, posicion - 2) + "_" + texto.substring(posicion - 1,texto.length);
              ponerCursorEnPosicion(posicion - 2,this);
          }
          return false;
      }

      indices = cadenaIndex2.split("/");
      if (whichCode == 46 && verificarPosicionComodin(indices,posicion)) {
          ponerCursorEnPosicion(posicion + 1,this);
          return false;
      }

      if (posicion == texto.length && whichCode != 35 && whichCode != 36 && whichCode != 37 && whichCode != 39 && whichCode != 8) {return false;}
      if (whichCode == 8) {
          var caracter = this.value.substring(posicion - 1,posicion);
          if (caracter == "_") {
              ponerCursorEnPosicion(posicion - 1,this);
              return false;
          } else {
              this.value = texto.substring(0, posicion - 1) + "_" + texto.substring(posicion,texto.length);
              ponerCursorEnPosicion(posicion - 1,this);
              return false;
          }
      }

      if (whichCode == 46) {
          var caracter = this.value.substring(posicion,posicion + 1);
          if (caracter == "_") {
              return false;
          } else {
              this.value = texto.substring(0, posicion) + "_" + texto.substring(posicion + 1,texto.length);
              ponerCursorEnPosicion(posicion + 1,this);
              return false;
          }
      }

      return noCopyKey(event);
  }

  //Para FireFox
  function maskKeyPressMO(evento) {
      var texto = this.value;
      var posicion = obtenerPosicionCursor(this);
      var key = evento.keyCode;
      var keyChar = evento.charCode;
      var cadenaIndex = "";
      
      if (posicion == texto.length) {
	  if (key != 35 && key != 36 && key != 37 && key != 39 && key != 8) { //Fin, Inicio, Izquierda, Derecha, BackSpace
              return false;
          }
      }

      if (!checkCaracterSpecial(evento,this,posicion)) {
          return false;
      }

      if (key != 35 && key != 36 && key != 37 && key != 39 && key != 8) { //Fin, Inicio, Izquierda, Derecha, BackSpace
          var caracter = String.fromCharCode(keyChar);
          if (checkCaracter(posicion,keyChar)) {      
              for (var i = 0; i < format.length; i++) {                  
                  if (comodines.indexOf(format.substring(i,i + 1)) != -1) {
                      if (cadenaIndex.length > 0)
                          cadenaIndex = cadenaIndex + "/";
                      cadenaIndex = cadenaIndex + i;               
                  }
              }
              var indices = cadenaIndex.split("/");                   
              if (verificarPosicionComodin(indices,posicion)) {
                  this.value = texto.substring(0, posicion + 1) + caracter + texto.substring(posicion + 2,texto.length);
                  ponerCursorEnPosicion(posicion + 2,this);
                  return false;
              } else {
                  this.value = texto.substring(0, posicion) + caracter + texto.substring(posicion + 1,texto.length);
                  ponerCursorEnPosicion(posicion + 1,this);
                  return false;
              }
          } else {
              return false;
          }    
      }

      return true;
  }

  //Para FireFox
  function checkCaracterSpecial(evento,input,posicion) {
      var key = evento.keyCode;
      var keyChar = evento.charCode;
      var texto = input.value;
      var cadenaIndex = "";
      var cadenaIndex2 = "";

      if (key == 27) {return false;} //ESC
      if (keyChar == 32) {return false;} //Barra Espaciadora     

      for (var i = 0; i < format.length; i++) {	  
          if (comodines.indexOf(format.substring(i,i + 1)) != -1) {
              if (cadenaIndex.length > 0)
                  cadenaIndex = cadenaIndex + "/";
              if (cadenaIndex2.length > 0)
                  cadenaIndex2 = cadenaIndex2 + "/";
              cadenaIndex = cadenaIndex + (i + 1);
              cadenaIndex2 = cadenaIndex2 + i;
          }
      }

      var indices; 
      if (key == 8) { //BackSpace
	  if (posicion != 0) {  
              indices = cadenaIndex.split("/");             
	      if (verificarPosicionComodin(indices,posicion)) {
                  input.value = texto.substring(0, posicion - 2) + "_" + texto.substring(posicion - 1,texto.length);
                  ponerCursorEnPosicion(posicion - 2,input);
              } else {
                  input.value = texto.substring(0, posicion - 1) + "_" + texto.substring(posicion,texto.length);
                  ponerCursorEnPosicion(posicion - 1,input);              
              }
	      return false;
          }	       
      }

      if (key == 46) { //Delete  
          indices = cadenaIndex2.split("/");        
          if (verificarPosicionComodin(indices,posicion)) {
              ponerCursorEnPosicion(posicion + 1,input);
          } else {
              input.value = texto.substring(0, posicion) + "_" + texto.substring(posicion + 1,texto.length);              
              ponerCursorEnPosicion(posicion + 1,input);
          }
          return false;	  
      }

      return true;
  }

  //Igual para todos
  function verificarPosicionComodin(indices,posicion) {
      for (var i = 0; i < indices.length; i++) {
	  if (parseInt(indices[i]) == parseInt(posicion)) {
              return true;
          }
      }
      return false;      
  }

  //Igual para todos
  function checkCaracter(posicion,key) {
      var formatCaracter;
      var cadenaIndex = "";

      for (var i = 0; i < format.length; i++) {
          if (comodines.indexOf(format.substring(i,i + 1)) != -1) {
              if (cadenaIndex.length > 0)
                  cadenaIndex = cadenaIndex + "/";
              cadenaIndex = cadenaIndex + i;                
          }
      }

      var indices = cadenaIndex.split("/");
      if (!verificarPosicionComodin(indices,posicion)) {
          formatCaracter = format.substring(posicion,posicion + 1);
      } else {
          formatCaracter = format.substring(posicion + 1,posicion + 2);
      }

      if (formatCaracter == "Z") {
          return true;
      }

      if (formatCaracter == "L") {
          if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 48 && key <= 57)) {
              return true;
          } else {
              return false;
          }
      }

      if (formatCaracter == "A") {
          if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122)) {
              return true;
          } else {
              return false;
          }
      }

      if (formatCaracter == "9") {
          if (key >= 48 && key <= 57) {
              return true;
          } else {
              return false;
          }
      }

      return true;
  }

  //Igual para todos
  function ponerCursorEnPosicion(pos,input) {
      if (sBrowser == "ie") {
          if(typeof document.selection != 'undefined' && document.selection) {
              var tex = input.value;
              input.value = '';
              input.focus();
              var str = document.selection.createRange();
              input.value = tex;
              str.move("character", pos);
              str.moveEnd("character", 0);
              str.select();
          } else if(typeof input.selectionStart != 'undefined') {
              input.setSelectionRange(pos,pos);
              forzar_focus();
          }
      } else if (sBrowser == "mo") {
          input.selectionStart = pos;
          input.selectionEnd = pos;
      }
  }

  //Igual para todos
  function obtenerPosicionCursor(input) {
      if (sBrowser == "ie") {
          var range = document.selection.createRange();
          var range2 = input.createTextRange();
          range2.collapse(true);
          range2.moveEnd('character', 0);
          range2.setEndPoint('EndToStart', range);
          distancia = range2.text.length;
          return distancia;
      } else if (sBrowser == "mo") {
          var start = input.selectionStart;
          var end = input.selectionEnd;
          return start;
      }
  }

  //Para Internet Explorer
  function noCopyKey(e) {
      var forbiddenKeys = new Array('x','v');
      var keyCode = (e.keyCode) ? e.keyCode : e.which;
      var isCtrl;

      if(window.event)
          isCtrl = e.ctrlKey;
      else
          isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;

      if(isCtrl) {
          for(i = 0; i < forbiddenKeys.length; i++) {
              if(forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                  return false;
              }
          }
      }
      return true;
  }
