Imports HS.BE
Imports HS.DAL

Public Interface IBitacoraBLL
    Inherits ICRUD(Of BitacoraDTO)

End Interface


Public Class BitacoraBLL
    Implements IBitacoraBLL

    ''' <summary>
    ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
    ''' </summary>
    ''' <remarks></remarks>
    Private _dao As IBitacoraDAL = Nothing

    Public Sub New(ByVal pDAO As IBitacoraDAL)
        Me._dao = pDAO
    End Sub

    Public Sub New()
        Me._dao = New BitacoraDAL()
    End Sub

    Public Function Alta(ByRef value As BitacoraDTO) As Boolean Implements ICRUD(Of BitacoraDTO).Alta
        Try
            Return Me._dao.Alta(value)
        Catch ex As Exception
            Throw New Exception("No se puede agregar.", ex)
        End Try
    End Function

    Enum CRITICIDAD
        ALTA = 1
        MEDIA = 2
        BAJA = 3
    End Enum
    Enum TIPOLOG
        ALTAUSUARIO = 1
        BAJAUSUARIO = 2
        MODIFICACIONUSUARIO = 3
        ALTAFAMILIA = 4
        BAJAFAMILIA = 5
        MODIFICACIONFAMILIA = 6
        LOGINOK = 10
        LOGINFAIL = 11
        CUENTABLOQUEADA = 12
        CHECKINTEGRIDADOK = 20
        CHECKINTEGRIDADFAIL = 21

    End Enum


    Public Sub Loguear(TLog As Integer, Optional INFOEXTRA As String = "")
        Dim mBitacora As BitacoraDTO = New BitacoraDTO()
        mBitacora.Fecha = Date.Now()
        mBitacora.Eliminado = False

        Select Case TLog
            Case TIPOLOG.ALTAUSUARIO
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Creacion de usuario"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.MODIFICACIONUSUARIO
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Modificacion de usuario"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.BAJAUSUARIO
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Baja de usuario"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.ALTAFAMILIA
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Creacion de familia"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.MODIFICACIONFAMILIA
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Modificacion de familia"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.BAJAFAMILIA
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Baja de familia"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.CHECKINTEGRIDADFAIL
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "ERROR. Chequeo de integridad de BD FALLIDO"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.ALTA))
            Case TIPOLOG.CHECKINTEGRIDADOK
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Chequeo de integridad OK"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.BAJA))
            Case TIPOLOG.CUENTABLOQUEADA
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Cuenta de usuario bloqueada por intentos"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.MEDIA))
            Case TIPOLOG.LOGINOK
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Intento de Login con exito"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.BAJA))
            Case TIPOLOG.LOGINFAIL
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Intento de Login fallido"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.BAJA))
            Case Else
                mBitacora.Autor = INFOEXTRA
                mBitacora.Descripcion = "Evento no contemplado"
                mBitacora.Criticidad = CChar(CStr(CRITICIDAD.ALTA))
        End Select
        Try
            Alta(mBitacora)
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Function Baja(ByRef value As BitacoraDTO) As Boolean Implements ICRUD(Of BitacoraDTO).Baja
        Try
            Return Me._dao.Baja(value)
        Catch ex As Exception
            Throw New Exception("No se puede eliminar.", ex)
        End Try
    End Function
    Public Function Modificacion(ByRef value As BitacoraDTO) As Boolean Implements ICRUD(Of BitacoraDTO).Modificacion
        Try
            Return Me._dao.Modificacion(value)
        Catch ex As Exception
            Throw New Exception("No se puede modificar.", ex)
        End Try
    End Function

    Public Function Consulta(ByRef filtro As BitacoraDTO) As BitacoraDTO Implements ICRUD(Of BitacoraDTO).Consulta
        Return Nothing
    End Function

    Public Function ConsultaRango(ByRef filtroDesde As BitacoraDTO, ByRef filtroHasta As BitacoraDTO) As List(Of BitacoraDTO) Implements ICRUD(Of BitacoraDTO).ConsultaRango
        Return Nothing
    End Function
End Class

