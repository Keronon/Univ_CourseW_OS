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
        public static async void Start()
        {
            #if DEBUG
            using (StreamWriter stream_writer = new StreamWriter(Data_Keeper.res_folder + "debug.log", false))
            {
                await stream_writer.WriteLineAsync("========== LOG START ==========");
            }
            #endif
        }

        public static async void Write(string _text)
        {
            #if DEBUG
            using (StreamWriter stream_writer = new StreamWriter(Data_Keeper.res_folder + "debug.log", true))
            {
                await stream_writer.WriteAsync(_text);
            }
            #endif
        }
    }
}
