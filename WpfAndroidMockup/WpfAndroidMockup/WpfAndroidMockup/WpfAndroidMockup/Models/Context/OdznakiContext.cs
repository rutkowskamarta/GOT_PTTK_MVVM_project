using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Typ enumeracyjny dot. statusu przyznania odznaki
    /// </summary>
    public enum StatusOdznaki
    {
        DOWERYFIKACJI, PRZYZNANA, ODRZUCONA
    }

    /// <summary>
    /// Klasa zajmująca się transformacją obiektów odznak otrzymanych z DAO do modeli odznak obsługiwanych przez ViewModel.
    /// </summary>
    public class OdznakiContext
    {
        public const string DUZA_BRAZOWA_IMG_PATH = "/WpfAndroidMockup;component/Assets/brazowa.png";
        public const string POPULARNA_IMG_PATH = "/WpfAndroidMockup;component/Assets/zlota.png";
        public const string MALA_BRAZOWA_IMG_PATH = "/WpfAndroidMockup;component/Assets/brazowa.png";

        private static OdznakiContext instance;

        private OdznakaDAO DAO;
        private TurysciContext turysciContext;
        private WycieczkiContext wycieczkiContext;

        private OdznakiContext()
        {
            DAO = new OdznakaDAO(Utils.BAZA_DANYCH_PATH);
        }

        /// <summary>
        /// Zwraca instancję singletonu <see cref="OdznakiContext"/>.
        /// </summary>
        /// <returns>instancja singetonu</returns>
        public static OdznakiContext GetInstance()
        {
            if (instance == null)
                instance = new OdznakiContext();

            return instance;
        }

        /// <summary>
        /// Funkcja ustawiająca image path dla ikony odznaki ze względu na rodzaj i stopien odznaki
        /// </summary>
        /// <param name="o">referencja do odznaki</param>
        private void SetImage(ref OdznakaModel o)
        {
            string typ = o.Stopien + " " + o.Rodzaj;
            switch (typ)
            {
                case "mala brazowa":
                    o.ImgPath = MALA_BRAZOWA_IMG_PATH;
                    break;
                case "duza brazowa":
                    o.ImgPath = DUZA_BRAZOWA_IMG_PATH;
                    break;
                case "popularna":
                    o.ImgPath = POPULARNA_IMG_PATH;
                    break;
                default:
                    o.ImgPath = MALA_BRAZOWA_IMG_PATH;
                    break;
            }
        }

        /// <summary>
        /// Dodawanie modeli wycieczek do modelu odznaki powiązanych z wycieczkami
        /// </summary>
        /// <param name="odznakaModel"></param>
        private void DodajWycieczki(ref OdznakaModel odznakaModel)
        {
            long idOdznaki = odznakaModel.Id;
            List<Wycieczka> wycieczkiNaOdznake = wycieczkiContext.DAO.GetAll().Where(e => e.IdCyklu == idOdznaki).ToList();
            foreach (Wycieczka wycieczka in wycieczkiNaOdznake)
            {
                WycieczkaModel wycieczkaModel = new WycieczkaModel(ref odznakaModel)
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

                odznakaModel.DodajWycieczke(wycieczkaModel);
            }
        }

        /// <summary>
        /// Funkcja zwracają model pracownika, o numerze podanym w parametrze, z bazy.
        /// Zwraca null jeżeli pracownik o podanym numerze nie istnieje.
        /// </summary>
        /// <param name="numer">Numer pracownika identyfikujący pracaownika w bazie</param>
        /// <returns></returns>
        public OdznakaModel GetModel(long Id)
        {
            if (turysciContext == null)
                turysciContext = TurysciContext.GetInstance();
            if (wycieczkiContext == null)
                wycieczkiContext = WycieczkiContext.GetInstance();

            Odznaka o = DAO.Find(Id);
            TurystaModel t = turysciContext.GetModel(o.IdTurysty);
            OdznakaModel odznakaModel = new OdznakaModel() { Id = o.Id, Rodzaj = o.Rodzaj, Stopien = o.Stopien, MinPkt = o.MinPkt, DataRozpoczecia = o.DataRozpoczecia, NrPracownika = o.NrPracownika };
            odznakaModel.Turysta = t;
            SetImage(ref odznakaModel);
            DodajWycieczki(ref odznakaModel);

            return odznakaModel;

        }

        /// <summary>
        /// Zwraca wszystkie odznaki, które nie zostały wysłane do weryfikacji
        /// </summary>
        /// <returns></returns>
        public List<OdznakaModel> GetOdznakiNieDoWeryfikacji()
        {
            if (turysciContext == null)
                turysciContext = TurysciContext.GetInstance();
            if (wycieczkiContext == null)
                wycieczkiContext = WycieczkiContext.GetInstance();

            List<OdznakaModel> odznakiList = new List<OdznakaModel>();
            List<Odznaka> odznakiWszystkie = DAO.GetAll();
            foreach (Odznaka o in odznakiWszystkie)
            {
                if (!o.CzyPrzyznana && !o.CzyDoWeryfikacji)
                {
                    TurystaModel t = turysciContext.GetModel(o.IdTurysty);
                    OdznakaModel odznakaModel = new OdznakaModel() { Id = o.Id, Rodzaj = o.Rodzaj, Stopien = o.Stopien, MinPkt = o.MinPkt, DataRozpoczecia = o.DataRozpoczecia, NrPracownika = o.NrPracownika };
                    odznakaModel.Turysta = t;
                    SetImage(ref odznakaModel);
                    DodajWycieczki(ref odznakaModel);

                    odznakiList.Add(odznakaModel);
                }
            }
            return odznakiList;
        }

        /// <summary>
        /// Zwraca wszystkie odznaki
        /// </summary>
        /// <returns></returns>
        public List<OdznakaModel> GetWszystkieOdznaki()
        {
            if (turysciContext == null)
                turysciContext = TurysciContext.GetInstance();
            if (wycieczkiContext == null)
                wycieczkiContext = WycieczkiContext.GetInstance();

            List<OdznakaModel> odznakiList = new List<OdznakaModel>();
            List<Odznaka> odznakiWszystkie = DAO.GetAll();
            foreach (Odznaka o in odznakiWszystkie)
            {
                TurystaModel t = turysciContext.GetModel(o.IdTurysty);
                OdznakaModel odznakaModel = new OdznakaModel() { Id = o.Id, Rodzaj = o.Rodzaj, Stopien = o.Stopien, MinPkt = o.MinPkt, DataRozpoczecia = o.DataRozpoczecia, NrPracownika = o.NrPracownika };
                odznakaModel.Turysta = t;
                SetImage(ref odznakaModel);
                DodajWycieczki(ref odznakaModel);

                odznakiList.Add(odznakaModel);
            }

            return odznakiList;
        }

        /// <summary>
        /// Zwraca wszystkie zweryfikowane odznaki
        /// </summary>
        /// <returns></returns>
        public List<OdznakaModel> GetOdznakiZweryfikowane()
        {
            if (turysciContext == null)
                turysciContext = TurysciContext.GetInstance();
            if (wycieczkiContext == null)
                wycieczkiContext = WycieczkiContext.GetInstance();

            List<OdznakaModel> odznakiList = new List<OdznakaModel>();
            List<Odznaka> odznakiWszystkie = DAO.GetAll();
            foreach (Odznaka o in odznakiWszystkie)
            {
                if (o.CzyZweryfikowana)
                {
                    TurystaModel t = turysciContext.GetModel(o.IdTurysty);
                    OdznakaModel odznakaModel = new OdznakaModel() { Id = o.Id, Rodzaj = o.Rodzaj, Stopien = o.Stopien, MinPkt = o.MinPkt, DataRozpoczecia = o.DataRozpoczecia, NrPracownika = o.NrPracownika };
                    odznakaModel.Turysta = t;
                    SetImage(ref odznakaModel);
                    DodajWycieczki(ref odznakaModel);

                    odznakiList.Add(odznakaModel);
                }
            }
            return odznakiList;
        }

        /// <summary>
        /// Zwraca wszystkie odznaki, które mogą być przekazane do weryfikacji
        /// </summary>
        /// <returns></returns>
        public List<OdznakaModel> GetOdznakiDoWeryfikacji()
        {
            if (turysciContext == null)
                turysciContext = TurysciContext.GetInstance();
            if (wycieczkiContext == null)
                wycieczkiContext = WycieczkiContext.GetInstance();

            List<OdznakaModel> odznakiList = new List<OdznakaModel>();
            List<Odznaka> odznakiWszystkie = DAO.GetAll();
            foreach (Odznaka o in odznakiWszystkie)
            {
                if (o.CzyDoWeryfikacji && !o.CzyZweryfikowana)
                {
                    TurystaModel t = turysciContext.GetModel(o.IdTurysty);
                    OdznakaModel odznakaModel = new OdznakaModel() { Id = o.Id, Rodzaj = o.Rodzaj, Stopien = o.Stopien, MinPkt = o.MinPkt, DataRozpoczecia = o.DataRozpoczecia, NrPracownika = o.NrPracownika };
                    odznakaModel.Turysta = t;
                    SetImage(ref odznakaModel);
                    DodajWycieczki(ref odznakaModel);

                    odznakiList.Add(odznakaModel);
                }
            }
            return odznakiList;
        }

        /// <summary>
        /// Zmienia status odznaki 
        /// </summary>
        /// <param name="id">Id odznaki</param>
        /// <param name="nrPracownika">Numer pracownika wprowadzający zmiany</param>
        /// <param name="status">Nowy status odznaki (Odrzucona, Zaakceptowana lub Do weryfikacji)</param>
        public void ZmienStatus(long id, StatusOdznaki status, long nrPracownika = OdznakaModel.NR_PRACOWNIKA_DO_WERYFIKACJI)
        {
            Odznaka odznaka = DAO.Find(id);
            if (odznaka != null)
            {
                switch (status)
                {
                    case StatusOdznaki.DOWERYFIKACJI:
                        odznaka.NrPracownika = OdznakaModel.NR_PRACOWNIKA_DO_WERYFIKACJI;
                        odznaka.CzyDoWeryfikacji = true;
                        odznaka.CzyPrzyznana = false;
                        odznaka.CzyZweryfikowana = false;
                        break;
                    case StatusOdznaki.ODRZUCONA:
                        odznaka.NrPracownika = nrPracownika;
                        odznaka.CzyDoWeryfikacji = false;
                        odznaka.CzyPrzyznana = false;
                        odznaka.CzyZweryfikowana = false;
                        break;
                    case StatusOdznaki.PRZYZNANA:
                        odznaka.NrPracownika = nrPracownika;
                        odznaka.CzyDoWeryfikacji = true;
                        odznaka.CzyPrzyznana = true;
                        odznaka.CzyZweryfikowana = true;
                        break;
                }
                DAO.Update(odznaka);

            }
        }
    }
}
