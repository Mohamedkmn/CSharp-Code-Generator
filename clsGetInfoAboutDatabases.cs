using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsGetInfoAboutDatabases
    {

        public static DataTable GetNamesDatabasesFromDatabase(string UserName, string Password)
        {
            return clsGetInfoAboutDatabasesData.GetNamesDatabasesFromDatabase(UserName , Password);
        }

        public static DataTable GetNameTablesForDatabase(string UserName, string Password ,string DatabaseName)
        {
            return clsGetInfoAboutDatabasesData.GetNameTablesForDatabase(UserName , Password,DatabaseName);
            
        }
    }
}
