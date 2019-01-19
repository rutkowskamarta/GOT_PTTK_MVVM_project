using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    class PrzodownikModel : INotifyPropertyChanged, IComparable<TurystaModel>
    {
        private long nrPrzodownika;
        private string imie;
        private string nazwisko;
        private List<string> obszaryUprawnien;

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

        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                if (imie != value)
                {
                    imie = value;
                    RaisePropertyChanged("Imie");
                }
            }
        }

        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
            set
            {
                if (nazwisko != value)
                {
                    nazwisko = value;
                    RaisePropertyChanged("Nazwisko");
                }
            }
        }

        public List<string> ObszaryUprawnien
        {
            get
            {
                return obszaryUprawnien;
            }
            set
            {
                if (obszaryUprawnien != value)
                {
                    obszaryUprawnien = value;
                    RaisePropertyChanged("ObszaryUprawnien");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PrzodownikModel()
        {
            ObszaryUprawnien = new List<string>();
        }

        public PrzodownikModel(long id)
        {
            this.NrPrzodownika = id;
            ObszaryUprawnien = new List<string>();
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public int CompareTo(TurystaModel other)
        {
            return nrPrzodownika.CompareTo(other.Id);
        }

    }
}
