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
                 .ForMember(dest => dest.CoverUri, opt => opt.MapFrom(src => src.CoverUri))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
                 .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.Sold))
                 .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
                 .ForMember(dest => dest.IsDiscount, opt => opt.MapFrom(src => src.IsDiscount))
                 .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src => src.DiscountPrice));

            CreateMap<Book, BooksWithoutDetailsVM>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.CoverUri, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.IsDiscount, opt => opt.MapFrom(src => src.IsDiscount))
                .ForMember(dest => dest.DisPrice, opt => opt.MapFrom(src => src.DiscountPrice))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<Address, AddressVM>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.LastEdit, opt => opt.MapFrom(src => src.LastEdit))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number));

            CreateMap<CartElement, CartVM>()
                .ForMember(dest => dest.CartElementID, opt => opt.MapFrom(src => src.CartElementId))
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.NumberOfBooks))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.RealPrice()*src.NumberOfBooks));

            CreateMap<OrderDetail, OrderDetailsVM>()
                .ForMember(dest => dest.BootTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.NumberOfBooks))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.RealPrice()*src.NumberOfBooks));

            CreateMap<Order, OrderVM>()
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
               .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
               .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
               .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
               .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
               .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
               .ForMember(dest => dest.IsShipped, opt => opt.MapFrom(src => src.IsShipped));

            CreateMap<Order, OrderWithDetailsVM>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
               .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
               .ForMember(dest => dest.PostalCodeNCity, opt => opt.MapFrom(src => src.PostalCode + " " + src.City))
               .ForMember(dest => dest.StreetWithNumber, opt => opt.MapFrom(src => src.Street + " " + src.Number))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
               .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
               .ForMember(dest => dest.IsShipped, opt => opt.MapFrom(src => src.IsShipped))
               .AfterMap((s, d) =>
               {
                   //prepare lists
                   var details = new List<OrderDetailsVM>();

                   //map tags
                   foreach (var item in s.OrderDetails)
                   {
                       var detail = new OrderDetailsVM()
                       {
                           BootTitle = item.Book.Title,
                           NumberOfBooks = item.NumberOfBooks,
                           Price = item.TotalPriceSet(),
                       };
                       details.Add(detail);
                   }

                   d.OrderDetailsVM = details;

               });


            CreateMap<AppRoles, RoleVM>()
               .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));

            CreateMap<AppUser, UserVM>()
               .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate));

        }
    }
}
