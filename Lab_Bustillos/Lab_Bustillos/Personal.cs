using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class Personal : Form
    {
        Conexion cn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlCommand comando = new SqlCommand();
        private static int suma=0;
        public static string nombreP = "";
        public static int click = 0;
        private static int opcion=0;
        string nombre, edad, direccion, telefono, usuario, contraseña, rol;
        public Personal()
        {
            InitializeComponent();
            ComboRol.SelectedIndex = 0;
            Usuarios(dataGridView1);
            cont.Text = Convert.ToString(click);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ClickB();
            if (opcion == 0)
            {
                try
                {
                    int i = 0;
                    //Se abre la conexion a la base de datos y se ejecuta el procedimiendo almacenado con el paso de parametros dado
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "Exec AgregarU '" + Nombretxt.Text + "','" + Edadtxt.Text + "','" + Direcciontxt.Text + "','" + Telefonotxt.Text + "','" + Usuariotxt.Text + "','" + Contraseñatxt.Text + "','" + ComboRol.SelectedItem.ToString() + "';";
                    comando.ExecuteNonQuery();
                    Usuarios(dataGridView1);
                    i++;

                    if (i == 1)//Si el procedimiendo se ejecuto con exito da como resultado uno
                    {
                        MessageBox.Show("Se agrego correctamente");//Todo OK
                        Limpiar();
                    }
                    else
                    {
                        //Si los datos ya existen lanza un mensaje de datos duplicados
                        MessageBox.Show("Datos Duplicados");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Datos Duplicados");
                    Limpiar();
                }
            }
            else {
                try
                {
                    int i = 0;
                    //Se abre la conexion a la base de datos y se ejecuta el procedimiendo almacenado con el paso de parametros dado
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "Exec ModificarU '" + Nombretxt.Text + "','" + Edadtxt.Text + "','" + Direcciontxt.Text + "','" + Telefonotxt.Text + "','" + Usuariotxt.Text + "','" + Contraseñatxt.Text + "','" + ComboRol.Text + "','" + nombre + "','" + edad + "','" + direccion + "','" + telefono + "','" + usuario + "','" + contraseña + "','" + rol + "';";
                    comando.ExecuteNonQuery();
                    Usuarios(dataGridView1);
                    i++;

                    if (i == 1)//Si el procedimiendo se ejecuto con exito da como resultado uno
                    {
                        MessageBox.Show("Se Modifico correctamente");
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch (SqlException ex)
                {
                    //Si hubo algun problema con la base de datos el programa lanzara un error
                    MessageBox.Show("Error: " + ex.ToString());
                }
                opcion = 0;
            }
        }
        //Estos metodos llaman al metodo ClickB para llevar el conteo de los clicks
        private void ComboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Nombretxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Edadtxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Direcciontxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Telefonotxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Usuariotxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void Contraseñatxt_Click(object sender, EventArgs e)
        {
            ClickB();
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            ClickB();
        }
        //Los metodos _Enter sirven para el placeholder del campo de texto al igual que los metodos _Leave
        private void Nombretxt_Enter(object sender, EventArgs e)
        {
            if (Nombretxt.Text == "Nombre")
            {
                Nombretxt.Text = "";
                Nombretxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Edadtxt_Enter(object sender, EventArgs e)
        {
            if (Edadtxt.Text == "Edad")
            {
                Edadtxt.Text = "";
                Edadtxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Direcciontxt_Enter(object sender, EventArgs e)
        {
            if (Direcciontxt.Text == "Dirección")
            {
                Direcciontxt.Text = "";
                Direcciontxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Telefonotxt_Enter(object sender, EventArgs e)
        {
            if (Telefonotxt.Text == "Telefono")
            {
                Telefonotxt.Text = "";
                Telefonotxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Usuariotxt_Enter(object sender, EventArgs e)
        {
            if (Usuariotxt.Text == "Usuario")
            {
                Usuariotxt.Text = "";
                Usuariotxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Contraseñatxt_Enter(object sender, EventArgs e)
        {
            if (Contraseñatxt.Text == "Contraseña")
            {
                Contraseñatxt.Text = "";
                Contraseñatxt.ForeColor = System.Drawing.Color.White;
            }
        }

        private void Nombretxt_Leave(object sender, EventArgs e)
        {
            if (Nombretxt.Text == "")
            {
                Nombretxt.Text = "Nombre";
                Nombretxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void Edadtxt_Leave(object sender, EventArgs e)
        {
            if (Edadtxt.Text == "")
            {
                Edadtxt.Text = "Edad";
                Edadtxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void Direcciontxt_Leave(object sender, EventArgs e)
        {
            if (Direcciontxt.Text == "")
            {
                Direcciontxt.Text = "Dirección";
                Direcciontxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void Telefonotxt_Leave(object sender, EventArgs e)
        {
            if (Telefonotxt.Text == "")
            {
                Telefonotxt.Text = "Telefono";
                Telefonotxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void Usuariotxt_Leave(object sender, EventArgs e)
        {
            if (Usuariotxt.Text == "")
            {
                Usuariotxt.Text = "Usuario";
                Usuariotxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void Contraseñatxt_Leave(object sender, EventArgs e)
        {
            if (Contraseñatxt.Text == "")
            {
                Contraseñatxt.Text = "Contraseña";
                Contraseñatxt.ForeColor = System.Drawing.Color.LightGray;
            }
        }
        //-------------------------------------------------------------------
        
        //Estos metodos reciben un parametro de tipo datagridview para el llenado de las tablas
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
                    //Los valores de la base de datos se pasan a variables creadas anteriormente
                    nombre = leer.GetString(0);
                    edad = leer.GetInt32(1);
                    direccion = leer.GetString(2);
                    telefono = leer.GetString(3);
                    usuario = leer.GetString(4);
                    contraseña = leer.GetString(5);
                    rol = leer.GetString(6);
                    //Los datos de agregan a la tabla en la interfaz
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
        //Este metodo busca todas la coincidencias que encuentre con lo que se bsuca en el campo de texto de BUSQUEDA
        public void PersonalB(DataGridView dgv)
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
                comando = new SqlCommand("select * from Personal where Nombre like '" + txtBuscar.Text + "%'", cn.AbrirConexion());
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    //Los valores de la base de datos se pasan a variables creadas anteriormente
                    nombre = leer.GetString(0);
                    edad = leer.GetInt32(1);
                    direccion = leer.GetString(2);
                    telefono = leer.GetString(3);
                    usuario = leer.GetString(4);
                    contraseña = leer.GetString(5);
                    rol = leer.GetString(6);
                    //Los datos de agregan a la tabla en la interfaz
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
        //-----------------------------------------------------------------------------------------------------------------
        //Los keyPress nos sirvieron para validar si son numero o letras dependiendo del tipo de campo
        private void Nombretxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //IsLetter(e.KeyChar) permite cualquier caracter entre la a y la z tanto mayusculas como minusculas
            //IsWhiteSpace permite espacio en blanco.
            //'\b' permite poder eliminar caracteres con BackSpace

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Direcciontxt_KeyPress(object sender, KeyPressEventArgs e)
        {

            //IsLetter(e.KeyChar) permite cualquier caracter entre la a y la z tanto mayusculas como minusculas
            //IsWhiteSpace permite espacio en blanco.
            //'\b' permite poder eliminar caracteres con BackSpace

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Edadtxt_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void Telefonotxt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 0;
            new AyudaP().Show();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Usuariotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //IsLetter(e.KeyChar) permite cualquier caracter entre la a y la z tanto mayusculas como minusculas
            //IsWhiteSpace permite espacio en blanco.
            //'\b' permite poder eliminar caracteres con BackSpace

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            PersonalB(dataGridView1);
        }

        
        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ClickB();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {               
                nombre = row.Cells[0].Value.ToString();
                edad = row.Cells[1].Value.ToString();
                direccion = row.Cells[2].Value.ToString();
                telefono = row.Cells[3].Value.ToString();
                usuario = row.Cells[4].Value.ToString();
                contraseña = row.Cells[5].Value.ToString();
                rol = row.Cells[6].Value.ToString();
            }
            try
            {
                int i = 0;
                comando.Connection = cn.AbrirConexion();
                comando.CommandText = "Exec EliminarU '" + nombre + "','" + edad + "','" + direccion + "','" + telefono + "','" + usuario + "','" + contraseña + "','" + rol + "';";
                comando.ExecuteNonQuery();
                Usuarios(dataGridView1);
                i++;

                if (i == 1)
                {
                    MessageBox.Show("Se Elimino correctamente");
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
            ClickB();
            opcion = 1;
            //Saca los valores de la fila seleccionada y los pasa a los campos de texto
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Nombretxt.Text = row.Cells[0].Value.ToString();
                Edadtxt.Text = row.Cells[1].Value.ToString();
                Direcciontxt.Text = row.Cells[2].Value.ToString();
                Telefonotxt.Text = row.Cells[3].Value.ToString();
                Usuariotxt.Text = row.Cells[4].Value.ToString();
                Contraseñatxt.Text = row.Cells[5].Value.ToString();
                ComboRol.SelectedItem = row.Cells[6].Value.ToString();
                nombre = row.Cells[0].Value.ToString();
                edad = row.Cells[1].Value.ToString();
                direccion = row.Cells[2].Value.ToString();
                telefono = row.Cells[3].Value.ToString();
                usuario = row.Cells[4].Value.ToString();
                contraseña = row.Cells[5].Value.ToString();
                rol = row.Cells[6].Value.ToString();
            }
            Nombretxt.Enabled = false;
        }
        public void Limpiar()
        {
            Nombretxt.Text = "Nombre";
            Edadtxt.Text = "Edad";
            Direcciontxt.Text = "Dirección";
            Telefonotxt.Text = "Telefono";
            Usuariotxt.Text = "Usuario";
            Contraseñatxt.Text = "Contraseña";
            ComboRol.SelectedIndex = 0;
            Nombretxt.Enabled = true;
            Usuarios(dataGridView1);
        }
        //Este metodo sirve para llevar el conteo de los clicks que el usuario da.
        public void ClickB() {
            click += 1;
            try
            {
                comando.Connection = cn.AbrirConexion();
                comando.CommandText = "update Clicks set Clicks='"+click+"' where Nombre='" + nombreP + "'";
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw;
            }
            cn.CerrarConexion();
            try
            {
                comando.Connection = cn.AbrirConexion();
                comando.CommandText = "select * from Clicks where Nombre='"+nombreP+"'";
                leer = comando.ExecuteReader();
                int ed = 0;

                while (leer.Read())
                {
                    ed++;
                    cont.Text = Convert.ToString(leer.GetInt32(1));
                }
            }
            catch (Exception e)
            {

                throw;
            }
            cn.CerrarConexion();

        }
    }
}
