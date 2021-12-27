using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace datagridtimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Users = new List<UserTab>();
            UserTab user = new UserTab();
            this.Users = user.GetUsers();
            dgSimple.ItemsSource = this.Users;

            this.StartMockValue();

            this.StartRefreshDataGrid();
        }

        private List<UserTab> Users;

        private void StartMockValue()
        {
            // NOTE: this is a thread to mock the business logic, it change 'Time' value.
            Task.Run(() =>
            {
                var rando = new Random();
                while (true)
                {
                    foreach (var user in this.Users)
                    {
                            user.Time = user.Time + 1;
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        public void StartRefreshDataGrid()
        {
            // NOTE: this is a thread to update data grid.
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var user in this.Users)
                    {
                        // NOTE: update if the time changed.
                        if (user.DisplayTime != user.Time)
                        {
                            user.DisplayTime = user.Time;
                        }
                    }

                    // NOTE: refresh the grid every seconds.
                    Thread.Sleep(1000);
                }
            });
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string input = TxtSearch.Text;
            foreach (var mc in this.Users)
            {
                if(mc.AgentName.ToLower() == input.ToLower())
                {
                    mc.Time = 0;
                }
            }
        }
    }


}  