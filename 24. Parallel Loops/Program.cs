// Parallel loops
// Parallel loops are used to perform iterations in parallel, which can
// improve performance for large datasets or computationally intensive tasks.
// Parallel loops helps us to do multiple operations at the same time by utilizing multiple threads.
// enabling us to write multithreading programs similar to the sequential programs
// This is achieved using the Task Parallel Library (TPL) in .NET.
// With Parallel loops you are writing multi threaded programming just like sequential programming



//int[] arrayList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//int sum = 0;
//for (int i = 0; i < arrayList.Length; i++)
//{
//    sum += arrayList[i];
//}

//Console.WriteLine($"Result is {sum}");

//******************************************************************************************************


//// Same using parallel loops

//int[] arrayList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//object sumLock = new();

//int sum = 0;

////Parallel.For(0, arrayList.Length, i =>
////{
////    lock (sumLock)
////    {
////        sum += arrayList[i];    //since sum is a shared variable accessed by multiple threads within Parallel loop
////    }
////    //We protect it using a lock.
////});

////Console.WriteLine($"Result is {sum}");

//// We also have foreach
//Parallel.ForEach(arrayList, i =>
//{
//    lock (sumLock)
//    {
//        sum += i;
//    }
//});

//Console.WriteLine($"Result is {sum}");


//******************************************************************************************************



//Parallel.Invoke(
//    ()=> { Thread.Sleep(2000); Console.WriteLine("Hello 1"); },
//    () => { Console.WriteLine("Hello 2"); },
//    () => { Console.WriteLine("Hello 3"); }
//    );


//******************************************************************************************************


//// Behind the scenes of parallel loops
//// 1. Data Partitioning: The input data is divided into smaller chunks or partitions.


//int[] arrayList = Enumerable.Range(0,100).ToArray();
//object sumLock = new();

//int sum = 0;

//Parallel.For(0, arrayList.Length, i =>
//{
//    lock (sumLock)
//    {
//        Console.WriteLine($"Task id = {Task.CurrentId}");
//        sum += arrayList[i];
//    }
//});

//Console.WriteLine($"Result is {sum}");
//// last task id is 17; thus it divides the array/work into small chunks or partitions

////2. Mostly uses threadpool thread from .net thread pool to execute the tasks.

////3. Which thread to use is been decided by the Task scheduler by itself.
//// Eaelier we  used to manually decide the number of threads to complete the tasks,
//// but with TPL it is been managed by the Task scheduler itself.
//// Use thread manually to have the complete thread control or use Parallel
//// loops to have the task scheduler manage the threads for you.

//// It is a blocking call; the main thread waits for the parallel loop to complete before proceeding further.

//******************************************************************************************************


//// Exception handling in Parallel loops
//// We are aware that parallel loops work on a collection of data
//// It uses multiple threads to process the data in parallel.
//// What happens if exception occurs in one of the threads?
//// When an exception occurs in a parallel loop, it is captured and aggregated into an AggregateException.

//int[] arrayList = Enumerable.Range(0, 100).ToArray();
//object sumLock = new();

//int sum = 0;

// state shows the state of the complete loop
//Parallel.For(0, arrayList.Length, (i, state) =>
//{
//    lock (sumLock)
//    {
//        if(!state.IsExceptional) // this prevents the further processing once exception occurs
//                                 // 
//        {
//            Console.WriteLine($"Iteration= {i} Task id = {Task.CurrentId}");
//            if (i == 65) throw new Exception("This is on purpose");
//            sum += arrayList[i];
//        }

//    }
//});

//Console.WriteLine($"Result is {sum}");

//******************************************************************************************************


//// The Stop method
//// Parallel loops provide a mechanism to stop the execution of the loop prematurely using the Stop method.
//// This can be useful in scenarios where a certain condition is met, and you want to halt further processing.
//// The Stop method signals the loop to stop, but it does not immediately terminate the ongoing iterations.
//// Instead, it allows the currently executing iterations to complete before exiting the loop.


//int[] arrayList = Enumerable.Range(0, 100).ToArray();
//object sumLock = new();

//int sum = 0;

//Parallel.For(0, arrayList.Length, (i, state) =>
//{
//    lock (sumLock)
//    {
//        if (!state.IsExceptional) // this prevents the further processing once exception occurs
//                                  // 
//        {
//            Console.WriteLine($"Iteration= {i} Task id = {Task.CurrentId}");
//            if (i == 65) state.Stop(); // stops further processing once i reaches 65
//            sum += arrayList[i];
//        }

//    }
//});

//Console.WriteLine($"Result is {sum}");

//******************************************************************************************************

//// The Break method
//// Similar to the Stop method, the Break method allows you to exit the loop prematurely.
//// However, unlike Stop, Break allows the currently executing iterations to complete
//// but prevents any new iterations from starting beyond the point where Break was called.

//int[] arrayList = Enumerable.Range(0, 100).ToArray();
//object sumLock = new();

//int sum = 0;

//Parallel.For(0, arrayList.Length, (i, state) =>
//{
//    lock (sumLock)
//    {
//        if (state.ShouldExitCurrentIteration && state.LowestBreakIteration < i) return; 
//        if (i == 65) state.Break(); // stops further processing once i reaches 65

//        Console.WriteLine($"Iteration= {i} Task id = {Task.CurrentId}");

//            sum += arrayList[i];


//    }
//});

//Console.WriteLine($"Result is {sum}");

//******************************************************************************************************


//// Parallel loop result
//// All the parallel loops methods return an ParallelLoopResult object
//// that provides information about the execution of the loop.

//int[] arrayList = Enumerable.Range(0, 100).ToArray();
//object sumLock = new();

//int sum = 0;
//ParallelLoopResult result;
//try
//{
//    result = Parallel.For(0, arrayList.Length, (i, state) =>
//    {
//        lock (sumLock)
//        {
//            if (state.ShouldExitCurrentIteration && state.LowestBreakIteration < i) return;
//            if (i == 65) state.Break(); // stops further processing once i reaches 65

//            Console.WriteLine($"Iteration= {i} Task id = {Task.CurrentId}");

//            sum += arrayList[i];


//        }
//    });
//}
//catch (AggregateException ex)
//{
//    Console.WriteLine(ex.InnerException);
//}



//Console.WriteLine($"Result is {sum}");

//// result.IsCompleted is false and the lowest break iteration is 65

//******************************************************************************************************


// Cancellation in Parallel Loops
// Remember: Parallel loops are blocking

var cts = new CancellationTokenSource();
var task = Task.Run(Work, cts.Token);

Console.WriteLine("Press c to cancel the operation");
var input = Console.ReadLine();

if(input =="c")
{
    cts.Cancel();
}

task.Wait();
Console.WriteLine($"Task status is {task.Status}");
Console.ReadLine();

void Work()
{
    Console.WriteLine($"Task is started");
    var options = new ParallelOptions
    {
        CancellationToken = cts.Token
    };
    Parallel.For(0, 10000, options, i =>
    {
        Console.WriteLine(DateTime.Now);
        Thread.SpinWait(50000000);
    });
}

