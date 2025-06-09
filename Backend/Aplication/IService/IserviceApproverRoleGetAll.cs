using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IserviceApproverRoleGetAll
    {
        Task<List<ApproverRoleGetDTO>?> GetAllApproverRoles();
    }
}
