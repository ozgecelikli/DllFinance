using DevExpress.Skins;
using DevExpress.UserSkins;
using DllFinance.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using System.Data;

namespace DllFinance
{ 
    static class Program
    {
        private static System.Threading.Timer updateTimer;
         
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
  
        static void Main()
        {
            // IP, database adı, kullanıcı adı ve şifreyi buraya yazın
            #region ERPIS
            //string ip = "89.252.161.43,5331";
            //string databaseName = "UTEST";
            //string username = "sa";
            //string password = "_!P@ss_0rdXws2022*..+41-Edit";
            #endregion


            #region SHINKO
            string ip = "192.168.30.231";
            string databaseName = "SQLERPIS";
            string username = "erpis";
            string password = "htTPcjDFZAfbgRqmK2dE4z";
            #endregion

       

            #region OZGUR_U
            //string ip = "81.213.147.43";
            //string databaseName = "SQLERPIS";
            //string username = "sa";
            //string password = "1qwr59!p0?zxe";
            #endregion


            //SqlHelper sqlHelper = new SqlHelper(ip, databaseName, username, password);
            SqlHelper.Instance.Initialize(ip, databaseName, username, password);
            // Define view names
            List<string> viewNames = new List<string>
            {
                "CF_alisfaturaView",
                "CF_satisfaturaView",
                "CF_vadesigelenView",
                "CF_leasingView",
                "CF_krediView",
                "CF_ceksenetView"
            };

            // Load data from views
            SqlHelper.Instance.LoadDataFromViews(viewNames);
  


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
          
        }
    }
}