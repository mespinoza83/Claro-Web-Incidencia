Imports Ni.Com.Xolo.Safetypad.Ws.Interface

''' <summary>
''' Descripcion: Pagina de salida de sistema
''' </summary>
''' <remarks></remarks>
Partial Class LogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim safetypadWS As New SafetypadWS
        'Dim loginPath As String = safetypadWS.GetLoginPath()

        Dim scm As Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager = _
          CType(System.Configuration.ConfigurationManager.GetSection( _
            "safetypad-client"),  _
          Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)

        Try
            'safetypadWS.TerminateSessionByApp(Session.SessionID, scm.ApplicationName, Request.UserHostAddress, Request.UserHostName)
            ' Response.Redirect(loginPath)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Titulo As New Label
        Titulo = CType(Master.FindControl("lbTituloOpcion"), Label)
        If Not Titulo Is Nothing Then
            Titulo.Text = "Cerrar Sesión"
        End If
    End Sub
End Class
