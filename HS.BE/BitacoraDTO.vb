'<Serializable()>
Public Class BitacoraDTO

    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _fecha As Date
    Public Property Fecha() As Date
        Get
            Return _fecha
        End Get
        Set(ByVal value As Date)
            _fecha = value
        End Set
    End Property

    Private _autor As String
    Public Property Autor() As String
        Get
            Return _autor
        End Get
        Set(ByVal value As String)
            _autor = value
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




    Private _criticidad As Char
    Public Property Criticidad() As Char
        Get
            Return _criticidad
        End Get
        Set(ByVal value As Char)
            _criticidad = value
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




    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If Not obj Is Nothing AndAlso TypeOf obj Is BitacoraDTO Then
            Return CType(obj, BitacoraDTO).Id.Equals(Me.Id)
        Else
            Return False
        End If
    End Function

    Public Overrides Function ToString() As String
        Return Me.Descripcion
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function
End Class