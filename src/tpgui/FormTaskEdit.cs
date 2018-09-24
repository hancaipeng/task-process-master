using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xworks.taskprocess
{
    public partial class FormTaskEdit : Form
    {
        ListViewItem oldItem = new ListViewItem();
        TaskPriority priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Normal");
        public string updateoradd;
        public string[] updatetask = new string[13];
        public FormTaskEdit()
		{
			InitializeComponent();
		}

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
   
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (TaskFile.Tasks == null)
            {
                MessageBox.Show("请先打开文件");
            }
            else
            {
                TaskFile tf = new TaskFile();
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("作业者和详细不能为空");
                }
                else
                {
                    if (dateTimePicker1.Value < DateTime.Now)
                    {
                        MessageBox.Show("预定日应该为今天以后的日期");
                    }
                    else
                    {
                        switch (updateoradd)
                        {
                            case "add":
                                tf.Addtask(priority, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"), textBox2.Text, textBox1.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                                break;
                            case "update":
                                tf.UpdateTask(updatetask[11].ToString(), priority, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"), textBox2.Text.ToString(), textBox1.Text.ToString());
                                break;
                        }
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show("编辑成功");
                        this.Close();
                    }
                }
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要退出吗?", "退出编辑", messButton);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void FormTaskEdit_Load(object sender, EventArgs e)
        {
            string prioritycolor="";
            switch (updatetask[12])
            {
                case "High":
                    prioritycolor = "高";
                    break;
                case "Middle":
                    prioritycolor = "中";
                    break;
                case "Normal":
                    prioritycolor = "普通";
                    break;
                case "Low":
                    prioritycolor = "低";
                    break;
            }
            if (updateoradd == "update")
            {
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is RadioButton)
                    {
                        if ((c as RadioButton).Text == prioritycolor)
                        {
                            (c as RadioButton).Checked = true;
                        }
                    }
                }
                dateTimePicker1.Value = DateTime.ParseExact(updatetask[10], "yy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
                textBox2.Text = updatetask[7];
                textBox1.Text = updatetask[3];
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "High");
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Middle");
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Low");
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Normal");
        }


        private void FormTaskEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("你确定要关闭此窗体么？", "关闭提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
