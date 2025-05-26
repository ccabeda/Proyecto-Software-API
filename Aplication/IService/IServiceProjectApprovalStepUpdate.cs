using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceProjectApprovalStepUpdate
    {
        Task<ProjectProposalCompleteGetDTO> UpdateSteps(Guid projectId, ProjectApprovalStepDesitionDTO ProjectApprovalStep);
    }
}
