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
    public partial class ProductosR : Form
    {
        Conexion cn = new Conexion();
        Conexion2 cn2 = new Conexion2();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        public ProductosR()
        {
            InitializeComponent();
            ProductosC(dataGridViewRespaldo);
            ProductosRes(dataGridViewRestauro);
        }
        public void ProductosC(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                comando = new SqlCommand("select * from Productos", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dgv.Rows.Add(codigo, descrip, precio, exist);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }
        public void ProductosRes(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                comando = new SqlCommand("select * from Productos", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dgv.Rows.Add(codigo, descrip, precio, exist);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }

        private void txtBuscarRespaldo_TextChanged(object sender, EventArgs e)
        {
            dataGridViewRespaldo.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                comando = new SqlCommand("select * from Productos where Codigo like '"+txtBuscarRespaldo.Text+"'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dataGridViewRespaldo.Rows.Add(codigo, descrip, precio, exist);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
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
                        comando = new SqlCommand("insert into Productos values(@Cod,@Tit,@Cat,@Pre)", cn2.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
                MessageBox.Show("Respaldo Guardado Correctamente");
                ProductosRes(dataGridViewRestauro);
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
                        comando = new SqlCommand("insert into Productos values(@Cod,@Tit,@Cat,@Pre)", cn.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
                MessageBox.Show("Restauración exitosa");
                ProductosC(dataGridViewRespaldo);
            }
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 1;
            new AyudaP().Show();
        }
    }
}
