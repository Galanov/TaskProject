using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WFMyApp
{
    public partial class Form1 : Form
    {

        FixedThreadPool ftp;
        //Queue<ExampleTask> lTask;
        //Queue<ExampleTask> threadQueue;
        // главная очередь
        List<ExampleTask> threadQueue;
        //List<ExampleTask> lTask;
        int nextNORMAL = 0;

        Task informationTask;

        private Queue<ExampleTask> highPriority;
        private Queue<ExampleTask> normalPriority;
        private Queue<ExampleTask> lowPriority;

        public Form1()
        {
            InitializeComponent();
            ftp = new FixedThreadPool(4);
            btnTest.Click += TestButtonClick;
            btnHigh.Click += HighButtonClick;
            btnLow.Click += LowButtonClick;
            btnNormal.Click += NormalButtonClick;
            btnStop.Click += StopButtonClick;

            informationTask = new Task(Govnocode);
            informationTask.Start();

            //lTask = new Queue<ExampleTask>();
            //lTask = new List<ExampleTask>();

            //threadQueue = new Queue<ExampleTask>();
            threadQueue = new List<ExampleTask>();
            // любимый говнокод , создание очереди для потоков разного приоритета
            highPriority = new Queue<ExampleTask>();
            normalPriority = new Queue<ExampleTask>();
            lowPriority = new Queue<ExampleTask>();

            StartTestCode();
        }
        private void Govnocode()
        {
            Information(tb);
        }
        private void TestButtonClick(object sender,EventArgs e)
        {
            //добавление потока в очередь
            Console.WriteLine(threadQueue.Count);

            // проверка на завершенные потоки
            if (threadQueue.Count!=0)
            {
                List<int> li = new List<int>();
                int i = 0;
                foreach (var threadTask in threadQueue)
                {
                    Console.WriteLine(threadTask.task.Status);
                    if (TaskStatus.RanToCompletion == threadTask.task.Status)
                    {
                        li.Add(i);
                        //threadQueue.RemoveAt(i);
                        Console.WriteLine(threadTask.task.Status);
                        Console.WriteLine(i);
                    }
                    i++;
                    
                }
                if (li.Count!=0)
                {
                    DeleteExampleThread(threadQueue, li);
                }
            }

            // проверка на кол-во элементов и добавление элементов из очереди
            //Task task = new Task(RandomTime);
            //ExampleTask extask = new ExampleTask(task,Priority.HIGH);

            /*
            if (ReturnNextTask(extask)!=null)
            {
                lTask.Add(ReturnNextTask(extask));
            }
            */
            //EnterTask(extask);
            if (HaveNewTask())
            {
                ExampleTask exampleTask = ReturnNextTask();
                exampleTask.task.Start();
                Console.WriteLine(exampleTask.priority);
                threadQueue.Add(exampleTask);
            }
                //= lTask.Last();
                //lTask.Dequeue();
            //exampleTask.task.Status
           
            //threadQueue.Enqueue(exampleTask);
            
            Console.WriteLine(threadQueue.Count);
        }

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
            if (highPriority.Count!=0)
            {
                if (nextNORMAL ==3)
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
                if (normalPriority.Count!=0)
                {
                    return normalPriority.Dequeue();
                }
                else
                {
                    if (lowPriority.Count!=0)
                    {
                        return lowPriority.Dequeue();
                    }
                }

            }
            return null;

        }

        // метод для удаления из списка выполняемых потоков
        //передается 2 списка, один со списком потоков, второй со списком элементов которые надо удалить 
        private void DeleteExampleThread(List<ExampleTask> exTask, List<int> lInt)
        {
            for (int i = lInt.Count-1; i >= 0; i--)
            {
                exTask.RemoveAt(lInt[i]);
            }
        }

        //StartTestCode версия с локальными переменными
        /*
        private void StartTestCode()
        {
            for (int i = 0; i < 5; i++)
            {
                EnterTask(new ExampleTask(new Task(RandomTime), Priority.HIGH));

            }
            for (int i = 0; i < 3; i++)
            {
                EnterTask(new ExampleTask(new Task(RandomTime), Priority.NORMAL));
            }
            for (int i = 0; i < 3; i++)
            {
                EnterTask(new ExampleTask(new Task(RandomTime), Priority.LOW));
            }
            Console.WriteLine("High={0},Normal={1},Low={2},lTask={3}", highPriority.Count, normalPriority.Count, lowPriority.Count, threadQueue.Count);
        }
        */
        private void StartTestCode()
        {
            for (int i = 0; i < 5; i++)
            {
                ftp.Execute( new Task(RandomTime), Priority.HIGH);

            }
            for (int i = 0; i < 3; i++)
            {
                ftp.Execute(new Task(RandomTime), Priority.NORMAL);
            }
            for (int i = 0; i < 3; i++)
            {
                ftp.Execute(new Task(RandomTime), Priority.LOW);
            }
            Console.WriteLine("High={0},Normal={1},Low={2},lTask={3}", ftp.highPriority.Count, ftp.normalPriority.Count, ftp.lowPriority.Count, threadQueue.Count);
        }
        private void HighButtonClick(object sender,EventArgs e)
        {
            Task t = new Task(RandomTime);
            ftp.Execute(t, Priority.HIGH);
        }

        private void NormalButtonClick(object sender, EventArgs e)
        {
            Task t = new Task(RandomTime);
            ftp.Execute(t, Priority.NORMAL);
        }

        private void LowButtonClick(object sender,EventArgs e)
        {
            Task t = new Task(RandomTime);
            ftp.Execute(t, Priority.LOW);
        }

        private void StopButtonClick(object sender,EventArgs e)
        {
            ftp.Stop();
        }

        // ошибка из-за вызова из другого потока
        /*
        public void Information(TextBox lb)
        {
            while (true)
            {
                string iformation = " ";
                iformation = String.Format("High:{0},Normal:{1},Low:{2}", ftp.highPriority.Count, ftp.normalPriority.Count, ftp.lowPriority.Count);
                //lb.Items[0] = String.Format(iformation);
                
                lb.Text=iformation;
                Thread.Sleep(1000);
            }
        }
        */

        public void MyTestCode()
        {
            //FixedThreadPool ftp = new FixedThreadPool(5);
            Task task1 = new Task(() => RandomTime());
            Task task2 = new Task(() => RandomTime());
            Task task3 = new Task(() => RandomTime());
            

            Queue<Task> tQueue = new Queue<Task>();
            tQueue.Enqueue(task1);
            tQueue.Enqueue(task2);
            tQueue.Enqueue(task3);
            foreach (var k in tQueue)
            {
                k.Start();
            }

            //do
            //{
            //    Thread.Sleep(200);

            //} while (!task1.IsCompleted || task1.IsCanceled);


            if (task1.IsCompleted)
            {
                Console.WriteLine("{0} поток завершен", task1.Id);
            }
            Console.WriteLine("Основной поток завершен");
            Console.ReadLine();
        }
        static void RandomTime()
        {
            Random random = new Random();
            int time = random.Next(900, 4000);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(time);
                Console.WriteLine("Id задачи :{0}", Task.CurrentId);
            }
            Console.WriteLine("Id задачи :{0} Cancelled", Task.CurrentId);
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

        static void Work()
        {
            Console.WriteLine("Выполнение задачи...");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("{0} ", i);
            }
            Thread.Sleep(300);
            Console.WriteLine();

            //return true;
        }


        private void Execute()
        {

        }

        void Run(ThreadStart doSomething)
        {
            if (doSomething == null)
                throw new ArgumentNullException("doSomething");

            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                doSomething();
            else
            {
                var th = new Thread(doSomething);

                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                th.Join();
            }
        }
    }
}
