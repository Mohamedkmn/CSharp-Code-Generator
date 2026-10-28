using MyProjectCodGeneratorUtils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class clsCreateStoredProcedures
    {
        private  string TableName { get; }
        private DataTable InfoTable { get; }

       private clsConnectionSting Connection { get; }

        public clsCreateStoredProcedures(string UserName, string Password, string DatabaseName  , string TableName)
        {
            InfoTable = clsGetInfoAboutDatabasesData.GetInfoTable(UserName , Password , DatabaseName , TableName);
            this.TableName = TableName;
            Connection = new clsConnectionSting(UserName , Password , DatabaseName);
        }

        public DataTable ReturnInfoTable()
        {
            return InfoTable.Copy();
        }

        public string ReturnConnectionSting()
        {
            return Connection.ConnectionSting;
        }
        private   (string PramtersInput , string PramtersWhere ) GetPramterForProcedureGetInfoIDAndDelete()
        {
            StringBuilder PramtersInput = new StringBuilder("");
            StringBuilder PramtersWhere = new StringBuilder("");
 
            string ColumnName = "";

            foreach (DataRow Row in  InfoTable.Rows)
            {
                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes") 
                {
                    ColumnName = Row["COLUMN_NAME"].ToString();
 
                    PramtersInput.Append(" , "+"@"+ ColumnName + " " + Row["DATA_TYPE"].ToString() + Row["CharCount"].ToString());
                    PramtersWhere.Append(" and " + ColumnName + " = @" + ColumnName);
                }
 
            }
            if (PramtersInput.Length>0)
            {
                PramtersInput.Remove(0, 3);
                PramtersWhere.Remove(0, 5);

            }

            return (PramtersInput.ToString() , PramtersWhere.ToString()  );
        }
        private   ( string PramtersInput , string ColumnNamesInsart , string PramtersVlues , string SendColumnIdentity ) GetPramterForProcedureForAddNew()
        {
            StringBuilder PramtersInput = new StringBuilder("");
            StringBuilder ColumnNamesInsart = new StringBuilder("");
            StringBuilder PramtersVlues = new StringBuilder("");

            


            string COLUMN_NAME = "";

            string ColumnIdentity = "";
            string SendColumnIdentity = "";


            foreach (DataRow Row in  InfoTable.Rows)
            {
                COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                if (Row["Is_Identity"].ToString().ToLower() == "no")
                { 
                    PramtersInput.Append(" , "+"@" + COLUMN_NAME + " " + Row["DATA_TYPE"].ToString() + Row["CharCount"].ToString());

                    ColumnNamesInsart.Append(" , "+COLUMN_NAME );

                    PramtersVlues.Append(" , "+"@"+ COLUMN_NAME);

                 }
                else 
                {
                    ColumnIdentity =" , " + "@" + COLUMN_NAME + " " + Row["DATA_TYPE"].ToString() + " output ";
                    SendColumnIdentity ="@"+ COLUMN_NAME;
                 }
            }
            
            PramtersInput.Append(ColumnIdentity);

            if (ColumnNamesInsart.Length > 0)
            {
                 ColumnNamesInsart.Remove(0, 3);
                PramtersVlues.Remove(0, 3);

            }

            if(PramtersInput.Length > 0)
                PramtersInput.Remove(0, 3);


            return ( PramtersInput.ToString() , ColumnNamesInsart.ToString() , PramtersVlues.ToString() , SendColumnIdentity);
        }

        private   (string PramtersInput , string PramtersSet , string PramtersWhere) GetPramterForProcedureForUpdte()
        {
            StringBuilder PramtersInput = new StringBuilder("");
            StringBuilder PramtersSet = new StringBuilder("");
            StringBuilder PramtersWhere = new StringBuilder("");


            string COLUMN_NAME = "";

            foreach (DataRow Row in  InfoTable.Rows)
            {
                COLUMN_NAME = Row["COLUMN_NAME"].ToString();
                 
                PramtersInput.Append(" , "+"@" + COLUMN_NAME + " "+ Row["DATA_TYPE"].ToString() + Row["CharCount"].ToString()); 

                if (Row["Is_PrimaryKey"].ToString().ToLower() == "no")
                {
                    PramtersSet.Append(" , " + COLUMN_NAME + " = @" + COLUMN_NAME );
                }
                else
                {
                    PramtersWhere.Append( " and "+COLUMN_NAME + " = @" + COLUMN_NAME );
                }
            }

            if (PramtersInput.Length >0)
                PramtersInput.Remove(0, 3);

            if(PramtersSet.Length > 0)
                PramtersSet.Remove(0, 3);

            if(PramtersWhere.Length > 0)
                PramtersWhere.Remove(0, 5);


            return (PramtersInput.ToString() , PramtersSet.ToString() , PramtersWhere.ToString());
        }
        private   string GetNamesColumnPKForProcedureGetFullInfo()
        {
            StringBuilder PKs = new StringBuilder("");

            string FirstColumn = "";

            foreach (DataRow Row in  InfoTable.Rows)
            {
                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes")
                {
                    PKs.Append(" , " + Row["COLUMN_NAME"].ToString());
                }

            }

            if (PKs.Length > 0)
                PKs.Remove(0, 3);
            else
            {
                if ( InfoTable.Rows.Count > 0)
                {
                    FirstColumn =  InfoTable.Rows[0][0].ToString();

                }
            }

            return PKs.Length > 0 ? PKs.ToString() : FirstColumn;
        }

        public string CreateProcedureForGetFullInfoTable()
        {
            string PKs = GetNamesColumnPKForProcedureGetFullInfo();

            if (string.IsNullOrEmpty(PKs))
                throw new Exception("Pagination error: Table has No primary key columns available to use in ORDER BY clause.");

                try
                {

                string Qure1 = $"IF OBJECT_ID('sp_Get{TableName}InfoByOffset', 'P') IS NOT NULL   drop procedure sp_Get{TableName}InfoByOffset";

                string Qure2 = 
                    
                    $" create procedure sp_Get{TableName}InfoByOffset \n" +
                    " @PageSize int , @PageNumber int \n as begin \n\n" +
                    $" select * ,TotalRows = Count(1) over() from {TableName} \n  order by {PKs} \n " +
                    "  offset (@PageNumber - 1)*@PageSize Rows \n Fetch Next @PageSize Rows Only \n\n end  ";
                     
                       

                    using (SqlConnection connection = new SqlConnection(Connection.ConnectionSting))
                   { 
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Qure1, connection))
                    { 
                        command.ExecuteNonQuery();
                          
                    }
                    using (SqlCommand command2 = new SqlCommand(Qure2, connection))
                    {
                        command2.ExecuteNonQuery();

                    }



                }
                     

                   return $"sp_Get{TableName}InfoByOffSet" ;

                }
                catch (Exception ex)
                {

                  throw;
                }
               
        }

        public string   CreateProcedureForAddNew()
        { 
            (string PramtersInput, string ColumnNamesInsart, string PramtersVlues ,string SendColumnIdentity) Pramters = GetPramterForProcedureForAddNew();

            string QureSCOPE_IDENTITY = Pramters.SendColumnIdentity != "" ? $" set {Pramters.SendColumnIdentity} = SCOPE_IDENTITY(); " : "";


            string Qure1 = $"IF OBJECT_ID('sp_InsertInto{TableName}', 'P') IS NOT NULL   drop procedure sp_InsertInto{TableName}";

            string Qure2 = $" create procedure sp_InsertInto{TableName} \n {Pramters.PramtersInput} \n " +
                " as " +
                $" begin \n\n INSERT INTO [{TableName}] ({Pramters.ColumnNamesInsart}) VALUES ({Pramters.PramtersVlues}) \n " +
                $" " +
                $"    {QureSCOPE_IDENTITY}    \n end ";


            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionSting))
                {
                    connection.Open();

                   

                    using (SqlCommand command = new SqlCommand(Qure1, connection))
                    {
                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(Qure2, connection))
                    {

                        command.ExecuteNonQuery();
                    }

                }

                return $"sp_InsertInto{TableName}";
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public   string CreateProcedureForUpdate()
        {
            (string PramtersInput, string PramtersSet, string PramtersWhere) Pramters = GetPramterForProcedureForUpdte();

            if (string.IsNullOrEmpty(Pramters.PramtersWhere))
                return "";

            string Qure2 = $" create procedure sp_Update{TableName} \n " +
                $"{Pramters.PramtersInput} \n" +
                " as begin \n\n" +
                $" update [{ TableName}] \n  set {Pramters.PramtersSet} \n" +
                $" where  {Pramters.PramtersWhere} \n\n" +
                $" end ";

            string Qure1 = $"IF OBJECT_ID('sp_Update{TableName}', 'P') IS NOT NULL   drop procedure sp_Update{TableName}";


            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionSting))
                {
                    connection.Open();


                    using (SqlCommand command = new SqlCommand(Qure1, connection))
                    {
                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(Qure2, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }

                return $"sp_Update{TableName}";
            }
            catch (Exception  ex)
            {
                throw;
            }
           

        }

        public  string   CreateProcedureGetInfoByID()
        {
            (string PramtersInput, string PramtersWhere ) Pramters = GetPramterForProcedureGetInfoIDAndDelete();

            if (string.IsNullOrEmpty(Pramters.PramtersInput))
                return "" ; // make rorr      throw new Exception("Pagination error: Table has No primary key columns available to use in ORDER BY clause.");


            string Qure1 = $"IF OBJECT_ID('sp_GetInfo{TableName}ByID', 'P') IS NOT NULL   drop procedure sp_GetInfo{TableName}ByID";


            string Qure2 = $"create procedure sp_GetInfo{TableName}ByID \n" +
                $" {Pramters.PramtersInput} \n as " +
                $" begin \n\n select * from {TableName} \n" +
                $" where {Pramters.PramtersWhere} \n\n end";

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionSting))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Qure1, connection))
                    {
                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(Qure2 , connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }

                return $"sp_GetInfo{TableName}ByID";
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public string CreateProcedureForDeleteByID()
        {
            (string PramtersInput, string PramtersWhere) Pramters = GetPramterForProcedureGetInfoIDAndDelete();

            if (string.IsNullOrEmpty(Pramters.PramtersInput))
                return "";

            string Qure1 = $"IF OBJECT_ID('sp_DeleteFrom{TableName}ByID', 'P') IS NOT NULL   drop procedure sp_DeleteFrom{TableName}ByID ";


            string Qure2 = $" create procedure sp_DeleteFrom{TableName}ByID \n " +
                $" {Pramters.PramtersInput} \n as " +
                $"begin \n\n delete from {TableName} \n where {Pramters.PramtersWhere} \n\n end ";

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionSting))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Qure1, connection))
                    {
                        command.ExecuteNonQuery();

                    }

                    using (SqlCommand command = new SqlCommand(Qure2, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }

                return $"sp_DeleteFrom{TableName}ByID";
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
