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
    /// Interaction logic for PoliceManageAllHouseholds.xaml
    /// </summary>
    public partial class PoliceManageAllHouseholds : Window
    {
        public PoliceManageAllHouseholds()
        {
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.Households.Include(h => h.HeadOfHousehold).Include(h => h.HouseholdMembers).ToList();
            dg_main.ItemsSource = tmp_list;
        }

        public void load_cb_head_of_household(Household household)
        {
            var ctx = new Prn212ProjectContext();
            //ctx.HouseholdMembers.Where(hm => hm.HouseholdId)
            var hm_id_list = household.HouseholdMembers.Select(hm => hm.UserId).ToList();
            var hm_user_list = ctx.Users.ToList().Where(u => hm_id_list.Contains(u.UserId)).ToList();
            cb_HeadOfHousehold.ItemsSource = hm_user_list;
            foreach(var user in hm_user_list)
            {
                if(user.UserId == household.HeadOfHouseholdId)
                {
                    cb_HeadOfHousehold.SelectedItem = user;
                    break;
                }
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_view_HouseholdMembers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dg_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_item = dg_main.SelectedItem as Household;
            if (selected_item == null) { return; }
            tb_HouseholdId.Text = selected_item.HouseholdId.ToString();
            //cb_HeadOfHousehold.SelectedItem
            tb_Address.Text = selected_item.Address;
            if (selected_item.CreatedDate == null)
            {
                dp_CreatedDate.SelectedDate = null;
            }
            else
            {
                dp_CreatedDate.SelectedDate = Utils.FromDateOnly(selected_item.CreatedDate.Value);
            }

            label_member_count.Content = selected_item.HouseholdMembers.Count;
            load_cb_head_of_household(selected_item);
        }
    }
}
