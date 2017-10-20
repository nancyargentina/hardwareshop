Imports System.Configuration

Public Class ComandoFactory

    ''' <summary>
    ''' Retorna una instancia del objeto que se va a conectar a la base de datos.
    ''' </summary>
    Public Shared Function CrearComando(ByVal connectionKey As String) As IComando
        Dim connSettings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(connectionKey)
        If Not connSettings Is Nothing Then
            Select Case connSettings.ProviderName.ToUpper()
                Case "SYSTEM.DATA.SQLCLIENT"
                    Return New DB(connSettings.ConnectionString)
                Case Else
                    Return New DB(connSettings.ConnectionString)
            End Select
        Else
            Throw New ConfigurationErrorsException(String.Format("No se configuró la cadena de conexion para la entrada '{0}'.", connectionKey))
        End If

    End Function


End Class
