using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Model turysty
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IComparable{WpfAndroidMockup.Models.TurystaModel}" />
    public class TurystaModel : INotifyPropertyChanged, IComparable<TurystaModel>
    {
        private long id;
        private string imie;
        private string nazwisko;

        /// <summary>
        /// Akcesor i mutator identyfikatora turysty.
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
                    RaisePropertyChanged("Id");
                }
            }
        }


        /// <summary>
        ///  Akcesor i mutator imienia turysty.
        /// </summary>
        /// <value>
        /// Imie turysty
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
        ///  Akcesor i mutator nazwiska turysty.
        /// </summary>
        /// <value>
        /// Nazwisko turysty
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
        /// Aktywuje sie gdy wartosc atrybutu sie zmieni.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Konstruktor nieparametryczny klasy <see cref="TurystaModel"/>.
        /// </summary>
        public TurystaModel()
        {

        }

        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="TurystaModel"/>.
        /// </summary>
        /// <param name="id">Identyfikator</param>
        public TurystaModel(long id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Konstruktor parametryczny klasy <see cref="TurystaModel"/>.
        /// </summary>
        /// <param name="id">identyfikator.</param>
        /// <param name="imie">imie.</param>
        /// <param name="nazwisko">nazwisko.</param>
        public TurystaModel(long id, string imie, string nazwisko)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
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
        public int CompareTo(TurystaModel other)
        {
            return id.CompareTo(other.Id);
        }

    }
}