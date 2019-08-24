using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Lab_Bustillos
{
    public partial class InventarioCR : Form
    {
        public InventarioCR()
        {
            InitializeComponent();
            Reporte();
        }
        public void Reporte()
        {
            ReportDocument crystal = new ReportDocument();
            crystal.Load(@"C:\Users\MarioBrosPlus\Documents\Visual Studio 2017\projects\Lab_Bustillos\Lab_Bustillos\InventarioR.rpt");
            crystalReportViewer1.ReportSource = crystal;
            crystalReportViewer1.Zoom(100);
            crystalReportViewer1.RefreshReport();
        }
    }
}
