using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceApproverRole
{
    public class ServiceApproverRoleGetAll : IserviceApproverRoleGetAll
    {
        private readonly IUnitOfWorkCatalogs _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceApproverRoleGetAll> _logger;

        public ServiceApproverRoleGetAll(IUnitOfWorkCatalogs unitOfWork, IMapper mapper, ILogger<ServiceApproverRoleGetAll> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ApproverRoleGetDTO>?> GetAllApproverRoles()
        {
            try
            {
                var listApproverRoles = await _unitOfWork._repositoryApproverRoleQuery.GetAll();
                if (ControllerHelper.CheckIfListIsNull(listApproverRoles))
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
