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
    /// Interaction logic for PotwierdzanieOdbytejWycieczkiTurystaView.xaml
    /// </summary>
    public partial class PotwierdzanieOdbytejWycieczkiTurystaView : UserControl
    {
        public WycieczkaViewModel wycieczkaViewModel;
        private const int WRONG_INPUT = -1;
        Grid previousGridToClose;

        public PotwierdzanieOdbytejWycieczkiTurystaView()
        {
            InitializeComponent();
            
        }

        
        private void ListViewItem_OnPressed(object sender, MouseButtonEventArgs e)
        {
            ListBox listView = sender as ListBox;
            if (listView.SelectedItem != null)
            {
                WycieczkaModel selectedItem = (WycieczkaModel)listView.SelectedItems[0];
                wycieczkaViewModel.SetCurrentWycieczka(selectedItem);
                WyswietlOknoWybieraniaPrzodownika();
            }

        }

        private void WyswietlOknoWybieraniaPrzodownika()
        {
            AlertPrzeslijDoPrzodownikaGrid.Visibility = Visibility.Visible;
        }

        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        public void ZareagujGdyListaPusta()
        {
            if(wycieczkaViewModel.WycieczkiObservableCollection.Count == 0)
            {
                WyswietlKomunikat("BRAK WYCIECZEK DO POTWIERDZENIA");
            }
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

        private void Button_powrot(object sender, RoutedEventArgs e)
        {
            AlertPrzeslijDoPrzodownikaGrid.Visibility = Visibility.Hidden;
        }

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
                wycieczkaViewModel.ZmienStatus(nrPrzodownika, StatusyPotwierdzenia.WTRAKCIE);
                WyswietlKomunikat("POMYŚLNIE WYSŁANO PROŚBĘ O POTWIERDZENIE");

                
            }
            
        }

        private void WyslijDoPrzodownikaIZmienStatus()
        {
            
        }

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
