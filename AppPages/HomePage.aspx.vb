Imports Ni.Com.Xolo.Safetypad.Ws.Interface

''' <summary>
''' Descripcion: Pagina de inicio
''' </summary>
''' <remarks></remarks>
Partial Class HomePage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim wsInterface As New SafetypadWS
        'Dim scm As Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager = _
        '      CType(System.Configuration.ConfigurationManager.GetSection( _
        '        "safetypad-client"),  _
        '      Ni.Com.Xolo.Safetypad.Web.Client.SafetypadClientManager)

        'Dim canSave As Boolean = wsInterface.IsActionAllowed(Session.SessionID, scm.ApplicationName, Page.Request.ApplicationPath, "Guardar")
        'Dim canCancel As Boolean = wsInterface.IsActionAllowed(Session.SessionID, scm.ApplicationName, Page.Request.ApplicationPath, "Cancelar")


        Dim Titulo As New Label
        Titulo = CType(Master.FindControl("lbTituloOpcion"), Label)
        If Not Titulo Is Nothing Then
            Titulo.Text = "Inicio"
        End If

    End Sub

End Class
