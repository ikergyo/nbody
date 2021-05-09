using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace N_Body
{
    public enum TaskStatus { Stopped, Running, Finished }
    public class PhysicNode
    {
        static int _id = 0;
        public static List<TaskStatus> tasksStatus = new List<TaskStatus>();
        
        public static double deltaTime = 1;

        public int Id { get; private set; }

        private int offset;
        private int steps;
        private ManualResetEvent wStatus;
        private AutoResetEvent nStatus;


        public PhysicNode(int steps, ManualResetEvent wStatus, AutoResetEvent nStatus)
        {
            Id = _id++;
            offset = Id;
            this.steps = steps;
            this.wStatus = wStatus;
            this.nStatus = nStatus;
            
        }
        public void CalculateVelocities()
        {
            while (Physics.globalRunning)
            {
                    for (int i = offset; i < Physics.objectIndex; i += steps)
                    {
                        for (int j = 0; j < Physics.objectIndex; j++)
                        {
                            NObject obj1 = Physics.objects[i];
                            NObject obj2 = Physics.objects[j];
                            if (obj1 == obj2)
                                continue;

                            double dirx = obj2.posx - obj1.posx;
                            double diry = obj2.posy - obj1.posy;
                            double dirLength = Math.Sqrt(dirx * dirx + diry * diry);
                            dirx /= dirLength;
                            diry /= dirLength;

                            double forceTwo = (Physics.gravity * obj1.Mass * obj2.Mass) / (dirLength * dirLength);

                            double forx = dirx * forceTwo;
                            double fory = diry * forceTwo;
                        if (Double.IsNaN(forx))
                            forx = 0;
                        if (Double.IsNaN(fory))
                            fory = 0;
                        lock (obj1)
                            {
                                obj1.velx += (deltaTime * forx) / obj1.Mass;
                                obj1.vely += (deltaTime * fory) / obj1.Mass;
                                obj1.temp_posx += deltaTime * obj1.velx;
                                obj1.temp_posy += deltaTime * obj1.vely;
                            }
                        }
                    }
                WaitHandle.SignalAndWait(wStatus, nStatus);
            }
        }
    }
}
