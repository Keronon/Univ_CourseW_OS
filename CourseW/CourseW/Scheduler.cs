using System;
using System.Collections.Generic;
using System.Drawing;
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
        private const byte MAX_PROCESSES_TOTAL   = 5;
        private const byte MAX_PROCESSES_WORKING = 4;

        /// <summary>
        /// 0 - created; 1 - ready; 2 - high prior; 3 - common prior; 4 - low prior; 5 - wait; 6 - zombie;
        /// </summary>
        private List<Queue<Process>> queues;
        private int cur_queue;

        private static byte steps;
        public bool running;

        #endregion VARIABLES

        #region FUNCTIONS

        public void Run(int _processes_count)
        {
            Queues_Tracing.Start();
            Burst_Tracing.Start();
            Log.Write("Scheduler | Ran\n");
            running = true;
            steps = 0;

            queues = new List<Queue<Process>>() { new Queue<Process>(),   // created
                                                  new Queue<Process>(),   // ready
                                                  new Queue<Process>(),   // high prior
                                                  new Queue<Process>(),   // common prior
                                                  new Queue<Process>(),   // low prior
                                                  new Queue<Process>(),   // wait
                                                  new Queue<Process>() }; // zombie

            for (int i = 0; i < _processes_count; i++) { Create_proc(new Process()); Queues_Tracing.Write("process created\n"); }
            Queues_Tracing.Write("========================================\n");

            _processes_count = 0;
            cur_queue = 0;
            do
            {
                Show_queues_state();
                switch (cur_queue)
                {
                    case 0: // created queue
                        {
                            int created_count = queues[cur_queue].Count;
                            for (int i = 0; i < created_count; i++)
                            {
                                Make_step();

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
                                    continue;
                                }

                                process.CPU_burst = Data_Keeper.random.Next(1, 1000);
                                process.IO_burst =  Data_Keeper.random.Next(1, 300);
                                process.Priority =  Data_Keeper.random.Next(1, 10);

                                queues[1].Enqueue(process);
                                _processes_count++;

                                Show_queues_state();
                            }
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

                                Make_step();
                                Show_queues_state();
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
                                if (process.IO_ticking(cur_queue))
                                {
                                    process.State = Process.STATES.ready;
                                    queues[1].Enqueue(process);
                                }
                                else queues[cur_queue].Enqueue(process);

                                Make_step();
                                Show_queues_state();
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

                                Make_step();
                                Show_queues_state();
                            }
                        }
                    break;
                }
                if (_processes_count == 0)
                {
                    Log.Write("Scheduler | Stopped becouse processes ended\n");

                    running = false;
                }
                if (cur_queue != 6) cur_queue++;
                else cur_queue = 0;
            }
            while (running);

            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_work.Text = "!!!!!"; }));
            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                Data_Keeper.FORM_Main.TXT_tracing.Text = "===================================\r\n\r\n========== TRACING ENDED ==========\r\n\r\n==================================="; }));
        }
        private void Process_work_queue(byte _prior_major_num)
        {
            int processes_count = 0;
            if (queues[cur_queue].Count < _prior_major_num) processes_count = queues[cur_queue].Count;
            else processes_count = _prior_major_num;
            for (int i = 0; i < processes_count; i++)
            {
                Process process = queues[cur_queue].Dequeue();
                // >>>
                if (process.IO_burst > 0)
                {
                    process.State = Process.STATES.wait;
                    queues[5].Enqueue(process);
                }
                else
                {
                    process.State = Process.STATES.task;
                    if (process.CPU_ticking(_prior_major_num))
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

                Make_step();
                Show_queues_state();
            }
        }
        private void Show_queues_state()
        {
            StringBuilder tracing_part;
            tracing_part = new StringBuilder("=== Queues: 0 - created      | 1 - ready     | 2 - high prior |            ===\r\n");
            tracing_part             .Append("===         3 - common prior | 4 - low prior | 5 - wait       | 6 - zombie ===\r\n");
            tracing_part             .Append("========================================\r\n");
            tracing_part             .Append("States: created = 0, ready = 1, core = 2, task = 3, wait = 4, zombie = 5\r\n");
            tracing_part             .Append("========================================\r\n");
            tracing_part             .Append("[id|priority|state|cpu|io]\r\n");
            tracing_part             .Append("========================================\r\n");

            StringBuilder line;
            int process_num = 0;
            for (int i = 0; i < queues.Count; i++)
            {
                if (i != cur_queue) line = new StringBuilder($"Queue {i}: ");
                else                line = new StringBuilder($"Queue {i}> ");
                foreach (Process process in queues[i])
                {
                    process_num++;
                    if (process.Id == 0) line.Append("[0]");
                    else if (process_num > MAX_PROCESSES_TOTAL) line.Append("[X]");
                    else line.Append($"[{process.Id:d2}|{process.Priority}|{(int)process.State}|{process.CPU_burst:d3}|{process.IO_burst:d3}] ");
                }
                tracing_part.Append($"{line.ToString()}\r\n");
            }
            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => {
                Data_Keeper.FORM_Main.TXT_tracing.Text = tracing_part.ToString(); }));
            tracing_part.Append("========================================\r\n");
            Queues_Tracing.Write(tracing_part.ToString());
        }
        private static void Make_step()
        {
            steps++;
            if (steps > 5) steps = 1;
            Data_Keeper.FORM_Main.TXT_tracing.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_work.Text = new String('|', steps); }));
            Thread.Sleep(500);
        }

        public void Stop()
        {
            Log.Write("Scheduler | Stopped by user\n");
            Data_Keeper.FORM_Main.TXT_work.Text = "";

            running = false;
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
                Burst_Tracing.Write($"===== {Id} | CPU burst =====\n");
                Make_step();
                Show_queues_state(true);

                while (CPU_burst > 0 && _ticks > 0)
                {
                    _ticks--;
                    CPU_burst -= TICK;
                    if (CPU_burst < 0) CPU_burst = 0;
                    Thread.Sleep(TICK);

                    Make_step();
                    Show_queues_state(true);
                }
                if (CPU_burst > 0)
                {
                    Data_Keeper.FORM_Main.TXT_burst.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_burst.Text = ""; }));
                    Burst_Tracing.Write("=========================\n");
                    return false;
                }
                else
                {
                    Data_Keeper.FORM_Main.TXT_burst.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_burst.Text = ""; }));
                    Burst_Tracing.Write("=========================\n");
                    return true;
                }
            }
            public bool IO_ticking(int _ticks, bool _received = true, Message? message_holder = null,
                                               Process _recepient = null, string _message_header = "", string _message_body = "", bool _can_wait = true)
            {
                Burst_Tracing.Write($"===== {Id} | I/O burst =====\n");
                Make_step();
                Show_queues_state(true);

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

                    Make_step();
                    Show_queues_state(false);
                }
                if (IO_burst > 0)
                {
                    Data_Keeper.FORM_Main.TXT_burst.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_burst.Text = ""; }));
                    Burst_Tracing.Write("=========================\n");
                    return false;
                }
                else
                {
                    Data_Keeper.FORM_Main.TXT_burst.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_burst.Text = ""; }));
                    Burst_Tracing.Write("=========================\n");
                    return true;
                }
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

            private void Show_queues_state(bool _CPU_burst)
            {
                Burst_Tracing.Write($"{CPU_burst:d3} | {IO_burst:d3}\n");

                Data_Keeper.FORM_Main.TXT_burst.Invoke(new Action(() => { Data_Keeper.FORM_Main.TXT_burst.Text =
                     "| Type: CPU\r\n" +
                    $"| ID: {Id}\r\n" +
                    $"| State: {State}\r\n" +
                    $"{(_CPU_burst ? '>' : '|')} CPU burst: {CPU_burst}\r\n" +
                    $"{(_CPU_burst ? '|' : '>')} IO burst: {IO_burst}"; }));
            }

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
