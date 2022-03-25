using System;
using System.Collections.Generic;
using System.Threading;

namespace AlgorithmFCFS
{
    class Program
    {
        
        public static int QTime = 0;
        public static int AdditionalProcess = 10;

        //////////////////////////////////////////////////////////////////////////////////////
        //Queue of process
        public static List<Process> queue = new()
        {
            new Process(3),
            new Process(5),
            new Process(4),
            new Process(17),
            new Process(7),
            new Process(6)
        };

        static void Main(string[] args)
        {
            Console.SetBufferSize(500, Console.BufferHeight);

            List<Process> doneTasks = new();
            Thread addProcess = new(Simulate);
            addProcess.Start();

            //////////////////////////////////////////////////////////////////////////////////////
            //Procesor work simulation
            Process inProcesor;
            do
            {
                if(queue.Count > 0)
                {

                    inProcesor = queue[0];
                    queue.RemoveAt(0);
                    do
                    {
                        inProcesor.ExecutingForOneQTime();
                        Thread.Sleep(100);
                        queue.ForEach(Wait);
                    } while (inProcesor.IfMustWork());

                    doneTasks.Add(inProcesor);
                }
            } while (queue.Count != 0 || AdditionalProcess > 0);

            //////////////////////////////////////////////////////////////////////////////////////
            //Summary of simulation
            Console.WriteLine();

            int avgTime = 0;

            foreach(Process p in doneTasks)
            {
                avgTime += p.getWait();
                p.ShowWaitTime();
            }

            avgTime /= doneTasks.Count;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\nAverage waiting time: {avgTime}");
            Console.WriteLine("\nProcess execution timeline:");

            foreach (Process p in doneTasks)
            {
                p.ShowRunningTimeBar();
            }
            Console.WriteLine();
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Black;

            //////////////////////////////////////////////////////////////////////////////////////
            //Aditional functions
            static void Wait(Process p)
            {
                p.Wait();
            }

            static void Simulate()
            {
                Random rnd = new();
                do
                {
                    if (AdditionalProcess != 0)
                    {
                        queue.Add(new Process(rnd.Next(6) + 3));
                        AdditionalProcess--;
                    }
                    else return;
                } while (true);
            }
        }
    }
}
