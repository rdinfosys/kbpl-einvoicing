using AutoMapper;
using System;

namespace KBPL.Mapper
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            //CreateMap<UserDetailViewModel, User>();
            //CreateMap<CylinderDetailsRequestModel, CylinderDetails>();
            //CreateMap<UnitCommLossDetailsRequestModel, Units>();
            //CreateMap<UserRequestModel, User>().ForMember(dest => dest.ExpirationDate, opr => opr.MapFrom(src => DateTime.Parse(src.ExpirationDate)));
            //CreateMap<EventPartyDetailsRequestModel, FaultCodes>();
            //CreateMap<PartyDetailsRequestModel, EventParty>();
            //CreateMap<DashboardShutdownUnitsRequestModel, UnitListRequestModel>()
            //    .ForMember(dest => dest.Regions, opr => opr.MapFrom(src => src.Region))
            //    .ForMember(dest => dest.Workzones, opr => opr.MapFrom(src => src.Workzone))
            //    .ForMember(dest => dest.Customers, opr => opr.MapFrom(src => src.Customer))
            //    .ForMember(dest => dest.Units, opr => opr.MapFrom(src => src.Unit))
            //    .ForMember(dest => dest.UnitTypes, opr => opr.MapFrom(src => src.UnitTypes))
            //    .ForMember(dest => dest.ContractStatus, opr => opr.MapFrom(src => src.ContractStatus));
        }
    }
}