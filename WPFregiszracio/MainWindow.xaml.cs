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
using System.IO;
using System.Text.RegularExpressions;

namespace WPFregiszracio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> felhasznalok = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsValidEmail(string email) 
        {
            int  atIndex = email.IndexOf("@");
            if (atIndex < 0 || atIndex!=email.LastIndexOf("@")) return false;
            string local = email.Substring(0,atIndex);
            string domain = email.Substring(atIndex+1);

            if (string.IsNullOrEmpty(local)||string.IsNullOrEmpty(domain)) return false;
            int dotIndex = domain.LastIndexOf(".");
            if (dotIndex < 0 || dotIndex>=domain.Length-2) return false;
            if (domain.Contains("..")) return false;
            if (string.IsNullOrEmpty(email)) return false;

            return true;
        }

        private void btn_regisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbx_nev.Text)||string.IsNullOrWhiteSpace(tbx_email.Text)|| string.IsNullOrWhiteSpace(tbx_jelszo1.Text) || string.IsNullOrWhiteSpace(tbx_jelszo2.Text))
            {
                MessageBox.Show("A mezők kitöltése kötelező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (feltetel.IsChecked == true && tbx_jelszo1.Text == tbx_jelszo2.Text)
                {
                    string nev = tbx_nev.Text;
                    string email = tbx_email.Text;
                    string jelszo = tbx_jelszo1.Text;
                    var egyFelhasznalo = new User(nev, email, jelszo);
                    felhasznalok.Add(egyFelhasznalo);
                    using (StreamWriter iro = new StreamWriter("felhasznalok.txt", true))
                    {
                        iro.WriteLine($"{egyFelhasznalo.Nev};{egyFelhasznalo.Email};{egyFelhasznalo.Jelszo}");
                    }
                    MessageBox.Show("Sikeres regisztráció!");
                }
                else
                {
                    MessageBox.Show("Fogadd el feltételeket, vagy ellenőrizd a jelszavak egyezőségét!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
            
            
        }

        private void tbx_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(tbx_email.Text))
            {
                tbx_email.BorderBrush = Brushes.Green;

            }
            else
            {
                tbx_email.BorderBrush=  Brushes.Red;
                MessageBox.Show("Nem megfelelő az e-mail formátuma!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool IsValidPassword(string jelszo)
        {
            jelszo = tbx_jelszo1.Text;
            if (!Regex.IsMatch(jelszo, @"(?=.*\d)")) return false;
            if (!Regex.IsMatch(jelszo, @"(?=.*[^\w\s])")) return false;
            if (jelszo.Length<8) return false;
       
            return true;
        }

        private void tbx_jelszo1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsValidPassword(tbx_jelszo1.Text))
            {
                tbx_jelszo1.BorderBrush = Brushes.Green;
            }
            else
            {
                tbx_jelszo1.BorderBrush = Brushes.Red;
                MessageBox.Show("Nem megfelelő a jelszo formátuma!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
