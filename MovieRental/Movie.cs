using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental
{
    public class Movie
    {
        private static int ID;
        private string title;
        private string genre;
        private bool isAvailable = true;

        /// <summary>
        /// This is the Movie Constructor
        /// </summary>
        /// <param name="title">title of the movie</param>
        /// <param name="genre">genre of the movie</param>
        /// <param name="availability">the availability of the movie</param>
        public Movie(string title, string genre, bool availability)
        {
            ID++;
            this.title = title;
            this.genre = genre;
            this.isAvailable = availability;
        }

        /// <summary>
        /// This is title property
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// This is genre property
        /// </summary>
        public string Genre
        { 
            get { return genre; } 
            set { genre = value; } 
        }

        /// <summary>
        /// This is availability property
        /// </summary>
        public bool IsAvailable
        {
            get { return isAvailable; } 
            set { isAvailable = value; }
        }

        /// <summary>
        /// This is Overrided ToString for the object to have a pretty print
        /// </summary>
        public override string ToString()
        {
            return $"[ID : {ID}, Title : {Title}, Genre : {Genre}, Is it Available : {IsAvailable}]";
        }
    }
}
