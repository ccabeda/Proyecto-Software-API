using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;
namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceUserGetAll
    {
        Task<List<UserGetDTO>?> GetAllUsers();
    }
}
