using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    class PrzodownicyContext
    {
        private static PrzodownicyContext instance;
        private Dictionary<long, PrzodownikModel> przodownicyDisct;

        private PrzodownicyContext()
        {
            LoadExamplaryPrzodownik();
        }

        public static PrzodownicyContext GetInstance()
        {
            if (instance == null)
            {
                instance = new PrzodownicyContext();
            }
            return instance;
        }

        private void LoadExamplaryPrzodownik()
        {
            przodownicyDisct = new Dictionary<long, PrzodownikModel>();
            przodownicyDisct.Add(0, new PrzodownikModel(0));
            przodownicyDisct.Add(1, new PrzodownikModel(1));
            przodownicyDisct.Add(2, new PrzodownikModel(2));
        }

        public PrzodownikModel GetPrzodownik(long id)
        {
            PrzodownikModel value = null;
            przodownicyDisct.TryGetValue(id, out value);
            return value;
        }

        public bool Exists(long nrPrzodownika)
        {
            var przodownicy = from przodownik in przodownicyDisct
                              where przodownik.Value.NrPrzodownika == nrPrzodownika
                              select przodownik.Value;
            return (przodownicy.ToList<PrzodownikModel>().Count != 0);
           
        }
    }
}
