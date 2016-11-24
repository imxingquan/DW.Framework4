using System;
using System.Collections.Generic;
using System.Text;

namespace DW.Framework.Utils
{
    public class TimeThread
    {
        protected System.Timers.Timer _time;
        public TimeThread(double interval)
        {
            _time = new System.Timers.Timer(interval);
            _time.Elapsed += new System.Timers.ElapsedEventHandler(_time_Elapsed);
            _time.AutoReset = true;
           

            
        }

        public void Start()
        {
            _time.Start();
        }

        public void Stop()
        {
            _time.Stop();

        }


        void _time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (this)
            {
                if (Elapsed != null)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Elapsed));
                    thread.Start();
                }
            }
        }

        //protected void Run()
        //{
        //    Elapsed();
        //}

        public delegate void ElapsedEvent();
        public event ElapsedEvent Elapsed;

     

    }
}
