<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proyectos.aspx.cs" Inherits="Software.Proyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center mt-4">Gestión de Proyectos</h2>

    <div class="container mt-5">
        <div class="row">
            
            <div class="col-md-4">
                <asp:HiddenField ID="hdfIdProyecto" runat="server" />

                <label>Nombre del Proyecto:</label>
                <asp:TextBox placeholder="Ingrese el nombre del proyecto" ID="txtNombreProyecto" runat="server" CssClass="form-control" />

                <label class="mt-3">Descripción:</label>
                <asp:TextBox placeholder="Ingrese la descripcion del proyecto" ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />

                <label class="mt-3">Fecha de Inicio:</label>
                <asp:TextBox placeholder="Ingrese la Fecha de Inicio" ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />

                <label class="mt-3">Fecha de Fin:</label>
                <asp:TextBox placeholder="Ingrese la Fecha de Finalizacion" ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />

                <label class="mt-3">Responsable:</label>
                <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="form-control" />

                <div class="mt-4 d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-warning" OnClick="btnActualizar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>

            </div>

            <div class="col-md-8">
                <asp:GridView ID="gvProyectos" runat="server" CssClass="table table-bordered mt-3"
                    AutoGenerateColumns="False" OnRowCommand="gvProyectos_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdProyecto" HeaderText="ID" />
                        <asp:BoundField DataField="NombreProyecto" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="FechaFin" HeaderText="Fin" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar"
                                    CommandName="Editar"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    CssClass="btn btn-sm btn-primary me-2" />

                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("IdProyecto") %>'
                                    CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('¿Estás seguro de eliminar este proyecto?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
