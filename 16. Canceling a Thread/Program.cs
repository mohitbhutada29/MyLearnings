// Canceling a thread

//bool cancelThread = false;
//Console.WriteLine("Work started...");

//Console.WriteLine("To cancel press c");

//Thread thread = new Thread(Work);
//thread.Start();

//var cancelOperation = Console.ReadLine();
//if (cancelOperation == "c")
//{
//    cancelThread = true;
//}

//void Work(object? obj)
//{
//    for (int i = 0; i < 100000; i++)
//    {
//        if (cancelThread)
//        {
//            Console.WriteLine($"\nOperation cancelled at iteration {i}");
//            break;
//        }
//        Thread.SpinWait(300000);
//    }
//}

//thread.Join();

//Console.ReadLine();


//**************************************************************************************************************

// other way is by passing cancellation token source


var cancellationToken = new CancellationTokenSource();
Console.WriteLine("Work started...");

Console.WriteLine("To cancel press c");

Thread thread = new Thread(Work);
thread.Start();

var cancelOperation = Console.ReadLine();
if (cancelOperation == "c")
{
    cancellationToken.Cancel();
}

void Work(object? obj)
{
    for (int i = 0; i < 100000; i++)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"\nOperation cancelled at iteration {i}");
            break;
        }
        Thread.SpinWait(300000);
    }
}

thread.Join();

Console.ReadLine();

