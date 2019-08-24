using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class Ventas : Form
    {
        Conexion cn = new Conexion();
        //SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlDataReader leer;
        public static string codigo;
        public Ventas()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = "";
                string titulo = "";
                int categoria = 0;
                int precio = 0;
                comando = new SqlCommand("select * from Productos where Codigo='" + Codigotxt.Text + "'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    titulo = leer.GetString(1);
                    categoria = leer.GetInt32(2);
                    precio = leer.GetInt32(3);

                }
                leer.Close();
                if (codigo != "")
                {
                    Tabla1.Rows.Add(codigo, titulo, precio, 1, precio);
                }
                else
                {
                    MessageBox.Show("Elemento no existente");
                }
                cn.CerrarConexion();
                //da.Fill(tabla);
                //Tabla1.DataSource = tabla;
                Codigotxt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
            Suma();
        }

        private void btnMás_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Tabla1.SelectedRows)
            {
                row.Cells[3].Value = Convert.ToInt16(row.Cells[3].Value) + 1;
                row.Cells[4].Value = Convert.ToInt16(row.Cells[2].Value) * Convert.ToInt16(row.Cells[3].Value);
            }
            Suma();
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Tabla1.SelectedRows)
            {
                row.Cells[3].Value = Convert.ToInt16(row.Cells[3].Value) - 1;
                row.Cells[4].Value = Convert.ToInt16(row.Cells[2].Value) * Convert.ToInt16(row.Cells[3].Value);

                if (row.Cells[3].Value.Equals(0))
                {
                    Tabla1.Rows.RemoveAt(Tabla1.CurrentRow.Index);
                }
            }
            Suma();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            int c = 0;
            int f = 0;
            foreach (DataGridViewRow row in Tabla1.Rows)
            {
                int codigo = 0;
                comando = new SqlCommand("select Existencias from Productos where Codigo='" + row.Cells[0].Value + "'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetInt32(0);
                }
                c = Convert.ToInt16(row.Cells[3].Value);
                cn.CerrarConexion();
                if (c <= codigo)
                {
                    try
                    {
                        comando = new SqlCommand("insert into Ventas values(@Cod,@Tit,@Cat,@Pre,@cant,@fec)", cn.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.Parameters.AddWithValue("@cant", row.Cells[4].Value.ToString());
                        comando.Parameters.AddWithValue("@fec", date.ToString("dd/MM/yyyy"));
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();
                        //----------------------------------------------------------------------------------------------------------
                        try
                        {
                            int d = 0;
                            d = codigo - c;
                            comando = new SqlCommand("update Productos set Existencias='" + d.ToString() + "' where Codigo='" + row.Cells[0].Value + "'", cn.AbrirConexion());
                            comando.ExecuteNonQuery();
                            cn.CerrarConexion();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error de Actualizacion: " + ex);
                            throw;
                        }
                        //----------------------------------------------------------------------------------------------------------

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error de insercion: " + ex);
                    }
                    f = f + Int16.Parse(row.Cells[4].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Elementos Insuficientes: " + row.Cells[1].Value);
                }
            }
            MessageBox.Show("Monto Total: " + "\n" + f);
            Tabla1.Rows.Clear();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProd b = new BuscarProd();
            b.pasado += new BuscarProd.pasar(ejecutar);
            b.Show();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 4;
            new AyudaP().Show();
        }
        public void ejecutar(string codi,string desc,string preci)
        {
            Tabla1.Rows.Add(codi,desc,preci,1,preci);
            Suma();
        }
        
        public void Suma() {
            int sumaC = 0;
            const int columna = 4;            
            foreach (DataGridViewRow row in Tabla1.Rows)
            {
                sumaC += Convert.ToInt32(row.Cells[columna].Value);
                label4.Text = Convert.ToString(sumaC);
            }

        }
    }
}
