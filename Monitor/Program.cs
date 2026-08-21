// Monitor: To monitor the critical section of code.
// Similar to locks, it is also one of the thread synchronization technique.
// lock uses monitor in background to
// block the critical section so that it can be accessed by one thread only
// It is one of the thread synchronisation technique.
// It is similar to lock but it provides more control over the critical section.
// if one thread is inside critical section, monitor blocks other threads from entering
// the critical section.


//// Basic syntax

//object LockCookieObject = new();

//Monitor.Enter(LockCookieObject); 

//// critical section
//try
//{

//}
//catch (Exception)
//{

//	throw;
//}

//// critical section ends here

//finally
//{
//    Monitor.Exit(LockCookieObject); // to release the lock
//}


//// After applying monitor, the critical section can be accessed
//// by maximum one thread only.
//// This makes the critical section inside the body of monitor Atomic

//**************************************************************************************************************



//int counter = 0;
//object counterLock = new();

//Thread thread1 = new Thread(IncrementCounter);
//Thread thread2 = new Thread(IncrementCounter);

//thread1.Start();
//thread2.Start();

//thread1.Join();     //this will block the execution until thread1 is completed
//thread2.Join();


//Console.WriteLine($"Counter value = {counter}");



//void IncrementCounter()
//{
//    for (int i = 0; i < 100; i++)
//    {
//        Monitor.Enter(counterLock); // to acquire the lock
//		try
//		{
//            counter++; //critical section
//        }
//        catch (Exception)
//		{

//			throw;
//		}
//        finally
//        {
//            Monitor.Exit(counterLock); // to release the lock
//        }
//    }
//}

////output must be 200 here

//**************************************************************************************************************

// We can even apply timers to our monitor.
// It uses a different method called as TryEnter
// this timer attepmpts for the specified milliseconds
// to acquire the exclusive locks

int counter = 0;
object counterLock = new();

Thread thread1 = new Thread(IncrementCounter);
Thread thread2 = new Thread(IncrementCounter);

thread1.Start();
thread2.Start();

thread1.Join();     //this will block the execution until thread1 is completed
thread2.Join();


Console.WriteLine($"Counter value = {counter}");



void IncrementCounter()
{
    for (int i = 0; i < 100; i++)
    {
        if (Monitor.TryEnter(counterLock, 100)) // to acquire the lock
        // this will wait for 100 milliseconds and after that it returns false
        // which means we were not succesful in entering the critical section.
        {
            try
            {
                counter++; //critical section
                Console.WriteLine($"counter value {counter}");
                Thread.Sleep(120);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Monitor.Exit(counterLock); // to release the lock
            }
        }

        else
        {
            Console.WriteLine($"The system is busy.  {counter}");
        }

    }
}

