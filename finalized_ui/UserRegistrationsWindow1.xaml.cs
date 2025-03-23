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
    /// Interaction logic for UserRegistrationsWindow1.xaml
    /// </summary>
    public partial class UserRegistrationsWindow1 : Window
    {
        public User CurrentUser { get; set; }
        public UserRegistrationsWindow1(User user)
        {
            CurrentUser = user;
            InitializeComponent();
            load_dg();
            load_cb_registration_type();
            load_cb_registration_status();
            tb_UserId.Text = CurrentUser.UserId.ToString();
        }

        public void load_dg()
        {
            if (CurrentUser == null)
            {
                return;
            }
            var ctx = new Prn212ProjectContext();
            dg_main.ItemsSource = ctx.Registrations.Where(i => i.UserId == CurrentUser.UserId).ToList();
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            load_dg();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Registration registration_obj = new Registration();
            registration_obj.RegistrationId = 0;

            registration_obj.UserId = CurrentUser.UserId;
            if (cb_registration_type.SelectedValue == null)
            {
                MessageBox.Show("Please select RegistrationType!");
                return;
            }
            else
            {
                registration_obj.RegistrationType = cb_registration_type.SelectedValue as string;
            }

            //registration_obj.Comments = tb_Comments.Text;
            registration_obj.ApprovedBy = null;
            registration_obj.Status = "Pending";
            if (dp_startdate.SelectedDate == null)
            {
                registration_obj.StartDate = Utils.FromDateTime(DateTime.Now);
            }
            else
            {
                registration_obj.StartDate = Utils.FromDateTime(dp_startdate.SelectedDate.Value);
            }
            if (dp_EndDate.SelectedDate == null)
            {
                registration_obj.EndDate = null;
            }
            else
            {
                registration_obj.EndDate = Utils.FromDateTime(dp_EndDate.SelectedDate.Value);
            }

            var ctx = new Prn212ProjectContext();
            ctx.Registrations.Add(registration_obj);
            ctx.SaveChanges();

            load_dg();
        }


        public void load_cb_registration_type()
        {
            List<string> value_list = new List<string>();
            value_list.Add("Permanent");
            value_list.Add("Temporary");
            value_list.Add("TemporaryStay");

            cb_registration_type.ItemsSource = value_list;
        }

        public void load_cb_registration_status()
        {
            List<string> value_list = new List<string>();
            value_list.Add("Pending");
            value_list.Add("Approved");
            value_list.Add("Rejected");

            cb_registration_status.ItemsSource = value_list;
        }
        private void dg_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var registration_obj = dg_main.SelectedItem as Registration;
            if (registration_obj == null) { return; }

            tb_RegistrationId.Text = registration_obj.RegistrationId.ToString();
            tb_UserId.Text = registration_obj.UserId.ToString();
            cb_registration_type.SelectedValue = registration_obj.RegistrationType;
            tb_Comments.Text = registration_obj.Comments;
            tb_ApprovedBy.Text = registration_obj.ApprovedBy.ToString();
            cb_registration_status.SelectedValue = registration_obj.Status;
            dp_startdate.SelectedDate = Utils.FromDateOnly(registration_obj.StartDate);
            if (registration_obj.EndDate == null)
            {
                dp_EndDate.SelectedDate = null;
            }
            else
            {
                dp_EndDate.SelectedDate = Utils.FromDateOnly(registration_obj.EndDate.Value);
            }
        }
    }
}
