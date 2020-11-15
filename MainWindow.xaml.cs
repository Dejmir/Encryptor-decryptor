using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace Encryptor_decryptor
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool used1 = true, used2 = true;
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!used1) return;
            text.Text = "";
            used1 = false;
        }
        private void Keyy_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!used2) return;
            keyy.Text = "";
            used2 = false;
        }
        private void Keyy_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            count.Content = keyy.Text.Length;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { final.Text = Encrypt(text.Text, keyy.Text); }
            catch { final.Text = "Wrong key or text !"; }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try { final.Text = Decrypt(text.Text, keyy.Text); }
            catch { final.Text = "Wrong key or text !"; }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(final.Text);
        }

        private string Encrypt(string text, string keyy)
        {
            AesCryptoServiceProvider aesCryptoService = new AesCryptoServiceProvider();
            aesCryptoService.KeySize = 256;
            byte[] iv = Encoding.UTF8.GetBytes("MxmBAS9zd9%1B^od"); //16
            byte[] key = Encoding.UTF8.GetBytes($"{keyy}"); //32
            aesCryptoService.IV = iv;
            aesCryptoService.Key = key;
            ICryptoTransform transform = aesCryptoService.CreateEncryptor(key, iv);

            byte[] encrypted = transform.TransformFinalBlock(Encoding.UTF8.GetBytes(text), 0, text.Length);
            text = Convert.ToBase64String(encrypted);

            return text;
        }
        private string Decrypt(string text, string keyy)
        {
            AesCryptoServiceProvider aesCryptoService = new AesCryptoServiceProvider();
            aesCryptoService.KeySize = 256;
            byte[] iv = Encoding.UTF8.GetBytes("MxmBAS9zd9%1B^od"); //16
            byte[] key = Encoding.UTF8.GetBytes($"{keyy}"); //32
            aesCryptoService.IV = iv;
            aesCryptoService.Key = key;
            ICryptoTransform transform = aesCryptoService.CreateDecryptor(key, iv);

            byte[] byteText = Convert.FromBase64String(text);
            byte[] decrypted = transform.TransformFinalBlock(byteText, 0, byteText.Length);
            text = Encoding.UTF8.GetString(decrypted);

            return text;
        }
    }
}
