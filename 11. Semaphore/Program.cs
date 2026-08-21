// Semaphore:
// Another thread synchronisation technique  used to limit the number of threads
// that access the critical section of the code.
// Can be used to protect the critical sections similar to locks, monitor, mutex.

//void MonitorRequest()
//{
//    while (true)
//    {
//        if (processQueue.Count > 0)
//        {
//            var input = processQueue.Dequeue();
//            Thread thread = new Thread(() => ProcessInput(input));
//            thread.Start();
//        }
//    }
//}

// Consider above example:
//  Here we are starting a new thread for each request.
//  If there are too many requests, it might create too many threads
//  which might overwhelm the server and degrade the performance.
//  To avoid this we can use Semaphore to limit the number of threads
//  that can access the critical section at a time.
//  Semaphore allows a fixed number of threads to access a
//  resource or critical section at a time.
//  Other threads have to wait until a thread
//  releases the semaphore.

//**************************************************************************************************************

////Syntax

//using (var semaphore = new SemaphoreSlim(initialCount:3, maxCount:3))
//{
//    semaphore.Wait();
//    try
//    {
//        // use semaphore to limit access to the following section
//        // critical section
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//    finally
//    {
//        semaphore.Release();
//    }
//}

//**************************************************************************************************************

//Console.WriteLine("Server is running");

//var processQueue = new Queue<string>();
//using var semaphore = new SemaphoreSlim(3, 3);

////2. Start a thread to monitor the request queue, we cant do it in the main thread as it is blocked

//Thread monitorThread = new Thread(MonitorRequest);
//monitorThread.Start();

//// 1. Enqueue user input request into the queue
//while (true)
//{
//    var input = Console.ReadLine();
//    if (input.ToLower() == "exit")
//    {
//        Console.WriteLine("Server is shutting down.");
//        break;
//    }
//    else
//    {
//        processQueue.Enqueue(input);
//    }
//}

//void MonitorRequest()
//{
//    while (true)
//    {
//        if (processQueue.Count > 0)
//        {
//            var input = processQueue.Dequeue();

//            semaphore.Wait();
//            Thread thread = new Thread(() => ProcessInput(input));
//            thread.Start();
//        }
//    }
//}

//// 3. Process the thread
//void ProcessInput(string input)
//{
//    try
//    {
//        Thread.Sleep(20000);
//        Console.WriteLine($"Processed input: {input}");
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//    finally
//    {
//        // release thread only when the process is complete
//        int previousSemaphoreCount = semaphore.Release();
//        Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId} " +
//            $"released the semaphore. Previous count is :{previousSemaphoreCount}");
//    }
//}


//// protects the application from overloading the server by limiting 
//// the number of threads that can access the critical section at a time.
//// Semaphore can be cross process
//// for a cross process semaphore it must have a name
//// SemaphoreSlim can only be used within a process
//// Semaphore can be used across multiple processes with a name

//using (Semaphore semaphore1 =
//    new Semaphore(3, 3, "GlobalSemaphoreName")) ;

//**************************************************************************************************************

// Here we just limit the number of threads. we are not protecting the critical
// sections using semaphores
// to protect the ciritical sections, we can simply use a lock

Console.WriteLine("Server is running");

var processQueue = new Queue<string>();
object lockQueue = new();
using var semaphore = new SemaphoreSlim(3, 3);

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

            //semaphore.Wait();
            Thread thread = new Thread(() => ProcessInput(input));
            thread.Start();
        }
    }
}

// 3. Process the thread
void ProcessInput(string input)
{
    try
    {
        Thread.Sleep(2000);
        Console.WriteLine($"Processed input: {input}");
    }
    catch (Exception)
    {
        throw;
    }
    finally
    {
        // release thread only when the process is complete
        //int previousSemaphoreCount = semaphore.Release();
        Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId} ");
        //    $"released the semaphore. Previous count is :{previousSemaphoreCount}");
    }
}


// protects the application from overloading the server by limiting 
// the number of threads that can access the critical section at a time.
// Semaphore can be cross process
// for a cross process semaphore it must have a name
// SemaphoreSlim can only be used within a process
// Semaphore can be used across multiple processes with a name

//using (Semaphore semaphore1 =
//    new Semaphore(3, 3, "GlobalSemaphoreName")) ;