using System.Diagnostics;

Stopwatch stopWatch = new Stopwatch();
Console.WriteLine(" Parellel For Loop Execution start");
stopWatch.Start();
for (int i = 0; i < 10; i++)
{
    long total = DoSomeIndependentTask();
    Console.WriteLine("{0} - {1}", i, total);
}
DateTime EndDateTime = DateTime.Now;
Console.WriteLine("Parallel For Loop Execution end ");
stopWatch.Stop();
Console.WriteLine($"Time Taken to Execute the parellel For Loop in miliseconds {stopWatch.ElapsedMilliseconds}");



Console.WriteLine("For Loop Execution start");
stopWatch.Start();
for (int i = 0; i < 10; i++)
{
    long total = DoSomeIndependentTask();
    Console.WriteLine("{0} - {1}", i, total);
}

Console.WriteLine("For Loop Execution end ");
stopWatch.Stop();
Console.WriteLine($"Time Taken to Execute the For Loop in miliseconds {stopWatch.ElapsedMilliseconds}");
Console.ReadLine();        
        static long DoSomeIndependentTask()
{
   
    long total = 0;
    for (int i = 1; i < 100000000; i++)
    {
        total += i;
    }
    return total;

}
