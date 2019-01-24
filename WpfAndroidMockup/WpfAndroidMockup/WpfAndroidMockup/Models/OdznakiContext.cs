using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
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
        public const string BRAZOWA_IMG_PATH = "/WpfAndroidMockup;component/Assets/brazowa.png";
        public const string POPULARNA_IMG_PATH = "/WpfAndroidMockup;component/Assets/got_pop.png";
        public const string ZLOTA_IMG_PATH = "/WpfAndroidMockup;component/Assets/zlota.png";

        private static OdznakiContext instance;

        private TurysciContext turysciContext;
        private Dictionary<long, OdznakaModel> odznakiDict;
        private Dictionary<long, OdznakaModel> odznakiDoWeryfikacjiDict;
        private Dictionary<long, OdznakaModel> odznakiZweryfikowaneDict;

        /// <summary>
        /// Konstruktor nieparametryczny dla singletonu <see cref="OdznakiContext"/>.
        /// </summary>
        private OdznakiContext()
        {
            turysciContext = TurysciContext.GetInstance();
            TurystaModel turysta = turysciContext.GetTurysta(0);
            odznakiDict = new Dictionary<long, OdznakaModel>();

            odznakiDict.Add(0, new OdznakaModel(ref turysta) { Id = 0, Rodzaj = "mała", Stopien = "złota", ImgPath = OdznakiContext.ZLOTA_IMG_PATH, MinPkt = 120, DataRozpoczecia = DateTime.Now.AddDays(-100) });
            odznakiDict.Add(1, new OdznakaModel(ref turysta) { Id = 1, Rodzaj = "mała", Stopien = "brązowa", ImgPath = OdznakiContext.BRAZOWA_IMG_PATH, MinPkt = 60, DataRozpoczecia = DateTime.Now.AddDays(-100) });
            odznakiDict.Add(2, new OdznakaModel(ref turysta) { Id = 2, Rodzaj = "popularna", Stopien = "", ImgPath = OdznakiContext.POPULARNA_IMG_PATH, MinPkt = 60, DataRozpoczecia = DateTime.Now.AddDays(-100) });

            OdznakaModel o = null;
            
            odznakiDict.TryGetValue(2, out o);
            o.Pkt = 61;

            odznakiDoWeryfikacjiDict = new Dictionary<long, OdznakaModel>();
            
            odznakiZweryfikowaneDict = new Dictionary<long, OdznakaModel>();
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
        /// Funkcja zwracają model pracownika, o numerze podanym w parametrze, z bazy.
        /// Zwraca null jeżeli pracownik o podanym numerze nie istnieje.
        /// </summary>
        /// <param name="numer">Numer pracownika identyfikujący pracaownika w bazie</param>
        /// <returns></returns>
        public OdznakaModel GetOdznaka(long Id)
        {
            odznakiDict.TryGetValue(Id, out OdznakaModel pracownik);

            return pracownik;
        }


        /// <summary>
        /// Zwraca listę odznak niezaakcpetowanych.
        /// </summary>
        /// <returns>Lista <see cref="OdznakaModel"/>.</returns>
        public List<OdznakaModel> GetOdznakiNiezaakcpetowane()
        {
            var o = from odznaka in odznakiDict
                    where odznaka.Value.CzyZweryfikowana == false
                    select odznaka.Value;
            return o.ToList<OdznakaModel>();
        }

        /// <summary>
        /// Zmienia status.
        /// </summary>
        /// <param name="id">Identyfikator odznaki.</param>
        /// <param name="status">Typ enumeracyjny status.</param>
        public void ZmienStatus(long id, StatusOdznaki status)
        {
            OdznakaModel odznaka;


            switch (status)
            {
                case StatusOdznaki.DOWERYFIKACJI:
                    odznakiDict.TryGetValue(id, out odznaka);
                    if (odznaka != null)
                    {
                        odznakiZweryfikowaneDict.Remove(id);
                        odznakiDoWeryfikacjiDict.Add(id, odznaka);
                    }
                    break;
                case StatusOdznaki.ODRZUCONA:
                    odznakiDoWeryfikacjiDict.Remove(id);
                    odznakiZweryfikowaneDict.Remove(id);
                    break;
                case StatusOdznaki.PRZYZNANA:
                    odznakiDict.TryGetValue(id, out odznaka);
                    if (odznaka != null)
                    {
                        odznakiZweryfikowaneDict.Add(id, odznaka);
                        odznakiDoWeryfikacjiDict.Remove(id);
                    }
                    break;
            }
        }
    }
}
