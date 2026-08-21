//// Task Cancellation

//// Canceling a thread

//using var cts = new CancellationTokenSource();

//Console.WriteLine("Work started...");

//Task task = new Task(Work, cts.Token);
//task.Start();

//Console.WriteLine("To cancel press c");
//var cancelOperation = Console.ReadLine();
//if (cancelOperation == "c")
//{
//    cts.Cancel();
//}

//task.Wait();
//Console.WriteLine($"Task completion status: {task.Status}");
//Console.ReadLine();

//void Work(object? obj)
//{
//    Console.WriteLine("Work started...");
//    for (int i = 0; i < 100000; i++)
//    {
//        Console.WriteLine(DateTime.Now);
//        if (cts.IsCancellationRequested)
//        {
//            Console.WriteLine($"\nOperation cancelled at iteration {i}");
//            break;
//        }
//        Thread.SpinWait(3000000);
//    }
//}

//// Most of the async methods written are provided with a way to cancel them using the CancellationToken.
////  client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon", CancellationToken cts);

//**************************************************************************************************************

// Cancellation token can be provided with timeout


var cts1 = new CancellationTokenSource();
cts1.CancelAfter(10);

var client = new HttpClient();
var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon", cts1.Token);

Console.WriteLine("End of the program");
Console.WriteLine(task.Result);
Console.ReadLine();