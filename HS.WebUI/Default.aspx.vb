Imports HS.BE
Imports hs.BLL

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblMensaje.Text = String.Empty

            Dim autenticacionVista As AutenticacionVista = New AutenticacionVista()
            Dim usuarioActual = autenticacionVista.UsuarioActual
            If Not usuarioActual Is Nothing Then


                lblMensaje.Text = String.Format("Bienvenido {0} a HARDWARE SHOP.", usuarioActual.Nombre)

                btnCarrito.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "USUARIOFINAL")

                btnIntegridadBD.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                btnAdministracionUsuarios.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_USUARIOS") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_PERFILES") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")

                'VERIFICACION DE INTEGRIDAD' --> Segunda entrega

            Else
                lblMensaje.Text = "Inicie su sesión."
            End If

        Catch ex As Exception
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
        End Try
    End Sub

    Protected Sub btnBitacora_Click(sender As Object, e As EventArgs) Handles btnBitacora.Click
        Response.Redirect("~/Bitacora.aspx")
    End Sub

    
End Class