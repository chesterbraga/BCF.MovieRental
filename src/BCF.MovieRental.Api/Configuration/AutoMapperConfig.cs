using AutoMapper;
using BCF.MovieRental.Api.ViewModels;
using BCF.MovieRental.Business.Models;

namespace BCF.MovieRental.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Customer, Customer1ViewModel>().ReverseMap();

            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Movie, Movie1ViewModel>().ReverseMap();

            CreateMap<RentalViewModel, Rental>();
            CreateMap<Rental1ViewModel, Rental>();

            CreateMap<Rental, RentalViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title));                
        }
    }
}