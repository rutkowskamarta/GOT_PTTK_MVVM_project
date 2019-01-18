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
            wycieczkiDict.Add(0, new WycieczkaModel(turysciContext.GetTurysta(0), "Wycieczka0", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(1, new WycieczkaModel(turysciContext.GetTurysta(0), "Dominicza Góra", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(2, new WycieczkaModel(turysciContext.GetTurysta(0), "Wycieczka1", StatusyPotwierdzenia.POTWIERDZONA));
            wycieczkiDict.Add(3, new WycieczkaModel(turysciContext.GetTurysta(0), "Wycieczka2", StatusyPotwierdzenia.NIEPOTWIERDZONA));
            wycieczkiDict.Add(4, new WycieczkaModel(turysciContext.GetTurysta(1), "WycieczkaInnegoTurysty", StatusyPotwierdzenia.WTRAKCIE));
        }

        public WycieczkaModel GetWycieczka(long id)
        {
            WycieczkaModel value = null;
            wycieczkiDict.TryGetValue(id, out value);
            return value;
        }

        public void DeleteWycieczka(long id)
        {
            throw new NotImplementedException();
        }

        public List<WycieczkaModel> GetWycieczkiZalogowanegoTurysty()
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                        where wycieczka.Value.Turysta.idTurysty == DaneLogowania.IdZalogowanegoTurysty
                        select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }
    }
}
