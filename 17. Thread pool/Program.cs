//// Thread pool
//// Thread takes time and memory to be created
//// This impacts the performance of an application when multiple threads are created
//// and destroyed frequently
//// To avoid this overhead, we can use thread pool
//// Thread pool is a collection of worker threads that are managed by the .NET runtime
//// These threads are created once and reused for multiple tasks
//// This improves the performance of the application
//// Thread pool also manages the number of threads based on the workload
//// This prevents the system from being overwhelmed with too many threads
//// We can use the ThreadPool class to work with thread pool

//// Every application has its own thread pool.
//// We can get the minimum and maximum number of threads in the thread pool


//ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxIOThreads);
//Console.WriteLine($"Max worker threads:{maxWorkerThreads}");
//Console.WriteLine($"Max i/o threads:{maxIOThreads}");


//ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableIOThreads);
//Console.WriteLine("\nAvailable threads");
//Console.WriteLine($"Max available worker threads:{availableWorkerThreads}");
//Console.WriteLine($"Max i/o threads:{availableIOThreads}");

//Console.WriteLine($"Active worker threads={maxWorkerThreads- availableWorkerThreads}");
//Console.WriteLine($"Active i/o threads={maxIOThreads - availableIOThreads}");


////Syntax
////ThreadPool.QueueUserWorkItem(a, b);

//**************************************************************************************************************


// Running on a multiple threads

Console.WriteLine("Server is running");

var processQueue = new Queue<string>();

//2. Start a thread to monitor the request queue, we cant do it in the main thread as it is blocked

Thread monitorThread = new Thread(MonitorRequest);
monitorThread.Start();

// 1. Enqueue user input request into the queue
while (true)
{
    var input = Console.ReadLine();
    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Server is shutting down.");
        break;
    }
    else
    {
        processQueue.Enqueue(input);
    }
}

void MonitorRequest()
{
    while (true)
    {
        if (processQueue.Count > 0)
        {
            var input = processQueue.Dequeue();

            //Thread thread = new Thread(() => ProcessInput(input));
            //thread.Start();

            // Using thread pool to process the request
            ThreadPool.QueueUserWorkItem(ProcessInput, input);
        }
    }
}

// 3. Process the thread
void ProcessInput(object? input)
{
    Console.WriteLine($"Is threadpool thread:{Thread.CurrentThread.IsThreadPoolThread}"); 
    Thread.Sleep(2000);
    Console.WriteLine($"Processed input: {input}");
}

