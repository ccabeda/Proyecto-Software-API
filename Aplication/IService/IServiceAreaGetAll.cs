using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceAreaGetAll
    {
        Task<List<AreaGetDTO>?> GetAllAreas();
    }
}
