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
using System.Collections.ObjectModel;

namespace WpfAndroidMockup.Views
{
    /// <summary>
    /// Logika widoku ObslugaWycieczekView.xaml
    /// </summary>
    public partial class PrzegladanieWycieczkiView : UserControl
    {
        /// <summary>
        /// View model wycieczki
        /// </summary>
        public WycieczkaViewModel WycieczkaViewModel;
        private Grid buttonOkBackGrid;

        /// <summary>
        /// Konstruktor nieparametryczny widoku
        /// </summary>
        public PrzegladanieWycieczkiView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Logika wyboru elementu z listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_OnPressed(object sender, MouseButtonEventArgs e)
        {
            ListBox listView = sender as ListBox;
            if (listView.SelectedItem != null)
            {
                WycieczkaModel selectedItem = (WycieczkaModel)listView.SelectedItems[0];
                WycieczkaViewModel.WczytajWycieczke(selectedItem);
                ChangeLayoutToTripLayout();
            }

        }

        /// <summary>
        /// Wyświetlenie widoku z informacjami o wycieczce
        /// </summary>
        private void ChangeLayoutToTripLayout()
        {
            SelectedTripGrid.Visibility = Visibility.Visible;
            SetStatusTextColor();
        }

        /// <summary>
        /// Logika przycisku usuwania wybranej wycieczki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_UsunWycieczkeOnClick(object sender, RoutedEventArgs e)
        {
            if (WycieczkaViewModel.CzyCurrentWycieczkaPotwierdzona())
            {
                WyswietlKomunikat("NIE MOŻNA USUNĄĆ WYCIECZKI POTWIERDZONEJ PRZEZ PRZODOWNIKA");
                buttonOkBackGrid = BasicKomunikatGrid;
            }
            else
            {
                WyswietlPotwierdzenieUsunieciaGrid();
            }
        }

        /// <summary>
        /// Logika przycisku powrotu do wyboru z listy i zamknięcia podglądu wycieczki
        /// </summary
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CofnijDoWyboruOnClick(object sender, RoutedEventArgs e)
        {
            SelectedTripGrid.Visibility = Visibility.Hidden;
            AllTripsGrid.Visibility = Visibility.Visible;
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
        /// Wyświetla podstawowy komunikat
        /// </summary>
        /// <param name="wiadomosc"></param>
        private void WyswietlKomunikat(string wiadomosc)
        {
            Message.Text = wiadomosc;
            BasicKomunikatGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Wyświetla komunikat potwierdzenia chęci usunięcia wycieczki
        /// </summary>
        private void WyswietlPotwierdzenieUsunieciaGrid()
        {
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Zamyka podstawowy komunikat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            buttonOkBackGrid.Visibility = Visibility.Visible;
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przycisku zamykania komunikatu o potwierdzeniu usunięcia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ZamknijKomunikatUsuwania(object sender, RoutedEventArgs e)
        {
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Logika przycisku potwierdzającego chęć usunięcia wycieczki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Usun(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat("POMYSLNIE USUNIETO WYCIECZKE " + WycieczkaViewModel.CurrentWycieczka.Nazwa);
            WycieczkaViewModel.UsunAktualnaWycieczke();
            SelectedTripGrid.Visibility = Visibility.Hidden;
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Hidden;
            buttonOkBackGrid = AllTripsGrid;
            WycieczkiListView.UpdateLayout();
            
        }

        /// <summary>
        /// Zmienia kolor statusu wycieczki w podglądzie na odpowiedni
        /// </summary>
        private void SetStatusTextColor()
        {
            
            if (WycieczkaViewModel.CurrentWycieczka.Status.Equals(StatusyPotwierdzenia.NIEPOTWIERDZONA))
            {
                StatusTextBlock.Foreground = Brushes.Red;
            }
            else if (WycieczkaViewModel.CurrentWycieczka.Status.Equals(StatusyPotwierdzenia.POTWIERDZONA))
            {
                StatusTextBlock.Foreground = Brushes.LawnGreen;
            }
            else
            {
                StatusTextBlock.Foreground = Brushes.Yellow;
            }
        }
    }
}
