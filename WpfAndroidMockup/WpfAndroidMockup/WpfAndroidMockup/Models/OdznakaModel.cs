using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Model odznaki
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
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
                    RaisePropertyChanged("Id");
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
                    RaisePropertyChanged("Stopien");
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
                    RaisePropertyChanged("Rodzaj");
                }
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
                    RaisePropertyChanged("ImgPath");
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
                    RaisePropertyChanged("MinPkt");
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
                    RaisePropertyChanged("DataRozpoczecia");
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator daty zakończenia cyklu odznaki
        /// </summary>
        /// <value>
        /// Data zakonczenia cyklu odznaki
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
        /// Akcesor turysty, posiadacza odznaki.
        /// </summary>
        /// <value>
        /// Turysta, posiadach odznaki
        /// </value>
        public TurystaModel Turysta { get; }

        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="OdznakaModel"/>
        /// </summary>
        /// <param name="turysta"></param>
        public OdznakaModel(ref TurystaModel turysta)
        {
            Turysta = turysta;
            wycieczki = new ObservableCollection<WycieczkaModel>();

        }

        /// <summary>
        /// Akcesor i mutator statusu odznaki
        /// </summary>
        /// <value>
        /// status odznaki jako napis
        /// </value>
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

        /// <summary>
        /// ustala i odczytuje czy odznaka została przesłana do weryfikacji.
        /// </summary>
        /// <value>
        ///   <c>true</c> jeśli przeslana do weryfikacji; w przeciwnym przypadku, <c>false</c>.
        /// </value>
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
        /// Aktywuje sie gdy wartosc atrybutu sie zmieni.
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

        /// <summary>
        /// Sprawdza czy warunki są spełnione
        /// </summary>
        /// <returns>prawda- spelnione</returns>
        private bool CzyWarunkiSpelnione()
        {
            return Pkt >= MinPkt;
        }

    }
}