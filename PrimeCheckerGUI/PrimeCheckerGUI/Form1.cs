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
        List<string> report_results_result_ni = new List<string>(); //Saves the result of the number input from the user 
        public static object user_input_from_file = new List<Object>(); //This is the user input from the file and stores into here

        public MainScreen() //Constructor for the main screen window 
        {
            InitializeComponent(); //Calls the initialize component 
        }

        private void button4_Click_1(object sender, EventArgs e) //This is the button for the upload file button 
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(); //Opens the dialog to browse for the user 

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; //Gives the option of the text file 
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                var fileContent = string.Empty;
                //Read the contents of the file into a stream
                var fileStream = openFileDialog1.OpenFile();
                var linesRead = File.ReadLines(selectedFileName); //Reading each line from the file which is upload please make sure it's all positive integers no error checking has been implemented
                foreach (var lineRead in linesRead)
                {
                    string cleanedLineRead = lineRead.Replace("\n", ""); //Read line and replace the new line character with an empty string 
                    user_input_manually =  Int32.Parse(cleanedLineRead); //Parse the string to an integer and store it into the variable 
                    running = true; //The checking is going on so the boolean will stay on 
                    if (running)
                    {
                        if (IsPrime(user_input_manually)) //Checks if the number is a prime or not 
                        { //Enters if it is and adds the respective things to the variables in case the lists. 
                            report_results_result_ni.Add(user_input_manually.ToString());
                            report_results_result_cp.Add("Prime");
                        }
                        else
                        {//Enters if it is not and adds the respective things to the variables in case the lists. 
                            report_results_result_ni.Add(user_input_manually.ToString());
                            report_results_result_cp.Add("Composite");
                        }
                    }
                    
                }
                const string message = "The results from the text file can be seen in the Report Section of the Program";
                const string caption = "Information";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information); //Displays the message box saying after a succesful completion of running the tests
                //The user can proceed to the accessing the report 
            }
        }

        private void button3_Click(object sender, EventArgs e) //Similar functionality as above except this is reading from the text box and doing it one per 
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
        public static bool IsPrime (int number) //Checking the primarility of a number 
        {
            for (int i = 2; i < number; i++)
                if (number % i == 0) //Checks to see if it is divisible or not 
                    return false;
            return true;
        }

        private void button1_Click(object sender, EventArgs e) //Cancel button is in place here 
        {
            running = false;
            label4.Text = "Cancelled Running Prime Checker";
        }
    }
}
