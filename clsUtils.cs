using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjectCodGeneratorUtils
{
    public class clsUtils
    {
        public static string GetDataTypeCFormSQLDataType(string SQLDataType)
        {
            switch (SQLDataType.ToLower())
            {

                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "xml":
                    return "string";
                    break;


                case "int":
                    return "int?";

                case "bigint":
                    return "long?";

                case "smallint":
                    return "short?";

                case "tinyint":
                    return "byte?";

                case "bit":
                    return "bool?";

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    return "decimal?";

                case "float":
                    return "double?";

                case "real":
                    return "float?";


                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return "DateTime?";


                case "datetimeoffset":
                    return "DateTimeOffset?";

                case "time":
                    return "TimeSpan?";

                case "uniqueidentifier":
                    return "Guid?";


                case "binary":
                case "varbinary":
                case "image":
                case "timestamp":
                    return "byte[]";

                case "sql_variant":
                    return "object";

                default:
                    return "object";


            }
        }
         
        //public static SqlDbType GetSqlDbTypeFromSQLDataType(string SQLDataType)
        //{
        //    switch (SQLDataType.ToLower())
        //    {
        //        // نصوص
        //        case "char":
        //            return SqlDbType.Char;
        //        case "varchar":
        //            return SqlDbType.VarChar;
        //        case "nchar":
        //            return SqlDbType.NChar;
        //        case "nvarchar":
        //            return SqlDbType.NVarChar;
        //        case "text":
        //            return SqlDbType.Text;
        //        case "ntext":
        //            return SqlDbType.NText;
        //        case "xml":
        //            return SqlDbType.Xml;

        //        // أرقام صحيحة
        //        case "int":
        //            return SqlDbType.Int;
        //        case "bigint":
        //            return SqlDbType.BigInt;
        //        case "smallint":
        //            return SqlDbType.SmallInt;
        //        case "tinyint":
        //            return SqlDbType.TinyInt;

        //        // boolean
        //        case "bit":
        //            return SqlDbType.Bit;

        //        // decimal / float
        //        case "decimal":
        //        case "numeric":
        //            return SqlDbType.Decimal;
        //        case "money":
        //            return SqlDbType.Money;
        //        case "smallmoney":
        //            return SqlDbType.SmallMoney;
        //        case "float":
        //            return SqlDbType.Float;
        //        case "real":
        //            return SqlDbType.Real;

        //        // التاريخ و الوقت
        //        case "date":
        //            return SqlDbType.Date;
        //        case "datetime":
        //            return SqlDbType.DateTime;
        //        case "datetime2":
        //            return SqlDbType.DateTime2;
        //        case "smalldatetime":
        //            return SqlDbType.SmallDateTime;
        //        case "time":
        //            return SqlDbType.Time;
        //        case "datetimeoffset":
        //            return SqlDbType.DateTimeOffset;

        //        // Binary
        //        case "binary":
        //            return SqlDbType.Binary;
        //        case "varbinary":
        //            return SqlDbType.VarBinary;
        //        case "image":
        //            return SqlDbType.Image;
        //        case "timestamp":
        //            return SqlDbType.Timestamp;

        //        // أنواع خاصة
        //        case "uniqueidentifier":
        //            return SqlDbType.UniqueIdentifier;
        //        case "sql_variant":
        //            return SqlDbType.Variant;

        //        default:
        //            return SqlDbType.Variant; // fallback لأي نوع غير معروف
        //    }
        //}

        //public static string GetClrTypeNameFromSqlDbType(SqlDbType sqlDbType)
        //{
        //    switch (sqlDbType)
        //    {
        //        // نصوص
        //        case SqlDbType.Char:
        //        case SqlDbType.VarChar:
        //        case SqlDbType.NChar:
        //        case SqlDbType.NVarChar:
        //        case SqlDbType.Text:
        //        case SqlDbType.NText:
        //        case SqlDbType.Xml:
        //            return "string";

        //        // أرقام صحيحة
        //        case SqlDbType.Int:
        //            return "int";
        //        case SqlDbType.BigInt:
        //            return "long";
        //        case SqlDbType.SmallInt:
        //            return "short";
        //        case SqlDbType.TinyInt:
        //            return "byte";

        //        // Boolean
        //        case SqlDbType.Bit:
        //            return "bool";

        //        // Decimal / Float
        //        case SqlDbType.Decimal:
        //         case SqlDbType.Money:
        //        case SqlDbType.SmallMoney:
        //            return "decimal";

        //        case SqlDbType.Float:
        //            return "double";

        //        case SqlDbType.Real:
        //            return "float";

        //        // تاريخ ووقت
        //        case SqlDbType.Date:
        //        case SqlDbType.DateTime:
        //        case SqlDbType.DateTime2:
        //        case SqlDbType.SmallDateTime:
        //            return "DateTime";

        //        case SqlDbType.Time:
        //            return "TimeSpan";

        //        case SqlDbType.DateTimeOffset:
        //            return "DateTimeOffset";

        //        // Binary
        //        case SqlDbType.Binary:
        //        case SqlDbType.VarBinary:
        //        case SqlDbType.Image:
        //        case SqlDbType.Timestamp:
        //            return "byte[]";

        //        // أنواع خاصة
        //        case SqlDbType.UniqueIdentifier:
        //            return "Guid";

        //        case SqlDbType.Variant:
        //            return "object";

        //        default:
        //            return "object";
        //    }
        //}
    }
}
