using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// typ enumaracyjny statusów potwierdzenia dla wycieczki
    /// </summary>
    public enum StatusyPotwierdzenia { POTWIERDZONA, NIEPOTWIERDZONA, WTRAKCIE };

    /// <summary>
    /// Model Wycieczki
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class WycieczkaModel : INotifyPropertyChanged
    {
        private long id;
        private long nrPrzodownika;
        private TurystaModel turysta;
        private OdznakaModel odznaka;
        private string nazwa;
        private DateTime dataRozpoczecia;
        private DateTime dataZakonczenia;
        private StatusyPotwierdzenia status;
        private string obszarGorski;
        private bool czyWielodniowa;
        private string trasa;
        private long dlugosc; //w metrach
        private long wysokosc; //w metrach
        private int punktacja;
        private string cyklOdznaki;
        private string punktPoczatkowy;

        /// <summary>
        /// Akcesor i mutator identyfikatora wycieczki
        /// </summary>
        /// <value>
        /// Identyfikator wycieczki
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
                    RaisePropertyChanged("Id");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator nr przodownika, potwierdzajacego wycieczkę
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
                    RaisePropertyChanged("NrPrzodownika");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator tursty, który przebył wycieczke
        /// </summary>
        /// <value>
        /// turysta.
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
                    RaisePropertyChanged("Turysta");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator odznaki, w ramach której wycieczka została przebyta
        /// </summary>
        /// <value>
        /// odznaka.
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
                    RaisePropertyChanged("Odznaka");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator nazwy nadanej wycieczce przez turyste
        /// </summary>
        /// <value>
        /// Nazwa wycieczki.
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
                    RaisePropertyChanged("Imie");
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator daty rozpoczęcia wycieczki.
        /// </summary>
        /// <value>
        /// data rozpoczęcia wycieczki.
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
                    RaisePropertyChanged("DataRozpoczecia");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator  daty zakończenia wycieczki
        /// </summary>
        /// <value>
        /// data zakonczenia.
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
                    RaisePropertyChanged("DataZakonczenia");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator  statusu potwierdzenia wycieczki
        /// </summary>
        /// <value>
        /// Status.
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
                    RaisePropertyChanged("Status");
                    odznaka.RaisePropertyChanged("Pkt");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator obszaru górskiego, na którym odbyła się wycieczka
        /// </summary>
        /// <value>
        /// obszar gorski.
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
                    RaisePropertyChanged("ObszarGorski");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator wartości określającej czy wycieczka jest wycieczką wielodniową
        /// </summary>
        /// <value>
        ///   <c>true</c> wielodniowa; w przeciwnym przypadku, <c>false</c>.
        /// </value>
        public bool CzyWielodniowa
        {
            get
            {
                return czyWielodniowa;
            }
            set
            {
                if (czyWielodniowa != value)
                {
                    czyWielodniowa = value;
                    RaisePropertyChanged("CzyWielodniowa");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator trasy wycieczki
        /// </summary>
        /// <value>
        /// trasa.
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
                    RaisePropertyChanged("Trasa");
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator długości trasy wycieczki
        /// </summary>
        /// <value>
        /// dlugość.
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
                    RaisePropertyChanged("Dlugosc");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator wysokości n.p.m. wycieczki
        /// </summary>
        /// <value>
        /// wysokość.
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
                    RaisePropertyChanged("Wysokosc");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator punktacji wycieczki
        /// </summary>
        /// <value>
        /// punktacja.
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
                    RaisePropertyChanged("Punktacja");
                }
            }
        }

        /// <summary>
        ///  Akcesor i mutator nazwy cyklu odznaki wycieczki
        /// </summary>
        /// <value>
        /// cykl odznaki.
        /// </value>
        public string CyklOdznaki
        {
            get
            {
                return cyklOdznaki;
            }
            set
            {
                if (cyklOdznaki != value)
                {
                    cyklOdznaki = value;
                    RaisePropertyChanged("CyklOdznaki");
                }
            }
        }

        /// <summary>
        /// Akcesor całkowitgo czasu trwania wycieczki
        /// </summary>
        /// <value>
        /// calkowity czas trwania.
        /// </value>
        public string CalkowityCzasTrwania
        {
            get
            {
                return CalkowityCzasWycieczki();
            }
        }


        /// <summary>
        /// Akcesor daty wycieczki skonwerowanej do odpowiedniego formatu
        /// </summary>
        /// <value>
        /// data wycieczki.
        /// </value>
        public string DataWycieczki
        {
            get
            {
                return DataRozpoczecia.ToString("dd.MM.yyyy");
            }
        }

        /// <summary>
        /// Akcesor danych osobowych turysty.
        /// </summary>
        /// <value>
        /// dane turysty.
        /// </value>
        public string DaneTurysty
        {
            get
            {
                return turysta.Imie + " " + turysta.Nazwisko;
            }
        }

        /// <summary>
        ///  Akcesor i mutator punktu początkowego
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
                    RaisePropertyChanged("PunktPoczatkowy");
                }
            }
        }

        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="WycieczkaModel"/>.
        /// </summary>
        /// <param name="id">identyfikator.</param>
        /// <param name="turysta">turysta.</param>
        /// <param name="odznaka">odznaka.</param>
        /// <param name="Nazwa">nazwa.</param>
        /// <param name="status">status.</param>
        /// <param name="obszarGorski">obszar gorski.</param>
        /// <param name="pktPoczatkowy">PKT poczatkowy.</param>
        /// <param name="trasaWyc">trasa wycieczki.</param>
        public WycieczkaModel(long id, ref TurystaModel turysta, ref OdznakaModel odznaka, string Nazwa, StatusyPotwierdzenia status, string obszarGorski, string pktPoczatkowy, string trasaWyc)
        {
            this.id = id;
            this.Turysta = turysta;
            this.Odznaka = odznaka;
            this.Nazwa = Nazwa;
            this.DataRozpoczecia = DateTime.Now;
            this.DataZakonczenia = DateTime.Now.AddHours(13);
            this.DataZakonczenia = DataZakonczenia.AddMinutes(32);
            this.Status = status;
            this.ObszarGorski = obszarGorski;
            this.PunktPoczatkowy = pktPoczatkowy;
            CzyWielodniowa = false;
            this.Trasa = trasaWyc;
            Dlugosc = 3457;
            Wysokosc = 342;
            Punktacja = 30 + new Random(Guid.NewGuid().GetHashCode()).Next(10);
            CyklOdznaki = Odznaka.Rodzaj;
            odznaka.DodajWycieczke(this);
        }

        /// <summary>
        /// Wydarzenie- Aktywuje sie gdy wartosc atrybutu sie zmieni.
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

        /// <summary>
        /// Oblicza całkowity czas trwania wycieczki i konwertuje go do odpowieddniego formatu
        /// </summary>
        /// <returns>napis z czasem</returns>
        private string CalkowityCzasWycieczki()
        {
            int hours = (int)(DataZakonczenia - DataRozpoczecia).TotalHours;
            int minutes = (int)(DataZakonczenia - DataRozpoczecia).TotalMinutes - hours * 60;
            string time = hours + " h " + minutes + " min ";
            Console.WriteLine(time);
            return time;
        }


        /// <summary>
        /// Zwraca czy wycieczka jest potwierdzona.
        /// </summary>
        /// <returns>true - potwierdzona</returns>
        public bool CzyPotwierdzona()
        {
            return Status.Equals(StatusyPotwierdzenia.POTWIERDZONA);
        }

        /// <summary>
        /// Zwraca czy wycieczka jest  niepotwierdzona
        /// </summary>
        /// <returns>true- niepotwierdzona</returns>
        public bool CzyNiepotwierdzona()
        {
            return Status.Equals(StatusyPotwierdzenia.NIEPOTWIERDZONA);
        }

    }
}
