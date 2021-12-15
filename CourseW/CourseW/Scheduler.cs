using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseW
{
    class Scheduler
    {
        #region VARIABLES

        private const byte TICK                  = 50;
        private const byte MAX_PROCESSES_TOTAL   = 15;
        private const byte MAX_PROCESSES_WORKING = 10;

        /// <summary>
        /// 0 - created; 1 - ready; 2 - high prior; 3 - common prior; 4 - low prior; 5 - wait; 6 - zombie;
        /// </summary>
        private List<Queue<Process>> queues;
        private int cur_queue;
        public bool Running { set; get; }

        #endregion VARIABLES

        #region FUNCTIONS

        public void Run(int _processes_count)
        {
            Log.Write("Scheduler | Ran\n");
            Running = true;

            StringBuilder tracing_part = new StringBuilder("========== TRACING START ==========\r\n");


            queues = new List<Queue<Process>>() { new Queue<Process>(),   // created
                                                  new Queue<Process>(),   // ready
                                                  new Queue<Process>(),   // high prior
                                                  new Queue<Process>(),   // common prior
                                                  new Queue<Process>(),   // low prior
                                                  new Queue<Process>(),   // wait
                                                  new Queue<Process>() }; // zombie

            for (int i = 0; i < _processes_count; i++) { Create_proc(new Process()); tracing_part.Append("process created\r\n"); }

            tracing_part.Append("=== Queues: 0 - created      | 1 - ready     | 2 - high prior |            ===\r\n");
            tracing_part.Append("===         3 - common prior | 4 - low prior | 5 - wait       | 6 - zombie ===\r\n");

            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                Data_Keeper.FORM_Main.TXT_tracing.Text = tracing_part.ToString(); }));

            _processes_count = 0;
            cur_queue = 0;
            do
            {
                Log.Write("Scheduler | New iteration\n");
                tracing_part = new StringBuilder("========== NEW ITERATION ==========\r\n");

                StringBuilder line = null;
                for (int i = 0; i < queues.Count; i++)
                {
                    if (i != cur_queue) line = new StringBuilder($"Queue {i}: ");
                    else                line = new StringBuilder($"Queue {i}> ");
                    foreach (Process process in queues[i])
                    {
                        if (process.Id == 0) line.Append("[0]");
                        else line.Append($"[{process.Id:d2}|{process.CPU_burst:d3}|{process.IO_burst:d3}]");
                    }
                    tracing_part.Append($"{line.ToString()}\r\n");
                }
                Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                    Data_Keeper.FORM_Main.TXT_tracing.Text += tracing_part.ToString(); }));

                switch (cur_queue)
                {
                    case 0: // created queue
                        {
                            int created_count = queues[cur_queue].Count;
                            tracing_part = new StringBuilder("    === initialize created processes\r\n");
                            for (int i = 0; i < created_count; i++)
                            {
                                Process process = queues[cur_queue].Dequeue();
                                process.State = Process.STATES.ready;
                                for (int id = 1; id <= MAX_PROCESSES_TOTAL; id++)
                                {
                                    bool breaker = false;
                                    for (int j = 1; j < queues.Count; j++)
                                    {
                                        foreach (Process proc in queues[j])
                                        {
                                            if (proc.Id == id) { breaker = true; break; }
                                        }
                                        if (breaker) break;
                                    }
                                    if (!breaker) { process.Id = id; break; }
                                }
                                if (process.Id == 0)
                                {
                                    tracing_part.Append($"    Can not initialize process\r\n");

                                    Log.Write("Scheduler | Can not initialize process\n");
                                    continue;
                                }

                                process.CPU_burst = Data_Keeper.random.Next(1, 1000);
                                process.IO_burst =  Data_Keeper.random.Next(1, 300);
                                process.Priority =  Data_Keeper.random.Next(1, 10);

                                queues[1].Enqueue(process);
                                _processes_count++;

                                tracing_part.Append($"    ID: {process.Id:d2}; Parent ID: {process.Parent_id:d2}; Priority: {process.Priority}; CPU burst: {process.CPU_burst:d3}; IO burst: {process.IO_burst:d3}\r\n");
                            }
                            if (created_count > 0) Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                                Data_Keeper.FORM_Main.TXT_tracing.Text += tracing_part.ToString(); }));
                        }
                    break;
                    case 1: // ready queue
                        {
                            int working_count = 0;
                            for (int queue = 2; queue < 5; queue++)
                                foreach (Process proc in queues[queue])
                                    working_count++;
                            working_count = MAX_PROCESSES_WORKING - working_count;
                            if (queues[cur_queue].Count < working_count) working_count = queues[cur_queue].Count;
                            for (int i = 0; i < working_count; i++)
                            {
                                Process process = queues[cur_queue].Dequeue();
                                process.State = Process.STATES.core;
                                int queue_num = 0;
                                switch (process.Priority)
                                {
                                    case 1: case 2: case 3: queue_num = 2; break; // high   prior queue
                                    case 4: case 5: case 6: queue_num = 3; break; // common prior queue
                                    case 7: case 8: case 9: queue_num = 4; break; // low    prior queue
                                }
                                int processes_count = queues[queue_num].Count;
                                bool setted = false;
                                for (int j = 0; j < processes_count; j++)
                                {
                                    Process queue_process = queues[queue_num].Dequeue();
                                    if (queue_process.Priority > process.Priority && !setted) { queues[queue_num].Enqueue(process); setted = true; }
                                    queues[queue_num].Enqueue(queue_process);
                                }
                                if (!setted) queues[queue_num].Enqueue(process);
                            }
                        }
                    break;
                    case 2: Process_work_queue(7); break; // high prior queue
                    case 3: Process_work_queue(5); break; // common prior queue
                    case 4: Process_work_queue(3); break; // low prior queue
                    case 5: // wait queue
                        {
                            int processes_count = queues[cur_queue].Count;
                            for (int i = 0; i < processes_count; i++)
                            {
                                Process process = queues[cur_queue].Dequeue();
                                if (process.IO_ticking(5))
                                {
                                    process.State = Process.STATES.ready;
                                    queues[1].Enqueue(process);
                                }
                                else queues[cur_queue].Enqueue(process);
                            }
                        }
                    break;
                    case 6: // zombie queue
                        {
                            int processes_count = queues[cur_queue].Count;
                            for (int i = 0; i < processes_count; i++)
                            {
                                Process process = queues[cur_queue].Dequeue();
                                bool parent_zombie = false;
                                foreach (Process zombie_process in queues[cur_queue])
                                    if (zombie_process.Id == process.Parent_id) { parent_zombie = true; break; }
                                if (process.Parent_id == 0) parent_zombie = true;
                                if (!parent_zombie) queues[cur_queue].Enqueue(process);
                                else _processes_count--;
                            }
                        }
                    break;
                }
                if (_processes_count == 0)
                {
                    Log.Write("Scheduler | Stopped becouse processes ended\n");

                    Running = false;
                }
                if (cur_queue != 6) cur_queue++;
                else cur_queue = 0;
            }
            while (Running);
            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                Data_Keeper.FORM_Main.TXT_tracing.Text += "========== TRACING ENDED =========="; }));
        }
        private void Process_work_queue(byte _prior_major_num)
        {
            int processes_count = 0;
            if (queues[cur_queue].Count < _prior_major_num) processes_count = queues[cur_queue].Count;
            else processes_count = _prior_major_num;
            for (int i = 0; i < processes_count; i++)
            {
                Process process = queues[cur_queue].Dequeue();
                if (process.IO_burst > 0)
                {
                    process.State = Process.STATES.wait;
                    queues[5].Enqueue(process);
                }
                else
                {
                    process.State = Process.STATES.task;
                    if (process.CPU_ticking(_prior_major_num * 3))
                    {
                        process.State = Process.STATES.zombie;
                        queues[6].Enqueue(process);
                    }
                    else
                    {
                        process.State = Process.STATES.core;
                        queues[cur_queue].Enqueue(process);
                    }
                }
            }
        }

        public void Stop()
        {
            Log.Write("Scheduler | Stopped by user\n");

            Running = false;
        }

        public void Create_proc(Process _parent_process)
        {
            Log.Write("Scheduler | Create process\n");

            queues[0].Enqueue(new Process(_parent_process));
        }

        public void Kill_proc(int _process_id)
        {
            Log.Write($"Scheduler | Kill process {_process_id}\n");

            bool breaker = false;
            for (int i = 0; i < queues.Count - 1; i++)
            {
                for (int j = 0; j < queues[i].Count; j++)
                {
                    Process process = queues[i].Dequeue();
                    if (process.Id == _process_id)
                    {
                        process.State = Process.STATES.zombie;
                        queues[6].Enqueue(process);
                        breaker = true;
                    }
                    else
                    {
                        queues[i].Enqueue(process);
                    }
                }
                if (breaker) break;
            }
        }

        /*
        public int Reflect_file(int _address_for_file, int _memory_size, bool[] _can_operate, bool[] _reflection_params, int _file_descriptor, int _file_offset)
        {
            if (!Get_file(_file_descriptor)) return -1;
            //Get_file_size();
            if (_memory_size < file_size) return -1;
            if (!Can_write_in_memory_at(_address_for_file, file_size)) return -1;

            if (_reflection_params.must_reflect_at_this_address)
            {
                if (!Can_reflect_at_this_address()) return -1;
            }
            else
            {
                if (!Can_reflect_at_this_address()) _address_for_file = Search_suitable_address(file_size);

                memory = Get_memory_at(_address_for_file);
                Reflect_file(memory);
                Set_show_params(params);
                Set_rights_for_memory(rights);
            }

            return _address_for_file;
        }

        public bool Free_file_reflect(int _addres_for_file, int _memory_size)
        {
            if (!Get_memory_at(_address_for_file);) return false;
            if (size > memory_size) return false;

            Free_file(_addres_for_file);
            Notify_processes_with_mempry_work(_addres_for_file, free);
            Free_memory(_addres_for_file);

            return true;
        }

        public bool Sync_file_and_reflecion(int _address_for_file, int _memory_size, bool[] _flags)
        {
            if (!Get_memory_at(_address_for_file);) return false;
            if (size > memory_size) return false;

            foreach(address in _address_for_file.data_addresses)
                Write_file(_address_for_file.file, address.data);

            return true;
        }
        */

        public bool Reflect_file(string _full_file_path)
        {
            try
            {
                Data_Keeper.reflected_files.Add(_full_file_path, new FileStream(_full_file_path, FileMode.Open));
                return true;
            }
            catch { return false; }
        }

        public bool Free_file_reflect(string _full_file_path)
        {
            try
            {
                Data_Keeper.reflected_files[_full_file_path].Close();
                Data_Keeper.reflected_files.Remove(_full_file_path);
                return true;
            }
            catch { return false; }
        }

        #endregion FUNCTIONS

        #region OTHER

        public class Process
        {
            public enum STATES { created, ready, core, task, wait, zombie }
            public STATES State  { get; set; }
            public int Id        { get; set; }
            public int Parent_id { get; private set; }
            public int CPU_burst { get; set; }
            public int IO_burst  { get; set; }
            public int Priority  { get; set; }
            public Queue<Message> Messages { get; set; }
            public const byte MAX_MESSAGES_COUNT = 5;
            public bool Wake_up_for_receiving { get; set; }

            public bool CPU_ticking(int _ticks)
            {
                while (CPU_burst > 0 && _ticks > 0)
                {
                    _ticks--;
                    CPU_burst -= TICK;
                    if (CPU_burst < 0) CPU_burst = 0;
                    Thread.Sleep(TICK);
                }
                if (CPU_burst > 0) return false;
                else               return true;
            }
            public bool IO_ticking(int _ticks, bool _received = true, Message? message_holder = null,
                                               Process _recepient = null, string _message_header = "", string _message_body = "", bool _can_wait = true)
            {
                Wake_up_for_receiving = _received;
                bool sent = false;
                while (IO_burst > 0 && _ticks > 0 || !sent || !_received)
                {
                    if (!_received)
                        if (Wake_up_for_receiving) _received = Receive_message(message_holder.Value);
                    if (!sent) sent = Send_message(_recepient, _message_header, _message_body, _can_wait);
                    if (IO_burst > 0 && _ticks > 0)
                    {
                        _ticks--;
                        IO_burst -= TICK;
                        if (IO_burst < 0) IO_burst = 0;
                    }
                    Thread.Sleep(TICK);
                }
                if (IO_burst > 0) return false;
                else              return true;
            }

            public Process()
            {
                State = STATES.zombie;
                Id = Parent_id = CPU_burst = IO_burst = Priority = 0;
                Messages = new Queue<Message>();
            }
            public Process(Process _parent_process)
            {
                State = STATES.created;
                Id = 0;
                Parent_id = _parent_process.Id;
                CPU_burst = _parent_process.CPU_burst;
                IO_burst  = _parent_process.IO_burst;
                Priority  = _parent_process.Priority;
            }

            /*
            public int Get_proc_messages_queue_descriptor(string _key, bool[] _flags)
            {
                if (queue[key].IsExist())
                {
                    if (!flags.need_new_queue) return queue[key].descriptor;
                }
                else
                {
                    if (can_create_queue) { Create_queue(key); return queue[key]}
                }

                return -1;
            }

            public bool Setting_message_queue(int _queue_descriptor, int _operation, string _buffer)
            {
                switch (_operation)
                {
                    case 1: // get info
                        {
                            Get_proc_messages_queue_descriptor(queue key, queue flags);
                            string info = "";
                            if (can read info) _buffer = info;
                            else return false;
                        }
                        break;
                    case 2: // set info
                        {
                            Get_proc_messages_queue_descriptor(queue key, queue flags);
                            string info = "";
                            if (can write info) info = _buffer;
                            else return false;
                        }
                        break;
                    case 3: // del queue
                        {
                            Get_proc_messages_queue_descriptor(queue key, queue flags);
                            if (can del queue) Delete_queue(key);
                            else return false;
                        }
                        break;
                }
                return true;
            }

            public int Send_message(int _queue_descriptor, int _send_message_memory_address, int _send_message_size, bool[] _flags)
            {
                if (!has_send_rights) return -1;
                if (!Get_proc_messages_queue(_queue_descriptor)) return -1;
                while (have'n available place)
                {
                    if (flags.cant_wait) return -1;
                    wait(10);
                }

                Get_message_header();
                Read_message_from_task_to_core();
                Setting_data_structures();
                Wake_up_waited_to_read_msg_processes();

                return _send_message_size;
            }

            public int Get_message(int _queue_descriptor, int _receive_message_memory_address, int _receive_message_size, bool[] _flags, int _message_type)
            {
                if (!can_receive_rights) return -1;
                if (!Get_proc_messages_queue(_queue_descriptor)) return -1;
                while (queue.IsHollow())
                {
                    if (flags.cant_wait) return -1;
                    wait(10);
                }

                if (_message_type == 0) message = queue[0];
                else if (_message_type > 0) message = queue.First(_message_type);
                else
                {
                    min = foreach(queue.message_type) < queue[min].message_type;
                    message = queue.[min];
                }

                if (message != null)
                {
                    if (_receive_message_size< message.Length()) return -1;
                    else
                    {
                        _receive_message_memory_address = message.address;
                        Delete_message(message.id)
                    }
                }

                return message.Length();
            }
            */

            public bool Send_message(Process _recepient, string _message_header, string _message_body, bool _can_wait)
            {
                if (_recepient == null) return true;
                if (_recepient.Messages.Count == MAX_MESSAGES_COUNT)
                    if (_can_wait) return false;
                    else           return true;

                _recepient.Messages.Enqueue(new Message() { Message_header = _message_header, Message_body = _message_body });
                _recepient.Wake_up_for_receiving = true;

                return true;
            }

            public bool Receive_message(Message _message_holder)
            {
                _message_holder = Messages.Dequeue();
                return true;
            }

            public struct Message
            {
                public string Message_header { get; set; }
                public string Message_body   { get; set; }
            }
        }

        #endregion OTHER
    }
}
