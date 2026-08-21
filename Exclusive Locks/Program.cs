// One of the synchronisation technique is called exclusive locks.
// locks are used to lock the critical section so that
// it can be accessed only by one thread at a time.
// Basic syntax
// lock(object)
//{
//  .....critical section
//}
//


// when you apply the lock this way, the critical section can be accessed
// by maximum one thread only
// this makes the critical section inside the body of lock Atomic
// they can be DIVISIBLE (can be interrupted by other threads at
// an intermediate level) but applying lock
// makes them indivisible.


// Consider same example from previous section

object counterLock = new();  // lock object
//System.Threading.Lock counterLock = new();  // looks like this in c# 13
int counter = 0;

Thread thread1 = new Thread(IncrementCounter);
Thread thread2 = new Thread(IncrementCounter);

thread1.Start();
thread2.Start();

thread1.Join();     //this will block the execution until thread1 is completed
thread2.Join();


Console.WriteLine($"Counter value = {counter}");


void IncrementCounter()
{
    for (int i = 0; i < 100000; i++)
    {
        lock(counterLock)   // after applying this, the body
                            // within the lock can be executed by only
                            // one thread at a time
        {
            counter++;      //critical section
        }
    }
}

//out put is:
// Execution 1: Counter value = 200000


//**************************************************************************************************************

// Lock in itself have try catch and finally mechanism.
