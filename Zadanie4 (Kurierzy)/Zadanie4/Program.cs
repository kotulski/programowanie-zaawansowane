using System;

namespace Logistyka
{
    // Interfejsy produktów
    public interface IPaczka
    {
        void Spakuj();
    }

    public interface IKurier
    {
        void Dostarcz();
    }

    // Implementacje produktów
    public class MalaPaczka : IPaczka
    {
        public void Spakuj() => Console.WriteLine("Spakowano MAŁĄ paczkę.");
    }

    public class DuzaPaczka : IPaczka
    {
        public void Spakuj() => Console.WriteLine("Spakowano DUŻĄ paczkę.");
    }

    public class DHLKurier : IKurier
    {
        public void Dostarcz() => Console.WriteLine("Dostarczono przez kuriera DHL.");
    }

    public class UPSKurier : IKurier
    {
        public void Dostarcz() => Console.WriteLine("Dostarczono przez kuriera UPS.");
    }

    // Interfejs Abstract Factory
    public interface IFabrykaLogistyki
    {
        IPaczka UtworzPaczke();
        IKurier UtworzKuriera();
    }

    // Konkretne fabryki
    public class FabrykaLogistykiPolska : IFabrykaLogistyki
    {
        public IPaczka UtworzPaczke() => new MalaPaczka();
        public IKurier UtworzKuriera() => new DHLKurier();
    }

    public class FabrykaLogistykiUSA : IFabrykaLogistyki
    {
        public IPaczka UtworzPaczke() => new DuzaPaczka();
        public IKurier UtworzKuriera() => new UPSKurier();
    }

    // Singleton zarządzający przesyłkami
    public class ZarzadzaniePrzesylkami
    {
        private static ZarzadzaniePrzesylkami _instancja;
        private IFabrykaLogistyki fabrykaLogistyki;

        private ZarzadzaniePrzesylkami() { }

        public static ZarzadzaniePrzesylkami Instancja
        {
            get
            {
                if (_instancja == null)
                    _instancja = new ZarzadzaniePrzesylkami();
                return _instancja;
            }
        }

        // Metoda obsługująca zamówienie
        public void PrzyjmijZamowienie(string lokalizacja) // Funkcja string 
        {
            if (lokalizacja.ToLower() == "polska")
                fabrykaLogistyki = new FabrykaLogistykiPolska();
            else if (lokalizacja.ToLower() == "usa")
                fabrykaLogistyki = new FabrykaLogistykiUSA();
            else
            {
                Console.WriteLine("Nieobsługiwana lokalizacja.");
                return;
            }

            var paczka = fabrykaLogistyki.UtworzPaczke();
            var kurier = fabrykaLogistyki.UtworzKuriera();

            paczka.Spakuj();
            kurier.Dostarcz();
            Console.WriteLine("Zamówienie zrealizowane dla: " + lokalizacja + "\n");
        }
    }

    // Testowanie programu 
    class Program
    {
        static void Main(string[] args)
        {
            var system = ZarzadzaniePrzesylkami.Instancja;

            system.PrzyjmijZamowienie("Polska");  // Mała paczka + DHL
            system.PrzyjmijZamowienie("USA");     // Duża paczka + UPS
            system.PrzyjmijZamowienie("Niemcy");  // Nieobsługiwane

            Console.ReadLine();
        }
    }
}