Imports HS.BE

Public Class PermisoConversor
    Implements IConversor(Of BE.PermisoDTO)

    Private _famPemisosDAL As FamiliaPermisoDAL = New FamiliaPermisoDAL()

    Public Function Convertir(ByVal row As System.Data.DataRow) As BE.PermisoDTO Implements IConversor(Of BE.PermisoDTO).Convertir
        Dim cantHijos As Int32 = Convert.ToInt32(row("Cant_Hijos"))

        Dim permiso As PermisoDTO
        If cantHijos > 0 Then
            permiso = New PermisoCompuestoDTO
        Else
            permiso = New PermisoSimple
        End If
        permiso.Id = Convert.ToInt32(row("Per_Id"))
        permiso.Nombre = Convert.ToString(row("Per_Nombre"))
        permiso.Descripcion = Convert.ToString(row("Per_Descripcion"))
        permiso.Eliminado = Convert.ToBoolean(row("Per_Eliminado"))

        ' obtener los permisos hijos del permiso actual
        If cantHijos > 0 Then
            Me._famPemisosDAL.PermisoPadre = permiso
            permiso.Hijos = Me._famPemisosDAL.ConsultaRango(Nothing, Nothing)
        End If

        Return permiso
    End Function

    Public Function Convertir(ByVal reader As System.Data.IDataReader) As BE.PermisoDTO Implements IConversor(Of BE.PermisoDTO).Convertir
        Dim cantHijos As Int32 = Convert.ToInt32(reader("Cant_Hijos"))

        Dim permiso As PermisoDTO
        If cantHijos > 0 Then
            permiso = New PermisoCompuestoDTO
        Else
            permiso = New PermisoSimple
        End If
        permiso.Id = Convert.ToInt32(reader("Per_Id"))
        permiso.Nombre = Convert.ToString(reader("Per_Nombre"))
        permiso.Descripcion = Convert.ToString(reader("Per_Descripcion"))
        permiso.Eliminado = Convert.ToBoolean(reader("Per_Eliminado"))

        ' obtener los permisos hijos del permiso actual
        If cantHijos > 0 Then
            Me._famPemisosDAL.PermisoPadre = permiso
            permiso.Hijos = Me._famPemisosDAL.ConsultaRango(Nothing, Nothing)
        End If

        Return permiso
    End Function
End Class
