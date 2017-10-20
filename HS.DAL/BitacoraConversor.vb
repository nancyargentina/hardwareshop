Imports HS.BE

Public Class BitacoraConversor
    Implements IConversor(Of BE.BitacoraDTO)


    Public Function Convertir(row As DataRow) As BitacoraDTO Implements IConversor(Of BitacoraDTO).Convertir
        Dim bitacoraDTO As BitacoraDTO = New BitacoraDTO
        bitacoraDTO.Id = Convert.ToInt32(row("IDBitacora"))
        Dim log As String = Convert.ToString(row("LOG"))
        Dim logSplited() As String = log.Split(New String() {"|"}, StringSplitOptions.None)
        bitacoraDTO.Fecha = CDate(logSplited.GetValue(0))
        bitacoraDTO.Autor = CStr(logSplited.GetValue(1))
        bitacoraDTO.Descripcion = CStr(logSplited.GetValue(2))
        bitacoraDTO.Criticidad = CChar(logSplited.GetValue(3))
        bitacoraDTO.DigitoHorizontal = Convert.ToInt32(row("DigitoHorizontal"))
        bitacoraDTO.Eliminado = Convert.ToBoolean(row("Eliminado"))
        Return bitacoraDTO
    End Function

    Public Function Convertir(reader As IDataReader) As BitacoraDTO Implements IConversor(Of BitacoraDTO).Convertir
        Dim bitacoraDTO As BitacoraDTO = New BitacoraDTO

        bitacoraDTO.Id = Convert.ToInt32(reader("IDBitacora"))
        Dim log As String = Convert.ToString(reader("LOG"))
        Dim logSplited() As String = log.Split(New String() {"|"}, StringSplitOptions.None)
        bitacoraDTO.Fecha = CDate(logSplited.GetValue(0))
        bitacoraDTO.Autor = CStr(logSplited.GetValue(1))
        bitacoraDTO.Descripcion = CStr(logSplited.GetValue(2))
        bitacoraDTO.Criticidad = CChar(logSplited.GetValue(3))
        bitacoraDTO.DigitoHorizontal = Convert.ToInt32(reader("DigitoHorizontal"))
        bitacoraDTO.Eliminado = Convert.ToBoolean(reader("Eliminado"))

        Return bitacoraDTO
    End Function
End Class