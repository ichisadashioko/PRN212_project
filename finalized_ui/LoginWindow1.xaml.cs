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
    /// Interaction logic for LoginWindow1.xaml
    /// </summary>
    public partial class LoginWindow1 : Window
    {
        public LoginWindow1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = tb_username.Text;
            string password = tb_password.Text;

            if (!Utils.is_sha1_hash(password))
            {
                //password = Utils.password_to_sha1(password).ToLower();
                password = Utils.password_to_sha1(password);

            }


            User login_user = null;
            var ctx = new Prn212ProjectContext();
            //var login_user = ctx.Users.ToList().Where(user => user.Email.Equals(username) && user.Password.ToLower().Equals(password)).FirstOrDefault();
            foreach (var user in ctx.Users.ToList())
            {
                //if (user.Email.Equals(username) && user.Password.ToLower().Equals(password))
                if (user.Email.Equals(username) && user.Password.Equals(password))
                {
                    login_user = user;
                    break;
                }
            }
            if (login_user == null)
            {
                MessageBox.Show("invalid user");
            }
            else
            {
                var window = new AllCitizenWindow1(login_user);
                window.CurrentUser = login_user;
                window.Show();
                //MessageBox.Show($"welcome {login_user}");

                // TODO

            }
        }
    }
}
