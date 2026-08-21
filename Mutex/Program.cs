//// Mutex
//// Another thread synchronization mechanism.
//// similar to lock and monitor

//// Basic syntax

//using (var mutex = new Mutex())
//{
//    mutex.WaitOne();
//	try
//	{
//		// critical section
//	}
//	catch (Exception)
//	{
//		throw;
//	}

//	finally
//	{
//		mutex.ReleaseMutex();
//	}
//}

//// why use Mutex when we have locks and monitors?
//// Mutex can be used not only within the process but also across the processes.
//// Each app can have multiple processes.
//// You can see this in task manager.
//// Multiple processes can try to acccess the shared resources.
//// We cannot use locks or monitor because they block the threads within the same processes.
//// Mutex can be used in situations where we want to block the threads/ lock the
//// critical section across multiple processes.

//// Mutex (bool initiallyOwned, string? nameofTheMutex, out bool createdNew)

//// We are using "using" here. This is because we want to dispose the mutex.
//// Why? This is a resource within an OS, similar to file.

//**************************************************************************************************************

//// Global Mutex example

//// int counter which we used in the previous example cant be used here.
//// This is because it is a shared resource within a process and cant be shared
//// across multiple processes.
//// Hence in this exmaple we will create an txt file to output our counter
//// and this will then be a shared resource acorss the process.

//string filePath = "counter.txt";

//for (int i = 0; i < 5000; i++)
//{
//    int counter = ReadCounter(filePath);
//    counter++;
//    WriteCounter(filePath, counter);
//}

//Console.WriteLine("Process finished");
//Console.ReadLine();

//int ReadCounter(string filePath)
//{
//    using (var stream = new FileStream(
//        filePath,
//        FileMode.Create,
//        FileAccess.Read,
//        FileShare.ReadWrite))
//    {
//        using (var reader = new StreamReader(stream))
//        {
//            var content = reader.ReadToEnd();
//            if (int.TryParse(content, out int counter))
//            {
//                return counter;
//            }
//            return 0;
//        }
//    }
//}

//void WriteCounter(string filePath, int counter)
//{
//    using (var stream = new FileStream(
//        filePath,
//        FileMode.Create,
//        FileAccess.Write,
//        FileShare.ReadWrite))
//    {
//        using (var writer = new StreamWriter(stream))
//        {
//            writer.Write(counter);
//        }
//    }
//}

//// build this and run the exe in the bin folder
//// when you run the exe twice; the expected output in the txt file must be 10000.
//// but you will see its less than 10000.
//// (why running the exe twice: so that the same shared resource "counter.txt" is
//// been accessed by two different processes.)
//// here there is a race condition as both processes are accessing and modifying
//// the same file at the same time. Due to which we get inconsistent results.
//// Hence we need some kind of thread synchronization technique here;
//// locks, monitor wont work becuase synchronization needs to be across processes.
//// Hence we use mutex here.
///

//**************************************************************************************************************

//// Try using locks acorss the processes
//// same example as above

//string filePath = "counter.txt";
//object fileLock = new();
//for (int i = 0; i < 5000; i++)
//{
//    lock (fileLock)
//    {
//        int counter = ReadCounter(filePath);
//        counter++;
//        WriteCounter(filePath, counter);
//    }
//}

//Console.WriteLine("Process finished");
//Console.ReadLine();

//int ReadCounter(string filePath)
//{
//    using (var stream = new FileStream(
//        filePath,
//        FileMode.Create,
//        FileAccess.Read,
//        FileShare.ReadWrite))
//    {
//        using (var reader = new StreamReader(stream))
//        {
//            var content = reader.ReadToEnd();
//            if (int.TryParse(content, out int counter))
//            {
//                return counter;
//            }
//            return 0;
//        }
//    }
//}

//void WriteCounter(string filePath, int counter)
//{
//    using (var stream = new FileStream(
//        filePath,
//        FileMode.Create,
//        FileAccess.Write,
//        FileShare.ReadWrite))
//    {
//        using (var writer = new StreamWriter(stream))
//        {
//            writer.Write(counter);
//        }
//    }
//}

//// we will get same inconsistent results in this case too.
////lock doesnt works

//**************************************************************************************************************

//Using Mutex

string filePath = "counter.txt";
object fileLock = new();

// When we give a name to the mutex; it becomes global across all the processes
using (var mutex = new Mutex(false, $"GlobalFileMutex:{filePath}"))
{
    for (int i = 0; i < 5000; i++)
    {
        mutex.WaitOne();  // acquire the mutex
        {
            try
            {
                //Critical section
                int counter = ReadCounter(filePath);
                counter++;
                WriteCounter(filePath, counter);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}

Console.WriteLine("Process finished");
Console.ReadLine();

int ReadCounter(string filePath)
{
    using (var stream = new FileStream(
        filePath,
        FileMode.OpenOrCreate,
        FileAccess.Read,
        FileShare.ReadWrite))
    {
        using (var reader = new StreamReader(stream))
        {
            var content = reader.ReadToEnd();
            if (int.TryParse(content, out int counter))
            {
                return counter;
            }
            return 0;
        }
    }
}

void WriteCounter(string filePath, int counter)
{
    using (var stream = new FileStream(
        filePath,
        FileMode.OpenOrCreate,
        FileAccess.Write,
        FileShare.ReadWrite))
    {
        using (var writer = new StreamWriter(stream))
        {
            writer.Write(counter);
        }
    }
}

// output is 10000 as expected