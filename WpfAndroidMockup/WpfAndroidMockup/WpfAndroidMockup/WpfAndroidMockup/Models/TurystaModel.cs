using System;
using System.ComponentModel;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Model turysty
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IComparable{GOT_PTTK.Models.TurystaModel}" />
    public class TurystaModel : INotifyPropertyChanged, IComparable<TurystaModel>
    {
        private const string ID_PROPERTY = "Id";
        private const string IMIE_PROPERTY = "Imie";
        private const string NAZWISKO_PROPERTY = "Nazwisko";

        private long id;
        private string imie;
        private string nazwisko;

        #region Properties

        /// <summary>
        /// akcesor i mutator dla identyfikatora.
        /// </summary>
        /// <value>
        /// identyfikator turysty.
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
        /// akcesor i mutator dla imienia.
        /// </summary>
        /// <value>
        /// imie turysty.
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
        /// akcesor i mutator dla nazwiska.
        /// </summary>
        /// <value>
        /// nazwisko turysty.
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

        #endregion

        /// <summary>
        /// Porownuje instancje dwóch modeli po identyfikatorze i zwraca integer ktory identyfikuje wynik porownania
        /// </summary>
        /// <param name="other">Model porownywany</param>
        /// <returns>
        /// identyfikator porownania
        /// </returns>
        public int CompareTo(TurystaModel other)
        {
            return id.CompareTo(other.Id);
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