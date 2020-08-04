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
using System.Threading;

namespace WpfThreadWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public int i_sk = 0;

        public MainWindow()
        {
            InitializeComponent();
            GeneruotiNumeri(2, 15);
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            i_sk = 0;
            listBox.Items.Clear();
            Random rnd = new Random();
            for (int i = 1; i < int.Parse(comboBox.Text.ToString()) + 1; i++)
            {
                new Thread(() =>
                   {
                       i_sk = i_sk+ 1; //Thread.CurrentThread.ManagedThreadId;
                       int abc = i_sk;
                       this.Dispatcher.Invoke(() => { listBox.Items.Add($"Thread {abc}" + "  starting"); });

                       int thread_laikas_sek = rnd.Next(500, 2000);
                       int simboliu_sk = rnd.Next(5, 10);
                       Thread.Sleep(thread_laikas_sek);

                       this.Dispatcher.Invoke(() =>
                       {
                           listBox.Items.Add(abc.ToString() + ", thread:  " + randomstring(int.Parse(simboliu_sk.ToString()))
                           + " time: " + thread_laikas_sek.ToString()+" simb."+ simboliu_sk.ToString());
                       });

                   }).Start();
                   
            }
        }

        public static string randomstring(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(S => S[random.Next(S.Length)]).ToArray());
        }
        public void GeneruotiNumeri(int genStart, int genEnd)
        {
            for (int i = 0; genStart < genEnd + 1; genStart++)
            {
                this.comboBox.Items.Add(genStart);
            }
        }
    }
}
