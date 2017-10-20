Imports HS.BE
Imports HS.BLL
Public Class PermisoVista
    Private _permiso As PermisoDTO
    Public Property Permiso() As PermisoDTO
        Get
            If _permiso Is Nothing Then _permiso = New PermisoSimple()
            Return _permiso
        End Get
        Set(ByVal value As PermisoDTO)
            _permiso = value
        End Set
    End Property

    Private _PermisoBLL As IPermisoBLL
    Public Property PermisoBLL() As IPermisoBLL
        Get
            If _PermisoBLL Is Nothing Then _PermisoBLL = New PermisoBLL()
            Return _PermisoBLL
        End Get
        Set(ByVal value As IPermisoBLL)
            _PermisoBLL = value
        End Set
    End Property

    Public Function ObtenerPorId(ByVal id As Integer) As PermisoDTO
        Dim filtro As PermisoDTO = New PermisoSimple
        filtro.Id = id
        Me.Permiso = Me.PermisoBLL.Consulta(filtro)
        Return Me.Permiso
    End Function

    Public Function ObtenerPorId(ByVal id As String) As PermisoDTO
        If IsNumeric(id) Then
            Return Me.ObtenerPorId(Convert.ToInt32(id))
        Else
            Me.Permiso = Nothing
        End If
        Return Me.Permiso
    End Function

    Public Function EliminarPorId(ByVal id As String) As Boolean
        Dim filtro As PermisoDTO = New PermisoSimple
        filtro.Id = Convert.ToInt32(id)
        Dim _permiso As PermisoDTO = Me.PermisoBLL.Consulta(filtro)
        If Not _permiso Is Nothing Then
            _permiso.Eliminado = True
            Return Me.PermisoBLL.Baja(_permiso)
        Else
            Return False
        End If
    End Function

    Public Function RestaurarPorId(ByVal id As String) As Boolean
        Dim filtro As PermisoDTO = New PermisoSimple
        filtro.Id = Convert.ToInt32(id)
        Dim _permiso As PermisoDTO = Me.PermisoBLL.Consulta(filtro)
        If Not _permiso Is Nothing Then
            _permiso.Eliminado = False
            Return Me.PermisoBLL.Baja(_permiso)
        Else
            Return False
        End If
    End Function

    Public Sub LlenarGrilla(ByRef dataGrid As System.Web.UI.WebControls.GridView)
        'traer todos los permisos y enlazarlo al DataSource del control
        dataGrid.DataSource = Me.PermisoBLL.ConsultaRango(Nothing, Nothing)
        dataGrid.DataBind()
    End Sub

    Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl)
        Dim lista As List(Of PermisoDTO) = Me.PermisoBLL.ConsultaRango(Nothing, Nothing)
        Me.LlenarLista(listBox, lista)
    End Sub

    Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl, ByVal lista As List(Of PermisoDTO))
        listBox.Items.Clear()
        listBox.DataTextField = "Nombre"
        listBox.DataValueField = "Id"
        'traer todos los permisos y enlazarlo al DataSource del control
        listBox.DataSource = lista
        listBox.DataBind()
    End Sub

    Public Function ObtenerIdEnLista(ByVal listBox As System.Web.UI.WebControls.ListControl) As Integer
        If Not listBox Is Nothing AndAlso Not listBox.SelectedItem Is Nothing Then
            Return Convert.ToInt32(listBox.SelectedItem.Value)
        Else
            Return 0
        End If
    End Function

End Class
