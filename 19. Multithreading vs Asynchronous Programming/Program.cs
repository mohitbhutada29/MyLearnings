// Multi threading Vs Asynchronous Programming

// Multi threading
//  emphasis on divide and conquer
//  is about running multiple threads in parallel

// Asynchronous programming
//  focuses on responsiveness
//  it is about non blocking operations;
//  It is to offload a long running task

// Task based programming runs a long running task on a separate thread
// So Asynchronous programming and multithreading are more or less the same thing
// Just the emphasis is different.


// Multi threading is mainly used for CPU bound operations like games, mathematical computation, image/data processing
// Asynchronous programming is mainly used for I/O bound operations 
// where the program spends most of its time waiting (for disk, network, database, etc.), not using the CPU.
// like web requests, file operations like reading/ writing, database operation like querying data.
// We need the big fat task to return something. Does not take much of CPU.

//**************************************************************************************************************

//// Basic syntax of using a Task

////Thread thread1 = new Thread(Work);

////void Work(object? obj)
////{
////    throw new NotImplementedException();
////}

//Task task = new Task(Work);
//task.Start();

//void Work()
//{
//    Console.WriteLine("Async programming");
//}

////Other way

//Task.Run(Work);

//**************************************************************************************************************

//// Task vs Thread

//// A task is a promise
//// It does not have to necessarily run on a separate thread
//// Its a promise that a task will be done sometime in the future.
//// When excatly? We dont know.
//// A thread is a "basic programming unit" for running something in a CPU.
//// A task may run on a thread, but not necessarily.
//// Task are generally recommend by Microsoft; because it has higher level of abstraction
//// And offers many benefits.
//// Such as easy composition, cancellation, continuations, etc.

//// Benefits of Tasks
//// 1. Uses thread pool by default. Does not overwhelm an application with too many threads.
//// 2. Returns Values (Foir thread we require a shared variable)
//// 3. Easy continuation
//// 4. Better Exception Handling
//// 5. Feels like writing synchronous code
//// 6. Easy Contect management(synchronization context with async and await keyword).

//Task task = new Task(Work);
//task.Start();

//Console.ReadLine();

//void Work()
//{
//    Console.WriteLine("Async programming");
//    Console.WriteLine($"Is current thread from Thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
//}

//// If you pause and see the "Threads"; you will find that the Thread is coming from
//// ".NET TP Worker" where TP stands for Thread pool.

//**************************************************************************************************************

//// Returning result from task

//var result = Task.Run(Work);

//Console.WriteLine($"The result of the task is {result.Result}");

//Console.ReadLine();

//int Work()
//{
//    return 100;
//}

//**************************************************************************************************************

// Assignment

int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int SumSegment(int start, int end)  //long running CPU intensive calculation
{
    int sum = 0;
    for (int i = start; i < end; i++)
    {
        Thread.Sleep(100);
        sum += array[i];
    }
    return sum;
}

var startTime = DateTime.Now;

int numOfThreads = 4;
int segmentLength = array.Length / numOfThreads;

var tasks = new Task<int>[numOfThreads];
tasks[0] = Task.Run(() => { return SumSegment(0, segmentLength); });
tasks[1] = Task.Run(() => { return SumSegment(segmentLength, 2 * segmentLength); });
tasks[2] = Task.Run(() => { return SumSegment(2 * segmentLength, 3 * segmentLength); });
tasks[3] = Task.Run(() => { return SumSegment(3 * segmentLength, array.Length); });

var endtime = DateTime.Now;

Console.WriteLine($"The sum is {tasks[0].Result + tasks[1].Result + tasks[2].Result + tasks[3].Result}");
Console.WriteLine($"The sum is {tasks.Sum(t => t.Result)}");
Console.WriteLine($"Time taken is {(endtime - startTime).TotalMilliseconds}");