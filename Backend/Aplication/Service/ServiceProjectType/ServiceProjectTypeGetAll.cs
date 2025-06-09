using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectTypeDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectType
{
    public class ServiceProjectTypeGetAll : IServiceProjectTypeGetAll
    {
        private readonly IUnitOfWorkProjectTypeQuery _unitOfWorkProjectTypeQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProjectTypeGetAll> _logger;

        public ServiceProjectTypeGetAll(IUnitOfWorkProjectTypeQuery unitOfWorkProjectTypeQuery, IMapper mapper, ILogger<ServiceProjectTypeGetAll> logger)
        {
            _unitOfWorkProjectTypeQuery = unitOfWorkProjectTypeQuery;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProjectTypeGetDTO>?> GetAllProjectTypes()
        {
            try
            {
                List<ProjectType> listProjectType = await _unitOfWorkProjectTypeQuery._repositoryProjectTypeQuery.GetAll();
                if (Helper.CheckIfListIsNull(listProjectType))
                {
                    _logger.LogInformation("La lista de tipos de proyectos está vacía.");
                    throw new KeyNotFoundException("No se encontraron tipos de proyectos.");
                }
                return _mapper.Map<List<ProjectTypeGetDTO>>(listProjectType);
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al traer todos los tipos de proyectos.");
                throw;
            }
        }
    }
}
