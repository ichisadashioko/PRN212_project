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
using System.Text.RegularExpressions;

namespace PRN212_project
{
    /// <summary>
    /// Interaction logic for UserWindow1.xaml
    /// </summary>
    public partial class UserWindow1 : Window
    {
        private readonly Prn212ProjectContext _context;
        private User _selectedUser;

        public UserWindow1()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            load_datagrid_users();
            load_cb_roles();
            ResetForm();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context.Dispose();
        }

        public void load_cb_roles()
        {
            List<string> value_list = new List<string>
            {
                "Citizen",
                "AreaLeader",
                "Police"
            };
            cb_role.ItemsSource = value_list;
            cb_role.SelectedIndex = 0;
        }

        public void load_datagrid_users()
        {
            try
            {
                datagrid_users.ItemsSource = _context.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetForm()
        {
            tb_userid.Text = string.Empty;
            tb_fullname.Text = string.Empty;
            tb_Email.Text = string.Empty;
            tb_Password.Password = string.Empty;
            tb_Address.Text = string.Empty;
            cb_role.SelectedIndex = 0;
            _selectedUser = null;
            tb_fullname.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(tb_fullname.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_fullname.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tb_Email.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_Email.Focus();
                return false;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(tb_Email.Text, emailPattern))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_Email.Focus();
                return false;
            }

            if (_selectedUser == null && string.IsNullOrWhiteSpace(tb_Password.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_Password.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tb_Address.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_Address.Focus();
                return false;
            }

            if (cb_role.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                cb_role.Focus();
                return false;
            }

            return true;
        }

        private void datagrid_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedUser = datagrid_users.SelectedItem as User;
                if (_selectedUser == null)
                {
                    ResetForm();
                    return;
                }

                tb_userid.Text = $"{_selectedUser.UserId}";
                tb_fullname.Text = _selectedUser.FullName;
                tb_Email.Text = _selectedUser.Email;
                tb_Address.Text = _selectedUser.Address;
                tb_Password.Password = string.Empty; // Don't show password

                foreach (string role in cb_role.Items)
                {
                    if (role.Equals(_selectedUser.Role))
                    {
                        cb_role.SelectedItem = role;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_user_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Check if email already exists
                if (_context.Users.Any(u => u.Email == tb_Email.Text))
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb_Email.Focus();
                    return;
                }

                var newUser = new User
                {
                    FullName = tb_fullname.Text.Trim(),
                    Email = tb_Email.Text.Trim(),
                    Password = Utils.password_to_sha1(tb_Password.Password),
                    Role = cb_role.SelectedItem as string,
                    Address = tb_Address.Text.Trim()
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                MessageBox.Show("Thêm cư dân thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                load_datagrid_users();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm cư dân: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_user_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Vui lòng chọn cư dân cần cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!ValidateInput())
                    return;

                // Check if email already exists for other users
                if (_context.Users.Any(u => u.Email == tb_Email.Text && u.UserId != _selectedUser.UserId))
                {
                    MessageBox.Show("Email đã tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb_Email.Focus();
                    return;
                }

                _selectedUser.FullName = tb_fullname.Text.Trim();
                _selectedUser.Email = tb_Email.Text.Trim();
                if (!string.IsNullOrWhiteSpace(tb_Password.Password))
                {
                    _selectedUser.Password = Utils.password_to_sha1(tb_Password.Password);
                }
                _selectedUser.Role = cb_role.SelectedItem as string;
                _selectedUser.Address = tb_Address.Text.Trim();

                _context.SaveChanges();

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                load_datagrid_users();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_user_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Vui lòng chọn cư dân cần xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa cư dân {_selectedUser.FullName}?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Users.Remove(_selectedUser);
                    _context.SaveChanges();

                    MessageBox.Show("Xóa cư dân thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    load_datagrid_users();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa cư dân: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
