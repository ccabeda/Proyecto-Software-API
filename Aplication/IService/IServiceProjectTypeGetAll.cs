using Proyecto_Software_Individual.Aplication.DTOs.ProjectTypeDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceProjectTypeGetAll
    {
        Task<List<ProjectTypeGetDTO>?> GetAllProjectTypes();
    }
}
