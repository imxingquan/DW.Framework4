using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace DW.Framework.Utils
{
    public enum WorkThreadStatus
    {
        Stop,
        Starting,
        Error
    }

    public class WorkThread
    {
        public delegate void WorkEvent(object sender,WorkEventArgs e);
        public event WorkEvent Work;

        public WorkThreadStatus Status = WorkThreadStatus.Stop;

        //当前用户
        private int _userID;
        public int UserID
        {
            set
            {
                _userID = value;
            }
            get { return _userID; }
        }

        //接收人列表
        private List<Pair> _userList;
        public List<Pair> UserList
        {
            set
            {
                _userList = value;
            }
            get
            {
                return _userList;
            }
        }

        //处理信息
        private string _process_msg = "";
        public string ProcessMsg
        {
            set
            {
                _process_msg = value;
            }
            get
            {
                return _process_msg;
            }
        }

       
        //参数
        public class WorkEventArgs : EventArgs
        {
            public WorkThreadStatus status = WorkThreadStatus.Stop;
            public int userId = 0;
            public List<Pair> userList;
            public bool isAllSend;
            public WorkEventArgs(WorkThreadStatus status,int userId,List<Pair> userList)
            {
                this.status = status;
                this.userId = userId;
                this.userList = userList;
                
            }


        }


        public WorkThread()
        {
        }

        
        public void Run()
        {
            lock (this)
            {
                if (Status == WorkThreadStatus.Stop)
                {


                    Status = WorkThreadStatus.Starting;
                    System.Threading.Thread thread  = new System.Threading.Thread(new System.Threading.ThreadStart(_work));
                    //thread.Name = "work";
                    thread.Start();
                }
            }
        }

        protected void _work()
        {
            if (Work != null)
            {
                WorkEventArgs e = new WorkEventArgs(this.Status,this._userID,this._userList);
                Work(this,e);
                Status = e.status;
               
            }
        }
    }
}
