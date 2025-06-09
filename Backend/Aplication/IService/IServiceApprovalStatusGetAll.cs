using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;

namespace Proyecto_Software_Individual.Aplication.IService
{
    public interface IServiceApprovalStatusGetAll
    {
        Task<List<ApprovalStatusGetDTO>?> GetAllApprovalStatuses();
    }
}
