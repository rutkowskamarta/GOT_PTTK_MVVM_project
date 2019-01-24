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
using GOT_PTTK.Models;

namespace WpfAndroidMockup.Views
{
    /// <summary>
    /// Logika widoku PotwierdzanieOdbytejWycieczkiPrzodownikView.xaml
    /// </summary>
    public partial class PotwierdzanieOdbytejWycieczkiPrzodownikView : UserControl
    {
        private const string BRAK_WYCIECZEK_STRING = "BRAK WYCIECZEK DO POTWIERDZENIA";
        private const string BRAK_UPRAWNIEN_STRING = "NIE POSIADASZ UPRAWNIEŃ NA TEN OBSZAR GÓRSKI";
        private const string POMYSLNE_USUNIECIE_WYCIECZKI_STRING = "POMYŚLNIE POTWIERDZONO WYCIECZKĘ";
        private const string ODRZUCENIE_WYCIECZKI_STRING = "POMYŚLNIE ODRZUCONO WYCIECZKĘ";

        /// <summary>
        /// View model wycieczki
        /// </summary>
        public WycieczkaViewModel wycieczkaViewModel;
        private Grid previousGridToClose;

        /// <summary>
        /// Konstruktor nieparametryczny widoku potwierdzania wycieczki przez przodownika
        /// </summary>
        public PotwierdzanieOdbytejWycieczkiPrzodownikView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda wyświetlająca komunikat o pustej liście wycieczek do potwierdzenia
        /// </summary>
        public void ZareagujGdyListaPusta()
        {
            if (wycieczkaViewModel.WycieczkiObservableCollection.Count == 0)
            {
                WyswietlKomunikat(BRAK_WYCIECZEK_STRING);
            }
        }

        /// <summary>
        /// Logika przycisku na element z listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_OnPressed(object sender, MouseButtonEventArgs e)
        {
            ListBox listView = sender as ListBox;
            if (listView.SelectedItem != null)
            {
                WycieczkaModel selectedItem = (WycieczkaModel)listView.SelectedItems[0];
                wycieczkaViewModel.WczytajWycieczke(selectedItem);
                WyswietlOknoBraniaUdzialu();
            }

        }

        /// <summary>
        /// Wyświetla okno brania udziału w wycieczce przez przodownika
        /// </summary>
        private void WyswietlOknoBraniaUdzialu()
        {
            AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Wyświetla podstawowy komunika
        /// </summary>
        /// <param name="wiadomosc"></param>
        private void WyswietlKomunikat(string wiadomosc)
        {
            Message.Text = wiadomosc;
            BasicKomunikatGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// odpowiada za logikę przycisku zamykającegopodstawowy komunikat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
            if (previousGridToClose != null)
            {
                previousGridToClose.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Logika przycisku, odpowiedzialnego za nawigację do menu głownego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przyciski, który zamyka okno uczestnictwa 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_zamknijAlert(object sender, RoutedEventArgs e)
        {
            AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przycisku odpowiedzialnego za potwierdzanie odbycia wycieczki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_potwierdz(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat(POMYSLNE_USUNIECIE_WYCIECZKI_STRING);
            AlertCzyPotwierdzaPrzodownikGrid.Visibility = Visibility.Hidden;
            wycieczkaViewModel.PotwierdzAktualnaWycieczke();
            previousGridToClose = AlertCzyUstestniczylPrzodownikGrid;
            wycieczkaViewModel.UsunObecnaWycieczkeZWyswietlania();
        }

        /// <summary>
        /// Wyświetla okno do potwierdzania wycieczki przez przodownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_wyswietlAlertPotwierdzania(object sender, RoutedEventArgs e)
        {
            if (wycieczkaViewModel.CzyZalogowanyPrzodownikPosiadaUprawnieniaNaCurrentWycieczke())
            {
                AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Hidden;
                AlertCzyPotwierdzaPrzodownikGrid.Visibility = Visibility.Visible;

            }
            else
            {
                WyswietlKomunikat(BRAK_UPRAWNIEN_STRING);
                previousGridToClose = AlertCzyUstestniczylPrzodownikGrid;
                wycieczkaViewModel.OdrzucAktualnaWycieczke();
                wycieczkaViewModel.UsunObecnaWycieczkeZWyswietlania();
            }
        }


        /// <summary>
        /// Logika przycisku odpowiedzialnego za odrzucenie wycieczki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_odrzuc(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat(ODRZUCENIE_WYCIECZKI_STRING);
            wycieczkaViewModel.OdrzucAktualnaWycieczke();
            previousGridToClose = AlertCzyPotwierdzaPrzodownikGrid;
            wycieczkaViewModel.UsunObecnaWycieczkeZWyswietlania();
        }
    }
}
