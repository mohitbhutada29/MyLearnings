//// Concurrent Stack
//// ConcurrentStack is a thread-safe collection class in the System.Collections.Concurrent namespace.
//// It is designed to allow multiple threads to safely add and remove items from the stack without
//// the need for explicit locking.

//using System.Collections.Concurrent;

//var stack = new ConcurrentStack<int>();
//stack.Push(1);
//stack.Push(2);
//stack.Push(3);

//if (stack.TryPop(out int result))
//{
//    Console.WriteLine($"The destacked element is {result}");
//}

//******************************************************************************************************

//Blocking collection
// Two features: Blocking and bounding
// Blocking: If the collection is empty, the consumer thread will block until an item is available.
// If the collection is full, the producer thread will block until space is available.
// Bounding: You can set a maximum capacity for the collection/buffer,
// which can help prevent memory issues in scenarios where producers are
// generating items faster than consumers can process them.

using System.Collections.Concurrent;

var concurrentQueue = new ConcurrentQueue<string>();
var blockingCollection = new BlockingCollection<string>(concurrentQueue, 3); // Set a maximum capacity of 3


Thread monitorThread = new Thread(MonitorCollection);
monitorThread.Start();

while (true)
{
    var input = Console.ReadLine();
    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Exiting...");
        break;
    }
    else
    {
        blockingCollection.Add(input);
        Console.WriteLine($"Enqueued:{input}; queue size{blockingCollection.Count}");
    }
}

void MonitorCollection()
{
    foreach (var request in blockingCollection.GetConsumingEnumerable())
    {
        Thread thread = new Thread(() => ConsumeInput(request));
        thread.Start();
        Thread.Sleep(2000);
    }
}

void ConsumeInput(string input)
{
    Thread.Sleep(2000);
    Console.WriteLine($"Processed input: {input}");
}


//Eaach time we add an item to the blocking collection, it will print the current queue size. If the queue is full (i.e., has 3 items),
//the producer thread will block until space is available. The monitor thread will consume items from the collection and process them, simulating a delay of 2 seconds for each item.