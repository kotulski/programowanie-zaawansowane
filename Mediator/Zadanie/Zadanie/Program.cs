using System;
using System.IO;

namespace WzorceProjektowe
{
    // MEDIATOR

    public interface IMediator
    {
        void RealizujOperacje(IOperacjaFinansowa operacja);
    }

    public class Bank : IMediator
    {
        private const string PlikOperacji = "operacje.txt";

        public void RealizujOperacje(IOperacjaFinansowa operacja)
        {
            operacja.Realizuj();
            ZapiszDoPliku(operacja.GetType().Name);
        }

        public void ZapiszDoPliku(string operacja)
        {
            string wpis = $"{DateTime.Now}: Wykonano operację: {operacja}";
            File.AppendAllText(PlikOperacji, wpis + Environment.NewLine);
        }
    }

    public interface IOperacjaFinansowa
    {
        void Realizuj();
    }

    public interface IWyplacalne
    {
        void Wyplac();
    }

    public interface IWplacalne
    {
        void Wplac();
    }

    public class Wplata : IOperacjaFinansowa, IWplacalne
    {
        private readonly IMediator _mediator;

        public Wplata(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Realizuj()
        {
            Wplac();
            Console.WriteLine("Wykonano operację wpłaty.");
        }

        public void Wplac()
        {
            // Tutaj może być logika wpłaty
        }
    }

    public class Wyplata : IOperacjaFinansowa, IWyplacalne
    {
        private readonly IMediator _mediator;

        public Wyplata(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Realizuj()
        {
            Wyplac();
            Console.WriteLine("Wykonano operację wypłaty.");
        }

        public void Wyplac()
        {
            // Tutaj może być logika wypłaty
        }
    }

    // STRATEGIA

    public interface IPodatekStrategia
    {
        decimal ObliczPodatek(decimal kwota);
    }

    public class PodatekPolska : IPodatekStrategia
    {
        public decimal ObliczPodatek(decimal kwota)
        {
            return kwota * 0.23m;
        }
    }

    public class PodatekNiemcy : IPodatekStrategia
    {
        public decimal ObliczPodatek(decimal kwota)
        {
            return kwota * 0.19m;
        }
    }

    public class KalkulatorPodatku
    {
        private readonly IPodatekStrategia _strategia;

        public KalkulatorPodatku(IPodatekStrategia strategia)
        {
            _strategia = strategia;
        }

        public decimal Oblicz(decimal kwota)
        {
            return _strategia.ObliczPodatek(kwota);
        }
    }

    // PROGRAM GŁÓWNY

    class Program
    {
        static void Main(string[] args)
        {
            // Mediator - System Bankowy
            Bank bank = new Bank();

            IOperacjaFinansowa wplata = new Wplata(bank);
            IOperacjaFinansowa wyplata = new Wyplata(bank);

            bank.RealizujOperacje(wplata);
            bank.RealizujOperacje(wyplata);

            Console.WriteLine("Operacje bankowe zakończone.");

            // Strategia - Podatek
            decimal kwota = 1000m;

            KalkulatorPodatku kalkulatorPL = new KalkulatorPodatku(new PodatekPolska());
            Console.WriteLine($"Podatek w Polsce od {kwota} zł: {kalkulatorPL.Oblicz(kwota)} zł");

            KalkulatorPodatku kalkulatorDE = new KalkulatorPodatku(new PodatekNiemcy());
            Console.WriteLine($"Podatek w Niemczech od {kwota} EUR: {kalkulatorDE.Oblicz(kwota)} EUR");

            Console.WriteLine("Obliczanie podatków zakończone.");
        }
    }
}
