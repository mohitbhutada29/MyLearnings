//// Exception Handling in Tasks

// 1. Exceptions in tasks are actually hidden
// 2. Using try catch doesn't work
// 3. Exceptions are stored in the tasks itself
// 4. Multiple exceptions can be stored in a Task, hence we need to iterate through them.
// 5. Using wait or result will make the stored exception thrown.

//**************************************************************************************************************

//// Exceptions in tasks are actually hidden
//// If an exception occurs in a task, it is stored in the task object
//// and is rethrown when you access the Result property or call Wait() method on the task.
//// This is different from synchronous code where exceptions are thrown immediately.

//// Using try catch doesn't work

//using System.Text.Json;

//var client = new HttpClient();
//var taskListJson = client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon"); //false url
//var taskGetFirstUrl = taskListJson.ContinueWith(t =>
//{
//    var result = t.Result;
//    var jSonDocument = JsonDocument.Parse(result);
//    JsonElement root = jSonDocument.RootElement;
//    JsonElement results = root.GetProperty("results");
//    JsonElement firstPokemon = results[0];

//    Console.WriteLine(firstPokemon.GetProperty("url").ToString());

//});

//Console.WriteLine("End of the program");

//// here we have provided a false url and expect the app to crash
//// But it doesn't crash
//// Instead the exception is stored in the task object
//// We get "End of the program" but not the exception details
//// This is beacuse the task runs on a separate thread and the exception is
//// not propagated to the main thread

//**************************************************************************************************************

//// try catch also doesnt works here

//using System.Text.Json;

//try
//{
//    var client = new HttpClient();
//    var taskListJson = client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon"); //false url
//    taskListJson.ContinueWith(t =>
//    {
//        var result = t.Result;
//        var jSonDocument = JsonDocument.Parse(result);
//        JsonElement root = jSonDocument.RootElement;
//        JsonElement results = root.GetProperty("results");
//        JsonElement firstPokemon = results[0];

//        Console.WriteLine(firstPokemon.GetProperty("url").ToString());
//    });
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//Console.WriteLine("End of the program");
//Console.ReadLine();

//**************************************************************************************************************

// Exceptions are stored in the tasks itself

//using System.Text.Json;

//var client = new HttpClient();
//var task1 = client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon"); //false url
//var task2 = task1.ContinueWith(t =>
//{
//    var result = t.Result;
//    var jSonDocument = JsonDocument.Parse(result);
//    JsonElement root = jSonDocument.RootElement;
//    JsonElement results = root.GetProperty("results");
//    JsonElement firstPokemon = results[0];

//    Console.WriteLine(firstPokemon.GetProperty("url").ToString());
//});

//Thread.Sleep(1000);
//Console.WriteLine(task1.Status);    // output : Faulted
//Console.WriteLine(task2.Status);    // output : Faulted
//// the status of both task is faulted which means there is an exception thrown in both the tasks

//// Handling this exceptions
//// Multiple exceptions can be stored in a Task; hence we need to iterate throught them.

//if (task1.IsFaulted && task1.Exception != null) // Exception here is aggregate exception which contains
//                                                // a list of all the exceptions InnerExceptions

//{
//    foreach (var ex in task1.Exception.InnerExceptions)
//    {
//        Console.WriteLine($"Task 1 Exception: {ex.Message}");
//    }
//}

//Console.WriteLine("End of the program");
//Console.ReadLine();

//**************************************************************************************************************

//// Multiple exceptions

//using System.Threading.Tasks;

//var tasks = new Task[]
//{
//    Task.Run(() => throw new InvalidOperationException("Invalid operation in Task 1")),
//    Task.Run(() => throw new ArgumentNullException("Argument null in Task 2")),
//    Task.Run(() => throw new Exception("General exception in Task 3"))
//};

//Task.WhenAll(tasks).ContinueWith(t =>
//    {
//        // this t is a task created using Continue With which stores all thr innerexceptions.

//        if (t.IsFaulted && t.Exception != null) // Exception here is aggregate exception which contains
//                                                         // a list of all the exceptions InnerExceptions
//        {
//            foreach (var ex in t.Exception.InnerExceptions)
//            {
//                Console.WriteLine($"Task 1 Exception: {ex.Message}");
//            }
//        }
//        Console.WriteLine("Continue after all three tasks are finished");
//    });

//Console.WriteLine("Program ends here");
//Console.ReadLine();

//**************************************************************************************************************

//// Using wait or result will make the stored exception thrown.

//var tasks = new Task[]
//{
//    Task.Run(() => throw new InvalidOperationException("Invalid operation in Task 1")),
//    Task.Run(() => throw new ArgumentNullException("Argument null in Task 2")),
//    Task.Run(() => throw new Exception("General exception in Task 3"))
//};

//var task = Task.WhenAll(tasks);
//task.Wait(); // here the exception is rethrown when we call wait on the task.
//// throws AggregateException

//// We dont have t.Result here because WhenAll returns a Task that does not produce a result.
//// None of the tasks return anything

//Console.ReadLine();

//**************************************************************************************************************

//// Continue with method has different signatures
//// As ContinueWith helps us to chain multiple tasks together
//// if the first task throws an exception we can tell the compiler to stop
//// executing the next task using TaskContinuationOptions

//using System.Text.Json;

//var client = new HttpClient();
//var task1 = client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon"); //false url
//var task2 = task1.ContinueWith(t =>
//{
//    var result = t.Result;
//    var jSonDocument = JsonDocument.Parse(result);
//    JsonElement root = jSonDocument.RootElement;
//    JsonElement results = root.GetProperty("results");
//    JsonElement firstPokemon = results[0];

//    Console.WriteLine(firstPokemon.GetProperty("url").ToString());
//}, TaskContinuationOptions.NotOnFaulted);

//Console.WriteLine("End of the program");
//Console.ReadLine();

//**************************************************************************************************************

// We can use await keyowrd also to wait for the result


using System.Text.Json;

var client = new HttpClient();
var task1 = await client.GetStringAsync("https://pokeapi123.co/api/v2/pokemon"); //false url

// await returns only the first exception within the aggregate
// Wait returns the aggregate exception which contains all the exceptions.

Console.WriteLine("End of the program");
Console.ReadLine();