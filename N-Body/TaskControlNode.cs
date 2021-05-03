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
        private bool IsFinished()
        {
            lock (PhysicNode.tasksStatus)
            {
                foreach (var item in PhysicNode.tasksStatus)
                {

                    if (item != TaskStatus.Finished)
                        return false;

                }
                return true;
            }
            
        }
        private void Initialize()
        {
            for (int i = 0; i < threadNum; i++)
            {
                pNodes.Add(new PhysicNode(threadNum));
                tasks.Add(new Task(pNodes[i].CalculateVelocities, TaskCreationOptions.LongRunning));
            }
            tasks.Add(new Task(this.Run, TaskCreationOptions.LongRunning));
        }
        private void ResetWorkerRunning()
        {
            lock (PhysicNode.tasksStatus)
            {
                for (int i = 0; i < PhysicNode.tasksStatus.Count; i++)
                {
                
                    PhysicNode.tasksStatus[i] = TaskStatus.Running;
                
                }
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
                if (IsFinished())
                {
                    Debug.WriteLine("Finished: " + actualTime);
                    if (actualTime < timeSlice)
                    {
                        Physics.UpdatePositions();
                        actualTime += timeSteps;
                        ResetWorkerRunning();
                    }
                    else
                    {
                        sw.Stop();
                        lastElapsed = sw.Elapsed;
                        Debug.WriteLine("Elapsed time: " + lastElapsed);
                        actualTime = 0.0;
                        ResetWorkerRunning();

                        sw.Restart();
                    }
                }
                Thread.Sleep(5);
            }
        }
        
    }
}
