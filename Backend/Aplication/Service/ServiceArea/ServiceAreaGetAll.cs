using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceArea
{
    public class ServiceAreaGetAll : IServiceAreaGetAll
    {
        private readonly IUnitOfWorkAreaQuery _unitOfWorkAreaQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceAreaGetAll> _logger;

        public ServiceAreaGetAll(IUnitOfWorkAreaQuery unitOfWorkAreaQuery, IMapper mapper, ILogger<ServiceAreaGetAll> logger)
        {
            _unitOfWorkAreaQuery = unitOfWorkAreaQuery;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AreaGetDTO>?> GetAllAreas()
        {
            try
            {
                var listArea = await _unitOfWorkAreaQuery._repositoryAreaQuery.GetAll();
                if (Helper.CheckIfListIsNull(listArea))
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
