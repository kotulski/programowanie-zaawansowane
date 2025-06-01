using System;
using System.Collections.Generic;

public interface IOsoba
{
    void Przyjmij(IOdwiedzajacy odwiedzajacy);
}

public interface IOdwiedzajacy
{
    void Odwiedz(Uczen uczen);
    void Odwiedz(Nauczyciel nauczyciel);
    void Odwiedz(Administrator admin);
}

public static class Konsola
{
    public static void Pokaz(string tekst) => Console.WriteLine(tekst);
    public static void PokazZWcieciem(string tekst) => Console.WriteLine("  " + tekst);
}

public class Uczen : IOsoba
{
    public string Imie { get; }
    public List<int> Oceny { get; }

    public Uczen(string imie, List<int> oceny)
    {
        Imie = imie;
        Oceny = oceny ?? new List<int>();
    }

    public void Przyjmij(IOdwiedzajacy odwiedzajacy)
    {
        odwiedzajacy.Odwiedz(this);
    }
}

public class Nauczyciel : IOsoba
{
    public string Imie { get; }
    public int LiczbaOcen { get; }

    public Nauczyciel(string imie, int liczbaOcen)
    {
        Imie = imie;
        LiczbaOcen = liczbaOcen;
    }

    public void Przyjmij(IOdwiedzajacy odwiedzajacy)
    {
        odwiedzajacy.Odwiedz(this);
    }
}

public class Administrator : IOsoba
{
    public string Imie { get; }
    public List<string> DziennikZdarzen { get; }

    public Administrator(string imie)
    {
        Imie = imie;
        DziennikZdarzen = new List<string>();
    }

    public Administrator(string imie, List<string> logi)
    {
        Imie = imie;
        DziennikZdarzen = logi ?? new List<string>();
    }

    public void DodajZdarzenie(string opis)
    {
        DziennikZdarzen.Add(opis);
    }

    public void Przyjmij(IOdwiedzajacy odwiedzajacy)
    {
        odwiedzajacy.Odwiedz(this);
    }
}

public class WypiszRaport : IOdwiedzajacy
{
    public void Odwiedz(Uczen uczen)
    {
        if (uczen.Oceny.Count == 0)
        {
            Konsola.Pokaz($"Uczeń {uczen.Imie} – Brak ocen.");
            return;
        }

        double suma = 0;
        foreach (var ocena in uczen.Oceny)
        {
            suma += ocena;
        }

        double srednia = suma / uczen.Oceny.Count;
        Konsola.Pokaz($"Uczeń {uczen.Imie} – Średnia ocen: {srednia:0.00}");
    }

    public void Odwiedz(Nauczyciel nauczyciel)
    {
        var tekst = string.Format("Nauczyciel {0} – Wystawił ocen: {1}", nauczyciel.Imie, nauczyciel.LiczbaOcen);
        Konsola.Pokaz(tekst);
    }

    public void Odwiedz(Administrator admin)
    {
        Konsola.Pokaz($"Administrator {admin.Imie} – Logi systemowe:");
        if (admin.DziennikZdarzen.Count > 0)
        {
            foreach (var wpis in admin.DziennikZdarzen)
            {
                Konsola.PokazZWcieciem("- " + wpis);
            }
        }
        else
        {
            Konsola.PokazZWcieciem("- Brak logów systemowych.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        var ZbiorOsob = new List<IOsoba>
        {
            new Uczen("Kasia", new List<int> { 5, 4, 3 }),
            new Uczen("Marek", new List<int>()),
            new Nauczyciel("Jan Kowalski", 142),
            new Administrator("Admin1", new List<string> { "Zalogowano użytkownika", "Zmieniono hasło" }),
            new Administrator("Admin2", new List<string>()),
            new Administrator("Admin3")
        };

        // Dodaj przykładowe zdarzenia do Admin3
        if (ZbiorOsob[5] is Administrator admin3)
        {
            admin3.DodajZdarzenie("Dodano użytkownika");
            admin3.DodajZdarzenie("Zmieniono uprawnienia");
        }

        var raport = new WypiszRaport();

        foreach (var osoba in ZbiorOsob)
        {
            osoba.Przyjmij(raport);
        }
    }
}
