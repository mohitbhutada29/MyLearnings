//// Why we need to use multithreading
////Divide and Conquer type of problems
//// Offload long running tasks

////Without threading

//int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//int SumSegment()
//{
//    int sum = 0;
//    for (int i = 0; i < array.Length; i++)
//    {
//        Thread.Sleep(100);
//        sum += array[i];
//    }
//    return sum;
//}

//var startTime = DateTime.Now;

//var sum = SumSegment();

//var endtime = DateTime.Now;

//Console.WriteLine($"The sum is {sum}");
//Console.WriteLine($"Time taken is {(endtime-startTime).TotalMilliseconds}");

////Output
////The sum is 55
////Time taken is 1106.3634

//**************************************************************************************************************

//Multi threading allow us to do a task more efficiently by running multiple threads in parallel.
//This can help us to perform longer tasks simultaneously,
//such as downloading files, processing data, or running background tasks without blocking the main thread.

int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int SumSegment(int start, int end)  //long running CPU intensive calculation
{
    int sum = 0;
    for (int i = start; i < end; i++)
    {
        Thread.Sleep(100);
        sum += array[i];
    }
    return sum;
}

int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;
var startTime = DateTime.Now;

int numOfThreads = 4;
int segmentLength = array.Length / numOfThreads;

var threads = new Thread[numOfThreads];
threads[0] = new Thread(() => { sum1 = SumSegment(0, segmentLength); });
threads[1] = new Thread(() => { sum2 = SumSegment(segmentLength, 2 * segmentLength); });
threads[2] = new Thread(() => { sum3 = SumSegment(2 * segmentLength, 3 * segmentLength); });
threads[3] = new Thread(() => { sum4 = SumSegment(3 * segmentLength, array.Length); });

var endtime = DateTime.Now;

//starting threads

foreach (var thread in threads)
{
    thread.Start();
}

foreach (var thread in threads)
{
    thread.Join(); //blocks the calling thread until current method is executed
}

Console.WriteLine($"The sum is {sum1 + sum2 + sum3 + sum4}");
Console.WriteLine($"Time taken is {(endtime - startTime).TotalMilliseconds}");

//Output
//The sum is 55
//Time taken is 1.8857

//If you compare the two tasks, multithreading helps us to complete more tasks in less time.