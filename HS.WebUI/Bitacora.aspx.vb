Imports HS.BE
Imports HS.BLL
Public Class Bitacora
    Inherits System.Web.UI.Page

    Private _vistaUsuarios As UsuarioVista = New UsuarioVista()
    Private _vistaPermisos As PermisoVista = New PermisoVista()

    Private _vista As BitacoraVista = New BitacoraVista()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblMensaje.Text = String.Empty
            If Not Page.IsPostBack Then
                MultiView1.ActiveViewIndex = 0
                LlenarGrilla()
            End If
        Catch ex As Exception
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
            lblMensaje.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVolver.Click
        Me.Response.Redirect("~/Default.aspx")
    End Sub

    Private Sub LlenarGrilla()
        Me._vista.LlenarGrilla(grillaBitacora)
    End Sub

End Class