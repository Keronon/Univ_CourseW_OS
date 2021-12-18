using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class Burst_Tracing
    {
        public static async void Start()
        {
            using (StreamWriter stream_writer = new StreamWriter(Data_Keeper.res_folder + "burst.tracing", false))
            {
                await stream_writer.WriteLineAsync("========== BURST TRACING START ==========");
            }
        }

        public static async void Write(string _text)
        {
            using (StreamWriter stream_writer = new StreamWriter(Data_Keeper.res_folder + "burst.tracing", true))
            {
                await stream_writer.WriteAsync(_text);
            }
        }
    }
}
