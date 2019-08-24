using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class VentasCR : Form
    {
        ReportDocument crystal = new ReportDocument();
        public static int opcion = 0;
        public VentasCR()
        {
            InitializeComponent();
        }
        public void Reporte()
        {

            crystal.Load(@"C:\Users\MarioBrosPlus\Documents\Visual Studio 2017\Projects\Lab_Bustillos\Lab_Bustillos\VentasRE.rpt");
            crystal.Refresh();
            ParameterFieldDefinition crpfd;
            ParameterFieldDefinitions crpfds;
            ParameterValues crpv = new ParameterValues();
            ParameterDiscreteValue crpdv = new ParameterDiscreteValue();
            string algo = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            crpdv.Value = algo;
            crpfds = crystal.DataDefinition.ParameterFields;
            crpfd = crpfds["fechaVenta"];
            crpv = crpfd.CurrentValues;
            crpv.Add(crpdv);
            crpfd.ApplyCurrentValues(crpv);
            crystalReportViewer1.ReportSource = crystal;
            crystalReportViewer1.Zoom(100);


        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            Reporte();
        }
    }
}
