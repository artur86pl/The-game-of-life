using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zycie2wersja
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuInstrukcje();
            char RozdzielaczLinii = '~';
            char Ramka = 'X';
            string LinieWszystkiePolaczone = WczytywanieLiniaPoLinii(RozdzielaczLinii);
            int WysokoscY = ObliczanieWymiaruY(LinieWszystkiePolaczone, RozdzielaczLinii);
            int SzerokoscX = ObliczanieWymiaruX(LinieWszystkiePolaczone, RozdzielaczLinii);
            bool[,] PoleGry = TworzenieTabeli(LinieWszystkiePolaczone, RozdzielaczLinii, WysokoscY, SzerokoscX);
            bool[,] PoprzedniaRunda;
            do
            {
                PoprzedniaRunda = PoleGry;
                Console.Clear();
                RysowaniePolaGry(PoleGry, Ramka, WysokoscY, SzerokoscX);
                PoleGry = NowaRunda(PoleGry, WysokoscY, SzerokoscX);
                Console.ReadLine();
            } while (PoprzedniaRunda != PoleGry);
            Console.ReadLine();
        }
        private static void MenuInstrukcje()
        {
            Console.WriteLine(
                "Ten program to \"symulacja\" zycia.\n" +
                "Wzależności od ilości życia w sąsiedztwie na polach życie będzie się rodzić bądź umierać.\n"
                );
            NastępnaLinia();
            Console.WriteLine(
                "Wprowadź swoja populacje linia po linii.\n" +
                "Program potraltuje \"1\" jako miejsca zamieszkałe.\n" +
                "Linie puste, \"spacje\" oraz \"0\" zostaną potraktowane jako miejsca puste.\n"
                );
            NastępnaLinia();
            Console.WriteLine(
                "Na przykład \n\n" +
                "11  11\n" +
                "101000\n\n" +
                "0011...\n\n" +
                "Aby zakończyć wpisz coś innego niż \"0\" i \"1\"\n" +
                "Zacznij wprowadzanie tutaj:\n"
                );
        }

        private static void NastępnaLinia()
        {
            Console.WriteLine("Nacisnij Enter");
            Console.ReadLine();
            Console.Clear();
        }
        private static string WczytywanieLiniaPoLinii(char RozdzielaczLinii)
        {
            string ZlepioneLinie = "";
            bool CzyZeraJedynki;
            do
            {
                string BierzacaLinia = Console.ReadLine();
                CzyZeraJedynki = SprawdzenieZeraJedynki(BierzacaLinia);
                if (CzyZeraJedynki)
                {
                    ZlepioneLinie = ZlepioneLinie + BierzacaLinia + RozdzielaczLinii;
                }
            } while (CzyZeraJedynki);
            return ZlepioneLinie;
        }
        private static bool SprawdzenieZeraJedynki(string WierszDoSprawdzenia)
        {
            bool TAKsameZERAjedynki = true;
            char[] znak = WierszDoSprawdzenia.ToCharArray();
            foreach (var item in znak)
            {
                int ZnakASCII = (int)item;
                if (ZnakASCII == (int)'0' || ZnakASCII == (int)'1' || ZnakASCII == (int)' ')
                {

                }
                else
                {
                    TAKsameZERAjedynki = false;
                }
            }
            return TAKsameZERAjedynki;
        }
        private static int ObliczanieWymiaruX(string LinieWszystkiePolaczone, char RozdzielaczLinii)
        {
            int BierzacaLinia = 0;
            int NajdluzszaLinia = 0;
            char[] znaki = LinieWszystkiePolaczone.ToCharArray();
            foreach (var item in znaki)
            {
                BierzacaLinia = BierzacaLinia + 1;
                if ((int)item == (int)RozdzielaczLinii)
                {
                    if (BierzacaLinia > NajdluzszaLinia)
                    {
                        NajdluzszaLinia = BierzacaLinia;
                    }
                    BierzacaLinia = 0;
                }
            }
            return NajdluzszaLinia - 1;
        }
        private static int ObliczanieWymiaruY(string LinieWszystkiePolaczone, char RozdzielaczLinii)
        {
            int LiczbaRozdzielaczy = 0;
            char[] znaki = LinieWszystkiePolaczone.ToCharArray();
            foreach (var item in znaki)
            {
                if ((int)item == (int)RozdzielaczLinii)
                {
                    LiczbaRozdzielaczy = LiczbaRozdzielaczy + 1;
                }
            }
            return LiczbaRozdzielaczy;
        }
        private static bool[,] TworzenieTabeli(string LinieWszystkiePolaczone, char RozdzielaczLinii, int WysokoscY, int SzerokoscX)
        {
            bool[,] tablica = new bool[WysokoscY, SzerokoscX];
            char[] znaki = LinieWszystkiePolaczone.ToCharArray();
            int OdczytaneZnaki = 0;
            int x = 0;
            for (int y = 0; y < WysokoscY; y++)
            {
                if (x == SzerokoscX && (int)znaki[OdczytaneZnaki] == (int)RozdzielaczLinii)
                {
                    OdczytaneZnaki++;
                }

                int IleBrakujeZnakow = SzerokoscX;
                for (x = 0; x < SzerokoscX; x++)
                {

                    if ((int)znaki[OdczytaneZnaki] == (int)'1') ////wpisuje "true" jeżeli w tym miejsu string znajduje się "1"
                    {
                        tablica[y, x] = true;
                        IleBrakujeZnakow--;
                    }
                    if ((int)znaki[OdczytaneZnaki] == (int)'0' || (int)znaki[OdczytaneZnaki] == (int)' ') ////wpisuje "false" jeżeli w tym miejsu string znajduje się "0"
                    {
                        tablica[y, x] = false;
                        IleBrakujeZnakow--;
                    }
                    if ((int)znaki[OdczytaneZnaki] == (int)RozdzielaczLinii) ////uzupełnia (ewentualne) brakujące znaki parametrem "false"
                    {
                        while (IleBrakujeZnakow > 0)
                        {
                            tablica[y, x] = false;
                            x++;
                            IleBrakujeZnakow--;
                        }
                    }
                    OdczytaneZnaki++;
                }
            }
            return tablica;
        }
        private static void RysowaniePolaGry(bool[,] PoleGry, char Ramka, int WysokoscY, int SzerokoscX)
        {
            Console.WriteLine();
            PoziomaBelka(SzerokoscX, Ramka);
            for (int j = 0; j < WysokoscY; j++)
            {
                Console.Write(Ramka + " ");
                for (int i = 0; i < SzerokoscX; i++)
                {
                    if (PoleGry[j, i] == true)
                    {
                        Console.Write('!');
                    }
                    if (PoleGry[j, i] == false)
                    {
                        Console.Write(' ');
                    }
                    Console.Write(' ');
                }
                Console.Write(Ramka + " ");
                Console.WriteLine();
            }
            PoziomaBelka(SzerokoscX, Ramka);
        }
        private static void PoziomaBelka(int IleElementow, char Ramka)
        {
            for (int i = 0; i < IleElementow + 2; i++)
            {
                Console.Write(Ramka + " ");
            }
            Console.WriteLine();
        }
        private static bool[,] NowaRunda(bool[,] PoleGry, int WysokoscY, int SzerokoscX)
        {
            bool[,] NowaTablica = new bool[WysokoscY, SzerokoscX];
            for (int y = 0; y < WysokoscY; y++)
            {
                for (int x = 0; x < SzerokoscX; x++)
                {
                    int sasiedztwa = 0;
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        if (j > -1 && j < WysokoscY)
                        {
                            for (int i = x - 1; i < x + 2; i++)
                            {
                                if (i > -1 && i < SzerokoscX)
                                {
                                    if (x != i || y != j)
                                    {
                                        if (PoleGry[j, i] == true)
                                        {
                                            sasiedztwa++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ///Console.WriteLine(sasiedztwa);
                    if (sasiedztwa == 0 || sasiedztwa == 1) ///umiera z samotnosci
                    {
                        NowaTablica[y, x] = false;
                    }
                    if (sasiedztwa == 2 || sasiedztwa == 3) ///rodzi sie nowy
                    {
                        NowaTablica[y, x] = true;
                    }
                    if (sasiedztwa == 4 || sasiedztwa == 5) /// dla 4 i 5 pole sie nie zmienia
                    {
                        NowaTablica[y, x] = PoleGry[y, x];
                    }
                    if (sasiedztwa == 6 || sasiedztwa == 7 || sasiedztwa == 8) ///umiera z samotnosci
                    {
                        NowaTablica[y, x] = false;
                    }
                }
                Console.WriteLine();
            }
            return NowaTablica;
        }
    }
}
