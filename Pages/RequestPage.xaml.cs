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
            var allStatus = AppEntities.GetContext().Status.ToList();
            allStatus.Insert(0, new Status
            {
                Name = "все статусы"
            });
            FiltrCB.ItemsSource = allStatus;

             if (Flag.FlagRole == 2)
            {
               
                delBtn.Visibility = Visibility.Collapsed;
            }
            else if (Flag.FlagRole == 4)
            {
          
                delBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
              
                delBtn.Visibility = Visibility.Collapsed;
            }



            Seeching();
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

        private void SerchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Seeching();
        }

        private void FiltrCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seeching();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seeching();
        }
        private void Seeching()
        {
            var allPoisk = AppEntities.GetContext().Request.ToList();

            // Применяем фильтрацию по пользователю
             if (Flag.FlagRole == 4)
            {
                allPoisk = null;
            }
            if (Flag.FlagRole !=1 && Flag.FlagRole !=2) 
            {
                allPoisk = allPoisk.Where(x => x.IdUser == Flag.FlagIdUser).ToList();
            }

            // Применяем фильтрацию по тексту
            allPoisk = allPoisk.Where(x => x.User.Surname.ToLower().Contains(SerchTB.Text.ToLower())
                || (x.User.Name ?? "").ToLower().Contains(SerchTB.Text.ToLower())
                || (x.User.FhaterName ?? "").ToLower().Contains(SerchTB.Text.ToLower())).ToList();

            // Применяем фильтрацию по статусу
            if (FiltrCB.SelectedIndex != 0)
            {
                allPoisk = allPoisk.Where(s => s.Status == FiltrCB.SelectedValue).ToList();
            }

            // Применяем сортировку
            switch (SortCB.SelectedIndex)
            {
                case 1:
                    allPoisk = allPoisk.OrderBy(x => x.StartDate).ToList();
                    break;
                case 2:
                    allPoisk = allPoisk.OrderByDescending(x => x.StartDate).ToList();
                    break;
            }

            // Устанавливаем источник данных
            DGRequest.ItemsSource = allPoisk;
        }



    }
}
