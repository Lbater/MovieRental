using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * TODO:
 * Add a new movie. <- Done
 * Search for movies by title or genre. <- Done
 * Rent a movie (mark it as unavailable). <- Done
 * Return a movie (mark it as available). <- Done
 * Save the movie list to a CSV file. <- Done
 * Load the movie list from a CSV file. <- Done
 * 
 * I'd Also like to take a look at how to maybe put this to a website
 */

namespace MovieRental
{
    public class MovieRentalSystem
    {
        /// <summary>
        /// This Enum is used to help with the searching function
        /// </summary>
        public enum SearchType
        {
            Title,
            Genre
        }

        //This list is the proper list that will have implementation with the CSV files
        private List<Movie> movies = new List<Movie>();

        //Temporary storage will keep this empty when done
        private List<Movie> moviesTesting = new List<Movie>()
        {
            new Movie("Star Wars", "Action", true),
            new Movie("Top Gun: Maverick", "Action", false),
            new Movie("Liar Liar", "Comedy", true),
            new Movie("Terminator", "Action", true),
            new Movie("Journey To The Center Of The Earth", "Adventure", false),
            new Movie("The Exorcist", "Horror", false)
        };

        /// <summary>
        /// This method adds a movie to the list using the Title and Genre
        /// </summary>
        /// <param name="Title">The title of the movie being added</param>
        /// <param name="Genre">The genre of the movie being added</param>
        public void AddMovie(string Title, string Genre)
        {
            var newMovie = new Movie(Title, Genre, true);
            movies.Add(newMovie);
            moviesTesting.Add(newMovie);

            //Console.WriteLine($"Added movie: {newMovie.Title}, {newMovie.Genre}");
        }


        /// <summary>
        /// This uses LINQ to search through the list of movies then add all of them to a list that will be returned
        /// </summary>
        /// <param name="Choice">Whether to search by Title or Genre</param>
        /// <param name="ToG">Stands for TitleOrGenre basically the title or genre you're searching for</param>
        /// <returns>Returns a list with all matching search results</returns>
        public List<Movie> Search(SearchType Choice, string ToG)
        {
            List<Movie> tempList = new List<Movie>();
            switch(Choice)
            {
                case SearchType.Title:
                    var searchedTitle = from movie in movies where movie.Title.Contains(ToG) select movie;
                    foreach (var movie in searchedTitle)
                        tempList.Add(movie);
                    break;
                case SearchType.Genre:
                    var searchedGenre = from movie in movies where movie.Genre == ToG select movie;
                    foreach (var movie in searchedGenre)
                        tempList.Add(movie);
                    break;
            }
            return tempList;
        }

        /// <summary>
        /// This rents a movie if the movie is available in our system
        /// </summary>
        /// <param name="movie">The movie the user wants to rent</param>
        /// <returns>Returns true if the movie is available to rent and false if the either doesn't exist or someone else is renting it</returns>
        public bool RentAMovie(Movie movie)
        {
            var searchedMovie = movies.FirstOrDefault(m => m.Title == movie.Title);

            if (searchedMovie == null || !searchedMovie.IsAvailable)
            {
                return false;
            }

            movie.IsAvailable = true;
            return true;
        }

        /// <summary>
        /// This returns a movie if the movie is not available in our systems
        /// </summary>
        /// <param name="movie">The movie the user wants to return</param>
        /// <returns>Returns true if the movie is not available and false if the movie either doesn't exist or is available</returns>
        public bool ReturnAMovie(Movie movie)
        {
            var searchedMovie = movies.FirstOrDefault(m => m.Title == movie.Title);

            if (searchedMovie == null || searchedMovie.IsAvailable)
            {
                return false;
            }

            searchedMovie.IsAvailable = true;
            return true;
        }

        /// <summary>
        /// This saves all of the new data from the local list to a CSV file
        /// </summary>
        /// <param name="filename">The CSV file you are adding to</param>
        public void SaveToCSV(string filename)
        {
            List<Movie> tempList = LoadFromCSV(filename);

            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                foreach (var movie in movies) // For Testing you can change the "movies" part of the foreach loop into moviesTesting or any other testing variables
                {
                    if (!tempList.Any(m => m.Title.Trim().Equals(movie.Title.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        sw.WriteLine($"{movie.Title}, {movie.Genre}, {movie.IsAvailable}");
                    }
                }
            }
        }

        /// <summary>
        /// This loads the content of a CSV file into a local list
        /// </summary>
        /// <param name="filename">The name of the file that will be searched</param>
        /// <returns>The filled local list</returns>
        public List<Movie> LoadFromCSV(string filename)
        {
            List<Movie> tempMovies = new List<Movie>();

            if (!File.Exists(filename)) return tempMovies;

            using (StreamReader data = new StreamReader(filename))
            {
                string line;
                while ((line = data.ReadLine()) != null)
                {
                    string[] movs = line.Split(',');
                    bool temp = movs[2].Trim().ToLower() == "false" ? false : true;
                    Movie mov = new Movie(movs[0].Trim(), movs[1].Trim(), temp);

                    if (!tempMovies.Any(m => m.Title == mov.Title)) // Prevent overwriting
                    {
                        tempMovies.Add(mov);
                    }
                }
            }
            return tempMovies;
        }

    }
}
