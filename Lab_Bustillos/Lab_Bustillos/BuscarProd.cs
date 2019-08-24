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
    public partial class BuscarProd : Form
    {
        Conexion cn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        Ventas v = new Ventas();
        public delegate void pasar(string codigo,string descrip,string precio);
        public event pasar pasado;
        public BuscarProd()
        {
            InitializeComponent();
            ProductosC(dataGridViewBuscar);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow  row in dataGridViewBuscar.SelectedRows)
            {
                string nombre = row.Cells[0].Value.ToString();
                string desc = row.Cells[1].Value.ToString();
                string prec = row.Cells[2].Value.ToString();
                pasado(nombre,desc,prec);
                this.Close();
            }
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
        public void ProductosB(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                string codigo = "";
                string descrip = "";
                int precio = 0;
                int exist = 0;
                comando = new SqlCommand("select * from Productos where Codigo like '" + txtBuscar.Text + "%'", cn.AbrirConexion());
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ProductosB(dataGridViewBuscar);
        }
    }
}
