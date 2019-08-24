using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class PersonalR : Form
    {
        Conexion cn = new Conexion();
        //SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        DataTable tabla = new DataTable();
        SqlDataAdapter da;
        SqlDataReader leer;
        public PersonalR()
        {
            InitializeComponent();
            btnRespaldar.Visible = false;
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
            string nombre = "";
            int edad = 0;
            string direccion = "";
            string telefono = "";
            string usuario = "";
            string contraseña = "";
            string rol = "";
            try
            {
                comando = new SqlCommand("select * from RespaldoU where Estado='" + ComboRespaldo.SelectedItem.ToString() + "'", cn.AbrirConexion());
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
                    dataGridView1.Rows.Add(nombre, edad, direccion, telefono, usuario, contraseña, rol);
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
            string nombre="", edad="", direccion="", telefono="", usuario="", contraseña="", rol="";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                nombre = row.Cells[0].Value.ToString();
                edad= row.Cells[1].Value.ToString();
                direccion= row.Cells[2].Value.ToString();
                telefono= row.Cells[3].Value.ToString();
                usuario= row.Cells[4].Value.ToString();
                contraseña= row.Cells[5].Value.ToString();
                rol= row.Cells[6].Value.ToString();
            }
            if (ComboRespaldo.Text.Equals("Eliminado"))
            {
                try
                {
                    int i = 0;
                    comando.Connection = cn.AbrirConexion();
                    comando.CommandText = "insert into Personal values('"+nombre+"','"+edad+"','"+direccion+"','"+telefono+"','"+usuario+"','"+contraseña+"','"+rol+"');";
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
                    comando.CommandText = "update Personal set Nombre='"+nombre+"',Edad='"+edad+"',Direccion='"+direccion+"',Telefono='"+telefono+"',Usuario='"+usuario+"',Contraseña='"+contraseña+"',Rol='"+rol+"' where Nombre='"+nombre+"'";
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
    }
}
