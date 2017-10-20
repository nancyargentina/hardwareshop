<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bitacora.aspx.vb" Inherits="HS.WebUI.Bitacora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <h2>Bitacora</h2>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ViewBitacora" runat="server">
                        <table>
                        <tr>
                            <td colspan="3"><h3>CONTENIDO DE LA BITACORA</h3></td>
                        </tr>                        
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grillaBitacora" runat="server" autogeneratecolumns="false">
                                    <columns>
                                      <asp:boundfield datafield="Fecha"
                                        readonly="true"      
                                        headertext="Fecha" />
                                      <asp:boundfield datafield="Autor"
                                        convertemptystringtonull="true"
                                        HeaderStyle-Width ="50px"
                                        headertext="Usuario"/>
                                      <asp:boundfield datafield="Descripcion"
                                        convertemptystringtonull="true"
                                        headertext="Descripcion"/>
                                      <asp:boundfield datafield="Criticidad"
                                        convertemptystringtonull="true"
                                        headertext="Criticidad"/>
                                    </columns>
                                </asp:GridView>
                            </td>
                        </tr>               
                    </table>
                    </asp:View>                    
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnVolver" runat="server" Text="Volver" BackColor="LightGray" />
            </td>
        </tr>
    </table>
</asp:Content>
