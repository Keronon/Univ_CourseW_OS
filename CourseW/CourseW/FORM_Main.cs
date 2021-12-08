using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseW
{
    public partial class FORM_Main : Form
    {
        #region VARIABLES
        private bool action_cut = false;
        private string action_path = "";
        #endregion VARIABLES

        public FORM_Main()
        {
            Log.Write("FORM_Main | Initialization\n");

            Data_Keeper.FORM_Main = this;
            InitializeComponent();
            PANEL_change_password.Hide();
            PANEL_users.Hide();

            ImageList image_list = new ImageList();
            image_list.Images.Add("file", new Bitmap(Data_Keeper.res_folder + "file_sys.ico"));
            image_list.Images.Add("folder", new Bitmap(Data_Keeper.res_folder + "folder.ico"));
            TREE_view.ImageList = image_list;

            ToolStripMenuItem create_item = new ToolStripMenuItem("Create");
            ToolStripMenuItem create_file_item = new ToolStripMenuItem("File");
            ToolStripMenuItem create_directory_item = new ToolStripMenuItem("Directory");
            create_item.DropDownItems.AddRange(new[] { create_file_item, create_directory_item });
            ToolStripMenuItem delete_directory_item = new ToolStripMenuItem("Delete");
            ToolStripMenuItem rename_directory_item = new ToolStripMenuItem("Rename");
            ToolStripMenuItem copy_directory_item = new ToolStripMenuItem("Copy");
            ToolStripMenuItem cut_directory_item = new ToolStripMenuItem("Cut");
            ToolStripMenuItem paste_directory_item = new ToolStripMenuItem("Paste");
            ToolStripMenuItem properties_directory_item = new ToolStripMenuItem("Properties");

            ToolStripMenuItem write_item = new ToolStripMenuItem("Write");
            ToolStripMenuItem append_item = new ToolStripMenuItem("Append");
            ToolStripMenuItem renew_item = new ToolStripMenuItem("Renew");
            write_item.DropDownItems.AddRange(new[] { append_item, renew_item });
            ToolStripMenuItem delete_file_item = new ToolStripMenuItem("Delete");
            ToolStripMenuItem rename_file_item = new ToolStripMenuItem("Rename");
            ToolStripMenuItem copy_file_item = new ToolStripMenuItem("Copy");
            ToolStripMenuItem cut_file_item = new ToolStripMenuItem("Cut");
            ToolStripMenuItem properties_file_item = new ToolStripMenuItem("Properties");

            CONTEXT_directory.Items.AddRange(new[] { create_item, delete_directory_item, rename_directory_item, copy_directory_item, cut_directory_item, paste_directory_item, properties_directory_item });
            CONTEXT_file.Items.AddRange(new[] { write_item, delete_file_item, rename_file_item, copy_file_item, cut_file_item, properties_file_item });

            create_file_item.Click += (o, e) =>
            {
                Log.Write("FORM_Main | Create file clicked\n");

                string path = TREE_view.SelectedNode.FullPath;
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                try
                {
                    File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                    if (inode == null) { throw new Exception(); }

                    if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true ||
                        (inode.Value.owner_id == 0 && new string(Data_Keeper.cur_user.login).Trim().Trim('\0') != path.Substring(2)))
                    {
                        MessageBox.Show("You can not create at this directory");

                        Log.Write($"FORM_Main | User can not create at directory\n");
                        return;
                    }
                }
                catch
                {
                    Log.Write($"FORM_Main | Creating ERROR at owner comparing\n");
                    MessageBox.Show("Error");
                    return;
                }

                FORM_Input input_dialog = new FORM_Input();
                if (input_dialog.ShowDialog(this, "New file name") == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Creating file ACCEPTED\n");

                    path = $"{path}\\{input_dialog.TXT_input.Text}";
                    if (Data_Keeper.file_system.Create_system_object(path, File_System.FILE_SYSTEM_OBJECT.file))
                    {
                        Refresh_tree_view();
                        MessageBox.Show("Created");
                    }
                    else MessageBox.Show("Error");
                }
                else
                {
                    Log.Write("FORM_Main | Creating file CANCELED\n");

                    MessageBox.Show("Canceled");
                }
                input_dialog.Dispose();
            };
            create_directory_item.Click += (o, e) =>
            {
                Log.Write("FORM_Main | Create directory clicked\n");

                string path = TREE_view.SelectedNode.FullPath;
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                try
                {
                    File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                    if (inode == null) { throw new Exception(); }

                    if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true ||
                        (inode.Value.owner_id == 0 && new string(Data_Keeper.cur_user.login).Trim().Trim('\0') != path.Substring(2)))
                    {
                        MessageBox.Show("You can not create at this directory");

                        Log.Write($"FORM_Main | User can not create at directory\n");
                        return;
                    }
                }
                catch
                {
                    Log.Write($"FORM_Main | Creating ERROR at owner comparing\n");
                    MessageBox.Show("Error");
                    return;
                }

                FORM_Input input_dialog = new FORM_Input();
                if (input_dialog.ShowDialog(this, "New directory name") == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Creating directory ACCEPTED\n");

                    path = $"{path}\\{input_dialog.TXT_input.Text}";
                    if (Data_Keeper.file_system.Create_system_object(path, File_System.FILE_SYSTEM_OBJECT.directory))
                    {
                        Refresh_tree_view();
                        MessageBox.Show("Created");
                    }
                    else MessageBox.Show("Error");
                }
                else
                {
                    Log.Write("FORM_Main | Creating directory CANCELED\n");

                    MessageBox.Show("Canceled");
                }
                input_dialog.Dispose();
            };
            delete_directory_item.Click += (o, e) => Delete_object();
            rename_directory_item.Click += (o, e) => Rename_object();
            copy_directory_item.Click += (o, e) => Copy_object();
            cut_directory_item.Click += (o, e) => Cut_object();
            paste_directory_item.Click += (o, e) => Paste_object();
            properties_directory_item.Click += (o, e) => Show_properties(o.GetType().Name);

            append_item.Click += (o, e) =>
            {
                Log.Write("FORM_Main | Append file clicked\n");

                string path = TREE_view.SelectedNode.FullPath;
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                try
                {
                    File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                    if (inode == null) { throw new Exception(); }

                    if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true)
                    {
                        MessageBox.Show("You can not append this file");

                        Log.Write($"FORM_Main | User can not append file\n");
                        return;
                    }
                }
                catch
                {
                    Log.Write($"FORM_Main | Appending ERROR at owner comparing\n");
                    MessageBox.Show("Error");
                    return;
                }

                string file = new string(Encoding.ASCII.GetChars(Data_Keeper.file_system.Read_file(path).Value.data.ToArray())).Trim().Trim('\0');

                FORM_Append append_dialog = new FORM_Append();
                if (append_dialog.ShowDialog(this, $"{TREE_view.SelectedNode.Name} appending", file) == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Appending file ACCEPTED\n");

                    string append = append_dialog.TXT_append.Text + "\r\n";
                    if (Data_Keeper.file_system.Append_file(path, append.ToArray().ToList())) MessageBox.Show("Appended");
                    else MessageBox.Show("Error");
                }
                else
                {
                    Log.Write("FORM_Main | Appending file CANCELED\n");

                    MessageBox.Show("Canceled");
                }
                append_dialog.Dispose();
            };
            renew_item.Click += (o, e) =>
            {
                Log.Write("FORM_Main | Renew file clicked\n");

                string path = TREE_view.SelectedNode.FullPath;
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                try
                {
                    File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                    if (inode == null) { throw new Exception(); }

                    if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true)
                    {
                        MessageBox.Show("You can not write this file");

                        Log.Write($"FORM_Main | User can not write file\n");
                        return;
                    }
                }
                catch
                {
                    Log.Write($"FORM_Main | Writing ERROR at owner comparing\n");
                    MessageBox.Show("Error");
                    return;
                }

                FORM_Write write_dialog = new FORM_Write();
                if (write_dialog.ShowDialog(this, $"{TREE_view.SelectedNode.Name} rewriting") == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Renewing file ACCEPTED\n");

                    string write = write_dialog.TXT_write.Text + "\r\n";
                    if (Data_Keeper.file_system.Write_file(path, Encoding.ASCII.GetBytes(write.ToArray()).ToList())) MessageBox.Show("Writed");
                    else MessageBox.Show("Error");
                }
                else
                {
                    Log.Write("FORM_Main | Renewing file CANCELED\n");

                    MessageBox.Show("Canceled");
                }
                write_dialog.Dispose();
            };
            delete_file_item.Click += (o, e) => Delete_object();
            rename_file_item.Click += (o, e) => Rename_object();
            copy_file_item.Click += (o, e) => Copy_object();
            cut_file_item.Click += (o, e) => Cut_object();
            properties_file_item.Click += (o, e) => Show_properties(o.GetType().Name);

            TREE_view.NodeMouseClick += (o, e) =>
            {
                TREE_view.BeginUpdate();
                TREE_view.SelectedNode = e.Node;
                try
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (TREE_view.SelectedNode.Text.IndexOf('[') < 0) CONTEXT_file.Show(MousePosition);
                        else                                              CONTEXT_directory.Show(MousePosition);
                    }
                }
                finally
                {
                    TREE_view.EndUpdate();
                }
            };

            Refresh_tree_view();

            BTN_search.Click += (o, e) => Show_properties(o.GetType().Name);

            Log.Write("FORM_Main | Initialized\n");
        }

        #region INTERFACE

        #region File system

        private void BTN_clear_action_Click(object sender, EventArgs e) { action_cut = false; action_path = ""; LBL_cur_action.Text = "Current action: none"; }

        #endregion File system

        #region System

        #region change password
        private void BTN_change_password_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Change password Clicked\n");

            PANEL_change_password.Show();
        }

        private void BTN_cancel_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Cancel Clicked\n");

            TXT_old_password.Text = TXT_new_password.Text = TXT_repeat_password.Text = "";
            PANEL_change_password.Hide();
        }

        private void BTN_accept_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Accept Clicked\n");

            if (TXT_old_password.Text.GetHashCode() == Data_Keeper.cur_user.password_hash)
            {
                if (TXT_new_password.Text == TXT_repeat_password.Text)
                {
                    for (int i = 1; i <= Data_Keeper.file_system.super_block.cur_users_count; i++)
                    {
                        File_System.User? user = Data_Keeper.file_system.Read_user(i);
                        if (user != null && user.Value.id == Data_Keeper.cur_user.id)
                        {
                            Data_Keeper.cur_user.password_hash = TXT_new_password.Text.GetHashCode();
                            Data_Keeper.file_system.Write_user(i, Data_Keeper.cur_user);
                            break;
                        }
                    }
                    MessageBox.Show("Password changed");
                    TXT_old_password.Text = TXT_new_password.Text = TXT_repeat_password.Text = "";
                    PANEL_change_password.Hide();
                }
                else MessageBox.Show("Incorrect repeating password");
            }
            else MessageBox.Show("Incorrect old password");
        }
        #endregion change password

        #region users
        private void BTN_users_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Users Clicked\n");

            if (PANEL_users.Visible) PANEL_users.Hide();
            else PANEL_users.Show();
        }

        private void BTN_add_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Add user Clicked\n");

        }

        private void BTN_delete_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Delete user Clicked\n");

        }

        private void BTN_edit_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Edit user Clicked\n");

        }
        #endregion users

        private void BTN_logout_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Logout Clicked\n");

            Hide();
            Data_Keeper.FORM_Authorization.Load_users();
            Data_Keeper.FORM_Authorization.Show();

            Log.Write("FORM_Main | Logged out\n");
        }

        private void BTN_quit_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Quit Clicked\n");

            Application.Exit();
        }

        #endregion System

        #endregion INTERFACE

        #region FUNCTIONS

        public void Set_user()
        {
            TXT_user.Text = new string(Data_Keeper.cur_user.login).TrimEnd();
            if (Data_Keeper.cur_user.id > 1) { BTN_add_user.Enabled = BTN_delete_user.Enabled = BTN_edit_user.Enabled = false; }

            LIST_users.Items.Clear();
            for (int i = 1; i <= Data_Keeper.file_system.super_block.cur_users_count; i++)
            {
                File_System.User? user = Data_Keeper.file_system.Read_user(i);
                if (user != null) LIST_users.Items.Add(user.Value);
                else throw new Exception();
            }
        }

        public void Refresh_tree_view()
        {
            Log.Write("FORM_Main | Refreshing tree view\n");

            TREE_view.Nodes.Clear();

            TREE_view.Nodes.Add("[/]", "[/]", 1, 1);
            FillNode(TREE_view.Nodes["[/]"]);
            TREE_view.Nodes["[/]"].ExpandAll();
        }
        private bool FillNode(TreeNode _node)
        {
            Log.Write($"FORM_Main | Filling node {_node.Name}\n");

            try
            {
                string path = _node.FullPath;
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                File_System.System_Directory directory = Data_Keeper.file_system.Get_directory(path).Value;
                foreach (File_System.Directory_Record record in directory)
                {
                    string name = record.name;
                    bool[] atributes = Data_Keeper.file_system.Get_atributes($"{path}\\{name}");
                    if (atributes == null) throw new Exception();
                    if (atributes[11] == true) continue;
                    if (atributes[0] == true && atributes[1] == false)
                    {
                        name = $"[{name}]";
                        _node.Nodes.Add(name, name, 1, 1);
                        FillNode(_node.Nodes[name]);
                    }
                    else
                    {
                        _node.Nodes.Add(name, name, 0, 0);
                    }
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Filling node {_node.Name} FAIL\n");
                return false;
            }

            Log.Write($"FORM_Main | Filling node {_node.Name} SUCCESS\n");
            return true;
        }

        private void Delete_object()
        {
            Log.Write("FORM_Main | Delete object clicked\n");

            string path = TREE_view.SelectedNode.FullPath;
            while (path.IndexOf('[') >= 0)
            {
                path = path.Remove(path.IndexOf('['), 1);
                path = path.Remove(path.IndexOf(']'), 1);
            }

            try
            {
                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                if (inode == null) { throw new Exception(); }

                if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 || inode.Value.owner_id == 0)
                {
                    MessageBox.Show("You can not delete this object");

                    Log.Write($"FORM_Main | User can not delete object\n");
                    return;
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Deleting ERROR at owner comparing\n");
                MessageBox.Show("Error");
                return;
            }

            if (MessageBox.Show("Accept action", "Object deleting", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Log.Write("FORM_Main | Deleting object ACCEPTED\n");

                if (Data_Keeper.file_system.Delete_system_object(path))
                {
                    Refresh_tree_view();
                    MessageBox.Show("Deleted");
                }
                else MessageBox.Show("Error");
            }
            else
            {
                Log.Write("FORM_Main | Deleting object CANCELED\n");

                MessageBox.Show("Canceled");
            }
        }
        private void Rename_object()
        {
            Log.Write("FORM_Main | Rename object clicked\n");

            string path = TREE_view.SelectedNode.FullPath;
            while (path.IndexOf('[') >= 0)
            {
                path = path.Remove(path.IndexOf('['), 1);
                path = path.Remove(path.IndexOf(']'), 1);
            }

            try
            {
                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                if (inode == null) { throw new Exception(); }

                if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true)
                {
                    MessageBox.Show("You can not rename this object");

                    Log.Write($"FORM_Main | User can not rename object\n");
                    return;
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Renaming ERROR at owner comparing\n");
                MessageBox.Show("Error");
                return;
            }

            FORM_Input input_dialog = new FORM_Input();
            if (input_dialog.ShowDialog(this, "New name") == DialogResult.OK)
            {
                Log.Write("FORM_Main | Renaming object ACCEPTED\n");
                try
                {
                    if (!Data_Keeper.file_system.Rename_system_object(path, input_dialog.TXT_input.Text)) throw new Exception();

                    Refresh_tree_view();
                    MessageBox.Show("Renamed");
                    Log.Write("FORM_Main | Rename object SUCCESS\n");
                }
                catch
                {
                    Refresh_tree_view();
                    MessageBox.Show("Error");
                    Log.Write("FORM_Main | Rename object ERROR\n");
                }
            }
            else
            {
                Log.Write("FORM_Main | Renaming object CANCELED\n");

                MessageBox.Show("Canceled");
            }
            input_dialog.Dispose();
        }
        private void Copy_object()
        {
            Log.Write("FORM_Main | Copy object clicked\n");

            string path = TREE_view.SelectedNode.FullPath;
            while (path.IndexOf('[') >= 0)
            {
                path = path.Remove(path.IndexOf('['), 1);
                path = path.Remove(path.IndexOf(']'), 1);
            }

            try
            {
                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                if (inode == null) { throw new Exception(); }

                if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 || inode.Value.owner_id == 0)
                {
                    MessageBox.Show("You can not copy this object");

                    Log.Write($"FORM_Main | User can not copy object\n");
                    return;
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Copying ERROR at owner comparing\n");
                MessageBox.Show("Error");
                return;
            }

            action_path = path;
            LBL_cur_action.Text = "Current action: copy";

            MessageBox.Show("Copied");
            Log.Write("FORM_Main | Copy object SUCCESS\n");
        }
        private void Cut_object()
        {
            Log.Write("FORM_Main | Cut object clicked\n");

            string path = TREE_view.SelectedNode.FullPath;
            while (path.IndexOf('[') >= 0)
            {
                path = path.Remove(path.IndexOf('['), 1);
                path = path.Remove(path.IndexOf(']'), 1);
            }

            try
            {
                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                if (inode == null) { throw new Exception(); }

                if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 || inode.Value.owner_id == 0)
                {
                    MessageBox.Show("You can not cut this object");

                    Log.Write($"FORM_Main | User can not cut object\n");
                    return;
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Cutting ERROR at owner comparing\n");
                MessageBox.Show("Error");
                return;
            }

            action_cut = true;
            action_path = path;
            LBL_cur_action.Text = "Current action: cut";

            MessageBox.Show("Cutted");
            Log.Write("FORM_Main | Cut object SUCCESS\n");
        }
        private void Paste_object()
        {
            Log.Write("FORM_Main | Paste object clicked\n");

            if (action_path == "")
            {
                MessageBox.Show("Nothing to paste");
                Log.Write("FORM_Main | Paste object nothing\n");
                return;
            }

            string path = TREE_view.SelectedNode.FullPath;
            while (path.IndexOf('[') >= 0)
            {
                path = path.Remove(path.IndexOf('['), 1);
                path = path.Remove(path.IndexOf(']'), 1);
            }

            try
            {
                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(Data_Keeper.file_system.Search_record_by_name(path).Value.inode);
                if (inode == null) { throw new Exception(); }

                if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true ||
                    (inode.Value.owner_id == 0 && new string(Data_Keeper.cur_user.login).Trim().Trim('\0') != path.Substring(2)))
                {
                    MessageBox.Show("You can not paste at this directory");

                    Log.Write($"FORM_Main | User can not paste at directory\n");
                    return;
                }
            }
            catch
            {
                Log.Write($"FORM_Main | Pasting ERROR at owner comparing\n");
                MessageBox.Show("Error");
                return;
            }

            try
            {
                if (!Data_Keeper.file_system.Copy_system_object(action_path, path)) throw new Exception();

                if (action_cut)
                    if (!Data_Keeper.file_system.Delete_system_object(action_path)) throw new Exception();

                Refresh_tree_view();
                MessageBox.Show("Pasted");
                Log.Write("FORM_Main | Cut object SUCCESS\n");
            }
            catch
            {
                Refresh_tree_view();
                MessageBox.Show("Error");
                Log.Write("FORM_Main | Cut object ERROR\n");
            }

            action_cut = false;
            action_path = "";
            LBL_cur_action.Text = "Current action: none";
        }
        private void Show_properties(string _sender)
        {
            Log.Write("FORM_Main | Show properties clicked\n");

            try
            {
                string path = "";
                if (_sender == "ToolStripMenuItem") path = $"{TREE_view.SelectedNode.FullPath}";
                else if (_sender == "Button")       path = TXT_search.Text;
                if (path == "") throw new Exception();
                while (path.IndexOf('[') >= 0)
                {
                    path = path.Remove(path.IndexOf('['), 1);
                    path = path.Remove(path.IndexOf(']'), 1);
                }

                File_System.Directory_Record? record = Data_Keeper.file_system.Search_record_by_name(path);
                if (record == null) { throw new Exception(); }

                File_System.Inode? inode = Data_Keeper.file_system.Read_inode(record.Value.inode);
                if (inode == null)  { throw new Exception(); }

                FORM_Properties properties_dialog = new FORM_Properties();
                properties_dialog.ShowDialog(this, path, inode.Value, record.Value.inode);

                Refresh_tree_view();
                properties_dialog.Dispose();

                Log.Write("FORM_Main | Showing properties SUCCESS\n");
            }
            catch
            {
                MessageBox.Show("Error");

                Log.Write("FORM_Main | Showing properties ERROR\n");
            }
        }

        #endregion FUNCTIONS
    }
}
