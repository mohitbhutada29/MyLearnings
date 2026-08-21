//namespace OffloadingTasks
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            ShowMessageBigFatTask("First Message", 2000);
//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            ShowMessageBigFatTask("Second Message", 3000);
//        }

//        private void ShowMessageBigFatTask(string message, int delay)
//        {
//            Thread.Sleep(delay); // Simulating a long-running task
//            label1.Text = message;
//        }
//    }
//}

//// The threads itself here have the thread affinity problems which is covered later
//// When you click on button 1 or button 2, the UI thread is blocked for 2 or 3 seconds respectively.
//// within this time if you try clicking on other thread buttons, they will not respond until the first thread is completed.

//// without threading, button1_Click, button2_Click is run on the main thread and blocks it
//// until the task is completed.

//**************************************************************************************************************


// this issue can be resolved by multi threading as shown below

namespace OffloadingTasks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var thread1 = new Thread(()=>ShowMessageBigFatTask("First Message", 2000));
            thread1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var thread2 = new Thread(() => ShowMessageBigFatTask("Second Message", 3000));
            thread2.Start();
        }

        private void ShowMessageBigFatTask(string message, int delay)
        {
            Thread.Sleep(delay); // Simulating a long-running task
            label1.Text = message;
        }
    }
}

// Now this functions are delegated to a new thread, so the UI thread is not blocked.