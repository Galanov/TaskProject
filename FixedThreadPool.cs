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
        // очередь для потоков с высоким приоритетом
        private Queue<ExampleTask> highPriority;
        // очередь для потоков со средним приоритетом
        private Queue<ExampleTask> normalPriority;
        // очередь для потоков с низкиим приоритетом
        private Queue<ExampleTask> lowPriority;
        //переменная отвечающая за возвращаемый результат и добавления потоков в очередь
        bool stop = true;
        // переменная отвечающая за добавление потоков со средним приоритетом между потоками с высоким приоритетом
        int nextNORMAL;
        //количесвто потоков одновременно выполняющих операции
        private int countThread;
        //  список в котором находятся рабочие потоки
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
        
        // метод для удаления из списка выполняемых потоков
        //передается 2 списка, один со списком потоков, второй со списком элементов которые надо удалить 
        private void DeleteExampleThread( List<int> lInt)
        {
            for (int i = lInt.Count - 1; i >= 0; i--)
            {
                threadQueue.RemoveAt(lInt[i]);
                
            }
        }
        
        //возвращает следующий поток в рабочую очередь
        private ExampleTask ReturnNextTask()
        {
            if (threadQueue.Count != countThread)
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
            // в зависимости от приоритета добавляем в соответствующую очередь
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

        //проверка есть ли в очередях потоки для выполнения
        private bool HaveNewTask()
        {
            if (threadQueue.Count != countThread)
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
            do
            {
                // запускаем проверку на завершенные потоки
                if (threadQueue.Count != 0)
                {
                    List<int> li = new List<int>();
                    int i = 0;
                    foreach (var threadTask in threadQueue)
                    {
                       // определяем есть ли завершенные потоки
                       if (TaskStatus.RanToCompletion == threadTask.task.Status)
                       {
                            li.Add(i);
                        }
                        i++;
                    }
                    if (li.Count != 0)
                    {
                            // удаляем завершенные потоки из рабочей очереди
                            DeleteExampleThread( li);
                        }
                        //если есть новый поток, добавляем его в рабочую очередь
                        if (HaveNewTask())
                        {
                            ExampleTask exampleTask = ReturnNextTask();
                            exampleTask.Execute();
                            threadQueue.Add(exampleTask);
                        }
                    }
                    else
                    {
                        if (HaveNewTask())
                        {
                            ExampleTask exampleTask = ReturnNextTask();
                            exampleTask.Execute();
                            threadQueue.Add(exampleTask);
                        }
                    }
                    Thread.Sleep(100);
                    Console.WriteLine("High={0},Normal={1},Low={2},threadQueue={3}", highPriority.Count, normalPriority.Count, lowPriority.Count, threadQueue.Count);
                } while (stop != false || threadQueue.Count != 0);
            Console.WriteLine("End");
            
        }
    }
}
