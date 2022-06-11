using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hotel
{
    class MySqlDB
    {
        public string server;
        public string databaseName;
        public string userName;
        public string password;

        public MySqlConnection Connection;

        /// <summary>
        /// Устанавливает соединение с базой данных MySql
        /// </summary>
        /// <returns></returns>
        public bool isConnect()
        {
            try
            {
                if (Connection == null)
                {
                    string conString = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, databaseName, userName, password);
                    Connection = new MySqlConnection(conString);
                    Connection.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Изменяет статус соединения с Closed на Open. 
        /// Если соединение уже открыто, то ничего не делает.
        /// </summary>
        public void connectionStatus()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
    }
}
