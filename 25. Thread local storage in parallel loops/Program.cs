//// Thread local storage
//// When we run a multi threaded operation; each thread need to have its own local storage.
//// Thus if multiple threads are updating a variable; it will lead to data corruption.

//// Parallel threads provide a way to have local storage for each thread

//int[] arrayList = Enumerable.Range(1, 10).ToArray();
//object sumLock = new();

//int sum = 0;

//Parallel.For(
//    0,
//    arrayList.Length,
//    () => 0,                    // here the type can be anything; int, string, dictionary etc
//    (i, state, tls) =>          //tls stands for thread local storage
//    {
//        tls += arrayList[i];
//        return tls;
//    },
//    tls =>                      // this is all at the end of all the iterations
//                                // Calls only once per thread.
//                                // if it has 8 thread ids that means we have used 8 threads
//    {
//        lock (sumLock)
//        {
//            sum += tls;
//            Console.WriteLine($"The task id is {Task.CurrentId}");
//        }
//    });



//Console.WriteLine($"Result is {sum}");

//******************************************************************************************************


// Performance Considerations in parallel loops

var beginTime = DateTime.Now;
int[] arrayList = Enumerable.Range(1, 1000000).ToArray();
object sumLock = new();

int sum = 0;


for (int i = 0; i < arrayList.Length; i++)
{
    sum += arrayList[i];
}


//Parallel.For(
//    0,
//    arrayList.Length,
//    () => 0,                    // here the type can be anything; int, string, dictionary etc
//    (i, state, tls) =>          //tls stands for thread local storage
//    {
//        tls += arrayList[i];
//        return tls;
//    },
//    tls =>                      // this is all at the end of all the iterations
//                                // Calls only once per thread.
//                                // if it has 8 thread ids that means we have used 8 threads
//    {
//        lock (sumLock)
//        {
//            sum += tls;
//            Console.WriteLine($"The task id is {Task.CurrentId}");
//        }
//    });


var endTime = DateTime.Now;
Console.WriteLine($"Result is {sum}");
Console.WriteLine($"Time needed is {(endTime-beginTime).TotalMilliseconds}");


// In the above scenario; sequential loop is actually faster than the parallel loop
// This is because of the overhead of managing multiple threads in parallel loop
// Creating and managing thread requires time and resources
// So if you have a huge task loop, parallel loops will be beneficial
