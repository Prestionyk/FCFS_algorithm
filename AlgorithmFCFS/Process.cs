using System;

namespace AlgorithmFCFS
{
    class Process
    {
        public static int idProcess = 0, colorId = 0;

        bool isDone = false;
        int Id, workTime, workLeft,waitTime = 0;
        ConsoleColor color;

        public Process(int time)
        {
            Id = idProcess++;
            workTime = time;
            workLeft = workTime;
            color = GetNextColor();
        }
        public void ExecutingForOneQTime()
        {
            workLeft--;
            Console.ForegroundColor = color;
            Console.WriteLine($"QTime:{Program.QTime++}  \tProcess {Id} working...");

            if (workLeft == 0)
                WorkDone();
        }
        void WorkDone()
        {
            isDone = true;
            Console.ForegroundColor = color;
            Console.WriteLine($"QTime:{Program.QTime}  \tProcess {Id} done his work");
        }

        public bool IfMustWork()
        {
            if (isDone)
                return false;
            else
                return true;
        }
        public void Wait()
        {
            waitTime++;
        }

        public int getWait()
        {
            return waitTime;
        }
        public void ShowWaitTime()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nProcess {Id} waited {waitTime} QT");
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < waitTime; i++)
                Console.Write(' ');
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void ShowRunningTimeBar()
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < workTime; i++)
            {
                if(i == workTime / 2)
                    Console.Write(Id);
                else
                    Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static ConsoleColor GetNextColor()
        {
            ConsoleColor tempColor = (ConsoleColor)(((int)Console.ForegroundColor + colorId++) % Enum.GetValues(typeof(ConsoleColor)).Length);
            if (tempColor == ConsoleColor.Black)
                tempColor = (ConsoleColor)(((int)Console.ForegroundColor + colorId++) % Enum.GetValues(typeof(ConsoleColor)).Length);

            return tempColor;
        }
    }
}
