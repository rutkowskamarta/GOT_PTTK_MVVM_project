using System;
using System.ComponentModel;

namespace GOT_PTTK.Models
{

    /// <summary>
    /// Model pracownika
    /// </summary>
    public class PracownikModel : INotifyPropertyChanged, IComparable<PracownikModel>
    {
        private const string NR_PRACOWNIKA_PROPERTY = "NumerPracownika";
        private const string IMIE_PROPERTY = "Imie";
        private const string NAZWISKO_PROPERTY = "Nazwisko";
        private const string ODZNAKI_ZWERYFIKOWANE_PROPERTY = "OdznakiZweryfikowane";
        private const string DATA_ZATRUDNIENIA_PROPERTY = "DataZatrudnienia";
        private const string MIASTO_TRW_PROPERTY = "MiastoTRW";
        private const string TYP_PRACOWNIKA_PROPERTY = "TRWczyCRW";
        private const string TYP_CRW_STRING = "CRW";
        private const string TYP_TRW_STRING = "TRW";

        private long numer;
        private string imie;
        private string nazwisko;
        private DateTime dataZatrudnienia;
        private int odznakiZweryfikowane;
        private string miastoTRW;

        #region Properties

        /// <summary>
        /// Akcesor i mutator dla numeru pracownika
        /// </summary>
        /// <value>
        ///   Numer pracownika
        /// </value>
        public long NumerPracownika
        {
            get
            {
                return numer;
            }
            set
            {
                if (numer != value)
                {
                    numer = value;
                    RaisePropertyChanged(NR_PRACOWNIKA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator dla imienia pracownika
        /// </summary>
        /// <value>
        ///   Imie pracownika
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
        /// Akcesor i mutator dla nazwiska Pracownika
        /// </summary>
        /// <value>
        ///   Nazwisko pracownika
        /// </value>
        public string Nazwisko
        {
            get { return nazwisko; }

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
        /// Akcesor i mutator dla liczby zweryfikowanych odznak
        /// </summary>
        /// <value>
        ///   Liczba zweryfikowanych przez pracownika odznak
        /// </value>
        public int OdznakiZweryfikowane
        {
            get
            {
                return odznakiZweryfikowane;
            }
            set
            {
                if (odznakiZweryfikowane != value)
                {
                    odznakiZweryfikowane = value;
                    RaisePropertyChanged(ODZNAKI_ZWERYFIKOWANE_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutator dla daty zatrudnienia
        /// </summary>
        /// <value>
        ///   Data zatrudnienia pracownika
        /// </value>
        public DateTime DataZatrudnienia
        {
            get
            {
                return dataZatrudnienia;
            }
            set
            {
                if (dataZatrudnienia != value)
                {
                    dataZatrudnienia = value;
                    RaisePropertyChanged(DATA_ZATRUDNIENIA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor zwracający typ pracownika
        /// </summary>
        /// <value>
        ///  "CRW" lub "TRW"
        /// </value>
        public string TRWczyCRW
        {
            get
            {
                return (miastoTRW == null || miastoTRW.Length == 0) ? TYP_CRW_STRING : TYP_TRW_STRING;
            }

        }

        /// <summary>
        /// Akcesor i mutator dla miasta TRW
        /// </summary>
        /// <value>
        ///   Jeżeli pracownik jest typu "TRW" to miasto, w przeciwnym razie zawiera pusty string
        /// </value>
        public string MiastoTRW
        {
            get
            {
                return miastoTRW;
            }
            set
            {
                if (miastoTRW != value)
                {
                    miastoTRW = value;
                    RaisePropertyChanged(MIASTO_TRW_PROPERTY);
                    RaisePropertyChanged(TYP_PRACOWNIKA_PROPERTY);
                }
            }
        }

        #endregion

        /// <summary>
        /// Komparator porównujący numery pracowników
        /// </summary>
        /// <param name="other">model pracownika do porównania</param>
        /// <returns></returns>
        public int CompareTo(PracownikModel other)
        {
            return NumerPracownika.CompareTo(other.NumerPracownika);
        }

        /// <summary>
        /// Zdarzenie, aktywuje sie gdy wartosc atrybutu sie zmieni.
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

    }
}
