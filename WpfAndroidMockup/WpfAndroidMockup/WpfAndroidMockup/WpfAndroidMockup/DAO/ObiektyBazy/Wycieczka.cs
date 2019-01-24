using WpfAndroidMockup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOT_PTTK.DAO
{
    public class Wycieczka
    {
        public long Id;
        public long NrPrzodownika;
        public long IdCyklu;
        public string Nazwa;
        public DateTime DataRozpoczecia;
        public DateTime DataZakonczenia;
        public string Status;
        public string ObszarGorski;
        public string Trasa;
        public string PunktPoczatkowy;
        public long Wysokosc;
        public long Dlugosc;
        public int Punktacja;
    }
}
