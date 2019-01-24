using GOT_PTTK.DAO;
using System.Linq;
using GOT_PTTK.Utilities;

namespace GOT_PTTK.Models
{

    /// <summary>
    /// Klasa zajmująca się transformacją obiektów pracowników otrzymanych z DAO do modeli odznak obsługiwanych przez ViewModel.
    /// </summary>
    public class PracownicyContext
    {
        private static PracownicyContext instance;

        private PracownikDAO DAO;
        private OdznakiContext odznakiContext;

        private PracownicyContext()
        {
            DAO = new PracownikDAO(Utils.BAZA_DANYCH_PATH);
            odznakiContext = OdznakiContext.GetInstance();
        }

        /// <summary>
        /// Zwraca instancje singletonu.
        /// </summary>
        /// <returns><see cref="PracownicyContext"/> singleton </returns>
        public static PracownicyContext GetInstance()
        {
            if (instance == null)
                instance = new PracownicyContext();

            return instance;
        }

        /// <summary>
        /// Funkcja zwracają model pracownika, o numerze podanym w parametrze, z bazy.
        /// Zwraca null jeżeli pracownik o podanym numerze nie istnieje.
        /// </summary>
        /// <param name="numer">Numer pracownika identyfikujący pracaownika w bazie</param>
        /// <returns></returns>
        public PracownikModel GetModel(long numer)
        {
            Pracownik p = DAO.Find(numer);
            if (p != null)
            {
                PracownikModel pracownik = new PracownikModel()
                {
                    NumerPracownika = p.Numer,
                    Imie = p.Imie,
                    Nazwisko = p.Nazwisko,
                    DataZatrudnienia = p.DataZatrudnienia,
                    MiastoTRW = p.MiastoTRW.Equals(Utils.EMPTY_FIELD) ? null : p.MiastoTRW,
                    OdznakiZweryfikowane = odznakiContext.GetOdznakiZweryfikowane().Count(e => e.NrPracownika == numer)
                };

                return pracownik;
            }

            return null;
        }

        /// <summary>
        /// Usunięcie pracownika, o podanym numerze, w bazie
        /// </summary>
        /// <param name="numer">Numer pracownika identyfikujący pracownika w bazie</param>
        public void DeletePracownik(long numer)
        {
            Pracownik pracownik = DAO.Find(numer);
            if (pracownik != null)
            {
                DAO.Delete(pracownik);
            }
        }

    }
}
