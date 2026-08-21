//// Basics of parallel linq
//// Parallel loops are for using parallelism in a collection of data.
//// Parallel LINQ (PLINQ) is a parallel implementation of LINQ to Objects.
//// It provides a simple and efficient way to perform
//// parallel operations on collections using LINQ syntax.
//// Parallel linq helps to speed up the execution of
//// LINQ queries by utilizing multiple processors or cores.

//var items = Enumerable.Range(1, 20);

//var evenNumbers = items.AsParallel().Where(x =>
//{
//    Console.WriteLine($"Processing number = {x}");
//    return (x % 2 == 0);
//});

//Console.WriteLine();

//Console.WriteLine($"There are {evenNumbers.Count()} even numbers");

//// We see the result in random sequence since we are using parallel linq.

//******************************************************************************************************

// AsOrdered method is used to preserve the order of the sequence in parallel linq.

//var items = Enumerable.Range(1, 200);

//var evenNumbers = items.AsParallel().Where(x =>
//{
//    return (x % 2 == 0);
//});

//Console.WriteLine("Unordered:");
//foreach (var item in evenNumbers)
//{
//    Console.WriteLine(item);
//}

//var evenNumbersAsOrdered = items.AsParallel().AsOrdered().Where(x =>
//{
//    return (x % 2 == 0);
//});

//Console.WriteLine("Asordered:");

//foreach (var item in evenNumbersAsOrdered)
//{
//    Console.WriteLine(item);
//}

//******************************************************************************************************


//var items = Enumerable.Range(1, 200);

//var evenNumbers = items.AsParallel().Where(x =>
//{
//    Console.WriteLine($"Processing number {x}; Thread Id: {Thread.CurrentThread.ManagedThreadId}");
//    return (x % 2 == 0);
//});


//evenNumbers.ForAll(item=>
//{
//    Console.WriteLine($"{item}: Threaad ID : {Thread.CurrentThread.ManagedThreadId}");
//});

//******************************************************************************************************

//// How PLINQ works: Producer consumer and buffer


//// Merge options
//// PLINQ provides different merge options that control how the
//// results of parallel operations are combined and returned to
//// the caller for consumption.
//// The available merge options include:
//// 1. Default: This is the default merge option.
//// It allows PLINQ to determine the best way to merge results based
//// on the workload and system resources. It may use buffering or streaming as needed.
//// 2. NotBuffered: This option indicates that PLINQ should not buffer the results.
//// Instead, it will return results as soon as they are produced
//// by the parallel operations.
//// This can be useful when you want to start processing results immediately
//// without waiting for the entire operation to complete.
//// 3. FullyBuffered: This option indicates that PLINQ should buffer all results
//// before returning them to the caller. This can be useful when you need to ensure
//// that all results are available before processing them, but it may increase
//// memory usage and delay the availability of results.


//var items = Enumerable.Range(1, 200);

//var evenNumbers = items.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered).Where(x =>
//{
//    Console.WriteLine($"Producing number {x}; Thread Id: {Thread.CurrentThread.ManagedThreadId}");
//    return (x % 2 == 0);
//});

//Console.WriteLine();

//foreach (var item in evenNumbers)
//{
//    Console.WriteLine($"Consuming number {item}; Thread Id: {Thread.CurrentThread.ManagedThreadId}");

//}



//******************************************************************************************************


// Cancellation in PLINQ

using var cts = new CancellationTokenSource();

var items = Enumerable.Range(1, 20);

var evenNumbers = items
    .AsParallel()
    .WithCancellation(cts.Token)
    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
    .Where(x =>
{
    Console.WriteLine($"Producing number {x}; Thread Id: {Thread.CurrentThread.ManagedThreadId}");
    return (x % 2 == 0);
});

Console.WriteLine();
try
{
    evenNumbers.ForAll(item =>
    {

        if (item > 8) cts.Cancel();
        Console.WriteLine($"{item}: Threaad ID : {Thread.CurrentThread.ManagedThreadId}");
    });
}
catch (OperationCanceledException ex)
{
    Console.WriteLine($"Operation is cancelled; Message = {ex.Message}");
}
catch (AggregateException ex)
{
    ex.Handle(e =>
    {
        Console.WriteLine($"Exception is handled; Message = {e.Message}");
        return true;
    });
}

