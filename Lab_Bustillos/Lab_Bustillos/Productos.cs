using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class Productos : Form
    {

        Conexion cn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        private static int opcion = 0;
        public Productos()
        {
            InitializeComponent();
            ProductosC(dataGridView1);
            Random();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (opcion == 0)
            {
                try
                {
                    int i = 0;
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "exec AgregarP'" + txtCodigo.Text + "','" + txtDescripcion.Text + "','" + txtPrecio.Text + "','" + txtExist.Text + "'";
                    comando.ExecuteNonQuery();
                    //Usuarios(dataGridView1);
                    ProductosC(dataGridView1);

                    i++;

                    if (i == 1)
                    {
                        MessageBox.Show("Se agrego correctamente");
                        Limpiar();
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
                    comando.CommandText = "exec ModificarP '" + txtCodigo.Text + "','" + txtDescripcion.Text + "','" + txtPrecio.Text + "','" + txtExist.Text + "','" + codigo + "','" + desc + "','" + pre + "','" + exist + "'";
                    comando.ExecuteNonQuery();
                    //Usuarios(dataGridView1);
                    ProductosC(dataGridView1);

                    i++;

                    if (i == 1)
                    {
                        MessageBox.Show("Se agrego correctamente");
                        Limpiar();
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
                opcion = 0;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dat in dataGridView1.SelectedRows)
            {
                codigo = dat.Cells["Column1"].Value.ToString();
                desc = dat.Cells["Column2"].Value.ToString();
                pre = dat.Cells["Column3"].Value.ToString();
                exist = dat.Cells["Column4"].Value.ToString();
            }
            try
            {
                int i = 0;
                comando.Connection = cn.AbrirConexion();
                comando.CommandText = "exec EliminarP'" + codigo + "','" + desc + "','" + pre + "','" + exist + "'";
                comando.ExecuteNonQuery();
                ProductosC(dataGridView1);

                i++;

                if (i == 1)
                {
                    MessageBox.Show("Se agrego correctamente");
                    Limpiar();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            opcion = 1;
            foreach (DataGridViewRow dat in dataGridView1.SelectedRows)
            {
                txtCodigo.Text = dat.Cells["Column1"].Value.ToString();
                txtDescripcion.Text = dat.Cells["Column2"].Value.ToString();
                txtPrecio.Text = dat.Cells["Column3"].Value.ToString();
                txtExist.Text = dat.Cells["Column4"].Value.ToString();
                codigo = dat.Cells["Column1"].Value.ToString();
                desc = dat.Cells["Column2"].Value.ToString();
                pre = dat.Cells["Column3"].Value.ToString();
                exist = dat.Cells["Column4"].Value.ToString();
                txtCodigo.Enabled = false;
            }
        }
        string codigo, desc, pre, exist;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ProductosB(dataGridView1);
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

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "Descripción")
            {
                txtDescripcion.Text = "";
                txtDescripcion.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                txtDescripcion.Text = "Descripción";
                txtDescripcion.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "Precio")
            {
                txtPrecio.Text = "";
                txtPrecio.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "")
            {
                txtPrecio.Text = "Precio";
                txtPrecio.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void txtExist_Enter(object sender, EventArgs e)
        {
            if (txtExist.Text == "Existencias")
            {
                txtExist.Text = "";
                txtExist.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtExist_Leave(object sender, EventArgs e)
        {
            if (txtExist.Text == "")
            {
                txtExist.Text = "Existencias";
                txtExist.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "Codigo")
            {
                txtCodigo.Text = "";
                txtCodigo.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                txtCodigo.Text = "Existencias";
                txtCodigo.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //IsLetter(e.KeyChar) permite cualquier caracter entre la a y la z tanto mayusculas como minusculas
            //IsWhiteSpace permite espacio en blanco.
            //'\b' permite poder eliminar caracteres con BackSpace

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtExist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Index == 3)
            {
                if (Convert.ToInt32(e.Value) <= 5)
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Red;
                }
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
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
        public void Limpiar()
        {
            txtCodigo.Text = "Codigo";
            txtDescripcion.Text = "Descripción";
            txtPrecio.Text = "Precio";
            txtExist.Text = "Existencias";
            txtCodigo.Enabled = true;
            ProductosC(dataGridView1);
            Random();
        }
        public void Random()
        {
            Random r = new Random();
            int num = r.Next(1000, 9999);
            txtCodigo.Text = Convert.ToString(num);

        }
    }

}
