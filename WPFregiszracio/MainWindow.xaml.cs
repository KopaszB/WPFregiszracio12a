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

        private void btn_regisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (feltetel.IsChecked==true && tbx_jelszo1.Text==tbx_jelszo2.Text)
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
            }
            else
            {
                MessageBox.Show("Fogadd el feltételeket, vagy ellenőrizd a jelszavak egyezőségét!", "Hiba!", MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            
        }
    }
}
