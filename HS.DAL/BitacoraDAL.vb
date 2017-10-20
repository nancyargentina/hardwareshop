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
    Private _wrapper As IComando = Nothing
    ''' <summary>
    ''' conversor a entidades de los datos devueltos por la consulta SQL.
    ''' </summary>
    Private _conversor As IConversor(Of BE.BitacoraDTO) = Nothing

    Public Function Alta(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Alta
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("INSERT INTO BITACORA VALUES(0, @log,0) SET @identity=@@Identity", CommandType.Text)
        Try
            Dim log As String = CStr(value.Fecha) + "|" + value.Autor + "|" + value.Descripcion + "|" + value.Criticidad
            Me.Wrapper.AgregarParametro(comando, "@log", log)


            Dim paramRet As IDataParameter = Me.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output)

            resultado = Me._wrapper.EjecutarConsulta(comando)

        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        ' este metodo retornará true si hubo registros afectados en el origen de datos
        Return (resultado > 0)
    End Function

    Public Function Baja(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Baja
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE BITACORA SET eliminado=@eliminado WHERE idbitacora=@id", CommandType.Text)
        Try
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)

            resultado = Me._wrapper.EjecutarConsulta(comando)
        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
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

        Dim comando As IDbCommand = Me.Wrapper.CrearComando("SELECT * FROM BITACORA WHERE (idbitacora=@id OR @id IS NULL)", CommandType.Text)
        Try
            If Not filtroDesde Is Nothing AndAlso filtroDesde.Id > 0 Then
                Me.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id)
            Else
                Me.Wrapper.AgregarParametro(comando, "@id", DBNull.Value)
            End If
            Using reader As IDataReader = Me.Wrapper.ConsultarReader(comando)

                Do While reader.Read()
                    lista.Add(Me.Conversor.Convertir(reader))
                Loop

            End Using

        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        Return lista
    End Function

    Public Function Modificacion(ByRef value As BitacoraDTO) As Boolean Implements IMapeador(Of BitacoraDTO).Modificacion
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE BITACORA SET LOG=@log, DIGITOHORIZONTAL=@digitohorizontal, ELIMINADO=@eliminado WHERE idbitacora=@id", CommandType.Text)
        Try
            Dim log As String = CStr(value.Fecha) + "|" + value.Autor + "|" + value.Descripcion + "|" + value.Criticidad
            Me.Wrapper.AgregarParametro(comando, "@log", log)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)

            resultado = Me._wrapper.EjecutarConsulta(comando)

        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        Return (resultado > 0)
    End Function

    Public Property Conversor As IConversor(Of BitacoraDTO) Implements IMapeador(Of BitacoraDTO).Conversor
        Get
            If Me._conversor Is Nothing Then
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
                Me._wrapper = ComandoFactory.CrearComando("Default")
            End If
            Return Me._wrapper
        End Get
        Set(ByVal value As IComando)
            Me._wrapper = value
        End Set
    End Property


End Class
