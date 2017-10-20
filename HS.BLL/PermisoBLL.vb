Imports HS.BE
Imports HS.DAL

Public Interface IPermisoBLL
    Inherits ICRUD(Of PermisoDTO)

    ''' <summary>
    ''' Agrega nuevo un permiso hijo a otro permiso.
    ''' </summary>
    Function AgregarHijo(ByVal padre As BE.PermisoDTO, ByVal hijo As BE.PermisoDTO) As Boolean

    ''' <summary>
    ''' Elimina un permiso hijo de su permiso padre.
    ''' </summary>
    Function QuitarHijo(ByVal padre As BE.PermisoDTO, ByVal hijo As BE.PermisoDTO) As Boolean
End Interface

''' <summary>
''' Gestiona los permisos del sistema.
''' </summary>

Public Class PermisoBLL
    Implements IPermisoBLL

    ''' <summary>
    ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
    ''' </summary>
    Private _dao As IPermisoDAL = Nothing
    Private _daoFamilia As IFamiliaPermisoDAL = Nothing

    Public Sub New(ByVal pDAO As IPermisoDAL, ByVal pFamiliaDAO As IFamiliaPermisoDAL)
        Me._dao = pDAO
        Me._daoFamilia = pFamiliaDAO
    End Sub

    Public Sub New()
        Me._dao = New PermisoDAL()
        Me._daoFamilia = New FamiliaPermisoDAL()
    End Sub

    ''' <summary>
    ''' Agrega un nuevo permiso al sistema.
    ''' </summary>
    Public Function Alta(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoBLL.Alta
        Try
            Return Me._dao.Alta(value)
        Catch ex As Exception
            Throw New Exception("No se puede agregar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina un permiso existente del sistema.
    ''' </summary>
    Public Function Baja(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoBLL.Baja
        Try
            Return Me._dao.Baja(value)
        Catch ex As Exception
            Throw New Exception("No se puede eliminar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Retorna el primer permiso que coincida con el filtro especificado.
    ''' </summary>
    Public Function Consulta(ByRef filtro As BE.PermisoDTO) As BE.PermisoDTO Implements IPermisoBLL.Consulta
        Try
            Return Me._dao.Consulta(filtro)
        Catch ex As Exception
            Throw New Exception("No se puede consultar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Retorna todos los permisos que coincidan con el fitrol especificado.
    ''' </summary>
    Public Function ConsultaRango(ByRef filtroDesde As BE.PermisoDTO, ByRef filtroHasta As BE.PermisoDTO) As System.Collections.Generic.List(Of BE.PermisoDTO) Implements IPermisoBLL.ConsultaRango
        Try
            Return Me._dao.ConsultaRango(filtroDesde, filtroHasta)
        Catch ex As Exception
            Throw New Exception("No se puede consultar por rango.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Modifica un permiso existente del sistema.
    ''' </summary>
    Public Function Modificacion(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoBLL.Modificacion
        Try
            Return Me._dao.Modificacion(value)
        Catch ex As Exception
            Throw New Exception("No se puede modificar.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Agrega nuevo un permiso hijo a otro permiso.
    ''' </summary>
    Public Function AgregarHijo(ByVal padre As BE.PermisoDTO, ByVal hijo As BE.PermisoDTO) As Boolean Implements IPermisoBLL.AgregarHijo
        '1. validar que se pueda agregar
        If Not ValidarPermisoHijo(padre, hijo, padre) Then
            Throw New Exception("No se puede agregar el hijo, porque violaria la regla de recursividad.")
        End If

        Try
            '2. agregar
            Me._daoFamilia.PermisoPadre = padre
            Return Me._daoFamilia.Alta(hijo)
        Catch ex As Exception
            Throw New Exception("No se puede agregar el hijo.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina un permiso hijo de su permiso padre.
    ''' </summary>
    Public Function QuitarHijo(ByVal padre As BE.PermisoDTO, ByVal hijo As BE.PermisoDTO) As Boolean Implements IPermisoBLL.QuitarHijo
        Try
            'quitar
            Me._daoFamilia.PermisoPadre = padre
            Return Me._daoFamilia.Baja(hijo)
        Catch ex As Exception
            Throw New Exception("No se puede quitar el hijo.", ex)
        End Try
    End Function

    ''' <summary>
    ''' Valida recursivamente que el nuevo permiso hijo no este dentro de los padres o dentro de los padres de los padres.
    ''' </summary>
    Private Function ValidarPermisoHijo(ByVal padre As BE.PermisoDTO, ByVal nuevoHijo As BE.PermisoDTO, ByVal padreOriginal As BE.PermisoDTO) As Boolean
        'Para que un permiso se pueda agregar a otro permiso padre:
        '0) el nuevo permiso no debe ser nulo.
        '1) el nuevo permiso no debe ser el mismo permiso que el padre.
        '2) el nuevo permiso no debe ser ninguno de sus padres.
        '3) el nuevo permiso no debe ser ninguno de los padres de sus padres.
        Dim esValido As Boolean = True
        ' 0 no puede ser nulo
        If Not nuevoHijo Is Nothing Then
            ' 1 comparar contra el padre
            If Not padre.Equals(nuevoHijo) Then
                ' traer los padres del padre
                ' si tiene padres, recorrerlos recursivamente.
                Dim filtroPadre As PermisoDTO = New PermisoSimple()
                filtroPadre.Id = padre.Id
                Dim i As Int32 = 0
                Me._daoFamilia.PermisoPadre = Nothing
                Dim listaPadres As List(Of PermisoDTO) = Me._daoFamilia.ConsultaRango(Nothing, filtroPadre)
                Dim padreDePadre As PermisoDTO
                Do While i < listaPadres.Count And esValido
                    padreDePadre = listaPadres(i)
                    If Not padreDePadre.Equals(padreOriginal) Then
                        ' 2 validar que el padre del futuro permiso padre sea valido
                        esValido = Me.ValidarPermisoHijo(padreDePadre, nuevoHijo, padreOriginal)
                    End If
                    i += 1
                Loop
            Else
                esValido = False
            End If
        Else
            esValido = False
        End If
        Return esValido
    End Function

End Class

