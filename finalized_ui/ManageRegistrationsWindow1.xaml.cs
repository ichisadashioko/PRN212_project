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

namespace PRN212_project.finalized_ui
{
    /// <summary>
    /// Interaction logic for ManageRegistrationsWindow1.xaml
    /// </summary>
    public partial class ManageRegistrationsWindow1 : Window
    {
        public User CurrentUser { get; set; }
        public ManageRegistrationsWindow1(User user)
        {
            CurrentUser = user;
            InitializeComponent();
            load_dg();
            load_cb_registration_type();
            load_cb_registration_status();
        }

        public void load_dg()
        {
            var ctx = new Prn212ProjectContext();
            dg_main.ItemsSource = ctx.Registrations.ToList();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var ctx = new Prn212ProjectContext();
            int registration_id = Int32.Parse(tb_RegistrationId.Text);

            Registration registration_obj = ctx.Registrations.ToList().Where(r => r.RegistrationId == registration_id).FirstOrDefault();
            if(registration_obj == null)
            {
                MessageBox.Show($"invalid registration id - {registration_id}");
                return;
            }

            registration_obj.ApprovedBy = CurrentUser.UserId;
            if(cb_registration_status.SelectedValue == null)
            {
                MessageBox.Show($"invalid Status");
                return;
            }

            registration_obj.Status = cb_registration_status.SelectedValue as string;
            registration_obj.Comments = tb_Comments.Text;

            ctx.Registrations.Update(registration_obj);
            ctx.SaveChanges();
            registration_obj = ctx.Registrations.ToList().Where(r => r.RegistrationId == registration_id).FirstOrDefault();
            load_dg();
            populate_ui(registration_obj);

        }

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {

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

        public void populate_ui(Registration registration_obj)
        {
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
