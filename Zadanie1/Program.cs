public interface INPC
{
    void PrzedstawSie();
}

public class Wojownik : INPC
{
    public void PrzedstawSie() => Console.WriteLine("Jestem Wojownikiem, walczę mieczem");
}

public class Mag : INPC
{
    public void PrzedstawSie() => Console.WriteLine("Jestem Magiem, władającym magią żywiołów");
}

public class Zlodziej : INPC
{
    public void PrzedstawSie() => Console.WriteLine("Jestem Złodziejem, nie mam atrybutów");
}

public interface IFabrykaNPC
{
    INPC StworzNPC();
}

public class FabrykaWojownika : IFabrykaNPC
{
    public INPC StworzNPC() => new Wojownik();
}

public class FabrykaMaga : IFabrykaNPC
{
    public INPC StworzNPC() => new Mag();
}

public class FabrykaZlodzieja : IFabrykaNPC
{
    public INPC StworzNPC() => new Zlodziej();
}

class Program
{
    static void Main()
    {
        Random rand = new Random();
        int losowaPostac = rand.Next(3);

        IFabrykaNPC fabryka;

        switch(losowaPostac)
        {
            case 0: fabryka = new FabrykaWojownika(); break;
            case 1: fabryka = new FabrykaMaga(); break;
            default: fabryka = new FabrykaZlodzieja(); break;
        }

        INPC npc = fabryka.StworzNPC();
        npc.PrzedstawSie();
    }
}