<%@ Page Title="Página principal" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="HS.WebUI._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="home-banner">
<div class="overlay">
    <div class="banner-text">
        <h1>¡Bienvenido a <span>HARDWARE</span>SHOP!<h1>
        <p>Más de 1500 productos de informática de la más alta calidad</p>
    </div>
</div>

<p><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></p>
<p><asp:Label ID="lblMensajeEspecial" runat="server" Text=""></asp:Label></p>
<p><asp:Button ID="btnClientes" runat="server" Text="Acceso de Clientes" Visible="False" /></p>
<p><asp:Button ID="btnAdministracion" runat="server" Text="Usuarios y Accesos" Visible="False" />&nbsp;</p>
<p><asp:Button ID="btnBitacora" runat="server" Text="Bitacora" Visible="False" Width="174px" /></p>
<p><asp:Button ID="btnBackupYRestore" runat="server" Text="Backup y Restore" Visible="False" Width="174px" /></p>
<p><asp:Button ID="btnIntegridadBD" runat="server" Text="Verificar Integridad de Datos" Visible="False" Width="174px" /></p>
</div>   
</asp:Content>