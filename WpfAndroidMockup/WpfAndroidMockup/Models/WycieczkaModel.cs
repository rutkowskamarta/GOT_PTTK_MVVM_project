using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfAndroidMockup.Models
{
    public enum StatusyPotwierdzenia { POTWIERDZONA, NIEPOTWIERDZONA, WTRAKCIE };

    public class WycieczkaModel : INotifyPropertyChanged
    {
        private TurystaModel turysta;
        private string name;
        private DateTime startDate;
        private DateTime finishDate;
        private StatusyPotwierdzenia status;
        private string obszarGorski;
        private bool czyWielodniowa;
        private string trasa;
        private long length; //w metrach
        private long height; //w metrach
        private int punktacja;
        private string cyklOdznaki;
        

        public TurystaModel Turysta
        {
            get
            {
                return turysta;
            }
            set
            {
                if (turysta != value)
                {
                    turysta = value;
                    RaisePropertyChanged("Turysta");
                }
            }

            // czy tu ten set jest potrzebny??? nie wiem
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    RaisePropertyChanged("StartDate");
                }
            }
        }

        public DateTime FinishDate
        {
            get
            {
                return finishDate;
            }
            set
            {
                if (finishDate != value)
                {
                    finishDate = value;
                    RaisePropertyChanged("FinishDate");
                }
            }
        }

        public StatusyPotwierdzenia Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        public string ObszarGorski
        {
            get
            {
                return obszarGorski;
            }
            set
            {
                if (obszarGorski != value)
                {
                    obszarGorski = value;
                    RaisePropertyChanged("ObszarGorski");
                }
            }
        }
        public bool CzyWielodniowa
        {
            get
            {
                return czyWielodniowa;
            }
            set
            {
                if (czyWielodniowa != value)
                {
                    czyWielodniowa = value;
                    RaisePropertyChanged("CzyWielodniowa");
                }
            }
        }
        public string Trasa
        {
            get
            {
                return trasa;
            }
            set
            {
                if (trasa != value)
                {
                    trasa = value;
                    RaisePropertyChanged("Trasa");
                }
            }
        }
        public long Length
        {
            get
            {
                return length;
            }
            set
            {
                if (length != value)
                {
                    length = value;
                    RaisePropertyChanged("Length");
                }
            }
        }
        public long Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    RaisePropertyChanged("Height");
                }
            }
        }
        public int Punktacja
        {
            get
            {
                return punktacja;
            }
            set
            {
                if (punktacja != value)
                {
                    punktacja = value;
                    RaisePropertyChanged("Punkctacja");
                }
            }
        }
        public string CyklOdznaki
        {
            get
            {
                return cyklOdznaki;
            }
            set
            {
                if (cyklOdznaki != value)
                {
                    cyklOdznaki = value;
                    RaisePropertyChanged("CyklOdznaki");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string TotalTime
        {
            get
            {
                return CalculateTotalTime();
            }
        }

        public string DataWycieczki
        {
            get
            {
                return StartDate.ToString("dd.MM.yyyy");
            }
        }

        public WycieczkaModel(TurystaModel Turysta, string Name, StatusyPotwierdzenia Status)
        {
            this.Turysta = Turysta;
            this.Name = Name;
            StartDate = DateTime.Now;
            FinishDate = DateTime.Now.AddHours(13);
            FinishDate = FinishDate.AddMinutes(32);
            this.Status = Status;
            obszarGorski = "Bieszczady";
            CzyWielodniowa = false;
            Trasa = "jakas trasa";
            Length = 3457; 
            Height = 342;
            Punktacja = 3;
            CyklOdznaki = "Popularna";

    }

        public void Copy(WycieczkaModel wycieczka)
        {
            Turysta = wycieczka.Turysta;
            Name = wycieczka.Name;
            StartDate = wycieczka.StartDate;
            FinishDate = wycieczka.FinishDate;
            Status = wycieczka.Status;
            ObszarGorski = wycieczka.ObszarGorski;
            CzyWielodniowa = wycieczka.CzyWielodniowa;
            Trasa = wycieczka.Trasa;
            Length = wycieczka.Length;
            Height = wycieczka.Height;
            Punktacja = wycieczka.Punktacja;
            CyklOdznaki = wycieczka.CyklOdznaki;

        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string CalculateTotalTime()
        {
            int hours = (int)(FinishDate - StartDate).TotalHours;
            int minutes = (int)(FinishDate - StartDate).TotalMinutes - hours * 60;
            string time = hours + " h " + minutes+" min ";
            Console.WriteLine(time);
            return time;
        }

    }
}
