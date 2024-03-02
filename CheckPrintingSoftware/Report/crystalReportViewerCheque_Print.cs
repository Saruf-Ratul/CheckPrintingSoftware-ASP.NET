using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckPrintingSoftware.Report
{
    public partial class crystalReportViewerCheque_Print : Form
    {

        public crystalReportViewerCheque_Print()
        {
            InitializeComponent();
        }

        public static ReportDocument ReportSource { get; internal set; }

        private void crystalReportViewerCheque_Print_Load(object sender, EventArgs e)
        {
            LoadDefaultReport();
            this.reportViewer2.RefreshReport();
        }
        private void LoadDefaultReport()
        {
            // Load the default Crystal Report file
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("C:\\Users\\Saruf Ratul\\Downloads\\Compressed\\CheckPrintingSoftware\\CheckPrintingSoftware\\Report\\cheque_print.rpt");

            // Set the report document to the CrystalReportViewer control
            crystalReportViewerCheque_Print.ReportSource = reportDocument;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
