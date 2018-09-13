using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xworks.taskprocess
{
	class TaskFile
	{
		public List<Task> LoadTasks(string filePath)
		{
			List<Task> Tasks = new List<Task>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);  //加载Xml文件 
            XmlElement rootElem = doc.DocumentElement;  //获取根节点
            XmlNodeList RowpersonNodes = rootElem.GetElementsByTagName("task"); 
            foreach (XmlElement node in RowpersonNodes)
            {
                Task task = new Task();
                task.Id = Guid.Parse( node.Attributes["id"].Value);
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
                Tasks.Add(task);

            }
            return Tasks;
		}
	}
}
