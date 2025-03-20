using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserWindow2.xaml
    /// </summary>
    public partial class UserWindow2 : Window
    {
        public UserWindow2()
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

        public User popupate_user_from_ui()
        {
            var retval = new User();
            retval.UserId = int.Parse(tb_userid.Text);
            retval.FullName = tb_fullname.Text;
            retval.Address = tb_Address.Text;
            retval.Email = tb_Email.Text;
            retval.Password = tb_Password.Text;
            if (!Utils.is_sha1_hash(retval.Password))
            {
                retval.Password = Utils.password_to_sha1(retval.Password);
            }

            retval.Role = cb_role.SelectedItem as string;

            return retval;
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
            var user = popupate_user_from_ui();
            user.UserId = 0;
            var ctx = new Prn212ProjectContext();
            ctx.Users.Add(user);
            ctx.SaveChanges();
            load_datagrid_users();
        }

        private void btn_user_edit_Click(object sender, RoutedEventArgs e)
        {
            var user = popupate_user_from_ui();
            var ctx = new Prn212ProjectContext();
            ctx.Users.Update(user);
            ctx.SaveChanges();
            load_datagrid_users();
        }

        private void btn_user_delete_Click(object sender, RoutedEventArgs e)
        {
            var user = popupate_user_from_ui();
            var ctx = new Prn212ProjectContext();
            ctx.Users.Remove(user);
            ctx.SaveChanges();
            load_datagrid_users();
        }

        public bool is_search_match(User user, string search_str)
        {
            if (user.FullName.Contains(search_str, StringComparison.OrdinalIgnoreCase)) { return true; }
            if (user.UserId.ToString().Contains(search_str, StringComparison.OrdinalIgnoreCase)) { return true; }
            if (user.Email.Contains(search_str, StringComparison.OrdinalIgnoreCase)) { return true; }
            if (user.Role.Contains(search_str, StringComparison.OrdinalIgnoreCase)) { return true; }
            if (user.Address.Contains(search_str, StringComparison.OrdinalIgnoreCase)) { return true; }

            return false;
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string search_str = tb_search.Text;
            if (string.IsNullOrWhiteSpace(search_str))
            {
                load_datagrid_users();
                return;
            }

            var ctx = new Prn212ProjectContext();
            datagrid_users.ItemsSource = ctx.Users.ToList().Where(user => is_search_match(user, search_str)).ToList();
        }
    }
}
