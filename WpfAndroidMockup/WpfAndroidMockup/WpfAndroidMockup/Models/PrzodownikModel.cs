using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Model Przodownika
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IComparable{GOT_PTTK.Models.TurystaModel}" />
    class PrzodownikModel : INotifyPropertyChanged, IComparable<PrzodownikModel>
    {
        private const string NR_PRZODOWNIKA_PROPERTY = "NrPrzodownika";
        private const string IMIE_PROPERTY = "Imie";
        private const string NAZWISKO_PROPERTY = "Nazwisko";
        private const string OBSZARY_UPRAWNIEN_PROPERTY = "ObszaryUprawnien";

        private long nrPrzodownika;
        private string imie;
        private string nazwisko;
        private List<string> obszaryUprawnien;

        #region Properties

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
                    RaisePropertyChanged(NR_PRZODOWNIKA_PROPERTY);
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
                    RaisePropertyChanged(IMIE_PROPERTY);
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
                    RaisePropertyChanged(NAZWISKO_PROPERTY);
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
                    RaisePropertyChanged(OBSZARY_UPRAWNIEN_PROPERTY);
                }

            }
        }

        #endregion

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
        /// Porownuje instancje dwóch modeli po identyfikatorze i zwraca integer ktory identyfikuje wynik porownania
        /// </summary>
        /// <param name="other">Model porownywany</param>
        /// <returns>
        /// identyfikator porownania
        /// </returns>
        public int CompareTo(PrzodownikModel other)
        {
            return nrPrzodownika.CompareTo(other.nrPrzodownika);
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
