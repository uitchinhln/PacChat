using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Storage
{
    public class MySQLStr
    {
        //Number of attempt to reconnect before displaying an error and stopping the server.
        private static readonly int TRY_OUT = 10;

        MySqlConnection dbConn;


        public MySQLStr()
        {

        }


    }

    public class Query
    {
        /*-----------------QUERY FOR THE FIRST RUN-----------------*/
        public static string CREATE_TBL_USER = "";


        /*-----------------QUERY FOR USERs TABLE-----=------------*/
        public static string CHECK_USER_EXISTED = "";
        public static string ADD_NEW_USER = "";


        /*-----------------QUERY FOR MESSAGEs TABLE----------------*/

    }
}
