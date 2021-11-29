using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class TEST_CLASS
    {
        public static void START()
        {
            File_System.System_Directory directory = Data_Keeper.file_system.Get_directory("/").Value;
            Console.WriteLine("=====");
            foreach(File_System.Directory_Record record in directory)
                Console.WriteLine($"{record.inode} {record.name}");
            Console.WriteLine("=====");
        }
    }
}
