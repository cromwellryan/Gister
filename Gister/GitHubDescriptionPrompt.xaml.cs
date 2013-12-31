using System.Windows;

namespace EchelonTouchInc.Gister
{
    /// <summary>
    /// Interaction logic for GitHubDescriptionPrompt.xaml
    /// </summary>
    public partial class GitHubDescriptionPrompt : Window, IDescriptionPrompt
    {
        public GitHubDescriptionPrompt()
        {
            InitializeComponent();
        }

        public bool GistPrivate{get;private set;}

        public string Description { get; private set; }

        public void Prompt()
        {
            ShowDialog();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            Description = txtDescription.Text;
            GistPrivate = chkPrivate.IsChecked==true?true:false;
            DialogResult = true;
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

    }
}
