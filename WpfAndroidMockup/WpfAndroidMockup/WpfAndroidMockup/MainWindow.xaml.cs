using System.Windows;
using GOT_PTTK.Utilities;
using WpfAndroidMockup.ViewModels;

namespace WpfAndroidMockup
{
    /// <summary>
    /// Logika widoku dla for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Konstruktor nieparametryczny głwonego widoku
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Logika dla przycisku odpowiedzialnego za wywołanie przypadku użycia przeglądania wycieczek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ObslugaWycieczek(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            tripViewModelObject.LoadAllWycieczkiToObservableCollection();
            ObslugaWycieczekViewControl.DataContext = tripViewModelObject;
            ObslugaWycieczekViewControl.WycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.CurrentView = ObslugaWycieczekViewControl;
            ObslugaWycieczekViewControl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika dla przycisku odpowiedzialnego za wywołanie przypadku użycia przesyłania odznaki do weryfikacji i przyznania przez pracownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_WyslijDoWeryfikacji(object sender, RoutedEventArgs e)
        {
            OdznakiViewModel odznakiViewModel = new OdznakiViewModel();
            odznakiViewModel.LoadWszystkieRozpoczeteCykle();
            PrzeslijOdznakeDoWeryfikacji.DataContext = odznakiViewModel;
            PrzeslijOdznakeDoWeryfikacji.odznakiViewModel = odznakiViewModel;
            PrzeslijOdznakeDoWeryfikacji.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika dla przycisku odpowiedzialnego za wywołanie przypadku użycia potwierdzania jako przodownik udziału w wycieczce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Potwierdz(object sender, RoutedEventArgs e)
        {
            WycieczkaViewModel tripViewModelObject = new WycieczkaViewModel();
            tripViewModelObject.WczytajWycieczkiPrzodownikaDoPotwierdzenia(Utils.ID_ZALOGOWANEGO_PRZODOWNIKA);
            PotwierdzOdbyteWycieczki.DataContext = tripViewModelObject;
            PotwierdzOdbyteWycieczki.wycieczkaViewModel = tripViewModelObject;
            tripViewModelObject.CurrentView = PotwierdzOdbyteWycieczki;
            PotwierdzOdbyteWycieczki.ZareagujGdyListaPusta();
            PotwierdzOdbyteWycieczki.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika dla przycisku odpowiedzialnego za wywołanie przypadku użycia wysyłania przodownikowy prośby o weryfikację wycieczki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
