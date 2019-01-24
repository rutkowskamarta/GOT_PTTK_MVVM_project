using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAndroidMockup.Models
{
    /// <summary>
    /// Statyczna klasa odpowiedzialna za przechowywanie danych logowania
    /// </summary>
    public static class Utils
    {

        public static string BAZA_DANYCH_PATH = "C:\\Users\\Fryderyk\\Desktop\\BazaDanych\\";
        public static string TURYSCI_FILE = "Turysci.txt";
        public static string CYKLE_FILE = "Cykle.txt";
        public static string NORMY_FILE = "Normy.txt";
        public static string OBSZARY_UPRAWNIEN_FILE = "ObszaryUprawnień.txt";
        public static string ODZNAKI_FILE = "Odznaki.txt";
        public static string POTWIERDZENIA_WYCIECZEK_FILE = "PotwierdzeniaWycieczek.txt";
        public static string PRACOWNICY_FILE = "Pracownicy.txt";
        public static string PRZODOWNICY_FILE = "Przodownicy.txt";
        public static string WERYFIKACJE_CYKLI_FILE = "WeryfikacjeCykli.txt";
        public static string WYCIECZKI_FILE = "Wycieczki.txt";

        public static long ID_ZALOGOWANEGO_TURYSTY = 0;
        public static long ID_ZALOGOWANEGO_PRZODOWNIKA = 1;
        public static long ID_ZALOGOWANEGO_PRACOWNIKA = 0;
    }
}
