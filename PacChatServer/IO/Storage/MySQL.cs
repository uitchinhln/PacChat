using MySql.Data.MySqlClient;
using PacChatServer.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.IO.Storage
{
    public class MySQL
    {
        static MySQL instance;

        //Number of attempt to reconnect before displaying an error and stopping the server.
        private static int RETRY_LIMIT = 10;
        private static int RETRY_COUNT = 0;

        /*-----------------QUERY FOR THE FIRST RUN-----------------*/
        static readonly string CREATE_TBL_USER = "CREATE TABLE IF NOT EXISTS `users` (`id` INT NOT NULL AUTO_INCREMENT, `email` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `passhash` VARCHAR(45) NOT NULL, `firstname` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `lastname` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `dob` DATE NULL, `gender` TINYINT(10) NULL DEFAULT 0, PRIMARY KEY (`id`), UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE) ENGINE = InnoDB;";

        MySqlConnection dbConn;


        private MySQL()
        {
            OpenConnection();
            CreateDefault();
        }

        public void OpenConnection()
        {
            CloseConnection();

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = ServerSettings.MYSQL_HOST;
            builder.UserID = ServerSettings.MYSQL_USER;
            builder.Password = ServerSettings.MYSQL_PASSWORD;
            builder.Database = ServerSettings.MYSQL_DATABASE;
            builder.Port = ServerSettings.MYSQL_PORT;

            dbConn = new MySqlConnection(builder.ToString());
            dbConn.Open();
        }

        public void ExecuteToDB(String query, Dictionary<string, object> parameters)
        {
            MySqlDataReader reader = null;
            MySqlCommand command = null;

            try
            {
                command = new MySqlCommand(query, dbConn);

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                PacChatServer.GetServer().Logger.Error(e);
                if (RETRY_COUNT >= RETRY_LIMIT)
                {
                    PacChatServer.GetCommandManager().ExecuteCommand(ConsoleSender.Instance, DefaultCommands.STOP);
                    return;
                }
                RETRY_COUNT++;
                OpenConnection();
                ExecuteToDB(query, parameters);
            }
            finally
            {
                Cleanup(reader);
            }
            RETRY_COUNT = 0;
        }


        private void CreateDefault()
        {
            MySqlDataReader reader = null;
            MySqlCommand query = null;

            try
            {
                query = new MySqlCommand(CREATE_TBL_USER, dbConn);
                query.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                PacChatServer.GetServer().Logger.Error(e);
                if (RETRY_COUNT >= RETRY_LIMIT)
                {
                    PacChatServer.GetCommandManager().ExecuteCommand(ConsoleSender.Instance, DefaultCommands.STOP);
                    return;
                }
                RETRY_COUNT++;
                OpenConnection();
                CreateDefault();
            }
            finally
            {
                Cleanup(reader);
            }
            RETRY_COUNT = 0;
        }

        public void CloseConnection()
        {
            try
            {
                if (dbConn != null && dbConn.State != System.Data.ConnectionState.Closed)
                {
                    dbConn.Close();
                }
            }
            catch (Exception) { }
        }

        private void Cleanup(MySqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed) reader.Close();
        }

        public static MySQL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySQL();
                }
                return instance;
            }
        }
    }
}
