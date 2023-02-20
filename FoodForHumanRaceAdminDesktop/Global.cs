using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAndStaff
{
    public static class Global
    {
        private static string _globalVar = "http://192.168.0.190:3310/";

        public static string GlobalVar
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }
    }
}
