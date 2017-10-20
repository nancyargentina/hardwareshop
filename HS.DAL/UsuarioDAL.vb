Imports System.Data
Imports HS.DAL
Imports HS.BE

Public Interface IUsuarioDAL
    Inherits IMapeador(Of UsuarioDTO) ', IVerificator(Of UsuarioDTO)
    Function BloquearCuenta(ByRef value As BE.UsuarioDTO) As Boolean
End Interface

Public Class UsuarioDAL
    Implements IUsuarioDAL
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
    Private _conversor As IConversor(Of BE.UsuarioDTO) = Nothing

    Public Function Alta(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioDAL.Alta
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("INSERT INTO USUARIO VALUES(@nombre, @clave, @email, @bloqueado, 0,0) SET @identity=@@Identity", CommandType.Text)
        Try

            Me.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre)
            Me.Wrapper.AgregarParametro(comando, "@clave", value.Clave)
            Me.Wrapper.AgregarParametro(comando, "@email", value.Email)
            Me.Wrapper.AgregarParametro(comando, "@bloqueado", value.Bloqueado)

            Dim paramRet As IDataParameter = Me.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output)

            resultado = Me._wrapper.EjecutarConsulta(comando)

        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        Return (resultado > 0)
    End Function

    Public Function Baja(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioDAL.Baja
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE USUARIO SET Usu_Eliminado=@eliminado WHERE Usu_Id=@id", CommandType.Text)
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

    Public Function Consulta(ByRef filtro As BE.UsuarioDTO) As BE.UsuarioDTO Implements IUsuarioDAL.Consulta
        Dim lista As List(Of BE.UsuarioDTO) = Me.ConsultaRango(filtro, Nothing)
        If Not lista Is Nothing AndAlso lista.Count > 0 Then
            ' retornar solo el primer objeto que cumpla con el filtro
            Return lista(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function BloquearCuenta(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioDAL.BloquearCuenta
        value.Bloqueado = True
        Dim resultado As Integer = 0

        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE USUARIO SET Usu_Bloqueado=@bloqueado WHERE Usu_Id=@id", CommandType.Text)
        Try
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)
            Me.Wrapper.AgregarParametro(comando, "@bloqueado", value.Eliminado)

            resultado = Me._wrapper.EjecutarConsulta(comando)
        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        Return (resultado > 0)
    End Function

    Public Function ConsultaRango(ByRef filtroDesde As BE.UsuarioDTO, ByRef filtroHasta As BE.UsuarioDTO) As System.Collections.Generic.List(Of BE.UsuarioDTO) Implements IUsuarioDAL.ConsultaRango
        Dim lista As List(Of BE.UsuarioDTO) = New List(Of BE.UsuarioDTO)

        Dim comando As IDbCommand = Me.Wrapper.CrearComando("SELECT * FROM USUARIO WHERE (Usu_nombre=@nombre OR @nombre IS NULL) AND (Usu_Id=@id OR @id IS NULL) ORDER BY Usu_Nombre", CommandType.Text)
        Try
            If Not filtroDesde Is Nothing AndAlso filtroDesde.Id > 0 Then
                Me.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id)
            Else
                Me.Wrapper.AgregarParametro(comando, "@id", DBNull.Value)
            End If
            If Not filtroDesde Is Nothing AndAlso Not String.IsNullOrEmpty(filtroDesde.Nombre) Then
                Me.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre)
            Else
                Me.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value)
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

    Public Function Modificacion(ByRef value As BE.UsuarioDTO) As Boolean Implements IUsuarioDAL.Modificacion
        Dim resultado As Integer = 0
        Dim comando As IDbCommand = Me.Wrapper.CrearComando("UPDATE USUARIO SET Usu_Nombre=@nombre, DIGITOHORIZONTAL=@digitohorizontal, Usu_Clave=@clave, Usu_Email=@email, Usu_Bloqueado=@bloqueado, Usu_Eliminado=@eliminado WHERE Usu_Id=@id", CommandType.Text)
        Try
            Me.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre)
            Me.Wrapper.AgregarParametro(comando, "@clave", value.Clave)
            Me.Wrapper.AgregarParametro(comando, "@email", value.Email)
            Me.Wrapper.AgregarParametro(comando, "@bloqueado", value.Bloqueado)
            Me.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado)
            Me.Wrapper.AgregarParametro(comando, "@id", value.Id)
        Catch
            Throw
        Finally
            Me.Wrapper.CerrarConexion(comando)
        End Try
        Return (resultado > 0)
    End Function

    Public Property Conversor As IConversor(Of BE.UsuarioDTO) Implements IUsuarioDAL.Conversor
        Get
            If Me._conversor Is Nothing Then
                Me._conversor = New UsuarioConversor()
            End If
            Return Me._conversor
        End Get
        Set(ByVal value As IConversor(Of BE.UsuarioDTO))
            Me._conversor = value
        End Set
    End Property

    Public Property Wrapper As IComando Implements IUsuarioDAL.Wrapper
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