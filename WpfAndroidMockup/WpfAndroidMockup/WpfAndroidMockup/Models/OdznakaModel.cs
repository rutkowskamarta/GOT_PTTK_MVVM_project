using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Model odznaki
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class OdznakaModel : INotifyPropertyChanged, IComparable<OdznakaModel>
    {
        public const long NR_PRACOWNIKA_BEZ_WERYFIKACJI = -1;
        public const long NR_PRACOWNIKA_DO_WERYFIKACJI = -2;

        public const string PUNKTY_PROPERTY = "Pkt";

        private const string ID_PROPERTY = "Id";
        private const string STOPIEN_PROPERTY = "Stopien";
        private const string RODZAJ_PROPERTY = "Rodzaj";
        private const string IMG_PATH_PROPERTY = "ImgPath";
        private const string MIN_PUNKTY_PROPERTY = "MinPkt";
        private const string DATA_ROZPOCZECIA_PROPERTY = "DataRozpoczecia";
        private const string NR_PRACOWNIKA_PROPERTY = "NrPracownika";
        private const string CZY_WYSLANA_DO_WERYFIKACJI_PROPERTY = "CzyWyslanaDoWeryfikacji";
        private const string TURYSTA_PROPERTY = "Turysta";

        private long id;
        private TurystaModel turysta;
        private string stopien;
        private string rodzaj;
        private ObservableCollection<WycieczkaModel> wycieczki;
        private string imgPath;
        private int minPkt;
        private DateTime dataRozpoczecia;
        private long nrPracownika;

        #region Properties

        /// <summary>
        /// akcesor i mutator dla identyfikatora.
        /// </summary>
        /// <value>
        /// identyfikator odznaki.
        /// </value>
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged(ID_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator stopnia odznaki.
        /// </summary>
        /// <value>
        /// Stopien odznaki.
        /// </value>
        public string Stopien
        {
            get
            {
                return stopien;
            }
            set
            {
                if (stopien != value)
                {
                    stopien = value;
                    RaisePropertyChanged(STOPIEN_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator rodzaju.
        /// </summary>
        /// <value>
        /// Rodzaj odznaki.
        /// </value>
        public string Rodzaj
        {
            get
            {
                return rodzaj;
            }
            set
            {
                if (rodzaj != value)
                {
                    rodzaj = value;
                    RaisePropertyChanged(RODZAJ_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator wskazujący czy odznaka została wysłana do weryfikacji.
        /// </summary>
        /// <value>
        ///   <c>true</c> jeśli [wysłana do weryfikacji]; w przeciwnym wypadku, <c>false</c>.
        /// </value>
        public bool CzyWyslanaDoWeryfikacji
        {
            get
            {
                return NrPracownika == NR_PRACOWNIKA_DO_WERYFIKACJI;
            }
        }

        /// <summary>
        /// Akcesor i mutator wskazujący czy odznaka została zweryfikowana.
        /// </summary>
        /// <value>
        ///   <c>true</c> jeśli [zweryfikowana]; w przeciwnym wypadku, <c>false</c>.
        /// </value>
        public bool CzyZweryfikowana
        {
            get
            {
                int licznikNiezweryfikowanychWycieczek = 0;
                foreach (WycieczkaModel wycieczka in Wycieczki)
                {
                    if (wycieczka.StatusWeryfikacji == StatusWeryfikacjiWycieczki.NIEZWERYFIKOWANA)
                    {
                        licznikNiezweryfikowanychWycieczek++;
                    }
                }
                return licznikNiezweryfikowanychWycieczek == 0;
            }
        }

        /// <summary>
        /// Akcesor wskazujący czy odznaka została przyznana
        /// </summary>
        public bool CzyPrzyznana
        {
            get
            {
                return CzyZweryfikowana && (Pkt > MinPkt);
            }
        }

        /// <summary>
        /// Akcesor i mutator ścieżki do obrazku odznaki.
        /// </summary>
        /// <value>
        /// ścieżka do pliku.
        /// </value>
        public string ImgPath
        {
            get
            {
                return imgPath;
            }
            set
            {
                if (imgPath != value)
                {
                    imgPath = value;
                    RaisePropertyChanged(IMG_PATH_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator punktów zdobytych w ramach odznaki.
        /// </summary>
        /// <value>
        /// PKT.
        /// </value>
        public int Pkt
        {
            get
            {
                int suma = 0;
                foreach (WycieczkaModel wycieczka in wycieczki)
                {
                    if (wycieczka.Status == StatusyPotwierdzenia.POTWIERDZONA && wycieczka.StatusWeryfikacji != StatusWeryfikacjiWycieczki.ODRZUCONA)
                        suma += wycieczka.Punktacja;
                }
                return suma;
            }
            set
            {
                if (minPkt != value)
                {
                    minPkt = value;
                    RaisePropertyChanged(PUNKTY_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator liczby punktów potrzebnych do zdobycia odznaki
        /// </summary>
        /// <value>
        /// Punkty potrzebne do przyznania odznaki.
        /// </value>
        public int MinPkt
        {
            get
            {
                return minPkt;
            }
            set
            {
                if (minPkt != value)
                {
                    minPkt = value;
                    RaisePropertyChanged(MIN_PUNKTY_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator daty rozpoczecia cyklu odznaki
        /// </summary>
        /// <value>
        /// Data rozpoczecia cyklu odznaki
        /// </value>
        public DateTime DataRozpoczecia
        {
            get
            {
                return dataRozpoczecia;
            }
            set
            {
                if (dataRozpoczecia != value)
                {
                    dataRozpoczecia = value;
                    RaisePropertyChanged(DATA_ROZPOCZECIA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator numeru pracownika, powiązanego z przyznaniem zdobycia odznaki
        /// </summary>
        /// <value>
        /// Numer pracownika
        /// </value>
        public long NrPracownika
        {
            get
            {
                return nrPracownika;
            }
            set
            {
                if (nrPracownika != value)
                {
                    nrPracownika = value;
                    RaisePropertyChanged(NR_PRACOWNIKA_PROPERTY);
                    RaisePropertyChanged(CZY_WYSLANA_DO_WERYFIKACJI_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor wycieczek w ramach odznaki
        /// </summary>
        /// <value>
        /// Wycieczki w ramach odznaki.
        /// </value>
        public ObservableCollection<WycieczkaModel> Wycieczki
        {
            get
            {
                return wycieczki;
            }
        }

        /// <summary>
        /// Akcesor turysty
        /// </summary>
        /// <value>
        /// Turysta, odbywający wycieczki na odznakę
        /// </value>
        public TurystaModel Turysta
        {
            get
            {
                return turysta;
            }
            set
            {
                if (turysta != value)
                {
                    turysta = value;
                    RaisePropertyChanged(TURYSTA_PROPERTY);
                }
            }
        }

        #endregion

        public OdznakaModel()
        {
            wycieczki = new ObservableCollection<WycieczkaModel>();
        }

        /// <summary>
        /// Dodaje wycieczke.
        /// </summary>
        /// <param name="wycieczka">Wycieczka.</param>
        public void DodajWycieczke(WycieczkaModel wycieczka)
        {
            wycieczki.Add(wycieczka);
        }

        /// <summary>
        /// Usuwa wycieczke.
        /// </summary>
        /// <param name="wycieczka">wycieczka.</param>
        public void UsunWycieczke(WycieczkaModel wycieczka)
        {
            wycieczki.Remove(wycieczka);
        }

        /// <summary>
        /// Sprawdza czy warunki są spełnione
        /// </summary>
        /// <returns>prawda- spelnione</returns>
        private bool CzyWarunkiSpelnione()
        {
            return Pkt >= MinPkt;
        }

        /// <summary>
        /// Komparator porównujący Id odznak
        /// </summary>
        /// <param name="other">model odznaki do porównania</param>
        /// <returns></returns>
        public int CompareTo(OdznakaModel other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// Zdarzenie, aktywuje sie gdy wartosc atrybutu sie zmieni.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Zgłasza zmiane atrybutu.
        /// </summary>
        /// <param name="property">atrybut</param>
        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}