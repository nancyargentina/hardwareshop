<%@ Page Title="Página principal" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="HS.WebUI._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="home-banner">
        <div class="overlay">
            <div class="banner-text">
                <asp:Label ID="welcomeLabel" runat="server" Text=""></asp:Label>
                <p>Más de 1500 productos de informática de la más alta calidad</p>
            </div>
        </div>
    </div>
    <section>
        <h2>Pagina principal</h2>
        <p><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></p>
        <p><asp:Button ID="btnCarrito" runat="server" Text="Ir a Carrito" Width ="250px" BackColor="LightGray" Visible="False" /></p>
        <p><asp:Button ID="btnAdministracionProductos" runat="server" Text="Administración de productos" Width ="250px" BackColor="LightGray"  Visible="False" />&nbsp;</p>
        <p><asp:Button ID="btnBitacora" runat="server" Text="Bitacora" Width ="250px" BackColor="LightGray" Visible="False" /></p>
        <p><asp:Button ID="btnBackupYRestore" runat="server" Text="Backup y Restore" BackColor="LightGray" Visible="False"  Width ="250px"/></p>
        <p><asp:Button ID="btnIntegridadBD" runat="server" Text="Verificar Integridad de Datos" BackColor="LightGray" Visible="False"  Width ="250px" /></p>
        <p><asp:Button ID="btnCambioDePrecios" runat="server" Text="Actualización de precios" BackColor="LightGray" Visible="False"  Width ="250px" /></p>
    </section>
</asp:Content>