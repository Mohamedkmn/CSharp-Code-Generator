using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class clsGetInfoAboutDatabasesData
    {
        public static DataTable GetNamesDatabasesFromDatabase(string UserName, string Password)
        {
            string ConnectionSting = $"server=.;user id ={UserName};password={Password}";


            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSting))
                {
                    connection.Open();

                    string CommandString = " select name from sys.databases ";

                    using (SqlCommand command = new SqlCommand(CommandString, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }

                    }
                }

                return dt;
                 
            }
            catch (Exception ex)
            {

                throw;

            }


         }


        public static DataTable GetNameTablesForDatabase(string UserName, string Password,string DatabaseName)
        { 
            DataTable dt = new DataTable();
            string ConnectionSting= $"server=.;database={DatabaseName};user id ={UserName};password={Password}";
            try
            { 
                using (SqlConnection connection = new SqlConnection(ConnectionSting))
                {
                    connection.Open();

                    string CommandString = "  SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'; ";

                    using (SqlCommand command = new SqlCommand(CommandString, connection))
                    {
 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }

                    }
                }

                return dt;


            }
            catch (Exception ex)
            {
                throw;
            }



        }


        public static DataTable GetInfoTable(string UserName, string Password, string DatabaseName, string tableName)
        {
            DataTable InfoTable = new DataTable();
            
            string ConnectionSting = $"server=.;database={DatabaseName};user id ={UserName};password={Password}";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSting))
                {
                    connection.Open();

                    string CommandString = " SELECT   C.COLUMN_NAME,  C.DATA_TYPE ,  CharCount = ISNULL( '('+cast( C.CHARACTER_MAXIMUM_LENGTH as nvarchar)+')' ,'') ,    C.IS_NULLABLE,     CASE    " +
                        " WHEN TC.CONSTRAINT_TYPE = 'PRIMARY KEY' THEN 'YES'        ELSE 'NO'     END AS Is_PrimaryKey " +
                        " ,CASE        WHEN COLUMNPROPERTY(       OBJECT_ID(C.TABLE_SCHEMA + '.' + C.TABLE_NAME),          C.COLUMN_NAME," +
                        "         'IsIdentity'     ) = 1 THEN 'YES'        ELSE 'NO'   END AS Is_Identity" +
                        " FROM INFORMATION_SCHEMA.COLUMNS C LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU   " +
                        "  ON C.TABLE_NAME = KCU.TABLE_NAME       AND C.COLUMN_NAME = KCU.COLUMN_NAME LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC" +
                        "   ON KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME    AND TC.CONSTRAINT_TYPE = 'PRIMARY KEY' WHERE C.TABLE_NAME = @TableName " +
                        "  order by case when TC.CONSTRAINT_TYPE ='PRIMARY KEY'  then 0 else 1 end ";

                    using (SqlCommand command = new SqlCommand(CommandString, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            InfoTable.Load(reader);
                        }

                    }
                    return InfoTable;

                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
