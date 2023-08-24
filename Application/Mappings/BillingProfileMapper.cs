using Application.Endpoints.SalesOrders.Commands;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class BillingProfileMapper : Profile
    {
        public BillingProfileMapper()
        {
            CreateMap<Billing, BillingViewModel>()
                .ReverseMap();
            CreateMap<CloseSalesOrderCommand, Billing>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderHeader, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderHeaderID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ChangeAmount, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedTime, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedTime, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());
        }
    }
}
