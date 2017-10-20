''' <summary>
''' Interfaz que define los metodos que todas las clases que expongan mecanismos de
''' conversion desde origenes de datos deben implementar.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Interface IConversor(Of T)

    ''' <summary>
    ''' Convertir datos desde un objeto IDataReader.
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convertir(ByVal reader As IDataReader) As T

    ''' <summary>
    ''' Convertir datos desde un objeto DataRow.
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convertir(ByVal row As DataRow) As T

End Interface

