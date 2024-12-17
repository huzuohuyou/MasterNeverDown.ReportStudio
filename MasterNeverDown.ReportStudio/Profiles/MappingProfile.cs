using AutoMapper;
using CommunityToolkit.ReportEditor.Model.Models;
using AakStudio.Shell.UI.Showcase.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ConnectionEditorViewModel, Connection>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Host, opt => opt.MapFrom(src => src.Host))
            .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Database, opt => opt.MapFrom(src => src.Database));
    }
}