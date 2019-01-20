using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{

    /// <summary>
    /// Klasa zajmująca się transformacją obiektów wycieczek otrzymanych z DAO do modeli wycieczek obsługiwanych przez ViewModel.
    /// </summary>
    public class WycieczkiContext
    {
        private static WycieczkiContext instance;
        private Dictionary<long, WycieczkaModel> wycieczkiDict;

        /// <summary>
        /// Konsturktor nieparametryczny klasy <see cref="WycieczkiContext"/>.
        /// </summary>
        private WycieczkiContext()
        {
            LoadExamplaryTrips();   
        }

        /// <summary>
        /// Zwraca instancje singletonu.
        /// </summary>
        /// <returns><see cref="WycieczkiContext"/> singleton </returns>
        public static WycieczkiContext GetInstance()
        {
            if (instance == null)
            {
                instance = new WycieczkiContext();
            }
            return instance;
        }

        /// <summary>
        /// Ładuje przykładowe wycieczki.
        /// </summary>
        private void LoadExamplaryTrips()
        {
            wycieczkiDict = new Dictionary<long, WycieczkaModel>();
            TurysciContext turysciContext = TurysciContext.GetInstance();
            TurystaModel t1 = turysciContext.GetTurysta(0);
            TurystaModel t2 = turysciContext.GetTurysta(1);

            OdznakaModel o1 = OdznakiContext.GetInstance().GetOdznaka(0);
            OdznakaModel o2 = new OdznakaModel(ref t2);
           
            wycieczkiDict.Add(0, new WycieczkaModel(0, ref t1, ref o1, "Rusinowa Polana", StatusyPotwierdzenia.POTWIERDZONA, "Tatry Wysokie", "z Dolin Flipka","Rusinowa Polana"));
           
            wycieczkiDict.Add(1, new WycieczkaModel(1, ref t1, ref o1, "Dominicza Góra", StatusyPotwierdzenia.NIEPOTWIERDZONA, "Podgórze Wiśnickie", "z Rajbrontu", "Dominicza Góra"));
            wycieczkiDict.Add(2, new WycieczkaModel(2, ref t1, ref o1, "Moja ulubiona wycieczka", StatusyPotwierdzenia.NIEPOTWIERDZONA, "Beskid Śląski", "z Goleszowa", "Dzięgielów - Zamek"));
            wycieczkiDict.Add(3, new WycieczkaModel(3, ref t1, ref o1, "Jasieniowa 2", StatusyPotwierdzenia.WTRAKCIE,"Beskid Śląski","z Goleszowa", "Jasieniowa"));
            wycieczkiDict.Add(4, new WycieczkaModel(4, ref t2, ref o2, "Jasieniowa 2", StatusyPotwierdzenia.WTRAKCIE,"Beskid Śląski","z Goleszowa", "Jasieniowa"));
        }

        /// <summary>
        /// Zwraca wycieczkę o id.
        /// </summary>
        /// <param name="id">id wycieczki</param>
        /// <returns> wycieczka</returns>
        public WycieczkaModel GetWycieczka(long id)
        {
            WycieczkaModel value = null;
            wycieczkiDict.TryGetValue(id, out value);
            return value;
        }

        /// <summary>
        /// Usuwa wycieczkę o id
        /// </summary>
        /// <param name="idWycieczki"></param>
        public void Usun(long idWycieczki)
        { var wycieczkaDoUsuniecia = from wycieczka in wycieczkiDict
                                     where wycieczka.Value.Id == idWycieczki
                                     select wycieczka.Key;
            
            wycieczkiDict.Remove(wycieczkaDoUsuniecia.ToList()[0]);
        }

        /// <summary>
        /// Zwraca wszystkie wycieczki turysty, które nie zostały potwierdzone
        /// </summary>
        /// <param name="idTurysty"></param>
        /// <returns>lista modeli wycieczek</returns>
        public List<WycieczkaModel> GetNiepotwierdzoneWycieczkiTurysty(long idTurysty)
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                            where wycieczka.Value.Turysta.Id == DaneLogowania.IdZalogowanegoTurysty && wycieczka.Value.CzyNiepotwierdzona()
                            select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        /// <summary>
        /// Zwraca wszystkie wycieczki do potwierdzenia dla przodownika
        /// </summary>
        /// <param name="nrPrzodownika"></param>
        /// <returns> lista modeli wycieczek</returns>
        public List<WycieczkaModel> GetWycieczkiPrzodownikaDoPotwierdzenia(long nrPrzodownika)
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                            where wycieczka.Value.NrPrzodownika == nrPrzodownika && wycieczka.Value.Status == StatusyPotwierdzenia.WTRAKCIE
                            select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        /// <summary>
        /// Zwraca wszystkie wycieczki zalogowanego turysty
        /// </summary>
        /// <returns>lista modeli wycieczek</returns>
        public List<WycieczkaModel> GetWycieczkiZalogowanegoTurysty()
        {
            List<WycieczkaModel> wycieczkiList = new List<WycieczkaModel>();
            var wycieczki = from wycieczka in wycieczkiDict
                        where wycieczka.Value.Turysta.Id == DaneLogowania.IdZalogowanegoTurysty
                        select wycieczka.Value;

            wycieczkiList = wycieczki.ToList();
            return wycieczkiList;
        }

        /// <summary>
        /// zmienia status wycieczki
        /// </summary>
        /// <param name="idWycieczki"></param>
        /// <param name="nrPrzodownika"></param>
        /// <param name="status"></param>
        public void ZmienStatus(long idWycieczki, long nrPrzodownika, StatusyPotwierdzenia status)
        {
            WycieczkaModel w = null;
            wycieczkiDict.TryGetValue(idWycieczki, out w);
            if (w != null)
            {
                w.NrPrzodownika = nrPrzodownika;
                w.Status = status;
            }
            
        }
    }
}
