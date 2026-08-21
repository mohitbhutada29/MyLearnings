
Console.WriteLine("Server is running. type b to book and c to cancel");

int availableSeats = 10;
object ticketsLock = new();

var processQueue = new Queue<string>();
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
            Thread thread = new Thread(() => ProcessBooking(input));
            thread.Start();
        }
    }
}

// 3. Process the thread
void ProcessBooking(string input)
{
    Thread.Sleep(2000);

    lock(ticketsLock)
    {
        if (input.ToLower() == "b")
        {
            if (availableSeats > 0)
            {
                availableSeats--;
                Console.WriteLine($"Booking successful. Seats left: {availableSeats}");
            }
            else
            {
                Console.WriteLine("Tickets are sold out.");
            }
        }

        else if (input.ToLower() == "c")
        {
            Console.WriteLine("Operation cancelled.");
        }
    }
   

  
}

