using GOT_PTTK.DAO;
using GOT_PTTK.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace GOT_PTTK.DAO
{
    class OdznakaDAO : IDAO<Odznaka>
    {

        string odznakaPath;
        string normyPath;
        string cyklePath;
        string weryfikacjePath;

        /// <summary>
        /// Konstruktor parametryczny OdznakaDAO
        /// </summary>
        /// <param name="databasePath"> ścieżka do bazy danych</param>
        public OdznakaDAO(string databasePath)
        {
            odznakaPath = databasePath + Utils.ODZNAKI_FILE;
            normyPath = databasePath + Utils.NORMY_FILE;
            cyklePath = databasePath + Utils.CYKLE_FILE;
            weryfikacjePath = databasePath + Utils.WERYFIKACJE_CYKLI_FILE;
        }

        public Odznaka Find(long Id)
        {
            string[] lines = System.IO.File.ReadAllLines(cyklePath);
            List<Odznaka> odznaki = new List<Odznaka>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == Id)
                {
                    Odznaka odznaka = new Odznaka();
                    odznaka.Id = long.Parse(temp[1]);
                    odznaka.IdTurysty = long.Parse(lines[i + 2]);
                    odznaka.DataRozpoczecia = DateTime.ParseExact(lines[i + 3], Utils.DATE_FORMAT, null);

                    string[] linesOdznaki = System.IO.File.ReadAllLines(odznakaPath);
                    string idNormy = "";
                    for (int k = 0; k < linesOdznaki.Length; k++)
                    {
                        string[] temp2 = linesOdznaki[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(lines[i + 1]))
                        {
                            idNormy = linesOdznaki[k + 1];
                            odznaka.Stopien = linesOdznaki[k + 2].Equals(Utils.EMPTY_FIELD) ? "" : linesOdznaki[k + 2];
                            odznaka.Rodzaj = linesOdznaki[k + 3].Equals(Utils.EMPTY_FIELD) ? "" : linesOdznaki[k + 3];
                            k = linesOdznaki.Length + 1;
                        }
                    }

                    string[] linesNormy = System.IO.File.ReadAllLines(normyPath);
                    for (int k = 0; k < linesNormy.Length; k++)
                    {
                        string[] temp2 = linesNormy[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(idNormy))
                        {
                            odznaka.MinPkt = int.Parse(linesNormy[k + 1]);
                            k = linesNormy.Length + 1;
                        }
                    }

                    odznaka.CzyPrzyznana = odznaka.CzyZweryfikowana = lines[i + 4].Equals(Utils.TRUE_VALUE_STRING) ? true : false;

                    if (!odznaka.CzyZweryfikowana) //Sprawdźmy czy do weryfikacji
                    {
                        string[] linesWeryfikacje = System.IO.File.ReadAllLines(weryfikacjePath);
                        for (int k = 0; k < linesWeryfikacje.Length; k++)
                        {
                            string[] temp2 = linesWeryfikacje[k].Split(' ');
                            if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(odznaka.Id.ToString()))
                            {
                                if (linesWeryfikacje[k + 2].Equals(Utils.EMPTY_FIELD))
                                    odznaka.CzyDoWeryfikacji = true;
                                k = linesWeryfikacje.Length + 1;
                            }
                        }
                    }
                    return odznaka;
                }
            }
            return null;
        }

        public List<Odznaka> GetAll()
        {
            string[] lines = System.IO.File.ReadAllLines(cyklePath);
            List<Odznaka> odznaki = new List<Odznaka>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER))
                {
                    Odznaka odznaka = new Odznaka();
                    odznaka.Id = long.Parse(temp[1]);
                    odznaka.IdTurysty = long.Parse(lines[i + 2]);
                    odznaka.DataRozpoczecia = DateTime.ParseExact(lines[i + 3], Utils.DATE_FORMAT, null);

                    string[] linesOdznaki = System.IO.File.ReadAllLines(odznakaPath);
                    string idNormy = "";
                    for (int k = 0; k < linesOdznaki.Length; k++)
                    {
                        string[] temp2 = linesOdznaki[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(lines[i + 1]))
                        {
                            idNormy = linesOdznaki[k + 1];
                            odznaka.Stopien = linesOdznaki[k + 2].Equals(Utils.EMPTY_FIELD) ? "" : linesOdznaki[k + 2];
                            odznaka.Rodzaj = linesOdznaki[k + 3].Equals(Utils.EMPTY_FIELD) ? "" : linesOdznaki[k + 3];
                            k = linesOdznaki.Length + 1;
                        }
                    }

                    string[] linesNormy = System.IO.File.ReadAllLines(normyPath);
                    for (int k = 0; k < linesNormy.Length; k++)
                    {
                        string[] temp2 = linesNormy[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(idNormy))
                        {
                            odznaka.MinPkt = int.Parse(linesNormy[k + 1]);
                            k = linesNormy.Length + 1;
                        }
                    }

                    odznaka.CzyPrzyznana = odznaka.CzyZweryfikowana = lines[i + 4].Equals(Utils.TRUE_VALUE_STRING) ? true : false;

                    if (!odznaka.CzyZweryfikowana) //Sprawdźmy czy do weryfikacji
                    {
                        string[] linesWeryfikacje = System.IO.File.ReadAllLines(weryfikacjePath);
                        for (int k = 0; k < linesWeryfikacje.Length; k++)
                        {
                            string[] temp2 = linesWeryfikacje[k].Split(' ');
                            if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && temp2[1].Equals(odznaka.Id.ToString()))
                            {
                                if (linesWeryfikacje[k + 2].Equals(Utils.EMPTY_FIELD))
                                    odznaka.CzyDoWeryfikacji = true;
                                k = linesWeryfikacje.Length + 1;
                            }
                        }
                    }

                    odznaki.Add(odznaka);
                    i += 4;
                }
            }

            return odznaki;
        }

        public bool Insert(Odznaka model)
        {
            if (Exists(model.Id))
                return false;

            StringWriter modelText = new StringWriter();
            modelText.WriteLine();
            modelText.WriteLine(Utils.KEY_HEADER + " " + model.Id);
            modelText.WriteLine(model.IdTurysty);
            string[] linesOdznaki = System.IO.File.ReadAllLines(odznakaPath);
            for (int k = 0; k < linesOdznaki.Length; k++)
            {
                string[] temp2 = linesOdznaki[k].Split(' ');
                if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && linesOdznaki[k + 2].Equals(model.Stopien == "" ? Utils.EMPTY_FIELD : model.Stopien) && linesOdznaki[k + 3].Equals(model.Rodzaj == "" ? Utils.EMPTY_FIELD : model.Rodzaj))
                {
                    modelText.WriteLine(temp2[1]);
                    k = linesOdznaki.Length + 1;
                }
            }
            string data = model.DataRozpoczecia.ToString(Utils.DATE_FORMAT);
            modelText.WriteLine(data);

            if (model.CzyDoWeryfikacji)
            {
                if (model.CzyZweryfikowana)
                {
                    if (model.CzyPrzyznana)
                    {
                        StringWriter sw = new StringWriter();
                        sw.WriteLine(Utils.KEY_HEADER + " " + model.Id);
                        sw.WriteLine("0");
                        sw.WriteLine(DateTime.Now.ToString(Utils.DATE_FORMAT));
                        File.AppendAllText(weryfikacjePath, string.Format("{0}{1}", sw.ToString(), Environment.NewLine));
                    }
                    else
                    {
                        model.CzyZweryfikowana = false;
                        model.CzyDoWeryfikacji = false;
                    }
                }
                else
                {
                    StringWriter sw = new StringWriter();
                    sw.WriteLine(Utils.KEY_HEADER + " " + model.Id);
                    sw.WriteLine("0");
                    sw.WriteLine(DateTime.Now.ToString(Utils.EMPTY_FIELD));
                    File.AppendAllText(weryfikacjePath, string.Format("{0}{1}", sw.ToString(), Environment.NewLine));
                }
            }

            modelText.WriteLine(model.CzyZweryfikowana ? Utils.TRUE_VALUE_STRING : Utils.FALSE_VALUE_STRING);
            File.AppendAllText(cyklePath, string.Format("{0}{1}", modelText.ToString(), Environment.NewLine));

            return true;
        }

        public bool Update(Odznaka model)
        {
            if (!Exists(model.Id))
                return false;

            if (model.CzyPrzyznana)
            {
                string[] lines2 = System.IO.File.ReadAllLines(weryfikacjePath);

                for (int i = 0; i < lines2.Length; i++)
                {
                    string[] temp = lines2[i].Split(' ');
                    if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                    {
                        if (lines2[i + 2].Equals(Utils.EMPTY_FIELD))
                            lines2[i + 2] = DateTime.Now.ToString(Utils.DATE_FORMAT);
                        i = lines2.Length + 1;
                    }
                }

                using (var sw = new StreamWriter(weryfikacjePath, false))
                {
                    for (int i = 0; i < lines2.Length; i++)
                    {
                        sw.WriteLine(lines2[i]);
                    }
                }
            }
            else
            {
                if (model.CzyZweryfikowana)
                {
                    model.CzyZweryfikowana = false;
                    model.CzyDoWeryfikacji = false;

                    string[] lines2 = System.IO.File.ReadAllLines(weryfikacjePath);
                    int index = -1;

                    for (int i = 0; i < lines2.Length; i++)
                    {
                        string[] temp = lines2[i].Split(' ');
                        if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                        {
                            index = i;
                            i = lines2.Length + 1;
                        }
                    }

                    if (index != -1)
                    {
                        var lines2List = new List<string>(lines2);
                        lines2List.RemoveRange(index, 3);
                        lines2 = lines2List.ToArray();
                        using (var sw = new StreamWriter(weryfikacjePath, false))
                        {
                            for (int i = 0; i < lines2.Length; i++)
                            {
                                sw.WriteLine(lines2[i]);
                            }
                        }
                    }
                }
                else if (model.CzyDoWeryfikacji)
                {
                    string[] lines2 = System.IO.File.ReadAllLines(weryfikacjePath);
                    bool czyZnaleziony = false;

                    for (int i = 0; i < lines2.Length; i++)
                    {
                        string[] temp = lines2[i].Split(' ');
                        if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                        {
                            if (!lines2[i + 2].Equals(Utils.EMPTY_FIELD))
                                lines2[i + 2] = Utils.EMPTY_FIELD;
                            czyZnaleziony = true;
                            i = lines2.Length + 1;
                        }
                    }

                    if (czyZnaleziony)
                    {
                        using (var sw = new StreamWriter(weryfikacjePath, false))
                        {
                            for (int i = 0; i < lines2.Length; i++)
                            {
                                sw.WriteLine(lines2[i]);
                            }
                        }
                    }
                    else
                    {
                        StringWriter sw = new StringWriter();
                        sw.WriteLine(Utils.KEY_HEADER + " " + model.Id);
                        sw.WriteLine("0");
                        sw.WriteLine(Utils.EMPTY_FIELD);
                        File.AppendAllText(weryfikacjePath, string.Format("{0}{1}", sw.ToString(), Environment.NewLine));
                    }
                }
            }

            string[] lines = System.IO.File.ReadAllLines(cyklePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                {
                    lines[i] = Utils.KEY_HEADER + " " + model.Id.ToString();
                    lines[i + 1] = model.IdTurysty.ToString();
                    string[] linesOdznaki = System.IO.File.ReadAllLines(odznakaPath);
                    for (int k = 0; k < linesOdznaki.Length; k++)
                    {
                        string[] temp2 = linesOdznaki[k].Split(' ');
                        if (temp2.Length > 0 && temp2[0].Equals(Utils.KEY_HEADER) && linesOdznaki[k + 2].Equals(model.Stopien == "" ? Utils.EMPTY_FIELD : model.Stopien) && linesOdznaki[k + 2].Equals(model.Rodzaj == "" ? Utils.EMPTY_FIELD : model.Rodzaj))
                        {
                            lines[i + 2] = temp2[1];
                            k = linesOdznaki.Length + 1;
                        }
                    }
                    lines[i + 3] = model.DataRozpoczecia.ToString(Utils.DATE_FORMAT);
                    lines[i + 4] = model.CzyZweryfikowana ? Utils.TRUE_VALUE_STRING : Utils.FALSE_VALUE_STRING;
                    i = lines.Length + 1;
                }
            }

            using (var sw = new StreamWriter(cyklePath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }

                return true;
            }
        }

        public bool Delete(Odznaka model)
        {
            if (!Exists(model.Id))
                return false;

            string[] lines = System.IO.File.ReadAllLines(cyklePath);
            int begin_index = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                {
                    begin_index = i;
                    i = lines.Length + 1;
                }
            }
            var linesList = new List<string>(lines);
            linesList.RemoveRange(begin_index, 5);

            lines = linesList.ToArray();

            using (var sw = new StreamWriter(cyklePath, false))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }

            string[] lines2 = System.IO.File.ReadAllLines(weryfikacjePath);
            int index = -1;

            for (int i = 0; i < lines2.Length; i++)
            {
                string[] temp = lines2[i].Split(' ');
                if (temp.Length > 0 && temp[0].Equals(Utils.KEY_HEADER) && long.Parse(temp[1]) == model.Id)
                {
                    index = i;
                    i = lines2.Length + 1;
                }
            }

            if (index != -1)
            {
                var lines2List = new List<string>(lines2);
                lines2List.RemoveRange(index, 3);
                using (var sw = new StreamWriter(weryfikacjePath, false))
                {
                    for (int i = 0; i < lines2.Length; i++)
                    {
                        sw.WriteLine(lines2[i]);
                    }
                }
            }

            return true;
        }

        public bool Exists(long ID)
        {
            string[] lines = System.IO.File.ReadAllLines(cyklePath);

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
