using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System.Collections.Generic;

namespace GOT_PTTK.Models
{
    /// <summary>
    /// Klasa zajmująca się transformacją obiektów turystow otrzymanych z DAO do modeli turystów obsługiwanych przez ViewModel.
    /// </summary>
    public class TurysciContext
    {
        private static TurysciContext instance;

        private TurystaDAO DAO;

        private TurysciContext()
        {
            DAO = new TurystaDAO(Utils.BAZA_DANYCH_PATH);
        }

        /// <summary>
        /// Zwraca instancję klasy - singletonu <see cref="TurysciContext"/>
        /// </summary>
        /// <returns></returns>
        public static TurysciContext GetInstance()
        {
            if (instance == null)
                instance = new TurysciContext();

            return instance;
        }

        /// <summary>
        /// Zwraca model turysty o podanym identyfikatorze
        /// </summary>
        /// <param name="id"></param>
        /// <returns>model turysty o podanym identyfikatorze</returns>
        public TurystaModel GetModel(long id)
        {
            Turysta turysta = DAO.Find(id);
            TurystaModel model = null;
            if (turysta != null)
                model = new TurystaModel() { Id = turysta.Id, Imie = turysta.Imie, Nazwisko = turysta.Nazwisko };
            return model;
        }

        /// <summary>
        /// Zwraca listę wszystkich turystów z bazy danych
        /// </summary>
        /// <returns>Lista modeli turysty</returns>
        public List<TurystaModel> GetAll()
        {
            List<Turysta> turysci = DAO.GetAll();
            List<TurystaModel> list = new List<TurystaModel>();
            foreach (Turysta turysta in turysci)
                list.Add(new TurystaModel() { Id = turysta.Id, Imie = turysta.Imie, Nazwisko = turysta.Nazwisko });
            return list;
        }
    }
}
