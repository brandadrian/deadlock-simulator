using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DeadlockSimulatorWpf
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Button_Click start; ThreadId; {Thread.CurrentThread.ManagedThreadId}");
            var task = ExecuteAsync();
            Debug.WriteLine($"Set current thread on wait; ThreadId; {Thread.CurrentThread.ManagedThreadId}");
            task.Wait();
            Debug.WriteLine($"Button_Click done; ThreadId; {Thread.CurrentThread.ManagedThreadId}");
            Button1.Background = new SolidColorBrush(Colors.Red);
        }

        private async Task ExecuteAsync()
        {
            Debug.WriteLine($"ExecuteAsync start; ThreadId; {Thread.CurrentThread.ManagedThreadId}");
            //No Deadlock
            await Task.Delay(2000).ConfigureAwait(false); // Will continue the following code in different thread than caller.
            //Deadlock
            //await Task.Delay(2000);
            Debug.WriteLine($"ExecuteAsync done; ThreadId; {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
