Imports HS.BE
Imports HS.BLL

Public Interface IAutenticador
    ''' <summary>
    ''' Retorna el numero de veces que se intento un inicio de sesion (sin exito)
    ''' </summary>
    ReadOnly Property IntentosFallidos As Integer

    ''' <summary>
    ''' Retorna el usuario con todas sus propiedades establecidas si
    ''' el inicio de sesion fue exitoso, sino Nothing.
    ''' </summary>
    Function IniciarSesion(ByVal value As UsuarioDTO, ByVal intentosFallidos As Integer) As UsuarioDTO

    ''' <summary>
    ''' Valida si existe el nombre de un pemiso dentro de una lista de permisos.
    ''' </summary>
    Function ValidarPermiso(ByVal nombrePermiso As String, ByVal lista As List(Of PermisoDTO)) As Boolean

End Interface

''' <summary>
''' Gestiona la Autenticacion de usuarios en el sistema.
''' </summary>

Public Class Autenticador
    Implements IAutenticador

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(ByVal UsuarioBLL As IUsuarioBLL)
        Me._UsuarioBLL = UsuarioBLL
        If Me._UsuarioBLL Is Nothing Then Me._UsuarioBLL = New UsuarioBLL()
    End Sub

    Private _UsuarioBLL As IUsuarioBLL = Nothing

    Private _intentosFallidos As Integer = 0

    ''' <summary>
    ''' Retorna el numero de veces que se intento un inicio de sesion (sin exito)
    ''' </summary>
    Public ReadOnly Property IntentosFallidos As Integer Implements IAutenticador.IntentosFallidos
        Get
            Return Me._intentosFallidos
        End Get
    End Property

    ''' <summary>
    ''' Retorna el usuario con todas sus propiedades establecidas si
    ''' el inicio de sesion fue exitoso, sino Nothing.
    ''' </summary>
    Public Function IniciarSesion(ByVal value As UsuarioDTO, ByVal intentosFallidos As Integer) As UsuarioDTO Implements IAutenticador.IniciarSesion
        Dim usuarioIntentoActual As UsuarioDTO = Nothing

        Me._intentosFallidos = intentosFallidos

        If Not value Is Nothing AndAlso Not String.IsNullOrWhiteSpace(value.Nombre) AndAlso _
            Not String.IsNullOrWhiteSpace(value.Clave) Then

            usuarioIntentoActual = Me._UsuarioBLL.Consulta(value)

            If Not usuarioIntentoActual Is Nothing Then

                If usuarioIntentoActual.Nombre.ToUpper().Equals(value.Nombre.ToUpper()) AndAlso _
                    usuarioIntentoActual.Clave.Equals(value.Clave) AndAlso _
                    usuarioIntentoActual.Bloqueado = False AndAlso _
                    usuarioIntentoActual.Eliminado = False Then

                    Me._intentosFallidos = 0
                Else
                    Me._intentosFallidos += 1

                    ' si se llego a los 3 intentos fallidos, bloquear al usuario
                    If Me.IntentosFallidos.Equals(3) Then
                        usuarioIntentoActual.Bloqueado = True
                        Me._UsuarioBLL.Modificacion(usuarioIntentoActual)
                    End If

                    ' limpiar el usuario actual ya que tiene datos invalidos
                    usuarioIntentoActual = Nothing
                End If
            End If
        End If
        Return usuarioIntentoActual
    End Function

    ''' <summary>
    ''' Valida si existe el nombre de un pemiso dentro de una lista de permisos.
    ''' </summary>
    Public Function ValidarPermiso(ByVal nombrePermiso As String, ByVal lista As List(Of PermisoDTO)) As Boolean Implements IAutenticador.ValidarPermiso
        Dim contiene As Boolean = False
        If Not lista Is Nothing Then
            For Each perm As PermisoDTO In lista
                If perm.Nombre.ToUpper().Equals(nombrePermiso.ToUpper()) Then
                    contiene = True
                    Exit For
                Else
                    ' es permiso compuesto?
                    If TypeOf perm Is PermisoCompuestoDTO Then
                        contiene = Me.ValidarPermiso(nombrePermiso, perm.Hijos)
                        If contiene Then Exit For
                    End If
                End If
            Next
        End If
        Return contiene
    End Function
End Class
