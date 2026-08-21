// Thread affinity
// Certain resources or operations in a program are tied to specific thread and
// must always be accessed or executed on that same thread
// in wpf, ui elements have thread affinity. if you create a button on the ui thread
// you cannot update it from a background thread directly.



//// UI thread
//Button myButton = new Button { Content = "Click Me" };

//// Background thread
//Task.Run(() =>
//{
//    // This will throw InvalidOperationException:
//    // "The calling thread cannot access this object because
//    // a different thread owns it."
//    myButton.Content = "Updated";
//});

// Why?
// myButton is tied to the UI thread.
// WPF enforces this to prevent unsafe concurrent access.


// Correct way is using Dispatcher
//Task.Run(() =>
//{
//    Application.Current.Dispatcher.Invoke(() =>
//    {
//        myButton.Content = "Updated safely!";
//    });
//});

// Doing thius synchronizes the two threads as one.

//**************************************************************************************************************


//Thread safety
// A function, data struct, class, etc is considered thread safe
// if it can be safely invoked or accessed by multiple threads at the same time
// without causing data corruption or inconsistent results or race conditions.

// Thread safe types have the synchronization mechanisms like lock, monitors, mutex
// built into them to handle concurrent access.
// Examples: ConcurrentQueue, ConcurrentDictionary, etc

//**************************************************************************************************************

//// Nested locks and dead locks

//// Dead locks
//// When two threads try to wait for each other; we get a dead lock situation.
////A deadlock occurs when two or more threads are waiting for each other
////to release resources, and none of them can proceed because they are stuck
////in a circular wait. 


//object lockA = new ();
//object lockB = new ();

//void Thread1()
//{
//    lock (lockA)
//    {
//        Console.WriteLine("Thread1 acquired lockA");
//        Thread.Sleep(100); // Simulate work
//        lock (lockB)
//        {
//            Console.WriteLine("Thread1 acquired lockB");
//        }
//    }
//}

//void Thread2()
//{
//    lock (lockB)
//    {
//        Console.WriteLine("Thread2 acquired lockB");
//        Thread.Sleep(100); // Simulate work
//        lock (lockA)
//        {
//            Console.WriteLine("Thread2 acquired lockA");
//        }
//    }
//}

//// Start both threads
//new Thread(Thread1).Start();
//new Thread(Thread2).Start();


//Console.WriteLine("Program finished");


//**************************************************************************************************************


//// We can use Parallel Stacks, Threads tool in Visual studio while debugging.
//// You can pause any thread; thaw (play) any thread.
//// When you pause while debugging; all the threads are paused; its not like
//// some of the threads are still running; everything is paused.

//Thread[] threads = new Thread[10];
//for (int i = 0; i < 10; i++)
//{
//    threads[i] = new Thread(Work);
//    threads[i].Name = $"Thread {i + 1}";

//    threads[i].Start();
//}

//void Work(object? obj)
//{
//    Console.WriteLine($"Thread {Thread.CurrentThread.Name} have started working.");
//    Thread.Sleep(10000);
//    Console.WriteLine($"Thread {Thread.CurrentThread.Name} have finished working.");

//}


//**************************************************************************************************************

//// States of a thread
//// Thread can have a state
//// The thread states are as follows:
//// Unstarted - thread is created but not started. Every thread is in this state initially.
//// Running - thread is executing.
//// WaitSleepJoin - thread is blocked; waiting for some resource or sleeping or waiting for another
//// thread to complete.
//// Suspended - thread is suspended.
//// Stopped - thread has completed execution or aborted.
//// Background - thread is a background thread.
//// Foreground - thread is a foreground thread.
//// AbortRequested - thread abort has been requested.
//// Aborted - thread has been aborted.
//// StopRequested - thread stop has been requested.
//// The thread state can be checked using the Thread.ThreadState property.


//Thread[] threads = new Thread[10];
//for (int i = 0; i < 10; i++)
//{
//    threads[i] = new Thread(Work);
//    threads[i].Name = $"Thread {i + 1}";
//    Console.WriteLine($"Thread {i} state:{threads[i].ThreadState}");
//    threads[i].Start();
//}

//Thread.Sleep(1000);     // this adds one more state WaitSleepJoin
//for (int i = 0; i < 10; i++)
//{ 
//    Console.WriteLine($"Thread {i} state:{threads[i].ThreadState}");

//}

//Work();
//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine($"Thread {i} state:{threads[i].ThreadState}");

//}


//void Work()
//{
//    Console.WriteLine($"Thread {Thread.CurrentThread.Name} have started working. " +
//        $"Thread state {Thread.CurrentThread.ThreadState}");

//    Thread.Sleep(10000);

//    Console.WriteLine($"Thread {Thread.CurrentThread.Name} have finished working. " +
//        $"Thread state {Thread.CurrentThread.ThreadState}");

//}

//// Thread states are used just for debugging purpose


//**************************************************************************************************************

// Making thread wait for a period of time
// there are 3 ways to make a thread wait for a period of time
// 1. Thread.Sleep(milliseconds)
// 2. Thread.SpinWait(iterations) blocks a thread for given number of iterations (like a forloop)
// Difference between Thread.Sleep and Thread.SpinWait: Thread.Sleep kicks the thread out of the CPU scheduler
// Whereas Thread.SpinWait keeps the thread in the CPU scheduler but just makes it busy wait
// 3. SpinWait.SpinUntil(condition, timeout)

//**************************************************************************************************************

//Returning Result from thread

// The only way to return a result from a thread is using a shared variable

string result = null;

Thread thread1 = new Thread(Work);
thread1.Start();

thread1.Join();  // main thread waits for thread1 to complete

Console.WriteLine($"Result from thread: {result}");

void Work()
{
    Console.WriteLine("Doing some work");
    Thread.Sleep(3000);

    result = "Here is the result";
}


// Here we are not actually returning the result; we are just using a shared variable
// Results can be returned using a Task which we will study further