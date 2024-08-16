
using System.Data.SqlClient;

namespace posSystemWindows
{
    internal class ConnectionStrings
    {
        public static SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            //"Data Source=SQL8006.site4now.net;Initial Catalog=db_aaaeb1_posdb;User Id=db_aaaeb1_posdb_admin;Password=admin@123;TrustServerCertificate=True"
            DataSource = "SQL8020.site4now.net",
            InitialCatalog = "db_aaaeb1_posdb",
            UserID = "db_aaaeb1_posdb_admin",
            Password = "admin@123",
            TrustServerCertificate = true

            //"DefaultConnection": "Server=.;Database=posDB;User Id=sa;Password=admin123!@#;TrustServerCertificate=True"
            //    DataSource = ".",
            //    InitialCatalog = "posDB",
            //    UserID = "sa",
            //    Password = "admin123!@#",
            //    TrustServerCertificate = true
            };
        }
}
