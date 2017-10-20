<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="HS.WebUI.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Iniciar sesión
    </h2>
    <asp:Panel ID="divIniciarSesion" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Usuario:
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Contraseña:
                </td>
                <td>
                    <asp:TextBox ID="txtClave" runat="server" MaxLength="16" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesion" BackColor="LightGray"  />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="divCerrarSesion" runat="server">
    <table>
        <tr>
        <td>Usuario actual:</td>
        <td><asp:Label ID="lblUsuarioActual" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button
            ID="btnCerrarSesion" runat="server" Text="Cerrar Sesion" BackColor="LightGray"  /></td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>

