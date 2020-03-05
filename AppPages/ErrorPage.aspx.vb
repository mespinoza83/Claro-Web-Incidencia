''' <summary>
''' Descripcion: Pagina de error
''' </summary>
''' <remarks></remarks>
Partial Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Titulo As New Label
        Titulo = CType(Master.FindControl("lbTituloOpcion"), Label)
        If Not Titulo Is Nothing Then
            Titulo.Text = "Error"
        End If
    End Sub
End Class
