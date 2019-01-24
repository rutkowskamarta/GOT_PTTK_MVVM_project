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
    /// <summary>
    /// View model dla wycieczek
    /// </summary>
    public class WycieczkaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WycieczkaModel> wycieczkiObservableCollection;
        private WycieczkaModel currentWycieczka;
        private WycieczkiContext wycieczkiContext;
        private PrzodownicyContext przodownicyContext;
                
        /// <summary>
        /// Mutatior i akcesor listy wycieczek
        /// </summary>
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
        
        /// <summary>
        /// Mutator i akcesor dla aktualnie obsługiwanej wycieczki
        /// </summary>
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

        /// <summary>
        /// Wydarzenie ragujące na zmianę wartości atrybutu
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Przetrzymuje obecny widok
        /// </summary>
        public UserControl CurrentView;

        /// <summary>
        /// Konstruktor nieparametryczny klasy <see cref="WycieczkaViewModel"/>
        /// </summary>
        public WycieczkaViewModel()
        {
            wycieczkiContext = WycieczkiContext.GetInstance();
            przodownicyContext = PrzodownicyContext.GetInstance();

            TurystaModel t = new TurystaModel();
            OdznakaModel o = new OdznakaModel(ref t);
            CurrentWycieczka = new WycieczkaModel(1, ref t, ref o, "", StatusyPotwierdzenia.NIEPOTWIERDZONA, "", "", "");

        }

        /// <summary>
        /// Reaguje na zmianę wartości atrybutu
        /// </summary>
        /// <param name="property">atrybut</param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        /// <summary>
        /// Przypisuje do listy wycieczek wszystkie wycieczki zalogowanego turysty
        /// </summary>
        public void LoadAllWycieczkiToObservableCollection()
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetWycieczkiZalogowanegoTurysty();

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Przypisuje do listy wycieczek wszystkie niepotwierdzone wycieczki turysty
        /// </summary>
        public void WczytajNiepotwierdzoneWycieczkiTurysty()
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetNiepotwierdzoneWycieczkiTurysty(DaneLogowania.IdZalogowanegoTurysty);

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Przypisuje do listy wycieczek wycieczki przodwnika, które czekają na potwierdzenie
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        public void WczytajWycieczkiPrzodownikaDoPotwierdzenia(long nrPrzodownika)
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetWycieczkiPrzodownikaDoPotwierdzenia(DaneLogowania.NrZalogowanegoPrzodownika);

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Przypisuje aktualnej wycieczce odpowiednią wycieczkę
        /// </summary>
        /// <param name="wycieczka"></param>
        public void WczytajWycieczke(WycieczkaModel wycieczka)
        {
            CurrentWycieczka = wycieczka;
        }

        /// <summary>
        /// Sprawcza czy aktualna wycieczka jest potwierdzona
        /// </summary>
        /// <returns></returns>
        public bool CzyCurrentWycieczkaPotwierdzona()
        {
            return CurrentWycieczka.CzyPotwierdzona();
        }

        /// <summary>
        /// Usuwa aktualną wycieczkę z bazy danych i z listy wycieczek
        /// </summary>
        public void UsunAktualnaWycieczke()
        {
            wycieczkiContext.Usun(CurrentWycieczka.Id);
            WycieczkiObservableCollection.Remove(CurrentWycieczka);
        }

        /// <summary>
        /// Sprawdza czy przodownik o podanym numerze istnieje w bazie
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        /// <returns></returns>
        public bool CzyPrzodownikONumerzeIstnieje(long nrPrzodownika)
        {
            return przodownicyContext.Exists(nrPrzodownika);
        }

        /// <summary>
        /// Wysyła przodownikowi wycieczkę do potwierdzenia
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        public void WyslijWycieczkeDoPotwierdzenia(long nrPrzodownika)
        {
            wycieczkiContext.ZmienStatus(currentWycieczka.Id, nrPrzodownika, StatusyPotwierdzenia.WTRAKCIE);
        }

        /// <summary>
        /// usuwa aktualną wycieczkę z wyświetlania jej w list box
        /// </summary>
        public void UsunObecnaWycieczkeZWyswietlania()
        {
            wycieczkiObservableCollection.Remove(CurrentWycieczka);
        }

        /// <summary>
        /// Zmienia w bazie status aktualnej wycieczki na potwierdzoną przez przodownika
        /// </summary>
        public void PotwierdzAktualnaWycieczke()
        {
            wycieczkiContext.ZmienStatus(currentWycieczka.Id, DaneLogowania.NrZalogowanegoPrzodownika, StatusyPotwierdzenia.POTWIERDZONA);
            
        }

        /// <summary>
        /// Zmienia w bazie status aktualnej wycieczki na niepotwierdzoną przez przodownika
        /// </summary>
        public void OdrzucAktualnaWycieczke()
        {
            wycieczkiContext.ZmienStatus(currentWycieczka.Id, DaneLogowania.NrZalogowanegoPrzodownika, StatusyPotwierdzenia.NIEPOTWIERDZONA);
        }

        /// <summary>
        /// Sprawdza czy zalogowany przodownika posiada uprawnienia odnośnie obszarów górskich wczytanej wycieczki
        /// </summary>
        /// <returns>true- posiada uprawniania</returns>
        public bool CzyZalogowanyPrzodownikPosiadaUprawnieniaNaCurrentWycieczke()
        {
            return przodownicyContext.CzyPosiadaUprawnienia(DaneLogowania.NrZalogowanegoPrzodownika, CurrentWycieczka.ObszarGorski);
        }

    }
}
