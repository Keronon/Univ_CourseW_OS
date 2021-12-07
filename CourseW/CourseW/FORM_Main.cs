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
            ToolStripMenuItem properties_directory_item = new ToolStripMenuItem("Properties");

            ToolStripMenuItem write_item = new ToolStripMenuItem("Write");
            ToolStripMenuItem append_item = new ToolStripMenuItem("Append");
            ToolStripMenuItem renew_item = new ToolStripMenuItem("Renew");
            write_item.DropDownItems.AddRange(new[] { append_item, renew_item });
            ToolStripMenuItem delete_file_item = new ToolStripMenuItem("Delete");
            ToolStripMenuItem properties_file_item = new ToolStripMenuItem("Properties");

            CONTEXT_directory.Items.AddRange(new[] { create_item, delete_directory_item, properties_directory_item });
            CONTEXT_file.Items.AddRange(new[] { write_item, delete_file_item, properties_file_item });

            create_file_item.Click += (o, e) =>
            {
                Log.Write("FORM_Main | Create file clicked\n");

                FORM_Input input_dialog = new FORM_Input();
                if (input_dialog.ShowDialog(this, "New file name") == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Creating file ACCEPTED\n");

                    string path = $"{TREE_view.SelectedNode.FullPath}\\{input_dialog.TXT_input.Text}";
                    while (path.IndexOf('[') >= 0)
                    {
                        path = path.Remove(path.IndexOf('['), 1);
                        path = path.Remove(path.IndexOf(']'), 1);
                    }

                    try
                    {
                        File_System.Inode? inode = Data_Keeper.file_system.Read_inode(
                                                        Data_Keeper.file_system.Search_record_by_name(path.Remove(path.LastIndexOf('\\'))).Value.inode);
                        if (inode == null) { throw new Exception(); }

                        if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true ||
                            (inode.Value.owner_id == 0 && new string(Data_Keeper.cur_user.login).Trim().Trim('\0') != path.Remove(path.LastIndexOf('\\')).Substring(2)))
                        {
                            MessageBox.Show("You can not create at this folder");

                            Log.Write($"FORM_Main | User can not create at folder\n");
                            return;
                        }
                    }
                    catch
                    {
                        Log.Write($"FORM_Main | Creating ERROR at owner comparing\n");
                        MessageBox.Show("Error");
                        return;
                    }

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

                FORM_Input input_dialog = new FORM_Input();
                if (input_dialog.ShowDialog(this, "New directory name") == DialogResult.OK)
                {
                    Log.Write("FORM_Main | Creating directory ACCEPTED\n");

                    string path = $"{TREE_view.SelectedNode.FullPath}\\{input_dialog.TXT_input.Text}";
                    while (path.IndexOf('[') >= 0)
                    {
                        path = path.Remove(path.IndexOf('['), 1);
                        path = path.Remove(path.IndexOf(']'), 1);
                    }

                    try
                    {
                        File_System.Inode? inode = Data_Keeper.file_system.Read_inode(
                                                        Data_Keeper.file_system.Search_record_by_name(path.Remove(path.LastIndexOf('\\'))).Value.inode);
                        if (inode == null) { throw new Exception(); }

                        if (Data_Keeper.cur_user.id != inode.Value.owner_id && Data_Keeper.cur_user.id != 1 && inode.Value.atributes[9] != true ||
                            (inode.Value.owner_id == 0 && new string(Data_Keeper.cur_user.login).Trim().Trim('\0') != path.Remove(path.LastIndexOf('\\')).Substring(2)))
                        {
                            MessageBox.Show("You can not create at this folder");

                            Log.Write($"FORM_Main | User can not create at folder\n");
                            return;
                        }
                    }
                    catch
                    {
                        Log.Write($"FORM_Main | Creating ERROR at owner comparing\n");
                        MessageBox.Show("Error");
                        return;
                    }

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
            delete_directory_item.Click += Delete_element;
            properties_directory_item.Click += Show_properties;

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
            delete_file_item.Click += Delete_element;
            properties_file_item.Click += Show_properties;

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

            Log.Write("FORM_Main | Initialized\n");
        }

        #region INTERFACE

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

            PANEL_change_password.Hide();
        }

        private void BTN_accept_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Accept Clicked\n");
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

        public void Set_user() { TXT_user.Text = new String(Data_Keeper.cur_user.login).TrimEnd(); }

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

        private void Delete_element(object o, EventArgs e)
        {
            Log.Write("FORM_Main | Delete element clicked\n");

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

            if (MessageBox.Show("Accept action", "Element deleting", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Log.Write("FORM_Main | Deleting element ACCEPTED\n");

                if (Data_Keeper.file_system.Delete_file_system_object(path))
                {
                    Refresh_tree_view();
                    MessageBox.Show("Deleted");
                }
                else MessageBox.Show("Error");
            }
            else
            {
                Log.Write("FORM_Main | Deleting element CANCELED\n");

                MessageBox.Show("Canceled");
            }
        }
        private void Show_properties(object o, EventArgs e)
        {
            Log.Write("FORM_Main | Show properties clicked\n");

            try
            {
                string path = $"{TREE_view.SelectedNode.FullPath}";
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
