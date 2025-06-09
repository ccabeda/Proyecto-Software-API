using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceProjectProposalGetFiltered
    {
        Task<List<ProjectProposalGetDTO>?> GetProjectProposalFiltered(ProjectProposalFilterDTO filters);
    }
}
