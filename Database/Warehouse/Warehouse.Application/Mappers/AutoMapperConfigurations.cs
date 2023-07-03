using AutoMapper;
using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Mappers;

public class AutoMapperConfigurations : Profile
{
    public AutoMapperConfigurations()
    {
        CreateMap<InvoiceLog, InvoiceHistoryResponseModel>()
            .ForMember(s => s.ProductName, opt => opt.MapFrom(f => f.Rest.Product.Name));
        
        CreateMap<ClientRequestModel, Client>();
        CreateMap<Client, ClientResponseModel>();

        CreateMap<Product, ProductResponseModel>();
        CreateMap<ProductRequestModel, Product>();

        CreateMap<Rest, RestResponseModel>()
            .ForMember(m=>m.ProductName, opt=>opt.MapFrom(f=>f.Product.Name));


        CreateMap<SaleLog, ClientsHistoryResponseModel>()
            .ForMember(m=>m.ProductName,opt=>opt.MapFrom(f=>f.Rest.Product.Name))
            .ForMember(m=>m.ClientName, opt=>opt.MapFrom(m=>m.Client.Name))
            .ForMember(m=>m.InvoicePrice,opt=>opt.MapFrom(f=>f.Rest.Price))
            .ForMember(m=>m.SalePrice,opt=>opt.MapFrom(f=>f.Price));
    }
}