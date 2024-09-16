using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class DatenValidator
    {


        public static string ValidiereString(string prompt)
        {
            string? eingabe;
            do
            {
                Console.WriteLine(prompt);
                eingabe = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(eingabe))
                {
                    return eingabe;
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie einen gültigen Text ein.");
                }
            } while (true);
        }


        public static int ValidiereInt(string prompt)
        {
            int zahl;
            do
            {
                Console.WriteLine(prompt);
                string? eingabe = Console.ReadLine();

                if (int.TryParse(eingabe, out zahl))
                {
                    return zahl;
                }
                else
                {
                    Console.WriteLine("Ungültige Zahl. Bitte geben Sie eine gültige Ganzzahl ein.");
                }
            } while (true);
        }


        public static DateTime ValidiereDatum(string prompt)
        {
            DateTime datum;
            do
            {
                Console.WriteLine(prompt);
                string? eingabe = Console.ReadLine();

                if (DateTime.TryParseExact(eingabe, "yyyy-MM-dd", null, DateTimeStyles.None, out datum))
                {
                    return datum;
                }
                else
                {
                    Console.WriteLine("Ungültiges Datum. Bitte verwenden Sie das Format yyyy-MM-dd.");
                }
            } while (true);
        }
    }
    /* Save to xml HB.xml
    try
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create("HB"))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HistorischeBahn>));
                        xmlSerializer.Serialize(xmlWriter, lstHB);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Speichern der Daten: {ex.Message}");
                }

        Console.WriteLine("Das Element wurde erfolgreich hinzugefügt.");
    */
}
