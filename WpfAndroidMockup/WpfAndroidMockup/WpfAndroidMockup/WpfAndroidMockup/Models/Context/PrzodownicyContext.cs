using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Klasa zajmująca się transformacją obiektów przodownikow otrzymanych z DAO do modeli przodownikow obsługiwanych przez ViewModel.
    /// </summary>
    class PrzodownicyContext
    {
        private static PrzodownicyContext instance;

        private PrzodownikDAO DAO;

        /// <summary>
        /// Konstruktor nieparametryczny dla <see cref="PrzodownicyContext"/>.
        /// </summary>
        private PrzodownicyContext()
        {
            DAO = new PrzodownikDAO(Utils.BAZA_DANYCH_PATH);
        }

        /// <summary>
        /// Zwraca instancje singletonu.
        /// </summary>
        /// <returns><see cref="PrzodownicyContext"/> singleton </returns>
        public static PrzodownicyContext GetInstance()
        {
            if (instance == null)
            {
                instance = new PrzodownicyContext();
            }
            return instance;
        }

        /// <summary>
        /// Akcesor dla przodownik.
        /// </summary>
        /// <param name="id">nr przodownika.</param>
        /// <returns></returns>
        public PrzodownikModel GetModel(long nrPrzodownika)
        {
            Przodownik pracownik = DAO.Find(nrPrzodownika);
            PrzodownikModel model = null;
            if (pracownik != null)
                model = new PrzodownikModel() { NrPrzodownika = pracownik.NrPrzodownika, Imie = pracownik.Imie, Nazwisko = pracownik.Nazwisko, ObszaryUprawnien = pracownik.ObszaryUprawnien };
            return model;
        }

        /// <summary>
        /// Sprawdza czy dany przodownik istnieje w bazie.
        /// </summary>
        /// <param name="nrPrzodownika">nr przodownika.</param>
        /// <returns>true- istnieje</returns>
        public bool Exists(long nrPrzodownika)
        {
            return (DAO.Find(nrPrzodownika) != null);
        }

        /// <summary>
        /// Sprawdza czy przodownik posiada uprawnienia.
        /// </summary>
        /// <param name="nrPrzodownika">nr przodownika.</param>
        /// <param name="obszarGorski">obszar gorski.</param>
        /// <returns>treu- posiada uprawnienia na dany obszar gorski</returns>
        public bool CzyPosiadaUprawnienia(long nrPrzodownika, string obszarGorski)
        {
            var przodownikModel = GetModel(nrPrzodownika);
            return przodownikModel.ObszaryUprawnien.Contains(obszarGorski);
        }
    }
}
