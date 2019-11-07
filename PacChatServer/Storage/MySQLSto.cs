using MySql.Data.MySqlClient;
using PacChatServer.Command;
using PacChatServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Storage
{
    public class MySQLSto
    {
        static MySQLSto instance;

        //Number of attempt to reconnect before displaying an error and stopping the server.
        private static int TRY_OUT = 10;
        private static int TRIED = 0;


        MySqlConnection dbConn;


        private MySQLSto()
        {
            OpenConnection();
            CreateDefault();
        }

        public User GetUser(string emailOrID)
        {
            User user = null;
            int id = -1;
            try
            {
                id = Convert.ToInt32(emailOrID);
            } catch (Exception) { }

            try
            {
                MySqlCommand query = new MySqlCommand(Query.GET_USER_INFO, dbConn);
                query.Parameters.AddWithValue("email", emailOrID);
                query.Parameters.AddWithValue("id", id);

                MySqlDataReader reader = query.ExecuteReader();                
            }
            catch (MySqlException e)
            {
                if (TRIED >= TRY_OUT)
                {
                    PacChatServer.GetServer().Logger.Error(e);
                    PacChatServer.GetCommandManager().ExecuteCommand(ConsoleSender.Instance, DefaultCommands.STOP);
                    return user;
                }
                TRIED++;
                CloseConnection();
                OpenConnection();
                return GetUser(emailOrID);
            }
            finally
            {
                TRIED = 0;
            }

            return user;
        }

        public void AddNewUser(User user)
        {

        }

        private void CreateDefault()
        {
            try
            {
                MySqlCommand query = new MySqlCommand(Query.CREATE_TBL_USER, dbConn);
                query.ExecuteNonQuery();


            } catch (MySqlException e)
            {
                if (TRIED >= TRY_OUT)
                {
                    PacChatServer.GetServer().Logger.Error(e);
                    PacChatServer.GetCommandManager().ExecuteCommand(ConsoleSender.Instance, DefaultCommands.STOP);
                    return;
                }
                TRIED++;
                CloseConnection();
                OpenConnection();
                CreateDefault();
            } finally
            {
                TRIED = 0;
            }
        }

        public void OpenConnection()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = ServerSettings.MYSQL_HOST;
            builder.UserID = ServerSettings.MYSQL_USER;
            builder.Password = ServerSettings.MYSQL_PASSWORD;
            builder.Database = ServerSettings.MYSQL_DATABASE;
            builder.Port = ServerSettings.MYSQL_PORT;

            dbConn = new MySqlConnection(builder.ToString());
            dbConn.Open();
        }

        public void CloseConnection()
        {
            if (dbConn != null && dbConn.State != System.Data.ConnectionState.Closed)
            {
                dbConn.Close();
            }
        }



        private class Query
        {
            /*-----------------QUERY FOR THE FIRST RUN-----------------*/
            public static readonly string CREATE_TBL_USER = "CREATE TABLE IF NOT EXISTS `users` (`id` INT NOT NULL AUTO_INCREMENT, `email` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `passhash` VARCHAR(45) NOT NULL, `firstname` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `lastname` VARCHAR(45) CHARACTER SET 'utf8' NOT NULL, `dob` DATE NULL DEFAULT now(), `Gender` TINYINT(10) NULL DEFAULT 0, PRIMARY KEY (`id`), UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE) ENGINE = InnoDB;";


            /*-----------------QUERY FOR USERs TABLE-------------------*/
            public static readonly string GET_USER_INFO = "SELECT * FROM `user` WHERE email= ?email OR id= ?id;";
            public static readonly string GET_USER_RELA = "SELECT * FROM `user_rla` WHERE user1= ?id OR user2= ?id;";
            public static readonly string ADD_NEW_USER = "";


            /*-----------------QUERY FOR USER RELATIONSHIPs TABLE------*/



            /*-----------------QUERY FOR MESSAGEs TABLE----------------*/

        }

        public static MySQLSto Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySQLSto();
                }
                return instance;
            }
        }
    }    
}
