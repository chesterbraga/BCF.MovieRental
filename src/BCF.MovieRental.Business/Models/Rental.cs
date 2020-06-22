using BCF.MovieRental.Business.Models.Enums;
using System;

namespace BCF.MovieRental.Business.Models
{
    public class Rental : Entity
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public RentalStatus Status { get; set; }
        
        public Customer Customer { get; set; }        
        public Movie Movie { get; set; }
    }
}