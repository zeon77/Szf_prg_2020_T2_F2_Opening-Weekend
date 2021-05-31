using System;

namespace OpeningWeekend
{
    class Film
    {
        public string EredetiCím { get; set; }
        public string MagyarCím { get; set; }
        public DateTime BemutatóDátum { get; set; }
        public string Forgalmazó { get; set; }
        public int Bevétel { get; set; }
        public int LátógatókSzáma { get; set; }

        public Film(string sor)
        {
            string[] splitted = sor.Split(';');
            EredetiCím = splitted[0];
            MagyarCím = splitted[1];
            BemutatóDátum = DateTime.Parse(splitted[2]);
            Forgalmazó = splitted[3];
            Bevétel = int.Parse(splitted[4]);
            LátógatókSzáma = int.Parse(splitted[5]);
        }
    }
}
