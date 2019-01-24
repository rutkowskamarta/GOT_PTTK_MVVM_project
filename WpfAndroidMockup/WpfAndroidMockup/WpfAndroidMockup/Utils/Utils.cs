namespace GOT_PTTK.Utilities
{
    /// <summary>
    /// Statyczna klasa odpowiedzialna za przechowywanie ścieżek do bazy i danych zalogowanych użytkowników
    /// </summary>
    public static class Utils
    {
        public const string KEY_HEADER = "ID";
        public const string EMPTY_FIELD = "NULL";
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public const string TRUE_VALUE_STRING = "TRUE";
        public const string FALSE_VALUE_STRING = "FALSE";

        public const string STATUS_NIEPOTWIERDZONA_STRING = "NIEPOTWIERDZONA";
        public const string STATUS_POTWIERDZONA_STRING = "POTWIERDZONA";
        public const string STATUS_WTRAKCIE_STRING = "W TRAKCIE";

        public const string CZAS_WYCIECZKI_FORMAT = "{0} h {1} m";

        public static string BAZA_DANYCH_PATH = @"C:\Users\marta\Desktop\GitHub\GOT_PTTK_rest_project\BazaDanych\";
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
        public static long ID_ZALOGOWANEGO_PRZODOWNIKA = 0;
        public static long ID_ZALOGOWANEGO_PRACOWNIKA = 0;
    }
}
