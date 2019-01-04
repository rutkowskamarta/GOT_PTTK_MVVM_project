using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAndroidMockup.Models;
using System.Collections.ObjectModel;

namespace WpfAndroidMockup.ViewModels
{
    class WycieczkaViewModel
    {
        public ObservableCollection<Wycieczka> Wycieczki { get; set;}

        public void LoadExamplaryTrips()
        {
            Wycieczki = new ObservableCollection<Wycieczka>();
            Wycieczki.Add(new Wycieczka());
        }
    }
}
