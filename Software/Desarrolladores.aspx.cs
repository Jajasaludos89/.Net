using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Software
{
    public partial class Desarrolladores : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionSistema"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarDesarrolladores();
                btnActualizar.Visible = true;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblMensaje.Text = "El apellido es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNacionalidad.Text))
            {
                lblMensaje.Text = "La nacionalidad es obligatoria.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                lblMensaje.Text = "La especialidad es obligatoria.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                lblMensaje.Text = "Debe ingresar un correo electrónico válido.";
                return false;
            }

            lblMensaje.Text = ""; // Limpiar mensaje si todo está bien
            return true;
        }


        private void MostrarDesarrolladores()
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ConsultarDesarrolladores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDesarrolladores.DataSource = dt;
                gvDesarrolladores.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("InsertarDesarrollador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                cmd.Parameters.AddWithValue("@Especialidad", txtEspecialidad.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                LimpiarCampos();
                MostrarDesarrolladores();
                lblMensaje.Text = "Desarrollador guardado correctamente.";
            }
        }


        protected void gvDesarrolladores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvDesarrolladores.Rows[index];

                hdfIdDesarrollador.Value = row.Cells[0].Text;
                txtNombre.Text = row.Cells[1].Text;
                txtApellido.Text = row.Cells[2].Text;
                txtNacionalidad.Text = row.Cells[3].Text;
                txtEspecialidad.Text = row.Cells[4].Text;
                txtEmail.Text = row.Cells[5].Text;

                btnGuardar.Visible = true;
                btnActualizar.Visible = true;
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("EliminarDesarrollador", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDesarrollador", id);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    MostrarDesarrolladores();
                }
            }
        }

        

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            btnGuardar.Visible = true;
            btnActualizar.Visible = true;
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("ActualizarDesarrollador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdDesarrollador", Convert.ToInt32(hdfIdDesarrollador.Value));
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                cmd.Parameters.AddWithValue("@Especialidad", txtEspecialidad.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                LimpiarCampos();
                MostrarDesarrolladores();
                lblMensaje.Text = "Desarrollador actualizado correctamente.";
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNacionalidad.Text = "";
            txtEspecialidad.Text = "";
            txtEmail.Text = "";
            hdfIdDesarrollador.Value = "";
        }
    }
}
