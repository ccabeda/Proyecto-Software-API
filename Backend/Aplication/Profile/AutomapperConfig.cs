using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectTypeDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Mapping
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Area, AreaGetDTO>().ReverseMap();

            CreateMap<ProjectApprovalStepCreateDTO, ProjectApprovalStep>().ReverseMap();

            CreateMap<ProjectApprovalStep, ProjectApprovalStepGetDTO>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ApprovalStatus))
           .ForMember(dest => dest.ApproverUser, opt => opt.MapFrom(src => src.User));

            CreateMap<ProjectApprovalStep, ProjectApprovalStepUpdateDTO>().ReverseMap();

            CreateMap<ProjectProposalCreateDTO, ProjectProposal>()
    .ForMember(dest => dest.AreaNavegation, opt => opt.Ignore())
    .ForMember(dest => dest.ProjectType, opt => opt.Ignore())
    .ForMember(dest => dest.User, opt => opt.Ignore())
    .ForMember(dest => dest.ApprovalStatus, opt => opt.Ignore())
    .ForMember(dest => dest.EstimatedAmount, opt => opt.MapFrom(src => src.Amount))
    .ForMember(dest => dest.EstimatedDuration, opt => opt.MapFrom(src => src.Duration));

            CreateMap<ProjectProposal, ProjectProposalCompleteGetDTO>()
    .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.AreaNavegation))
    .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.ProjectType))
    .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => src.ApprovalStatus))
    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.EstimatedAmount))
    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EstimatedDuration))
    .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title));

            CreateMap<ProjectProposal, ProjectProposalGetDTO>()
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.EstimatedAmount))
           .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EstimatedDuration))
           .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.AreaNavegation.Name))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ApprovalStatus.Name))
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProjectType.Name));

            CreateMap<ProjectProposalUpdateDTO, ProjectProposal>()
           .ForMember(dest => dest.EstimatedDuration, opt => opt.MapFrom(src => src.Duration));

            CreateMap<ProjectType, ProjectTypeGetDTO>().ReverseMap();

            CreateMap<ApprovalStatus, ApprovalStatusGetDTO>().ReverseMap();

            CreateMap<ApproverRole, ApproverRoleGetDTO>().ReverseMap();

            CreateMap<User, UserGetDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.ApproverRole));

        }
    }
}
