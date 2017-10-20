Imports HS.BE
Imports HS.BLL

Public Class Login
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Provee servicios de inicio y fin de sesion
    ''' </summary>
    ''' <remarks></remarks>
    Private vistaAutenticacion As AutenticacionVista = New AutenticacionVista()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Not vistaAutenticacion.UsuarioActual Is Nothing Then
                    divIniciarSesion.Visible = False
                    lblUsuarioActual.Text = vistaAutenticacion.UsuarioActual.Nombre
                Else
                    divIniciarSesion.Visible = True
                End If
                divCerrarSesion.Visible = Not divIniciarSesion.Visible
            End If
        Catch ex As Exception
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
            lblMensaje.ForeColor = Drawing.Color.Red
            lblUsuarioActual.Text = ErrorHandler.ObtenerMensajeDeError(ex)
            lblUsuarioActual.ForeColor = Drawing.Color.OrangeRed
        End Try
    End Sub

    Private MaxIntentos As Integer = 3

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Try
            Dim usuarioLogin As UsuarioDTO = Nothing

            usuarioLogin = vistaAutenticacion.CrearUsuarioParaIniciarSesion(txtUsuario.Text, txtClave.Text)
            If (vistaAutenticacion.UsuarioBloqueado(usuarioLogin)) Then
                lblMensaje.Text = "El usuario que ingreso se encuentra bloqueado. "
                lblMensaje.ForeColor = Drawing.Color.Red
            ElseIf vistaAutenticacion.IniciarSesion(usuarioLogin) Then
                lblMensaje.Text = ""
                lblUsuarioActual.Text = vistaAutenticacion.UsuarioActual.Nombre
                divCerrarSesion.Visible = True
                divIniciarSesion.Visible = Not divCerrarSesion.Visible
                TryCast(Me.Master, Site).UpdateLogin()

                'Verificación de integridad -- Segunda entrega
            Else
                If (vistaAutenticacion.IntentosFallidos >= MaxIntentos) Then
                    vistaAutenticacion.BloquearUsuario(usuarioLogin)
                    lblMensaje.Text = "Intento numero " + CStr(MaxIntentos) + " fallido. Se ha bloqueado el usuario " + usuarioLogin.Nombre
                    vistaAutenticacion.IntentosFallidos = 0
                Else
                    Dim strIntentos = ""
                    If (vistaAutenticacion.IntentosFallidos > 0) Then
                        strIntentos = "Intentos restantes " + CStr(MaxIntentos - vistaAutenticacion.IntentosFallidos)
                    End If
                    lblMensaje.Text = "Usuario o contraseña invalida, por favor vuela a intentarlo. " + strIntentos
                End If
                lblMensaje.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
            lblMensaje.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub brnCerrarSesion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrarSesion.Click
        Try
            vistaAutenticacion.CerrarSesion()
            divIniciarSesion.Visible = True
            divCerrarSesion.Visible = Not divIniciarSesion.Visible
            TryCast(Me.Master, Site).UpdateLogin()
        Catch ex As Exception
            lblUsuarioActual.Text = ErrorHandler.ObtenerMensajeDeError(ex)
        End Try
    End Sub
End Class