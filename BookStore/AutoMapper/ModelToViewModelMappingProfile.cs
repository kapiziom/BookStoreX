using AutoMapper;
using BookStore.Domain;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.AutoMapper
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<Book, BooksDetailsVM>()
                 .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                 .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.ToShortDateString()))
                 .ForMember(dest => dest.AddedToStore, opt => opt.MapFrom(src => src.AddedToStore.ToShortDateString()))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                 .ForMember(dest => dest.ISBN_10, opt => opt.MapFrom(src => src.ISBN_10))
                 .ForMember(dest => dest.ISBN_13, opt => opt.MapFrom(src => src.ISBN_13))
                 .ForMember(dest => dest.CoverUri, opt => opt.MapFrom(src => src.CoverUri));
        }
    }
}
