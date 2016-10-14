using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WFMyApp
{
    public class FixedThreadPool
    {
        Queue<ExampleTask> lTask = new Queue<ExampleTask>();
        //private 
        public Queue<ExampleTask> highPriority;
        //private 
        public Queue<ExampleTask> normalPriority;
        //private 
        public Queue<ExampleTask> lowPriority;
        bool stop = true;
        int nextNORMAL;
        private int countThread;
        private List<ExampleTask> threadQueue;
        private Task workTask;

        public FixedThreadPool(int n)
        {
            countThread = n;
            highPriority = new Queue<ExampleTask>();
            normalPriority = new Queue<ExampleTask>();
            lowPriority = new Queue<ExampleTask>();
            threadQueue = new List<ExampleTask>();
            nextNORMAL = 0;
            workTask = new Task(Work);
            workTask.Start();
        }

        public bool Execute(Task task,Priority priority)
        {
            if (stop)
            {
                EnterExampleTask(new ExampleTask( task ,priority));
                return stop;
            }
            return stop;
        }

        private void ThreadWork()
        {
            if (threadQueue.Count == countThread)
            {
                return;
            }
            else
            { 
                if (lTask.Count!=0)
                {
                    ExampleTask exampleTask = lTask.Dequeue();
                    //exampleTask.task.Status
                    exampleTask.task.Start();
                    threadQueue.Add(exampleTask);
                    
                }
            }
            
        }

        private void EnterExampleTask(ExampleTask exTask)
        {
            switch (exTask.priority)
            {
                case Priority.HIGH:
                    highPriority.Enqueue(exTask);
                    break;
                case Priority.NORMAL:
                    normalPriority.Enqueue(exTask);
                    break;
                case Priority.LOW:
                    lowPriority.Enqueue(exTask);
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            this.stop = false;
        }

        private void Action()
        {
            
        }
        
        // метод для удаления из списка выполняемых потоков
        //передается 2 списка, один со списком потоков, второй со списком элементов которые надо удалить 
        private void DeleteExampleThread(List<ExampleTask> exTask, List<int> lInt)
        {
            for (int i = lInt.Count - 1; i >= 0; i--)
            {
                exTask.RemoveAt(lInt[i]);
            }
        }

        private ExampleTask ReturnNextTask(ExampleTask extask)
        {
            switch (extask.priority)
            {
                case Priority.HIGH:
                    {
                        highPriority.Enqueue(extask);
                        break;
                    }
                case Priority.NORMAL:
                    {
                        normalPriority.Enqueue(extask);
                        break;
                    }
                case Priority.LOW:
                    {
                        lowPriority.Enqueue(extask);
                        break;
                    }
            }
            if (highPriority.Count != 0)
            {
                if (nextNORMAL == 3)
                {
                    nextNORMAL = 0;
                    return normalPriority.Dequeue();
                }
                else
                {
                    nextNORMAL++;
                    return highPriority.Dequeue();
                }
            }
            else
            {
                if (normalPriority.Count != 0)
                {
                    return normalPriority.Dequeue();
                }
                else
                {
                    if (lowPriority.Count != 0)
                    {
                        return lowPriority.Dequeue();
                    }
                }

            }
            return null;
        }

        //возвращает поток для начала работы
        private ExampleTask ReturnNextTask()
        {
            if (threadQueue.Count != 5)
            {
                if (highPriority.Count != 0)
                {
                    if (nextNORMAL == 3)
                    {
                        nextNORMAL = 0;
                        return normalPriority.Dequeue();
                    }
                    else
                    {
                        nextNORMAL++;
                        return highPriority.Dequeue();
                    }
                }
                else
                {
                    if (normalPriority.Count != 0)
                    {
                        return normalPriority.Dequeue();
                    }
                    else
                    {
                        if (lowPriority.Count != 0)
                        {
                            return lowPriority.Dequeue();
                        }
                    }

                }
            }
            return null;
        }

        //добавление потока в очередь
        private void EnterTask(ExampleTask exTask)
        {
            switch (exTask.priority)
            {
                case Priority.HIGH:
                    {
                        highPriority.Enqueue(exTask);
                        break;
                    }
                case Priority.NORMAL:
                    {
                        normalPriority.Enqueue(exTask);
                        break;
                    }
                case Priority.LOW:
                    {
                        lowPriority.Enqueue(exTask);
                        break;
                    }
            }
        }

        //проверка на добавление нового потока в очередь
        private bool HaveNewTask()
        {
            if (threadQueue.Count != 5)
            {
                if (highPriority.Count != 0)
                {
                    return true;
                }
                else
                {
                    if (normalPriority.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (lowPriority.Count != 0)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private void Work()
        {
            //while (true)
            //{
                //if (stop == true && threadQueue.Count <= 0)

                do
                {
                    if (threadQueue.Count != 0)
                    {
                        List<int> li = new List<int>();
                        int i = 0;
                        foreach (var threadTask in threadQueue)
                        {
                           // Console.WriteLine(threadTask.task.Status);
                            if (TaskStatus.RanToCompletion == threadTask.task.Status)
                            {
                                li.Add(i);
                                //threadQueue.RemoveAt(i);
                                //Console.WriteLine(threadTask.task.Status);
                                //Console.WriteLine(i);
                            }
                            i++;
                        }
                        if (li.Count != 0)
                        {
                            DeleteExampleThread(threadQueue, li);
                        }

                        if (HaveNewTask())
                        {
                            ExampleTask exampleTask = ReturnNextTask();
                            exampleTask.task.Start();
                           // Console.WriteLine(exampleTask.priority);
                            threadQueue.Add(exampleTask);
                        }
                    }
                    else
                    {
                        if (HaveNewTask())
                        {
                            ExampleTask exampleTask = ReturnNextTask();
                            exampleTask.task.Start();
                            Console.WriteLine(exampleTask.priority);
                            threadQueue.Add(exampleTask);
                        }
                    }
                    
                    Thread.Sleep(100);
                Console.WriteLine("High={0},Normal={1},Low={2},threadQueue={3}", highPriority.Count, normalPriority.Count, lowPriority.Count, threadQueue.Count);

                    //}
                    //else
                    //{
                    //    return;
                    //}
                } while (stop != false || threadQueue.Count != 0);
            Console.WriteLine("End");
            //}
        }

        



        private void MyCode()
        {
            Task task1 = new Task(() => DisplayMessage("вызов метода с параметрами"));
            task1.Start();

            Task task2 = new Task(Display);
            task2.Start();

            Task task3 = new Task(() =>
            {
                Console.WriteLine("Id задачи {0}", Task.CurrentId);
            });
            task3.Start();

            

            Task task4 = new TaskFactory().StartNew(() =>
            {
                Console.WriteLine("Id задачи: {0}", Task.CurrentId);
            });

            TaskFactory tf = new TaskFactory();
            Task t5 = tf.StartNew(Display);

            Console.ReadLine();
        }
        
        static void Display()
        {
            Console.WriteLine("Id задачи :{0}", Task.CurrentId);
        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine("Сообщение {0}", message);
            Console.WriteLine("Id задачи :{0}", Task.CurrentId);
        }
    }
}
