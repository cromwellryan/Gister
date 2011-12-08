using System.Windows;
using System.Windows.Forms;

namespace EchelonTouchInc.Gister
{
    /// <summary>
    /// Interaction logic for GitHubCredentialsPrompt.xaml
    /// </summary>
    public partial class GitHubCredentialsPrompt : Window, CredentialsPrompt
    {
        public GitHubCredentialsPrompt()
        {
            InitializeComponent();
        }

        public bool? Result { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public void Prompt()
        {
            Result = ShowDialog();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            Username = txtUsername.Text;
            Password = txtPassword.Password;

            DialogResult = true;

            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Username = string.Empty;
            Password = string.Empty;

            DialogResult = false;

            Close();
        }
    }
}
