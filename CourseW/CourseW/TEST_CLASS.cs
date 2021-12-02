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
            for (int i = 0; i < directory.records.Count; i++)
            {
                Console.WriteLine($"{directory.records[i].inode} {directory.records[i].name}");

                if (directory.records[i].name.IndexOf('[') >= 0)
                {
                    File_System.System_Directory in_directory = Data_Keeper.file_system.Get_directory($"/\\{directory.records[i].name}").Value;
                    for (int j = 0; j < in_directory.records.Count; i++)
                    {
                        Console.WriteLine($"\t{in_directory.records[i].inode} {in_directory.records[i].name}");
                    }
                }
            }
            Console.WriteLine("=====");
        }
    }
}
