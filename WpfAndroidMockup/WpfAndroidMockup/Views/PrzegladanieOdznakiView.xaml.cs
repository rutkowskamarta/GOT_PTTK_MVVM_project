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
    /// Interaction logic for PrzegladanieOdznakiView.xaml
    /// </summary>
    public partial class PrzegladanieOdznakiView : UserControl
    {
        public OdznakiViewModel odznakiViewModel;

        public PrzegladanieOdznakiView()
        {
            InitializeComponent();
        }

        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }


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
        
        private void ChangeLayoutToOdznakaLayout()
        {
            SelectedOdznakaGrid.Visibility = Visibility.Visible;
        }

        private void Button_CofnijDoWyboruOnClick(object sender, RoutedEventArgs e)
        {
            SelectedOdznakaGrid.Visibility = Visibility.Hidden;
        }

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

        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
            ButtonAndTextSetVisibility();
        }

        private void Button_przeslijDoWeryfikacji(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat("POMYŚLNIE PRZESŁANO DO WERYFIKACJI");
            odznakiViewModel.PrzeslijAktualnaOdznakeDoWeryfikacji();
        }

        private void WyswietlKomunikat(string wiadomosc)
        {
            BasicKomunikatGrid.Visibility = Visibility.Visible;
            Message.Text = wiadomosc;
        }
    }
}
