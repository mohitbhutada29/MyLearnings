// Task Synchronization
// Similar to Thread Synchronization

// All the thread synchronization texhniques like
// the locks, monitors, mutex, semaphore, etc can be used with tasks as well.


// same example as in Semaphore

Console.WriteLine("Server is running");

var processQueue = new Queue<string>();
object lockQueue = new();
using var semaphore = new SemaphoreSlim(3, 3);

//2. Start a thread to monitor the request queue, we cant do it in the main thread as it is blocked

Task monitorThread = new Task(MonitorRequest);
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
        lock (lockQueue) // applying locks to protect the critical section to allow only one thread at a time
        {
            processQueue.Enqueue(input);
        }
    }
}

void MonitorRequest()
{
    while (true)
    {
        if (processQueue.Count > 0)
        {
            string? input;
            lock (lockQueue)
            {
                input = processQueue.Dequeue();
            }

            semaphore.Wait();
            Task thread = new Task(() => ProcessInput(input));
            thread.Start();
        }
    }
}

// 3. Process the thread
void ProcessInput(string input)
{
    try
    {
        Thread.Sleep(20000);
        Console.WriteLine($"Processed input: {input}");
    }
    catch (Exception)
    {
        throw;
    }
    finally
    {
        // release thread only when the process is complete
        int previousSemaphoreCount = semaphore.Release();
        Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId} " +
            $"released the semaphore. Previous count is :{previousSemaphoreCount}");
    }
}

