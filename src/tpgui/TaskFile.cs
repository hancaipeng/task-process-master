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
        public static List<Task> Tasks { get; set; }
        public static int UpdateLine { get; set; }
        public const string NODE_NAME_TASK = "task";
		public List<Task> LoadTasks(string filePath)
		{
            Tasks  = new List<Task>();
            if (IsXml(filePath) != null)
            {
                XmlDocument doc = new XmlDocument();
                doc= IsXml(filePath);
                XmlElement rootElem = doc.DocumentElement;
                XmlNodeList rowpersonnodes = rootElem.GetElementsByTagName(NODE_NAME_TASK);
                foreach (XmlElement node in rowpersonnodes)
                {
                    Task task = new Task();
                    if (CheckNodeExists(filePath,doc) == true)
                    {
                        if (Datecheck(node.SelectSingleNode("SubmitTime").InnerText) == true && Datecheck(node.SelectSingleNode("SubmitTime").InnerText) == true
                            && Datecheck(node.SelectSingleNode("CheckTime").InnerText) == true&&Datecheck(node.SelectSingleNode("FinishTime").InnerText)==true)
                        {
                            if (Enum.IsDefined(typeof(TaskPriority), (TaskPriority)Enum.Parse(typeof(TaskPriority), node.SelectSingleNode("Priority").InnerText)) == true
                                && Enum.IsDefined(typeof(TaskStatus), (TaskStatus)Enum.Parse(typeof(TaskStatus), node.SelectSingleNode("Status").InnerText)) == true)
                            {
                                task.Id = node.Attributes["id"].Value;
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
                                Tasks.Add(task);
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
                        MessageBox.Show(node.Attributes["id"].Value + "节点不对");
                    }
                }
                return Tasks;
            }
            else
            {
                MessageBox.Show("文件格式不对");
                return Tasks;
            }
		}
        
        bool Datecheck(string checkdate)
        {
            return DateTime.TryParseExact(checkdate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime result);
        }

        bool CheckNodeExists(string filepath, XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;
            if (root.SelectSingleNode(NODE_NAME_TASK) != null)
            {
                XmlNodeList rowpersonnodes = root.GetElementsByTagName(NODE_NAME_TASK);
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
                    XmlNode status = node.SelectSingleNode("Status");
                    if (node.Attributes["id"] == null || author == null || submittime == null 
                        || priority == null || duetime == null || assignee == null || content == null || status == null)
                    {

                    }
                    else
                    {
                       bo.Add(false);
                    }
                }
                return bo1;
            }
            else
            {
                return false;
            }
        }

        XmlDocument IsXml(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            string strXml = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                return xml;
            }
            catch
            {
                return null;
            }
        }

        public void Savefile(String filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", ""));
            XmlElement root = doc.CreateElement(NODE_NAME_TASK);
            if (Tasks == null)
            {
                MessageBox.Show("没有可保存的文件");
            }
            else
            {
                foreach (Task x in Tasks)
                {
                    XmlElement node = doc.CreateElement("task");
                    node.SetAttribute("id", x.Id.ToString());
                    XmlElement author = doc.CreateElement("Author");
                    author.InnerText = x.Author;
                    node.AppendChild(author);
                    XmlElement submittime = doc.CreateElement("SubmitTime");
                    submittime.InnerText = x.SubmitTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(submittime);
                    XmlElement priority = doc.CreateElement("Priority");
                    priority.InnerText = ((int)Enum.Parse(typeof(TaskPriority), x.Priority.ToString())).ToString();
                    node.AppendChild(priority);
                    XmlElement duetime = doc.CreateElement("DueTime");
                    duetime.InnerText = x.DueTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(duetime);
                    XmlElement assignee = doc.CreateElement("Assignee");
                    assignee.InnerText = x.Assignee;
                    node.AppendChild(assignee);
                    XmlElement content = doc.CreateElement("Content");
                    content.InnerText = x.Content;
                    node.AppendChild(content);
                    XmlElement handlingnote = doc.CreateElement("HandlingNote");
                    handlingnote.InnerText = x.HandlingNote;
                    node.AppendChild(handlingnote);
                    XmlElement status = doc.CreateElement("Status");
                    status.InnerText =((int)Enum.Parse(typeof(TaskStatus), x.Status.ToString())).ToString();
                    node.AppendChild(status);
                    XmlElement checker = doc.CreateElement("Checker");
                    checker.InnerText = x.Checker;
                    node.AppendChild(checker);
                    XmlElement checktime = doc.CreateElement("CheckTime");
                    checktime.InnerText = x.CheckTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(checktime);
                    XmlElement finishtime = doc.CreateElement("FinishTime");
                    finishtime.InnerText = x.FinishTime.ToString("yyyyMMddHHmmss");
                    node.AppendChild(finishtime);
                    root.AppendChild(node);
                }
            }
            doc.AppendChild(root);
            doc.Save(filepath);
        }

        public void Addtask(TaskPriority priority,string duetime,string assignee,string content,string submittime)
        {
            Task task = new Task
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = "张三",
                    SubmitTime = DateTime.ParseExact(submittime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    Priority = priority,
                    DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    Assignee = assignee,
                    Content = content,
                    HandlingNote = "",
                    Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), "0"),
                    Checker = "",
                    CheckTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                    FinishTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture)
                };
                Tasks.Add(task);
         
        }
        
        public void DeleteTask(string id)
        {
            for(int i = Tasks.Count - 1; i >= 0; i--)
            {
                if (Tasks[i].Id ==id)
                {
                    Tasks.Remove(Tasks[i]);
                }
            }
        }

        public void UpdateTask(string id, TaskPriority priority, string duetime, string assignee, string content)
        {
            foreach(Task x in Tasks)
            {
                if (x.Id == id)
                {
                    x.Priority =  priority;
                    x.DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    x.Assignee = assignee;
                    x.Content = content;
                }
            }
        }

        public void UpdatePriority(string id, TaskPriority priority)
        {
            foreach (Task x in Tasks)
            {
                if (x.Id == id)
                {
                    x.Priority = priority;
                }
            }
        }
        
    }
}
