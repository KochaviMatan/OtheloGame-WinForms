using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OtheloGameUI startOtheloGame = new OtheloGameUI();
        }
    }
}