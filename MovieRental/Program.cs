using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string FileName = "MovieList - Copy.csv";
            Movie movie = new Movie("Test", "Action", true);
            MovieRentalSystem mrs = new MovieRentalSystem();

            foreach (var item in mrs.LoadFromCSV(FileName))
                Console.WriteLine(item);
        }
    }
}