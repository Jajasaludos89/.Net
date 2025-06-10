using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Software
{
    public partial class Proyectos : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarProyectos();
                CargarDesarrolladores();
                btnActualizar.Visible = true;
            }
        }

        private void MostrarProyectos()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ConsultarProyectos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvProyectos.DataSource = dt;
                gvProyectos.DataBind();
            }
        }

        private void CargarDesarrolladores()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT IdDesarrollador, Nombre + ' ' + Apellido AS NombreCompleto FROM Desarrolladores", conexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlResponsable.DataSource = dt;
                ddlResponsable.DataTextField = "NombreCompleto";
                ddlResponsable.DataValueField = "IdDesarrollador";
                ddlResponsable.DataBind();

                ddlResponsable.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("InsertarProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreProyecto", txtNombreProyecto.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@FechaInicio", txtFechaInicio.Text);
                cmd.Parameters.AddWithValue("@FechaFin", txtFechaFin.Text);
                cmd.Parameters.AddWithValue("@IdResponsable", ddlResponsable.SelectedValue);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                LimpiarCampos();
                MostrarProyectos();
            }
        }

        protected void gvProyectos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProyectos.Rows[index];

                hdfIdProyecto.Value = row.Cells[0].Text;
                txtNombreProyecto.Text = row.Cells[1].Text;
                txtDescripcion.Text = row.Cells[2].Text;
                txtFechaInicio.Text = Convert.ToDateTime(row.Cells[3].Text).ToString("yyyy-MM-dd");
                txtFechaFin.Text = Convert.ToDateTime(row.Cells[4].Text).ToString("yyyy-MM-dd");

                string nombreResponsable = row.Cells[5].Text;
                ddlResponsable.ClearSelection();
                ListItem item = ddlResponsable.Items.FindByText(nombreResponsable);
                if (item != null)
                {
                    item.Selected = true;
                }

                btnGuardar.Visible = true;
                btnActualizar.Visible = true;
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarProyecto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProyecto", id);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    MostrarProyectos();
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ActualizarProyecto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdProyecto", Convert.ToInt32(hdfIdProyecto.Value));
                cmd.Parameters.AddWithValue("@NombreProyecto", txtNombreProyecto.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@FechaInicio", txtFechaInicio.Text);
                cmd.Parameters.AddWithValue("@FechaFin", txtFechaFin.Text);
                cmd.Parameters.AddWithValue("@IdResponsable", ddlResponsable.SelectedValue);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                LimpiarCampos();
                MostrarProyectos();
                btnGuardar.Visible = true;
                btnActualizar.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            btnGuardar.Visible = true;
            btnActualizar.Visible = true;
        }


        private void LimpiarCampos()
        {
            txtNombreProyecto.Text = "";
            txtDescripcion.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
            ddlResponsable.SelectedIndex = 0;
            hdfIdProyecto.Value = "";
        }
    }
}
