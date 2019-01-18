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
using WpfAndroidMockup.ViewModels;
using WpfAndroidMockup.Models;

namespace WpfAndroidMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int PRZYKLADOWY_TURYSTA = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoginUser(PRZYKLADOWY_TURYSTA);
        }
        
        private void LoginUser(int idTurysty)
        {
            DaneLogowania.IdZalogowanegoTurysty = idTurysty;
        }
        
        private void Button_ObslugaWycieczek(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            ObslugaWycieczekViewControl.DataContext = tripViewModelObject;
            ObslugaWycieczekViewControl.WycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.currentView = ObslugaWycieczekViewControl;
            ObslugaWycieczekViewControl.Visibility = Visibility.Visible;
        }

        private void Button_WyslijDoPotwierdzenia(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Potwierdz(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
