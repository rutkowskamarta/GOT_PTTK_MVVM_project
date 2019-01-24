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
using GOT_PTTK.Models;
using WpfAndroidMockup.ViewModels;

namespace WpfAndroidMockup.Views
{
    /// <summary>
    /// Logika widoku PrzegladanieOdznakiView.xaml
    /// </summary>
    public partial class PrzegladanieOdznakiView : UserControl
    {
        private const string PRZESLANO_DO_WERYFIKACJI_STATE_STRING = "Przesłano do weryfikacji";
        private const string ZA_MALO_PUNKTOW_STATE_STRING = "Za mało punktów, by przesłać do weryfikacji";
        private const string POMYSLNIE_PRZESLANO_DO_WERYFIKACJI_STRING = "POMYŚLNIE PRZESŁANO DO WERYFIKACJI";

        /// <summary>
        /// View model odznaki
        /// </summary>
        public OdznakiViewModel odznakiViewModel;

        /// <summary>
        /// Konstruktor nieparametryczny widoku
        /// </summary>
        public PrzegladanieOdznakiView()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Logika przycisku nawigacji wstecznej do menu głównego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przycisku wyboru elementu z listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            

        /// <summary>
        /// Ustala kolor punktów w zależności od zdobycia odpowiedniej ich liczby
        /// </summary>
        private void SetPunktyTextColor()
        {
            if (odznakiViewModel.AktualnaOdznaka.Pkt < odznakiViewModel.AktualnaOdznaka.MinPkt)
            {
                Punkty_text.Foreground = Brushes.LightCoral;
                
            }
            else
            {
                Punkty_text.Foreground = Brushes.LightGreen;
            }

        }

        /// <summary>
        /// Przełącza pomiędzy widokiem przycisku a komuniaktu o weryfikacji
        /// </summary>
        private void ButtonAndTextSetVisibility()
        {
            if (odznakiViewModel.AktualnaOdznaka.NrPracownika == OdznakaModel.NR_PRACOWNIKA_DO_WERYFIKACJI)
            {
                WeryfikacjaLabel.Content = PRZESLANO_DO_WERYFIKACJI_STATE_STRING;
                WeryfikacjaLabel.Visibility = Visibility.Visible;
                Weryfikuj_button.Visibility = Visibility.Hidden;
            }
            else
            {
                if (odznakiViewModel.AktualnaOdznaka.Pkt < odznakiViewModel.AktualnaOdznaka.MinPkt)
                {
                    WeryfikacjaLabel.Visibility = Visibility.Visible;
                    Weryfikuj_button.Visibility = Visibility.Hidden;
                    WeryfikacjaLabel.Content = ZA_MALO_PUNKTOW_STATE_STRING;

                }
                else
                {
                    WeryfikacjaLabel.Visibility = Visibility.Hidden;
                    Weryfikuj_button.Visibility = Visibility.Visible;

                }
            }
            
        }

        /// <summary>
        /// Zamyka podstawowy komunikat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
            ButtonAndTextSetVisibility();
        }

        /// <summary>
        /// Logika przycisku przesyłu odznaki do weryfikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_przeslijDoWeryfikacji(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat(POMYSLNIE_PRZESLANO_DO_WERYFIKACJI_STRING);
            odznakiViewModel.WyslijAktualnaOdznakeDoWeryfikacji();
        }

        /// <summary>
        /// Wyświetla podstawowy komunikat
        /// </summary>
        /// <param name="wiadomosc"></param>
        private void WyswietlKomunikat(string wiadomosc)
        {
            BasicKomunikatGrid.Visibility = Visibility.Visible;
            Message.Text = wiadomosc;
        }

        /// <summary>
        /// Ustawia, żeby wyswietlaly sie odpowiednie elementy i kolory na widoku
        /// </summary>
        public void PokazWidok()
        {
            if(odznakiViewModel.AktualnaOdznaka != null)
            {
                SetPunktyTextColor();
                ButtonAndTextSetVisibility();
            }
            else
            {
                SelectedOdznakaGrid.Visibility = Visibility.Hidden;
                WyswietlKomunikat("NIE MASZ ŻADNEGO ROZPOCZĘTEGO CYKLU ZDOBYWANIA ODZNAKI");
            }
        }
    }
}
