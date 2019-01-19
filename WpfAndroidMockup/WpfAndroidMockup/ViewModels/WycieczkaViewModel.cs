using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAndroidMockup.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfAndroidMockup.ViewModels
{
    public class WycieczkaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WycieczkaModel> wycieczkiObservableCollection;
        private WycieczkaModel currentWycieczka;
        private WycieczkiContext wycieczkiContext;
                
        public ObservableCollection<WycieczkaModel> WycieczkiObservableCollection
        {
            get
            {
                return wycieczkiObservableCollection;
            }
            set
            {
                if (wycieczkiObservableCollection != value)
                {
                    wycieczkiObservableCollection = value;
                    RaisePropertyChanged("WycieczkiObservableCollection");
                }

            }

        }
        
        public WycieczkaModel CurrentWycieczka
        {
            get
            {
                return currentWycieczka;
            }
            set
            {
                if (currentWycieczka != value)
                {
                    currentWycieczka = value;
                    RaisePropertyChanged("CurrentWycieczka");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public UserControl CurrentView;

        public WycieczkaViewModel()
        {
            wycieczkiContext = WycieczkiContext.GetInstance();
            TurystaModel t = new TurystaModel();
            OdznakaModel o = new OdznakaModel(ref t);
            CurrentWycieczka = new WycieczkaModel(1, ref t, ref o, "", StatusyPotwierdzenia.NIEPOTWIERDZONA);

        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void LoadAllWycieczkiToObservableCollection()
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetWycieczkiZalogowanegoTurysty();

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        public void LoadNiepotwierdzoneWycieczkiToObservableCollection()
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetNiepotwierdzoneWycieczkiTurysty(DaneLogowania.IdZalogowanegoTurysty);

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        public void SetCurrentWycieczka(WycieczkaModel wycieczka)
        {
            CurrentWycieczka = wycieczka;
        }

        public bool CzyCurrentWycieczkaPotwierdzona()
        {
            return CurrentWycieczka.CzyPotwierdzona();
        }

        public void UsunAktualnaWycieczke()
        {
            wycieczkiContext.Usun(CurrentWycieczka.Id);
            WycieczkiObservableCollection.Remove(CurrentWycieczka);
        }
    }
}
