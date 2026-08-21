// Performing certain tasks require multiple threads to share common resources
// Sharing common resources may run into conflicts

//**************************************************************************************************************

//// Here common variable counter is shared between thread 1 and thread 2. But we have used thread.Join method
//// so that the exectuion of thread 2 starts only when thread1 is completed.
//int counter = 0;

//Thread thread1 =new Thread(IncrementCounter);
//thread1.Start();
//thread1.Join();     //this will block the execution until thread1 is completed


//Thread thread2 = new Thread(IncrementCounter);
//thread2.Start();
//thread2.Join();


//Console.WriteLine($"Counter value = {counter}");



//void IncrementCounter()
//{
//    for (int i = 0; i < 100000; i++)
//    {
//        counter++;
//    }
//}


//// This is running synchronously. So the final value of counter will be 200000 always.


//**************************************************************************************************************

// But what if we run threads asynchrnously.

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
        counter++;
    }
}


// Now when we execute this
// Execution 1: Counter value = 128734
// Execution 2: Counter value = 180315
// Execution 3: Counter value = 135632


// We can notice that the value is always less than 200000.
// Why?
// Because both threads are accessing and modifying the same variable counter at the same time.
// Lets say counter is 1000 and both threads read the value 1000 at the same time.
// thread1 increments the value of 1. so the counter is now 1001.
// thread2 also increments the value of 1. so the counter is again 1001.
//In this way even though the loop runs 200000 times, the value of counter will always be lesser.
// there is a race condition here as the threads are racing to access and modify the same variable.
// We need a technique to solve this type of problem and the technique is called as thread synchronization.



//**************************************************************************************************************


//Critical Section:
//A critical section in multithreading refers to a part of the code that accesses
//shared resources (like variables, files, or data structures) and must not be executed
//by more than one thread at a time to prevent data corruption or inconsistent results.
// In this program, counter which is a shared resource between thread 1 and 2, is a critical section.


// Atomic Operation:
// An atomic operation is an operation that is completed in a single step
// relative to other threads—meaning it cannot be interrupted, split, or observed in an incomplete state.
// It is a low-level operation that is indivisible and thread-safe by nature,
// ensuring that no other thread can see or affect its intermediate state.
//If the operation within the critical section is not atomic there will be synchronization errors.

