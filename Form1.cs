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

        public Form1()
        {
            InitializeComponent();
            ftp = new FixedThreadPool(4);
            btnHigh.Click += HighButtonClick;
            btnLow.Click += LowButtonClick;
            btnNormal.Click += NormalButtonClick;
            btnStop.Click += StopButtonClick;
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
        
        // метод имитирующий работу
        static void RandomTime()
        {
            Random random = new Random();
            int time = random.Next(900, 4000);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(time);
                //Console.WriteLine("Id задачи :{0}", Task.CurrentId);
                
            }
            //Console.WriteLine("Id задачи :{0} Cancelled", Task.CurrentId);
        }
    }
}
