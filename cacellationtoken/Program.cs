using System.Diagnostics;

var CTS=new CancellationTokenSource();
CTS.CancelAfter(TimeSpan.FromSeconds(3));

//TPL method
ParallelOptions options = new ParallelOptions()
{
    CancellationToken = CTS.Token,
    MaxDegreeOfParallelism = Environment.ProcessorCount -1
};

Stopwatch stopwatch = new Stopwatch();  
stopwatch.Start();
List<int> nList =   Enumerable.Range(1, 20).ToList();
try
{   //Using PLINQ
    var newList = nList.AsParallel().WithDegreeOfParallelism(2).WithCancellation(
        CTS.Token).Where(i =>
        {
            Console.WriteLine(i);
           Thread.Sleep(TimeSpan.FromSeconds(1));
            return i % 2 == 0;
        });
    foreach (var item in newList)   
    Console.WriteLine(item);
    
    //using PCL

    //Parallel.ForEach(nList, options, (i) =>
    //{
    //    dosomething(i);
    //});
}
catch (Exception ex)
{
    stopwatch.Stop();
    Console.WriteLine($"stopped after { stopwatch.ElapsedMilliseconds}");
    Console.WriteLine(ex.Message);
}

 void dosomething(int i)
{
    Console.WriteLine(i);
    Thread.Sleep(TimeSpan.FromSeconds(2));
}