using System;
using System.Collections.Generic;
using System.IO;
using GOT_PTTK.Utilities;
using GOT_PTTK.DAO;

namespace GOT_PTTK.DAO
{
    class PrzodownikDAO : IDAO<Przodownik>
    {
        string przodownikPath;
        string uprawnieniaPath;

        /// <summary>
        /// Konstruktor parametryczny Przodownik
        /// </summary>
        /// <param name="databasePath"> ścieżka do bazy danych</param>
        public PrzodownikDAO(string databasePath)
        {
            przodownikPath = databasePath + Utils.PRZODOWNICY_FILE;
            uprawnieniaPath = databasePath + Utils.OBSZARY_UPRAWNIEN_FILE;
        }

        public Przodownik Find(long Id)
        {
            string[] lines = System.IO.File.ReadAllLines(przodownikPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == Id)
                {
                    Przodownik przodownik = new Przodownik();
                    przodownik.NrPrzodownika = long.Parse(temp[1]);
                    przodownik.Imie = lines[i + 1];
                    przodownik.Nazwisko = lines[i + 2];

                    string[] lines2 = System.IO.File.ReadAllLines(uprawnieniaPath);
                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == przodownik.NrPrzodownika)
                            przodownik.ObszaryUprawnien.Add(lines2[k + 1]);
                    }

                    return przodownik;
                }
            }
            return null;
        }

        public List<Przodownik> GetAll()
        {
            string[] lines = System.IO.File.ReadAllLines(przodownikPath);
            List<Przodownik> pracownicy = new List<Przodownik>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    Przodownik przodownik = new Przodownik();
                    przodownik.NrPrzodownika = long.Parse(temp[1]);
                    przodownik.Imie = lines[i + 1];
                    przodownik.Nazwisko = lines[i + 2];

                    string[] lines2 = System.IO.File.ReadAllLines(uprawnieniaPath);
                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == przodownik.NrPrzodownika)
                            przodownik.ObszaryUprawnien.Add(lines2[k + 1]);
                    }

                    pracownicy.Add(przodownik);
                    i += 3;
                }
            }

            return pracownicy;
        }

        public bool Insert(Przodownik model)
        {
            if (Exists(model.NrPrzodownika))
                return false;

            StringWriter modelText = new StringWriter();
            modelText.WriteLine();
            modelText.WriteLine(Utils.KEY_HEADER + " " + model.NrPrzodownika);
            modelText.WriteLine(model.Imie);
            modelText.WriteLine(model.Nazwisko);

            File.AppendAllText(przodownikPath, string.Format("{0}{1}", modelText.ToString(), Environment.NewLine));

            StringWriter uprawnieniaText = new StringWriter();
            foreach(string obszar in model.ObszaryUprawnien)
            {
                uprawnieniaText.WriteLine();
                uprawnieniaText.WriteLine(model.NrPrzodownika);
                uprawnieniaText.WriteLine(obszar);
            }

            File.AppendAllText(uprawnieniaPath, string.Format("{0}{1}", uprawnieniaText.ToString(), Environment.NewLine));

            return true;
        }

        public bool Update(Przodownik model)
        {
            if (!Exists(model.NrPrzodownika))
                return false;

            string[] lines = System.IO.File.ReadAllLines(przodownikPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.NrPrzodownika)
                {
                    lines[i + 1] = model.Imie;
                    lines[i + 2] = model.Nazwisko;

                    List<string> listaUprawnien = new List<string>(model.ObszaryUprawnien);

                    string[] lines2 = System.IO.File.ReadAllLines(uprawnieniaPath);
                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');

                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == model.NrPrzodownika && listaUprawnien.Contains(lines2[k + 1]))
                            listaUprawnien.Remove(lines2[k + 1]);
                    }

                    StringWriter uprawnieniaText = new StringWriter();
                    foreach (string obszar in listaUprawnien)
                    {
                        uprawnieniaText.WriteLine();
                        uprawnieniaText.WriteLine(model.NrPrzodownika);
                        uprawnieniaText.WriteLine(obszar);
                    }

                    File.AppendAllText(uprawnieniaPath, string.Format("{0}{1}", uprawnieniaText.ToString(), Environment.NewLine));

                    i = lines.Length + 1;
                }
            }

            using (var sw = new StreamWriter(przodownikPath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }

            return true;
        }

        public bool Delete(Przodownik model)
        {
            if (!Exists(model.NrPrzodownika))
                return false;

            string[] lines = System.IO.File.ReadAllLines(przodownikPath);
            int begin_index = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.NrPrzodownika)
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

                using (var sw = new StreamWriter(przodownikPath, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        sw.WriteLine(lines[i]);
                    }
                }

                string[] lines2 = System.IO.File.ReadAllLines(przodownikPath);

                for (int k = 0; k < lines2.Length; k++)
                {
                    string[] temp2 = lines2[k].Split(' ');
                    if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == model.NrPrzodownika)
                    {
                        var lines2List = new List<string>(lines);
                        lines2List.RemoveRange(k, 2);

                        lines2 = lines2List.ToArray();
                    }
                }

                return true;
            }
            return false;
        }

        public bool Exists(long ID)
        {
            string[] lines = System.IO.File.ReadAllLines(przodownikPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && temp[1].Equals(Utils.KEY_HEADER))
                {
                    if (ID == long.Parse(temp[1]))
                        return true;
                }
            }

            return false;
        }
    }
}

