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
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();
            if( Flag.FlagRole == 1)
            {
                DGRequest.ItemsSource = AppEntities.GetContext().Request.ToList();
            }
            else if (Flag.FlagRole == 2)
            {
                DGRequest.ItemsSource = AppEntities.GetContext().Request.ToList();
                delBtn.Visibility = Visibility.Collapsed;
            }
            else if (Flag.FlagRole == 4) 
            {
                DGRequest.ItemsSource = null;
            }
            else
            {
                DGRequest.ItemsSource = AppEntities.GetContext().Request.Where( x => x.IdUser == Flag.FlagIdUser).ToList();
                delBtn.Visibility = Visibility.Collapsed;
                

            }
        }

        private void ExLogBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Avtoriz());
        }

        private void newBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewReq(null));
        }

     

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var delReq = DGRequest.SelectedItems.Cast<Request>().ToList();
            if ( MessageBox.Show("вы уверены?", "предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                
                try
                {
                    AppEntities.GetContext().Request.RemoveRange(delReq);
                    AppEntities.GetContext().SaveChanges();
                    MessageBox.Show("udalili");
                    DGRequest.ItemsSource = AppEntities.GetContext().Request.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
        }

        private void redaktBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewReq((sender as Button).DataContext as Request));
           
        }

     
    }
}
