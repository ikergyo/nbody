# nbody
N-Body parallel in C#
 This project is for simulating N-Body problem with C#, using multitasking solution.
 The project uses MonoGame for rendering
 
 
 # Usage
 At the beginning, the program will ask two parameter. Object number and Task Number. Object number is the number of the objects.
 Every object has a mass (which is random) and position. 
 The task number means C# Tasks, so how many thread should be created. 
 
 After the start, the simulation will generate the map (objects) and with the 'S' key, the simulation can be started. It measures the the time, how fast will the simulation reach the 1500th period and when t = 1500 it will write the elapsed time as a messagebox. 
 If you close the the simulation will continue.
