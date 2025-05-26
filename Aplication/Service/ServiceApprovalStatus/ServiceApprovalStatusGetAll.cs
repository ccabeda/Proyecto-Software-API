using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceApprovalStatus
{
    public class ServiceApprovalStatusGetAll : IServiceApprovalStatusGetAll
    {
        private readonly IUnitOfWorkCatalogs _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceApprovalStatusGetAll> _logger;

        public ServiceApprovalStatusGetAll(IUnitOfWorkCatalogs unitOfWork, IMapper mapper, ILogger<ServiceApprovalStatusGetAll> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ApprovalStatusGetDTO>?> GetAllApprovalStatuses()
        {
            try
            {
                var listApprovalStatuses = await _unitOfWork._repositoryApprovalStatusQuery.GetAll();
                if (ControllerHelper.CheckIfListIsNull(listApprovalStatuses))
                {
                    _logger.LogInformation("La lista de estados de aprobación está vacía.");
                    throw new KeyNotFoundException("No se encontraron estados de aprobación.");
                }
                return _mapper.Map<List<ApprovalStatusGetDTO>>(listApprovalStatuses);
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener los estados de aprobación.");
                throw;
            }
        }
    }
}
