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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF6.Models;
using WPF6.Services;

namespace WPF6.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private int click;
        //comment
        public Autho()
        {
            InitializeComponent();
            click = 0;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, null));
        }

        private void GenerateCaptcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;
            string captchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = captchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
            tbCaptcha.Clear();
            tbPassword.Clear();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text.Trim();
            string passwordHash = Hash.HashPassword(password);

            var db = Computer_Company_FixDBEntities1.GetContext();
            var user = db.users
                .AsEnumerable()
                .FirstOrDefault(x => x.login == login &&
                                     string.Equals(x.password_hash, passwordHash, StringComparison.OrdinalIgnoreCase));

            if (click == 1)
            {
                if (user != null)
                {
                    string roleName = user.roles?.role_name ?? "Клиент";
                    MessageBox.Show("Вы вошли под: " + roleName);
                    LoadPage(roleName, user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCaptcha();
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                {
                    string roleName = user.roles?.role_name ?? "Клиент";
                    MessageBox.Show("Вы вошли под: " + roleName);
                    LoadPage(roleName, user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                    GenerateCaptcha();
                }
            }
        }

        private void LoadPage(string _role, users user)
        {
            click = 0;
            switch (_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user, _role));
                    break;
            }
        }
    }
}
