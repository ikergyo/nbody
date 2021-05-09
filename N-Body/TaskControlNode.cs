using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace N_Body
{
    public class TaskControlNode
    {
        static List<Task> tasks = new List<Task>();
        static List<PhysicNode> pNodes = new List<PhysicNode>();

        public ManualResetEvent[] workerStatus;
        public AutoResetEvent[] nextTimeStatus;

        int threadNum;
        int timeSlice;
        double actualTime = 0.0;
        double timeSteps;

        TimeSpan lastElapsed;

        public List<Task> Tasks
        {
            get { return tasks; }
        }

        public TaskControlNode(int threadNum, int timeSlice, double timeSteps)
        {
            this.threadNum = threadNum;
            this.timeSlice = timeSlice;
            this.timeSteps = timeSteps;
            
            Initialize();
        }
        
        private void Initialize()
        {
            workerStatus = new ManualResetEvent[threadNum];
            nextTimeStatus = new AutoResetEvent[threadNum];
            for (int i = 0; i < threadNum; i++)
            {
                workerStatus[i] = new ManualResetEvent(false);
                nextTimeStatus[i] = new AutoResetEvent(false);
                pNodes.Add(new PhysicNode(threadNum, workerStatus[i], nextTimeStatus[i]));
                tasks.Add(new Task(pNodes[i].CalculateVelocities, TaskCreationOptions.LongRunning));
            }
            tasks.Add(new Task(this.Run, TaskCreationOptions.LongRunning));
        }
        private void ResetWorkerRunning()
        {
            for (int i = 0; i < threadNum; i++)
            {
                workerStatus[i].Reset();
                nextTimeStatus[i].Set();
            }
        }
        public void SetAllStatus()
        {
            for (int i = 0; i < threadNum; i++)
            {
                workerStatus[i].Set();
                nextTimeStatus[i].Set();
            }
        }
        public void Start()
        {
            Physics.globalRunning = true;
            foreach (var item in tasks)
            {
                item.Start();
            }
        }
        public void Run()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (Physics.globalRunning)
            {
                WaitHandle.WaitAll(workerStatus);
                Debug.WriteLine("Finished t: " + actualTime);
                if (actualTime < timeSlice)
                {
                    Physics.UpdatePositions();
                    actualTime += timeSteps;
                }
                else
                {
                    sw.Stop();
                    lastElapsed = sw.Elapsed;
                    Debug.WriteLine("Elapsed time: " + lastElapsed);
                    actualTime = 0.0;
                    MessageBox.Show("Elapsed Time", lastElapsed.ToString(), new[] { "Ok" });
                    sw.Restart();
                }
                ResetWorkerRunning();
            }
        }
        
    }
}
