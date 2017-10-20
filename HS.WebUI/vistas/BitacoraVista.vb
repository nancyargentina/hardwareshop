Imports HS.BE
Imports HS.BLL

Public Class BitacoraVista

    Private _usuario As BE.UsuarioDTO
    Private _bitacoraDTO As BitacoraDTO
    Public Property Bitacora() As BitacoraDTO
        Get
            If _usuario Is Nothing Then _bitacoraDTO = New BitacoraDTO()
            Return _bitacoraDTO
        End Get
        Set(ByVal value As BitacoraDTO)
            _bitacoraDTO = value
        End Set
    End Property

    Private _BitacoraBLL As IBitacoraBLL
    Public Property BitacoraBLL() As IBitacoraBLL
        Get
            If _BitacoraBLL Is Nothing Then _BitacoraBLL = New BitacoraBLL()
            Return _BitacoraBLL
        End Get
        Set(ByVal value As IBitacoraBLL)
            _BitacoraBLL = value
        End Set
    End Property

    Public Function ObtenerPorId(ByVal id As Integer) As BitacoraDTO
        'modificar
        Dim filtro As BitacoraDTO = New BitacoraDTO
        filtro.Id = id
        Me.Bitacora = Me.BitacoraBLL.Consulta(filtro)
        Return Me.Bitacora
    End Function

    Public Function ObtenerPorId(ByVal id As String) As BitacoraDTO

        If IsNumeric(id) Then
            Return Me.ObtenerPorId(Convert.ToInt32(id))
        Else
            Me.Bitacora = Nothing
        End If
        Return Me.Bitacora
    End Function

    Public Function EliminarPorId(ByVal id As String) As Boolean
        Dim filtro As BitacoraDTO = New BitacoraDTO
        filtro.Id = Convert.ToInt32(id)
        Dim _bitacora As BitacoraDTO = Me.BitacoraBLL.Consulta(filtro)
        If Not _bitacora Is Nothing Then
            _bitacora.Eliminado = True
            Return Me.BitacoraBLL.Baja(_bitacora)
        Else
            Return False
        End If
    End Function

    Public Function RestaurarPorId(ByVal id As String) As Boolean
        Dim filtro As BitacoraDTO = New BitacoraDTO
        filtro.Id = Convert.ToInt32(id)
        Dim _bitacora As BitacoraDTO = Me.BitacoraBLL.Consulta(filtro)
        If Not _bitacora Is Nothing Then
            _bitacora.Eliminado = False
            Return Me.BitacoraBLL.Baja(_bitacora)
        Else
            Return False
        End If
    End Function

    Dim listaBitacoraDTo As List(Of BitacoraDTO) = New List(Of BitacoraDTO)
    Public Sub LlenarGrilla(ByRef dataGrid As System.Web.UI.WebControls.GridView)
        'traer todos los logs de bitacoras para enlazarlo al DataSource del control


        Dim Lista As List(Of BitacoraDTO) = Me.BitacoraBLL.ConsultaRango(Nothing, Nothing)
        Dim listaBitacoraDTo = New List(Of BitacoraDTO)
        For Each obj As BitacoraDTO In Lista
            If Not obj.Eliminado Then
                listaBitacoraDTo.Add(obj)
            End If
        Next
        listaBitacoraDTo.Sort(Function(x, y) y.Fecha.CompareTo(x.Fecha))
        dataGrid.DataSource = listaBitacoraDTo
        dataGrid.DataBind()
    End Sub

    Sub FiltrarGrilla(ByRef dataGrid As System.Web.UI.WebControls.GridView, txtFechaIni As String, txtFechaFin As String, txtDescripcion As String, txtAutor As String, cboxCriticidad As String)
        Dim Lista As List(Of BitacoraDTO) = Me.BitacoraBLL.ConsultaRango(Nothing, Nothing)
        Dim listaBitacoraDTo = New List(Of BitacoraDTO)
        For Each objDto As BitacoraDTO In Lista
            If objDto.Eliminado Then
                Continue For
            End If
            If (Not String.IsNullOrWhiteSpace(txtFechaIni)) Then
                If Date.Compare(CDate(txtFechaIni), objDto.Fecha) > 0 Then
                    Continue For
                End If
            End If
            If (Not String.IsNullOrWhiteSpace(txtFechaFin)) Then
                If Date.Compare(CDate(txtFechaFin), objDto.Fecha) < 0 Then
                    Continue For
                End If
            End If
            If (Not String.IsNullOrWhiteSpace(txtDescripcion)) Then
                If Not objDto.Descripcion.Contains(txtDescripcion) Then
                    Continue For
                End If
            End If

            If (Not String.IsNullOrWhiteSpace(txtAutor)) Then
                If Not objDto.Autor.Equals(txtAutor) Then
                    Continue For
                End If
            End If
            If (Not String.IsNullOrWhiteSpace(cboxCriticidad)) Then
                If Not objDto.Criticidad.Equals(cboxCriticidad) Then
                    Continue For
                End If
            End If
            listaBitacoraDTo.Add(objDto)
        Next
        dataGrid.DataSource = listaBitacoraDTo
        dataGrid.DataBind()
    End Sub

    'Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl)
    '    Dim lista As List(Of Usuario) = Me.UsuarioDinamico.ConsultaRango(Nothing, Nothing)
    '    Me.LlenarLista(listBox, lista)
    'End Sub

    'Public Sub LlenarLista(ByRef listBox As System.Web.UI.WebControls.ListControl, ByVal lista As List(Of Usuario))
    '    listBox.Items.Clear()
    '    listBox.DataTextField = "Nombre"
    '    listBox.DataValueField = "Id"
    '    'traer todos los permisos y enlazarlo al DataSource del control
    '    listBox.DataSource = lista
    '    listBox.DataBind()
    'End Sub

    Public Function ObtenerIdEnLista(ByVal listBox As System.Web.UI.WebControls.ListControl) As Integer
        If Not listBox Is Nothing AndAlso Not listBox.SelectedItem Is Nothing Then
            Return Convert.ToInt32(listBox.SelectedItem.Value)
        Else
            Return 0
        End If
    End Function



End Class