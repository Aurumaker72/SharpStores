using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace SharpStores.Demo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly Writable<int> Value = new(0);
        public readonly Readable<DateTime> Time;

        public MainWindow()
        {
            InitializeComponent();
            Value.Subscribe(value =>
            {
                ValueTextBlock.Text = value.ToString();
            });
            Time = new(DateTime.Now, set =>
            {
                void Tick(object? sender, EventArgs e)
                {
                    set(DateTime.Now);
                }

                DispatcherTimer dispatcherTimer = new()
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                dispatcherTimer.Tick += Tick;
                dispatcherTimer.Start();

                return () =>
                {
                    dispatcherTimer.Tick -= Tick;
                    dispatcherTimer.Stop();
                };
            });

            Time.Subscribe(value =>
            {
                Title = value.ToString();
            }); 

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Value.Update(x => x - 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Value.Update(x => x + 1);
        }
    }
}
