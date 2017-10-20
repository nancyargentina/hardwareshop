Imports HS.BE
Public Class UsuarioConversor
    Implements IConversor(Of BE.UsuarioDTO)

    Private _usuPermisoDAL As UsuarioPermisoDAL = New UsuarioPermisoDAL()

    Public Function Convertir(ByVal row As System.Data.DataRow) As BE.UsuarioDTO Implements IConversor(Of BE.UsuarioDTO).Convertir
        Dim usuario As UsuarioDTO = New UsuarioDTO
        usuario.Id = Convert.ToInt32(row("Usu_Id"))
        usuario.Nombre = Convert.ToString(row("Usu_Nombre"))
        usuario.Clave = Convert.ToString(row("Usu_Clave"))
        usuario.Email = Convert.ToString(row("Usu_Email"))
        usuario.Bloqueado = Convert.ToBoolean(row("Usu_Bloqueado"))
        usuario.Eliminado = Convert.ToBoolean(row("Usu_Eliminado"))
        usuario.DigitoHorizontal = Convert.ToInt32(row("DigitoHorizontal"))
        ' obtener el perfil del usuario
        Me._usuPermisoDAL.UsuarioActual = usuario
        usuario.Perfil = Me._usuPermisoDAL.ConsultaRango(Nothing, Nothing)

        Return usuario
    End Function

    Public Function Convertir(ByVal reader As System.Data.IDataReader) As BE.UsuarioDTO Implements IConversor(Of BE.UsuarioDTO).Convertir
        Dim usuario As UsuarioDTO = New UsuarioDTO
        usuario.Id = Convert.ToInt32(reader("Usu_Id"))
        usuario.Nombre = Convert.ToString(reader("Usu_Nombre"))
        usuario.Clave = Convert.ToString(reader("Usu_Clave"))
        usuario.Email = Convert.ToString(reader("Usu_Email"))
        usuario.Bloqueado = Convert.ToBoolean(reader("Usu_Bloqueado"))
        usuario.Eliminado = Convert.ToBoolean(reader("Usu_Eliminado"))
        usuario.DigitoHorizontal = Convert.ToInt32(reader("DigitoHorizontal"))
        ' obtener el perfil del usuario
        Me._usuPermisoDAL.UsuarioActual = usuario
        usuario.Perfil = Me._usuPermisoDAL.ConsultaRango(Nothing, Nothing)

        Return usuario
    End Function
End Class

