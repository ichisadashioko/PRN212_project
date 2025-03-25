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
using Microsoft.EntityFrameworkCore;

namespace PRN212_project.finalized_ui
{
    /// <summary>
    /// Interaction logic for ViewOwnHouseholdInfoWindow1.xaml
    /// </summary>
    public partial class ViewOwnHouseholdInfoWindow1 : Window
    {
        public User CurrentUser { get; set; }
        public ViewOwnHouseholdInfoWindow1(User user)
        {
            CurrentUser = user;
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("not an authenticated user");
                return;
            }
            var ctx = new Prn212ProjectContext();
            var my_hm_info = ctx.HouseholdMembers.Where(hm => hm.UserId == CurrentUser.UserId).FirstOrDefault();
            if (my_hm_info == null)
            {
                MessageBox.Show("not in any households");
                return;
            }

            var tmp_list = ctx.HouseholdMembers.Include(hm => hm.User).ToList().Where(hm => hm.HouseholdId == my_hm_info.HouseholdId).ToList();
            dg_household_members.ItemsSource = tmp_list;

            //Household my_household_info = ctx.Households.Include(h => h.HeadOfHousehold).Include(h => h.HouseholdMembers).Where(h => h.HouseholdId == my_hm_info.HouseholdId).FirstOrDefault();
            Household my_household_info = ctx.Households.Include(h => h.HeadOfHousehold).Where(h => h.HouseholdId == my_hm_info.HouseholdId).FirstOrDefault();
            if (my_household_info == null)
            {
                MessageBox.Show("system error. invalid household");
                return;
            }

            tb_Address.Text = my_household_info.Address;
            tb_HouseholdId.Text = my_household_info.HouseholdId.ToString();
            tb_HeadOfHousehold.Text = my_household_info.HeadOfHousehold?.ToString();

            dp_CreatedDate.SelectedDate = (my_household_info.CreatedDate != null) ? Utils.FromDateOnly(my_household_info.CreatedDate.Value) : null;
        }
    }
}
