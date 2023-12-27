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

namespace WpfApp3.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avtoriz.xaml
    /// </summary>
    public partial class Avtoriz : Page
    {
        public static int FlagRole;
        public static int FlagIdUser;
        public Avtoriz()
        {
            InitializeComponent();
        }

        private void logInBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Userrr = AppEntities.GetContext().User.FirstOrDefault(x => x.Login == LoginTB.Text && x.Password == PassTB.Password);
                if ( Userrr == null)
                {
                    MessageBox.Show("net takogo");
                }
                else
                {
                    Flag.FlagRole = Userrr.IdRole;
                    Flag.FlagIdUser = Userrr.Id;

                    switch (Userrr.IdRole)
                    {
                        case 1:
                            NavigationService.Navigate(new RequestPage());
                            break;
                        case 2:
                            NavigationService.Navigate(new RequestPage());
                            break;
                        case 3:
                            NavigationService.Navigate(new RequestPage());
                            break;
                        case 4:
                            NavigationService.Navigate(new NewReq(null));
                            break;
                    }
                }
                
            }
            catch(Exception )
            {
                MessageBox.Show("oyy");
            }
        }

        private void GostBTN_Click(object sender, RoutedEventArgs e)
        {
            Flag.FlagRole = 4;
            NavigationService.Navigate(new NewReq(null));
        }
    }
}
