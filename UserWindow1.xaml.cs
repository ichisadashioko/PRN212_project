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
    /// Interaction logic for UserWindow1.xaml
    /// </summary>
    public partial class UserWindow1 : Window
    {
        public UserWindow1()
        {
            InitializeComponent();
            load_datagrid_users();
            load_cb_roles();
        }

        public void load_cb_roles()
        {
            List<string> value_list = new List<string>();
            value_list.Add("Citizen");
            value_list.Add("AreaLeader");
            value_list.Add("Police");

            cb_role.ItemsSource = value_list;
        }

        public void load_datagrid_users()
        {
            // TODO refactor to MVC
            var ctx = new Prn212ProjectContext();
            datagrid_users.ItemsSource = ctx.Users.ToList();
        }

        private void datagrid_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = datagrid_users.SelectedItem as User;
            if (user == null) { return; }

            tb_userid.Text = $"{user.UserId}";
            tb_fullname.Text = user.FullName;
            tb_Email.Text = user.Email;
            tb_Address.Text = user.Address;
            tb_Password.Text = user.Password;

            var current_role_value = user.Role;

            foreach (object item in cb_role.Items)
            {
                if (item == null) { continue; }
                string item_s = item as string;
                if (item_s == null)
                {
                    continue;
                }

                if (item_s.Equals(current_role_value))
                {
                    cb_role.SelectedItem = item;
                    break;
                }
            }
        }

        private void btn_user_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_user_edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_user_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
