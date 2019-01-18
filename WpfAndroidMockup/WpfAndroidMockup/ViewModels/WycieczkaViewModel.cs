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
        public ObservableCollection<WycieczkaModel> wycieczkiObservableCollection { get; set; }
        public UserControl currentView;
        public WycieczkaModel currentWycieczka { get; set; }
        private WycieczkiContext wycieczkiContext;

        public WycieczkaViewModel()
        {
            wycieczkiContext = WycieczkiContext.GetInstance();
            LoadWycieczkiToObservableCollection();
            currentWycieczka = new WycieczkaModel(new TurystaModel(0), "", StatusyPotwierdzenia.NIEPOTWIERDZONA);

        }

        private void LoadWycieczkiToObservableCollection()
        {
            wycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetWycieczkiZalogowanegoTurysty();

            foreach (var item in wycieczki)
            {
                wycieczkiObservableCollection.Add(item);
            }
        }

        public void SetCurrentWycieczka(WycieczkaModel wycieczka)
        {
            currentWycieczka.Copy(wycieczka);
        }
    }
}
