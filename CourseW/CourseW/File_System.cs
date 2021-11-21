using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class File_System
    {
        #region VARIABLES
        private List<bool> file_system;
        private Super_Block super_block;
        #endregion VARIABLES

        #region FUNCTIONS
        /// <summary>
        /// Создаёт файл, соответствующий разрабатываемой файловой системе.
        /// Заполняет стартовые данные (суперблок, пользователь-администратор, корневой каталог).
        /// </summary>
        public static void Create(byte  file_system_type,
                                  int   clasters_bitmap_size,
                                  int   inode_bitmap_size,
                                  byte  inode_size,
                                  int   inode_mass_size,
                                  int   available_inodes_count,
                                  int   user_record_size,
                                  short max_users_count,
                                  short cur_users_count,
                                  byte  cluster_size,
                                  int   data_clusters_count,
                                  int   available_clusters_count,
                                  string file_system_file = "File_System")
        {
            Log.Write("File_System | Creating\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.res_folder + file_system_file, FileMode.OpenOrCreate)))
            {
                writer.Write(file_system_type);
                writer.Write(clasters_bitmap_size);
                writer.Write(inode_bitmap_size);
                writer.Write(inode_size);
                writer.Write(inode_mass_size);
                writer.Write(available_inodes_count);
                writer.Write(user_record_size);
                writer.Write(max_users_count);
                writer.Write(cur_users_count);
                writer.Write(cluster_size);
                writer.Write(data_clusters_count);
                writer.Write(available_clusters_count);

                writer.Seek(clasters_bitmap_size +
                            inode_bitmap_size +
                            inode_mass_size +
                            user_record_size * max_users_count,
                                SeekOrigin.Current);
            }

            Log.Write("File_System | Created\n");
        }

        /// <summary>
        /// Загружает в приложение данные файловой системы.
        /// </summary>
        /// <param name="file_system_file">Имя файла файловой системы, который необходимо загрузить.</param>
        public File_System(string file_system_file)
        {
            Log.Write("File_System | Initialization\n");

            using (BinaryReader reader = new BinaryReader(File.Open(file_system_file, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    try
                    {
                        super_block.file_system_type         = reader.ReadByte();
                        super_block.clasters_bitmap_size     = reader.ReadInt32();
                        super_block.inode_bitmap_size        = reader.ReadInt32();
                        super_block.inode_size               = reader.ReadByte();
                        super_block.inode_mass_size          = reader.ReadInt32();
                        super_block.available_inodes_count   = reader.ReadInt32();
                        super_block.user_record_size         = reader.ReadInt32();
                        super_block.max_users_count          = reader.ReadInt16();
                        super_block.cur_users_count          = reader.ReadInt16();
                        super_block.cluster_size             = reader.ReadByte();
                        super_block.data_clusters_count      = reader.ReadInt32();
                        super_block.available_clusters_count = reader.ReadInt32();
                    }
                    catch (Exception)
                    {
                        Log.Write("File_System | Exception while reading file system\n");
                        return;
                    }
                }
            }

            Log.Write("File_System | Initialized\n");
        }

        #region Control functions
        public void Create_Directory()
        {

        }

        public void Create_File()
        {

        }

        public void Get_Directory()
        {

        }

        public void Get_Atributes()
        {

        }

        public void Set_Atributes()
        {

        }

        public void Delete_Directory()
        {

        }

        public void Delete_File()
        {

        }

        public void Read_File()
        {

        }

        public void Write_File()
        {

        }

        public void Append_File()
        {

        }

        public void Rename_Directory()
        {

        }

        public void Rename_File()
        {

        }

        public void Copy_Directory()
        {

        }

        public void Copy_File()
        {

        }
        #endregion Control functions
        #endregion FUNCTIONS

        #region OTHER
        public struct Super_Block                   // 32
        {
            public byte  file_system_type;         // 1
            public int   clasters_bitmap_size;     // 4
            public int   inode_bitmap_size;        // 4
            public byte  inode_size;               // 1
            public int   inode_mass_size;          // 4
            public int   available_inodes_count;   // 4
            public int   user_record_size;         // 4
            public short max_users_count;          // 2
            public short cur_users_count;          // 2
            public byte  cluster_size;             // 1
            public int   data_clusters_count;      // 4
            public int   available_clusters_count; // 4
        }
        #endregion OTHER
    }
}
