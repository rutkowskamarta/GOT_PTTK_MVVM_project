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

namespace WpfAndroidMockup.Views
{
    /// <summary>
    /// Interaction logic for PotwierdzanieOdbytejWycieczkiPrzodownikView.xaml
    /// </summary>
    public partial class PotwierdzanieOdbytejWycieczkiPrzodownikView : UserControl
    {
        public WycieczkaViewModel wycieczkaViewModel;
        private Grid previousGridToClose;

        public PotwierdzanieOdbytejWycieczkiPrzodownikView()
        {
            InitializeComponent();
        }

        public void ZareagujGdyListaPusta()
        {
            if (wycieczkaViewModel.WycieczkiObservableCollection.Count == 0)
            {
                WyswietlKomunikat("BRAK WYCIECZEK DO POTWIERDZENIA");
            }
        }

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

        private void WyswietlOknoBraniaUdzialu()
        {
            AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Visible;
        }

        private void WyswietlKomunikat(string wiadomosc)
        {
            Message.Text = wiadomosc;
            BasicKomunikatGrid.Visibility = Visibility.Visible;
        }

        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
            if (previousGridToClose != null)
            {
                previousGridToClose.Visibility = Visibility.Hidden;
            }
        }

        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Button_zamknijAlert(object sender, RoutedEventArgs e)
        {
            AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Hidden;
        }

        private void Button_potwierdz(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat("POMYŚLNIE POTWIERDZONO WYCIECZKĘ");
            AlertCzyPotwierdzaPrzodownikGrid.Visibility = Visibility.Hidden;
            wycieczkaViewModel.PotwierdzAktualnaWycieczke();
            previousGridToClose = AlertCzyUstestniczylPrzodownikGrid;
            wycieczkaViewModel.UsunObecnaWycieczkeZWyswietlania();
        }

        private void Button_wyswietlAlertPotwierdzania(object sender, RoutedEventArgs e)
        {
            if (wycieczkaViewModel.CzyZalogowanyPrzodownikPosiadaUprawnieniaNaCurrentWycieczke())
            {
                AlertCzyUstestniczylPrzodownikGrid.Visibility = Visibility.Hidden;
                AlertCzyPotwierdzaPrzodownikGrid.Visibility = Visibility.Visible;

            }
            else
            {
                WyswietlKomunikat("NIE POSIADASZ UPRAWNIEŃ NA TEN OBSZAR GÓRSKI");
                previousGridToClose = AlertCzyUstestniczylPrzodownikGrid;
                wycieczkaViewModel.OdrzucAktualnaWycieczke();

            }
        }

        private void Button_odrzuc(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat("POMYŚLNIE ODRZUCONO WYCIECZKĘ");
            wycieczkaViewModel.OdrzucAktualnaWycieczke();
            previousGridToClose = AlertCzyPotwierdzaPrzodownikGrid;
            wycieczkaViewModel.UsunObecnaWycieczkeZWyswietlania();
        }
    }
}
