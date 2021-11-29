using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    class File_System // структура: суперблок (34B), битовая карта кластеров, битовая карта inode, массив inode, данные о пользователях, данные (начиная с корневого каталога)
    {
        #region VARIABLES

        public Super_Block super_block;
        private string cur_path = "/";

        #endregion VARIABLES

        #region FUNCTIONS

        /// <summary>
        /// Создаёт файл, соответствующий разрабатываемой файловой системе.
        /// Заполняет стартовые данные (суперблок, пользователь-администратор, корневой каталог).
        /// </summary>
        public static void Create(Super_Block _super_block, string _file_system_name = "File_System")
        {
            Log.Write("File_System | Creating\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.res_folder + _file_system_name, FileMode.OpenOrCreate), Encoding.Default))
            {
                // superblock
                writer.Write(_super_block.super_block_size);
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
                writer.Write(_super_block.directory_record_size);
                // clusters bitmap
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.clusters_bitmap), SeekOrigin.Begin);
                writer.Write(true); writer.Write(true); // root  directory - cluster 1 - flag 11 - engaged
                writer.Write(true); writer.Write(true); // admin directory - cluster 2 - flag 11 - engaged
                // inode bitmap
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inods_bitmap), SeekOrigin.Begin);
                writer.Write(true); writer.Write(true); // root  directory - inode 1 - flag 11 - engaged
                writer.Write(true); writer.Write(true); // admin directory - inode 2 - flag 11 - engaged
                // inode mass
                // --- root directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inode_mass), SeekOrigin.Begin);
                foreach (bool atribute in new bool[] { true,  false,          // atributes 0-1  - flag 10  - directory
                                                       true,  true,  false,   // atributes 2-4  - flag 110 - (rw-)
                                                       false, false, false,   // atributes 5-7  - flag 000 - (---)
                                                       false, false, false,   // atributes 8-10 - flag 000 - (---)
                                                       false,                 // hidden         - flag 0   - false
                                                       true,                  // system         - flag 1   - true
                                                       false, false, false }) // [reserved]
                    writer.Write(atribute);
                writer.Write((byte)0);                       // owner id       - 0          - system
                writer.Write((byte)0);                       // owner group id - 0          - system
                writer.Write(1);                             // first cluster  - 1
                writer.Write(1);                             // file size / elements in dir - 1
                writer.Write((long)DateTime.Now.ToBinary()); // creation date-time          - now
                writer.Write((long)DateTime.Now.ToBinary()); // changing date-time          - now
                // --- admin directory
                foreach (bool atribute in new bool[] { true,  false,          // atributes 0-1  - flag 10  - directory
                                                       true,  true,  false,   // atributes 2-4  - flag 110 - (rw-)
                                                       false, false, false,   // atributes 5-7  - flag 000 - (---)
                                                       false, false, false,   // atributes 8-10 - flag 000 - (---)
                                                       false,                 // hidden         - flag 0   - false
                                                       true,                  // system         - flag 1   - true
                                                       false, false, false }) // [reserved]
                    writer.Write(atribute);
                writer.Write((byte)0);                       // owner id       - 0          - system
                writer.Write((byte)0);                       // owner group id - 0          - system
                writer.Write(2);                             // first cluster  - 2
                writer.Write(0);                             // file size / elements in dir - 0
                writer.Write((long)DateTime.Now.ToBinary()); // creation date-time          - now
                writer.Write((long)DateTime.Now.ToBinary()); // changing date-time          - now
                // users data
                // --- admin
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.users_data), SeekOrigin.Begin); // id             - admin
                writer.Write((byte)1);                                                                     // group id       - admins
                writer.Write((byte)1);                                                                     // login          - admin
                writer.Write(new char[10] { 'a', 'd', 'm', 'i', 'n', '\0', '\0', '\0', '\0', '\0' });      // password hash  - of "password"
                writer.Write("password".GetHashCode());                                                    // home dir inode - 2
                writer.Write(2);
                // data
                // --- root directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.data), SeekOrigin.Begin);
                writer.Write(0);       // next cluster - neither
                // --- --- first record - admin home directory
                writer.Write(2);       // inode pos    - 2
                writer.Write("admin"); // name         - admin
                // --- admin directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.data) +
                                      (1 /*(cluster pos) - 1*/ * (int)Math.Pow(2, _super_block.cluster_size_pow)), SeekOrigin.Begin);
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

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.Default))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        super_block.super_block_size = reader.ReadByte();
                        super_block.file_system_type = reader.ReadByte();
                        super_block.clusters_bitmap_size = reader.ReadInt32();
                        super_block.inode_bitmap_size = reader.ReadInt32();
                        super_block.inode_size = reader.ReadByte();
                        super_block.inods_count = reader.ReadInt32();
                        super_block.available_inodes_count = reader.ReadInt32();
                        super_block.user_record_size = reader.ReadByte();
                        super_block.max_users_count = reader.ReadInt16();
                        super_block.cur_users_count = reader.ReadInt16();
                        super_block.cluster_size_pow = reader.ReadByte();
                        super_block.data_clusters_count = reader.ReadInt32();
                        super_block.available_clusters_count = reader.ReadInt32();
                        super_block.directory_record_size = reader.ReadByte();
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

        public void Set_atributes(string _path, bool[] _atributes)
        {
            Log.Write($"File_System | Setting atributes at {_path}\n");

            string search_name = _path.Substring(_path.LastIndexOf('/'));
            _path = _path.Remove(_path.LastIndexOf('/'));

            System_Directory directory = Get_directory(_path).Value;
            foreach (Directory_Record record in directory.records)
            {
                if (record.name == search_name)
                {
                    Inode inode = Read_inode(record.inode).Value;
                    inode.atributes = _atributes;
                    Write_inode(record.inode, inode);
                }
            }
        }

        public bool[] Get_atributes(string _path)
        {
            Log.Write($"File_System | Getting atributes at {_path}\n");

            string search_name = _path.Substring(_path.LastIndexOf('/'));
            _path = _path.Remove(_path.LastIndexOf('/'));

            System_Directory directory = Get_directory(_path).Value;
            foreach(Directory_Record record in directory.records)
            {
                if (record.name == search_name) return Read_inode(record.inode).Value.atributes;
            }
            return null;
        }

        public System_Directory? Get_directory(string _path)
        {
            Log.Write("File_System | Getting directory started\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            Cluster? cluster = Read_cluster(1);

            if (cluster == null) { return null; }

            System_Directory directory = new System_Directory(1, new List<Directory_Record>());
            for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
            {
                Directory_Record record = new Directory_Record();
                record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                if (record.inode == 0) continue;

                List<char> chars = new List<char>(Encoding.ASCII.GetChars(cluster.Value.data.GetRange(i + 4, super_block.directory_record_size - 4).ToArray()));
                chars = chars.GetRange(1, chars.IndexOf('\0') - 1);
                record.name = String.Join("", chars);

                if (path_parts.Count > 0 && record.name == path_parts[0]) return Get_directory(record.inode, path_parts);
                directory.records.Add(record);
            }

            return directory;
        }
        public System_Directory? Get_directory(int _inode, List<string> _path_parts)
        {
            Log.Write($"File_System | Getting directory at {_path_parts[0]}\n");

            Inode inode = Read_inode(_inode).Value;
            if (inode.atributes[0] != true && inode.atributes[1] != false) { return null; }

            Cluster? cluster = Read_cluster(Read_inode(_inode).Value.first_cluster_pos);
            if (cluster == null) { return null; }

            _path_parts.RemoveAt(0);
            System_Directory directory = new System_Directory(_inode, new List<Directory_Record>());
            for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
            {
                Directory_Record record = new Directory_Record();
                record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                if (record.inode == 0) continue;

                List<char> chars = new List<char>(Encoding.ASCII.GetChars(cluster.Value.data.GetRange(i + 4, super_block.directory_record_size - 4).ToArray()));
                chars = chars.GetRange(1, chars.IndexOf('\0') - 1);
                record.name = String.Join("", chars);

                if (_path_parts.Count > 0 && record.name == _path_parts[0]) return Get_directory(record.inode, _path_parts);
                directory.records.Add(record);
            }

            return directory;
        }

        public void Create_directory(string _path)
        {
            Log.Write("File_System | Creating directory\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Create_file(string _path)
        {
            Log.Write("File_System | Creating file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Delete_directory(string _path)
        {
            Log.Write("File_System | Deleting directory\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Delete_file(string _path)
        {
            Log.Write("File_System | Deleting file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Rename_directory(string _path)
        {
            Log.Write("File_System | Renaming directory\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Rename_file(string _path)
        {
            Log.Write("File_System | Renaming file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Copy_directory(string _path)
        {
            Log.Write("File_System | Copying directory\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Copy_file(string _path)
        {
            Log.Write("File_System | Copying file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Read_file(string _path)
        {
            Log.Write("File_System | Reading file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Write_file(string _path)
        {
            Log.Write("File_System | Writing file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Append_file(string _path)
        {
            Log.Write("File_System | Appending file\n");

            List<string> path_parts = new List<string>(_path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
        }

        #endregion User control functions

        #region System control functions

        public Control_Bits? Read_control_bits(int _offset, int _bits_pos)
        {
            Log.Write($"File_System | Reading control bits at {_bits_pos}, offset {_offset}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.Default))
            {
                if (reader.PeekChar() > -1)
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
            Log.Write($"File_System | Writing control bits at {_bits_pos}, offset {_offset}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.Default))
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
            Log.Write($"File_System | Reading inode at {_inode_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.Default))
            {
                if (reader.PeekChar() > -1)
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
            Log.Write($"File_System | Writing inode at {_inode_pos}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.Default))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.inode_mass) + (_inode_pos - 1) * super_block.inode_size, SeekOrigin.Begin);

                    foreach (bool atribute in _inode.atributes)
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
            Log.Write($"File_System | Reading user at {_user_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.Default))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.users_data) + (_user_pos - 1) * super_block.user_record_size);
                        User user = new User();
                        user.id = reader.ReadByte();
                        user.group = reader.ReadByte();
                        user.login = reader.ReadChars(10);
                        user.password_hash = reader.ReadInt32();
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
            Log.Write("File_System | Using reader at users FAILED\n");
            return null;
        }

        public void Write_user(int _user_pos, User _user)
        {
            Log.Write($"File_System | Writing user at {_user_pos}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.Default))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.users_data) + (_user_pos - 1) * super_block.user_record_size, SeekOrigin.Begin);

                    writer.Write(_user.id);
                    writer.Write(_user.group);
                    writer.Write(_user.login);
                    writer.Write(_user.password_hash);
                    writer.Write(_user.home_dir_inode);

                    Log.Write("File_System | Writing user SUCCESS\n");
                }
                catch (Exception)
                {
                    Log.Write("File_System | Writing user FAIL\n");
                    return;
                }
            }
        }

        public Cluster? Read_cluster(int _cluster_pos)
        {
            Log.Write($"File_System | Reading cluster at {_cluster_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.Default))
            {
                if (reader.PeekChar() > -1)
                {
                    reader.ReadChars(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.data) + (_cluster_pos - 1) * (int)Math.Pow(2, super_block.cluster_size_pow));
                }
                else
                {
                    return null;
                }
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < Math.Pow(2, super_block.cluster_size_pow) && reader.PeekChar() > -1; i++)
                {
                    try
                    {
                        bytes.Add(reader.ReadByte());
                    }
                    catch (Exception)
                    {
                        Log.Write("File_System | Reading cluster FAIL\n");
                        return null;
                    }
                }
                Log.Write("File_System | Reading cluster SUCCESS\n");
                return new Cluster(BitConverter.ToInt32(bytes.ToArray(), 0), bytes.GetRange(4, bytes.Count - 4));
            }
        }

        public void Write_cluster(int _cluster_pos, Cluster _cluster)
        {
            Log.Write($"File_System | Writing cluster at {_cluster}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.Default))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.data) + (_cluster_pos - 1) * (int)Math.Pow(2, super_block.cluster_size_pow),
                                    SeekOrigin.Begin);

                    writer.Write(_cluster.next_cluster);
                    writer.Write(_cluster.data.ToArray());

                    Log.Write("File_System | Writing cluster SUCCESS\n");
                }
                catch (Exception)
                {
                    Log.Write("File_System | Writing cluster FAIL\n");
                    return;
                }
            }
        }

        #endregion System control functions

        #region Other functions

        public enum FILE_SYSTEM_STRUCTURE { super_block, clusters_bitmap, inods_bitmap, inode_mass, users_data, data };
        public static int Get_offset(Super_Block _super_block, FILE_SYSTEM_STRUCTURE _system_part)
        {
            switch (_system_part)
            {
                default:
                case FILE_SYSTEM_STRUCTURE.super_block:
                    return 0;
                case FILE_SYSTEM_STRUCTURE.clusters_bitmap:
                    return _super_block.super_block_size;
                case FILE_SYSTEM_STRUCTURE.inods_bitmap:
                    return _super_block.super_block_size + _super_block.clusters_bitmap_size;
                case FILE_SYSTEM_STRUCTURE.inode_mass:
                    return _super_block.super_block_size + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size;
                case FILE_SYSTEM_STRUCTURE.users_data:
                    return _super_block.super_block_size + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size +
                                                           _super_block.inods_count * _super_block.inode_size;
                case FILE_SYSTEM_STRUCTURE.data:
                    return _super_block.super_block_size + _super_block.clusters_bitmap_size + _super_block.inode_bitmap_size +
                                                           _super_block.inods_count * _super_block.inode_size +
                                                           _super_block.max_users_count * _super_block.user_record_size;
            }
        }

        #endregion Other functions

        #endregion FUNCTIONS

        #region OTHER

        public struct Super_Block                  // 34
        {
            public byte  super_block_size;         // 1
            public byte  file_system_type;         // 1
            public int   clusters_bitmap_size;     // 4
            public int   inode_bitmap_size;        // 4
            public byte  inode_size;               // 1
            public int   inods_count;              // 4
            public int   available_inodes_count;   // 4
            public byte  user_record_size;         // 1
            public short max_users_count;          // 2
            public short cur_users_count;          // 2
            public byte  cluster_size_pow;         // 1
            public int   data_clusters_count;      // 4
            public int   available_clusters_count; // 4
            public byte  directory_record_size;    // 1

            public Super_Block(byte _super_block_size, byte _file_system_type,
                               int _clusters_bitmap_size, byte _cluster_size_pow, int _data_clusters_count, int _available_clusters_count,
                               int _inode_bitmap_size, byte _inode_size, int _inods_count, int _available_inodes_count,
                               byte _user_record_size, short _max_users_count, short _cur_users_count, byte _directory_record_size)
            {
                super_block_size = _super_block_size;
                file_system_type = _file_system_type;
                clusters_bitmap_size = _clusters_bitmap_size;
                cluster_size_pow = _cluster_size_pow;
                data_clusters_count = _data_clusters_count;
                available_clusters_count = _available_clusters_count;
                inode_bitmap_size = _inode_bitmap_size;
                inode_size = _inode_size;
                inods_count = _inods_count;
                available_inodes_count = _available_inodes_count;
                user_record_size = _user_record_size;
                max_users_count = _max_users_count;
                cur_users_count = _cur_users_count;
                directory_record_size = _directory_record_size;
            }
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
            public int first_cluster_pos;
            public int file_size;
            public long creation_date_time;
            public long changing_date_time;

            public Inode(bool[] _atributes, byte _owner_id, byte _owner_group_id,
                                            int _first_cluster_pos, int _file_size,
                                            long _creation_date_time, long _changing_date_time)
            {
                atributes = _atributes;
                owner_id = _owner_id;
                owner_group_id = _owner_group_id;
                first_cluster_pos = _first_cluster_pos;
                file_size = _file_size;
                creation_date_time = _creation_date_time;
                changing_date_time = _changing_date_time;
            }
        }

        public struct User
        {
            public byte id;
            public byte group;
            public char[] login; // 10
            public int password_hash;
            public int home_dir_inode;

            public User(byte _id, byte _group, char[] _login, int _password_hash, int _home_dir_inode)
            {
                id = _id;
                group = _group;
                login = _login;
                password_hash = _password_hash;
                home_dir_inode = _home_dir_inode;
            }

            public override string ToString()
            {
                return new string(login);
            }
        }

        public struct Cluster
        {
            public int next_cluster;
            public List<byte> data;

            public Cluster(int _next_cluster, List<byte> _data)
            {
                next_cluster = _next_cluster;
                data = _data;
            }
        }

        public struct Directory_Record
        {
            public int inode;
            public String name;

            public Directory_Record(int _inode, String _name)
            {
              inode = _inode;
              name = _name;
            }
        }

        public struct System_Directory
        {
            public int inode;
            public List<Directory_Record> records;

            public System_Directory(int _inode, List<Directory_Record> _records)
            {
                inode = _inode;
                records = _records;
            }

            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < records.Count; i++)
                {
                    yield return records[i];
                }
            }
        }

        public struct System_File
        {
            public int inode;
            public List<byte> data;

            public System_File(int _inode, List<byte> _data)
            {
                inode = _inode;
                data = _data;
            }
        }

        #endregion OTHER
    }
}
