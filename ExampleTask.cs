using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WFMyApp
{
    public class ExampleTask//:Task
    {
        
        public Task task;
        public Priority priority;



        public ExampleTask(Task _task, Priority _priority)
        {
            this.task = _task;
            this.priority = _priority;
        }
        //public ExampleTask(Action action, Priority priority)
        //    :base(action)
        //{
        //    this.priority = priority;
        //}

        public void Execute()
        {
            task.Start();
        }
    }
}
