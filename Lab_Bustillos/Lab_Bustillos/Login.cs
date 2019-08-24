using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class Login : Form
    {
        //Se instancia la clase conexion para poder acceder a la base de datos
        Conexion cn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        public Login()
        {
            InitializeComponent();
        }
        //El metodo _enter sirve para poner el placeholder del campo de texto al igual que en _leave
        private void txtUsu_Enter(object sender, EventArgs e)
        {
            if (txtUsu.Text == "USUARIO")
            {
                txtUsu.Text = "";
                txtUsu.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtUsu_Leave(object sender, EventArgs e)
        {
            if (txtUsu.Text=="")
            {
                txtUsu.Text = "USUARIO";
                txtUsu.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        private void txtContra_Enter(object sender, EventArgs e)
        {
            if (txtContra.Text == "CONTRASEÑA")
            {
                txtContra.Text = "";
                txtContra.ForeColor = System.Drawing.Color.White;
                txtContra.UseSystemPasswordChar = true;
            }
        }

        private void txtContra_Leave(object sender, EventArgs e)
        {
            if (txtContra.Text == "")
            {
                txtContra.Text = "CONTRASEÑA";
                txtContra.ForeColor = System.Drawing.Color.DimGray;
                txtContra.UseSystemPasswordChar = false;
            }
        }

        //El boton de acceder verifica en la base de datos que el usuario y la contraseña
        //ingresados sean los correctos.
        private void btnAcceder_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Primero abre la conexion para acceder al servidor
                comando.Connection = cn.AbrirConexion();
                //selecciona los datos pedidos de la base de datos.
                comando.CommandText = "select Personal.Nombre,Personal.Rol, Clicks.Clicks from Personal inner join Clicks on Personal.Nombre=Clicks.Nombre where Personal.Usuario='"+txtUsu.Text+"' and Personal.Contraseña='"+txtContra.Text+"'";
                leer = comando.ExecuteReader();
                int ed = 0;
                string rol = "";
                string clicks="";
                //si la sentencia cumple con la codicion se ejecuta un while donde se saca el rol del usuario
                while (leer.Read())
                {
                    ed++;
                    rol = leer.GetString(1);
                    Personal.nombreP= leer.GetString(0);
                    Personal.click= leer.GetInt32(2);
                }
                Principal p = new Principal();
                if (ed == 1)
                {
                    //si es administrador tiene acceso a todas las funcionalidades del programa
                    if (rol.Equals("Administrador"))
                    {
                        MessageBox.Show("Acceso Concedido");
                        this.Visible = false;
                        p.Show();
                    }
                    else
                    {
                        //el empleado solo tiene acceso a la ventana de ventas.
                        MessageBox.Show("Bienvenido");
                        new Ventas().Show();
                        this.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Acceso Denegado");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex);
                throw;
            }
            comando.Connection = cn.CerrarConexion();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
