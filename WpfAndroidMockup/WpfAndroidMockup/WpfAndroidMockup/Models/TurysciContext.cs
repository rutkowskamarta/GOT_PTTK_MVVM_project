using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Klasa zajmująca się transformacją obiektów turystow otrzymanych z DAO do modeli turystów obsługiwanych przez ViewModel.
    /// </summary>
    public class TurysciContext
    {
        private static TurysciContext instance;
        private Dictionary<long, TurystaModel> turysciDict;

        /// <summary>
        /// Konstruktor nieparametryczny dla <see cref="PrzodownicyContext"/>.
        /// </summary>
        private TurysciContext()
        {
            LoadExamplaryTourists();
        }

        /// <summary>
        /// Zwraca instancje singletonu.
        /// </summary>
        /// <returns><see cref="TurysciContext"/> singleton </returns>
        public static TurysciContext GetInstance()
        {
            if (instance == null)
            {
                instance = new TurysciContext();
            }
            return instance;
        }

        /// <summary>
        /// Wypelnia baze danych przykladowymi turystami
        /// </summary>
        private void LoadExamplaryTourists()
        {
            turysciDict = new Dictionary<long, TurystaModel>();
            turysciDict.Add(0, new TurystaModel(0, "Marta", "Rutkowska"));
            turysciDict.Add(1, new TurystaModel(1));
            turysciDict.Add(2, new TurystaModel(2));
        }

        /// <summary>
        /// Akcesor dla turysty.
        /// </summary>
        /// <param name="id">Identyfikator turysty.</param>
        /// <returns></returns>
        public TurystaModel GetTurysta(long id)
        {
            TurystaModel value = null;
            turysciDict.TryGetValue(id, out value);
            return value;
        }
    }
}
