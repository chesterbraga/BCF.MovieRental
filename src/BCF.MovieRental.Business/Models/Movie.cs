using BCF.MovieRental.Business.Models.Enums;

namespace BCF.MovieRental.Business.Models
{
    public class Movie : Entity
    {
        public string Title { get; set; }        
        public MovieStatus Status { get; set; }
    }
}
