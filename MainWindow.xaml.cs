using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRN212_project.finalized_ui;
using PRN212_project.show_all_ui;

namespace PRN212_project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btn_2_1_window_Click(object sender, RoutedEventArgs e)
    {
        var window = new PoliceManageAllUsersWindow();
        window.Show();
    }

    private void RegistrationsWindow1_btn_Click(object sender, RoutedEventArgs e)
    {
        var window = new RegistrationsWindow1();
        window.Show();
    }

    private void login_window_Click(object sender, RoutedEventArgs e)
    {

        var window = new LoginWindow1();
        window.Show();
    }

    private void all_logs_window_Click(object sender, RoutedEventArgs e)
    {
        var window = new AllLogsWindow1();
        window.Show();

    }

    private void test_UserRegistrationsWindow1_Click(object sender, RoutedEventArgs e)
    {
        var ctx = new Prn212ProjectContext();
        var user = ctx.Users.ToList().FirstOrDefault();

        var window = new UserRegistrationsWindow1(user);
        window.Show();
    }

    private void test_AllCitizenWindow1_Police_Click(object sender, RoutedEventArgs e)
    {
        var ctx = new Prn212ProjectContext();
        var user = ctx.Users.ToList().Where(u => u.Role == "Police").FirstOrDefault();

        var window = new AllCitizenWindow1(user);
        window.Show();
    }

    private void test_AllCitizenWindow1_AreaLeader_Click(object sender, RoutedEventArgs e)
    {
        var ctx = new Prn212ProjectContext();
        var user = ctx.Users.ToList().Where(u => u.Role == "AreaLeader").FirstOrDefault();

        var window = new AllCitizenWindow1(user);
        window.Show();
    }

    private void test_AllCitizenWindow1_Citizen_Click(object sender, RoutedEventArgs e)
    {
        var ctx = new Prn212ProjectContext();
        var user = ctx.Users.ToList().Where(u => u.Role == "Citizen").FirstOrDefault();

        var window = new AllCitizenWindow1(user);
        window.Show();
    }

    private void test_AllNotificationWindow1_Click(object sender, RoutedEventArgs e)
    {
        var window = new AllNotificationWindow1();
        window.Show();

    }

    private void test_AllHouseholdsWindow1_Click(object sender, RoutedEventArgs e)
    {
        var window = new AllHouseholdsWindow1();
        window.Show();

    }

    private void test_AllHouseholdMembersWindow1_Click(object sender, RoutedEventArgs e)
    {
        var window = new AllHouseholdMembersWindow1();
        window.Show();
    }

    private void test_PoliceManageAllHouseholds_Click(object sender, RoutedEventArgs e)
    {
        var window = new PoliceManageAllHouseholds();
        window.Show();
    }
}
