using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOT_PTTK.DAO
{
    public class Odznaka
    {
        public long Id;
        public long IdTurysty;
        public string Stopien;
        public string Rodzaj;
        public int MinPkt;
        public DateTime DataRozpoczecia;
        public bool CzyZweryfikowana;
        public bool CzyPrzyznana;
        public bool CzyDoWeryfikacji;
        public long NrPracownika;
    }
}
