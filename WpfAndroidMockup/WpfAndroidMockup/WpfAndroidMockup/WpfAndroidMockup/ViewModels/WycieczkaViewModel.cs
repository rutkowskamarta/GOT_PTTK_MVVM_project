using System.Collections.Generic;
using GOT_PTTK.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using GOT_PTTK.Utilities;

namespace WpfAndroidMockup.ViewModels
{
    /// <summary>
    /// View model dla wycieczek
    /// </summary>
    public class WycieczkaViewModel : INotifyPropertyChanged
    {
        private const string WYCIECZKI_PROPERTY = "WycieczkiObservableCollection";
        private const string CURRENT_WYCIECZKA_PROPERTY = "CurrentWycieczka";

        private ObservableCollection<WycieczkaModel> wycieczkiObservableCollection;
        private WycieczkaModel currentWycieczka;
        private WycieczkiContext wycieczkiContext;
        private PrzodownicyContext przodownicyContext;

        #region Properties

        /// <summary>
        /// Lista wycieczek
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
                    RaisePropertyChanged(WYCIECZKI_PROPERTY);
                }

            }

        }

        /// <summary>
        /// Aktualna wycieczka
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
                    RaisePropertyChanged(CURRENT_WYCIECZKA_PROPERTY);
                }

            }
        }

        #endregion

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

            CurrentWycieczka = wycieczkiContext.GetModel(0);
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
            wycieczki.Sort((e1, e2) => e2.DataRozpoczecia.CompareTo(e1.DataRozpoczecia));

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
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetNiepotwierdzoneWycieczkiTurysty(Utils.ID_ZALOGOWANEGO_TURYSTY);

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Przypisuje do listy wycieczek wycieczki porzodwnika, które czekają na potwierdzenie
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        public void WczytajWycieczkiPrzodownikaDoPotwierdzenia(long nrPrzodownika)
        {
            WycieczkiObservableCollection = new ObservableCollection<WycieczkaModel>();
            List<WycieczkaModel> wycieczki = wycieczkiContext.GetWycieczkiPrzodownikaDoPotwierdzenia(Utils.ID_ZALOGOWANEGO_PRZODOWNIKA);

            foreach (var item in wycieczki)
            {
                WycieczkiObservableCollection.Add(item);
            }
        }

        /// <summary>
        /// Przypisuje aktualnej wycieczke odpowiednią wycieczkę
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
            return CurrentWycieczka.Status == StatusyPotwierdzenia.POTWIERDZONA;
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
        /// Wysyła przodonikowi wycieczkę do potwierdzenia
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        public void WyslijWycieczkeDoPotwierdzenia(long nrPrzodownika)
        {
            currentWycieczka.Status = StatusyPotwierdzenia.WTRAKCIE;
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
            currentWycieczka.Status = StatusyPotwierdzenia.POTWIERDZONA;
            wycieczkiContext.ZmienStatus(currentWycieczka.Id, Utils.ID_ZALOGOWANEGO_PRZODOWNIKA, StatusyPotwierdzenia.POTWIERDZONA);
        }

        /// <summary>
        /// Zmienia w bazie status aktualnej wycieczki na niepotwierdzoną przez przodownika
        /// </summary>
        public void OdrzucAktualnaWycieczke()
        {
            currentWycieczka.Status = StatusyPotwierdzenia.NIEPOTWIERDZONA;
            wycieczkiContext.ZmienStatus(currentWycieczka.Id, Utils.ID_ZALOGOWANEGO_PRZODOWNIKA, StatusyPotwierdzenia.NIEPOTWIERDZONA);
        }

        /// <summary>
        /// Sprawdza czy zalogowany przodownika posiada uprawnienia odnośnie obszarów górskich wczytanej wycieczki
        /// </summary>
        /// <returns>true- posiada uprawniania</returns>
        public bool CzyZalogowanyPrzodownikPosiadaUprawnieniaNaCurrentWycieczke()
        {
            return przodownicyContext.CzyPosiadaUprawnienia(Utils.ID_ZALOGOWANEGO_PRZODOWNIKA, CurrentWycieczka.ObszarGorski);
        }

    }
}
