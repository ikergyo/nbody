using System;
using System.Collections.Generic;
using System.Text;

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
        

        public PhysicNode(int threadNum)
        {
            Id = _id++;
            offset = Id;
            steps = threadNum;
            tasksStatus.Add(TaskStatus.Finished);
        }
        public void CalculateVelocities()
        {
            while (Physics.globalRunning)
            {
                if (tasksStatus[Id] == TaskStatus.Running)
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

                            Physics.objects[i].velx += (deltaTime * forx) / Physics.objects[i].Mass;
                            Physics.objects[i].vely += (deltaTime * fory) / Physics.objects[i].Mass;
                            Physics.objects[i].temp_posx += deltaTime * Physics.objects[i].velx;
                            Physics.objects[i].temp_posy += deltaTime * Physics.objects[i].vely;
                        }
                    }
                    lock (tasksStatus)
                    {
                        tasksStatus[Id] = TaskStatus.Finished;
                    }
                }
            }
        }
    }
}
