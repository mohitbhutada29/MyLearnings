//// ConcurrentQueue is a thread-safe collection class in the System.Collections.Concurrent namespace.
//// It is designed to allow multiple threads to safely add and remove items from the queue without
//// the need for explicit locking.
//// ConcurrentQueue does not have Dequeue method, it has TryDequeue instead.
//// Since it is accessed by multiple threads, TryDequeue method first checks if the queue is empty and then
//// tries to remove an item from the queue.
//// Concurrent Queue already has a locking mechanism built-in, so we don't
//// need to worry about thread synchronization when using it.
///// In short ConcurrentQueue is a thread-safe collection class that allows multiple threads to safely add and remove items from the queue 
//without the need for explicit locking, 
//making it ideal for scenarios where multiple threads need to access a shared queue of data.

//using System.Collections.Concurrent;

//var queue = new ConcurrentQueue<int>();
//queue.Enqueue(1);
//queue.Enqueue(2);
//queue.Enqueue(3);

//if(queue.TryDequeue(out int result))
//{
//    Console.WriteLine($"The dequeued element is {result}");
//}

//******************************************************************************************************

// Web server simulation

using System.Collections.Concurrent;

Console.WriteLine("Server is running");

var processQueue = new ConcurrentQueue<string>();

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
            if (processQueue.TryDequeue(out var input))
            {
                Thread thread = new Thread(() => ProcessInput(input));
                thread.Start();
            }
        }
    }
}

// 3. Process the thread
void ProcessInput(string input)
{
    Thread.Sleep(2000);
    Console.WriteLine($"Processed input: {input}");
}