<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="HS.WebUI.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <section>
    <asp:Panel ID="divIniciarSesion" runat="server" CssClass="loginForm">
        
        <h2>Iniciar sesión</h2>

        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>

        <div class="formControl">
            <label for="txtUsuario">Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="16"></asp:TextBox>
        </div>

        <div class="formControl">
            <label for="txtUsuario">Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server" MaxLength="16" TextMode="Password"></asp:TextBox>
        </div>

        <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesion" BackColor="LightGray"  />
        
    </asp:Panel>
    
        
    <asp:Panel ID="divCerrarSesion" runat="server" CssClass="loguedUserBox">
        <div>
            <label>Usuario actual: <asp:Label ID="lblUsuarioActual" runat="server" Text=""></asp:Label></label>
        </div>
        <div><asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesion" BackColor="LightGray"  /></div>
    </asp:Panel>

    </section>
</asp:Content>