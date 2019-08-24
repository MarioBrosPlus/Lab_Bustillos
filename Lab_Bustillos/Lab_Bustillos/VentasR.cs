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
    public partial class VentasR : Form
    {
        //Se crean las conexiones y se declaran las variables responsables de que las consultas funcionen
        Conexion cn = new Conexion();
        Conexion2 cn2 = new Conexion2();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        public VentasR()
        {
            InitializeComponent();
            Respaldar(dataGridViewRespaldo);
            Restaurar(dataGridViewRestauro);
        }
        //Metodo que recibe parametro de tipo datagridview y llena la tabla de Respaldo
        public void Respaldar(DataGridView dgv) {
            dgv.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                int dg=0;
                string fecha = "";
                //La sentencia va a la base de datos y extrae todos los datos que se piden
                comando = new SqlCommand("select * from Ventas", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    //Los datos se guardan en variables previamente declaradas
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dg = leer.GetInt32(4);
                    fecha = leer.GetString(5);
                    //Se agregan a la tabla
                    dgv.Rows.Add(codigo, descrip, precio, exist,dg,fecha);
                }
                leer.Close();

                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                //Si hay algun error el catch manda un mensaje y el programa se detiene
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }
        //Este metodo hace exactamente lo mismo que el anterior solo que este carga la tabla de la base de datos de respaldo
        public void Restaurar(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                int dg = 0;
                string fecha = "";
                comando = new SqlCommand("select * from Ventas", cn2.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dg = leer.GetInt32(4);
                    fecha = leer.GetString(5);
                    dgv.Rows.Add(codigo, descrip, precio, exist, dg, fecha);
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
            //La condicion dicta que se la tabla respaldo esta vacia y el campo de busqueda igual
            if (dataGridViewRespaldo.Rows.Count == 0 && txtBuscarRespaldo.Text.Length == 0)
            {
                //Lanza un mensaje 
                MessageBox.Show("La base de datos no tiene datos para respaldar");
            }
            //Si la tabña de respaldo esta vacia y el campo de texto no
            else if (dataGridViewRespaldo.Rows.Count == 0 && txtBuscarRespaldo.Text.Length != 0) {
                //Lanza un mensaje
                MessageBox.Show("No se encontraron datos con esa referencia");
                //Si la tabla no esta vacia
            } else if (dataGridViewRespaldo.Rows.Count != 0) {
                foreach (DataGridViewRow row in dataGridViewRespaldo.Rows)
                {
                    try
                    {
                        //Se hace la insercion de los datos de la tabla a la base de datos de respaldo.
                        DateTime date = DateTime.Now;
                        comando = new SqlCommand("insert into Ventas values(@Cod,@Tit,@Cat,@Pre,@cant,@fec)", cn2.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.Parameters.AddWithValue("@cant", row.Cells[4].Value.ToString());
                        comando.Parameters.AddWithValue("@fec", row.Cells[5].Value.ToString());
                        comando.ExecuteNonQuery();
                        cn.CerrarConexion();
                        
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
                MessageBox.Show("Respaldo Guardado Correctamente");
            }
        }

        private void txtBuscarRespaldo_TextChanged(object sender, EventArgs e)
        {
            //Busca en tiempo real dentro de la base de datos y te muestra los datos que busques
            dataGridViewRespaldo.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                int dg = 0;
                string fecha = "";
                comando = new SqlCommand("select * from Ventas where Fecha like '"+txtBuscarRespaldo.Text+"%'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    descrip = leer.GetString(1);
                    precio = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dg = leer.GetInt32(4);
                    fecha = leer.GetString(5);
                    dataGridViewRespaldo.Rows.Add(codigo, descrip, precio, exist, dg, fecha);
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
                comando = new SqlCommand("truncate table Ventas", cn.AbrirConexion());
                comando.ExecuteNonQuery();
                foreach (DataGridViewRow row in dataGridViewRestauro.Rows)
                {
                    try
                    {
                        
                        comando = new SqlCommand("insert into Ventas values(@Cod,@Tit,@Cat,@Pre,@cant,@fec)", cn.AbrirConexion());
                        comando.Parameters.AddWithValue("@Cod", row.Cells[0].Value.ToString());
                        comando.Parameters.AddWithValue("@Tit", row.Cells[1].Value.ToString());
                        comando.Parameters.AddWithValue("@Cat", row.Cells[2].Value.ToString());
                        comando.Parameters.AddWithValue("@Pre", row.Cells[3].Value.ToString());
                        comando.Parameters.AddWithValue("@cant", row.Cells[4].Value.ToString());
                        comando.Parameters.AddWithValue("@fec", row.Cells[5].Value.ToString());
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

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 2;
            new AyudaP().Show();
        }
    }
}
