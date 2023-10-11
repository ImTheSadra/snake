using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleGame
{
    internal static class Program
    {
        /// <summary>
        /// snake game 
        /// made by Sadra (https://github.com/SadraZ3R0)
        /// :) 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new gameForm());
        }
    }
}
