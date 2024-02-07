using TP3Console.Models.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TP3Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exo2Q9();
            Console.ReadKey();
        }

        public static void Exo2Q1()
        {
            var ctx = new FilmsDbContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film + "\n");
            }
        }
        //Autre possibilité :
        public static void Exo2Q1Bis()
        {
            var ctx = new FilmsDbContext();
            //Pour que cela marche, il faut que la requête envoie les mêmes noms de colonnes que les classes c#.
            var films = ctx.Films.FromSqlRaw("SELECT * FROM film");
            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void Exo2Q2()
        {
            var ctx = new FilmsDbContext();

            foreach (var user in ctx.Utilisateurs)
            {
                Console.WriteLine(user.Email);
            }
        }

        public static void Exo2Q3()
        {
            var ctx = new FilmsDbContext();

            foreach (var user in ctx.Utilisateurs.OrderBy(u => u.Login))
            {
                Console.WriteLine(user + "\n");
            }
        }

        public static void Exo2Q4()
        {
            var ctx = new FilmsDbContext();
            var cate = ctx.Categories.First(c => c.Nom == "Action");
            ctx.Entry(cate).Collection(c => c.Films).Load();

            foreach (var film in cate.Films)
            {
                Console.WriteLine(film.Id + "  " + film.Nom + "\n");
            }
        }

        public static void Exo2Q5()
        {
            var ctx = new FilmsDbContext();

            Console.WriteLine("Nombre de catégories : " + ctx.Categories.Count());
        }

        public static void Exo2Q6()
        {
            var ctx = new FilmsDbContext();

            Console.WriteLine("Note la plus basse : " + ctx.Avis.OrderBy(a => a.Note).First().Note);
        }

        public static void Exo2Q7()
        {
            var ctx = new FilmsDbContext();

            var films = ctx.Films.Where(f => f.Nom.ToLower().StartsWith("le"));

            foreach (var film in films)
            {
                Console.WriteLine(film + "\n");
            }
        }

        public static void Exo2Q8()
        {
            var ctx = new FilmsDbContext();

            var film = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction");
            ctx.Entry(film).Collection(f => f.Avis).Load();

            decimal somme = 0;

            foreach (var avis in film.Avis)
            {
                somme += avis.Note;
            }

            Console.WriteLine("Moyenne de 'Pulp Fiction' : " + Math.Round(somme / film.Avis.Count(), 2));
        }

        public static void Exo2Q9()
        {
            var ctx = new FilmsDbContext();

            var user = ctx.Avis.OrderByDescending(a => a.Note).First();
        }
    }
}