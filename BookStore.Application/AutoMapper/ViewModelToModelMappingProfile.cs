using AutoMapper;
using BookStore.Application.ViewModels;
using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AutoMapper
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
        {

            CreateMap<CreateBookVM, Book>()
                 .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForPath(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                 .ForPath(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate))
                 .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForPath(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                 .ForPath(dest => dest.ISBN_10, opt => opt.MapFrom(src => src.ISBN_10))
                 .ForPath(dest => dest.ISBN_13, opt => opt.MapFrom(src => src.ISBN_13))
                 .ForPath(dest => dest.CoverUri, opt => opt.MapFrom(src => src.CoverUri))
                 .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForPath(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                 .ForPath(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryId))
                 .ForPath(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock));

            CreateMap<UpdateBookVM, Book>()
                 .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                 .ForPath(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                 .ForPath(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate))
                 .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForPath(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                 .ForPath(dest => dest.ISBN_10, opt => opt.MapFrom(src => src.ISBN_10))
                 .ForPath(dest => dest.ISBN_13, opt => opt.MapFrom(src => src.ISBN_13))
                 .ForPath(dest => dest.CoverUri, opt => opt.MapFrom(src => src.CoverUri))
                 .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForPath(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                 .ForPath(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
                 .ForPath(dest => dest.IsDiscount, opt => opt.MapFrom(src => src.IsDiscount))
                 .ForPath(dest => dest.DiscountPrice, opt => opt.MapFrom(src => src.DiscountPrice))
                 .ForPath(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID));

            CreateMap<UpdateAddressVM, Address>()
                 .ForPath(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                 .ForPath(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                 .ForPath(dest => dest.City, opt => opt.MapFrom(src => src.City))
                 .ForPath(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                 .ForPath(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                 .ForPath(dest => dest.Number, opt => opt.MapFrom(src => src.Number));

            CreateMap<AddCartElementVM, CartElement>()
                 .ForPath(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                 .ForPath(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.NumberOfBooks));

            CreateMap<UpdateCartItemsCount, CartElement>()
                 .ForPath(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.NumberOfBooks));

            CreateMap<CreateCategoryVM, Category>()
                .ForPath(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));






        }
    }
}
