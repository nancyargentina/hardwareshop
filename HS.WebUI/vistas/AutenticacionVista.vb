Imports HS.BE
Imports HS.BLL
Imports System.Web.HttpContext

''' <summary>
''' Provee funcionalidades de inicio y fin de sesion.
''' </summary>
Public Class AutenticacionVista

    ''' <summary>
    ''' Objeto de la capa de negocio dinamico que obtiene y actualiza objetos
    ''' del tipo Usuario.
    ''' </summary>
    Private _autenticador As IAutenticador = New Autenticador()

    ''' <summary>
    ''' Obtiene el usuario de la sesin actual, o Nothing (null en C#), si no
    ''' esta dentro de una sesion valida.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Usuario o Nothing si no esta dentro de una sesion valida.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UsuarioActual() As UsuarioDTO
        Get
            If Not HttpContext.Current Is Nothing Then
                If Not HttpContext.Current.Session("UsuarioActual") Is Nothing Then
                    ' existe sesion de usuario, usuario logueado y dentro de una
                    ' sesion valida
                    Return CType(HttpContext.Current.Session("UsuarioActual"), UsuarioDTO)
                Else
                    ' no existe ninguna sesion de usuario valida en el contexto actual
                    ' usuario no logueado o sesion expirada
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Cuanta los intentos fallidos de inicio de sesion de un usuario valido
    ''' </summary>
    ''' <value></value>
    Public Property IntentosFallidos() As Integer
        Get
            If Not HttpContext.Current Is Nothing Then
                If Not HttpContext.Current.Session("IntentosFallidos") Is Nothing Then
                    ' existe sesion de intentos fallidos
                    Return CType(HttpContext.Current.Session("IntentosFallidos"), Integer)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("IntentosFallidos") = value
        End Set
    End Property

    ''' <summary>
    ''' Inicia sesion con un usuario
    ''' </summary>
    Public Function IniciarSesion(ByVal value As UsuarioDTO) As Boolean
        ' por predeterminado se retorna falso, a menos que todas las condiciones sean validas
        Dim usuarioIntentoActual As UsuarioDTO = Me._autenticador.IniciarSesion(value, Me.IntentosFallidos)

        ' guardar los intentos fallidos
        Me.IntentosFallidos = Me._autenticador.IntentosFallidos
        Dim loginOk = False
        If Not usuarioIntentoActual Is Nothing Then
            ' es un usuario valido, crear session the usuario
            If Not HttpContext.Current Is Nothing Then
                HttpContext.Current.Session("UsuarioActual") = usuarioIntentoActual
            End If
            loginOk = True
        End If
        If loginOk Then
            Dim mBitacora As BitacoraBLL = New BitacoraBLL()
            mBitacora.Loguear(BitacoraBLL.TIPOLOG.LOGINOK, value.Nombre)
        Else
            Dim mBitacora As BitacoraBLL = New BitacoraBLL()
            mBitacora.Loguear(BitacoraBLL.TIPOLOG.LOGINFAIL, value.Nombre)
        End If
        Return (Not usuarioIntentoActual Is Nothing)
    End Function


    ''' <summary>
    ''' Elimina toda la informacion de la sesion del Usuario actual.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CerrarSesion()
        ' limpiar al sesion
        HttpContext.Current.Session.Clear()
    End Sub

    ''' <summary>
    ''' Retorna una instancia de un usuario valido de acuerdo a los parametros requeridos
    ''' de inicio de sesion.
    ''' </summary>
    Public Function CrearUsuarioParaIniciarSesion(ByVal nombre As String, ByVal clave As String) As UsuarioDTO
        Dim usuarioLogin As UsuarioDTO = New UsuarioDTO()
        usuarioLogin.Nombre = nombre
        usuarioLogin.Clave = Encrypter.EncriptarSHA512(clave)
        usuarioLogin.Eliminado = False
        usuarioLogin.Bloqueado = False
        Return usuarioLogin
    End Function

    ''' <summary>
    ''' Valida que un usuairo posee un permiso dentro de su perfil.
    ''' </summary>
    Public Function UsuarioPoseePermiso(ByVal usuarioActual As UsuarioDTO, ByVal nombrePermiso As String) As Boolean
        Dim contiene As Boolean = False
        If Not usuarioActual Is Nothing Then
            contiene = Me._autenticador.ValidarPermiso(nombrePermiso, usuarioActual.Perfil)
        End If
        Return contiene
    End Function

    Sub BloquearUsuario(UsuarioActual As UsuarioDTO)
        If Not UsuarioActual Is Nothing Then
            Dim mUsuarioBLL As UsuarioBLL = New UsuarioBLL()
            mUsuarioBLL.BloquearUsuario(UsuarioActual)
        End If
    End Sub

    Function UsuarioBloqueado(usuarioLogin As UsuarioDTO) As Boolean
        If Not usuarioLogin Is Nothing Then
            Dim mUsuarioBLL As UsuarioBLL = New UsuarioBLL()
            Dim mDTO = mUsuarioBLL.Consulta(usuarioLogin)
            If mDTO Is Nothing Then
                Return False
            End If
            Return mDTO.Bloqueado
        End If
        Return False
    End Function

End Class
