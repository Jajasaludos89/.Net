<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Desarrolladores.aspx.cs" Inherits="Software.Desarrolladores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center mt-4">Gestión de Desarrolladores</h2>

    <div class="container mt-5">
        <div class="row">
            <div class="col-md-4">
                <asp:HiddenField ID="hdfIdDesarrollador" runat="server" />

                <label>Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />

                <label class="mt-3">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />

                <label class="mt-3">Nacionalidad:</label>
                <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control" />

                <label class="mt-3">Especialidad:</label>
                <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />

                <label class="mt-3">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />

                <div class="mt-4 d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>
            </div>

            <div class="col-md-8">
                <asp:GridView ID="gvDesarrolladores" runat="server" CssClass="table table-bordered mt-3"
                    AutoGenerateColumns="False" OnRowCommand="gvDesarrolladores_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdDesarrollador" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar"
                                    CommandName="Editar"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CssClass="btn btn-sm btn-primary me-2" />

                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("IdDesarrollador") %>'
                                    CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('¿Estás seguro de eliminar este desarrollador?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
