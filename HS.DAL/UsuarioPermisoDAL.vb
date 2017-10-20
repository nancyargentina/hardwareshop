Imports System.Data
Imports HS.BE

Public Interface IUsuarioPermisoDAL
    Inherits IMapeador(Of PermisoDTO)

    ''' <summary>
    ''' Permiso padre de todos los permisos que se van a agregar, borrar o consultar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UsuarioActual As UsuarioDTO
End Interface

Public Class UsuarioPermisoDAL
    Implements IUsuarioPermisoDAL

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
    ''' <summary>
    ''' Usuario padre de todos los pemisos que se van a agregar, borrar o consultar
    ''' </summary>
    ''' <remarks></remarks>
    Private _usuario As UsuarioDTO = Nothing

    Public Property Conversor As IConversor(Of BE.PermisoDTO) Implements IUsuarioPermisoDAL.Conversor
        Get
            If Me._conversor Is Nothing Then Me._conversor = New PermisoConversor()
            Return Me._conversor
        End Get
        Set(ByVal value As IConversor(Of BE.PermisoDTO))
            Me._conversor = value
        End Set
    End Property

    Public Property Wrapper As IComando Implements IUsuarioPermisoDAL.Wrapper
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

    ''' <summary>
    ''' Permiso padre de todos los permisos que se van a agregar, borrar o consultar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UsuarioActual As UsuarioDTO Implements IUsuarioPermisoDAL.UsuarioActual
        Get
            If Me._usuario Is Nothing Then Throw New ArgumentNullException("No se especificó el usuario actual.")
            Return Me._usuario
        End Get
        Set(ByVal value As UsuarioDTO)
            Me._usuario = value
        End Set
    End Property

    Public Function Alta(ByRef value As BE.PermisoDTO) As Boolean Implements IUsuarioPermisoDAL.Alta
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_USUARIO_PERMISO_AGREGAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Me.Wrapper.AgregarParametro(comando, "@usuarioId", Me.UsuarioActual.Id)
            Me.Wrapper.AgregarParametro(comando, "@permisoId", value.Id)

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

    Public Function Baja(ByRef value As BE.PermisoDTO) As Boolean Implements IUsuarioPermisoDAL.Baja
        Dim resultado As Integer = 0
        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_USUARIO_PERMISO_ELIMINAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta
            Me.Wrapper.AgregarParametro(comando, "@usuarioId", Me.UsuarioActual.Id)
            Me.Wrapper.AgregarParametro(comando, "@permisoId", value.Id)

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

    Public Function Consulta(ByRef filtro As BE.PermisoDTO) As BE.PermisoDTO Implements IUsuarioPermisoDAL.Consulta
        Dim lista As List(Of PermisoDTO) = Me.ConsultaRango(filtro, Nothing)
        If lista.Count > 0 Then
            ' retornar solo el primer objeto que cumpla con el filtro
            Return lista(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function ConsultaRango(ByRef filtroDesde As BE.PermisoDTO, ByRef filtroHasta As BE.PermisoDTO) As System.Collections.Generic.List(Of BE.PermisoDTO) Implements IUsuarioPermisoDAL.ConsultaRango
        Dim lista As List(Of BE.PermisoDTO) = New List(Of BE.PermisoDTO)

        ' crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("HS_USUARIO_PERMISO_LISTAR", CommandType.StoredProcedure)
        Try
            ' agregar los parametros necesarios para poder ejecutar la consulta

            ' siempre buscar por el Id del padre
            Me.Wrapper.AgregarParametro(comando, "@usuarioId", Me.UsuarioActual.Id)
            ' solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
            If Not filtroDesde Is Nothing AndAlso filtroDesde.Id > 0 Then
                Me.Wrapper.AgregarParametro(comando, "@permisoId", filtroDesde.Id)
            Else
                Me.Wrapper.AgregarParametro(comando, "@permisoId", DBNull.Value)
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

    Public Function Modificacion(ByRef value As BE.PermisoDTO) As Boolean Implements IUsuarioPermisoDAL.Modificacion
        Throw New NotImplementedException("No se puede realizar una modificacion para el perfil.")
    End Function
End Class

