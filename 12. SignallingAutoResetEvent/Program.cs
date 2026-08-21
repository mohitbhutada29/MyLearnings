//using AutoResetEvent autoResetEvent = new AutoResetEvent(false);


////consumer thread
//autoResetEvent.WaitOne(); // Wait for a signal


//// producer thread
//autoResetEvent.Set();

//**************************************************************************************************************


//using AutoResetEvent autoResetEvent = new AutoResetEvent(false);

////Start the worker thread
//// This runs indivudually and waits until signal is received from AutoResetEvent
//Thread workerThread = new Thread(Worker);
//workerThread.Start();


//string? userInput;
//while (true)
//{
//    userInput = Console.ReadLine();

//    // signal the worker thread if the input is "go
//    if(userInput.ToLower() == "go")
//    {
//        autoResetEvent.Set(); //turns on the binary signal
//    }
//}


//void Worker()
//{
//    while (true)
//    {
//        Console.WriteLine("Worker thread waiting for signal");
//        autoResetEvent.WaitOne(); //waits for the signal so that it
//                                  //can allow the worker thread to proceed further.
//                                  //BLocked at this point until the worker thread recives the signal.

//        //as soon as the compiler proceeds, the binary signal is turned off

//        Console.WriteLine("Signal received. Worker thread proceeds");
//        Thread.Sleep(2000);
//    }
    
//}



//**************************************************************************************************************

// We can have multiple worker threads

using AutoResetEvent autoResetEvent = new AutoResetEvent(false);

//Start the worker thread
// This runs indivudually and waits until signal is received from AutoResetEvent

// this represents the farmer-pig analogy.
// wherein the pig can enter the farm when there is a signal;
// if there is no signal the pig waits for the signal.

for (int i = 0; i < 3; i++)
{
    Thread workerThread = new Thread(Worker);
    workerThread.Name = $"Worker Thread {i + 1}";
    workerThread.Start();
}

string? userInput;
while (true)
{
    userInput = Console.ReadLine();

    // signal the worker thread if the input is "go
    if (userInput.ToLower() == "go")
    {
        autoResetEvent.Set(); //turns on the binary signal
    }
}


void Worker()
{
    while (true)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} waiting for signal");
        autoResetEvent.WaitOne(); //waits for the signal so that it
                                  //can allow the worker thread to proceed further.
                                  //BLocked at this point until the worker thread recives the signal.

        //as soon as the compiler proceeds, the binary signal is turned off

        Console.WriteLine($"Signal received. " +
            $"Worker thread {Thread.CurrentThread.Name} proceeds");
        Thread.Sleep(2000);
    }

}


// AutoResetEvent is not used to protect the critical section.
// it is used to signal between threads; for threads interaction purpose
// instead of shared resources.
// We want to send the signal for the purpose of sharing the resource.