using System.Windows;
using HeroArena.ViewModels;

namespace HeroArena
{
    public partial class LoginWindow : Window
    {
        private LoginVMX _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginVMX();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage.Text = "Username et password requis!";
                return;
            }

            if (_viewModel.VerifyLogin(username, password))
            {
                // Ouvre la page d'accueil
                HomeWindow homeWindow = new HomeWindow();
                homeWindow.Show();
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Username ou password incorrect!";
            }
        }
    }
}