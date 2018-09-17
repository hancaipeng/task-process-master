using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xworks.taskprocess
{
	class TaskFile
	{
		public List<Task> LoadTasks(string filePath)
		{
            Program.Tasks  = new List<Task>();
            if (IsXml(filePath) == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlElement rootElem = doc.DocumentElement;
                XmlNodeList rowpersonnodes = rootElem.GetElementsByTagName("task");
                foreach (XmlElement node in rowpersonnodes)
                {
                    Task task = new Task();
                    if (NodeCheck(filePath) == true)
                    {
                        if (Datecheck(node.SelectSingleNode("SubmitTime").InnerText) == true && Datecheck(node.SelectSingleNode("SubmitTime").InnerText) == true && Datecheck(node.SelectSingleNode("CheckTime").InnerText) == true&&Datecheck(node.SelectSingleNode("FinishTime").InnerText)==true)
                        {
                            if (Enum.IsDefined(typeof(TaskPriority), (TaskPriority)Enum.Parse(typeof(TaskPriority), node.SelectSingleNode("Priority").InnerText)) == true && Enum.IsDefined(typeof(TaskStatus), (TaskStatus)Enum.Parse(typeof(TaskStatus), node.SelectSingleNode("Status").InnerText)) == true)
                            {
                                task.Id = Guid.Parse(node.Attributes["id"].Value);
                                task.Author = node.SelectSingleNode("Author").InnerText;
                                task.SubmitTime = DateTime.ParseExact(node.SelectSingleNode("SubmitTime").InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                task.Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), node.SelectSingleNode("Priority").InnerText);
                                task.DueTime = DateTime.ParseExact(node.SelectSingleNode("DueTime").InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                task.Assignee = node.SelectSingleNode("Assignee").InnerText;
                                task.Content = node.SelectSingleNode("Content").InnerText;
                                task.HandlingNote = node.SelectSingleNode("HandlingNote").InnerText;
                                task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), node.SelectSingleNode("Status").InnerText);
                                task.Checker = node.SelectSingleNode("Checker").InnerText;
                                task.CheckTime = DateTime.ParseExact(node.SelectSingleNode("CheckTime").InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                task.FinishTime = DateTime.ParseExact(node.SelectSingleNode("FinishTime").InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                               Program.Tasks.Add(task);
                            }
                            else
                            {
                                MessageBox.Show(node.Attributes["id"].Value + "枚举类型不对");
                            }
                        }
                        else
                        {
                            MessageBox.Show(node.Attributes["id"].Value + "时间格式不对");
                        }
                    }
                    else
                    {
                        MessageBox.Show("节点不对");
                    }
                }
                return Program.Tasks;
            }
            else
            {
                MessageBox.Show("文件格式不对");
                return Program.Tasks;
            }
		}
        
        bool Datecheck(string checkdate)
        {
            return DateTime.TryParseExact(checkdate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result);
        }

        bool NodeCheck(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlElement root = doc.DocumentElement;
            if (root.SelectSingleNode("task") != null)
            {
                XmlNodeList rowpersonnodes = root.GetElementsByTagName("task");
                List<bool> bo = new List<bool>();
                bool bo1 = true;
                foreach (XmlElement node in rowpersonnodes)
                {
                    XmlNode author = node.SelectSingleNode("Author");
                    XmlNode submittime = node.SelectSingleNode("SubmitTime");
                    XmlNode priority = node.SelectSingleNode("Priority");
                    XmlNode duetime = node.SelectSingleNode("DueTime");
                    XmlNode assignee = node.SelectSingleNode("Assignee");
                    XmlNode content = node.SelectSingleNode("Content");
                    XmlNode handlingnote = node.SelectSingleNode("HandlingNote");
                    XmlNode status = node.SelectSingleNode("Status");
                    XmlNode checker = node.SelectSingleNode("Checker");
                    XmlNode checktime = node.SelectSingleNode("CheckTime");
                    if (node.Attributes["id"] != null && author != null && submittime != null && priority != null && duetime != null && assignee != null && content != null && handlingnote != null && status != null && checker != null && checktime != null)
                    {
                        bo.Add(true);
                    }
                    else
                    {
                       bo.Add(false);
                    }
                }
                foreach(bool n in bo)
                {
                    if (n == false)
                    {
                        bo1 = false;
                    }
                }

                return bo1;
            }
            else
            {
                return false;
            }
           

        }
 
        bool IsXml(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string strXml = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                return true;
            }
            catch
            {
                return false;

            }
        }

        public void Savefile(String filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", ""));
            XmlElement root = doc.CreateElement("tasks");
            if (Program.Tasks == null)
            {
                MessageBox.Show("没有可保存的文件");
            }
            else
            {
                foreach (Task x in Program.Tasks)
                {
                    XmlElement node = doc.CreateElement("task");
                    node.SetAttribute("id", x.Id.ToString());
                    XmlElement e0 = doc.CreateElement("Author");
                    e0.InnerText = x.Author;
                    node.AppendChild(e0);
                    XmlElement e1 = doc.CreateElement("SubmitTime");
                    e1.InnerText = x.SubmitTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(e1);
                    XmlElement e2 = doc.CreateElement("Priority");
                    e2.InnerText = ((int)Enum.Parse(typeof(TaskPriority), x.Priority.ToString())).ToString();
                    node.AppendChild(e2);
                    XmlElement e3 = doc.CreateElement("DueTime");
                    e3.InnerText = x.DueTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(e3);
                    XmlElement e4 = doc.CreateElement("Assignee");
                    e4.InnerText = x.Assignee;
                    node.AppendChild(e4);
                    XmlElement e5 = doc.CreateElement("Content");
                    e5.InnerText = x.Content;
                    node.AppendChild(e5);
                    XmlElement e6 = doc.CreateElement("HandlingNote");
                    e6.InnerText = x.HandlingNote;
                    node.AppendChild(e6);
                    XmlElement e7 = doc.CreateElement("Status");
                    e7.InnerText =((int)Enum.Parse(typeof(TaskStatus), x.Status.ToString())).ToString();
                    node.AppendChild(e7);
                    XmlElement e8 = doc.CreateElement("Checker");
                    e8.InnerText = x.Checker;
                    node.AppendChild(e8);
                    XmlElement e9 = doc.CreateElement("CheckTime");
                    e9.InnerText = x.CheckTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(e9);
                    XmlElement e10 = doc.CreateElement("FinishTime");
                    e10.InnerText = x.FinishTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(e10);
                    root.AppendChild(node);
                }
            }
            doc.AppendChild(root);
            doc.Save(filepath);
        }

        public void Addtask(string priority,string duetime,string assignee,string content,string submittime)
        {
            Task task = new Task
                {
                    Id = Guid.NewGuid(),
                    Author = "张三",
                    SubmitTime = DateTime.ParseExact(submittime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), priority),
                    DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    Assignee = assignee,
                    Content = content,
                    HandlingNote = "",
                    Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), "0"),
                    Checker = "",
                    CheckTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    FinishTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture)
                };
                Program.Tasks.Add(task);
         
        }
        
        public void Delet(string id)
        {
            for(int i = Program.Tasks.Count - 1; i >= 0; i--)
            {
                if (Program.Tasks[i].Id == Guid.Parse(id))
                {
                    Program.Tasks.Remove(Program.Tasks[i]);
                }
            }
        }

        public void Update(string id, string priority, string duetime, string assignee, string content)
        {
            foreach(Task x in Program.Tasks)
            {
                if (x.Id == Guid.Parse(id))
                {
                    x.Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), priority);
                    x.DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    x.Assignee = assignee;
                    x.Content = content;
                }
            }
        }

        public void UpdatePriority(string id,string priority)
        {

            foreach (Task x in Program.Tasks)
            {
                if (x.Id == Guid.Parse(id))
                {
                    x.Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), priority);
                }
            }
        }
        
    }
}
