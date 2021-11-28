using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class File_System // структура: суперблок (32B), битовая карта кластеров, битовая карта inode, массив inode, данные о пользователях, данные (начиная с корневого каталога)
    {
        #region VARIABLES

        public Super_Block super_block;

        #endregion VARIABLES

        #region FUNCTIONS

        /// <summary>
        /// Создаёт файл, соответствующий разрабатываемой файловой системе.
        /// Заполняет стартовые данные (суперблок, пользователь-администратор, корневой каталог).
        /// </summary>
        public static void Create(Super_Block _super_block, string _file_system_name = "File_System")
        {
            Log.Write("File_System | Creating\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.res_folder + _file_system_name, FileMode.OpenOrCreate)))
            {
                // superblock
                writer.Write(_super_block.file_system_type);
                writer.Write(_super_block.clusters_bitmap_size);
                writer.Write(_super_block.inode_bitmap_size);
                writer.Write(_super_block.inode_size);
                writer.Write(_super_block.inods_count);
                writer.Write(_super_block.available_inodes_count);
                writer.Write(_super_block.user_record_size);
                writer.Write(_super_block.max_users_count);
                writer.Write(_super_block.cur_users_count);
                writer.Write(_super_block.cluster_size_pow);
                writer.Write(_super_block.data_clusters_count);
                writer.Write(_super_block.available_clusters_count);
                // clusters bitmap
                writer.Write(true); writer.Write(true); // root  directory - cluster 1 - flag 11 - engaged
                writer.Write(true); writer.Write(true); // admin directory - cluster 2 - flag 11 - engaged
                // inode bitmap
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inode_bitmap), SeekOrigin.Begin);
                writer.Write(true); writer.Write(true); // root  directory - inode 1 - flag 11 - engaged
                writer.Write(true); writer.Write(true); // admin directory - inode 2 - flag 11 - engaged
                // inode mass
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inode_mass), SeekOrigin.Begin);
                // --- root directory
                writer.Write(true);  writer.Write(false);                      // atributes 0-1  - flag 10  - directory
                writer.Write(true);  writer.Write(true);  writer.Write(false); // atributes 2-4  - flag 110 - (rw-)
                writer.Write(false); writer.Write(false); writer.Write(false); // atributes 5-7  - flag 000 - (---)
                writer.Write(false); writer.Write(false); writer.Write(false); // atributes 8-10 - flag 000 - (---)
                writer.Write(false);                                           // hidden         - flag 0   - false
                writer.Write(true);                                            // system         - flag 1   - true
                writer.Write(false); writer.Write(false); writer.Write(false); // [reserved]
                writer.Write((byte)0);                                         // owner id       - 0        - system
                writer.Write((byte)0);                                         // owner group id - 0        - system
                writer.Write(1);                                               // first cluster  - 1
                writer.Write(1);                                               // file size / elements in dir - 1
                writer.Write((long)DateTime.Now.ToBinary());                   // creation date-time          - now
                writer.Write((long)DateTime.Now.ToBinary());                   // changing date-time          - now
                // --- admin directory
                writer.Write(true);  writer.Write(false);                      // atributes 0-1  - flag 10  - directory
                writer.Write(true);  writer.Write(true);  writer.Write(false); // atributes 2-4  - flag 110 - (rw-) owner
                writer.Write(false); writer.Write(false); writer.Write(false); // atributes 5-7  - flag 000 - (---) owner group
                writer.Write(false); writer.Write(false); writer.Write(false); // atributes 8-10 - flag 000 - (---) other
                writer.Write(false);                                           // hidden         - flag 0   - false
                writer.Write(true);                                            // system         - flag 1   - true
                writer.Write(false); writer.Write(false); writer.Write(false); // [reserved]
                writer.Write((byte)0);                                         // owner id       - 0        - system
                writer.Write((byte)0);                                         // owner group id - 0        - system
                writer.Write(2);                                               // first cluster  - 2
                writer.Write(0);                                               // file size / elements in dir - 0
                writer.Write((long)DateTime.Now.ToBinary());                   // creation date-time          - now
                writer.Write((long)DateTime.Now.ToBinary());                   // changing date-time          - now
                // users data
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.users_data), SeekOrigin.Begin);
                // --- admin
                writer.Write((byte)1);                                                                // id             - admin
                writer.Write((byte)1);                                                                // group id       - admins
                writer.Write(new char[10] { 'a', 'd', 'm', 'i', 'n', '\0', '\0', '\0', '\0', '\0' }); // login          - admin
                writer.Write("password".GetHashCode());                                               // password hash  - of "password"
                writer.Write(2);                                                                      // home dir inode - 2
                // data
                // --- root directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.users_data), SeekOrigin.Begin);
                writer.Write(0);       // next cluster - neither
                // --- --- first record - admin home directory
                writer.Write(2);       // inode pos    - 2
                writer.Write("admin"); // name         - admin
                // --- admin directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.users_data) + (1 /*(cluster pos) - 1*/ * (int)Math.Pow(2, _super_block.cluster_size_pow)), SeekOrigin.Begin);
                writer.Write(0);       // next cluster - neither
            }

            Log.Write("File_System | Created\n");
        }

        /// <summary>
        /// Загружает в приложение данные файловой системы.
        /// </summary>
        /// <param name="file_system_file">Имя файла файловой системы, который необходимо загрузить.</param>
        public File_System()
        {
            Log.Write("File_System | Initialization\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open)))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        super_block.file_system_type         = reader.ReadByte();
                        super_block.clusters_bitmap_size     = reader.ReadInt32();
                        super_block.inode_bitmap_size        = reader.ReadInt32();
                        super_block.inode_size               = reader.ReadByte();
                        super_block.inods_count              = reader.ReadInt32();
                        super_block.available_inodes_count   = reader.ReadInt32();
                        super_block.user_record_size         = reader.ReadInt32();
                        super_block.max_users_count          = reader.ReadInt16();
                        super_block.cur_users_count          = reader.ReadInt16();
                        super_block.cluster_size_pow         = reader.ReadByte();
                        super_block.data_clusters_count      = reader.ReadInt32();
                        super_block.available_clusters_count = reader.ReadInt32();

                        CONTROL_BITS_TYPE._cluster = 30;
                        CONTROL_BITS_TYPE._inode   = 30 + super_block.clusters_bitmap_size;
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

        #region User control functions

        public void Create_directory()
        {
            Log.Write("File_System | Creating directory\n");

        }

        public void Create_file()
        {
            Log.Write("File_System | Creating file\n");

        }

        public void Get_directory()
        {
            Log.Write("File_System | Getting directory\n");

        }

        public void Get_atributes()
        {
            Log.Write("File_System | Getting file\n");

        }

        public void Set_atributes()
        {
            Log.Write("File_System | Setting atributes\n");

        }

        public void Delete_directory()
        {
            Log.Write("File_System | Deleting directory\n");

        }

        public void Delete_file()
        {
            Log.Write("File_System | Deleting file\n");

        }

        public void Read_file()
        {
            Log.Write("File_System | Reading file\n");

        }

        public void Write_file()
        {
            Log.Write("File_System | Writing file\n");

        }

        public void Append_file()
        {
            Log.Write("File_System | Appending file\n");

        }

        public void Rename_directory()
        {
            Log.Write("File_System | Renaming directory\n");

        }

        public void Rename_file()
        {
            Log.Write("File_System | Renaming file\n");

        }

        public void Copy_directory()
        {
            Log.Write("File_System | Copying directory\n");

        }

        public void Copy_file()
        {
            Log.Write("File_System | Copying file\n");

        }

        #endregion User control functions

        #region System control functions

        public Control_Bits? Read_control_bits(int _offset, int _bits_pos)
        {
            Log.Write(String.Format($"File_System | Reading control bits at {0}, offset {1}\n", _bits_pos, _offset));

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(_offset + (_bits_pos - 1) * 2);
                        Control_Bits bits = new Control_Bits(reader.ReadBoolean(), reader.ReadBoolean());

                        Log.Write("File_System | Reading control bits SUCCESS\n");

                        return bits;
                    }
                    catch (Exception)
                    {
                        Log.Write("File_System | Reading control bits FAIL\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader at control bits FAILED\n");
            return null;
        }

        public void Write_control_bits(int _offset, int _bits_pos, Control_Bits _bits)
        {
            Log.Write(String.Format($"File_System | Writing control bits at {0}, offset {1}\n", _bits_pos, _offset));

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate)))
            {
                try
                {
                    writer.Seek(_offset + (_bits_pos - 1) * 2, SeekOrigin.Begin);

                    writer.Write(_bits[0]);
                    writer.Write(_bits[1]);

                    Log.Write("File_System | Writing control bits SUCCESS\n");
                }
                catch (Exception)
                {
                    Log.Write("File_System | Writing control bits FAIL\n");
                    return;
                }
            }
        }

        public Inode? Read_inode(int _inode_pos)
        {
            Log.Write(String.Format($"File_System | Reading inode at {0}\n", _inode_pos));

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.inode_mass) + (_inode_pos - 1) * super_block.inode_size);
                        Inode inode = new Inode();
                        inode.atributes = new bool[] { reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                       reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                       reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                       reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean()};
                        inode.owner_id = reader.ReadByte();
                        inode.owner_group_id = reader.ReadByte();
                        inode.first_cluster_pos = reader.ReadInt32();
                        inode.file_size = reader.ReadInt32();
                        inode.creation_date_time = reader.ReadInt64();
                        inode.changing_date_time = reader.ReadInt64();

                        Log.Write("File_System | Reading inode SUCCESS\n");

                        return inode;
                    }
                    catch (Exception)
                    {
                        Log.Write("File_System | Reading inode FAIL\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader at inodes FAILED\n");
            return null;
        }

        public void Write_inode(int _inode_pos, Inode _inode)
        {
            Log.Write(String.Format($"File_System | Writing inode at {0}\n", _inode_pos));

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate)))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.inode_mass) + (_inode_pos - 1) * super_block.inode_size, SeekOrigin.Begin);

                    foreach(bool atribute in _inode.atributes)
                        writer.Write(atribute);
                    writer.Write(_inode.owner_id);
                    writer.Write(_inode.owner_group_id);
                    writer.Write(_inode.first_cluster_pos);
                    writer.Write(_inode.file_size);
                    writer.Write(_inode.creation_date_time);
                    writer.Write(_inode.changing_date_time);

                    Log.Write("File_System | Writing inode SUCCESS\n");
                }
                catch (Exception)
                {
                    Log.Write("File_System | Writing inode FAIL\n");
                    return;
                }
            }
        }

        public User? Read_user(int _user_pos)
        {
            Log.Write(String.Format($"File_System | Reading user at {0}\n", _user_pos));

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.users_data) + (_user_pos - 1) * super_block.user_record_size);
                        User user = new User();
                        user.id             = reader.ReadByte();
                        user.group          = reader.ReadByte();
                        user.login          = reader.ReadChars(10);
                        user.password_hash  = reader.ReadInt32();
                        user.home_dir_inode = reader.ReadInt32();

                        Log.Write("File_System | Reading user SUCCESS\n");

                        return user;
                    }
                    catch (Exception)
                    {
                        Log.Write("File_System | Reading user FAIL\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader FAILED\n");
            return null;
        }

        public void Write_user(int _user_pos, User _user)
        {
            Log.Write(String.Format($"File_System | Writing inode at {0}\n", _user_pos));

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate)))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.users_data) + (_user_pos - 1) * super_block.user_record_size, SeekOrigin.Begin);

                    writer.Write(_user.id);
                    writer.Write(_user.group);
                    writer.Write(_user.login);
                    writer.Write(_user.password_hash);
                    writer.Write(_user.home_dir_inode);

                    Log.Write("File_System | Writing control bits SUCCESS\n");
                }
                catch (Exception)
                {
                    Log.Write("File_System | Writing control bits FAIL\n");
                    return;
                }
            }
        }

        #endregion System control functions

        #region Other functions

        public enum FILE_SYSTEM_STRUCTURE { super_block, clusters_bitmap, inode_bitmap, inode_mass, users_data, data };
        public static int Get_offset(Super_Block _super_block, FILE_SYSTEM_STRUCTURE _system_part)
        {
            switch (_system_part)
            {
                default:
                case FILE_SYSTEM_STRUCTURE.super_block:
                    return 0;
                case FILE_SYSTEM_STRUCTURE.clusters_bitmap:
                    return 32;
                case FILE_SYSTEM_STRUCTURE.inode_bitmap:
                    return 32 + _super_block.clusters_bitmap_size;
                case FILE_SYSTEM_STRUCTURE.inode_mass:
                    return 32 + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size;
                case FILE_SYSTEM_STRUCTURE.users_data:
                    return 32 + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size +
                                _super_block.inods_count          * _super_block.inode_size;
                case FILE_SYSTEM_STRUCTURE.data:
                    return 32 + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size +
                                _super_block.inods_count          * _super_block.inode_size +
                                _super_block.max_users_count      * _super_block.user_record_size;
            }
        }

        #endregion Other functions

        #endregion FUNCTIONS

        #region OTHER

        public struct Super_Block                  // 32
        {
            public byte  file_system_type;         // 1
            public int   clusters_bitmap_size;     // 4
            public int   inode_bitmap_size;        // 4
            public byte  inode_size;               // 1
            public int   inods_count;              // 4
            public int   available_inodes_count;   // 4
            public int   user_record_size;         // 4
            public short max_users_count;          // 2
            public short cur_users_count;          // 2
            public byte  cluster_size_pow;         // 1
            public int   data_clusters_count;      // 4
            public int   available_clusters_count; // 4

            public Super_Block(byte _file_system_type, int _clusters_bitmap_size, byte _cluster_size_pow,     int _data_clusters_count, int _available_clusters_count,
                                                       int _inode_bitmap_size,    byte _inode_size,       int _inods_count,         int _available_inodes_count,
                                                       int _user_record_size,     short _max_users_count, short _cur_users_count)
            {
                file_system_type         = _file_system_type;
                clusters_bitmap_size     = _clusters_bitmap_size;
                cluster_size_pow         = _cluster_size_pow;
                data_clusters_count      = _data_clusters_count;
                available_clusters_count = _available_clusters_count;
                inode_bitmap_size        = _inode_bitmap_size;
                inode_size               = _inode_size;
                inods_count              = _inods_count;
                available_inodes_count   = _available_inodes_count;
                user_record_size         = _user_record_size;
                max_users_count          = _max_users_count;
                cur_users_count          = _cur_users_count;
            }
        }

        public struct CONTROL_BITS_TYPE
        {
            public static int _cluster;
            public static int _inode;
        }
        public struct Control_Bits
        {
            public bool[] bits;
            public Control_Bits(bool _bit_1, bool _bit_2)
            {
                bits = new bool[] { _bit_1, _bit_2 };
            }
            public Control_Bits(int _bit_1, int _bit_2)
            {
                bits = new bool[] { Convert.ToBoolean(_bit_1), Convert.ToBoolean(_bit_2) };
            }
            public bool this[byte _index]
            {
                get => bits[_index];
                set => bits[_index] = value;
            }
            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < bits.Length; i++)
                {
                    yield return bits[i];
                }
            }
        }

        public struct Inode
        {
            public bool[] atributes; /* 2Б: 0-1  – тип записи (00 – свободна, 10 – каталог, 01 – файл),
                                         2-4   – чтение, запись, исполнение пользователем-владельцем,
                                         5-7   – чтение, запись, исполнение группой-владельцем,
                                         8-10  – чтение, запись, исполнение другими,
                                         11    – «скрытый»,
                                         12    – «системный»,
                                         13-15 – зарезервировано.*/

            public byte owner_id;
            public byte owner_group_id;
            public int  first_cluster_pos;
            public int  file_size;
            public long creation_date_time;
            public long changing_date_time;
        }

        public struct User
        {
            public byte   id;
            public byte   group;
            public char[] login; // 10
            public int    password_hash;
            public int    home_dir_inode;

            public override string ToString()
            {
                return new string(login);
            }
        }

        #endregion OTHER
    }
}
