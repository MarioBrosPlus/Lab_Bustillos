using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class Acerca_de : Form
    {
        public Acerca_de()
        {
            InitializeComponent();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            AyudaP.opcion = 3;
            new AyudaP().Show();
            Console.WriteLine("Hola Mundo Mario");
        }
    }
}
