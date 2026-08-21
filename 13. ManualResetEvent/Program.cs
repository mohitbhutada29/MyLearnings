// Signalling between threads using ManualResetEvent
// similar to AutoResetEvent but the signal remains on until
// it is manually turned off

//using ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

//manualResetEvent.Set(); // switch on the signal
//manualResetEvent.Reset(); // switch off the signal

//**************************************************************************************************************

using ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

Console.WriteLine("Press enter to release all threads");

for (int i = 0; i < 3; i++)
{
    Thread thread = new Thread(Work);
    thread.Name = $"Thread {i + 1}";
    thread.Start();
}

//All the threads are blocked in the beginning as the initialstate is false.
Console.ReadLine();

manualResetEvent.Set(); //turn on the signal to release all threads
Console.ReadLine();

void Work()
{
    Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for signal..." );
    manualResetEvent.Wait(); // waits for the signal to proceed
    Thread.Sleep(2000);
    Console.WriteLine($"{Thread.CurrentThread.Name} has been released.");
}