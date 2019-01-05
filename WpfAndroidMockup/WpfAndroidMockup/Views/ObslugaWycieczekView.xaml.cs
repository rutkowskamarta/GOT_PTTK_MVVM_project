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
            Console.WriteLine("klik!");

            ListView listView = sender as ListView;
            Wycieczka selectedItem = (Wycieczka) listView.SelectedItems[0];
            Console.WriteLine(selectedItem.Name);
            WycieczkaViewModel.SetCurrentWycieczka(selectedItem);
            ChangeLayoutToTripLayout();

        }

        private void ChangeLayoutToTripLayout()
        {
            AllTripsGrid.Visibility = Visibility.Hidden;
            SelectedTripGrid.Visibility = Visibility.Visible;
        }
    }
}
