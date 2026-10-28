using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class clsConnectionSting
    {
        public  string UserName { get; private set; }
        public  string Password { get; private set; }

        public   string ConnectionSting { get; private set; }
        public   string DatabaseName { get; private set; }

        public  clsConnectionSting(string UserName , string Password , string DatabaseName)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.DatabaseName = DatabaseName;
            ConnectionSting = $"server=.;database={DatabaseName};user id ={UserName};password={Password}";
        }

        //private string GetConnectionStingOnly(string UserName, string Password)
        //{
        //    this.UserName = UserName;
        //    this.Password = Password;

        //    return $"server=.;user id ={UserName};password={Password}";
        //}

        //private  string GetConnectionStingWithConnectionDatabase(string DatabaseName)
        //{
        //    this.DatabaseName = DatabaseName;
        //    return ConnectionSting = $"server=.;database={DatabaseName};user id ={UserName};password={Password}";

        //}

    }
    //public class clsConnectionSting
    //{
    //    static string _UserName;
    //    static string _Password;

    //    public static string ConnectionStingWithConnectionDatabase { get; private set; }
    //    public static string DatabaseName { get; private set; }


    //    public static string GetConnectionSting(string UserName, string Password)
    //    {
    //        _UserName = UserName;
    //        _Password = Password;

    //        return $"server=.;user id ={UserName};password={Password}";
    //    }

    //    public static string GetConnectionStingWithConnectionDatabase(string DatabaseName)
    //    {
    //        clsConnectionSting.DatabaseName = DatabaseName;
    //        return ConnectionStingWithConnectionDatabase = $"server=.;database={DatabaseName};user id ={_UserName};password={_Password}";

    //    }

    //}
}
