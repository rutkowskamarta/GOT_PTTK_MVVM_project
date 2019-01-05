using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfAndroidMockup.Models
{
    public class WycieczkaModel
    {}

    public class Wycieczka : INotifyPropertyChanged
    {
        private string name;
        private DateTime startDate;

        public int Id { get; set; }
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
        public DateTime StartDate {
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
        public DateTime FinishDate { get; set; }
        public string Status { get; set; }
        public string ObszarGorski { get; set; }
        public bool IsSeveralDays { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        public Wycieczka(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            StartDate = DateTime.Now;
            FinishDate = DateTime.Now;
        }

        public void Copy(Wycieczka wycieczka)
        {
            Id = wycieczka.Id;
            Name = wycieczka.Name;

        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
