using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace GOT_PTTK.DAO
{
    public class WycieczkaDAO : IDAO<Wycieczka>
    {

        string wycieczkiPath;
        string potwierdzeniaWycieczekPath;

        /// <summary>
        /// Konstruktor parametryczny WycieczkaDAO
        /// </summary>
        /// <param name="databasePath"> ścieżka do bazy danych</param>
        public WycieczkaDAO(string databasePath)
        {
            wycieczkiPath = databasePath + Utils.WYCIECZKI_FILE;
            potwierdzeniaWycieczekPath = databasePath + Utils.POTWIERDZENIA_WYCIECZEK_FILE;
        }

        public Wycieczka Find(long Id)
        {
            string[] lines = System.IO.File.ReadAllLines(wycieczkiPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1])==Id)
                {
                    Wycieczka wycieczka = new Wycieczka();
                    wycieczka.Id = long.Parse(temp[1]);
                    wycieczka.IdCyklu = long.Parse(lines[i + 1]);
                    wycieczka.Nazwa = lines[i + 2];
                    wycieczka.Trasa = lines[i + 3];
                    wycieczka.PunktPoczatkowy = lines[i + 4];
                    wycieczka.DataRozpoczecia = DateTime.Parse(lines[i + 5]);
                    wycieczka.DataZakonczenia = DateTime.Parse(lines[i + 6]);
                    wycieczka.Status = lines[i + 7];
                    wycieczka.ObszarGorski = lines[i + 8];
                    wycieczka.Wysokosc = long.Parse(lines[i + 9]);
                    wycieczka.Dlugosc = long.Parse(lines[i + 10]);
                    wycieczka.Punktacja = int.Parse(lines[i + 11]);

                    string[] lines2 = System.IO.File.ReadAllLines(potwierdzeniaWycieczekPath);
                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && wycieczka.Id == long.Parse(temp2[1]))
                        {
                            wycieczka.NrPrzodownika = long.Parse(lines2[k + 1]);
                            k = lines2.Length;
                        }
                    }

                    return wycieczka;
                }
            }
            return null;
        }

        public List<Wycieczka> GetAll()
        {
            string[] lines = System.IO.File.ReadAllLines(wycieczkiPath);
            List<Wycieczka> wycieczki = new List<Wycieczka>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    Wycieczka wycieczka = new Wycieczka();
                    wycieczka.Id = long.Parse(temp[1]);
                    wycieczka.IdCyklu = long.Parse(lines[i + 1]);
                    wycieczka.Nazwa = lines[i + 2];
                    wycieczka.Trasa = lines[i + 3];
                    wycieczka.PunktPoczatkowy = lines[i + 4];
                    wycieczka.DataRozpoczecia = DateTime.Parse(lines[i + 5]);
                    wycieczka.DataZakonczenia = DateTime.Parse(lines[i + 6]);
                    wycieczka.Status = lines[i + 7];
                    wycieczka.ObszarGorski = lines[i + 8];
                    wycieczka.Wysokosc = long.Parse(lines[i + 9]);
                    wycieczka.Dlugosc = long.Parse(lines[i + 10]);
                    wycieczka.Punktacja = int.Parse(lines[i + 11]);

                    string[] lines2 = System.IO.File.ReadAllLines(potwierdzeniaWycieczekPath);
                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && wycieczka.Id == long.Parse(temp2[1]))
                        {
                            wycieczka.NrPrzodownika = long.Parse(lines2[k + 1]);
                            k = lines2.Length;
                        }
                    }

                    wycieczki.Add(wycieczka);
                    i += 10;
                }
            }

            return wycieczki;
        }

        public bool Insert(Wycieczka model)
        {
            if (Exists(model.Id))
                return false;

            StringWriter modelText = new StringWriter();
            modelText.WriteLine();
            modelText.WriteLine(Utils.KEY_HEADER + " " + model.Id);
            modelText.WriteLine(model.IdCyklu);
            modelText.WriteLine(model.Nazwa);
            modelText.WriteLine(model.Trasa);
            modelText.WriteLine(model.PunktPoczatkowy);
            modelText.WriteLine(model.DataRozpoczecia.ToString(Utils.DATE_FORMAT));
            modelText.WriteLine(model.DataZakonczenia.ToString(Utils.DATE_FORMAT));
            modelText.WriteLine(model.Status);
            modelText.WriteLine(model.ObszarGorski);
            modelText.WriteLine(model.Wysokosc);
            modelText.WriteLine(model.Dlugosc);
            modelText.WriteLine(model.Punktacja);

            if (model.Status!=Utils.STATUS_NIEPOTWIERDZONA_STRING)
            {
                StringWriter potwierdzeniaText = new StringWriter();
                potwierdzeniaText.WriteLine();
                potwierdzeniaText.WriteLine(Utils.KEY_HEADER + " " + model.Id);
                potwierdzeniaText.WriteLine(model.NrPrzodownika);
                File.AppendAllText(potwierdzeniaWycieczekPath, string.Format("{0}{1}", potwierdzeniaText.ToString(), Environment.NewLine));
            }

            File.AppendAllText(wycieczkiPath, string.Format("{0}{1}", modelText.ToString(), Environment.NewLine));

            return true;
        }

        public bool Update(Wycieczka model)
        {
            if (!Exists(model.Id))
                return false;

            string[] lines = System.IO.File.ReadAllLines(wycieczkiPath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1])==model.Id)
                {
                    lines[i + 1] = model.IdCyklu.ToString();
                    lines[i + 2] = model.Nazwa;
                    lines[i + 3] = model.Trasa;
                    lines[i + 4] = model.PunktPoczatkowy;
                    lines[i + 5] = model.DataZakonczenia.ToString(Utils.DATE_FORMAT);
                    lines[i + 6] = model.DataRozpoczecia.ToString(Utils.DATE_FORMAT);
                    lines[i + 7] = model.Status;
                    lines[i + 8] = model.ObszarGorski;
                    lines[i + 9] = model.Wysokosc.ToString();
                    lines[i + 10] = model.Dlugosc.ToString();
                    lines[i + 11] = model.Punktacja.ToString();

                    string[] lines2 = System.IO.File.ReadAllLines(potwierdzeniaWycieczekPath);
                    bool czyZnaleziony = false;
                    int index = -1;

                    for (int k = 0; k < lines2.Length; k++)
                    {
                        string[] temp2 = lines2[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == model.Id)
                        {
                            czyZnaleziony = true;
                            index = k;
                            k = lines2.Length + 1;
                        }
                    }

                    if (czyZnaleziony)
                    {
                        if(model.Status==Utils.STATUS_NIEPOTWIERDZONA_STRING) //Usuwamy z pliku
                        {
                            List<string> lines2List = new List<string>(lines2);
                            lines2List.RemoveRange(index, 2);
                            lines2 = lines2List.ToArray();
                            using (var sw = new StreamWriter(potwierdzeniaWycieczekPath, false))
                            {
                                for (int k = 0; k < lines2.Length; k++)
                                {
                                    sw.WriteLine(lines2[k]);
                                }
                            }
                        }
                    }
                    else 
                    {
                        if(model.Status!=Utils.STATUS_NIEPOTWIERDZONA_STRING)
                        {
                            StringWriter sw = new StringWriter();
                            sw.WriteLine(Utils.KEY_HEADER + " " + model.Id);
                            sw.WriteLine(model.NrPrzodownika);
                            File.AppendAllText(potwierdzeniaWycieczekPath, string.Format("{0}{1}", sw.ToString(), Environment.NewLine));
                        }
                        
                    }

                    i = lines.Length + 11;
                }
            }

            using (var sw = new StreamWriter(wycieczkiPath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }

            return true;
        }

        public bool Delete(Wycieczka model)
        {
            if (!Exists(model.Id))
                return false;

            string[] lines = System.IO.File.ReadAllLines(wycieczkiPath);
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
                linesList.RemoveRange(begin_index, 12);

                lines = linesList.ToArray();

                using (var sw = new StreamWriter(wycieczkiPath, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        sw.WriteLine(lines[i]);
                    }
                }

                string[] lines2 = System.IO.File.ReadAllLines(potwierdzeniaWycieczekPath);
                int index2 = -1;
                for (int k = 0; k < lines2.Length; k++)
                {
                    string[] temp2 = lines2[k].Split(' ');
                    if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && long.Parse(temp2[1]) == model.Id)
                    {
                        index2 = k;
                        k = lines2.Length + 1;
                    }
                }
                if (index2 != -1)
                {
                    List<string> lines2List = new List<string>(lines2);
                    lines2List.RemoveRange(index2, 2);
                    lines2 = lines2List.ToArray();
                    using (var sw = new StreamWriter(potwierdzeniaWycieczekPath, false))
                    {
                        for (int k = 0; k < lines2.Length; k++)
                        {
                            sw.WriteLine(lines2[k]);
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool Exists(long ID)
        {
            string[] lines = System.IO.File.ReadAllLines(wycieczkiPath);

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
