using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N_Body
{
    public class PhysicsController
    {
        bool ipressed = false;
        TaskControlNode tc;
        public PhysicsController(int threadNum, int timeSlice, double timeSteps, int objectNumber, int h, int w)
        {
            Physics.Initialize(objectNumber, h, w);
            tc = new TaskControlNode(threadNum, timeSlice, timeSteps);
        }

        public void Exit()
        {
            Physics.globalRunning = false;
            Task.WaitAll(tc.Tasks.ToArray());
        }

        public void Control(bool pressed)
        {
            if (!pressed)
                return;
            if (ipressed)
                return;
            ipressed = true;
            tc.Start();
        }
    }
}
