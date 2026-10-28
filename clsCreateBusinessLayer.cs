using DataLayer;
using MyProjectCodGeneratorUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public class clsCreateBusinessLayer
    {
        private string TableName;
        private DataTable InfoTable { get; }
        public clsCreateBusinessLayer(string UserName, string Password, string DatabaseName, string TableName)
        {
            InfoTable = clsGetInfoAboutDatabasesData.GetInfoTable(UserName, Password, DatabaseName, TableName);

            this.TableName = TableName;
        }

 
        private string CreatePropertyClass()
        {
            StringBuilder Properties = new StringBuilder("");
            StringBuilder PropertiesDTO = new StringBuilder("");

            string COLUMN_NAME = "";


            Properties.Append("public enum enMode { AddNew, Upadte };\n private enMode _Mode { get; set; }\n");

            foreach (DataRow Row in InfoTable.Rows)
            {
                COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                Properties.Append($"public {clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString())} {COLUMN_NAME} ");

                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes")
                    Properties.Append($"{{ get;private set; }}\n");
                else
                    Properties.Append($"{{ get; set; }}\n");

                PropertiesDTO.Append($"DTO.{COLUMN_NAME} = {COLUMN_NAME};\n");

            }


            return Properties.ToString() + $"public DAL.cls{TableName}DTO {TableName}DTO{{\n\n" +
                $"get{{\n" +
                $"DAL.cls{TableName}DTO DTO = new DAL.cls{TableName}DTO() ;\n" +
                $"{PropertiesDTO}" +
                $"return DTO;\n " +
                $"\n}}" +
                $"\n\n}}";
        }
        private string CreateDefaultConstructor()
        {
            string DefaultConstructor = "";

            DefaultConstructor = $"public cls{TableName}(){{\n";
            DefaultConstructor += "_Mode = enMode.AddNew;\n}";

            return DefaultConstructor;
        }
        private string CreateParameterizedConstructor()
        {

            StringBuilder ParameterizedConstructor = new StringBuilder("");


            foreach (DataRow Row in InfoTable.Rows)
            {

                ParameterizedConstructor.Append($"this.{Row["COLUMN_NAME"].ToString()} = {TableName}DTO.{Row["COLUMN_NAME"].ToString()};\n");
            }



            return $"private cls{TableName} (DAL.cls{TableName}DTO {TableName}DTO) {{\n\n this._Mode = enMode.Upadte;\n {ParameterizedConstructor}  \n}}";
        }

        private string GetPramterForGetInfoByPK()
        {
            string PramtersForFunction = "";


            foreach (DataRow Row in InfoTable.Rows)
            {

                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes")
                {
                    PramtersForFunction += $",{clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString())} {Row["COLUMN_NAME"].ToString()}";
                }


            }

            if (PramtersForFunction.Length > 0)
            {
                PramtersForFunction = PramtersForFunction.Remove(0, 1);
            }

            return PramtersForFunction;
        }

        private string CreateFunGetInfoByPK()
        {


            return $"static public cls{TableName} GetInfoFrom{TableName}ByPK ({GetPramterForGetInfoByPK()}) {{\n\n" +
                $"DAL.cls{TableName}DTO {TableName}DTO = new DAL.cls{TableName}DTO(); \n\n" +
                $"if(cls{TableName}Data.GetInfo{TableName}ByPK({TableName}DTO)){{\n" +
                $" return new cls{TableName} ({TableName}DTO);" +
                $"\n}}" +
                $"else\n" +
                $"return null;\n" +
                $"\n\n}}";
        }

        private (string PramtersForFunction, string CreateObjectFromClassDTOAndFill) GetPramterForDeleteByPKAndFillInfoDTO()
        {
            string PramtersForFunction = "";
            string CreateObjectFromClassDTOAndFill = $"DAL.cls{TableName}DTO {TableName}DTO = new DAL.cls{TableName}DTO(); \n\n";
            string COLUMN_NAME = "";

            foreach (DataRow Row in InfoTable.Rows)
            {


                if (Row["Is_PrimaryKey"].ToString().ToLower() == "yes")
                {

                    COLUMN_NAME = Row["COLUMN_NAME"].ToString();

                    PramtersForFunction += $",{clsUtils.GetDataTypeCFormSQLDataType(Row["DATA_TYPE"].ToString())} {COLUMN_NAME}";
                    CreateObjectFromClassDTOAndFill += $"{TableName}DTO.{COLUMN_NAME} = {COLUMN_NAME}; \n";
                }


            }

            if (PramtersForFunction.Length > 0)
            {
                PramtersForFunction = PramtersForFunction.Remove(0, 1);
            }

            return (PramtersForFunction, CreateObjectFromClassDTOAndFill);
        }

        private string CreateFunDeleteInfoByPK()
        {
            (string PramtersForFunction, string CreateObjectFromClassDTOAndFill) Pram = GetPramterForDeleteByPKAndFillInfoDTO();

            return $"static public bool DeleteFrom{TableName}ByPK ({Pram.PramtersForFunction}){{\n\n" +
                $"{Pram.CreateObjectFromClassDTOAndFill}\n" +
                $"return cls{TableName}Data.DeleteFrom{TableName}ByPK({TableName}DTO);\n" +
                $"\n\n}}";
        }

    
        private string GetCloumnIdentityForAddNew()
        {
            foreach (DataRow Row in InfoTable.Rows)
            {


                if (Row["Is_Identity"].ToString().ToLower() == "yes")
                    return Row["COLUMN_NAME"].ToString();
            }

            return "";
        }

        private string CreateFunAddNew()
        {
            string CloumnIdentity = GetCloumnIdentityForAddNew();

            string Text = "";

            if (CloumnIdentity != "")
            {
                Text = $" this.{CloumnIdentity} = cls{TableName}Data.AddNewIn{TableName}({TableName}DTO);\n" +
                    $"return this.{CloumnIdentity} != null;\n";
            }
            else
            {
                Text = $"cls{TableName}Data.AddNewIn{TableName}({TableName}DTO);\n" +
                    $"return true;\n";
            }


            return $"private bool _AddNew(){{\n\n" +
                $"DAL.cls{TableName}DTO {TableName}DTO = new DAL.cls{TableName}DTO();\n" +
             
                $"{Text}" +
                $"\n\n}}";
        }

        private string CreateFunUpdate()
        {

            return $"private bool _Update(){{\n\n" +
                $"DAL.cls{TableName}DTO {TableName}DTO = new DAL.cls{TableName}DTO();\n" +
                 $"return cls{TableName}Data.Update{TableName}({TableName}DTO);\n" +
                $"\n\n}}";
        }

        private string CreateFunSave()
        {
            return $"public bool Save(){{\n\n" +
                $"switch(_Mode){{\n\n" +
                $"case enMode.AddNew:\n" +
                $"if(_AddNew()){{\n\n" +
                $"_Mode = enMode.Upadte;\n" +
                $"return true;\n" +
                $"\n\n}}" +
                $"break;\n" +
                $"case enMode.Upadte:\n" +
                $"return _Update();\n" +
                $"break;\n" +
                $"\n\n}}" +
                $"return false;\n" +
                $"\n\n}}";
        }

        private string CreateFunGetInfoByOffsetPaging()
        {
            return $"static public DataTable Get{TableName}ByOffsetPaging (int PageNumber, int PageSize ){{\n\n" +
                $" return cls{TableName}Data.GetAllInfoFrom{TableName}ByOffsetPaging(PageNumber ,PageSize );" +
                $"\n\n}}";
        }


        public string CreateBusinessLayer()
        {
             
 
            return $"using DAL;\nusing System;\nusing System.Data;\nnamespace BLL{{\n\n" +
                $"public class cls{TableName}{{\n\n" +
                $"{CreatePropertyClass()}\n" +
                $"{CreateDefaultConstructor()}\n" +
                $"{CreateParameterizedConstructor()}\n" +
                $"{CreateFunGetInfoByPK()}\n" +
                $"{CreateFunDeleteInfoByPK()}\n" +
                $"{CreateFunAddNew( )}\n" +
                $"{CreateFunUpdate( )}\n" +
                $"{CreateFunSave()}\n" +
                $"{CreateFunGetInfoByOffsetPaging()}\n" +
                $"\n\n}}" +
                $"\n\n}}";
        }

 

    }
     

   
}

 
