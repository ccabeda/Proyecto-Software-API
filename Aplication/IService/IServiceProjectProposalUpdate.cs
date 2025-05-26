using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceProjectProposalUpdate
    {
        Task<ProjectProposalCompleteGetDTO?> UpdateProjectProposal(Guid id, ProjectProposalUpdateDTO ProjectProposal);
    }
}
