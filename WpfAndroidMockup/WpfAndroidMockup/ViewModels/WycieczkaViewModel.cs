using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAndroidMockup.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfAndroidMockup.ViewModels
{
    public class WycieczkaViewModel
    {
        public ObservableCollection<Wycieczka> Wycieczki { get; set; }
        public UserControl CurrentView;
        public Wycieczka CurrentWycieczka { get; set; }

        public void LoadExamplaryTrips()
        {
            Wycieczki = new ObservableCollection<Wycieczka>();
            Wycieczki.Add(new Wycieczka(1, "Dominicza Góra"));
            Wycieczki.Add(new Wycieczka(2, "Wycieczka1"));
            Wycieczki.Add(new Wycieczka(3, "Wycieczka2"));
            CurrentWycieczka = new Wycieczka(0, "");
        }

        public void SetCurrentWycieczka(Wycieczka wycieczka)
        {
            CurrentWycieczka.Copy(wycieczka);
        }
    }
}
