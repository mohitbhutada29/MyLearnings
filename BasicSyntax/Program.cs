
//Uncomment each //********** one by one to see the output
//**************************************************************************************************************
//void WriteThreadID()
//{
//    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//}

//// output "1": displays the main thread
//WriteThreadID();

//**************************************************************************************************************

////To start another thread
////We need to pass a method to the Thread constructor which acts as a delegate
////Thread executes this function
//// Create a new thread -> Assign a new task to the thread -> Start the thread
//var thread1 = new Thread(WriteThreadID);

//// output is different as we are now starting a different thread.
//// this thread number are assigned by OS
//thread1.Start();


//**************************************************************************************************************


//// Running threads parallely

//void WriteThreadID100Times()
//{
//    for (int i = 0; i < 100; i++)
//    {
//        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//        Thread.Sleep(50);
//    }
//}

//var thread2 = new Thread(WriteThreadID100Times);
//var thread3 = new Thread(WriteThreadID100Times);
//thread2.Start();
//thread3.Start();


//// here you can see thread2 started, displayed thread8 100 times and then thread3 started.
//// but this might not happen everytime
//// we get mix match output, because its on the thread scheduler to decide which thread to run at what time.
////now when we add thread.sleep(50) when one thread is sleeping, other is executed. thus thread scheduler has
//// to work more hard to pull in and out the threads and passing them to CPU


//**************************************************************************************************************

//// Running on main thread

//void WriteThreadID100Times()
//{
//    for (int i = 0; i < 100; i++)
//    {
//        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//        Thread.Sleep(50);
//    }
//}


//var thread2 = new Thread(WriteThreadID100Times);
//var thread3 = new Thread(WriteThreadID100Times);

////WriteThreadID100Times(); //running on main thread, this is not blocking
////thread1 will be printed 100 times and then other threads are executed

//thread2.Start();
//thread3.Start();

//WriteThreadID100Times(); // mix match output, as this is running on main thread


//**************************************************************************************************************


// Setting priorities to threads

//using System.ComponentModel.DataAnnotations.Schema;

//void WriteThreadID100Times()
//{
//    for (int i = 0; i < 100; i++)
//    {
//        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//    }
//}

//var thread2 = new Thread(WriteThreadID100Times);
//var thread3 = new Thread(WriteThreadID100Times);

//// we have 5 different types of ThreadPriority
//thread2.Priority = ThreadPriority.Highest; // Highest priority thread 8 
//Thread.CurrentThread.Priority = ThreadPriority.Normal; // Main thread 1
//thread3.Priority = ThreadPriority.Lowest; // Lowest priority thread 7

//thread2.Start();
//thread3.Start();

//WriteThreadID100Times();


// Results wont be same if the thread sleeps, as it assigns CPU core to different thread
// Priority doesnt matter if the thread is sleeping, as it is not using CPU core, Thread scheduler kicks 
// it off the core


// Threads can also have names


//thread2.Name = "Thread2";
//thread3.Name = "Thread3";
