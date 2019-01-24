using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Model Przodownika
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IComparable{WpfAndroidMockup.Models.TurystaModel}" />
    class PrzodownikModel : INotifyPropertyChanged, IComparable<PrzodownikModel>
    {
        private long nrPrzodownika;
        private string imie;
        private string nazwisko;
        private List<string> obszaryUprawnien;

        /// <summary>
        /// Akcesor i mutator nr przodownika.
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
        ///  Akcesor i mutator imienia przodownika.
        /// </summary>
        /// <value>
        /// Imie przodownika
        /// </value>
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

        /// <summary>
        ///  Akcesor i mutator nazwiska przodownika.
        /// </summary>
        /// <value>
        /// Nazwisko przodownika
        /// </value>
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

        /// <summary>
        ///  Akcesor i mutator obszarów uprawnien.
        /// </summary>
        /// <value>
        /// Lista obszarow uprawnien przodownika.
        /// </value>
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


        /// <summary>
        /// Aktywuje sie gdy wartosc atrybutu sie zmieni.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Kostryktor klasy <see cref="PrzodownikModel"/>.
        /// </summary>
        public PrzodownikModel()
        {
            ObszaryUprawnien = new List<string>();
        }

        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="PrzodownikModel"/>.
        /// </summary>
        /// <param name="id">Identyfikator</param>
        public PrzodownikModel(long id)
        {
            this.NrPrzodownika = id;
            ObszaryUprawnien = new List<string>();
        }

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
        /// Porownuje instancje dwóch obiektów i zwraca integer ktory identyfikuje wynik porownania
        /// </summary>
        /// <param name="other">Obiekt porownywany</param>
        /// <returns>
        /// identyfikator porownania
        /// </returns>
        public int CompareTo(PrzodownikModel other)
        {
            return nrPrzodownika.CompareTo(other.nrPrzodownika);
        }

    }
}
