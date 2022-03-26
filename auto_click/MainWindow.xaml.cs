using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Threading;
using System.IO;

namespace auto_click
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;
            KeyPress.OnKeyPressed += KeyPress_OnKeyPress;
            KeyPress.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }
        static DispatcherTimer timer = new DispatcherTimer();
        static bool _start = false;
        static bool _stop = true;
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        [Flags]
        public enum MOUSEEVENTF
        {
            MOVE = 0x01,
            LEFTDOWN = 0x02,
            LEFTUP = 0x04,
            RIGHTDOWN = 0x08,
            RIGHTUP = 0x10,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            XDOWN = 0x0080,
            XUP = 0x0100,
            WHEEL = 0x0800,
            HWHEEL = 0x01000,
            MOVE_NOCOALESCE = 0x2000,
            VIRTUALDESK = 0x4000,
            ABSOLUTE = 0x8000
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_start == true)
            {
                return;
            }
            timer.Interval = new TimeSpan(0, TimeIntertval.Hours, TimeIntertval.Minutes, TimeIntertval.Seconds, TimeIntertval.Milliseconds);
            timer.Start();
            _start = true;
            _stop = false;

        }
        private static void timer_Tick(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            mouse_event((uint)(MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.LEFTDOWN), x, y, 0, 0);
            mouse_event((uint)(MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.LEFTUP), x, y, 0, 0);
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (_stop == true)
            {
                return;
            }
            timer.Stop();
            _start = false;
            _stop = true;
        }
        static void KeyPress_OnKeyPress(KeyPress.Key Key)
        {
            if (Key == KeyPress.Key.Ctrl)
            {
                if (_stop == true)
                {
                    return;
                }
                timer.Stop();
                _start = false;
                _stop = true;
            }
            if (Key == KeyPress.Key.F6)
            {
                if (_start == true)
                {
                    return;
                }
                else
                {
                    timer.Interval = new TimeSpan(0, TimeIntertval.Hours, TimeIntertval.Minutes, TimeIntertval.Seconds, TimeIntertval.Milliseconds);
                    timer.Start();
                    _start = true;
                    _stop = false;
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            KeyPress.Stop();
            timer.Stop();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_start == true)
            {
                return;
            }
            Interval interval = new Interval();
            interval.ShowDialog();
            using (StreamWriter save = new StreamWriter("config.ini"))
            {
                save.WriteLine(TimeIntertval.Milliseconds);
                save.WriteLine(TimeIntertval.Seconds);
                save.WriteLine(TimeIntertval.Minutes);
                save.WriteLine(TimeIntertval.Hours);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader open = new StreamReader("config.ini"))
                {
                    TimeIntertval.Milliseconds = Convert.ToInt32(open.ReadLine());
                    TimeIntertval.Seconds = Convert.ToInt32(open.ReadLine());
                    TimeIntertval.Minutes = Convert.ToInt32(open.ReadLine());
                    TimeIntertval.Hours = Convert.ToInt32(open.ReadLine());
                }
            }
            catch
            {
                return;
            }
        }
    }
}
