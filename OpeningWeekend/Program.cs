using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace OpeningWeekend
{
    class Program
    {
        static void Main(string[] args)
        {
            //2. feladat
            List<Film> filmek = new List<Film>();
            foreach (var sor in File.ReadAllLines(@"nyitohetvege.txt").Skip(1))
            {
                filmek.Add(new Film(sor));
            }

            //3. feladat
            Console.WriteLine($"3. feladat: Filmek száma az állományban: {filmek.Count}");

            //4. feladat
            long Összeg = filmek.Where(x => x.Forgalmazó == "UIP").Sum(x => (long)x.Bevétel);
            Console.WriteLine($"4. feladat: UIP Duna Film forgalmazó 1. hetes bevételeinek összege: {Összeg.ToString("C0", CultureInfo.CurrentCulture)}");

            //5. feladat
            Film legtöbbetLátogatottFilm = filmek.OrderByDescending(x => x.LátógatókSzáma).First();
            Console.WriteLine("5. feladat: Legtöbb látogató az első héten:");
            Console.WriteLine($"\tEredeti cím: {legtöbbetLátogatottFilm.EredetiCím}");
            Console.WriteLine($"\tMagyar cím: {legtöbbetLátogatottFilm.MagyarCím}");
            Console.WriteLine($"\tForgalmazó: {legtöbbetLátogatottFilm.Forgalmazó}");
            Console.WriteLine($"\tBevétel az első héten: {legtöbbetLátogatottFilm.Bevétel.ToString("C0", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"\tLátogatók száma: {legtöbbetLátogatottFilm.LátógatókSzáma} fő");

            //6. feladat
            bool van = filmek.Any(x => x.EredetiCím
                                        .ToLower()
                                        .Split(' ')
                                        .All(y => y.ToLower().First() == 'w') &&
                                       x.MagyarCím
                                        .ToLower()
                                        .Split(' ')
                                        .All(y => y.ToLower().First() == 'w'));

            Console.WriteLine($"6. feladat: {(van ? "Ilyen film volt!" : "Ilyen film nem volt!")}");

            //var wFilmek = filmek.Where(x => x.EredetiCím.ToLower().Split(' ').All(y => y.ToLower().First() == 'w') && x.MagyarCím.ToLower().Split(' ').All(y => y.ToLower().First() == 'w'));
            //foreach (var item in wFilmek)
            //{
            //    Console.WriteLine(item.EredetiCím);
            //}

            //7. feladat
            Console.WriteLine("7. feladat: -> stat.csv");
            StreamWriter sw = new StreamWriter(@"stat.csv");
            sw.WriteLine("forgalmazo;filmekSzama");

            filmek.GroupBy(film => film.Forgalmazó)
                .Select(group => new { Forgalmazó = group.Key, FilmekSzáma = group.Count() })
                .ToList()
                .ForEach(x => sw.WriteLine($"{x.Forgalmazó};{x.FilmekSzáma}"));

            sw.Close();

            //8. feladat
            var filmekInterCom = filmek.Where(f => f.Forgalmazó == "InterCom").OrderBy(f => f.BemutatóDátum).ToList();
            int maxDiff = filmekInterCom
                .Take(filmekInterCom.Count() - 1)
                .Select((film, index) => (filmekInterCom[index + 1].BemutatóDátum - film.BemutatóDátum).Days)
                .Max();

            Console.WriteLine($"8. feladat: A leghosszabb időszak két InterCom-os bemutató között: {maxDiff} nap");

            //8. feladat (v2)
            filmekInterCom = filmek.Where(f => f.Forgalmazó == "InterCom").OrderBy(f => f.BemutatóDátum).ToList();
            maxDiff = filmekInterCom
                .Take(filmekInterCom.Count - 1)
                .Zip(filmekInterCom.Skip(1), (first, second) => (second.BemutatóDátum - first.BemutatóDátum).Days)
                .Max();

            Console.WriteLine($"8. feladat (v2): A leghosszabb időszak két InterCom-os bemutató között: {maxDiff} nap");

            Console.ReadKey();
        }
    }
}
