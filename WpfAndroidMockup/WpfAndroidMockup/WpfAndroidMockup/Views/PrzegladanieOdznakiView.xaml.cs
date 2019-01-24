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
using WpfAndroidMockup.Models;
using WpfAndroidMockup.ViewModels;

namespace WpfAndroidMockup.Views
{
    /// <summary>
    /// Logika widoku PrzegladanieOdznakiView.xaml
    /// </summary>
    public partial class PrzegladanieOdznakiView : UserControl
    {
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
        private void ListViewItem_OnPressed(object sender, MouseButtonEventArgs e)
        {
            ListBox listView = sender as ListBox;
            if (listView.SelectedItem != null)
            {
                //zmienić na odznaka model
                OdznakaModel selectedItem = (OdznakaModel)listView.SelectedItems[0];
                odznakiViewModel.AktualnaOdznaka = selectedItem;
                ChangeLayoutToOdznakaLayout();
                SetPunktyTextColor();
                ButtonAndTextSetVisibility();
            }

        }
        
        /// <summary>
        /// Wyświetla informacje o wybranej z listy odznace
        /// </summary>
        private void ChangeLayoutToOdznakaLayout()
        {
            SelectedOdznakaGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Logika przycisku zamykania podglądu odznaki i powrotu do listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CofnijDoWyboruOnClick(object sender, RoutedEventArgs e)
        {
            SelectedOdznakaGrid.Visibility = Visibility.Hidden;
        }

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
            if (odznakiViewModel.AktualnaOdznaka.CzyPrzeslanaDoWeryfikacji)
            {
                WeryfikacjaLabel.Content = "Przesłano do weryfikacji";
                WeryfikacjaLabel.Visibility = Visibility.Visible;
                Weryfikuj_button.Visibility = Visibility.Hidden;
            }
            else
            {
                if (odznakiViewModel.AktualnaOdznaka.Pkt < odznakiViewModel.AktualnaOdznaka.MinPkt)
                {
                    WeryfikacjaLabel.Visibility = Visibility.Visible;
                    Weryfikuj_button.Visibility = Visibility.Hidden;
                    WeryfikacjaLabel.Content = "Za mało punktów, by przesłać do weryfikacji";

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
            WyswietlKomunikat("POMYŚLNIE PRZESŁANO DO WERYFIKACJI");
            odznakiViewModel.WyslijOdznakeDoWeryfikacji();
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
    }
}
