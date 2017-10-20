Imports HS.BE
Imports HS.BLL

Public Class UsuarioVista

    Private _usuario As UsuarioDTO
    Public Property Usuario() As UsuarioDTO
        Get
            If _usuario Is Nothing Then _usuario = New UsuarioDTO()
            Return _usuario
        End Get
        Set(ByVal value As UsuarioDTO)
            _usuario = value
        End Set
    End Property

    Private _usuarioBLL As IUsuarioBLL
    Public Property usuarioBLL() As IUsuarioBLL
        Get
            If _usuarioBLL Is Nothing Then _usuarioBLL = New usuarioBLL()
            Return _usuarioBLL
        End Get
        Set(ByVal value As IUsuarioBLL)
            _usuarioBLL = value
        End Set
    End Property

    Public Function ObtenerPorId(ByVal id As Integer) As UsuarioDTO
        'modificar
        Dim filtro As UsuarioDTO = New UsuarioDTO
        filtro.Id = id
        Me.Usuario = Me.usuarioBLL.Consulta(filtro)
        Return Me.Usuario
    End Function

    Public Function ObtenerPorId(ByVal id As String) As UsuarioDTO
        If IsNumeric(id) Then
            Return Me.ObtenerPorId(Convert.ToInt32(id))
        Else
            Me.Usuario = Nothing
        End If
        Return Me.Usuario
    End Function

    Public Function EliminarPorId(ByVal id As String) As Boolean
        Dim filtro As UsuarioDTO = New UsuarioDTO
        filtro.Id = Convert.ToInt32(id)
        Dim _usuario As UsuarioDTO = Me.usuarioBLL.Consulta(filtro)
        If Not _usuario Is Nothing Then
            _usuario.Eliminado = True
            Return Me.usuarioBLL.Baja(_usuario)
        Else
            Return False
        End If
    End Function

    Public Function RestaurarPorId(ByVal id As String) As Boolean
        Dim filtro As UsuarioDTO = New UsuarioDTO
        filtro.Id = Convert.ToInt32(id)
        Dim _usuario As UsuarioDTO = Me.usuarioBLL.Consulta(filtro)
        If Not _usuario Is Nothing Then
            _usuario.Eliminado = False
            Return Me.usuarioBLL.Baja(_usuario)
        Else
            Return False
        End If
    End Function

    Public Sub LlenarGrilla(ByRef dataGrid As System.Web.UI.WebControls.GridView)
        'traer todos los usuarios y enlazarlo al DataSource del control
        dataGrid.DataSource = Me.usuarioBLL.ConsultaRango(Nothing, Nothing)
        dataGrid.DataBind()
    End Sub

    Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl)
        Dim lista As List(Of UsuarioDTO) = Me.usuarioBLL.ConsultaRango(Nothing, Nothing)
        Me.LlenarLista(listBox, lista)
    End Sub

    Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl, ByVal lista As List(Of UsuarioDTO))
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
