using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xworks.taskprocess
{
	enum TaskStatus
	{
		为着手= 0,//未着手
		作业中,//作业中
		完成,//完成
		已经确认,//已经确认
		被驳回,//被驳回
	}
}
