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
    public partial class FormTaskUpadte : Form
    {
        public string[] str = new string[13];
        TaskPriority priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Normal");
        public FormTaskUpadte()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TaskFile tf = new TaskFile();
            if (textBox1.Text.ToString() == "" || textBox2.Text.ToString() == "")
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
                    tf.UpdateTask(str[11].ToString(), priority, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"),textBox2.Text.ToString(),textBox1.Text.ToString());
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("修改成功");
                    this.Close();
                }
            }
        }

        private void FormTaskUpadte_Load(object sender, EventArgs e)
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is RadioButton)
                {
                    if ((c as RadioButton).Text==str[12])
                    {
                        (c as RadioButton).Checked = true;
                    }
                }
            }
            dateTimePicker1.Value = DateTime.ParseExact(str[10], "yy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
            textBox2.Text = str[7];
            textBox1.Text = str[3];
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

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "High");
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Middle");
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Normal");
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Low");
        }
    }
}
