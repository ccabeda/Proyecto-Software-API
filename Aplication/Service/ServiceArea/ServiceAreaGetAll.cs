using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceArea
{
    public class ServiceAreaGetAll : IServiceAreaGetAll
    {
        private readonly IUnitOfWorkCatalogs _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceAreaGetAll> _logger;

        public ServiceAreaGetAll(IUnitOfWorkCatalogs unitOfWork, IMapper mapper, ILogger<ServiceAreaGetAll> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AreaGetDTO>?> GetAllAreas()
        {
            try
            {
                var listArea = await _unitOfWork._repositoryAreaQuery.GetAll();
                if (ControllerHelper.CheckIfListIsNull(listArea))
                {
                    _logger.LogInformation("La lista de áreas está vacía.");
                    throw new KeyNotFoundException("No se encontraron áreas.");
                }
                return _mapper.Map<List<AreaGetDTO>>(listArea);
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener las áreas.");
                throw;
            }
        }
    }
}
