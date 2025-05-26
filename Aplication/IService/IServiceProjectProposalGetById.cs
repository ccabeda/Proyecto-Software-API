using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceProjectProposalGetById
    {
        Task<ProjectProposalCompleteGetDTO?> GetProjectProposalById(Guid id);
    }
}
