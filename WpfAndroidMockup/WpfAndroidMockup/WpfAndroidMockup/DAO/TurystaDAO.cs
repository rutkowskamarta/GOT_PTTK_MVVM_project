using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System;
using System.Collections.Generic;
using System.IO;


namespace GOT_PTTK.DAO
{
    class TurystaDAO : IDAO<Turysta>
    {

        string turystaPath;

        /// <summary>
        /// Konstruktor parametryczny TurystaDAO
        /// </summary>
        /// <param name="databasePath"> ścieżka do bazy danych</param>
        public TurystaDAO(string databasePath)
        {
            turystaPath = databasePath + Utils.TURYSCI_FILE;
        }

        public Turysta Find(long Id)
        {
            string[] lines = System.IO.File.ReadAllLines(turystaPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == Id)
                {
                    Turysta turysta = new Turysta();
                    turysta.Id = long.Parse(temp[1]);
                    turysta.Imie = lines[i + 1];
                    turysta.Nazwisko = lines[i + 2];
                    return turysta;
                }
            }

            return null;
        }

        public List<Turysta> GetAll()
        {
            string[] lines = System.IO.File.ReadAllLines(turystaPath);
            List<Turysta> turysci = new List<Turysta>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    Turysta turysta = new Turysta();
                    turysta.Id = long.Parse(temp[1]);
                    turysta.Imie = lines[i + 1];
                    turysta.Nazwisko = lines[i + 2];
                    turysci.Add(turysta);
                    i += 2;
                }
            }

            return turysci;
        }

        public bool Insert(Turysta model)
        {
            if (Exists(model.Id))
                return false;

            StringWriter modelText = new StringWriter();
            modelText.WriteLine();
            modelText.WriteLine(Utils.KEY_HEADER + " " + model.Id);
            modelText.WriteLine(model.Imie);
            modelText.WriteLine(model.Nazwisko);
            File.AppendAllText(turystaPath, string.Format("{0}{1}", modelText.ToString(), Environment.NewLine));

            return true;
        }

        public bool Update(Turysta model)
        {
            if (!Exists(model.Id))
                return false;

            string[] lines = System.IO.File.ReadAllLines(turystaPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1])==model.Id)
                {
                    lines[i + 1] = model.Imie;
                    lines[i + 2] = model.Nazwisko;
                    i = lines.Length + 1;
                }
            }

            using (var sw = new StreamWriter(turystaPath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }

            return true;
        }

        public bool Delete(Turysta model)
        {
            if (!Exists(model.Id))
                return false;

            string[] lines = System.IO.File.ReadAllLines(turystaPath);
            int begin_index = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1])==model.Id)
                {
                    begin_index = i;
                    i = lines.Length + 1;
                }
            }
            if (begin_index != -1)
            {
                var linesList = new List<string>(lines);
                linesList.RemoveRange(begin_index, 3);

                lines = linesList.ToArray();

                using (var sw = new StreamWriter(turystaPath, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        sw.WriteLine(lines[i]);
                    }
                }

                return true;
            }
            return false;
        }

        public bool Exists(long ID)
        {
            string[] lines = System.IO.File.ReadAllLines(turystaPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    if (ID == long.Parse(temp[1]))
                        return true;
                }
            }

            return false;
        }
    }
}
