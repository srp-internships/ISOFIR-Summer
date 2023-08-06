using AutoMapper;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.Models;

namespace Report.Application.Mappers;

public class AutoMapperConfigurations : Profile
{
    public AutoMapperConfigurations()
    {
        CreateMap<Rate, RateResponseModel>();

        CreateMap<ClientCashLog, ClientCashLogResponseModel>();

        CreateMap<ClientCashBoxRequestModel, ClientCashLog>();
        CreateMap<ClientCashLog, ClientCashBoxResponseModel>()
            .ForMember(m => m.ClientName, opt => opt.MapFrom(f => f.Client + ""))
            .ForMember(m => m.CashBoxName, opt => opt.MapFrom(f => f.CashBox + ""));


        CreateMap<ReasonRequestModel, Reason>();
        CreateMap<Reason, ReasonResponseModel>();

        CreateMap<ReasonCashLog, ReasonCashBoxResponseModel>()
            .ForMember(m => m.ReasonName, opt => opt.MapFrom(f => f.Reason + ""))
            .ForMember(m => m.CashBoxName, opt => opt.MapFrom(f => f.CashBox + ""));
        CreateMap<ReasonCashBoxRequestModel, ReasonCashLog>();

        CreateMap<CashBox, CashBoxResponseModel>();
        CreateMap<CashBoxRequestModel, CashBox>();

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
            .ForMember(s => s.ProductName, opt => opt.MapFrom(f => f.Product + ""));
        CreateMap<InvoiceRequestModel, InvoiceLog>();

        CreateMap<RestProduct, RestProductResponseModel>()
            .ForMember(s => s.ProductName, opt => opt.MapFrom(f => f.Product + ""))
            .ForMember(s => s.StorageName, opt => opt.MapFrom(f => f.Storage + ""))
            .ForMember(s => s.PriceUsd, opt => opt.MapFrom(f => f.InvoicePriceUsd))
            .ForMember(s => s.PriceTjs, opt => opt.MapFrom(f => f.InvoicePriceTjs))
            .ForMember(s => s.Sum, opt => opt.MapFrom(f => f.InvoicePriceUsd * f.Quantity));

        CreateMap<RestProduct, GetRestsByProductResponseModel>()
            .ForMember(s => s.StorageName, opt => opt.MapFrom(f => f.Storage + ""))
            .ForMember(s => s.PriceUsd, opt => opt.MapFrom(f => f.InvoicePriceUsd))
            .ForMember(s => s.PriceTjs, opt => opt.MapFrom(f => f.InvoicePriceTjs))
            .ForMember(m => m.RestId, opt => opt.MapFrom(f => f.Id));
        CreateMap<RestProduct, GetRestProductsNameResponseModel>()
            .ForMember(s => s.Name, opt => opt.MapFrom(f => f.Product + ""))
            .ForMember(s => s.Id, opt => opt.MapFrom(f => f.Product.Id));

        CreateMap<SaleRequestModel, SaleLog>();
        CreateMap<SaleLog, SaleLogResponseModel>()
            .ForMember(s => s.Client, opt => opt.MapFrom(f => f.Client + ""))
            .ForMember(s => s.Storage, opt => opt.MapFrom(f => f.RestProduct.Storage))
            .ForMember(s => s.Quantity, opt => opt.MapFrom(f => f.Quantity))
            .ForMember(s => s.SalePriceTjs, opt => opt.MapFrom(f => f.PriceTjs))
            .ForMember(s => s.SalePriceUsd, opt => opt.MapFrom(f => f.PriceUsd));
    }
}