Imports System.Data.SqlClient

Public Class DB
    Implements IComando

    Private connectionString As String
    Private connection As SqlConnection
    Private transaction As SqlTransaction

    Public Sub New(ByVal connectionString As String)
        Me.connectionString = connectionString
    End Sub

    Public Function AgregarParametro(ByVal command As System.Data.IDbCommand, ByVal parameterName As String, ByVal value As Object) As System.Data.IDataParameter Implements IComando.AgregarParametro
        Dim param As SqlParameter = New SqlParameter(parameterName, value)
        command.Parameters.Add(param)
        Return param
    End Function

    Public Function AgregarParametro(ByVal command As System.Data.IDbCommand, ByVal parameterName As String, ByVal value As Object, ByVal type As System.Data.DbType, ByVal direction As System.Data.ParameterDirection) As System.Data.IDataParameter Implements IComando.AgregarParametro
        Dim param As SqlParameter = New SqlParameter(parameterName, value)
        param.DbType = type
        param.Direction = direction
        command.Parameters.Add(param)
        Return param
    End Function

    Public Function IniciarTransaccion() As System.Data.IDbTransaction Implements IComando.IniciarTransaccion
        Return Me.ValidarConnection().BeginTransaction()
    End Function

    Public Sub CerrarConexion(ByVal command As System.Data.IDbCommand) Implements IComando.CerrarConexion
        If Not command.Connection Is Nothing Then
            If command.Connection.State <> ConnectionState.Closed Then
                command.Connection.Close()
            End If
        End If
    End Sub

    Public Sub FinalizarTransaccion(ByVal command As IDbCommand) Implements IComando.FinalizarTransaccion
        If Not command.Transaction Is Nothing Then
            Me.transaction = CType(command.Transaction, SqlTransaction)
        End If

        If Not Me.transaction Is Nothing Then
            Me.transaction.Commit()
            Me.transaction = Nothing
        End If
    End Sub

    Public Function CrearComando(ByVal commandText As String, ByVal commandType As System.Data.CommandType) As System.Data.IDbCommand Implements IComando.CrearComando
        Dim command As SqlCommand = New SqlCommand(commandText)
        command.CommandType = commandType
        Return command
    End Function

    Public Function EjecutarConsulta(ByVal command As System.Data.IDbCommand) As Integer Implements IComando.EjecutarConsulta
        command.Connection = Me.ValidarConnection()
        command.Transaction = Me.transaction
        Return command.ExecuteNonQuery()
    End Function

    Public Function EjecutarConsultaEscalar(ByVal command As System.Data.IDbCommand) As Integer Implements IComando.EjecutarConsultaEscalar
        command.Connection = Me.ValidarConnection()
        command.Transaction = Me.transaction
        Return CInt(command.ExecuteScalar())
    End Function


    Public Function ConsultarReader(ByVal command As System.Data.IDbCommand) As System.Data.IDataReader Implements IComando.ConsultarReader
        command.Connection = Me.ValidarConnection()
        Return command.ExecuteReader
    End Function

    Public Function ConsultarDataTable(ByVal command As System.Data.IDbCommand) As System.Data.DataTable Implements IComando.ConsultarDataTable
        Dim table As DataTable = New DataTable()
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(CType(command, SqlCommand))
        command.Connection = Me.ValidarConnection()
        adapter.Fill(table)
        Return table
    End Function

    Public Function ObtenerConexion() As System.Data.IDbConnection Implements IComando.ObtenerConexion
        If Me.connection Is Nothing Then
            Me.connection = New SqlConnection(connectionString)
        End If

        Return Me.connection
    End Function

    Public Sub CancelarTransaccion(ByVal command As IDbCommand) Implements IComando.CancelarTransaccion
        If Not command.Transaction Is Nothing Then
            Me.transaction = CType(command.Transaction, SqlTransaction)
        End If

        If Not Me.transaction Is Nothing Then
            Me.transaction.Rollback()
            Me.transaction = Nothing
        End If
    End Sub

    Private Function ValidarConnection() As SqlConnection
        If Me.connection Is Nothing Then
            Me.connection = CType(Me.ObtenerConexion(), SqlConnection)
        End If

        If Me.connection.State <> ConnectionState.Open Then
            Me.connection.Open()
        End If

        Return Me.connection
    End Function

    
End Class

