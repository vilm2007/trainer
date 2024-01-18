using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string fileName = "mozgas.txt";

        while (true)
        {
            Console.WriteLine("1. Új adat rögzítése");
            Console.WriteLine("2. Meglévő adatok kiíratása táblázatszerűen");
            Console.WriteLine("3. Meglévő adatok kiíratása adott edzéstípusra szűrve");
            Console.WriteLine("4. Kilépés");
            Console.Write("Válasszon egy opciót (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UjAdatRogzitese(fileName);
                    break;

                case "2":
                    MeglevoAdatokKiiratasa(fileName);
                    break;

                case "3":
                    AdottEdzestipusraSzurvesKiiras(fileName);
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Érvénytelen választás. Kérem, válasszon újra.");
                    break;
            }
        }
    }

    static void UjAdatRogzitese(string fileName)
    {
        Console.Write("Adja meg a dátumot (éééé.hh.nn formátumban): ");
        string datum = Console.ReadLine();

        Console.Write("Adja meg az edzés típusát: ");
        string edzesTipus = Console.ReadLine();

        Console.Write("Adja meg az időtartamot (percben): ");
        int idotartam;
        while (!int.TryParse(Console.ReadLine(), out idotartam) || idotartam <= 0)
        {
            Console.WriteLine("Érvénytelen időtartam. Kérem, adjon meg egy érvényes pozitív egész számot.");
        }

        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine($"{datum}\t{edzesTipus}\t{idotartam}");
        }

        Console.WriteLine("Adatok sikeresen rögzítve.");
    }

    static void MeglevoAdatokKiiratasa(string fileName)
    {
        Console.WriteLine("Meglévő adatok kiíratása táblázatszerűen:");

        Console.WriteLine("Dátum\t\tEdzés típusa\tIdőtartam (perc)");

        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('\t');
                Console.WriteLine($"{data[0]}\t{data[1]}\t\t{data[2]}");
            }
        }
    }

    static void AdottEdzestipusraSzurvesKiiras(string fileName)
    {
        Console.Write("Adja meg az edzéstípust: ");
        string keresettEdzesTipus = Console.ReadLine();

        Console.WriteLine($"Meglévő adatok kiíratása {keresettEdzesTipus} edzéstípusra szűrve:");

        Console.WriteLine("Dátum\t\tEdzés típusa\tIdőtartam (perc)");

        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('\t');
                if (data.Length == 3 && data[1].Equals(keresettEdzesTipus, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{data[0]}\t{data[1]}\t\t{data[2]}");
                }
            }
        }
    }
}