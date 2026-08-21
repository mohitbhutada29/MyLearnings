//// Running on a single thread

//Console.WriteLine("Server is running");

//var processQueue = new Queue<string>();

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
//        ProcessInput(input);
//    }
//}

//void ProcessInput(string input)
//{
//    Thread.Sleep(2000);
//    Console.WriteLine($"Processed input: {input}");
//}

//*************************************************************************************************************************

// Running on a multiple threads

Console.WriteLine("Server is running");

var processQueue = new Queue<string>();

//2. Start a thread to monitor the request queue, we cant do it in the main thread as it is blocked

Thread monitorThread = new Thread(MonitorRequest);
monitorThread.Start();

// 1. Enqueue user input request into the queue (this happens on the main thread)
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
            Thread thread = new Thread(() => ProcessInput(input));
            thread.Start();
        }
    }
}

// 3. Process the thread
void ProcessInput(string input)
{
    Thread.Sleep(2000);
    Console.WriteLine($"Processed input: {input}");
}




//Regular thread collections are not thread safe
// "thread safe" means that a piece of code, method, or class can be safely accessed and used
// by multiple threads simultaneously without causing data corruption, inconsistencies, or unexpected behavior.
//If we see there are three threads here running simulataneously
// Main thread, monitorThread and thread.
// Cpu allocates different core to this threads and they might run at the same time
// this might create an issue as regular queue is not thread safe.
// Here comes in the concept of Thread Synchronisation