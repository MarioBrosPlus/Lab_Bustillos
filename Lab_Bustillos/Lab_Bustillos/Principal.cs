using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;

namespace Lab_Bustillos
{
    public partial class Principal : Form
    {
        private Timer ti;
        Conexion cn = new Conexion();
        Conexion2 cn2 = new Conexion2();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        public static string nombreP = "";
        public static int click = 0;
        public Principal()
        {
            ti = new Timer();
            ti.Tick += new EventHandler(Hora);
            InitializeComponent();
            ti.Enabled = true;
        }
        private void Hora(object ob, EventArgs evt)
        {
            DateTime hot = DateTime.Now;
            lblHora.Text = hot.ToString("hh:mm:ss tt");
            lblFecha.Text= hot.ToString("dd/MM/yyyy");
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<InventarioCR>();
        }
        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        private void AbrirFormEnPanel<Forms>() where Forms : Form, new()
        {
            Form formulario;
            formulario = panelContenedor.Controls.OfType<Forms>().FirstOrDefault();

            //si el formulario/instancia no existe, creamos nueva instancia y mostramos
            if (formulario == null)
            {
                formulario = new Forms();
                formulario.TopLevel = false;
                //formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                //formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            else
            {
                //si la Formulario/instancia existe, lo traemos a frente
                formulario.BringToFront();
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<VentasCR>();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Productos>();
        }

        private void personalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Personal>();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Show();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void personalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<UsuariosR>();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<ProductosR>();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<ProductosP>();
        }

        private void personalToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<PersonalR>();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<VentasR>();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Acerca_de>();
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string pdfPath = Path.Combine(Application.StartupPath, "ManualUsuario.pdf");
            Process.Start(pdfPath);
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string pdfPath = Path.Combine(Application.StartupPath, "Manual.pdf");
            Process.Start(pdfPath);
        }

        private void baseDeDatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Restaurar();
        }

        public void Restaurar()
        {
            try
            {
                comando = new SqlCommand("truncate table Ventas", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Ventas", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Ventas values(@u, @d, @t, @c, @ci, @s)", cn.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetString(1));
                    comando.Parameters.AddWithValue("@t", leer.GetInt32(2));
                    comando.Parameters.AddWithValue("@c", leer.GetInt32(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetInt32(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Productos", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Productos", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Productos values(@u, @d, @t, @c)", cn.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetString(1));
                    comando.Parameters.AddWithValue("@t", leer.GetInt32(2));
                    comando.Parameters.AddWithValue("@c", leer.GetInt32(3));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Personal", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Personal", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Personal values(@u, @d, @t, @c, @ci, @s, @si)", cn.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.Parameters.AddWithValue("@t", leer.GetString(2));
                    comando.Parameters.AddWithValue("@c", leer.GetString(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetString(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.Parameters.AddWithValue("@si", leer.GetString(6));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table RespaldoU", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from RespaldoU", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into RespaldoU values(@u, @d, @t, @c, @ci, @s, @si, @o)", cn.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.Parameters.AddWithValue("@t", leer.GetString(2));
                    comando.Parameters.AddWithValue("@c", leer.GetString(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetString(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.Parameters.AddWithValue("@si", leer.GetString(6));
                    comando.Parameters.AddWithValue("@o", leer.GetString(7));
                    comando.ExecuteNonQuery();
                }
                leer.Close();                
                comando = new SqlCommand("truncate table Clicks", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Clicks", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Clicks values(@u, @d)", cn.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                MessageBox.Show("La Restauración de los Datos se Ejecuto Correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Respaldo()
        {
            try
            {
                comando = new SqlCommand("truncate table Ventas", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Ventas", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Ventas values(@u, @d, @t, @c, @ci, @s)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetString(1));
                    comando.Parameters.AddWithValue("@t", leer.GetInt32(2));
                    comando.Parameters.AddWithValue("@c", leer.GetInt32(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetInt32(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Productos", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Productos", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Productos values(@u, @d, @t, @c)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetString(1));
                    comando.Parameters.AddWithValue("@t", leer.GetInt32(2));
                    comando.Parameters.AddWithValue("@c", leer.GetInt32(3));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Personal", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Personal", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Personal values(@u, @d, @t, @c, @ci, @s, @si)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.Parameters.AddWithValue("@t", leer.GetString(2));
                    comando.Parameters.AddWithValue("@c", leer.GetString(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetString(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.Parameters.AddWithValue("@si", leer.GetString(6));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table RespaldoU", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from RespaldoU", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into RespaldoU values(@u, @d, @t, @c, @ci, @s, @si, @o)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.Parameters.AddWithValue("@t", leer.GetString(2));
                    comando.Parameters.AddWithValue("@c", leer.GetString(3));
                    comando.Parameters.AddWithValue("@ci", leer.GetString(4));
                    comando.Parameters.AddWithValue("@s", leer.GetString(5));
                    comando.Parameters.AddWithValue("@si", leer.GetString(6));
                    comando.Parameters.AddWithValue("@o", leer.GetString(7));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Nomina", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Nomina", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Nomina values(@u, @d)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetString(1));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                comando = new SqlCommand("truncate table Clicks", cn2.AbrirConexion());
                comando.ExecuteNonQuery();
                comando = new SqlCommand("select * from Clicks", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    comando = new SqlCommand("insert into Clicks values(@u, @d)", cn2.AbrirConexion());
                    comando.Parameters.AddWithValue("@u", leer.GetString(0));
                    comando.Parameters.AddWithValue("@d", leer.GetInt32(1));
                    comando.ExecuteNonQuery();
                }
                leer.Close();
                MessageBox.Show("El Respaldo se Ejecuto Correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void baseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Respaldo();
        }
    }
}
