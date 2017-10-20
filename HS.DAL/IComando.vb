Imports System.Data

''' <summary>
''' Interfaz que define los metodos que deben implementar todos los proveedores
''' de acceso a datos.
''' </summary>
''' <remarks></remarks>
Public Interface IComando
    Function CrearComando(ByVal commandText As String, ByVal commandType As CommandType) As IDbCommand
    Function AgregarParametro(ByVal command As IDbCommand, ByVal parameterName As String, ByVal value As Object) As IDataParameter
    Function AgregarParametro(ByVal command As IDbCommand, ByVal parameterName As String, ByVal value As Object, ByVal type As DbType, ByVal direction As ParameterDirection) As IDataParameter

    Function EjecutarConsulta(ByVal command As IDbCommand) As Integer

    Function EjecutarConsultaEscalar(ByVal command As System.Data.IDbCommand) As Integer
    Function ConsultarReader(ByVal command As IDbCommand) As IDataReader
    Function ConsultarDataTable(ByVal command As IDbCommand) As DataTable

    Function ObtenerConexion() As IDbConnection
    Sub CerrarConexion(ByVal command As IDbCommand)

    Function IniciarTransaccion() As IDbTransaction
    Sub FinalizarTransaccion(ByVal command As IDbCommand)
    Sub CancelarTransaccion(ByVal command As IDbCommand)

End Interface
