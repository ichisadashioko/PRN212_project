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

namespace PRN212_project
{
    /// <summary>
    /// Interaction logic for NotificationWindow1.xaml
    /// </summary>
    public partial class NotificationWindow1 : Window
    {
        public User CurrentUser { get; set; }
        public NotificationWindow1(User user)
        {
            CurrentUser = user;
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            if (CurrentUser == null) { return; }

            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.Notifications.Where(n => n.UserId == CurrentUser.UserId).ToList();
            datagrid_notification.ItemsSource = tmp_list;
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            load_dg();
        }

        private void cb_is_read_Checked(object sender, RoutedEventArgs e)
        {
            var selected_item = datagrid_notification.SelectedItem as Notification;
            if (selected_item == null) { return; }
            if (selected_item.IsRead ?? false) { return; }
            selected_item.IsRead = true;
            var ctx = new Prn212ProjectContext();
            ctx.Notifications.Update(selected_item);
            ctx.SaveChanges();
            load_dg();
        }

        private void datagrid_notification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_item = datagrid_notification.SelectedItem as Notification;
            if (selected_item == null) { return; }
            tb_NotificationId.Text = selected_item.NotificationId.ToString();
            tb_Message.Text = selected_item.Message;
            dp_SentDate.SelectedDate = (selected_item.SentDate != null) ? selected_item.SentDate.Value : null;
            cb_is_read.IsChecked = selected_item.IsRead;
        }
    }
}
