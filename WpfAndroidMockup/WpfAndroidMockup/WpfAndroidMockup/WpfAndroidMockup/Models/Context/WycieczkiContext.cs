using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace GOT_PTTK.Models
{

    /// <summary>
    /// Klasa zajmująca się transformacją obiektów wycieczek otrzymanych z DAO do modeli wycieczek obsługiwanych przez ViewModel.
    /// </summary>
    public class WycieczkiContext
    {
        private static WycieczkiContext instance;

        public WycieczkaDAO DAO;

        private WycieczkiContext()
        {
            DAO = new WycieczkaDAO(Utils.BAZA_DANYCH_PATH);
        }

        /// <summary>
        /// Zwraca instancję klasy - singletonu <see cref="WycieczkiContext"/>
        /// </summary>
        /// <returns></returns>
        public static WycieczkiContext GetInstance()
        {
            if (instance == null)
                instance = new WycieczkiContext();
            return instance;
        }

        /// <summary>
        /// Funkcja zwracają model wycieczke, o identyfikatorze podanym w parametrze.
        /// Zwraca null jeżeli wycieczka o podanym id nie istnieje.
        /// </summary>
        /// <param name="numer">Id wycieczki w bazie</param>
        /// <returns></returns>
        public WycieczkaModel GetModel(long Id)
        {
            Wycieczka wycieczka = DAO.Find(Id);
            WycieczkaModel wycieczkaModel = null;

            if (wycieczka != null)
            {
                OdznakiContext odznakiContext = OdznakiContext.GetInstance();
                OdznakaModel odznaka = odznakiContext.GetModel(wycieczka.IdCyklu);
                wycieczkaModel = new WycieczkaModel(ref odznaka)
                {
                    Id = wycieczka.Id,
                    NrPrzodownika = wycieczka.NrPrzodownika,
                    Nazwa = wycieczka.Nazwa,
                    DataRozpoczecia = wycieczka.DataRozpoczecia,
                    DataZakonczenia = wycieczka.DataZakonczenia,
                    ObszarGorski = wycieczka.ObszarGorski,
                    Trasa = wycieczka.Trasa,
                    PunktPoczatkowy = wycieczka.PunktPoczatkowy,
                    Wysokosc = wycieczka.Wysokosc,
                    Dlugosc = wycieczka.Dlugosc,
                    Punktacja = wycieczka.Punktacja
                };

                switch (wycieczka.Status)
                {
                    case Utils.STATUS_POTWIERDZONA_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.POTWIERDZONA;
                        break;
                    case Utils.STATUS_NIEPOTWIERDZONA_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.NIEPOTWIERDZONA;
                        break;
                    case Utils.STATUS_WTRAKCIE_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.WTRAKCIE;
                        break;
                }

            }
            return wycieczkaModel;
        }

        public List<WycieczkaModel> GetWszystkie()
        {
            List<Wycieczka> wycieczki = DAO.GetAll();
            WycieczkaModel wycieczkaModel = null;
            List<WycieczkaModel> wycieczkiModelList = new List<WycieczkaModel>();

            OdznakiContext odznakiContext = OdznakiContext.GetInstance();

            for (int i = 0; i < wycieczki.Count; i++)
            {
                Wycieczka wycieczka = wycieczki[i];
                OdznakaModel odznaka = odznakiContext.GetModel(wycieczka.IdCyklu);
                wycieczkaModel = new WycieczkaModel(ref odznaka)
                {
                    Id = wycieczka.Id,
                    NrPrzodownika = wycieczka.NrPrzodownika,
                    Nazwa = wycieczka.Nazwa,
                    DataRozpoczecia = wycieczka.DataRozpoczecia,
                    DataZakonczenia = wycieczka.DataZakonczenia,
                    ObszarGorski = wycieczka.ObszarGorski,
                    Trasa = wycieczka.Trasa,
                    PunktPoczatkowy = wycieczka.PunktPoczatkowy,
                    Wysokosc = wycieczka.Wysokosc,
                    Dlugosc = wycieczka.Dlugosc,
                    Punktacja = wycieczka.Punktacja
                };

                switch (wycieczka.Status)
                {
                    case Utils.STATUS_POTWIERDZONA_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.POTWIERDZONA;
                        break;
                    case Utils.STATUS_NIEPOTWIERDZONA_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.NIEPOTWIERDZONA;
                        break;
                    case Utils.STATUS_WTRAKCIE_STRING:
                        wycieczkaModel.Status = StatusyPotwierdzenia.WTRAKCIE;
                        break;
                }

                wycieczkiModelList.Add(wycieczkaModel);
            }
            return wycieczkiModelList;
        }

        /// <summary>
        /// Zwraca listę wszystkich wycieczek do potwierdzenia przez przodownika o podanym numerze
        /// </summary>
        /// <param name="idPrzodownika"></param>
        /// <returns></returns>
        internal List<WycieczkaModel> GetWycieczkiPrzodownikaDoPotwierdzenia(long idPrzodownika)
        {
            List<WycieczkaModel> wycieczkiDoPotwierdzenia = GetWszystkie().Where(e => e.NrPrzodownika == idPrzodownika && e.Status == StatusyPotwierdzenia.WTRAKCIE).ToList();
            return wycieczkiDoPotwierdzenia;
        }

        /// <summary>
        /// Zwraca listę niepotwierdzonych wycieczek turysty
        /// </summary>
        /// <param name="idTurysty"></param>
        /// <returns></returns>
        internal List<WycieczkaModel> GetNiepotwierdzoneWycieczkiTurysty(long idTurysty)
        {
            List<WycieczkaModel> wycieczkiNiepotwierdzone = GetWszystkie().Where(e => e.Odznaka.Turysta.Id == idTurysty && e.Status == StatusyPotwierdzenia.NIEPOTWIERDZONA).ToList();
            return wycieczkiNiepotwierdzone;
        }

        /// <summary>
        /// Zwraca listę wszystkich wycieczek zalogowanego turysty
        /// </summary>
        /// <returns></returns>
        internal List<WycieczkaModel> GetWycieczkiZalogowanegoTurysty()
        {
            List<WycieczkaModel> wycieczkiZalogowanegoTurysty = GetWszystkie().Where(e => e.Odznaka.Turysta.Id == Utils.ID_ZALOGOWANEGO_TURYSTY).ToList();
            return wycieczkiZalogowanegoTurysty;
        }

        /// <summary>
        /// Usuwa wycieczkę o wskazanym identyfikatorze
        /// </summary>
        /// <param name="idWycieczki"></param>
        public void Usun(long idWycieczki)
        {
            WycieczkaModel wycieczka = GetModel(idWycieczki);
            if (wycieczka.Status == StatusyPotwierdzenia.NIEPOTWIERDZONA)
            {
                wycieczka.Odznaka.UsunWycieczke(wycieczka);
                DAO.Delete(new Wycieczka() { Id = wycieczka.Id });
            }
        }

        /// <summary>
        /// Zmienia status wycieczki o podanym identyfikatorze i przypisuje jej przodownika, który operację wykonał
        /// </summary>
        /// <param name="idWycieczki"></param>
        /// <param name="nrPrzodownika"></param>
        /// <param name="status"></param>
        public void ZmienStatus(long idWycieczki, long nrPrzodownika, StatusyPotwierdzenia status)
        {
            WycieczkaModel wycieczka = GetModel(idWycieczki);
            wycieczka.NrPrzodownika = nrPrzodownika;
            wycieczka.Status = status;
            string statusString = status == StatusyPotwierdzenia.POTWIERDZONA ? Utils.STATUS_POTWIERDZONA_STRING : status == StatusyPotwierdzenia.NIEPOTWIERDZONA ? Utils.STATUS_NIEPOTWIERDZONA_STRING : Utils.STATUS_WTRAKCIE_STRING;
            Wycieczka w = new Wycieczka()
            {
                Id = wycieczka.Id,
                NrPrzodownika = nrPrzodownika,
                IdCyklu = wycieczka.Odznaka.Id,
                Nazwa = wycieczka.Nazwa,
                DataRozpoczecia = wycieczka.DataRozpoczecia,
                DataZakonczenia = wycieczka.DataZakonczenia,
                Status = statusString,
                ObszarGorski = wycieczka.ObszarGorski,
                Trasa = wycieczka.Trasa,
                PunktPoczatkowy = wycieczka.PunktPoczatkowy,
                Wysokosc = wycieczka.Wysokosc,
                Dlugosc = wycieczka.Dlugosc,
                Punktacja = wycieczka.Punktacja
            };
            DAO.Update(w);
        }
    }
}
