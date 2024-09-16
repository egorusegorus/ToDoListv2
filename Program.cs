using System.Threading.Channels;
using System.Xml;
using System.Xml.Serialization;

namespace ToDoList
{
    internal class Program
    { static List<Aufgaben> Tätigkeiten = new List<Aufgaben>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                int wahl = Menu();
                WasJetzt(wahl);
            }
        }

        public static void WasJetzt(int wahl)
        {
            if (wahl == 3) { Environment.Exit(0); }
            else if (wahl == 2) { NeueAufgabeHinZuFügen(wahl); }
            else if (wahl == 1) { ZeigListe(); }
        }

        private static void NeueAufgabeHinZuFügen(int wahl)
        {
            List<Aufgaben> tätigkeiten = new List<Aufgaben>();

            
            if (File.Exists("ToDo.xml"))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Aufgaben>));
                    using (FileStream fileStream = new FileStream("HB.xml", FileMode.Open))
                    {
                        tätigkeiten = (List<Aufgaben>)xmlSerializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Lesen der XML-Datei: {ex.Message}");
                }
            }

            
            int highestId = tätigkeiten.Any() ? tätigkeiten.Max(a => a.id) : 0;

            
            Aufgaben aufgaben = new Aufgaben();
            aufgaben.id = highestId + 1; 

            
            aufgaben.NameDerAufgabe = DatenValidator.ValidiereString("Bitte die Aufgabe beschreiben:");

            string? Ja = DatenValidator.ValidiereString("Ist Aufgabe erledigt (J/N)?");
            if (Ja == "Ja" || Ja == "JA" || Ja == "J" || Ja == "j" || Ja == "ja")
            {
                aufgaben.Erledigt = true;
            }
            else
            {
                aufgaben.Erledigt = false;
            }

            aufgaben.BisWannGültig = DatenValidator.ValidiereDatum("Bis wann die Aufgabe ist gültig (yyyy-MM-dd)");

            
            tätigkeiten.Add(aufgaben);

            
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create("ToDo.xml"))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Aufgaben>));
                    xmlSerializer.Serialize(xmlWriter, tätigkeiten);
                }

                Console.WriteLine("Das Element wurde erfolgreich hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
            }

            wahl = -1;
        }


        private static object ZeigListe()
        {
            if (File.Exists("ToDo.xml"))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Aufgaben>));

                    using (FileStream fileStream = new FileStream("ToDo.xml", FileMode.Open))
                    {
                        List<Aufgaben> deserializedList = (List<Aufgaben>)xmlSerializer.Deserialize(fileStream);

                        Console.WriteLine("\nInhalt der XML-Datei:");

                        foreach (var aufgaben in deserializedList)
                        {
                            Console.WriteLine($"Id: {aufgaben.id}");
                            Console.WriteLine($"Aufgabe: {aufgaben.NameDerAufgabe}");
                            Console.WriteLine($"Erledigt: {aufgaben.Erledigt}");
                            Console.WriteLine($"Bis wann gültig: {aufgaben.BisWannGültig}");

                            
                            var restZeit = aufgaben.BisWannGültig.HasValue
                                ? (aufgaben.BisWannGültig.Value - DateTime.Now).TotalDays
                                : (double?)null;

                            if (restZeit.HasValue && restZeit > 0)
                            {
                                Console.WriteLine($"Rest Zeit: {restZeit.Value} Tage");
                            }
                            else if (restZeit.HasValue && restZeit <= 0)
                            {
                                Console.WriteLine("Aufgabe ist überfällig.");
                            }
                            else
                            {
                                Console.WriteLine("Kein Enddatum festgelegt.");
                            }

                            Console.WriteLine();
                            Console.ReadLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Lesen der XML-Datei: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Die XML-Datei existiert nicht.");
            }
            Console.ReadLine();
            return null;
        }


        public static int Menu()
        {int wahl = -1;
            Console.WriteLine("Herzlich willkomenn in ToDoAnwendung");
            Console.WriteLine("");
            Console.WriteLine("1. Zeig mir Liste mit Aufgaben");
            Console.WriteLine("2. Ich möchte Aufgabe hinzufügen");
            Console.WriteLine("3. Exit");
            wahl = DatenValidator.ValidiereInt("Was möchten Sie tun?");
            return wahl;
        }
    }
}
