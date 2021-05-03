using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Threading;

namespace N_Body
{
    //1 pixel unit = 1e+6 km

    
    public static class Physics
    {
        //Physics Config
        //77 760 000 = 15 day

        public static bool globalRunning = false;

        const int gravity_increaser = 5;
        public static double gravity = 6.67430 * 1 * Math.Pow(10, -11 + gravity_increaser);
        
        static public NObject[] objects;
        static public int objectIndex = 0;

        public static void Initialize(int objectNumber, int h, int w)
        {
            objects = new NObject[objectNumber];
            for (int i = 0; i < objectNumber; i++)
            {
                AddObject(NObject.GenerateRandomObject(h, w));
                
            }
        }

        public static void AddObject(NObject obj)
        {
            if (objectIndex < objects.Length)
                objects[objectIndex++] = obj;
        }
       
        public static void UpdatePositions()
        {
            for (int i = 0; i < objectIndex; i++)
            {
                objects[i].posx = objects[i].temp_posx;
                objects[i].posy = objects[i].temp_posy;
            }
        }
    }
}
