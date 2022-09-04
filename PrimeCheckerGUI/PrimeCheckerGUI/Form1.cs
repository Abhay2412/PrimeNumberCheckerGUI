//Abhay Khosla
//Prime Number Checker GUI Application
//Project Timeline: September 3rd - September 4th
//Location: Zürich, Switzerland 
//Time taken to complete: 8 hours 

//All of the pacakges used in this application 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace PrimeCheckerGUI
{
    public partial class MainScreen : Form //This is the class for the main screen 
    {
        public static bool running = true; //Checks if the tests are running or not and will cancel them 
        List<string> report_results_result_cp = new List<string>(); //Saves the result of the composite or prime helpful for the results in the report
        List<string> report_results_result_ni = new List<string>(); //Saves the result of the number input from the user or reading it from the manually 
        public static object user_input_from_file = new List<Object>(); //

        public MainScreen()
        {
            InitializeComponent();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                var fileContent = string.Empty;
                //Read the contents of the file into a stream
                var fileStream = openFileDialog1.OpenFile();
                var linesRead = File.ReadLines(selectedFileName);
                foreach (var lineRead in linesRead)
                {
                    string cleanedLineRead = lineRead.Replace("\n", "");
                    user_input_manually =  Int32.Parse(cleanedLineRead);
                    running = true;
                    if (running)
                    {
                        if (IsPrime(user_input_manually))
                        {
                            report_results_result_ni.Add(user_input_manually.ToString());
                            report_results_result_cp.Add("Prime");
                        }
                        else
                        {
                            report_results_result_ni.Add(user_input_manually.ToString());
                            report_results_result_cp.Add("Composite");
                        }
                    }
                    
                }
                const string message = "The results from the text file can be seen in the Report Section of the Program";
                const string caption = "Information";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportScreen form2 = new ReportScreen(report_results_result_cp, report_results_result_ni);
            form2.ShowDialog();
        }
        int user_input_manually;
        private void button2_Click(object sender, EventArgs e)
        {
            running = true;
            if (running)
            {
                user_input_manually = Int32.Parse(textBox1.Text);
                if (IsPrime(user_input_manually))
                {
                    label4.Text = user_input_manually.ToString() + " - Prime";
                    report_results_result_ni.Add(user_input_manually.ToString());
                    report_results_result_cp.Add("Prime");
                }
                else
                {
                    label4.Text = user_input_manually.ToString() + " - Composite";
                    report_results_result_ni.Add(user_input_manually.ToString());
                    report_results_result_cp.Add("Composite");
                }
            }
        }
        public static bool IsPrime (int number)
        {
            for (int i = 2; i < number; i++)
                if (number % i == 0)
                    return false;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            running = false;
            label4.Text = "Cancelled Running Prime Checker";
        }
    }
}
