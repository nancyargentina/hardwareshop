Public MustInherit Class PermisoDTO

    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _descripcion As String
    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _eliminado As Boolean
    Public Property Eliminado() As Boolean
        Get
            Return _eliminado
        End Get
        Set(ByVal value As Boolean)
            _eliminado = value
        End Set
    End Property

    Private _hijos As List(Of PermisoDTO)
    Public Property Hijos() As List(Of PermisoDTO)
        Get
            If _hijos Is Nothing Then _hijos = New List(Of PermisoDTO)
            Return _hijos
        End Get
        Set(ByVal value As List(Of PermisoDTO))
            _hijos = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If Not obj Is Nothing AndAlso TypeOf obj Is PermisoDTO Then
            Return CType(obj, PermisoDTO).Id.Equals(Me.Id)
        Else
            Return False
        End If
    End Function

    Public Overrides Function ToString() As String
        Return Me.Nombre
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

End Class
