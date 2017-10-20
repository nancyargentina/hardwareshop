Imports System.Data
Imports HS.BE

Public Interface IPermisoDAL
    Inherits IMapeador(Of PermisoDTO)

End Interface

Public Class PermisoDAL
    Implements IPermisoDAL

    ''' <summary>
    ''' objeto que encapsula la funcionalidad de acceso, persistencia y lectura
    ''' de datos en el origen de datos.
    ''' </summary>
    ''' <remarks></remarks>
    Private _wrapper As IComando = Nothing
    ''' <summary>
    ''' conversor a BE de los datos devueltos por la consulta SQL.
    ''' </summary>
    ''' <remarks></remarks>
    Private _conversor As IConversor(Of BE.PermisoDTO) = Nothing

    Public Function Alta(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoDAL.Alta
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_PERMISO_AGREGAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Me.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre)
            Me.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion)
            Dim paramRet As IDataParameter = Me.Wrapper.AgregarParametro(comando, "RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue)

            ' ejecutar el comando/consulta SQL en el origen de datos
            resultado = Me._wrapper.EjecutarConsulta(comando)

            ' asignar el Id devuelto por la consulta al objeto
            If (resultado > 0) Then
                value.Id = CType(paramRet.Value, Integer)
            End If

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

    Public Function Baja(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoDAL.Baja
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_PERMISO_ELIMINAR", CommandType.StoredProcedure)
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

    Public Function Consulta(ByRef filtro As BE.PermisoDTO) As BE.PermisoDTO Implements IPermisoDAL.Consulta
        Dim lista As List(Of BE.PermisoDTO) = Me.ConsultaRango(filtro, Nothing)
        If Not lista Is Nothing AndAlso lista.Count > 0 Then
            ' retornar solo el primer objeto que cumpla con el filtro
            Return lista(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function ConsultaRango(ByRef filtroDesde As BE.PermisoDTO, ByRef filtroHasta As BE.PermisoDTO) As System.Collections.Generic.List(Of BE.PermisoDTO) Implements IPermisoDAL.ConsultaRango
        Dim lista As List(Of BE.PermisoDTO) = New List(Of BE.PermisoDTO)

        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_PERMISO_LISTAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            ' solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
            If Not filtroDesde Is Nothing AndAlso filtroDesde.Id > 0 Then
                Me.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id)
            Else
                Me.Wrapper.AgregarParametro(comando, "@id", DBNull.Value)
            End If
            ' solo buscar por Nombre, si se especifico filtrodesde y el Nombre en el filtroDesde no es vacio
            If Not filtroDesde Is Nothing AndAlso Not String.IsNullOrEmpty(filtroDesde.Nombre) Then
                Me.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre)
            Else
                Me.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value)
            End If
            ' solo buscar por Descripcion, si se especifico filtrodesde y la Descripcion en el filtroDesde no es vacio
            If Not filtroDesde Is Nothing AndAlso Not String.IsNullOrEmpty(filtroDesde.Descripcion) Then
                Me.Wrapper.AgregarParametro(comando, "@descripcion", filtroDesde.Descripcion)
            Else
                Me.Wrapper.AgregarParametro(comando, "@descripcion", DBNull.Value)
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

        ' este metodo retornará la lista con todas las BE convertidas que
        ' se obtuvieron del origen de datos
        Return lista
    End Function

    Public Function Modificacion(ByRef value As BE.PermisoDTO) As Boolean Implements IPermisoDAL.Modificacion
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_PERMISO_MODIFICAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Me.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre)
            Me.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)

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

    Public Property Conversor As IConversor(Of BE.PermisoDTO) Implements IPermisoDAL.Conversor
        Get
            If Me._conversor Is Nothing Then
                ' obtener el conversor por defecto para esta entidad
                Me._conversor = New PermisoConversor()
            End If
            Return Me._conversor
        End Get
        Set(ByVal value As IConversor(Of BE.PermisoDTO))
            Me._conversor = value
        End Set
    End Property

    Public Property Wrapper As IComando Implements IPermisoDAL.Wrapper
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

End Class

