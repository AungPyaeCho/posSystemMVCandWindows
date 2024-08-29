using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace posSystemWindows
{
    internal class ConnectionStrings
    {
        public static SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        static ConnectionStrings()
        {
            // Load connection string from file or use default
            string savedConnectionString = LoadConnectionStringFromFile();

            if (!string.IsNullOrEmpty(savedConnectionString))
            {
                _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(savedConnectionString);
            }
            else
            {
                // Default connection string
                _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    //DataSource = ".",
                    //InitialCatalog = "posDB",
                    //UserID = "sa",
                    //Password = "admin123!@#",
                    //TrustServerCertificate = true

                    DataSource = "SQL8020.site4now.net",
                    InitialCatalog = "db_aaaeb1_posdb",
                    UserID = "db_aaaeb1_posdb_admin",
                    Password = "admin@123",
                    TrustServerCertificate = true
                };
            }
        }

        public static void UpdateConnectionString(string dataSource, string initialCatalog, string userID, string password)
        {
            _sqlConnectionStringBuilder.DataSource = dataSource;
            _sqlConnectionStringBuilder.InitialCatalog = initialCatalog;
            _sqlConnectionStringBuilder.UserID = userID;
            _sqlConnectionStringBuilder.Password = password;

            // Save the updated connection string to file (encrypted)
            SaveConnectionStringToFile(_sqlConnectionStringBuilder.ConnectionString);
        }

        private static void SaveConnectionStringToFile(string connectionString)
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "posSystemWindows",
                "connectionString.txt");

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            // Encrypt the connection string
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(connectionString),
                null,
                DataProtectionScope.CurrentUser);

            // Save the encrypted data to file
            File.WriteAllBytes(filePath, encryptedData);
        }

        private static string LoadConnectionStringFromFile()
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "posSystemWindows",
                "connectionString.txt");

            if (File.Exists(filePath))
            {
                // Read the encrypted data from file
                byte[] encryptedData = File.ReadAllBytes(filePath);

                // Decrypt the data
                byte[] decryptedData = ProtectedData.Unprotect(
                    encryptedData,
                    null,
                    DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(decryptedData);
            }

            return null;
        }
    }
}
