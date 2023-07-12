using AutoMapper;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.Models;

namespace Report.Application.Mappers;

public class AutoMapperConfigurations : Profile
{
    public AutoMapperConfigurations()
    {
        CreateMap<Product, ProductResponseModel>()
            .ForMember(s => s.Category, opt => opt.MapFrom(f => f.Category != null ? f.Category.Name : null));
        CreateMap<ProductRequestModel, Product>();

        CreateMap<Firm, FirmResponseModel>();
        CreateMap<FirmRequestModel, Firm>();

        CreateMap<Client, ClientResponseModel>();
        CreateMap<ClientRequestModel, Client>();
        CreateMap<Client, GetClientForSelectResponseModel>();

        CreateMap<Category, CategoryResponseModel>();
        CreateMap<CategoryRequestModel, Category>();

        CreateMap<StorageRequestModel, Storage>();
        CreateMap<Storage, StorageResponseModel>();
        
        CreateMap<InvoiceLog, InvoicesLogResponseModel>()
            .ForMember(s=>s.ProductName,opt=>opt.MapFrom(f=> f.Product+""));
        CreateMap<InvoiceRequestModel, InvoiceLog>();

        CreateMap<RestProduct, RestProductResponseModel>()
            .ForMember(s => s.ProductName, opt => opt.MapFrom(f => f.Product.Name))
            .ForMember(s => s.StorageName, opt => opt.MapFrom(f => f.Storage.Name))
            .ForMember(s => s.PriceUsd, opt => opt.MapFrom(f => f.InvoicePriceUsd))
            .ForMember(s => s.PriceTjs, opt => opt.MapFrom(f => f.InvoicePriceTjs))
            .ForMember(s => s.Sum, opt => opt.MapFrom(f => f.InvoicePriceUsd*f.Quantity));

        CreateMap<RestProduct, GetRestsByProductResponseModel>()
            .ForMember(s => s.StorageName, opt => opt.MapFrom(f => f.Storage.Name))
            .ForMember(s => s.PriceUsd, opt => opt.MapFrom(f => f.InvoicePriceUsd))
            .ForMember(s => s.PriceTjs, opt => opt.MapFrom(f => f.InvoicePriceTjs));
    }
}