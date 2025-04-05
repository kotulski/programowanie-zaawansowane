using System;

public interface IPaczka
{
    void Przygotuj();
}

class MalaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano małą paczkę");
    }
}

class DuzaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano dużą paczkę");
    }
}

class GigantycznaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano gigantyczną paczkę");
    }
}

public interface IFabrykaPaczek
{
    IPaczka UtworzPaczke();
}

class FabrykaDuzychPaczek : IFabrykaPaczek
{
    public IPaczka UtworzPaczke()
    {
        return new DuzaPaczka();
    }
}

class FabrykaMalychPaczek : IFabrykaPaczek
{
    public IPaczka UtworzPaczke()
    {
        return new MalaPaczka();
    }
}

class FabrykaGigantycznychPaczek : IFabrykaPaczek
{
    public IPaczka UtworzPaczke()
    {
        return new GigantycznaPaczka();
    }
}

class ZarzadzaniePaczkami
{
    private IFabrykaPaczek fabrykaPaczek;
    private static ZarzadzaniePaczkami _instancja;

    private ZarzadzaniePaczkami() { }

    public static ZarzadzaniePaczkami Instancja
    {
        get
        {
            if (_instancja == null)
            {
                _instancja = new ZarzadzaniePaczkami();
            }
            return _instancja;
        }
    }

    public void UstawFabryke(IFabrykaPaczek fabryka)
    {
        fabrykaPaczek = fabryka;
    }

    public void PrzygotujPaczke()
    {
        if (fabrykaPaczek == null)
        {
            Console.WriteLine("Nie ustawiono fabryki paczek.");
            return;
        }

        var paczka = fabrykaPaczek.UtworzPaczke();
        paczka.Przygotuj();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var zarzadzanie = ZarzadzaniePaczkami.Instancja;

        zarzadzanie.UstawFabryke(new FabrykaMalychPaczek());
        zarzadzanie.PrzygotujPaczke();

        zarzadzanie.UstawFabryke(new FabrykaDuzychPaczek());
        zarzadzanie.PrzygotujPaczke();

        zarzadzanie.UstawFabryke(new FabrykaGigantycznychPaczek());
        zarzadzanie.PrzygotujPaczke();

        Console.ReadLine();
    }
}
