using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class MapProjectWithStep
    {
        private readonly IUnitOfWorkProjectApprovalStepQuery _unitOfWorkProjectApprovalStepQuery;
        private readonly IMapper _mapper;

        public MapProjectWithStep(IUnitOfWorkProjectApprovalStepQuery unitOfWorkProjectApprovalStepQuery, IMapper mapper)
        {
            _unitOfWorkProjectApprovalStepQuery = unitOfWorkProjectApprovalStepQuery;
            _mapper = mapper;
        }
        public async Task<ProjectProposalCompleteGetDTO> MapProjectWithSteps(ProjectProposal project)
        {
            var projectProposalDTO = _mapper.Map<ProjectProposalCompleteGetDTO>(project);
            var steps = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetByProjectId(project.Id);
            projectProposalDTO.Steps = _mapper.Map<List<ProjectApprovalStepGetDTO>>(steps);
            return projectProposalDTO;
        }
    }
}
