using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRN212_project
{
    /// <summary>
    /// Interaction logic for AllCitizenWindow1.xaml
    /// </summary>
    public partial class AllCitizenWindow1 : Window
    {
        public AllCitizenWindow1()
        {
            InitializeComponent();
        }

        public User CurrentUser { get; set; }

        private void view_notification_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NotificationWindow1();
            window.CurrentUser = CurrentUser;
            window.Show();
        }

        private void registrations_btn_view_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserRegistrationsWindow1();
            window.CurrentUser = CurrentUser;
            window.Show();

        }

        private void view_info_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserViewInfoWindow1();
            window.CurrentUser = CurrentUser;
            window.Show();

        }
        //public AllCitizenWindow1(User user)
        //{
        //    CurrentUser = user;
        //}
    }
}
