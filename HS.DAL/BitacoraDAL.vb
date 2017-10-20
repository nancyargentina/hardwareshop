Imports System.Data
Imports HS.BE
Imports HS.DAL

Public Interface IBitacoraDAL
    Inherits IMapeador(Of BitacoraDTO) ', IVerificator(Of BitacoraDTO)

End Interface

Public Class BitacoraDAL
    Implements IBitacoraDAL


    ''' <summary>
    ''' objeto que encapsula la funcionalidad de acceso, persistencia y lectura
    ''' de datos en el origen de datos.
    ''' </summary>
    ''' <remarks></remarks>
    Private _wrapper As IComando = Nothing
    ''' <summary>
    ''' conversor a entidades de los datos devueltos por la consulta SQL.
    ''' </summary>
    ''' <remarks></remarks>
    Private _conversor As IConversor(Of BE.BitacoraDTO) = Nothing


    Public Function Alta(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Alta
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("INSERT INTO BITACORA VALUES(0, @log,0) SET @identity=@@Identity", CommandType.Text)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Dim log As String = CStr(value.Fecha) + "|" + value.Autor + "|" + value.Descripcion + "|" + value.Criticidad
            Me.Wrapper.AgregarParametro(comando, "@log", log)


            Dim paramRet As IDataParameter = Me.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output)

            ' ejecutar el comando/consulta SQL en el origen de datos
            resultado = Me._wrapper.EjecutarConsulta(comando)

            ' asignar el Id devuelto por la consulta al objeto
            'If (resultado > 0) Then
            '    value.Id = CType(paramRet.Value, Integer)

            '    ' Calculo el nuevo digito horizontal
            '    value.DigitoHorizontal = CalcularDVH(value)
            '    Modificacion(value)

            '    VerificadorDAL.ActualizarDVV("BITACORA", "IDBITACORA")
            'End If

        Catch
            ' si se produjo una excepcion, devolverla al objeto que invocó al metodo (sin encapsularla)
            Throw
        Finally
            ' independientemente de si la accion fue exitosa o se produzco una excepcion
            ' cerrar la conexion
            Me.Wrapper.CerrarConexion(comando)
        End Try
        ' este metodo retornará true si hubo registros afectados en el origen de datos
        Return (resultado > 0)
    End Function

    Public Function Baja(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Baja
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE BITACORA SET eliminado=@eliminado WHERE idbitacora=@id", CommandType.Text)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)

            ' ejecutar el comando/consulta SQL en el origen de datos
            resultado = Me._wrapper.EjecutarConsulta(comando)
        Catch
            ' si se produjo una excepcion, devolverla al objeto que invocó al metodo (sin encapsularla)
            Throw
        Finally
            ' independientemente de si la accion fue exitosa o se produzco una excepcion
            ' cerrar la conexion
            Me.Wrapper.CerrarConexion(comando)
        End Try
        ' este metodo retornará true si hubo registros afectados en el origen de datos
        Return (resultado > 0)
    End Function

    Public Function Consulta(ByRef filtro As BitacoraDTO) As BitacoraDTO Implements IMapeador(Of BitacoraDTO).Consulta
        Dim lista As List(Of BE.BitacoraDTO) = Me.ConsultaRango(filtro, Nothing)
        If Not lista Is Nothing AndAlso lista.Count > 0 Then
            ' retornar solo el primer objeto que cumpla con el filtro
            Return lista(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function ConsultaRango(ByRef filtroDesde As BitacoraDTO, ByRef filtroHasta As BitacoraDTO) As List(Of BitacoraDTO) Implements IMapeador(Of BitacoraDTO).ConsultaRango
        Dim lista As List(Of BE.BitacoraDTO) = New List(Of BE.BitacoraDTO)

        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("SELECT * FROM BITACORA WHERE (idbitacora=@id OR @id IS NULL)", CommandType.Text)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            ' solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
            If Not filtroDesde Is Nothing AndAlso filtroDesde.Id > 0 Then
                Me.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id)
            Else
                Me.Wrapper.AgregarParametro(comando, "@id", DBNull.Value)
            End If

            ' ejecutar el comando/consulta SQL en el origen de datos
            ' la instruccion Usuing nos garantiza que el objeto reader va a ser cerrado luego de ser consumido
            Using reader As IDataReader = Me.Wrapper.ConsultarReader(comando)

                ' recorrer el IDataReader obtenido de la base de datos y convertirlo a un objeto entidad
                Do While reader.Read()
                    ' delegarle la responsabilidad de convertir un IDataReader al objeto Conversor
                    lista.Add(Me.Conversor.Convertir(reader))
                Loop

            End Using

        Catch
            ' si se produjo una excepcion, devolverla al objeto que invocó al metodo (sin encapsularla)
            Throw
        Finally
            ' independientemente de si la accion fue exitosa o se produzco una excepcion
            ' cerrar la conexion
            Me.Wrapper.CerrarConexion(comando)
        End Try



        ' este metodo retornará la lista con todas las entidades convertidas que
        ' se obtuvieron del origen de datos
        Return lista
    End Function

    Public Function Modificacion(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Modificacion
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE BITACORA SET LOG=@log, DIGITOHORIZONTAL=@digitohorizontal, ELIMINADO=@eliminado WHERE idbitacora=@id", CommandType.Text)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Dim log As String = CStr(value.Fecha) + "|" + value.Autor + "|" + value.Descripcion + "|" + value.Criticidad
            Me.Wrapper.AgregarParametro(comando, "@log", log)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)

            'value.DigitoHorizontal = CalcularDVH(value)
            'Me.Wrapper.AgregarParametro(comando, "@digitohorizontal", value.DigitoHorizontal)

            ' ejecutar el comando/consulta SQL en el origen de datos
            resultado = Me._wrapper.EjecutarConsulta(comando)

        Catch
            ' si se produjo una excepcion, devolverla al objeto que invocó al metodo (sin encapsularla)
            Throw
        Finally
            ' independientemente de si la accion fue exitosa o se produzco una excepcion
            ' cerrar la conexion
            Me.Wrapper.CerrarConexion(comando)
        End Try
        ' este metodo retornará true si hubo registros afectados en el origen de datos
        Return (resultado > 0)
    End Function

    Public Property Conversor As IConversor(Of BitacoraDTO) Implements IMapeador(Of BitacoraDTO).Conversor
        Get
            If Me._conversor Is Nothing Then
                ' obtener el conversor por defecto para esta entidad
                Me._conversor = New BitacoraConversor()
            End If
            Return Me._conversor
        End Get
        Set(ByVal value As IConversor(Of BitacoraDTO))
            Me._conversor = value
        End Set
    End Property



    Public Property Wrapper As IComando Implements IMapeador(Of BitacoraDTO).Wrapper
        Get
            If Me._wrapper Is Nothing Then
                ' obtener el wrapper por defecto
                Me._wrapper = ComandoFactory.CrearComando("Default")
            End If
            Return Me._wrapper
        End Get
        Set(ByVal value As IComando)
            Me._wrapper = value
        End Set
    End Property


    'Private Function CalcularDVH(ByRef value As BitacoraDTO) As Integer Implements IVerificator(Of BitacoraDTO).CalcularDVH
    '    Dim DVH As Integer = 0
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Id), 0)
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Fecha), 1)
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Descripcion), 2)
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Autor), 3)
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Criticidad), 4)
    '    DVH += DBUtils.CalcularDigitoVerificador(CStr(value.Eliminado), 5)
    '    Return DVH
    'End Function



    'Private Sub ActualizarDVH(value As BitacoraDTO) Implements IVerificator(Of BitacoraDTO).ActualizarDVH
    '    value.DigitoHorizontal = CalcularDVH(value)
    '    Modificacion(value)
    'End Sub

    'Public Sub ActualizarDVHTabla() Implements IVerificator(Of BitacoraDTO).ActualizarDVHTabla
    '    Dim listaDTO As List(Of BitacoraDTO) = ConsultaRango(Nothing, Nothing)
    '    For Each objDTO As BitacoraDTO In listaDTO
    '        ActualizarDVH(objDTO)
    '    Next
    'End Sub

    'Private Function VerificarDVH(value As BitacoraDTO) As Boolean Implements IVerificator(Of BitacoraDTO).VerificarDVH
    '    If (value.DigitoHorizontal <> CalcularDVH(value)) Then
    '        Return False
    '    End If
    '    Return True
    'End Function

    'Public Function VerificarDVHTabla() As Boolean Implements IVerificator(Of BitacoraDTO).VerificarDVHTabla
    '    Dim listaDTO As List(Of BitacoraDTO) = ConsultaRango(Nothing, Nothing)
    '    For Each objDTO As BitacoraDTO In listaDTO
    '        If (Not VerificarDVH(objDTO)) Then
    '            Throw New Exception("Verificacion Digito Horizontal en tabla BITACORA, id:" + CStr(objDTO.Id) + " Fallido")
    '        End If
    '    Next
    '    Return True
    'End Function
End Class
