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
    /// Logika widoku PotwierdzanieOdbytejWycieczkiTurystaView.xaml
    /// </summary>
    public partial class PotwierdzanieOdbytejWycieczkiTurystaView : UserControl
    {
        /// <summary>
        /// View model wycieczki
        /// </summary>
        public WycieczkaViewModel wycieczkaViewModel;
        private const int WRONG_INPUT = -1;
        Grid previousGridToClose;

        /// <summary>
        /// Konstruktor ineparametryczny widoku
        /// </summary>
        public PotwierdzanieOdbytejWycieczkiTurystaView()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Logika wybrania elementu z listy
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
                WyswietlOknoWybieraniaPrzodownika();
            }

        }

        /// <summary>
        /// Wyświetla okno wyboru przodownika
        /// </summary>
        private void WyswietlOknoWybieraniaPrzodownika()
        {
            AlertPrzeslijDoPrzodownikaGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika przycisku nawigacji wstecznej do menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Wyświetla komunikat w przypadku pustej listy
        /// </summary>
        public void ZareagujGdyListaPusta()
        {
            if(wycieczkaViewModel.WycieczkiObservableCollection.Count == 0)
            {
                WyswietlKomunikat("BRAK WYCIECZEK DO POTWIERDZENIA");
            }
        }

        /// <summary>
        /// Wyświetla podstawowy komunikat
        /// </summary>
        /// <param name="wiadomosc"></param>
        private void WyswietlKomunikat(string wiadomosc)
        {
            Message.Text = wiadomosc;
            BasicKomunikatGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika przycisku zamykającego podtawowy komunikat
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
        /// Logika przycisku zamykania okna wyboru przodownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_powrot(object sender, RoutedEventArgs e)
        {
            AlertPrzeslijDoPrzodownikaGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przycisku przesyłania wycieczki do potwierdzenia porzodownikowi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_wyslij(object sender, RoutedEventArgs e)
        {
            long nrPrzodownika = ConvertTextFromTextBox();

            if(nrPrzodownika == WRONG_INPUT || !wycieczkaViewModel.CzyPrzodownikONumerzeIstnieje(nrPrzodownika))
            {
                WyswietlKomunikat("PRZODOWNIK NR " + NrPrzodownika_textbox.Text + " NIE ISTNIEJE W BAZIE");
            }
            else
            {
                previousGridToClose = AlertPrzeslijDoPrzodownikaGrid;
                wycieczkaViewModel.WyslijWycieczkeDoPotwierdzenia(nrPrzodownika);
                WyswietlKomunikat("POMYŚLNIE WYSŁANO PROŚBĘ O POTWIERDZENIE");

                
            }
            
        }

        /// <summary>
        /// Konwertuje tekst wprowadzony do okienka na long
        /// </summary>
        /// <returns>skonwerotwany tekst</returns>
        private long ConvertTextFromTextBox()
        {
            long id = WRONG_INPUT;
            try
            {
                id = long.Parse(NrPrzodownika_textbox.Text);

            }
            catch (Exception e)
            {

            }
            return id;
        }
        

    }
}
