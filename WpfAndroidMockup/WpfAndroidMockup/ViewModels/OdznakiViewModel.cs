using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAndroidMockup.Models;

namespace WpfAndroidMockup.ViewModels
{
    public class OdznakiViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OdznakaModel> OdznakiObservableCollection { get; set; }
        
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
                    RaisePropertyChanged("AktualnaOdznaka");
                }
            }
        }
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
                    RaisePropertyChanged("AktualnaWycieczka");
                }
            }
        }

        private OdznakiContext odznakiContext;
        private OdznakaModel aktualnaOdznaka;
        private WycieczkaModel aktualnaWycieczka;

        public OdznakiViewModel()
        {
            odznakiContext = OdznakiContext.GetInstance();
            OdznakiObservableCollection = new ObservableCollection<OdznakaModel>();
        }

        public void LoadWszystkieRozpoczeteCykle()
        {
            foreach (OdznakaModel item in odznakiContext.GetOdznakiNiezaakcpetowane())
                OdznakiObservableCollection.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void WczytajOdznake(OdznakaModel odznaka)
        {
            AktualnaOdznaka = odznaka;
        }

        public void WczytajWycieczke(WycieczkaModel wycieczka)
        {
            AktualnaWycieczka = wycieczka;
        }

       public void PrzeslijAktualnaOdznakeDoWeryfikacji()
        {
            aktualnaOdznaka.CzyPrzeslanaDoWeryfikacji = true;
            odznakiContext.ZmienStatus(aktualnaOdznaka.Id, StatusOdznaki.DOWERYFIKACJI);
        }
    }
}

