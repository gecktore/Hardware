using Hardware.Components;
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

namespace Hardware.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditListPage.xaml
    /// </summary>
    public partial class AddEditListPage : Page
    {
        private Service service;
        public AddEditListPage(Service _service)
        {
            InitializeComponent();
            service = _service;
            this.DataContext = service;
        }
        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (service.DurationInSeconds < 0 || service.DurationInSeconds > 14400)
            {
                error.AppendLine("Время услуги не может превышать 4 часа или быть отрицательным!");
            }
            if (service.ID == 0)
            {
                if (App.Entities.Service.Any(x => x.Title == service.Title))
                {
                    error.AppendLine("Такая товар уже существуют!");
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    App.Entities.Service.Add(service);
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            App.Entities.SaveChanges();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
