using DemoApp.WPF.UserControls.DataEncryption.Cryptography;
using System.Windows;
using System.Windows.Controls;
namespace DemoApp.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for DataEncryption.xaml
    /// </summary>
    public partial class DataEncryptionUserControl : UserControl
    {
        public DataEncryptionUserControl()
        {
            InitializeComponent();
        }

        #region UI Events
        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            tBoxEncryptionResult.Text = CryptoService.EncryptString(tBoxEncrypt.Text);
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            tBoxDecryptionResult.Text = CryptoService.DecryptString(tBoxDecrypt.Text);
        }
        #endregion
    }
}
