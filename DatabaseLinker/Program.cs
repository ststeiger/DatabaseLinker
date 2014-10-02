
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace DatabaseLinker
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Globalization.IdnMapping idn = new System.Globalization.IdnMapping();
            string strPunyCode = idn.GetAscii("www.altstätten.ch");
            string strUnicode = idn.GetUnicode(strPunyCode);
            Console.WriteLine(strPunyCode);
            Console.WriteLine(strUnicode);

            if(true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                return;
            } // End if

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" --- Press any key to continue --- ");
            Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace DatabaseLinker 
