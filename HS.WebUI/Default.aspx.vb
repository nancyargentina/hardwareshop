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

                If Not autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION") And (autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_USUARIOS") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_PERFILES")) Then
                    lblMensajeEspecial.Text = "Usted esta autorizado a actualizar precios y datos."
                ElseIf Not autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION") And (autenticacionVista.UsuarioPoseePermiso(usuarioActual, "USUARIOFINAL")) Then
                    lblMensajeEspecial.Text = "Ya puede acceder al carrito."
                    End If


                    'btnIntegridadBD.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")


                    ' btnAdministracion.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_USUARIOS") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_PERFILES")
                    btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                    'btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                    ' btnClientes.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "USUARIOFINAL")


                    'If Integridad.VerificarIntegridadBD = Nothing Then
                    '    btnAdministracion.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_USUARIOS") Or autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMIN_PERFILES")
                    '    btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                    '    btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION")
                    '    btnClientes.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, "USUARIOFINAL")
                    'Else
                    '    If autenticacionVista.UsuarioPoseePermiso(usuarioActual, "ADMINISTRACION") Then
                    '        Response.Redirect("~/Servicios/IntegridadBD.aspx")
                    '    Else
                    '        lblMensaje.Text = "UPS :(  Parece haber problemas con el sistema. Nuestros administradores estan trabajando para solucionarlo. Muchas gracias"
                    '        lblMensaje.ForeColor = Drawing.Color.Red
                    '    End If


                    'End If

                Else
                    lblMensaje.Text = "Inicie sesion para acceder a las funciones del sistema."
                btnAdministracion.Visible = False
            End If

        Catch ex As Exception
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
        End Try
    End Sub

    'Protected Sub btnAdministracion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdministracion.Click
    '    Response.Redirect("~/Admin/Administracion.aspx")
    'End Sub

    Protected Sub btnBitacora_Click(sender As Object, e As EventArgs) Handles btnBitacora.Click
        Response.Redirect("~/Bitacora.aspx")
    End Sub

    'Protected Sub btnBackupYRestore_Click(sender As Object, e As EventArgs) Handles btnBackupYRestore.Click
    '    Response.Redirect("~/Servicios/BackupYRestore.aspx")
    'End Sub

    'Protected Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
    '    Response.Redirect("~/Servicios/Sorteos.aspx")
    'End Sub

    'Protected Sub btnIntegridadBD_Click(sender As Object, e As EventArgs) Handles btnIntegridadBD.Click
    '    Response.Redirect("~/Servicios/IntegridadBD.aspx")
    'End Sub
End Class