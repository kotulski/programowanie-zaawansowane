using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

[XmlInclude(typeof(Student))]
public class Osoba
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public int Wiek { get; set; }
    public DateTime DataUrodzenia { get; set; }
}

public class Student : Osoba
{
    public string NumerIndeksu { get; set; }
    public string NumerGrupy { get; set; }
}

class Program
{
    static void Main()
    {
        List<Osoba> osoby = new List<Osoba>
        {
            new Osoba { Imie = "Jan", Nazwisko = "Kowalski", Wiek = 30, DataUrodzenia = new DateTime(1994, 5, 10) },
            new Osoba { Imie = "Anna", Nazwisko = "Nowak", Wiek = 25, DataUrodzenia = new DateTime(1999, 7, 20) },
            new Student { Imie = "Adam", Nazwisko = "Wiśniewski", Wiek = 40, DataUrodzenia = new DateTime(1984, 3, 15), NumerIndeksu = "12345", NumerGrupy = "A1" },
            new Student { Imie = "Ewa", Nazwisko = "Krawczyk", Wiek = 22, DataUrodzenia = new DateTime(2002, 9, 5), NumerIndeksu = "54321", NumerGrupy = "B2" }
        };

        // Serializacja do XML
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Osoba>));
        using (FileStream fs = new FileStream("osoby.xml", FileMode.Create))
        {
            xmlSerializer.Serialize(fs, osoby);
        }
        Console.WriteLine("Obiekty zostały zserializowane do pliku 'osoby.xml'.");

        // Serializacja do JSON
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(osoby, options);
        File.WriteAllText("osoby.json", jsonString);
        Console.WriteLine("Obiekty zostały zserializowane do pliku 'osoby.json'.");

        // Deserializacja z XML
        List<Osoba> osobyXml;
        using (FileStream fs = new FileStream("osoby.xml", FileMode.Open))
        {
            osobyXml = (List<Osoba>)xmlSerializer.Deserialize(fs);
        }
        Console.WriteLine("\nDane wczytane z pliku XML:");
        foreach (var osoba in osobyXml)
        {
            Console.WriteLine($"Imię: {osoba.Imie}, Nazwisko: {osoba.Nazwisko}, Wiek: {osoba.Wiek}");
        }

        // Deserializacja z JSON
        string jsonFromFile = File.ReadAllText("osoby.json");
        List<Osoba> osobyJson = JsonSerializer.Deserialize<List<Osoba>>(jsonFromFile);

        Console.WriteLine("\nDane wczytane z pliku JSON:");
        foreach (var osoba in osobyJson)
        {
            Console.WriteLine($"Imię: {osoba.Imie}, Nazwisko: {osoba.Nazwisko}, Wiek: {osoba.Wiek}");
        }
    }
}
