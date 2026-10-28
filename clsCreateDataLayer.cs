using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyProjectCodGeneratorUtils;

namespace BusinessLayer
{
    public class clsCreateDataLayer
    {
        public string TableName { get; }

        private clsCreateStoredProcedures CreateProcedures;

        private string DatabaseName;

        private string ConnectionSting;
        private DataTable InfoTable { get; }


        public clsCreateDataLayer(string UserName, string Password, string DatabaseName, string TableName)
        {
            this.TableName = TableName;
            CreateProcedures = new clsCreateStoredProcedures(UserName, Password, DatabaseName, TableName);
            this.DatabaseName = DatabaseName;
            InfoTable = CreateProcedures.ReturnInfoTable();
            this.ConnectionSting = CreateProcedures.ReturnConnectionSting();
        }

        private string CreateClassDTO()
        {
            StringBuilder Propert = new StringBuilder("");

            foreach (DataRow Row in InfoTable.Rows)
            {
                Propert.Append($"public {clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString())} {Row["COLUMN_NAME"].ToString()} {{ get; set; }}\n");
            }

            return $"public class cls{TableName}DTO {{\n\n" +
                $"{Propert}" +
                $"\n\n}}";
        }

        private string ConnectionStringInfoToSetInAppConfing()
        {
            string InstructionsForUse = "Because the code is automatically generated, press (ctrl + K + D) to organize the code .      (-;\n\n" + "//Info To Show For User\n   // 1.Copy the following connection string.\n   //2.Open App.config in your project.\n      //3.Paste it inside<configuration>.\n  //4.Restart the application.\n//====>";

            string Info = $" //<connectionStrings> <add name=\"ConnectionString{DatabaseName}\" connectionString= \" {ConnectionSting}  \" /> </connectionStrings>\n" +
                $"//Steps:\n//Right-click on References\n//Click Add Reference\n//Select System.Configuration\n//Click OK\n";
            return InstructionsForUse + Info;
        }

        private string CreateFunGetAllInfoAboutTableByOffsetPaging()
        {

            string Fun = $"  public static  DataTable GetAllInfoFrom{TableName}ByOffsetPaging (int PageNumber , int PageSize ) {{ \n\n  " +
                $"   DataTable Info = new DataTable(); \n\n" +
                " try { \n\n   " +
                $" string ConnectionString = ConfigurationManager.ConnectionStrings[\"ConnectionString{DatabaseName}\"].ConnectionString; \n" +
                " using (SqlConnection connection = new SqlConnection(ConnectionString)) {\n\n " +
                " connection.Open(); \n" +
                $" using (SqlCommand command = new SqlCommand(\"{CreateProcedures.CreateProcedureForGetFullInfoTable()}\", connection)) {{ \n\n" +
                $" command.CommandType = CommandType.StoredProcedure; \n" +
                $" command.Parameters.AddWithValue(\"@PageNumber\", PageNumber); \n" +
                $" command.Parameters.AddWithValue(\"@PageSize\", PageSize); \n" +
                " using (SqlDataReader reader = command.ExecuteReader()) {\n\n" +
                "  Info.Load(reader); \n" +
                "\n\n}" +
                $"\n\n}}" +
                "  \n\n} return Info; " +

                "  \n\n}catch { \n\n  throw; \n } \n\n }";


            return Fun;
        }

        private string GetColumnNameIdentityFroAddNew()
        {


            foreach (DataRow Row in InfoTable.Rows)
            {

                if (Row["Is_Identity"].ToString().ToLower() == "yes")
                    return Row["COLUMN_NAME"].ToString();

            }

            return "";
        }

        private string GetParametersAddWithValueInputForAddNew(string VariableNameSqlCommand)
        {
            StringBuilder Text = new StringBuilder("");

            foreach (DataRow Row in InfoTable.Rows)
            {

                string COLUMN_NAME = "";

                if (Row["Is_Identity"].ToString().ToLower() == "no")
                {
                    COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                    Text.Append($"if({TableName}DTO.{COLUMN_NAME} != null)\n");

                    Text.Append(VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" ," + TableName + "DTO." + COLUMN_NAME + ");\n");

                    Text.Append("else\n" + VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" , DBNull.Value " + ");\n\n");

                }

            }

            return Text.ToString() + "\n";
        }
        private string GetParametersAddWithValueInputForUpdate(string VariableNameSqlCommand)
        {
            StringBuilder Text = new StringBuilder("");
            string COLUMN_NAME = "";

            foreach (DataRow Row in InfoTable.Rows)
            {


                COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                Text.Append($"if({TableName}DTO.{COLUMN_NAME} != null)\n");

                Text.Append(VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" ," + TableName + "DTO." + COLUMN_NAME + ");\n");

                Text.Append("else\n" + VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" , DBNull.Value " + ");\n\n");


            }

            return Text.ToString() + "\n";
        }
        private string GetParametersAddWithValueInputForGetInfoByIDAndDelete(string VariableNameSqlCommand)
        {
            StringBuilder Text = new StringBuilder("");

            foreach (DataRow Row in InfoTable.Rows)
            {

                string COLUMN_NAME = "";

                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes")
                {
                    COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                    Text.Append($"if({TableName}DTO.{COLUMN_NAME} != null)\n");

                    Text.Append(VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" ," + TableName + "DTO." + COLUMN_NAME + ");\n");

                    Text.Append("else\n" + VariableNameSqlCommand + ".Parameters.AddWithValue(\"@" + COLUMN_NAME + "\" , DBNull.Value " + ");\n\n");

                }

            }

            return Text.ToString() + "\n";
        }

        private string GetTextReadFromReaderForGetInfoByPK(string VariableNameReader)
        {
            StringBuilder Text = new StringBuilder("");

            string ColumnName = "";

            foreach (DataRow Row in InfoTable.Rows)
            {
                if (Row["Is_PrimaryKey"].ToString().ToLower() == "no")
                {
                    ColumnName = Row["COLUMN_NAME"].ToString();

                    if (Row["IS_NULLABLE"].ToString().ToLower() == "yes")
                    {
                        Text.Append("if(" + VariableNameReader + "[\"" + ColumnName + "\"]!= DBNull.Value) \n");
                        Text.Append(TableName + "DTO." + ColumnName + "= (" + clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString().ToLower()) + ") " + VariableNameReader + "[\"" + ColumnName + "\"];\n else \n" + TableName + "DTO." + ColumnName + " =default;\n\n");

                    }
                    else
                        Text.Append(TableName + "DTO." + ColumnName + "= (" + clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString().ToLower()) + ") " + VariableNameReader + "[\"" + ColumnName + "\"];\n\n");
                }
            }


            return Text.ToString();
        }

        private string CreateFunGetGetInfoByPK()
        {

            string Fun = $"  static public bool GetInfo{TableName}ByPK (cls{TableName}DTO {TableName}DTO) {{\n bool IsFound = false; \n\n" +
                $" try {{\n\n " +
                $" string ConnectionString = ConfigurationManager.ConnectionStrings[\"ConnectionString{DatabaseName}\"].ConnectionString; \n " +
                $" using (SqlConnection connection = new SqlConnection(ConnectionString)) {{\n\n" +
                $" connection.Open(); \n" +
                $" using (SqlCommand command = new SqlCommand(\"{CreateProcedures.CreateProcedureGetInfoByID()}\", connection)) {{\n\n" +
                $" command.CommandType = System.Data.CommandType.StoredProcedure;\n {GetParametersAddWithValueInputForGetInfoByIDAndDelete("command")} \n" +
                $"  using (SqlDataReader reader = command.ExecuteReader()){{\n\n" +
                $" if (reader.Read()){{\n\n" +
                $" {GetTextReadFromReaderForGetInfoByPK("reader")} IsFound = true;\n " +
                $"\n\n}}" +
                $"\n\n}} " +
                $"\n\n}}" +
                $"\n\n}}" +
                $"\n\n}} catch {{\nthrow;\n}}" +
                $"  return IsFound; \n" +
                $"\n\n}}";

            return Fun;
        }

        private string CreateFunAddNew(string ColumnNameIdentityFroAddNew)
        {
            string ReturnVariableDefinition = "";
            string DataTypeReturn = "void";
            string Return = "";
            string SqlParameterOutput = "";
            string SqlParameterValueOutput = "";


            if (ColumnNameIdentityFroAddNew != "")
            {
                ReturnVariableDefinition = "int? NumNewRow = default;\n";
                DataTypeReturn = "int?";
                Return = "return NumNewRow ;\n";
                SqlParameterOutput = $"SqlParameter  OutPutParameterNumNewRow = new SqlParameter( \"@{ColumnNameIdentityFroAddNew}\", SqlDbType.Int);\n" +
                  "OutPutParameterNumNewRow.Direction = ParameterDirection.Output;\n" +
                  "command.Parameters.Add(OutPutParameterNumNewRow);\n";
                SqlParameterValueOutput = "NumNewRow = OutPutParameterNumNewRow.Value != DBNull.Value ?  (int)OutPutParameterNumNewRow.Value : default;";
            }



            string Fun = $" static public {DataTypeReturn} AddNewIn{TableName}(cls{TableName}DTO {TableName}DTO) {{\n\n" +
                $" {ReturnVariableDefinition} try{{\n\n " +
                $" string ConnectionString = ConfigurationManager.ConnectionStrings[\"connectionString{TableName}\"].ConnectionString; \n " +
                $" using (SqlConnection connection = new SqlConnection(ConnectionString)){{\n\n" +
                $" connection.Open();\n" +
                $" using (SqlCommand command = new SqlCommand(\"{CreateProcedures.CreateProcedureForAddNew()}\", connection)){{\n " +
                $" command.CommandType = CommandType.StoredProcedure;\n" +
                $" {GetParametersAddWithValueInputForAddNew("command")} " +
                $"{SqlParameterOutput}" +
                $"command.ExecuteNonQuery();\n" +
                $" {SqlParameterValueOutput}" +

                $"\n\n}}" +
                $"\n\n}}" +
                $"\n\n}}catch {{\n\n" +
                $"throw;" +
                $"\n\n}}" +
                $"{Return}" +
                $"\n\n}}";



            return Fun;
        }

        private string CreateFunUpdate()
        {
            string Fun = $" static public bool Update{TableName} (cls{TableName}DTO {TableName}DTO) {{\n\n" +
                $"  bool IsSuccess = false;\n" +
                $"try{{\n\n" +
                $"  string ConnectionString = ConfigurationManager.ConnectionStrings[\"connectionString{DatabaseName}\"].ConnectionString;\n\n" +
                $" using (SqlConnection connection = new SqlConnection(ConnectionString)){{\n\n" +
                $"connection.Open();\n\n" +
                $"  using (SqlCommand command = new SqlCommand(\"{CreateProcedures.CreateProcedureForUpdate()}\", connection)){{\n\n" +
                $" command.CommandType = CommandType.StoredProcedure;\n" +
                $"{GetParametersAddWithValueInputForUpdate("command")}" +
                $"IsSuccess = command.ExecuteNonQuery() > 0 ? true : false ;\n" +
                $"\n\n}}" +
                $"\n\n}}" +
                $"\n\n}}catch{{\n" +
                $"throw;\n" +
                $"\n}}" +
                $" return IsSuccess; \n" +
                $"\n\n}}";

            return Fun;
        }
        private string CreateFunDelete()
        {
            string Fun = $" static public bool  DeleteFrom{TableName}ByPK(cls{TableName}DTO {TableName}DTO){{\n\n" +
                $" bool IsSuccess = false;\n" +
                $"try{{\n\n" +
                $" string ConnectionString = ConfigurationManager.ConnectionStrings[\"connectionString{DatabaseName}\"].ConnectionString;\n" +
                $" using (SqlConnection connection = new SqlConnection(ConnectionString)){{\n\n" +
                $" connection.Open();\n\n" +
                $"using (SqlCommand command = new SqlCommand(\"{CreateProcedures.CreateProcedureForDeleteByID()}\", connection)){{\n\n" +
                $"command.CommandType = CommandType.StoredProcedure;\n" +
                $"{GetParametersAddWithValueInputForGetInfoByIDAndDelete("command")}" +
                $" IsSuccess = command.ExecuteNonQuery() > 0 ? true : false;\n" +
                $"\n\n}}" +
                $"\n\n}}" +
                $"\n\n}}catch{{\n\n" +
                $"throw;" +
                $"\n\n}}" +
                $"return IsSuccess;\n" +
                $"\n\n}}";

            return Fun;
        }

        public string CreateDataLayer()
        {

            string Namespace = "using System;\nusing System.Configuration;\nusing System.Data;\nusing System.Data.SqlClient;\n" +
                " \n\n namespace DAL{\n\n" +
                $"{ConnectionStringInfoToSetInAppConfing()}\n" +
                $"{CreateClassDTO()}" +
                $"public class cls{TableName}Data{{\n\n" +
                $"{CreateFunGetAllInfoAboutTableByOffsetPaging()}\n" +
                $"{CreateFunGetGetInfoByPK()}\n" +
                $"{CreateFunAddNew(GetColumnNameIdentityFroAddNew())}\n" +
                $"{CreateFunUpdate()}\n" +
                $"{CreateFunDelete()}\n" +
                $"\n\n}}" +
                "\n\n}";

            return Namespace;
        }


       

    }
}
