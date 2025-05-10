using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Movie Rental System
1. Add Movie
2. Search Movie
3. Rent Movie
4. Return Movie
5. Save Movies to CSV
6. Load Movies from CSV
7. Exit

 */

namespace MovieRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string FileName = "MovieList.csv";
            Movie movie = new Movie("Test", "Action", true);
            MovieRentalSystem mrs = new MovieRentalSystem();
            mrs.AddMovie("Test", "Memes");

            mrs.SaveToCSV(FileName);

            //foreach (var item in mrs.LoadFromCSV(FileName))
                //Console.WriteLine(item);
        }
    }
}