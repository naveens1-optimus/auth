using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("Standard Foreach Loop Started");
stopwatch.Start();
List<int> integerList = Enumerable.Range(1, 10).ToList();
foreach (int i in integerList)
{
    DoSomeIndependentTask(i);
};

//PLINQ
List<int> ls = integerList.AsParallel()
                     .WithDegreeOfParallelism(Environment.ProcessorCount)
                     .Where(n => n % 2 == 0)
                     .ToList();

stopwatch.Stop();
Console.WriteLine("Standard Foreach Loop Ended");
Console.WriteLine($"Time Taken by Standard Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");
Console.WriteLine("\nParallel Foreach Loop Started");
stopwatch.Restart();
ParallelOptions options = new ParallelOptions()
{
    MaxDegreeOfParallelism = 2
};
Parallel.ForEach(integerList, options, i =>
{
    DoSomeIndependentTask(i);
});

stopwatch.Stop();
Console.WriteLine("Parallel Foreach Loop Ended");
Console.WriteLine($"Time Taken by Parallel Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");

Console.ReadLine();
        
        static void DoSomeIndependentTask(int i)
{
    Console.WriteLine($"thread id:{Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($" Number: {i} ");
   
}