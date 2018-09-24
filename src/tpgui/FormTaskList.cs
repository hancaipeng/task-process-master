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
            f.updateoradd = "add";
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                Listview(TaskFile.Tasks);
            }
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            FormTaskEdit f = new FormTaskEdit();
            if (listView1.SelectedItems.Count == 1)
            {
                int a = listView1.SelectedItems[0].Index;
                TaskFile.UpdateLine = a;
                for (int i = 0; i < 13; i++)
                {
                    f.updatetask[i] = this.listView1.Items[a].SubItems[i].Text;
                }
                f.updateoradd = "update";
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选中一组数据");
            }
            if (f.DialogResult == DialogResult.OK)
            {
                listView1.Items[TaskFile.UpdateLine].SubItems[7].Text = TaskFile.Tasks[TaskFile.UpdateLine].Assignee;
                listView1.Items[TaskFile.UpdateLine].SubItems[10].Text = TaskFile.Tasks[TaskFile.UpdateLine].DueTime.ToString("yy/MM/dd");
                listView1.Items[TaskFile.UpdateLine].SubItems[3].Text = TaskFile.Tasks[TaskFile.UpdateLine].Content;
                switch (Enum.GetName(typeof(TaskPriority), TaskFile.Tasks[TaskFile.UpdateLine].Priority))
                {
                    case "High":
                        listView1.Items[TaskFile.UpdateLine].ForeColor = Color.FromArgb(255, 0, 0);
                        break;
                    case "Middle":
                        listView1.Items[TaskFile.UpdateLine].ForeColor = Color.FromArgb(178, 34, 34);
                        break;
                    case "Normal":
                        listView1.Items[TaskFile.UpdateLine].ForeColor = Color.FromArgb(0, 0, 0);
                        break;
                    case "Low":
                        listView1.Items[TaskFile.UpdateLine].ForeColor = Color.FromArgb(0, 100, 0);
                        break;
                }
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
            if (string.IsNullOrEmpty(file) == true)
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
            if (string.IsNullOrEmpty(file)==false)
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
                switch (Enum.GetName(typeof(TaskStatus), x.Status))
                {
                    case "NotStart":
                        lvi.SubItems.Add("未着手");
                        break;
                    case "Handling":
                        lvi.SubItems.Add("作业中");
                        break;
                    case "Finished":
                        lvi.SubItems.Add("完成");
                        break;
                    case "Accept":
                        lvi.SubItems.Add("已经确认");
                        break;
                    case "Reject":
                        lvi.SubItems.Add("被驳回");
                        break;
                }
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
                    case "High":
                        lvi.ForeColor = Color.FromArgb(255, 0, 0);
                        break;
                    case "Middle":
                        lvi.ForeColor = Color.FromArgb(178, 34, 34);
                        break;
                    case "Normal":
                        lvi.ForeColor = Color.FromArgb(0, 0, 0);
                        break;
                    case "Low":
                        lvi.ForeColor = Color.FromArgb(0, 100, 0);
                        break;
                }
                if (Enum.GetName(typeof(TaskStatus), x.Status) == "Finished")
                {
                    lvi.ForeColor = Color.FromArgb(128, 128, 128);
                }
                if (DateTime.Now.Day - x.CheckTime.Day <= 2 && Enum.GetName(typeof(TaskStatus), x.Status) != "Finished")
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
            if (this.listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    TaskFile tf = new TaskFile();
                    tf.DeleteTask(listView1.SelectedItems[i].SubItems[11].Text);
                }
                Listview(TaskFile.Tasks);
            }
            else
            {
                MessageBox.Show("请先选择需要删除的项目");
            }
        }

        private void 高ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoicePriority((TaskPriority)Enum.Parse(typeof(TaskPriority), "0"));
        }

        private void 中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoicePriority((TaskPriority)Enum.Parse(typeof(TaskPriority), "1"));
        }

        private void 一般ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoicePriority((TaskPriority)Enum.Parse(typeof(TaskPriority), "2"));
        }

        private void 低ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChoicePriority( (TaskPriority)Enum.Parse(typeof(TaskPriority), "3"));
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savefile();
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void ChoicePriority(TaskPriority priority)
        {
            TaskFile tf = new TaskFile();
            if (this.listView1.SelectedItems.Count > 0)
            {
                int b = listView1.SelectedItems.Count;
                for (int i = 0; i < b; i++)
                {
                    tf.UpdatePriority(listView1.SelectedItems[i].SubItems[11].Text, priority);

                }
                Listview(TaskFile.Tasks);
            }
        }
    }
}
