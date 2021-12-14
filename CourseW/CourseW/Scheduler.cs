using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseW
{
    class Scheduler
    {
        #region VARIABLES

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

            int processes_count = _processes_count;
            for (int i = 0; i < processes_count; i++) { Create_proc(new Process()); tracing_part.Append("process created\r\n"); }

            tracing_part.Append("=== Queues: 0 - created      | 1 - ready     | 2 - high prior |            ===\r\n");
            tracing_part.Append("===         3 - common prior | 4 - low prior | 5 - wait       | 6 - zombie ===\r\n");

            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                Data_Keeper.FORM_Main.TXT_tracing.Text = tracing_part.ToString(); }));

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
                    foreach (Process process in queues[i]) line.Append($"[{process.Id:d2}]");
                    tracing_part.Append($"{line.ToString()}\r\n");
                }
                Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                    Data_Keeper.FORM_Main.TXT_tracing.Text += tracing_part.ToString(); }));

                switch (cur_queue)
                {
                    case 0: // created queue
                        {
                            int created_count = queues[0].Count;
                            tracing_part = new StringBuilder("    === initialize created processes\r\n");
                            for (int i = 0; i < created_count; i++)
                            {
                                Process process = queues[0].Dequeue();
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

                                tracing_part.Append($"    ID: {process.Id:d2}; Parent ID: {process.Parent_id:d2}; Priority: {process.Priority}; CPU burst: {process.CPU_burst:d3}; IO burst: {process.IO_burst:d3}\r\n");
                            }
                            if (created_count > 0) Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                                Data_Keeper.FORM_Main.TXT_tracing.Text += tracing_part.ToString(); }));
                        }
                    break;
                    case 1: // ready queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                    case 2: // high prior queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                    case 3: // common prior queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                    case 4: // low prior queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                    case 5: // wait queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                    case 6: // zombie queue
                        {
                            foreach (Process process in queues[cur_queue])
                            {
                            }
                        }
                    break;
                }
                if (processes_count == 0)
                {
                    Log.Write("Scheduler | Stopped becouse processes ended\n");

                    Running = false;
                    break;
                }
                if (cur_queue != 6) cur_queue++;
                else cur_queue = 0;

                Thread.Sleep(500);
            }
            while (Running);
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

        // >>>>>
        public int Get_proc_messages_queue_descriptor(string _key, bool[] _flags)
        {
            if (/*queue[key].IsExist()*/false)
            {
                if (/*!flags.need_new_queue*/false) return /*queue[key].descriptor*/1;
            }
            else
            {
                if (/*can_create_queue*/false) { /*Create_queue(key); return queue[key]*/ }
            }

            return -1;
        }

        // >>>>>
        public bool Setting_queue(int _queue_descriptor, int _operation, string _buffer)
        {
            switch(_operation)
            {
                case 1: // get info
                    {
                        Get_proc_messages_queue_descriptor(""/*queue key*/, new bool[0]/*queue flags*/);
                        string info = "";
                        if (/*can read info*/false) _buffer = info;
                        else return false;
                    }
                break;
                case 2: // set info
                    {
                        Get_proc_messages_queue_descriptor(""/*queue key*/, new bool[0]/*queue flags*/);
                        string info = "";
                        if (/*can write info*/false) info = _buffer;
                        else return false;
                    }
                break;
                case 3: // del queue
                    {
                        Get_proc_messages_queue_descriptor(""/*queue key*/, new bool[0]/*queue flags*/);
                        if (/*can del queue*/false) /*Delete_queue(key)*/;
                        else return false;
                    }
                break;
            }
            return true;
        }

        // >>>>>
        public int Send_message(int _queue_descriptor, int _send_message_memory_address, int _send_message_size, bool[] _flags)
        {
            if (/*!has_send_rights*/false) return -1;
            if (/*!Get_proc_messages_queue(_queue_descriptor)*/false) return -1;
            while (/*have'n available place*/false)
            {
                if (/*flags.cant_wait*/false) return -1;
                //wait(10);
            }

            //Get_message_header();
            //Read_message_from_task_to_core();
            //Setting_data_structures();
            //Wake_up_waited_to_read_msg_processes();

            return _send_message_size;
        }

        // >>>>>
        public int Get_message(int _queue_descriptor, int _receive_message_memory_address, int _receive_message_size, bool[] _flags, int _message_type)
        {
            if (/*!can_receive_rights*/false) return -1;
            if (/*!Get_proc_messages_queue(_queue_descriptor)*/false) return -1;
            while (/*queue.IsHollow()*/false)
            {
                if (/*flags.cant_wait*/false) return -1;
                //wait(10);
            }

            if (_message_type == 0) /*message = queue[0]*/;
            else if (_message_type > 0) /*message = queue.First(_message_type)*/;
            else
            {
                //min = foreach(queue.message_type) < queue[min].message_type;
                //message = queue.[min];
            }

            if (/*message != null*/false)
            {
                if (_receive_message_size < 0/*message.Length()*/) return -1;
                else
                {
                    _receive_message_memory_address = 0/*message.address*/;
                    //Delete_message(message.id)
                }
            }

            return 0/*message.Length()*/;
        }

        // >>>>>
        public int Reflect_file(int _address_for_file, int _memory_size, bool[] _can_operate, bool[] _reflection_params, int _file_descriptor, int _file_offset)
        {
            if (/*!Get_file(_file_descriptor)*/false) return -1;
            //Get_file_size();
            if (_memory_size < 0/*file_size*/) return -1;
            if (/*!Can_write_in_memory_at(_address_for_file, file_size)*/false) return -1;

            if (/*_reflection_params.must_reflect_at_this_address*/false)
            {
                if (/*!Can_reflect_at_this_address()*/false) return -1;
            }
            else
            {
                if (/*!Can_reflect_at_this_address()*/false) /*_address_for_file = Search_suitable_address(file_size)*/;

                //memory = Get_memory_at(_address_for_file);
                //Reflect_file(memory);
                //Set_show_params(params);
                //Set_rights_for_memory(rights);
            }

            return _address_for_file;
        }

        // >>>>>
        public bool Free_file_reflect(int _addres_for_file, int _memory_size)
        {
            if (/*!Get_memory_at(_address_for_file);*/false) return false;
            //if (size > memory_size) return false;

            //Free_file(_addres_for_file);
            //Notify_processes_with_mempry_work(_addres_for_file, free);
            //Free_memory(_addres_for_file);

            return true;
        }

        // >>>>>
        public bool Sync_file_and_reflecion(int _address_for_file, int _memory_size, bool[] _flags)
        {
            if (/*!Get_memory_at(_address_for_file);*/false) return false;
            //if (size > memory_size) return false;

            //foreach(address in _address_for_file.data_addresses)
                //Write_file(_address_for_file.file, address.data);

            return true;
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

            public void CPU_tick()
            {

            }
            public void IO_tick()
            {

            }

            public Process()
            {
                State = STATES.zombie;
                Id = Parent_id = CPU_burst = IO_burst = Priority = 0;
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
        }

        #endregion OTHER
    }
}
