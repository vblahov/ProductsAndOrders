using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace ProductsAndOrders.Tools.Managers
{
    public class ConnectionManager
    {
        private static readonly ConnectionManager instance = new ConnectionManager();

        private SqlConnectionStringBuilder Builder { get; set; }
        public SqlConnection Connection { get; private set; }

        private ConnectionManager()
        {
            Builder.DataSource = "<localhost>.database.windows.net"; 
            Builder.UserID = "<sa>";            
            Builder.Password = "<reallyStrongPwd123>";     
            Builder.InitialCatalog = "<master>";
            Connection = new SqlConnection(Builder.ConnectionString);
            try
            {
                Connection.Open();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static ConnectionManager GetInstance()
        {
            return instance;
        }
    }
}