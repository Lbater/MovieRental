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
 * Rent a movie (mark it as unavailable).
 * Return a movie (mark it as available).
 * Save the movie list to a CSV file.
 * Load the movie list from a CSV file.
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
            new Movie("Terminator", "Action", true),
            new Movie("Journey To The Center Of The Earth", "Adventure", true),
            new Movie("Liar Liar", "Comedy", true),
            new Movie("The Exorcist", "Horror", false)
        };

        /// <summary>
        /// This method adds a movie to the list using the Title and Genre
        /// </summary>
        /// <param name="Title">The title of the movie being added</param>
        /// <param name="Genre">The genre of the movie being added</param>
        public void AddMovie(string Title, string Genre)
        {
            movies.Add (new Movie (Title, Genre, true));
            moviesTesting.Add (new Movie (Title, Genre, true));
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

        // TODO make good
        //  - I figure just kinda load the file to the list then search for the title if it doesn't exist then append it to the end of the csv file
        public void SaveToCSV(string filename)
        {
            List<Movie> tempList = LoadFromCSV(filename);

            for (int i = 0; i < tempList.Count; i++)
            {
                for (int j = 0; j < moviesTesting.Count; j++)
                {
                    if (!(moviesTesting[j].Title == tempList[i].Title))
                    {
                        // TODO add the ability to write to the CSV
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
            try
            {
                FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);

                if (file == null)
                {
                    Console.WriteLine("File Not Found");
                    return null;
                }

                StreamReader data = new StreamReader(file);

                string line;
                while ((line = data.ReadLine()) != null)
                {
                    string[] movs = line.Split(',');
                    bool temp = true;
                    if (movs[2].Trim() == "false" || movs[2].Trim() == "False")
                        temp = false;
                    Movie mov = new Movie(movs[0].Trim(), movs[1].Trim(), temp);
                    movies.Add(mov);
                }

                data.Close();
                return movies;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}
