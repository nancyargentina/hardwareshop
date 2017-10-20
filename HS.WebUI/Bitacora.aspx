<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bitacora.aspx.vb" Inherits="HS.WebUI.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 21px;
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
                <td class="auto-style2">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewBitacora" runat="server">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <h3>CONTENIDO DE LA BITACORA</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="grillaBitacora" runat="server" AutoGenerateColumns="False" DataKeyNames="IDBITACORA" DataSourceID="HS">
                                            <Columns>
                                                <asp:BoundField DataField="IDBITACORA"
                                                    ReadOnly="true"
                                                    HeaderText="IDBITACORA" InsertVisible="False" SortExpression="IDBITACORA" />
                                                <asp:BoundField DataField="DIGITOHORIZONTAL"
                                                    ConvertEmptyStringToNull="true"
                                                    HeaderText="DIGITOHORIZONTAL" SortExpression="DIGITOHORIZONTAL" />
                                                <asp:BoundField DataField="LOG"
                                                    ConvertEmptyStringToNull="true"
                                                    HeaderText="LOG" SortExpression="LOG" />
                                                <asp:CheckBoxField DataField="ELIMINADO" HeaderText="ELIMINADO" SortExpression="ELIMINADO" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="HS" runat="server" ConnectionString="<%$ ConnectionStrings:HSConnectionString %>" SelectCommand="SELECT * FROM [BITACORA]"></asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
</asp:Content>
