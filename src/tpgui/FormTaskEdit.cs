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
            if (Program.Tasks == null)
            {
                MessageBox.Show("请先打开文件");
            }
            else
            {
                TaskFile tf = new TaskFile();
                string str = " ";
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is RadioButton)
                    {
                        if ((c as RadioButton).Checked)
                        {
                            str = c.Text;
                        }
                    }
                }
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
                        tf.Addtask(str, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"), textBox2.Text.ToString(), textBox1.Text.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show("添加成功");
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
    }
}
