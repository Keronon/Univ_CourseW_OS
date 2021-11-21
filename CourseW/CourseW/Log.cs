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
        private static StreamWriter writer = new StreamWriter(Data_Keeper.res_folder + "Log");

        public static async void Write(string text)
        {
            await Task.Run(() => writer.Write(text));
        }
    }
}
