using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xworks.taskprocess
{
    public partial class FormTaskList : Form
    {
        public FormTaskList()
        {
            InitializeComponent();
        }

		private void _toolStripButtonOpen_Click(object sender, EventArgs e)
		{
            Fileopen();
        }

		private void ToolStripButton2_Click(object sender, EventArgs e)
		{
			FormTaskEdit f = new FormTaskEdit();
			f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                Listview(Program.Tasks);
            }
        }

		private void ToolStripButton8_Click(object sender, EventArgs e)
		{
			FormTaskUpadte f = new FormTaskUpadte();
            try
            {
                if (listView1.SelectedItems.Count > 1)
                {
                    MessageBox.Show("请选中一组数据");
                }
                else
                {
                    int a = listView1.FocusedItem.Index;
                    for (int i = 0; i < 13; i++)
                    {
                        f.str[i] = this.listView1.Items[a].SubItems[i].Text;
                    }
                    f.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请选中一组数据");
            }
            if (f.DialogResult == DialogResult.OK)
            {
                Listview(Program.Tasks);
            }
        }

		private void ToolStripButton7_Click(object sender, EventArgs e)
		{
			FormTaskProcess f = new FormTaskProcess();
			f.ShowDialog();
		}

		private void ToolStripButton1_Click(object sender, EventArgs e)
		{
			FormTaskConfirm f = new FormTaskConfirm();
			f.ShowDialog();
		}

		private void ToolStripButton3_Click(object sender, EventArgs e)
		{
			FormLinkFile f = new FormLinkFile();
			f.ShowDialog();
		}

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fileopen();
        }
        private void Fileopen()
        {
            TaskFile tf = new TaskFile();
            string file = "";
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "请选择文件",
                Filter = "所有文件(*xml*)|*.xml*" 
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;    
            }
            if (file == "")
            {
                MessageBox.Show("请选择文件");
            }
            else
            {
                List<Task> tasks = tf.LoadTasks(file);
                Listview(tasks);
            }
        }
        
        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            Savefile();
        }

        private void Savefile()
        {
            TaskFile tf = new TaskFile();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "请选择保存路径",
                Filter = @"XML文件|*.xml"
            };
            sfd.ShowDialog();
            string file = sfd.FileName;
            if (file != "")
            {
                tf.Savefile(file);
            }
        }

        private void Listview(List<Task> tasks)
        {
            listView1.Items.Clear();
            int i = 0;
            foreach (Task x in tasks)
            {
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = "#" + i.ToString();
                lvi.SubItems.Add(x.Author);
                lvi.SubItems.Add(x.SubmitTime.ToString("yy/MM/dd"));
                lvi.SubItems.Add(x.Content);
                lvi.SubItems.Add(x.HandlingNote);
                lvi.SubItems.Add(Enum.GetName(typeof(TaskStatus), x.Status));
                if (x.FinishTime.ToString("yyyy/MM/dd") == "1900/01/01")
                {
                    lvi.SubItems.Add("");
                }
                else
                {
                    lvi.SubItems.Add(x.FinishTime.ToString("yy/MM/dd"));
                }
                lvi.SubItems.Add(x.Assignee);
                if (x.CheckTime.ToString("yyyy/MM/dd") == "1900/01/01")
                {
                    lvi.SubItems.Add("");
                }
                else
                {
                    lvi.SubItems.Add(x.CheckTime.ToString("yy/MM/dd"));
                }
                lvi.SubItems.Add(x.Checker);
                lvi.SubItems.Add(x.DueTime.ToString("yy/MM/dd"));
                switch (Enum.GetName(typeof(TaskPriority), x.Priority))
                {
                    case "高":
                        lvi.ForeColor = Color.FromArgb(255, 0, 0);
                        break;
                    case "中":
                        lvi.ForeColor = Color.FromArgb(178, 34, 34);
                        break;
                    case "普通":
                        lvi.ForeColor = Color.FromArgb(0, 0, 0);
                        break;
                    case "低":
                        lvi.ForeColor = Color.FromArgb(0, 100, 0);
                        break;

                }
                if (Enum.GetName(typeof(TaskStatus), x.Status) == "完成")
                {
                    lvi.ForeColor = Color.FromArgb(128, 128, 128);
                }
                if (DateTime.Now.Day - x.CheckTime.Day <= 2 && Enum.GetName(typeof(TaskStatus), x.Status) != "完成")
                {
                    lvi.BackColor = Color.FromArgb(240, 230, 140);
                }
                lvi.SubItems.Add(x.Id.ToString());
                lvi.SubItems.Add(Enum.GetName(typeof(TaskPriority), x.Priority));
                listView1.Items.Add(lvi);
            }
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        TaskFile tf = new TaskFile();
                        tf.Delet(listView1.SelectedItems[i].SubItems[11].Text);
                     
                    }
                }
                Listview(Program.Tasks);

            }
            catch (Exception )
            {
                MessageBox.Show("请先选择需要删除的项目");
            }
            
        }

        private void 高ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TaskFile tf = new TaskFile();
                if (this.listView1.SelectedItems.Count > 0)
                {
                    int b = listView1.SelectedItems.Count;
                    for (int i = 0; i < b; i++)
                    {
                        tf.UpdatePriority(listView1.SelectedItems[i].SubItems[11].Text, (TaskPriority)Enum.Parse(typeof(TaskPriority), "0"));
                      
                    }
                    Listview(Program.Tasks);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void 中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TaskFile tf = new TaskFile();
                if (this.listView1.SelectedItems.Count > 0)
                {
                    int b = listView1.SelectedItems.Count;
                    for (int i = 0; i < b; i++)
                    {
                        tf.UpdatePriority(listView1.SelectedItems[i].SubItems[11].Text, (TaskPriority)Enum.Parse(typeof(TaskPriority), "1"));

                    }
                    Listview(Program.Tasks);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
        }

        private void 一般ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TaskFile tf = new TaskFile();
                if (this.listView1.SelectedItems.Count > 0)
                {
                    int b = listView1.SelectedItems.Count;
                    for (int i = 0; i < b; i++)
                    {
                        tf.UpdatePriority(listView1.SelectedItems[i].SubItems[11].Text, (TaskPriority)Enum.Parse(typeof(TaskPriority), "2"));

                    }
                    Listview(Program.Tasks);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void 低ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoicePriority(listView1.SelectedItems[i].SubItems[11].Text, (TaskPriority)Enum.Parse(typeof(TaskPriority), "3"));
            try
            {
                TaskFile tf = new TaskFile();
                if (this.listView1.SelectedItems.Count > 0)
                {
                    int b = listView1.SelectedItems.Count;
                    for (int i = 0; i < b; i++)
                    {
                        tf.UpdatePriority(listView1.SelectedItems[i].SubItems[11].Text, (TaskPriority)Enum.Parse(typeof(TaskPriority), "3"));

                    }
                    Listview(Program.Tasks);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savefile();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void ChoicePriority(string id, TaskPriority priority, SelectedItems selectedItems)
        {
            try
            {
                TaskFile tf = new TaskFile();
                if (this.listView1.SelectedItems.Count > 0)
                {
                    int b = listView1.SelectedItems.Count;
                    for (int i = 0; i < b; i++)
                    {
                        tf.UpdatePriority(id,priority);

                    }
                    Listview(Program.Tasks);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
