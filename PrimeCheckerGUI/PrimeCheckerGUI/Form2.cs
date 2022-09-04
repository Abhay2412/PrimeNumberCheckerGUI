//Abhay Khosla
//Prime Number Checker GUI Application
//Project Timeline: September 3rd - September 4th
//Location: Zürich, Switzerland 
//Time taken to complete: 8 hours 

//All of the pacakges used in this application 
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.Analysis;
using System.IO;

namespace PrimeCheckerGUI
{
    public partial class ReportScreen : Form
    {
        List<string> report_results1_cp = new List<string>();
        List<string> report_results1_ni = new List<string>();
        public ReportScreen(List<string> report_resultsAsParameter1, List<string> report_resultsAsParameter2)
        {
            InitializeComponent();
            report_results1_cp = report_resultsAsParameter1;
            report_results1_ni = report_resultsAsParameter2;
            for (int index = 0; index < report_results1_cp.Count; index++)
            {
                dataGridView1.Rows.Add(report_results1_ni[index], report_results1_cp[index]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] report_results1_ni_array = report_results1_ni.ToArray();
            string[] report_results1_cp_array = report_results1_cp.ToArray();
            StringDataFrameColumn numberInput_Column = new StringDataFrameColumn("Number Input", report_results1_ni_array);
            StringDataFrameColumn compositePrime_Column = new StringDataFrameColumn("Composite/Prime", report_results1_cp_array);
            DataFrame df = new DataFrame(numberInput_Column, compositePrime_Column);
            Console.WriteLine(df);
            string userName = Environment.UserName;
            string strPath = @"C:\Users\" + userName + @"\Desktop\";
            string fileName = "primeNumberCheckerReport.txt";
            string fullPath = strPath + fileName;
            File.WriteAllText(fullPath, df.ToString());

        }
    }
}
