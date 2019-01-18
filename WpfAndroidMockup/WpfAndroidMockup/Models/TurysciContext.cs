using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    public class TurysciContext
    {
        private static TurysciContext instance;
        private Dictionary<long, TurystaModel> turysciDict;

        private TurysciContext()
        {
            LoadExamplaryTourists();
        }

        public static TurysciContext GetInstance()
        {
            if (instance == null)
            {
                instance = new TurysciContext();
            }
            return instance;
        }

        private void LoadExamplaryTourists()
        {
            turysciDict = new Dictionary<long, TurystaModel>();
            turysciDict.Add(0, new TurystaModel(0));
            turysciDict.Add(1, new TurystaModel(1));
            turysciDict.Add(2, new TurystaModel(2));
        }

        public TurystaModel GetTurysta(long id)
        {
            TurystaModel value = null;
            turysciDict.TryGetValue(id, out value);
            return value;
        }

       

    }
}
