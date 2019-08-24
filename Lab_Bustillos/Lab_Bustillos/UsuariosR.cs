using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class UsuariosR : Form
    {
        Conexion cn = new Conexion();
        Conexion2 cn2 = new Conexion2();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        public static string nombreP = "";
        public static int click = 0;
        public UsuariosR()
        {
            InitializeComponent();
            Usuarios(dataGridViewRespaldo);
            Restaurar(dataGridViewRestauro);
        }
        private void btnRespaldar_Click(object sender, EventArgs e)
        {
            if (dataGridViewRespaldo.Rows.Count == 0 && txtBuscarRespaldo.Text.Length == 0)
            {
                MessageBox.Show("La base de datos no tiene datos para respaldar");
            }
            else if (dataGridViewRespaldo.Rows.Count == 0 && txtBuscarRespaldo.Text.Length != 0)
            {
                MessageBox.Show("No se encontraron datos con esa referencia");
            }
            else if (dataGridViewRespaldo.Rows.Count != 0)
            {
                foreach (DataGridViewRow row in dataGridViewRespaldo.Rows)
                {
                    try
                    {
                        DateTime date = DateTime.Now;
                        comando = new SqlCommand("truncate table Personal insert into Personal values(@Cod,@Tit,@Cat,@Pre,@cant,@fec,@alf)", cn2.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.Parameters.AddWithValue("@cant", row.Cells[4].Value.ToString());
                        comando.Parameters.AddWithValue("@fec", row.Cells[5].Value.ToString());
                        comando.Parameters.AddWithValue("@alf", row.Cells[6].Value.ToString());
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
                MessageBox.Show("Respaldo Guardado Correctamente");
                Restaurar(dataGridViewRestauro);
            }
        }

        

       

        private void txtBuscarRespaldo_TextChanged(object sender, EventArgs e)
        {
            dataGridViewRespaldo.Rows.Clear();
            try
            {
                string nombre = "";
                int edad = 0;
                string direccion = "";
                string telefono = "";
                string usuario = "";
                string contraseña = "";
                string rol = "";
                comando = new SqlCommand("select * from Personal where Nombre like '"+txtBuscarRespaldo.Text+"%';", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    nombre = leer.GetString(0);
                    edad = leer.GetInt32(1);
                    direccion = leer.GetString(2);
                    telefono = leer.GetString(3);
                    usuario = leer.GetString(4);
                    contraseña = leer.GetString(5);
                    rol = leer.GetString(6);
                    dataGridViewRespaldo.Rows.Add(nombre, edad, direccion, telefono, usuario, contraseña, rol);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }

        
        public void Usuarios(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string nombre = "";
                int edad = 0;
                string direccion = "";
                string telefono = "";
                string usuario = "";
                string contraseña = "";
                string rol = "";
                comando = new SqlCommand("select * from Personal;", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    nombre = leer.GetString(0);
                    edad = leer.GetInt32(1);
                    direccion = leer.GetString(2);
                    telefono = leer.GetString(3);
                    usuario = leer.GetString(4);
                    contraseña = leer.GetString(5);
                    rol = leer.GetString(6);
                    dgv.Rows.Add(nombre, edad, direccion, telefono, usuario, contraseña, rol);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }
        public void Restaurar(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string nombre = "";
                int edad = 0;
                string direccion = "";
                string telefono = "";
                string usuario = "";
                string contraseña = "";
                string rol = "";
                comando = new SqlCommand("select * from Personal;", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    nombre = leer.GetString(0);
                    edad = leer.GetInt32(1);
                    direccion = leer.GetString(2);
                    telefono = leer.GetString(3);
                    usuario = leer.GetString(4);
                    contraseña = leer.GetString(5);
                    rol = leer.GetString(6);
                    dgv.Rows.Add(nombre, edad, direccion, telefono, usuario, contraseña, rol);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dataGridViewRestauro.Rows.Count == 0)
            {
                MessageBox.Show("La base de datos no tiene datos para restaurar");
            }
            else if (dataGridViewRestauro.Rows.Count != 0)
            {
                foreach (DataGridViewRow row in dataGridViewRestauro.Rows)
                {
                    try
                    {
                        DateTime date = DateTime.Now;
                        comando = new SqlCommand("truncate table Personal insert into Personal values(@Cod,@Tit,@Cat,@Pre,@cant,@fec,@alf)", cn.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.Parameters.AddWithValue("@cant", row.Cells[4].Value.ToString());
                        comando.Parameters.AddWithValue("@fec", row.Cells[5].Value.ToString());
                        comando.Parameters.AddWithValue("@alf", row.Cells[6].Value.ToString());
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
                MessageBox.Show("Restauración exitosa");
            }
        }
    }
}
