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
        public NotificationWindow1()
        {
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            if(CurrentUser == null) { return; }

            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.Notifications.Where(n => n.UserId == CurrentUser.UserId).ToList();
            datagrid_notification.ItemsSource = tmp_list;
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            load_dg();
        }
    }
}
