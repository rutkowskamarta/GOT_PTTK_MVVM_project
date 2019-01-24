using GOT_PTTK.Utilities;
using System;
using System.Collections.Generic;
using System.IO;


namespace GOT_PTTK.DAO
{
    class PracownikDAO : IDAO<Pracownik>
    {
        string pracownikPath;

        /// <summary>
        /// Konstruktor parametryczny PracownikDAO
        /// </summary>
        /// <param name="databasePath"> ścieżka do bazy danych</param>
        public PracownikDAO(string databasePath)
        {
            pracownikPath = databasePath + Utils.PRACOWNICY_FILE;
        }

        public Pracownik Find(long Id)
        {
            string[] lines = System.IO.File.ReadAllLines(pracownikPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1])==Id)
                {
                    Pracownik pracownik = new Pracownik();
                    pracownik.Numer = long.Parse(temp[1]);
                    pracownik.Imie = lines[i + 1];
                    pracownik.Nazwisko = lines[i + 2];
                    pracownik.DataZatrudnienia = DateTime.ParseExact(lines[i + 3], Utils.DATE_FORMAT, null);
                    pracownik.MiastoTRW = lines[i + 4];
                    pracownik.RodzajPracownika = lines[i + 5];
                    return pracownik;
                }
            }
            return null;
        }

        public List<Pracownik> GetAll()
        {
            string[] lines = System.IO.File.ReadAllLines(pracownikPath);
            List<Pracownik> pracownicy = new List<Pracownik>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    Pracownik pracownik = new Pracownik();
                    pracownik.Numer = long.Parse(temp[1]);
                    pracownik.Imie = lines[i + 1];
                    pracownik.Nazwisko = lines[i + 2];
                    pracownik.DataZatrudnienia = DateTime.ParseExact(lines[i + 3], Utils.DATE_FORMAT, null);
                    pracownik.MiastoTRW = lines[i + 4];
                    pracownik.RodzajPracownika = lines[i + 5];
                    pracownicy.Add(pracownik);
                    i += 5;
                }
            }

            return pracownicy;
        }

        public bool Insert(Pracownik model)
        {
            if (Exists(model.Numer))
                return false;

            StringWriter modelText = new StringWriter();
            modelText.WriteLine();
            modelText.WriteLine(Utils.KEY_HEADER + " " + model.Numer);
            modelText.WriteLine(model.Imie);
            modelText.WriteLine(model.Nazwisko);
            modelText.WriteLine(model.DataZatrudnienia.ToString(Utils.DATE_FORMAT));
            modelText.WriteLine(model.MiastoTRW);
            modelText.WriteLine(model.RodzajPracownika);
            File.AppendAllText(pracownikPath, string.Format("{0}{1}", modelText.ToString(), Environment.NewLine));

            return true;
        }

        public bool Update(Pracownik model)
        {
            if (!Exists(model.Numer))
                return false;

            string[] lines = System.IO.File.ReadAllLines(pracownikPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Numer)
                {
                    lines[i + 1] = model.Imie;
                    lines[i + 2] = model.Nazwisko;
                    lines[i + 3] = model.DataZatrudnienia.ToString(Utils.DATE_FORMAT);
                    lines[i + 4] = model.MiastoTRW;
                    lines[i + 5] = model.RodzajPracownika;
                    i = lines.Length + 1;
                }
            }

            using (var sw = new StreamWriter(pracownikPath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }

            return true;
        }

        public bool Delete(Pracownik model)
        {
            if (!Exists(model.Numer))
                return false;

            string[] lines = System.IO.File.ReadAllLines(pracownikPath);
            int begin_index = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Numer)
                {
                    begin_index = i;
                    i = lines.Length + 1;
                }
            }

            if (begin_index != -1)
            {
                var linesList = new List<string>(lines);
                linesList.RemoveRange(begin_index, 6);

                lines = linesList.ToArray();

                using (var sw = new StreamWriter(pracownikPath, false))
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
            string[] lines = System.IO.File.ReadAllLines(pracownikPath);

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
