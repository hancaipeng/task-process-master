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
            TaskFile tf = new TaskFile();
            string file="";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*xml*)|*.xml*"; //设置要选择的文件的类型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//返回文件的完整路径      
            }
            listView1.Items.Clear();
            List<Task> tasks = tf.LoadTasks(file);
            int i =0;
            foreach(Task x in tasks)
            {
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = i.ToString();
                lvi.SubItems.Add(x.Author);
                lvi.SubItems.Add(x.SubmitTime.ToString());
                //lvi.SubItems.Add(x.DueTime.ToString());
                lvi.SubItems.Add(x.Content);
                lvi.SubItems.Add(x.HandlingNote);
                lvi.SubItems.Add(Enum.GetName(typeof(TaskStatus),x.Status));
                lvi.SubItems.Add("");
                lvi.SubItems.Add(x.Assignee);
                lvi.SubItems.Add(x.CheckTime.ToString());
                lvi.SubItems.Add(x.Checker);
                listView1.Items.Add(lvi);
            }
        }

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			FormTaskEdit f = new FormTaskEdit();
			f.ShowDialog();
		}

		private void toolStripButton8_Click(object sender, EventArgs e)
		{
			FormTaskEdit f = new FormTaskEdit();
			f.ShowDialog();
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
			FormTaskProcess f = new FormTaskProcess();
			f.ShowDialog();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			FormTaskConfirm f = new FormTaskConfirm();
			f.ShowDialog();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			FormLinkFile f = new FormLinkFile();
			f.ShowDialog();
		}

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskFile tf = new TaskFile();
            string file = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*xml*)|*.xml*"; //设置要选择的文件的类型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//返回文件的完整路径      
            }
            listView1.Items.Clear();
            List<Task> tasks = tf.LoadTasks(file);
            int i = 0;
            foreach (Task x in tasks)
            {
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = i.ToString();
                lvi.SubItems.Add(x.Author);
                lvi.SubItems.Add(x.SubmitTime.ToString());
                //lvi.SubItems.Add(x.DueTime.ToString());
                lvi.SubItems.Add(x.Content);
                lvi.SubItems.Add(x.HandlingNote);
                lvi.SubItems.Add(Enum.GetName(typeof(TaskStatus), x.Status));
                lvi.SubItems.Add("");
                lvi.SubItems.Add(x.Assignee);
                lvi.SubItems.Add(x.CheckTime.ToString());
                lvi.SubItems.Add(x.Checker);
                listView1.Items.Add(lvi);
            }
        }
    }
}
