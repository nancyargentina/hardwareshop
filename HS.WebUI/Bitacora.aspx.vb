Imports HS.BE
Imports HS.BLL
Public Class Bitacora
    Inherits System.Web.UI.Page

    Private _vistaUsuarios As UsuarioVista = New UsuarioVista()
    Private _vistaPermisos As PermisoVista = New PermisoVista()

    Private _vista As BitacoraVista = New BitacoraVista()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblmensaje.Text = String.Empty
            Dim autenticacionVista As AutenticacionVista = New AutenticacionVista()
            Dim usuarioActual = autenticacionVista.UsuarioActual

            If Not usuarioActual Is Nothing Then
                If Not Page.IsPostBack Then
                    MultiView1.ActiveViewIndex = 0

                    LlenarGrilla()
                End If
            Else
                lblmensaje.Text = "Inicie sesion para acceder a las funciones del sistema."
            End If


        Catch ex As Exception
            'lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
            'lblMensaje.ForeColor = Drawing.Color.Red
        End Try
    End Sub


    'Protected Sub btnFiltrarBitacora_Click(sender As Object, e As EventArgs) Handles btnFiltrarBitacora.Click
    '    Try
    '        Dim criticidad As String = ""
    '        If Not cboxCriticidad.SelectedItem Is Nothing Then
    '            criticidad = cboxCriticidad.SelectedItem.Text
    '        End If
    '        Me._vista.FiltrarGrilla(grillaBitacora, txtFechaIni.Text, txtFechaFin.Text, txtDescripcion.Text, txtAutor.Text, criticidad)
    '    Catch ex As Exception
    '        lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex)
    '        lblMensaje.ForeColor = Drawing.Color.Red
    '    End Try
    'End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVolver.Click
        Me.Response.Redirect("~/Default.aspx")
    End Sub

    Private Sub LlenarGrilla()
        'traer todos los usuarios
        Me._vista.LlenarGrilla(grillaBitacora)
    End Sub
    '
    'Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs) Handles btnLimpiarFiltros.Click
    '    txtAutor.Text = ""
    '    txtDescripcion.Text = ""
    '    txtFechaFin.Text = ""
    '    txtFechaIni.Text = ""
    '    btnFiltrarBitacora_Click(sender, e)
    'End Sub
End Class