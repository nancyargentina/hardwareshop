Public Class ErrorHandler
    Public Shared Function ObtenerMensajeDeError(ByVal ex As Exception) As String
        Dim mensaje As String = ex.Message
        If Not ex.InnerException Is Nothing Then
            mensaje += String.Format("<br /><small>{0}</small>", ex.InnerException.Message)
        End If
        Return mensaje
    End Function
End Class