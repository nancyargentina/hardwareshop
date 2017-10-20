Imports HS.BE
Imports HS.DAL

Public Interface IUsuarioBLL
    Inherits ICRUD(Of UsuarioDTO)

    ''' <summary>
    ''' Agrega un permiso al perfil del usuario.
    ''' </summary>
    Function AgregarPermiso(ByVal usuario As BE.UsuarioDTO, ByVal permiso As BE.PermisoDTO) As Boolean
    ''' <summary>
    ''' Quita un permiso del perfil del usuario.
    ''' </summary>
    Function QuitarPermiso(ByVal usuario As BE.UsuarioDTO, ByVal permiso As BE.PermisoDTO) As Boolean

End Interface

''' <summary>
''' Gestiona los usuarios del sistema.
''' </summary>
Public Class UsuarioBLL
    Implements IUsuarioBLL

    ''' <summary>
    ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
    ''' </summary>
    Private _dal As IUsuarioDAL = Nothing
    Private _dalPerfil As IUsuarioPermisoDAL = Nothing

    Public Sub New(ByVal pDAO As IUsuarioDAL, ByVal pPerfilDAL As IUsuarioPermisoDAL)
        Me._dal = pDAO
        Me._dalPerfil = pPerfilDAL
    End Sub

    Public Sub New()
        Me._dal = New UsuarioDAL()
        Me._dalPerfil = New UsuarioPermisoDAL()
    End Sub

    ''' <summary>
    ''' Agrega un nuevo usuario al sistema.
    ''' </summary>
    Public Function Alta(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioBLL.Alta
        Try
            Return Me._dal.Alta(value)
        Catch ex As Exception
            Throw New Exception("No se puede agregar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina un usuario existente del sistema.
    ''' </summary>
    Public Function Baja(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioBLL.Baja
        Try
            Return Me._dal.Baja(value)
        Catch ex As Exception
            Throw New Exception("No se puede eliminar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Retorna el primer usuario que coincida con el filtro especificado.
    ''' </summary>
    Public Function Consulta(ByRef filtro As BE.UsuarioDTO) As BE.UsuarioDTO Implements IUsuarioBLL.Consulta
        Try
            Return Me._dal.Consulta(filtro)
        Catch ex As Exception
            Throw New Exception("No se puede consultar.", ex)
        End Try
    End Function

    Public Function BloquearUsuario(ByRef filtro As BE.UsuarioDTO) As Boolean
        Try
            Return Me._dal.BloquearCuenta(filtro)
        Catch ex As Exception
            Throw New Exception("No se puede consultar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Retorna todos los usuarios que coincidan con el filtro especificado.
    ''' </summary>
    Public Function ConsultaRango(ByRef filtroDesde As BE.UsuarioDTO, ByRef filtroHasta As BE.UsuarioDTO) As System.Collections.Generic.List(Of BE.UsuarioDTO) Implements IUsuarioBLL.ConsultaRango
        Try
            Return Me._dal.ConsultaRango(filtroDesde, filtroHasta)
        Catch ex As Exception
            Throw New Exception("No se puede consultar por rango.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Modifica un usuario existente del sistema.
    ''' </summary>
    Public Function Modificacion(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioBLL.Modificacion
        Try
            Return Me._dal.Modificacion(value)
        Catch ex As Exception
            Throw New Exception("No se puede modificar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Agrega un permiso al perfil del usuario.
    ''' </summary>
    Public Function AgregarPermiso(ByVal usuario As BE.UsuarioDTO, ByVal permiso As BE.PermisoDTO) As Boolean Implements IUsuarioBLL.AgregarPermiso
        Try
            Me._dalPerfil.UsuarioActual = usuario
            Return Me._dalPerfil.Alta(permiso)
        Catch ex As Exception
            Throw New Exception("No se puede agregar el permiso al perfil del usuario.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Quita un permiso del perfil del usuario.
    ''' </summary>
    Public Function QuitarPermiso(ByVal usuario As BE.UsuarioDTO, ByVal permiso As BE.PermisoDTO) As Boolean Implements IUsuarioBLL.QuitarPermiso
        Try
            Me._dalPerfil.UsuarioActual = usuario
            Return Me._dalPerfil.Baja(permiso)
        Catch ex As Exception
            Throw New Exception("No se puede quitar el permiso del perfil del usuario.", ex)
        End Try
    End Function

End Class

