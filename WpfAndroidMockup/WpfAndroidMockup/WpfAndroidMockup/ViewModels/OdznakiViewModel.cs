using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOT_PTTK.Models;

namespace WpfAndroidMockup.ViewModels
{
    /// <summary>
    /// ViewModel dla odznaki
    /// </summary>
    public class OdznakiViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Lista modeli odznaki
        /// </summary>
        public ObservableCollection<OdznakaModel> OdznakiObservableCollection { get; set; }

        private const string AKTUALNA_ODZNAKA_PROPERTY = "AktualnaOdznaka";
        private const string AKTUALNA_WYCIECZKA_PROPERTY = "AktualnaWycieczka";

        private OdznakiContext odznakiContext;
        private OdznakaModel aktualnaOdznaka;
        private WycieczkaModel aktualnaWycieczka;

        #region Properties

        /// <summary>
        /// Akcesor i mutator aktualnej odznaki
        /// </summary>
        public OdznakaModel AktualnaOdznaka
        {
            get
            {
                return aktualnaOdznaka;
            }
            set
            {
                if (aktualnaOdznaka != value)
                {
                    aktualnaOdznaka = value;
                    RaisePropertyChanged(AKTUALNA_ODZNAKA_PROPERTY);
                }
            }
        }

        /// <summary>
        /// Akcesor i mutato aktualnej wycieczki
        /// </summary>
        public WycieczkaModel AktualnaWycieczka
        {
            get
            {
                return aktualnaWycieczka;
            }
            set
            {
                if (aktualnaWycieczka != value)
                {
                    aktualnaWycieczka = value;
                    RaisePropertyChanged(AKTUALNA_WYCIECZKA_PROPERTY);
                }
            }
        }

        #endregion

        /// <summary>
        /// Konstruktor nieparametryczny klasy odznaka view model
        /// </summary>
        public OdznakiViewModel()
        {
            odznakiContext = OdznakiContext.GetInstance();
            OdznakiObservableCollection = new ObservableCollection<OdznakaModel>();
        }

        /// <summary>
        /// Ładuje wszystkie rozpoczęte cykle odznaki, nieprzyznane
        /// </summary>
        public void LoadWszystkieRozpoczeteCykle()
        {
            foreach (OdznakaModel item in odznakiContext.GetOdznakiNieDoWeryfikacji())
                OdznakiObservableCollection.Add(item);
            if(OdznakiObservableCollection.Count > 0)
                aktualnaOdznaka = OdznakiObservableCollection[0];

        }

        /// <summary>
        /// Wczytuje do aktualnej odznaki wybraną odznakę
        /// </summary>
        /// <param name="odznaka"></param>
        public void WczytajOdznake(OdznakaModel odznaka)
        {
            AktualnaOdznaka = odznaka;
        }

        /// <summary>
        /// Wczytuje do aktualnej wycieczki wybraną wycieczkę
        /// </summary>
        /// <param name="wycieczka"></param>
        public void WczytajWycieczke(WycieczkaModel wycieczka)
        {
            AktualnaWycieczka = wycieczka;
        }

        /// <summary>
        /// przesyła odznakę do weryfikacji
        /// </summary>
        public void WyslijAktualnaOdznakeDoWeryfikacji()
        {
            odznakiContext.ZmienStatus(aktualnaOdznaka.Id, StatusOdznaki.DOWERYFIKACJI);
            aktualnaOdznaka.NrPracownika = OdznakaModel.NR_PRACOWNIKA_DO_WERYFIKACJI;
        }

        /// <summary>
        ///Wydarzenie potrzebne do reakcji na zmianę atrybutu
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Reaguje na zmiane atrybutu
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

