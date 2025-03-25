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

namespace PRN212_project
{
    /// <summary>
    /// Interaction logic for AllLogsWindow1.xaml
    /// </summary>
    public partial class AllLogsWindow1 : Window
    {
        public AllLogsWindow1()
        {
            InitializeComponent();
            load_dg();
        }

        public void load_dg()
        {
            var ctx = new Prn212ProjectContext();
            var tmp_list = ctx.Logs.Include(log => log.User).ToList();
            dg_main.ItemsSource = tmp_list;
        }
    }
}
