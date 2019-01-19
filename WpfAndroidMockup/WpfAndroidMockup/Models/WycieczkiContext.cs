using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    public class WycieczkiContext
    {
        private static WycieczkiContext instance;
        private Dictionary<long, WycieczkaModel> wycieczkiDict;

        private WycieczkiContext()
        {
            LoadExamplaryTrips();   
        }

        public static WycieczkiContext GetInstance()
        {
            if (instance == null)
            {
                instance = new WycieczkiContext();
            }
            return instance;
        }

        private void LoadExamplaryTrips()
        {
            wycieczkiDict = new Dictionary<long, WycieczkaModel>();
            TurysciContext turysciContext = TurysciContext.GetInstance();
            TurystaModel t1 = turysciContext.GetTurysta(0);
            TurystaModel t2 = turysciContext.GetTurysta(1);

            OdznakaModel o1 = new OdznakaModel(ref t1);
            OdznakaModel o2 = new OdznakaModel(ref t2);
            wycieczkiDict.Add(0, new WycieczkaModel(0, ref t1, ref o1, "Wycieczka0", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(1, new WycieczkaModel(1, ref t1, ref o1, "Dominicza Góra", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(2, new WycieczkaModel(2, ref t1, ref o1, "Wycieczka1", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(3, new WycieczkaModel(3, ref t1, ref o1, "Wycieczka2", StatusyPotwierdzenia.NIEPOTWIERDZONA));
            wycieczkiDict.Add(4, new WycieczkaModel(4, ref t2, ref o2, "WycieczkaInnegoTurysty", StatusyPotwierdzenia.WTRAKCIE));
        }

        public WycieczkaModel GetWycieczka(long id)
        {
            WycieczkaModel value = null;
            wycieczkiDict.TryGetValue(id, out value);
            return value;
        }

        public void Usun(long id)
        { var wycieczkaDoUsuniecia = from wycieczka in wycieczkiDict
                                     where wycieczka.Value.Id == id
                                     select wycieczka.Key;
            
            wycieczkiDict.Remove(wycieczkaDoUsuniecia.ToList()[0]);
        }

        public List<WycieczkaModel> GetNiepotwierdzoneWycieczkiTurysty(long idTurysty)
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                            where wycieczka.Value.Turysta.Id == DaneLogowania.IdZalogowanegoTurysty && wycieczka.Value.CzyNiepotwierdzona()
                            select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        public List<WycieczkaModel> GetWycieczkiPrzodownikaDoPotwierdzenia(long nrPrzodownika)
        {
            throw new NotImplementedException();
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                            where wycieczka.Value.Turysta.Id == DaneLogowania.IdZalogowanegoTurysty
                            select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        public List<WycieczkaModel> GetWycieczkiZalogowanegoTurysty()
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                        where wycieczka.Value.Turysta.Id == DaneLogowania.IdZalogowanegoTurysty
                        select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        //public void ZmienStatus(WycieczkaModel aktualnaWycieczka, int idWycieczki, long nrPrzodownika, StatusWycieczkiEnum status)
        //{

        //}
    }
}
