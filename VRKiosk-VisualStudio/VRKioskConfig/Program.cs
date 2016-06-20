using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRKioskConfig
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new VrKioskMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show("VRKiosk Encountered an error: \n\n" + ex.Message + "\n\n" + ex.StackTrace, "ERROR - VRKiosk General Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
