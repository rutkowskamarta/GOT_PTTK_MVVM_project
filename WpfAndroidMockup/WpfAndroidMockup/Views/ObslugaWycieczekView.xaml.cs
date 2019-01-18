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
    public partial class ObslugaWycieczekView : UserControl
    {
        
        public WycieczkaViewModel WycieczkaViewModel;

        public ObslugaWycieczekView()
        {
            InitializeComponent();
        }

        private void ListViewItem_OnPressed(object sender, MouseButtonEventArgs e)
        {
            ListBox listView = sender as ListBox;
            if (listView.SelectedItem != null)
            {
                WycieczkaModel selectedItem = (WycieczkaModel)listView.SelectedItems[0];
                WycieczkaViewModel.SetCurrentWycieczka(selectedItem);
                ChangeLayoutToTripLayout();
            }

        }

        private void ChangeLayoutToTripLayout()
        {
            AllTripsGrid.Visibility = Visibility.Hidden;
            SelectedTripGrid.Visibility = Visibility.Visible;
        }

        private void Button_UsunWycieczkeOnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_CofnijDoWyboruOnClick(object sender, RoutedEventArgs e)
        {
            ChangeLayoutToSelectionLayout();
        }

        private void ChangeLayoutToSelectionLayout()
        {
            AllTripsGrid.Visibility = Visibility.Visible;
            SelectedTripGrid.Visibility = Visibility.Hidden;
        }
    }
}
