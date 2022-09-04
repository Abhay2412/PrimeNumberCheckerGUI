//Abhay Khosla
//Prime Number Checker GUI Application
//Project Timeline: September 3rd - September 4th
//Location: Zürich, Switzerland 
//Time taken to complete: 8 hours 
using System;
using System.Windows.Forms;

namespace PrimeCheckerGUI
{
    static class Program //Main program which runs everything 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainScreen()); //Calls the main window screen and starts loading other components 
        }
    }
}
