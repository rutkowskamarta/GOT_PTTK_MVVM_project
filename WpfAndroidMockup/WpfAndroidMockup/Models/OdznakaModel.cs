using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    public class OdznakaModel : INotifyPropertyChanged
    {
        private long id;
        private string stopien;
        private string rodzaj;
        private ObservableCollection<WycieczkaModel> wycieczki;
        private bool czyZweryfikowana;
        private string imgPath;
        private int minPkt;
        private int pkt;
        private DateTime dataRozpoczecia;
        private DateTime dataZakonczenia;
        private bool czyPrzeslanaDoWeryfikacji;

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
                    RaisePropertyChanged("Stopien");
                }
            }
        }

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
                    RaisePropertyChanged("Rodzaj");
                }
            }
        }

        public bool CzyZweryfikowana
        {
            get
            {
                return czyZweryfikowana;
            }
            set
            {
                if (czyZweryfikowana != value)
                {
                    czyZweryfikowana = value;
                    RaisePropertyChanged("CzyZweryfikowana");
                }
            }
        }

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
                    RaisePropertyChanged("ImgPath");
                }
            }
        }

        public int Pkt
        {
            get
            {
                return pkt;
            }
            set
            {
                if (pkt != value)
                {
                    pkt = value;
                    RaisePropertyChanged("Pkt");
                }
            }
        }

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
                    RaisePropertyChanged("MinPkt");
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

        public ObservableCollection<WycieczkaModel> Wycieczki
        {
            get
            {
                return wycieczki;
            }
        }

        public TurystaModel Turysta { get; }

        public OdznakaModel(ref TurystaModel turysta)
        {
            Turysta = turysta;
            wycieczki = new ObservableCollection<WycieczkaModel>();

        }

        public string StatusString
        {
            get
            {
                if (!CzyZweryfikowana)
                {
                    return "NIEZWERYFIKOWANA";
                }
                else
                {
                    return "ZWERYFIKOWANA";
                }
            }
            
        }

        public bool CzyPrzeslanaDoWeryfikacji
        {
            get
            {
                return czyPrzeslanaDoWeryfikacji;
            }
            set
            {
                if (czyPrzeslanaDoWeryfikacji != value)
                {
                    czyPrzeslanaDoWeryfikacji = value;
                    RaisePropertyChanged("CzyPrzeslanaDoWeryfikacji");
                }
            }
        }

        public void DodajWycieczke(WycieczkaModel wycieczka)
        {
            wycieczki.Add(wycieczka);
        }

        public void UsunWycieczke(WycieczkaModel wycieczka)
        {
            wycieczki.Remove(wycieczka);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private bool CzyWarunkiSpelnione()
        {
            return Pkt >= MinPkt;
        }

    }
}