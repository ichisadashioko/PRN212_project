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

namespace PRN212_project.show_all_ui
{
    /// <summary>
    /// Interaction logic for AllHouseholdMembersWindow1.xaml
    /// </summary>
    public partial class AllHouseholdMembersWindow1 : Window
    {
        public AllHouseholdMembersWindow1()
        {
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.HouseholdMembers.Include(hm => hm.User).ToList();
            dg_household_members.ItemsSource = tmp_list;
        }
    }
}
