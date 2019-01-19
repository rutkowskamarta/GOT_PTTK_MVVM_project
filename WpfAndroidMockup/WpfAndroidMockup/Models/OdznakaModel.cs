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
                int suma = 0;
                foreach (WycieczkaModel wycieczka in wycieczki)
                {
                    if (wycieczka.Status == StatusyPotwierdzenia.POTWIERDZONA)
                        suma += wycieczka.Punktacja;
                }
                return suma;
            }
            set
            {
                if (minPkt != value)
                {
                    minPkt = value;
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

        public ObservableCollection<WycieczkaModel> Wycieczki
        {
            get;
        }

        public TurystaModel Turysta { get; }

        public OdznakaModel(ref TurystaModel turysta)
        {
            Turysta = turysta;
            wycieczki = new ObservableCollection<WycieczkaModel>();

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