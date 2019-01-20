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
    /// Interaction logic for ObslugaWycieczekView.xaml
    /// </summary>
    public partial class PrzegladanieWycieczkiView : UserControl
    {

        public WycieczkaViewModel WycieczkaViewModel;
        private Grid buttonOkBackGrid;

        public PrzegladanieWycieczkiView()
        {
            InitializeComponent();
        }

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

        private void ChangeLayoutToTripLayout()
        {
            SelectedTripGrid.Visibility = Visibility.Visible;
            SetStatusTextColor();
        }

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

        private void Button_CofnijDoWyboruOnClick(object sender, RoutedEventArgs e)
        {
            ChangeLayoutToSelectionLayout();
        }

        private void ChangeLayoutToSelectionLayout()
        {
            SelectedTripGrid.Visibility = Visibility.Hidden;
            AllTripsGrid.Visibility = Visibility.Visible;
        }

        private void Button_CofnijDoMenuOnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void WyswietlKomunikat(string wiadomosc)
        {
            Message.Text = wiadomosc;
            BasicKomunikatGrid.Visibility = Visibility.Visible;
        }

        private void WyswietlPotwierdzenieUsunieciaGrid()
        {
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Visible;
        }

        private void Button_ZamknijKomunikat(object sender, RoutedEventArgs e)
        {
            buttonOkBackGrid.Visibility = Visibility.Visible;
            BasicKomunikatGrid.Visibility = Visibility.Hidden;
        }

        private void Button_ZamknijKomunikatUsuwania(object sender, RoutedEventArgs e)
        {
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Usun(object sender, RoutedEventArgs e)
        {
            WyswietlKomunikat("POMYSLNIE USUNIETO WYCIECZKE " + WycieczkaViewModel.CurrentWycieczka.Nazwa);
            WycieczkaViewModel.UsunAktualnaWycieczke();
            SelectedTripGrid.Visibility = Visibility.Hidden;
            CzyNaPewnoChceszUsunacWycieczkeGrid.Visibility = Visibility.Hidden;
            buttonOkBackGrid = AllTripsGrid;
            WycieczkiListView.UpdateLayout();
            
        }

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
