Public Interface IVerificator(Of T)

    Function CalcularDVH(ByRef value As T) As Integer
    Function VerificarDVH(value As T) As Boolean

    Function VerificarDVHTabla() As Boolean

    Sub ActualizarDVH(value As T)
    Sub ActualizarDVHTabla()



End Interface

