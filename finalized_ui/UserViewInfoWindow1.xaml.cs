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
    /// Interaction logic for UserViewInfoWindow1.xaml
    /// </summary>
    public partial class UserViewInfoWindow1 : Window
    {
        public User CurrentUser { get; set; }
        public UserViewInfoWindow1()
        {
            InitializeComponent();
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

        public void populate_data()
        {
            if(CurrentUser == null) { return; }

            User user = CurrentUser;
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

        private void btn_user_edit_Click(object sender, RoutedEventArgs e)
        {
            var user = popupate_user_from_ui();
            var ctx = new Prn212ProjectContext();
            ctx.Users.Update(user);
            ctx.SaveChanges();

            CurrentUser = ctx.Users.Where(u => u.UserId == CurrentUser.UserId).FirstOrDefault();
            populate_data();
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            populate_data();
        }
    }
}
