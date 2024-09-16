using System;

namespace ToDoList
{
    [Serializable]
    public class Aufgaben
    {
        public int id;
        public string? NameDerAufgabe;
        public bool? Erledigt;
        public DateTime? BisWannGültig;

        public Aufgaben() { }

       
        public string RestTimeOrOverTime
        {
            get
            {
                if (BisWannGültig.HasValue)
                {
                    var diff = (BisWannGültig.Value - DateTime.Now).TotalDays;
                    if (diff > 0)
                        return $"{diff:F2} Tage übrig";
                    else
                        return $"{Math.Abs(diff):F2} Tage überfällig";
                }
                return "Kein Enddatum festgelegt";
            }
        }

        public override string ToString()
        {
            return $"ID: {id}, Die Aufgabe: {NameDerAufgabe}, Erledigt: {Erledigt}, Bis wann ist gültig: {BisWannGültig}, Rest Zeit: {RestTimeOrOverTime}";
        }
    }
}
