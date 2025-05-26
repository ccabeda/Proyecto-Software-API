using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class MapProjectWithStep
    {
        private readonly IUnitOfWorkProjects _unitOfWorkProjects;
        private readonly IMapper _mapper;

        public MapProjectWithStep(IUnitOfWorkProjects unitOfWorkProjects, IMapper mapper)
        {
            _unitOfWorkProjects = unitOfWorkProjects;
            _mapper = mapper;
        }
        public async Task<ProjectProposalCompleteGetDTO> MapProjectWithSteps(ProjectProposal project)
        {
            var dto = _mapper.Map<ProjectProposalCompleteGetDTO>(project);
            var steps = await _unitOfWorkProjects._repositoryProjectApprovalStepQuery.GetByProjectId(project.Id);
            dto.Steps = _mapper.Map<List<ProjectApprovalStepGetDTO>>(steps);
            return dto;
        }
    }
}
