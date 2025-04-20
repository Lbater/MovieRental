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

        public Movie(string title, string genre, bool availability)
        {
            ID++;
            this.title = title;
            this.genre = genre;
            this.isAvailable = availability;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Genre
        { 
            get { return genre; } 
            set { genre = value; } 
        }

        public bool IsAvailable
        {
            get { return isAvailable; } 
            set { isAvailable = value; }
        }

        public override string ToString()
        {
            return $"[ID : {ID}, Title : {Title}, Genre : {Genre}, Is it Available : {IsAvailable}]";
        }
    }
}
