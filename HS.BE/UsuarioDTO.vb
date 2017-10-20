'<Serializable()>
Public Class UsuarioDTO

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

    Private _clave As String
    Public Property Clave() As String
        Get
            Return _clave
        End Get
        Set(ByVal value As String)
            _clave = value
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Private _bloqueado As Boolean
    Public Property Bloqueado() As Boolean
        Get
            Return _bloqueado
        End Get
        Set(ByVal value As Boolean)
            _bloqueado = value
        End Set
    End Property

    Private _digitohorizontal As Integer
    Public Property DigitoHorizontal() As Integer
        Get
            Return _digitohorizontal
        End Get
        Set(ByVal value As Integer)
            _digitohorizontal = value
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

    Private _perfil As List(Of PermisoDTO)
    Public Property Perfil() As List(Of PermisoDTO)
        Get
            If _perfil Is Nothing Then _perfil = New List(Of PermisoDTO)
            Return _perfil
        End Get
        Set(ByVal value As List(Of PermisoDTO))
            _perfil = value
        End Set
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If Not obj Is Nothing AndAlso TypeOf obj Is UsuarioDTO Then
            Return CType(obj, UsuarioDTO).Id.Equals(Me.Id)
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