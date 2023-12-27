using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для NewReq.xaml
    /// </summary>
    public partial class NewReq : Page
    {
        private Request newReq = new Request();
        public NewReq(Request reqSelect)
        {
            InitializeComponent();
            if (reqSelect != null)
            {
                newReq = reqSelect;
            }
            //else
            //{

            //    newReq.IdUser = Flag.FlagIdUser;
            //    newReq.StartDate = DateTime.Now;
            //    newReq.IdStatus = 1;
            //}


            DataContext = newReq;
               
            statusCB.ItemsSource = AppEntities.GetContext().Status.ToList();

            bool canEdit = Flag.FlagRole == 1 || Flag.FlagRole == 2;

            FamTB.IsEnabled = canEdit;
            NameTB.IsEnabled = canEdit;
            OtchTB.IsEnabled = canEdit;
            DataTB.IsEnabled = canEdit;
            statusCB.IsEnabled = canEdit;
        }

        private void backBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestPage());
        }

        private void savBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(newReq.Description)) error.AppendLine("Обязательное поле! опишите проблему!");
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                    return;
                }
                if( newReq.Id == 0)
                {
                    newReq.IdUser = Flag.FlagIdUser;
                    newReq.StartDate = DateTime.Now;
                    newReq.IdStatus = 1;
                }
                AppEntities.GetContext().Request.Add(newReq);
                AppEntities.GetContext().SaveChanges();
                MessageBox.Show("uspech");
                if (Flag.FlagRole == 1|| Flag.FlagRole == 2|| Flag.FlagRole == 3)
                NavigationService.Navigate(new RequestPage());
                else
                {
                    NavigationService.Navigate(new NewReq(null));

                }
            }
            catch (Exception){
                MessageBox.Show("doom");
            }
        }
    }
}
