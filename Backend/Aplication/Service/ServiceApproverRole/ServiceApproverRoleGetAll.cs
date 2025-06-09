using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceApproverRole
{
    public class ServiceApproverRoleGetAll : IserviceApproverRoleGetAll
    {
        private readonly IUnitOfWorkApproverRoleQuery _unitOfWorkApproverRoleQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceApproverRoleGetAll> _logger;

        public ServiceApproverRoleGetAll(IUnitOfWorkApproverRoleQuery unitOfWorkApproverRoleQuery, IMapper mapper, ILogger<ServiceApproverRoleGetAll> logger)
        {
            _unitOfWorkApproverRoleQuery = unitOfWorkApproverRoleQuery;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ApproverRoleGetDTO>?> GetAllApproverRoles()
        {
            try
            {
                var listApproverRoles = await _unitOfWorkApproverRoleQuery._repositoryApproverRoleQuery.GetAll();
                if (Helper.CheckIfListIsNull(listApproverRoles))
                {
                    _logger.LogInformation("La lista de roles está vacía.");
                    throw new KeyNotFoundException("No se encontraron roles.");
                }
                return _mapper.Map<List<ApproverRoleGetDTO>>(listApproverRoles);
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener los roles.");
                throw;
            }
        }
    }
}
