////// Async and AWait helps us to write asynchronous code that is more readable and maintainable.

////using System.Text.Json;

////using var client = new HttpClient();
////var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");

////var result = task.Result;   //blocks the main thread
////var jSonDocument = JsonDocument.Parse(result);
////JsonElement root = jSonDocument.RootElement;
////JsonElement results = root.GetProperty("results");
////JsonElement firstPokemon = results[0];
////Console.WriteLine($"First pokemon is {firstPokemon.GetProperty("name").ToString()}");

////Console.WriteLine("End of the program");
////Console.ReadLine();

////******************************************************************************************************

////// Instead of using Result which blocks the main thread, we can use async and await

////using System.Text.Json;

////OutPutResult();
////Console.WriteLine("End of the program");
////Console.ReadLine();

////async void OutPutResult()
////{
////    using var client = new HttpClient();
////    var task = client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");

////    var result = await task;
////    var jSonDocument = JsonDocument.Parse(result);
////    JsonElement root = jSonDocument.RootElement;
////    JsonElement results = root.GetProperty("results");
////    JsonElement firstPokemon = results[0];
////    Console.WriteLine($"First pokemon is {firstPokemon.GetProperty("name").ToString()}");
////}

////// 1. Everything after await keyword is considered to be the continuation of the task.
////// When the awaited task completes, the execution resumes from that point.
////// The main thread is not blocked while waiting for the task to complete.
////// This makes the code more readable and maintainable compared
////// to using ContinueWith or accessing Result property directly.
////// When program runs to the await thread, the calling thread is immediately released,
////// so that it is free to do any other things.

////// 2. await returns the value or result of the task directly

////// 3. Throws exception if any. As mentioned it throws the first exception in the AggregateException

////// 4. Manages the synchronization context. After the awaited task is complete,
////// the continuation resumes on the same context as the caller.
////// In GUI applications, this means that the continuation will run on the UI thread,
////// allowing safe access to UI elements.

////******************************************************************************************************

//// Basic syntax of async and await

////async Task MethodName()
////{
////...
////    await SomeTask;
////}

//// The return type of async method can be void, Task, or Task<T>
//// Task is recommended instead of void.
//// If the method returns a value, use Task<T> where T is the type of the value being returned.
//// If the method does not return a value, use Task.
//// If the method is an event handler, it can return void.

////******************************************************************************************************

////// everything after await keyword in a method is considered to be the continuation of the task
////// When the awaited task completes, the execution resumes from that point.

////Console.WriteLine("Starting to do some work");
////Work();
////Console.WriteLine("End of the program");

////Console.ReadLine();

////async void Work()
////{
////    await WorkAsync();
////}

////async Task WorkAsync()
////{
////    await Task.Delay(5000);
////    Console.WriteLine("Task is completed");
////}

////// Here End of the Program will be displayed first

////******************************************************************************************************

////Console.WriteLine("Starting to do some work");
////await WorkAsync();
////Console.WriteLine("End of the program");

////Console.ReadLine();

////async Task WorkAsync()
////{
////    await Task.Delay(5000);
////    Console.WriteLine("Task is completed");
////}

////// Now here, Console.WriteLine("End of the program"); is written after await keyword
////// So it will be executed only after the awaited task is completed.
////// This is because everything after await is considered to be the continuation of the task.

////******************************************************************************************************

////// Returning something from a task
////Console.WriteLine("Starting to do some work");
////var result = await WorkAsync();

////Console.WriteLine($"The result of the task is {result}");
////Console.WriteLine("End of the program");

////Console.ReadLine();

////async Task<string> WorkAsync()
////{
////    await Task.Delay(5000);
////    return "Task is completed";
////}

////// Its a common practice to include "Async" in the function name if its a async method.


////******************************************************************************************************


////// Which thread is used before and after await keyword


//Console.WriteLine($"1. Main thread id:{Thread.CurrentThread.ManagedThreadId}");
//Console.WriteLine("Starting to do some work");
//var result = await WorkAsync();

//Console.WriteLine($"The result of the task is {result}");
//Console.WriteLine($"2. Thread id:{Thread.CurrentThread.ManagedThreadId}");
//Console.WriteLine("End of the program");

//Console.ReadLine();

//async Task<string> WorkAsync()
//{
//    Console.WriteLine($"3. Thread id:{Thread.CurrentThread.ManagedThreadId}");
//    await Task.Delay(5000);
//    Console.WriteLine($"4. Thread id:{Thread.CurrentThread.ManagedThreadId}");
//    return "Task is completed";
//}

// Output
//1. Main thread id:1
//Starting to do some work
//3. Thread id:1
//4. Thread id:9
//The result of the task is Task is completed
//2. Thread id:9
//End of the program

////******************************************************************************************************


////// Task continuation using async await 

////using System.Text.Json;

////var client = new HttpClient();
////var result = await  client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");

////// Get first pokemon url
////var jSonDocument = JsonDocument.Parse(result);
////JsonElement root = jSonDocument.RootElement;
////JsonElement results = root.GetProperty("results");
////JsonElement firstPokemon = results[0];
////var firstPokemonUrl =  firstPokemon.GetProperty("url").ToString();

////// Get first pokemon details json
////var firstPokemonDetailsJson = await client.GetStringAsync(firstPokemonUrl);

////// Get all other properties 
////jSonDocument = JsonDocument.Parse(firstPokemonDetailsJson);
////root = jSonDocument.RootElement;
////Console.WriteLine($"The first pokemon details are: ");
////Console.WriteLine($"Name:{root.GetProperty("name").ToString()}");
////Console.WriteLine($"Weight:{root.GetProperty("weight").ToString()}");
////Console.WriteLine($"Height:{root.GetProperty("height").ToString()}");


////// Exception handling in async await throws the first exception and not the AggregateException

////******************************************************************************************************


//// Await and Synchronization context
//// Async and await keyword tries to make the continuation execute on the same context as the UI thread/
//// calling thread


////******************************************************************************************************


//// What does await do exactly?
//// When the compiler sees async await on a method it creates a state machine
//// The state machine is a special class that can be created to navigate between different states
//// Each await keyword represents a state in the state machine; it breaks the code at await keyword
//// When the awaited task is not completed the state machine saves the current state
//// and returns control to the calling thread
//// When the awaited task is completed the state machine resumes from the saved state
//// This is how async await helps to write asynchronous code that is more readable and maintainable

//// all the vairables are passed in the state machine along with their values
//// state machine remebers the values of this variables for a particular state
//// This is done by capturing the current context and restoring it when the awaited task is completed
//// Everytime it encountes the await keyword it captures the synchronization context
//// 

//// some code
//// task 1 = await SomeMethod ()    //state 1
//// some code
//// task 2 = await AnotherMethod()  //state 2 
//// some code                       //state 3


//// this is why we dont have synchronization issues in async await as compared to multithreading
//// as the context is captured and restored every time an await keyword is encountered
//// This is also the reason why async await is not suitable for CPU bound operations
//// as capturing and restoring context takes time and resources
//// It is more suitable for I/O bound operations where the thread is mostly waiting
//// for I/O operations to complete
//// like reading from a file, network operations, database operations, etc.
//// In CPU bound operations the thread is mostly busy doing computations
//// so the overhead of capturing and restoring context is not worth it



