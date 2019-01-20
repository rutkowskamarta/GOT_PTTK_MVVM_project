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
        private const long PRZYKLADOWY_PRZODOWNIK = 1;

        public MainWindow()
        {
            InitializeComponent();
            LoginUser(PRZYKLADOWY_TURYSTA, PRZYKLADOWY_PRZODOWNIK);
        }
        
        private void LoginUser(int idTurysty, long nrPrzodownika)
        {
            DaneLogowania.IdZalogowanegoTurysty = idTurysty;
            DaneLogowania.NrZalogowanegoPrzodownika = nrPrzodownika;

        }
        
        private void Button_ObslugaWycieczek(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            tripViewModelObject.LoadAllWycieczkiToObservableCollection();
            ObslugaWycieczekViewControl.DataContext = tripViewModelObject;
            ObslugaWycieczekViewControl.WycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.CurrentView = ObslugaWycieczekViewControl;
            ObslugaWycieczekViewControl.Visibility = Visibility.Visible;
        }

        private void Button_WyslijDoWeryfikacji(object sender, RoutedEventArgs e)
        {
            OdznakiViewModel odznakiViewModel = new OdznakiViewModel();
            odznakiViewModel.LoadWszystkieRozpoczeteCykle();
            PrzeslijOdznakeDoWeryfikacji.DataContext = odznakiViewModel;
            PrzeslijOdznakeDoWeryfikacji.odznakiViewModel = odznakiViewModel;
            PrzeslijOdznakeDoWeryfikacji.Visibility = Visibility.Visible;
        }

        private void Button_Potwierdz(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            tripViewModelObject.WczytajWycieczkiPrzodownikaDoPotwierdzenia(DaneLogowania.NrZalogowanegoPrzodownika);
            PotwierdzOdbyteWycieczki.DataContext = tripViewModelObject;
            PotwierdzOdbyteWycieczki.wycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.CurrentView = PotwierdzOdbyteWycieczki;
            PotwierdzOdbyteWycieczki.ZareagujGdyListaPusta();
            PotwierdzOdbyteWycieczki.Visibility = Visibility.Visible;
        }

        private void Button_WyslijDoPotwierdzenia(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            tripViewModelObject.WczytajNiepotwierdzoneWycieczkiTurysty();
            PrzeslijWycieczkeDoWeryfikacji.DataContext = tripViewModelObject;
            PrzeslijWycieczkeDoWeryfikacji.wycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.CurrentView = PrzeslijWycieczkeDoWeryfikacji;
            PrzeslijWycieczkeDoWeryfikacji.ZareagujGdyListaPusta();
            PrzeslijWycieczkeDoWeryfikacji.Visibility = Visibility.Visible;
        }
               
    }
}
