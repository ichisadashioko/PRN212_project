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
    /// Interaction logic for PoliceManageHouseholdMembersWindow.xaml
    /// </summary>
    public partial class PoliceManageHouseholdMembersWindow : Window
    {
        public Household Household { get; set; }
        public PoliceManageHouseholdMembersWindow(Household household)
        {
            Household = household;
            InitializeComponent();
            Title = $"{Household}";
            load_dg();
        }

        public void load_dg()
        {
            if (Household == null)
            {
                MessageBox.Show("no household data available");
                return;
            }

            int household_id = Household.HouseholdId;
            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.HouseholdMembers.Include(hm => hm.User).Where(hm => hm.HouseholdId == household_id).ToList();
            dg_household_members.ItemsSource = tmp_list;
            var hm_user_id_list = tmp_list.Select(hm => hm.UserId).ToList();
            var all_user_list = ctx.Users.ToList();
            var non_member_user_list = all_user_list.Where(user => !hm_user_id_list.Contains(user.UserId)).ToList();
            cb_user.ItemsSource = non_member_user_list;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Household == null)
                {
                    MessageBox.Show("no household data available");
                    return;
                }

                int household_id = Household.HouseholdId;

                var selected_item = cb_user.SelectedItem as User;
                if (selected_item == null)
                {
                    MessageBox.Show("no user selected");
                    return;
                }

                var ctx = new Prn212ProjectContext();
                var tmp_list = ctx.HouseholdMembers.Where(hm => hm.HouseholdId == household_id).ToList();
                var hm_user_id_list = tmp_list.Select(hm => hm.UserId).ToList();

                if (hm_user_id_list.Contains(selected_item.UserId))
                {
                    MessageBox.Show($"{selected_item} has already a member");
                    return;
                }

                HouseholdMember member = new HouseholdMember()
                {
                    MemberId = 0,
                    HouseholdId = household_id,
                    Relationship="",
                    UserId = selected_item.UserId
                };

                ctx.HouseholdMembers.Add(member);
                ctx.SaveChanges();

                MessageBox.Show("succesful");

                load_dg();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("failed");
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = from_ui();
                if (member == null)
                {
                    MessageBox.Show("no member selected!");
                    return;
                }

                var ctx = new Prn212ProjectContext();
                ctx.HouseholdMembers.Update(member);
                ctx.SaveChanges();
                MessageBox.Show("succesful");
                load_dg();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("failed");
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = from_ui();
                if(member == null)
                {
                    MessageBox.Show("no member selected!");
                    return;
                }
                var ctx = new Prn212ProjectContext();
                ctx.HouseholdMembers.Remove(member);
                ctx.SaveChanges();
                MessageBox.Show("succesful");
                load_dg();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("failed");
            }
        }

        public HouseholdMember from_ui()
        {
            HouseholdMember member = dg_household_members.SelectedItem as HouseholdMember;
            if (member == null) { return null; }
            int member_id;
            if (Int32.TryParse(tb_MemberId.Text, out member_id))
            {
                member.MemberId = member_id;
            }

            member.HouseholdId = Household.HouseholdId;
            member.Relationship = tb_Relationship.Text;
            return member;
        }

        private void dg_household_members_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_household_members.SelectedItem is not HouseholdMember member) { return; }

            tb_HouseholdId.Text = Household.HouseholdId.ToString();
            tb_MemberId.Text = member.MemberId.ToString();
            tb_Relationship.Text = member.Relationship;
            tb_User.Text = member.User?.ToString();
        }
    }
}
