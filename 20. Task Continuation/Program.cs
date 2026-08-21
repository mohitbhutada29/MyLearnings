//// Task Continuation: Wait, WaitAll and Result
//// As we said earlier, task is a promise that a task will be done sometime in the future.
//// So the next question is: When exactly will it be done?
//// There are several ways to find that out.
//// Wait, WaitAll and Result

//int sum = 0;

//var task = Task.Run(() =>
//{
//    for (int i = 1; i <= 100; i++)
//    {
//        Thread.Sleep(10);
//        sum += i;
//    }
//});


//task.Wait(); // Block the calling thread until the task is complete.
//             // This is simalr to Thread.Join() method. 
//             // if i comment this, the sum result will be zero since we are not waiting for the
//             // task to be completed


//Console.WriteLine($"The result sum is {sum}");
//Console.ReadLine();



//// Task.WaitAll(task1, task2, task3): Waits for all of the provided Task objects to complete execution.    


//**************************************************************************************************************

//var task = Task.Run(() =>
//{
//    var sum = 0;
//    for (int i = 1; i <= 100; i++)
//    {
//        Thread.Sleep(10);
//        sum += i;
//    }

//    return sum;
//});

//var result = task.Result;
//// Task.Result is the same thing as Task.Wait
//// It blocks the calling thread until the task is complete
//// So what is the point then? If the calling thread is blocked until we get the result;
//// This is synchronous. For this we have a method ContinueWith().



//Console.WriteLine($"The result sum is {result}");
//Console.ReadLine();


//**************************************************************************************************************

//using var client = new HttpClient();
////var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/20/");
//var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");
//// its always good to use Async methods when dealing with IO operations such as network calls
//// this makes sure that the threads are used from the ThreadPool
//// instead of creating new threads for each request which can overwhelm the system
//// It also makes sure that the main thread is not blocked while waiting for the response
//var result = task.Result;

//Console.WriteLine(result);


//**************************************************************************************************************


//// Task Continuation using ContinueWith()

//// We have already seen that Task.Wait blocks the main thread until the task is completed.
//// If the result of the first task is used in the second task, 
//// and if we use task.wait to wait for the first task to complete,
//// it first blocks the first task and then the second task too.
//// This is not efficient. Instead we can use ContinueWith() method.

//using var client = new HttpClient();
//var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");
//task.ContinueWith(r =>
//{
//    var result = r.Result;
//    Console.WriteLine(result);
//});


//Console.ReadLine();


//**************************************************************************************************************

//// One task can be started after another and so on using ContinueWith


//using System.Text.Json;

//using var client = new HttpClient();
//var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");
//task.ContinueWith(r =>
//{
//    var result = r.Result;
//    var jSonDocument = JsonDocument.Parse(result);
//    JsonElement root = jSonDocument.RootElement;
//    JsonElement results = root.GetProperty("results");
//    JsonElement firstPokemon = results[0];


//    Console.WriteLine($"The first pokemon is {firstPokemon.GetProperty("name")}");
//    Console.WriteLine($"The first pokemon's url is {firstPokemon.GetProperty("url")}");

//});


//Console.WriteLine("End of the program"); // this is displayed first
//                                         // Which represents that the blocking thread is not blocked
//                                         // untill the other tasks are performed.

//// Thus ContinueWith method is very helpful to chain multiple tasks together.
//Console.ReadLine();


//**************************************************************************************************************

////Task Continuation: WhenAll and WhenAny

//int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//int SumSegment(int start, int end)  //long running CPU intensive calculation
//{
//    int sum = 0;
//    for (int i = start; i < end; i++)
//    {
//        Thread.Sleep(100);
//        sum += array[i];
//    }
//    return sum;
//}

//var startTime = DateTime.Now;

//int numOfThreads = 4;
//int segmentLength = array.Length / numOfThreads;

//var tasks = new Task<int>[numOfThreads];
//tasks[0] = Task.Run(() => { return SumSegment(0, segmentLength); });
//tasks[1] = Task.Run(() => { return SumSegment(segmentLength, 2 * segmentLength); });
//tasks[2] = Task.Run(() => { return SumSegment(2 * segmentLength, 3 * segmentLength); });
//tasks[3] = Task.Run(() => { return SumSegment(3 * segmentLength, array.Length); });

//Console.WriteLine(Task.WhenAll(tasks).Result.Sum());
//Console.WriteLine("Before using ContinueWith");
//// When we dont use ContinueWith, the above one is blocking the main thread.
//// We get this "We are in the program" after the Task is completed.


////WhenAll creates a new task after completing all the tasks supplied as the argument.
//Task.WhenAll(tasks).ContinueWith(t =>
//{
//    Console.WriteLine($"The result is {t.Result.Sum()}");
//});

//Console.WriteLine("\nAfter using ContinueWith and When all");
//// When we  use ContinueWith, the above one is not blocking the main thread anymore.
//// We get this "We are in the program" before the Task is completed.
//// Since we now just made a promise to complete a task and that is performed whenever CPU is available.




//// WhenAny: When any of the task is complete return that
//// It returns the first task that is completed among the provided tasks.
//Task.WhenAny(tasks).ContinueWith(t =>
//{
//    Console.WriteLine($"The first completed task result is {t.Result.Result}");
//});

//Console.WriteLine("\nAfter using ContinueWith and When any");


//Console.ReadLine();


//**************************************************************************************************************


// Continuation Chain and Unwrap

using System.Text.Json;

var client = new HttpClient();
var taskListJson = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");
var taskGetFirstUrl = taskListJson.ContinueWith(t =>
{
    var result = t.Result;
    var jSonDocument = JsonDocument.Parse(result);
    JsonElement root = jSonDocument.RootElement;
    JsonElement results = root.GetProperty("results");
    JsonElement firstPokemon = results[0];

    return firstPokemon.GetProperty("url").ToString();

});


var taskGetDetailsJson = taskGetFirstUrl.ContinueWith(t =>
{
    var result = t.Result;
    return client.GetStringAsync(result);
}).Unwrap();


taskGetDetailsJson.ContinueWith(t =>
{
    var result = t.Result;
    var jSonDocument = JsonDocument.Parse(result);
    JsonElement root = jSonDocument.RootElement;
    Console.WriteLine($"The first pokemon details are: ");
    Console.WriteLine($"Name:{root.GetProperty("name").ToString()}");
    Console.WriteLine($"Weight:{root.GetProperty("weight").ToString()}");
    Console.WriteLine($"Height:{root.GetProperty("height").ToString()}");
});

// taskGetFirstUrl type is <Task<string>>
// taskGetJson type is <Task<Task<string>>> if we dont use Unwrap()
// We dont want this; we want <Task<string>>
// So we use Unwrap() to get the inner task.
// This is useful when we have a chain of tasks.
// It flattens the task of tasks into a single task.
// So we can directly work with the result of the inner task.
// Without Unwrap(), we would have to do t.Result.Result to get the actual result.
// This is not convenient.


// Thus, when you continue and continue again
// You will have a Task of Task of something
// To avoid that, we use Unwrap() to get the inner task directly


Console.ReadLine();


