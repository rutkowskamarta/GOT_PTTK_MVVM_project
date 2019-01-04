using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfAndroidMockup.Models
{
    class WycieczkaModel
    {}

    class Wycieczka : INotifyPropertyChanged
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private DateTime StartDate { get; set; }
        private DateTime FinishDate { get; set; }
        private StatusModel Status { get; set; }
        private ObszarGorskiModel ObszarGorski { get; set; }
        private bool IsSeveralDays { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public Wycieczka()
        {

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
