using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Klasa zajmująca się transformacją obiektów przodownikow otrzymanych z DAO do modeli przodownikow obsługiwanych przez ViewModel.
    /// </summary>
    class PrzodownicyContext
    {
        private static PrzodownicyContext instance;
        private Dictionary<long, PrzodownikModel> przodownicyDisct;

        /// <summary>
        /// Konstruktor nieparametryczny dla <see cref="PrzodownicyContext"/>.
        /// </summary>
        private PrzodownicyContext()
        {
            LoadExamplaryPrzodownik();
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
        /// Wypelnia baze danych przykladowymi przodownikami
        /// </summary>
        private void LoadExamplaryPrzodownik()
        {
            przodownicyDisct = new Dictionary<long, PrzodownikModel>();
            przodownicyDisct.Add(0, new PrzodownikModel(0));

            PrzodownikModel p1 = new PrzodownikModel(1);
            p1.ObszaryUprawnien.Add("Podgórze Wiśnickie");

            przodownicyDisct.Add(1, p1);
            przodownicyDisct.Add(2, new PrzodownikModel(2));
            
        }

        /// <summary>
        /// Akcesor dla przodownik.
        /// </summary>
        /// <param name="id">Identyfikator przodownika.</param>
        /// <returns></returns>
        public PrzodownikModel GetPrzodownik(long id)
        {
            PrzodownikModel value = null;
            przodownicyDisct.TryGetValue(id, out value);
            return value;
        }

        /// <summary>
        /// Sprawdza czy dany przodownik istnieje w bazie.
        /// </summary>
        /// <param name="nrPrzodownika">nr przodownika.</param>
        /// <returns>true- istnieje</returns>
        public bool Exists(long nrPrzodownika)
        {
            var przodownicy = from przodownik in przodownicyDisct
                              where przodownik.Value.NrPrzodownika == nrPrzodownika
                              select przodownik.Value;
            return (przodownicy.ToList<PrzodownikModel>().Count != 0);
           
        }

        /// <summary>
        /// Sprawdza czy przodownik posiada uprawnienia.
        /// </summary>
        /// <param name="nrPrzodownika">nr przodownika.</param>
        /// <param name="obszarGorski">obszar gorski.</param>
        /// <returns>treu- posiada uprawnienia na dany obszar gorski</returns>
        public bool CzyPosiadaUprawnienia(long nrPrzodownika, string obszarGorski)
        {
            var uprawnienia = from przodownik in przodownicyDisct
                              where przodownik.Value.NrPrzodownika == nrPrzodownika && przodownik.Value.ObszaryUprawnien.Contains(obszarGorski)
                              select przodownik.Value;
            return (uprawnienia.ToList().Count != 0);
        }
    }
}
