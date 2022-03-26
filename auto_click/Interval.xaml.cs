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
using System.Windows.Shapes;

namespace auto_click
{
    /// <summary>
    /// Логика взаимодействия для Interval.xaml
    /// </summary>
    public partial class Interval : Window
    {
        public Interval()
        {
            InitializeComponent();
            Topmost = true;
            millisecText.Text = Convert.ToString(TimeIntertval.Milliseconds);
            secText.Text = Convert.ToString(TimeIntertval.Seconds);
            minText.Text = Convert.ToString(TimeIntertval.Minutes);
            hoursText.Text = Convert.ToString(TimeIntertval.Hours);
        }
        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(millisecText.Text, out TimeIntertval.Milliseconds) && int.TryParse(secText.Text, out TimeIntertval.Seconds) && int.TryParse(minText.Text, out TimeIntertval.Minutes) && int.TryParse(hoursText.Text, out TimeIntertval.Hours) &&
                TimeIntertval.Milliseconds + TimeIntertval.Seconds + TimeIntertval.Minutes + TimeIntertval.Hours > 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
