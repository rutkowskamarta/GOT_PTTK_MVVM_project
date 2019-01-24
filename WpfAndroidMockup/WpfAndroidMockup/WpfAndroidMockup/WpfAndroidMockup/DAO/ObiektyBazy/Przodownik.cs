using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.DAO
{
    class Przodownik
    {
        public long NrPrzodownika;
        public string Imie;
        public string Nazwisko;
        public List<string> ObszaryUprawnien;

        public Przodownik ()
        {
            ObszaryUprawnien = new List<string>();
        }
    }
}
