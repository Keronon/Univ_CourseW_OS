using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class Log
    {
        private static StreamWriter writer = new StreamWriter(DataKeeper.res_folder + "Log.debug");

        public static void Write(string text)
        {
            writer.Write(text);
        }
    }
}
