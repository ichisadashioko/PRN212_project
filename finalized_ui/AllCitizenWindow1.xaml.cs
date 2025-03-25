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
using PRN212_project.finalized_ui;
using PRN212_project.show_all_ui;

namespace PRN212_project
{
    /// <summary>
    /// Interaction logic for AllCitizenWindow1.xaml
    /// </summary>
    public partial class AllCitizenWindow1 : Window
    {
        public AllCitizenWindow1(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            disable_feature_base_on_current_user_role();
            Title = $"{CurrentUser}";
        }

        public User CurrentUser { get; set; }

        public void disable_feature_base_on_current_user_role()
        {
            if (CurrentUser == null) { return; }
            if (CurrentUser.Role == "Citizen")
            {
                stackpanel_arealeader_features.Visibility = Visibility.Collapsed;
                stackpanel_police_only_features.Visibility = Visibility.Collapsed;
            }
            else if (CurrentUser.Role == "AreaLeader")
            {
                stackpanel_police_only_features.Visibility = Visibility.Collapsed;
            }
            else
            {

            }
        }

        private void view_notification_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new NotificationWindow1();
            window.CurrentUser = CurrentUser;
            window.Show();
        }

        private void registrations_btn_view_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserRegistrationsWindow1(CurrentUser);
            window.CurrentUser = CurrentUser;
            window.Show();

        }

        private void view_info_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserViewInfoWindow1();
            window.CurrentUser = CurrentUser;
            window.Show();

        }

        private void btn_manage_registrations_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManageRegistrationsWindow1(CurrentUser);
            window.Show();

        }

        private void view_logs_btn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AllLogsWindow1();
            window.Show();
        }

        private void btn_manage_households_Click(object sender, RoutedEventArgs e)
        {
            var window = new PoliceManageAllHouseholds();
            window.Show();
        }

        private void btn_manage_users_Click(object sender, RoutedEventArgs e)
        {
            var window = new PoliceManageAllUsersWindow();
            window.Show();
        }
    }
}
