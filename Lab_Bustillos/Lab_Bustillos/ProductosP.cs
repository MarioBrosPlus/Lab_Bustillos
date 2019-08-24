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
    public partial class ProductosP : Form
    {
        Conexion cn = new Conexion();
        //SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlDataReader leer;
        public ProductosP()
        {
            InitializeComponent();
            btnRespaldar.Visible = false;
        }

        private void btnRespaldar_Click(object sender, EventArgs e)
        {
            string nombre = "", edad = "", direccion = "", telefono = "", usuario = "", contraseña = "", rol = "";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                nombre = row.Cells[0].Value.ToString();
                edad = row.Cells[1].Value.ToString();
                direccion = row.Cells[2].Value.ToString();
                telefono = row.Cells[3].Value.ToString();
            }
            if (ComboRespaldo.Text.Equals("Eliminado"))
            {
                try
                {
                    int i = 0;
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "insert into Productos values('" + nombre + "','" + edad + "','" + direccion + "','" + telefono + "');";
                    comando.ExecuteNonQuery();
                    i++;
                    if (i == 1)
                    {
                        MessageBox.Show("Se agrego correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
            else
            {
                try
                {
                    int i = 0;
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "update Productos set Codigo='" + nombre + "',Descripcion='" + edad + "',Precio='" + direccion + "',Existencias='" + telefono +"' where Codigo='"+nombre+"'";
                    comando.ExecuteNonQuery();
                    i++;
                    if (i == 1)
                    {
                        MessageBox.Show("Se agrego correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
        }

        private void ComboRespaldo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboRespaldo.Text != "Insertado")
            {
                btnRespaldar.Visible = true;

            }
            else
            {
                btnRespaldar.Visible = false;
            }
            dataGridView1.Rows.Clear();
            string codigo = "";
            string desc = "";
            int prec = 0;
            int exist = 0;
            try
            {
                comando = new SqlCommand("select * from RespaldoP where Estado='" + ComboRespaldo.SelectedItem.ToString() + "'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    codigo = leer.GetString(0);
                    desc = leer.GetString(1);
                    prec = leer.GetInt32(2);
                    exist = leer.GetInt32(3);
                    dataGridView1.Rows.Add(codigo, desc, prec, exist);
                }
                leer.Close();
                cn.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo realizar la busqueda: " + ex.ToString());
            }
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 1;
            new AyudaP().Show();
        }
    }
}
