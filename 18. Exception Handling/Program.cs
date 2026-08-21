//// Exception Handling

//// Each thread has its own call stack

//var thread = new Thread(() =>
//{
//    throw new InvalidOperationException(
//        "An error occured in this worker thread. This is expected");
//});

//thread.Start();
//thread.Join();

//// This thread is not reflected in the main/calling thread.
//// Gets reflected only in worker thread.
///

//**************************************************************************************************************

//// If we try to handle the exception using try catch in the main thread
//// It will never be caught in the catch block since the exception is in the worker thread
//// And main thread cannot catch it.

//Thread thread = null;
//try
//{
//    thread = new Thread(() =>
//    {
//        throw new InvalidOperationException(
//            "An error occured in this worker thread. This is expected");
//    });
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}

//thread.Start();
//thread.Join();

//// This thread is not reflected in the main/calling thread.
//// Gets reflected only in worker thread.

//**************************************************************************************************************

//// You need to handle the thread within a thread

//Thread thread = null;

//thread = new Thread(() =>
//{
//    try
//    {
//        throw new InvalidOperationException(
//      "An error occured in this worker thread. This is expected");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//});

//thread.Start();
//thread.Join();

//// This thread is not reflected in the main/calling thread.
//// Gets reflected only in worker thread.


//**************************************************************************************************************

// We create a shared resource List<Exception> to have multiple exceptions thrown
// by different threads. In catch block just add all the exceptions

// You need to handle the thread within a thread

List<Exception> exceptions = new();
object lockExceptions = new();

Thread thread1 = new Thread(Work);

Thread thread2 = new Thread(Work);


thread1.Start();
thread2.Start();


thread1.Join();
thread2.Join();

void Work(object? obj)
{
    try
    {

    }
    catch (Exception ex)
    {
        lock (lockExceptions)
        {
            exceptions.Add(ex);
        }
        throw;
    }
}