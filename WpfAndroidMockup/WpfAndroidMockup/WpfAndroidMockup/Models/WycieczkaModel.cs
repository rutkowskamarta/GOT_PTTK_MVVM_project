using System;
using System.ComponentModel;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// typ enumaracyjny statusów potwierdzenia dla wycieczki
    /// </summary>
    public enum StatusyPotwierdzenia { POTWIERDZONA, NIEPOTWIERDZONA, WTRAKCIE };

    /// <summary>
    /// typ enumaracyjny statusów weryfikacji wycieczki przez pracownika w trakcie procesu przyznawania odznaki
    /// </summary>
    public enum StatusWeryfikacjiWycieczki { NIEZWERYFIKOWANA, ZAAKCEPTOWANA, ODRZUCONA };

    /// <summary>
    /// Model Wycieczki
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class WycieczkaModel : INotifyPropertyChanged, IComparable<WycieczkaModel>
    {
        private const string ID_PROPERTY = "Id";
        private const string NR_PRZODOWNIKA_PROPERTY = "NrPrzodownika";
        private const string TURYSTA_PROPERTY = "Turysta";
        private const string ODZNAKA_PROPERTY = "Odznaka";
        private const string NAZWA_PROPERTY = "Nazwa";
        private const string DATA_ROZPOCZECIA_PROPERTY = "DataRozpoczecia";
        private const string DATA_ZAKONCZENIA_PROPERTY = "DataZakonczenia";
        private const string PUNKT_POCZATKOWY_PROPERTY = "PunktPoczatkowy";
        private const string STATUS_POTWIERDZENIA_PROPERTY = "Status";
        private const string STATUS_WERYFIKACJI_PROPERTY = "StatusWeryfikacji";
        private const string OBSZAR_GORSKI_PROPERTY = "ObszarGorski";
        private const string TRASA_PROPERTY = "Trasa";
        private const string DLUGOSC_PROPERTY = "Dlugosc";
        private const string WYSOKOSC_PROPERTY = "Wysokosc";
        private const string PUNKTACJA_PROPERTY = "Punktacja";
        private const string CALKOWITY_CZAS_WYCIECZKI_STRING_FORMAT = "{0} h {1} m";

        private long id;
        private long nrPrzodownika;
        private TurystaModel turysta;
        private OdznakaModel odznaka;
        private string nazwa;
        private DateTime dataRozpoczecia;
        private DateTime dataZakonczenia;
        private StatusyPotwierdzenia status;
        private StatusWeryfikacjiWycieczki statusWeryfikacji;
        private string obszarGorski;
        private string trasa;
        private string punktPoczatkowy;
        private long dlugosc;
        private long wysokosc;
        private int punktacja;

        #region Properties

        /// <summary>
        /// Akcesor i mutator identyfikatora wycieczki.
        /// </summary>
        /// <value>
        /// identyfikator wycieczki.
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
        /// Akcesor i mutator nr przodownika, który potwierdza wycieczkę.
        /// </summary>
        /// <value>
        /// nr przodownika.
        /// </value>
        public long NrPrzodownika
        {
            get
            {
                return nrPrzodownika;
            }
            set
            {
                if (nrPrzodownika != value)
                {
                    nrPrzodownika = value;
                    RaisePropertyChanged(NR_PRZODOWNIKA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator modelu turysty, który odbyl wycieczke
        /// </summary>
        /// <value>
        /// model turysty.
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

        /// <summary>
        /// Akcesor i mutator modelu odznaki, w ramach ktorej odbyla sie wycieczka
        /// </summary>
        /// <value>
        /// model odznaki.
        /// </value>
        public OdznakaModel Odznaka
        {
            get
            {
                return odznaka;
            }
            set
            {
                if (odznaka != value)
                {
                    odznaka = value;
                    RaisePropertyChanged(ODZNAKA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator nazwy wycieczki.
        /// </summary>
        /// <value>
        /// nazwa wycieczki.
        /// </value>
        public string Nazwa
        {
            get
            {
                return nazwa;
            }
            set
            {
                if (nazwa != value)
                {
                    nazwa = value;
                    RaisePropertyChanged(NAZWA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator daty rozpoczecia wycieczki.
        /// </summary>
        /// <value>
        /// data rozpoczecia wycieczki.
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
        /// Akcesor i mutator daty zakonczenia wycieczki.
        /// </summary>
        /// <value>
        /// data zakonczenia wycieczki.
        /// </value>
        public DateTime DataZakonczenia
        {
            get
            {
                return dataZakonczenia;
            }
            set
            {
                if (dataZakonczenia != value)
                {
                    dataZakonczenia = value;
                    RaisePropertyChanged(DATA_ZAKONCZENIA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator punktu poczatkowego.
        /// </summary>
        /// <value>
        /// punkt poczatkowy.
        /// </value>
        public string PunktPoczatkowy
        {
            get
            {
                return punktPoczatkowy;
            }
            set
            {
                if (punktPoczatkowy != value)
                {
                    punktPoczatkowy = value;
                    RaisePropertyChanged(PUNKT_POCZATKOWY_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator statusu potwierdzenia wycieczki.
        /// </summary>
        /// <value>
        /// status potwierdzenia wycieczki.
        /// </value>
        public StatusyPotwierdzenia Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    odznaka.RaisePropertyChanged(OdznakaModel.PUNKTY_PROPERTY);
                    RaisePropertyChanged(STATUS_POTWIERDZENIA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator statusu weryfikacji wycieczki przez pracownika. 
        /// </summary>
        /// <value>
        /// status weryfikacji wycieczki.
        /// </value>
        public StatusWeryfikacjiWycieczki StatusWeryfikacji
        {
            get
            {
                return statusWeryfikacji;
            }
            set
            {
                if (statusWeryfikacji != value)
                {
                    statusWeryfikacji = value;
                    odznaka.RaisePropertyChanged(OdznakaModel.PUNKTY_PROPERTY);
                    RaisePropertyChanged(STATUS_WERYFIKACJI_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator obszaru gorskiego, na ktorym odbyla sie wycieczka
        /// </summary>
        /// <value>
        /// nazwa obszaru gorskiego.
        /// </value>
        public string ObszarGorski
        {
            get
            {
                return obszarGorski;
            }
            set
            {
                if (obszarGorski != value)
                {
                    obszarGorski = value;
                    RaisePropertyChanged(OBSZAR_GORSKI_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator nazwy trasy wycieczki.
        /// </summary>
        /// <value>
        /// nazwa trasy.
        /// </value>
        public string Trasa
        {
            get
            {
                return trasa;
            }
            set
            {
                if (trasa != value)
                {
                    trasa = value;
                    RaisePropertyChanged(TRASA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator dlugosci trasy w metrach.
        /// </summary>
        /// <value>
        /// dlugosc trasy w metrach
        /// </value>
        public long Dlugosc
        {
            get
            {
                return dlugosc;
            }
            set
            {
                if (dlugosc != value)
                {
                    dlugosc = value;
                    RaisePropertyChanged(DLUGOSC_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator przebytej wysokosci trasy w metrach.
        /// </summary>
        /// <value>
        /// przebyta wysokosc trasy w metrach
        /// </value>
        public long Wysokosc
        {
            get
            {
                return wysokosc;
            }
            set
            {
                if (wysokosc != value)
                {
                    wysokosc = value;
                    RaisePropertyChanged(WYSOKOSC_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator puntkacji wycieczki.
        /// </summary>
        /// <value>
        /// punktacja wycieczki
        /// </value>
        public int Punktacja
        {
            get
            {
                return punktacja;
            }
            set
            {
                if (punktacja != value)
                {
                    punktacja = value;
                    RaisePropertyChanged(PUNKTACJA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor calkowitego czasu trwania wycieczki.
        /// </summary>
        /// <value>
        /// calkowity czas trwania wycieczki w formacie "HH h MM m"
        /// </value>
        public string CalkowityCzasTrwania
        {
            get
            {
                return CalkowityCzasWycieczki();
            }
        }

        #endregion

        public WycieczkaModel(ref OdznakaModel odznaka)
        {
            Turysta = odznaka.Turysta;
            Odznaka = odznaka;
        }

        /// <summary>
        /// Funkcja zwracajaca w postaci stringa o formacie "HH h MM m" calkowity czas wycieczki
        /// </summary>
        /// <returns>calkowity czas wycieczki w godzinach i minutach</returns>
        private string CalkowityCzasWycieczki()
        {
            int hours = (int)(DataZakonczenia - DataRozpoczecia).TotalHours;
            int minutes = (int)(DataZakonczenia - DataRozpoczecia).TotalMinutes - hours * 60;
            string time = String.Format(CALKOWITY_CZAS_WYCIECZKI_STRING_FORMAT, hours, minutes);
            return time;
        }

        /// <summary>
        /// Porownuje instancje dwóch modeli po identyfikatorze i zwraca integer ktory identyfikuje wynik porownania
        /// </summary>
        /// <param name="other">Model porownywany</param>
        /// <returns>
        /// identyfikator porownania
        /// </returns>
        public int CompareTo(WycieczkaModel other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// Aktywuje sie gdy wartosc atrybutu sie zmieni.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Zgłasza zmiane atrybutu.
        /// </summary>
        /// <param name="property">atrybut</param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
