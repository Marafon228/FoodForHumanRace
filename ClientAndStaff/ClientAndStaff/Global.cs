using ClientAndStaff.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAndStaff
{
    public static class Global
    {
        private static string _globalVar = "http://192.168.0.14:3310/";

        public static string GlobalVar
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }

        private static User currentUser;

        public static User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        private static List<ManuProduct> _Mybasket = new List<ManuProduct>();

        public static List<ManuProduct> MyBasket
        {
            get { return _Mybasket; }
            set { _Mybasket = value; }
        }

    }
}
