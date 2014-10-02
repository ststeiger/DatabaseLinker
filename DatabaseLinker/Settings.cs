
namespace DatabaseLinker
{


    public class Settings
    {


        public static DB.Abstraction.cDAL SetupSourceDal()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = "CORDB2008R2";
            csb.InitialCatalog = "COR_Basic_Wincasa";
            // csb.CurrentLanguage = "de-CH";

            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ApertureWebServicesDE";
                csb.Password = "*****";
            }

            return DB.Abstraction.cDAL.CreateInstance("MS_SQL", csb.ConnectionString);
        } // End Function SetupSourceDal 


        public static DB.Abstraction.cDAL SetupTargetDal()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            //csb.DataSource = "COR-W81-101";
            csb.DataSource = System.Environment.MachineName;
            csb.InitialCatalog = "WincasaMirror";

            //csb.DataSource = "CORDB2005";
            //csb.InitialCatalog = "COR_Basic";
            // csb.CurrentLanguage = "de-CH";

            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "ApertureWebServicesDE";
                csb.Password = "*****";
            }

            return DB.Abstraction.cDAL.CreateInstance("MS_SQL", csb.ConnectionString);
        } // End Function SetupTargetDal 

        public static string RemoteServer = "CORDB2008R2";
        public static DB.Abstraction.cDAL Source = SetupSourceDal();
        public static DB.Abstraction.cDAL Target = SetupTargetDal();

    } // End Class Settings


} // End Namespace DatabaseLinker
