using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental
{
    public enum Choices
    {
        AddMovie,
        SearchMovie,
        RentMovie,
        ReturnMovie,
        SaveMoviesToCSV,
        LoadMoviesFromCSV,
        Exit
    }

    public class Program
    {
        private static void Format()
        {
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.WriteLine("|\t\t\tM E N U                               |");
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.WriteLine("|\t1 - Add a Movie locally                               |");
            Console.WriteLine("|\t2 - Search for a Movie by Genre or Title              |");
            Console.WriteLine("|\t3 - Rent a Movie                                      |");
            Console.WriteLine("|\t4 - Return a Movie                                    |");
            Console.WriteLine("|\t5 - Save the locally stored Movies to the CSV file    |");
            Console.WriteLine("|\t6 - Load the Movies from the CSV locally              |");
            Console.WriteLine("|\t7 - Exit application                                  |");
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.Write("Enter your choice: ");
        }

        static void Main(string[] args)
        {
            string FileName = "MovieList.csv";
            MovieRentalSystem MRS = new MovieRentalSystem();
            /*
            Theses are all just here for testing sakes
            
            Movie movie = new Movie("Test", "Action", true);
            MovieRentalSystem mrs = new MovieRentalSystem();
            mrs.AddMovie("Test", "Memes");

            mrs.SaveToCSV(FileName);

            //foreach (var item in mrs.LoadFromCSV(FileName))
                //Console.WriteLine(item);
            */

            int option = 0;
            while (option != 7)
            {
                Format();
                option = int.Parse(Console.ReadLine()); // Converting whatever is input from a string to an int

                Choices choice = (Choices)(option - 1);

                switch (choice)
                {
                    case Choices.AddMovie:
                        Console.Write("What movie would you like to add (Title, Genre): ");
                        string newMovie = Console.ReadLine();
                        string[] fullMovie = newMovie.Trim().Split(',');
                        string movieTitle = fullMovie[0].Trim();
                        string movieGenre = fullMovie[1].Trim();
                        
                        MRS.AddMovie(movieTitle, movieGenre);
                        break;
                    case Choices.SearchMovie:

                        Console.Write("Please input whether you're searching by Title or Genre then input the Title or Genre that you're looking for (whether your looking for title or genre, the title or genre of the movie your looking for):  ");
                        string searchedMovie = Console.ReadLine();
                        string[] searchedParts = searchedMovie.Trim().Split(',');
                        string searchedChoice = searchedParts[0].Trim(); // searchedChoice is for whether they're looking based on title or genre
                        string searchedToG = searchedParts[1].Trim(); //  searchedToG is for the title or genre of the movie you searched for

                        List<Movie> searched = MRS.Search(searchedChoice, searchedToG); // Searched is returning empty and will deal with later
                        foreach (Movie mov in searched)
                            Console.WriteLine(mov);
                        break;
                    case Choices.RentMovie:
                        Console.Write("What movie are you trying to rent (Title, Genre): ");
                        string rentingMovie = Console.ReadLine();
                        string[] rentingParts = rentingMovie.Trim().Split(',');
                        string rentingTitle = rentingParts[0].Trim();
                        string rentingGenre = rentingParts[1].Trim();

                        Movie rentingMovieObj = new Movie(rentingTitle, rentingGenre, true);
                        
                        MRS.RentAMovie(rentingMovieObj);
                        break;
                    case Choices.ReturnMovie:
                        Console.Write("What movie are you trying to return (Title, Genre): ");
                        string rentedMovie = Console.ReadLine();
                        string[] rentedParts = rentedMovie.Trim().Split(',');
                        string rentedTitle = rentedParts[0].Trim();
                        string rentedGenre = rentedParts[1].Trim();

                        Movie rentedMovieObj = new Movie(rentedTitle, rentedGenre, true);

                        MRS.RentAMovie(rentedMovieObj);
                        break;                    
                    case Choices.SaveMoviesToCSV:
                        MRS.SaveToCSV(FileName);
                        break;
                    case Choices.LoadMoviesFromCSV:
                        MRS.LoadFromCSV(FileName);
                        break;
                    case Choices.Exit:
                        Console.WriteLine("Goodbye!");
                        break;
                }
            }
        }
    }
}