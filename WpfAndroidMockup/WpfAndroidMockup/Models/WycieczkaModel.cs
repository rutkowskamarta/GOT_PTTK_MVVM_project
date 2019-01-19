using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfAndroidMockup.Models
{
    public enum StatusyPotwierdzenia { POTWIERDZONA, NIEPOTWIERDZONA, WTRAKCIE };

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


        public string CalkowityCzasTrwania
        {
            get
            {
                return CalkowityCzasWycieczki();
            }
        }

        public string DataWycieczki
        {
            get
            {
                return DataRozpoczecia.ToString("dd.MM.yyyy");
            }
        }

        public WycieczkaModel(long id, ref TurystaModel turysta, ref OdznakaModel odznaka, string Name, StatusyPotwierdzenia status)
        {
            this.id = id;
            this.Turysta = turysta;
            this.Odznaka = odznaka;
            this.Nazwa = Name;
            DataRozpoczecia = DateTime.Now;
            DataZakonczenia = DateTime.Now.AddHours(13);
            DataZakonczenia = DataZakonczenia.AddMinutes(32);
            this.Status = status;
            obszarGorski = "Bieszczady";
            CzyWielodniowa = false;
            Trasa = "jakas trasa";
            Dlugosc = 3457;
            Wysokosc = 342;
            Punktacja = 30 + new Random(Guid.NewGuid().GetHashCode()).Next(10);
            CyklOdznaki = Odznaka.Rodzaj;
            odznaka.DodajWycieczke(this);
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string CalkowityCzasWycieczki()
        {
            int hours = (int)(DataZakonczenia - DataRozpoczecia).TotalHours;
            int minutes = (int)(DataZakonczenia - DataRozpoczecia).TotalMinutes - hours * 60;
            string time = hours + " h " + minutes + " min ";
            Console.WriteLine(time);
            return time;
        }



        public bool CzyPotwierdzona()
        {
            return Status.Equals(StatusyPotwierdzenia.POTWIERDZONA);
        }

        public bool CzyNiepotwierdzona()
        {
            return Status.Equals(StatusyPotwierdzenia.NIEPOTWIERDZONA);
        }

    }
}
