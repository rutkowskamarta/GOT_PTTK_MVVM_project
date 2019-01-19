using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    public class TurystaModel : INotifyPropertyChanged, IComparable<TurystaModel>
    {
        private long id;
        private string imie;
        private string nazwisko;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public TurystaModel()
        {

        }

        public TurystaModel(long id)
        {
            this.Id = id;
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
            return id.CompareTo(other.Id);
        }

    }
}