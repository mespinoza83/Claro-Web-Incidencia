<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="AppPages_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        
        .limiter{
            width: 100%;
            margin: 0 auto;
        }
        .container{
            width: 100%;  
              min-height: 100vh;
              display: -webkit-box;
              display: -webkit-flex;
              display: -moz-box;
              display: -ms-flexbox;
              display: flex;
              flex-wrap: wrap;
              justify-content: center;
              align-items: center;
              padding: 15px;
              background: #f2f2f2;
        }
        .row{
            width: 1170px;
              background: #fff;
              overflow: hidden;
              display: -webkit-box;
              display: -webkit-flex;
              display: -moz-box;
              display: -ms-flexbox;
              display: flex;
              flex-wrap: wrap;
              align-items: stretch;
              flex-direction: row-reverse;
        }
        
        .logo{
            
            float: left;
            width: 60%;
            height: 700px;
            background-color: #fff;
            text-align: center;
        }
        
        .sesion{
            float: right;
            width: 40%;
            height: 700px;
            background-color: #d52b1e;            
            text-align: center;
            position: relative;
  z-index: 1;
        }
        .heading h3{
            position:relative;
            top: 100px;  
            color: #fff;
            font-weight: 600;
            font-size:25px;
            text-align: center;
        }
        .imagen {
            position:relative;
            top: 200px;  
        }
        .footer {
            position:relative;
            top: 300px;  
            color: #fff;
        }
        .group h3{
            position:relative;
            top: 100px;
            font-weight: 600;
            font-size:25px;
            text-align: center;
        }
        .logo .form-group{
            position: relative;
        }
         .logo .form-group input{
          width: 55%;
          height: 45px;
          margin-bottom: 30px;
          border: 1px solid #393939;
          border-radius: 5px 5px 5px 5px;
          outline: thin;
          background: white;
          padding-left: 45px;
          position: relative;
          top: 150px;
          font-size: 20px;
        }
         .logo .form-group input span{
            position: absolute;
            top: 8px;
            padding-left: 20px;
            color: #777;
        }
        .logo .form-group input::placeholder{
        padding-left: 0px;
        }
        .logo .form-group input:focus,
        .logo .form-group input:valid{
        border-bottom: 2px solid #dc3545;
        }
        .boton button[type="submit"]{
         border: none;
         cursor: pointer;
         width: 170px;
         height: 40px;
         border-radius: 5px;
         font-weight: bold;
         transition: 0.5s;
         background-color: #d52b1e;
         color: #fff;
         position: relative;
         top: 180px;
        }
        .boton .btn-login:hover{
        background: #AFAFAF;
        position: relative;
        }
       
    </style>
</head>
<body>
    
    <div class="limiter">
        <div class="container">
             <div class="row">
                 <div class="logo">
                     <div class="group">
                            <h3>Iniciar Sesi&oacute;n</h3>
                      </div>
                     <form>                     
                         <div class="form-group">
                             <input type="text" placeholder="Usuario" required>
                         </div>
                         <div class="form-group">
                            <input type="password" placeholder="Contraseña" required>
                         </div>
                         <div class="boton">
                                <button type="submit" class="btn-login">Ingresar</button>
                            </div>
                      </form>
                 </div>
                 <div class="sesion">
                     <div class="heading">
                         <h3>Sistema de Notificaci&oacute;n<br> de Incidencias</h3>
                     </div>
                     <div class="imagen">
                         <img src="/WebApp_NotificacionIncidencias/Images/footer.png" width="200px" />
                     </div> 
                     <!-- Copyright -->
                     <div class="footer">Todos los Derechos Reservados Claro 2020 © 
                     </div>
                     <!-- Copyright -->
                     
                 </div>
             </div>
         </div>
     </div>
</body>
</html>
