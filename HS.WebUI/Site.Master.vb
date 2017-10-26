Public Class Site
    Inherits System.Web.UI.MasterPage
    Public DBCorrupted As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateLogin()
    End Sub
    Public Sub UpdateLogin()
        Dim autenticacionVista As AutenticacionVista = New AutenticacionVista()
        Dim usuarioActual = autenticacionVista.UsuarioActual

        If Not usuarioActual Is Nothing Then
            HeadLoginStatus.InnerText = usuarioActual.Nombre
        Else
            HeadLoginStatus.InnerText = "Iniciar Sesion"
        End If
    End Sub
End Class