using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseW
{
    public class File_System
    {
        #region VARIABLES

        public enum SUPERBLOCK_OFFSETS { available_inodes_count = 6, cur_users_count = 13, available_clusters_count = 20 };
        public enum FILE_SYSTEM_STRUCTURE { super_block, clusters_bitmap, inods_bitmap, inode_mass, users_data, data };
        public enum FILE_SYSTEM_OBJECT { directory, file };

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

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.res_folder + _file_system_name, FileMode.Create), Encoding.ASCII))
            {
                // superblock
                writer.Write(_super_block.super_block_size);
                writer.Write(_super_block.inode_size);
                writer.Write(_super_block.inods_count);
                writer.Write(_super_block.available_inodes_count);
                writer.Write(_super_block.user_record_size);
                writer.Write(_super_block.max_users_count);
                writer.Write(_super_block.cur_users_count);
                writer.Write(_super_block.cluster_size_pow);
                writer.Write(_super_block.clusters_count);
                writer.Write(_super_block.available_clusters_count);
                writer.Write(_super_block.directory_record_size);
                // clusters bitmap
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.clusters_bitmap), SeekOrigin.Begin);
                writer.Write(true); // root  directory - cluster 1 - flag 1 - engaged
                writer.Write(true); // admin directory - cluster 2 - flag 1 - engaged
                // inode bitmap
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inods_bitmap), SeekOrigin.Begin);
                writer.Write(true); // root  directory - inode 1 - flag 1 - engaged
                writer.Write(true); // admin directory - inode 2 - flag 1 - engaged
                // inode mass
                // --- root directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.inode_mass), SeekOrigin.Begin);
                foreach (bool atribute in new[] { true,  false,          // atributes 0-1  - flag 10  - directory
                                                  true,  true,  false,   // atributes 2-4  - flag 110 - (rw-) owner
                                                  false, false, false,   // atributes 5-7  - flag 000 - (---) group owner
                                                  false, false, false,   // atributes 8-10 - flag 000 - (---) other
                                                  false,                 // hidden         - flag 0   - false
                                                  true,                  // system         - flag 1   - true
                                                  false, false, false }) // [reserved]
                    writer.Write(atribute);
                writer.Write((byte)0);                 // owner id       - 0          - system
                writer.Write((byte)0);                 // owner group id - 0          - system
                writer.Write(1);                       // first cluster  - 1
                writer.Write(1);                       // file size / elements in dir - 1
                writer.Write(DateTime.Now.ToBinary()); // creation date-time          - now
                writer.Write(DateTime.Now.ToBinary()); // changing date-time          - now
                // --- admin directory
                foreach (bool atribute in new[] { true,  false,          // atributes 0-1  - flag 10  - directory
                                                  true,  true,  false,   // atributes 2-4  - flag 110 - (rw-) owner
                                                  false, false, false,   // atributes 5-7  - flag 000 - (---) group owner
                                                  false, false, false,   // atributes 8-10 - flag 000 - (---) other
                                                  false,                 // hidden         - flag 0   - false
                                                  true,                  // system         - flag 1   - true
                                                  false, false, false }) // [reserved]
                    writer.Write(atribute);
                writer.Write((byte)0);                       // owner id       - 0          - system
                writer.Write((byte)0);                       // owner group id - 0          - system
                writer.Write(2);                             // first cluster  - 2
                writer.Write(0);                             // file size / elements in dir - 0
                writer.Write(DateTime.Now.ToBinary()); // creation date-time          - now
                writer.Write(DateTime.Now.ToBinary()); // changing date-time          - now
                // users data
                // --- admin
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.users_data), SeekOrigin.Begin);
                writer.Write((byte)1);                                                                     // id             - admin
                writer.Write((byte)1);                                                                     // group id       - admins
                writer.Write(new char[10] { 'a', 'd', 'm', 'i', 'n', '\0', '\0', '\0', '\0', '\0' });      // login          - admin
                writer.Write("password".GetHashCode());                                                    // password hash  - of "password"
                writer.Write(2);                                                                           // home dir inode - 2
                // data
                // --- root directory
                writer.Seek(Get_offset(_super_block, FILE_SYSTEM_STRUCTURE.data), SeekOrigin.Begin);
                writer.Write(0);       // next cluster - neither
                // --- --- first record - admin home directory
                writer.Write(2);                                // inode pos    - 2
                writer.Write(Encoding.ASCII.GetBytes("admin")); // name         - admin
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

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.ASCII))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        super_block.super_block_size = reader.ReadByte();
                        super_block.inode_size = reader.ReadByte();
                        super_block.inods_count = reader.ReadInt32();
                        super_block.available_inodes_count = reader.ReadInt32();
                        super_block.user_record_size = reader.ReadByte();
                        super_block.max_users_count = reader.ReadInt16();
                        super_block.cur_users_count = reader.ReadInt16();
                        super_block.cluster_size_pow = reader.ReadByte();
                        super_block.clusters_count = reader.ReadInt32();
                        super_block.available_clusters_count = reader.ReadInt32();
                        super_block.directory_record_size = reader.ReadByte();
                    }
                    catch
                    {
                        Log.Write("File_System | Exception while reading file system\n");
                        return;
                    }
                }
            }

            Log.Write("File_System | Initialized\n");
        }

        #region User control functions

        public bool Set_atributes(string _path, bool[] _atributes)
        {
            Log.Write($"File_System | Setting atributes at {_path}\n");

            try
            {
                string search_name = _path.Substring(_path.LastIndexOf('\\') + 1);
                _path = _path.Remove(_path.LastIndexOf('\\'));

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
            catch
            {
                Log.Write("File_System | Setting atributes ERROR\n");
                return false;
            }

            Log.Write("File_System | Setting atributes SUCCESS\n");
            return true;
        }

        public bool[] Get_atributes(string _path)
        {
            Log.Write($"File_System | Getting atributes at {_path}\n");

            if (_path == "/") return Read_inode(1).Value.atributes;

            string search_name = _path.Substring(_path.LastIndexOf('\\') + 1);
            _path = _path.Remove(_path.LastIndexOf('\\'));

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

            List<string> path_parts = new List<string>(_path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries));
            Cluster? cluster = Read_cluster(1);
            if (cluster == null) return null;

            System_Directory directory = new System_Directory() { inode = 1, records = new List<Directory_Record>() };

            bool runner = true;
            while (runner)
            {
                for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                {
                    Directory_Record record = new Directory_Record();
                    record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                    if (record.inode == 0) continue;

                    List<char> chars = new List<char>(Encoding.ASCII.GetChars(cluster.Value.data.GetRange(i + 4, super_block.directory_record_size - 4).ToArray()));
                    chars = chars.GetRange(0, chars.IndexOf('\0'));
                    record.name = String.Join("", chars);

                    if (path_parts.Count > 1 && record.name == path_parts[1]) return Get_directory(record.inode, path_parts);
                    directory.records.Add(record);
                }
                if (cluster.Value.next_cluster != 0)
                {
                    cluster = Read_cluster(cluster.Value.next_cluster);
                    if (cluster == null) return null;
                }
                else runner = false;
            }

            return directory;
        }
        private System_Directory? Get_directory(int _inode, List<string> _path_parts)
        {
            Log.Write($"File_System | Getting directory at {_path_parts[1]}\n");

            Inode inode = Read_inode(_inode).Value;
            if (inode.atributes[0] != true && inode.atributes[1] != false) { return null; }

            Cluster? cluster = Read_cluster(Read_inode(_inode).Value.first_cluster_pos);
            if (cluster == null) return null;

            _path_parts.RemoveAt(1);
            System_Directory directory = new System_Directory() { inode = _inode, records = new List<Directory_Record>() };

            bool runner = true;
            while (runner)
            {
                for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                {
                    Directory_Record record = new Directory_Record();
                    record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                    if (record.inode == 0) continue;

                    List<char> chars = new List<char>(Encoding.ASCII.GetChars(cluster.Value.data.GetRange(i + 4, super_block.directory_record_size - 4).ToArray()));
                    chars = chars.GetRange(0, chars.IndexOf('\0'));
                    record.name = String.Join("", chars);

                    if (_path_parts.Count > 1 && record.name == _path_parts[1]) return Get_directory(record.inode, _path_parts);
                    directory.records.Add(record);
                }
                if (cluster.Value.next_cluster != 0)
                {
                    cluster = Read_cluster(cluster.Value.next_cluster);
                    if (cluster == null) return null;
                }
                else runner = false;
            }

            return directory;
        }

        public bool Create_system_object(string _path, FILE_SYSTEM_OBJECT _object, byte _owner_id = 0, byte _owner_group_id = 0)
        {
            Log.Write($"File_System | Creating file system object type {_object}\n");

            try
            {
                if (Search_record_by_name(_path) != null) throw new Exception();

                string creation_name = _path.Substring(_path.LastIndexOf('\\') + 1);
                _path = _path.Remove(_path.LastIndexOf('\\'));

                System_Directory directory = Get_directory(_path).Value;
                Inode inode = Read_inode(directory.inode).Value;
                inode.size++;
                inode.changing_date_time = DateTime.Now.ToBinary();
                Write_inode(directory.inode, inode);

                int dir_cluster_pos = inode.first_cluster_pos;
                Cluster? cluster = Read_cluster(dir_cluster_pos);
                if (cluster == null) throw new Exception();

                if (super_block.available_clusters_count == 0 || super_block.available_inodes_count == 0) throw new Exception();

                int cluster_pos = Search_available_control_bit(FILE_SYSTEM_STRUCTURE.clusters_bitmap);
                if (cluster_pos != 0)
                {
                    Write_control_bits(FILE_SYSTEM_STRUCTURE.clusters_bitmap, cluster_pos, true);
                    Write_cluster(cluster_pos, new Cluster() { next_cluster = 0, data = new List<byte>() });
                }
                else throw new Exception();

                int inode_pos = Search_available_control_bit(FILE_SYSTEM_STRUCTURE.inods_bitmap);
                if (inode_pos != 0)
                {
                    Write_control_bits(FILE_SYSTEM_STRUCTURE.inods_bitmap, inode_pos, true);
                    bool[] atributes = null;
                    switch (_object)
                    {
                        case FILE_SYSTEM_OBJECT.directory:
                            atributes = new[] { true,  false,          // atributes 0-1  - flag 10  - directory
                                                true,  true,  false,   // atributes 2-4  - flag 110 - (rw-) owner
                                                true,  true,  false,   // atributes 5-7  - flag 110 - (rw-) group owner
                                                true,  false, false,   // atributes 8-10 - flag 100 - (r--) other
                                                false,                 // hidden         - flag 0   - false
                               _owner_id == 0 ? true : false,          // system         - flag 0   - false
                                                false, false, false }; // [reserved]
                            break;
                        case FILE_SYSTEM_OBJECT.file:
                            atributes = new[] { false, true,           // atributes 0-1  - flag 01  - file
                                                true,  true,  true,    // atributes 2-4  - flag 111 - (rwe) owner
                                                true,  true,  true,    // atributes 5-7  - flag 111 - (rwe) group owner
                                                true,  true,  false,   // atributes 8-10 - flag 110 - (rw-) other
                                                false,                 // hidden         - flag 0   - false
                               _owner_id == 0 ? true : false,          // system         - flag 0   - false
                                                false, false, false }; // [reserved]
                            break;
                    }
                    Write_inode(inode_pos, new Inode() { atributes = atributes,
                                                         owner_id = _owner_id,                            // owner id       - current user
                                                         owner_group_id = _owner_group_id,                // owner group id - 0            - system
                                                         first_cluster_pos = cluster_pos,                 // first cluster  - saved value
                                                         size = 0,                                        // file size / elements in dir - 0
                                                         creation_date_time = DateTime.Now.ToBinary(),    // creation date-time          - now
                                                         changing_date_time = DateTime.Now.ToBinary() }); // changing date-time          - now
                }
                else throw new Exception();

                bool runner = true;
                while (runner)
                {
                    if (cluster.Value.data.Count == 0)
                    {
                        cluster.Value.data.InsertRange(0, BitConverter.GetBytes(inode_pos).Concat(Encoding.ASCII.GetBytes(creation_name)));
                        Write_cluster(dir_cluster_pos, cluster.Value);
                        break;
                    }
                    for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                    {
                        Directory_Record record = new Directory_Record();
                        record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                        if (record.inode == 0)
                        {
                            if (((cluster.Value.data.Count - i) / super_block.directory_record_size) < 1) break;

                            cluster.Value.data.RemoveRange(i, super_block.directory_record_size);
                            int bytes = super_block.directory_record_size - BitConverter.GetBytes(inode_pos).Concat(Encoding.ASCII.GetBytes(creation_name)).ToList().Count;
                            cluster.Value.data.InsertRange(i, BitConverter.GetBytes(inode_pos).Concat(Encoding.ASCII.GetBytes(creation_name)).Concat(new byte[bytes]));
                            Write_cluster(dir_cluster_pos, cluster.Value);
                            runner = false;
                            break;
                        }
                    }
                    if (runner == false) break;
                    if (cluster.Value.next_cluster != 0)
                    {
                        dir_cluster_pos = cluster.Value.next_cluster;
                        cluster = Read_cluster(dir_cluster_pos);
                        if (cluster == null) throw new Exception();
                    }
                    else
                    {
                        int available_cluster = Search_available_control_bit(FILE_SYSTEM_STRUCTURE.clusters_bitmap);
                        if (available_cluster == 0) throw new Exception();
                        Cluster written_cluster = cluster.Value;
                        written_cluster.next_cluster = available_cluster;
                        Write_cluster(dir_cluster_pos, written_cluster);

                        written_cluster.next_cluster = 0;
                        written_cluster.data.Clear();
                        written_cluster.data.AddRange(BitConverter.GetBytes(inode_pos).Concat(Encoding.ASCII.GetBytes(creation_name)));
                        Write_cluster(available_cluster, written_cluster);
                        Write_control_bits(FILE_SYSTEM_STRUCTURE.clusters_bitmap, available_cluster, true);

                        runner = false;
                    }
                }
            }
            catch
            {
                Log.Write("File_System | Creating file system object ERROR\n");
                return false;
            }

            Log.Write("File_System | Creating file system object SUCCESS\n");
            return true;
        }

        public bool Delete_system_object(string _path)
        {
            Log.Write($"File_System | Deleting object \"{_path.Substring(_path.LastIndexOf('\\') + 1)}\"\n");

            try
            {
                Directory_Record? cur_record = Search_record_by_name(_path);
                if (cur_record != null)
                {
                    Inode inode = Read_inode(cur_record.Value.inode).Value;

                    if (inode.atributes[0] == true && inode.atributes[1] == false)
                    {
                        foreach (Directory_Record record in Get_directory(_path).Value.records)
                        {
                            if (!Delete_system_object($"{_path}\\{record.name}")) throw new Exception();
                        }
                    }

                    System_Directory directory = Get_directory(_path.Substring(0, _path.LastIndexOf('\\'))).Value;
                    Inode parent_inode = Read_inode(directory.inode).Value;
                    parent_inode.size--;
                    parent_inode.changing_date_time = DateTime.Now.ToBinary();
                    Write_inode(directory.inode, parent_inode);

                    int dir_cluster_pos = parent_inode.first_cluster_pos;
                    Cluster? cluster = Read_cluster(dir_cluster_pos);
                    if (cluster == null) throw new Exception();
                    do
                    {
                        for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                        {
                            Directory_Record record = new Directory_Record();
                            record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                            if (record.inode == cur_record.Value.inode)
                            {
                                cluster.Value.data.RemoveRange(i, 4);
                                cluster.Value.data.InsertRange(i, new List<byte>() { 0, 0, 0, 0 });
                                Write_cluster(dir_cluster_pos, cluster.Value);
                                break;
                            }
                        }

                        if (cluster.Value.next_cluster == 0) break;

                        dir_cluster_pos = cluster.Value.next_cluster;
                        cluster = Read_cluster(dir_cluster_pos);
                        if (cluster == null) throw new Exception();
                    }
                    while (true);

                    Clear_cluster_line(inode.first_cluster_pos);
                    Write_control_bits(FILE_SYSTEM_STRUCTURE.inods_bitmap, cur_record.Value.inode, false);
                }
                else throw new Exception();
            }
            catch
            {
                Log.Write("File_System | Deleting file system object ERROR\n");
                return false;
            }

            Log.Write("File_System | Deleting file system object SUCCESS\n");
            return true;
        }
        private void Clear_cluster_line(int cluster_pos)
        {
            Cluster cluster = Read_cluster(cluster_pos).Value;
            if (cluster.next_cluster != 0) Clear_cluster_line(cluster.next_cluster);
            cluster.next_cluster = 0; cluster.data.Clear();
            Write_cluster(cluster_pos, cluster);
            Write_control_bits(FILE_SYSTEM_STRUCTURE.clusters_bitmap, cluster_pos, false);
        }

        public bool Rename_system_object(string _object_path, string _name)
        {
            Log.Write("File_System | Rename file\n");

            try
            {
                Directory_Record? cur_record = Search_record_by_name(_object_path);

                System_Directory directory = Get_directory(_object_path.Substring(0, _object_path.LastIndexOf('\\'))).Value;
                Inode parent_inode = Read_inode(directory.inode).Value;

                int dir_cluster_pos = parent_inode.first_cluster_pos;
                Cluster? cluster = Read_cluster(dir_cluster_pos);
                if (cluster == null) throw new Exception();
                do
                {
                    for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                    {
                        Directory_Record record = new Directory_Record();
                        record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                        if (record.inode == cur_record.Value.inode)
                        {
                            cluster.Value.data.RemoveRange(i + 4, super_block.directory_record_size - 4);
                            List<byte> bytes = Encoding.ASCII.GetBytes(_name).ToList();
                            bytes.AddRange(new byte[super_block.directory_record_size - bytes.Count - 4]);
                            cluster.Value.data.InsertRange(i + 4, bytes);
                            Write_cluster(dir_cluster_pos, cluster.Value);
                            break;
                        }
                    }

                    if (cluster.Value.next_cluster == 0) break;

                    dir_cluster_pos = cluster.Value.next_cluster;
                    cluster = Read_cluster(dir_cluster_pos);
                    if (cluster == null) throw new Exception();
                }
                while (true);
            }
            catch
            {
                Log.Write("File_System | Renaming file ERROR\n");
                return false;
            }

            Log.Write("File_System | Renaming file SUCCESS\n");
            return true;
        }

        public bool Copy_system_object(string _object_path, string _target_path)
        {
            Log.Write("File_System | Copying file\n");

            try
            {
                string target = $"{_target_path}\\{_object_path.Substring(_object_path.LastIndexOf('\\') + 1)}";

                Inode inode = Read_inode(Search_record_by_name(_object_path).Value.inode).Value;
                if (inode.atributes[0] == false && inode.atributes[1] == true)
                {
                    System_File file = Read_file(_object_path).Value;

                    if (!Create_system_object(target, FILE_SYSTEM_OBJECT.file)) throw new Exception();
                    if (!Write_file(target, file.data)) throw new Exception();
                }
                else
                {
                    if (!Create_system_object(target, FILE_SYSTEM_OBJECT.directory)) throw new Exception();

                    System_Directory directory = Get_directory(_object_path).Value;
                    foreach (Directory_Record record in directory.records)
                    {
                        Copy_system_object($"{_object_path}\\{record.name}", target);
                    }
                }
            }
            catch
            {
                Log.Write("File_System | Copying file ERROR\n");
                return false;
            }

            Log.Write("File_System | Copying file SUCCESS\n");
            return true;
        }

        public System_File? Read_file(string _path)
        {
            Log.Write("File_System | Reading file\n");

            Cluster file_cluster = new Cluster();
            System_File file = new System_File() { inode = 0, data = new List<byte>() };

            Directory_Record? record = Search_record_by_name(_path);
            if (record != null)
            {
                file.inode = record.Value.inode;
                file_cluster = Read_cluster(Read_inode(file.inode).Value.first_cluster_pos).Value;
            }
            else return null;

            while (true)
            {
                file.data.AddRange(file_cluster.data);
                if (file_cluster.next_cluster != 0) file_cluster = Read_cluster(file_cluster.next_cluster).Value;
                else break;
            }

            return file;
        }

        public bool Write_file(string _path, List<byte> _write_bytes)
        {
            Log.Write("File_System | Writing file\n");
            try
            {
                string write_name = _path.Substring(_path.LastIndexOf('\\') + 1);
                _path = _path.Remove(_path.LastIndexOf('\\'));

                if (!Delete_system_object($"{_path}\\{write_name}")) throw new Exception();
                if (!Create_system_object($"{_path}\\{write_name}", FILE_SYSTEM_OBJECT.file)) throw new Exception();

                Directory_Record? record = Search_record_by_name($"{_path}\\{write_name}");
                if (record == null) throw new Exception();
                Inode inode = Read_inode(record.Value.inode).Value;

                inode.size = _write_bytes.Count;
                inode.changing_date_time = DateTime.Now.ToBinary();
                Write_inode(record.Value.inode, inode);

                int cluster_pos = inode.first_cluster_pos;

                List<List<byte>> clusters_data = new List<List<byte>>();
                int data_size = ((int)Math.Pow(2, super_block.cluster_size_pow)) - 4;
                int clusters_count = _write_bytes.Count / data_size;
                int i = 0;
                for ( ; i < clusters_count; i++)
                {
                    clusters_data.Add(new List<byte>(_write_bytes.GetRange(i * data_size, data_size)));
                }
                if (_write_bytes.Count % data_size > 0) clusters_data.Add(new List<byte>(_write_bytes.GetRange(i * data_size, _write_bytes.Count % data_size)));

                for (i = 0; i < clusters_data.Count; i++)
                {
                    if (i == clusters_data.Count - 1) Write_cluster(cluster_pos, new Cluster() { next_cluster = 0, data = clusters_data[i] });
                    else
                    {
                        int available_cluster = Search_available_control_bit(FILE_SYSTEM_STRUCTURE.clusters_bitmap);
                        if (available_cluster == 0) throw new Exception();
                        Write_cluster(cluster_pos, new Cluster() { next_cluster = available_cluster, data = clusters_data[i] });
                        Write_control_bits(FILE_SYSTEM_STRUCTURE.clusters_bitmap, available_cluster, true);
                        cluster_pos = available_cluster;
                    }
                }
            }
            catch
            {
                Log.Write("File_System | Writing file ERROR\n");
                return false;
            }

            Log.Write("File_System | Writing file SUCCESS\n");
            return true;
        }

        public bool Append_file(string _path, List<char> _append_chars)
        {
            Log.Write("File_System | Appending file\n");
            try
            {
                System_File file = Read_file(_path).Value;
                string old_part = new string(Encoding.ASCII.GetChars(file.data.ToArray())).Trim('\0');
                old_part += new string(_append_chars.ToArray());
                if (!Write_file(_path, Encoding.ASCII.GetBytes(old_part.ToArray()).ToList())) throw new Exception();
            }
            catch
            {
                Log.Write("File_System | Appending file ERROR\n");
                return false;
            }

            Log.Write("File_System | Appending file SUCCESS\n");
            return true;
        }

        #endregion User control functions

        #region System control functions

        public bool? Read_control_bits(FILE_SYSTEM_STRUCTURE _file_system_part, int _bit_pos)
        {
            Log.Write($"File_System | Reading control bits at {_bit_pos}, file system part {_file_system_part}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.ASCII))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(Get_offset(super_block, _file_system_part) + _bit_pos - 1);
                        bool bit = reader.ReadBoolean();

                        Log.Write("File_System | Reading control bits SUCCESS\n");

                        return bit;
                    }
                    catch
                    {
                        Log.Write("File_System | Reading control bits ERROR\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader at control bits ERROR\n");
            return null;
        }

        public bool Write_control_bits(FILE_SYSTEM_STRUCTURE _file_system_part, int _bit_pos, bool _bit)
        {
            Log.Write($"File_System | Writing control bits at {_bit_pos}, file system part {_file_system_part}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.ASCII))
            {
                try
                {
                    switch(_file_system_part)
                    {
                        case FILE_SYSTEM_STRUCTURE.clusters_bitmap:
                            {
                                writer.Seek((int)SUPERBLOCK_OFFSETS.available_clusters_count, SeekOrigin.Begin);
                                if (_bit == true)
                                {
                                    if (super_block.available_clusters_count == 0) throw new Exception();
                                    writer.Write(--super_block.available_clusters_count);
                                }
                                else if (_bit == false) writer.Write(++super_block.available_clusters_count);
                            } break;

                        case FILE_SYSTEM_STRUCTURE.inods_bitmap:
                            {
                                writer.Seek((int)SUPERBLOCK_OFFSETS.available_inodes_count, SeekOrigin.Begin);
                                if (_bit == true)
                                {
                                    if (super_block.available_inodes_count == 0) throw new Exception();
                                    writer.Write(--super_block.available_inodes_count);
                                }
                                else if (_bit == false) writer.Write(++super_block.available_inodes_count);
                            } break;
                    }

                    writer.Seek(Get_offset(super_block, _file_system_part) + _bit_pos - 1, SeekOrigin.Begin);

                    writer.Write(_bit);

                    Log.Write("File_System | Writing control bits SUCCESS\n");
                    return true;
                }
                catch
                {
                    Log.Write("File_System | Writing control bits ERROR\n");
                    return false;
                }
            }
        }

        public Inode? Read_inode(int _inode_pos)
        {
            Log.Write($"File_System | Reading inode at {_inode_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.ASCII))
            {
                if (reader.PeekChar() > -1)
                {
                    try
                    {
                        reader.ReadChars(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.inode_mass) + (_inode_pos - 1) * super_block.inode_size);
                        Inode inode = new Inode();
                        inode.atributes = new[] { reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                  reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                  reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(),
                                                  reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean()};
                        inode.owner_id = reader.ReadByte();
                        inode.owner_group_id = reader.ReadByte();
                        inode.first_cluster_pos = reader.ReadInt32();
                        inode.size = reader.ReadInt32();
                        inode.creation_date_time = reader.ReadInt64();
                        inode.changing_date_time = reader.ReadInt64();

                        Log.Write("File_System | Reading inode SUCCESS\n");

                        return inode;
                    }
                    catch
                    {
                        Log.Write("File_System | Reading inode ERROR\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader at inodes ERROR\n");
            return null;
        }

        public bool Write_inode(int _inode_pos, Inode _inode)
        {
            Log.Write($"File_System | Writing inode at {_inode_pos}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.ASCII))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.inode_mass) + (_inode_pos - 1) * super_block.inode_size, SeekOrigin.Begin);

                    foreach (bool atribute in _inode.atributes)
                        writer.Write(atribute);
                    writer.Write(_inode.owner_id);
                    writer.Write(_inode.owner_group_id);
                    writer.Write(_inode.first_cluster_pos);
                    writer.Write(_inode.size);
                    writer.Write(_inode.creation_date_time);
                    writer.Write(_inode.changing_date_time);

                    Log.Write("File_System | Writing inode SUCCESS\n");
                    return true;
                }
                catch
                {
                    Log.Write("File_System | Writing inode ERROR\n");
                    return false;
                }
            }
        }

        public User? Read_user(int _user_pos)
        {
            Log.Write($"File_System | Reading user at {_user_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.ASCII))
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
                    catch
                    {
                        Log.Write("File_System | Reading user ERROR\n");
                        return null;
                    }
                }
            }
            Log.Write("File_System | Using reader at users ERROR\n");
            return null;
        }

        public bool Write_user(int _user_pos, User _user)
        {
            Log.Write($"File_System | Writing user at {_user_pos}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.ASCII))
            {
                try
                {
                    writer.Seek(13, SeekOrigin.Begin);
                    if (_user.id > 0)
                    {
                        if (super_block.cur_users_count == super_block.max_users_count) throw new Exception();
                        writer.Write(++super_block.cur_users_count);
                    }
                    else if (_user.id == 0) writer.Write(--super_block.cur_users_count);

                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.users_data) + (_user_pos - 1) * super_block.user_record_size, SeekOrigin.Begin);

                    writer.Write(_user.id);
                    writer.Write(_user.group);
                    writer.Write(_user.login);
                    writer.Write(_user.password_hash);
                    writer.Write(_user.home_dir_inode);

                    Log.Write("File_System | Writing user SUCCESS\n");
                    return true;
                }
                catch
                {
                    Log.Write("File_System | Writing user ERROR\n");
                    return false;
                }
            }
        }

        public Cluster? Read_cluster(int _cluster_pos)
        {
            Log.Write($"File_System | Reading cluster at {_cluster_pos}\n");

            using (BinaryReader reader = new BinaryReader(File.Open(Data_Keeper.file_system_path, FileMode.Open), Encoding.ASCII))
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
                for (int i = 0; i < (int)Math.Pow(2, super_block.cluster_size_pow) && reader.PeekChar() > -1; i++)
                {
                    try
                    {
                        bytes.Add(reader.ReadByte());
                    }
                    catch
                    {
                        Log.Write("File_System | Reading cluster ERROR\n");
                        return null;
                    }
                }
                Log.Write("File_System | Reading cluster SUCCESS\n");
                return new Cluster() { next_cluster = BitConverter.ToInt32(bytes.ToArray(), 0), data = bytes.GetRange(4, bytes.Count - 4) };
            }
        }

        public bool Write_cluster(int _cluster_pos, Cluster _cluster)
        {
            Log.Write($"File_System | Writing cluster at {_cluster_pos}\n");

            using (BinaryWriter writer = new BinaryWriter(File.Open(Data_Keeper.file_system_path, FileMode.OpenOrCreate), Encoding.ASCII))
            {
                try
                {
                    writer.Seek(Get_offset(super_block, FILE_SYSTEM_STRUCTURE.data) + (_cluster_pos - 1) * (int)Math.Pow(2, super_block.cluster_size_pow),
                                    SeekOrigin.Begin);

                    writer.Write(_cluster.next_cluster);

                    while (_cluster.data.Count != (int)Math.Pow(2, super_block.cluster_size_pow) - 4)
                    {
                        _cluster.data.Add((byte)0);
                    }
                    writer.Write(_cluster.data.ToArray());

                    Log.Write("File_System | Writing cluster SUCCESS\n");
                    return true;
                }
                catch
                {
                    Log.Write("File_System | Writing cluster ERROR\n");
                    return false;
                }
            }
        }

        #endregion System control functions

        #region Other functions

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
                    return _super_block.super_block_size + _super_block.clusters_count;
                case FILE_SYSTEM_STRUCTURE.inode_mass:
                    return _super_block.super_block_size + _super_block.clusters_count + _super_block.inods_count;
                case FILE_SYSTEM_STRUCTURE.users_data:
                    return _super_block.super_block_size + _super_block.clusters_count + _super_block.inods_count +
                                                           _super_block.inods_count * _super_block.inode_size;
                case FILE_SYSTEM_STRUCTURE.data:
                    return _super_block.super_block_size + _super_block.clusters_count + _super_block.inods_count +
                                                           _super_block.inods_count * _super_block.inode_size +
                                                           _super_block.max_users_count * _super_block.user_record_size;
            }
        }

        public int Search_available_control_bit(FILE_SYSTEM_STRUCTURE _system_part)
        {
            int bitmap_size = 0;
            switch (_system_part)
            {
                case FILE_SYSTEM_STRUCTURE.clusters_bitmap: bitmap_size = super_block.clusters_count; break;
                case FILE_SYSTEM_STRUCTURE.inods_bitmap: bitmap_size = super_block.inods_count; break;
            }

            for (int i = 1; i <= bitmap_size; i++)
            {
                bool? bit = Read_control_bits(_system_part, i);
                if (bit == null) return 0;
                if (bit.Value == false) return i;
            }

            return 0;
        }

        public Directory_Record? Search_record_by_name(string _path)
        {
            if (_path == "/") return new Directory_Record() { inode = 1, name = _path };

            string record_name = _path.Substring(_path.LastIndexOf('\\') + 1);
            _path = _path.Remove(_path.LastIndexOf('\\'));

            System_Directory directory = Get_directory(_path).Value;
            Inode inode = Read_inode(directory.inode).Value;
            Cluster? cluster = Read_cluster(inode.first_cluster_pos);
            if (cluster == null) return null;

            bool runner = true;
            while (runner)
            {
                for (int i = 0; i < cluster.Value.data.Count; i += super_block.directory_record_size)
                {
                    Directory_Record record = new Directory_Record();
                    record.inode = BitConverter.ToInt32(cluster.Value.data.ToArray(), i);
                    if (record.inode == 0) continue;

                    List<char> chars = new List<char>(Encoding.ASCII.GetChars(cluster.Value.data.GetRange(i + 4, super_block.directory_record_size - 4).ToArray()));
                    chars = chars.GetRange(0, chars.IndexOf('\0'));
                    record.name = String.Join("", chars);

                    if (record.name == record_name) return record;
                }
                if (cluster.Value.next_cluster != 0)
                {
                    cluster = Read_cluster(cluster.Value.next_cluster);
                    if (cluster == null) return null;
                }
                else runner = false;
            }
            return null;
        }

        #endregion Other functions

        #endregion FUNCTIONS

        #region STRUCTURES

        /// <summary>
        /// size 25 : super_block_size 1,
        ///           inode_size 1,           inods_count 4,     available_inodes_count 4,
        ///           user_record_size 1,     max_users_count 2, cur_users_count 2,
        ///           cluster_size_pow 1,     clusters_count 4,  available_clusters_count 4,
        ///           directory_record_size 1
        /// </summary>
        public struct Super_Block
        {
            public byte  super_block_size;
            public byte  cluster_size_pow;
            public int   clusters_count;
            public int   available_clusters_count;
            public byte  inode_size;
            public int   inods_count;
            public int   available_inodes_count;
            public byte  user_record_size;
            public short max_users_count;
            public short cur_users_count;
            public byte  directory_record_size;
        }

        public struct Inode
        {
            public bool[] atributes; /* 2Б: 0-1   – тип записи (00 – свободна, 10 – каталог, 01 – файл),
                                            2-4   – чтение, запись, исполнение пользователем-владельцем,
                                            5-7   – чтение, запись, исполнение группой-владельцем,
                                            8-10  – чтение, запись, исполнение другими,
                                            11    – «скрытый»,
                                            12    – «системный»,
                                            13-15 – зарезервировано.*/

            public byte owner_id;
            public byte owner_group_id;
            public int  first_cluster_pos;
            public int  size;
            public long creation_date_time;
            public long changing_date_time;
        }

        public struct User
        {
            public byte id;
            public byte group;
            public char[] login; // 10
            public int password_hash;
            public int home_dir_inode;

            public override string ToString()
            {
                string name = new string(login).TrimEnd('\0');
                return $"Login: {name + "              ".Remove(14 - name.Length)} | ID: {id}";
            }
        }

        public struct Cluster
        {
            public int next_cluster;
            public List<byte> data;
        }

        public struct Directory_Record
        {
            public int inode;
            public string name;
        }

        public struct System_Directory
        {
            public int inode;
            public List<Directory_Record> records;

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
        }

        #endregion STRUCTURES
    }
}
