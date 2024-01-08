using AutoMapper;
using Edunext_Model.DTOs.Cart;
using Edunext_Model.DTOs.Product;
using Edunext_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunext_Model.Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, CartOrderDTO>().ReverseMap();
            CreateMap<OrderDetail, CartOrderDetailDTO>().ReverseMap();
            CreateMap<Product, ProductPost>().ReverseMap();
            CreateMap<Product, ProductGet>().ReverseMap();
            CreateMap<Product, ProductPut>().ReverseMap();
            CreateMap<Category, CategoryGet>().ReverseMap();

            // Additional mappings here...
        }
    }
}
