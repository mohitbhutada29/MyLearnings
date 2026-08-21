Queue<int> queue = new();
ManualResetEventSlim consumeEvent = new(false);
ManualResetEventSlim produceEvent = new(true);

object lockConsumerCount = new();

int consumerCounter = 1;

Thread[] consumerThreads = new Thread[3];

for (int i = 0; i < 3; i++)
{
    consumerThreads[i] = new Thread(Consume);
    consumerThreads[i].Name = $"Consumer Thread {i + 1}";
    consumerThreads[i].Start();
}

while (true)
{
    produceEvent.Wait();    //blocks the thread until produceEvent is signalled/set
    produceEvent.Reset();   // switches off the produceEvent signal as we dont want
                            // to overproduce.

    Console.WriteLine("To produce, press 'p'");
    var input = Console.ReadLine();

    if (input?.ToLower() == "p")
    {
        for (int i = 1; i <= 10; i++)
        {
            queue.Enqueue(i);
            Console.WriteLine($"Produced:{i}");
        }

        consumeEvent.Set();     //signals to start consumption
    }
}

void Consume()
{
    while (true)
    {
        consumeEvent.Wait();    // blocks until consumeEvent is signalled/set

        while (queue.TryDequeue(out int item))
        {
            Thread.Sleep(500);
            Console.WriteLine($"{Thread.CurrentThread.Name} consumed an item{item}.");
        }

        lock (lockConsumerCount)
        {
            consumerCounter++;
            if (consumerCounter == 3)
            {
                consumeEvent.Reset();   // turn off the consumeEvent signal as we dont want
                                        // to overproduce.
                produceEvent.Set(); // signals to produce more items
                consumerCounter = 1;
                Console.WriteLine("More food please....>");
            }
        }
    }
}