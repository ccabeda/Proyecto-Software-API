using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceUser
{
    public class ServiceUserGetAll : IServiceUserGetAll
    {
        private readonly IUnitOfWorkUsers _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceUserGetAll> _logger;

        public ServiceUserGetAll(IUnitOfWorkUsers unitOfWork, IMapper mapper, ILogger<ServiceUserGetAll> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserGetDTO>?> GetAllUsers()
        {
            try
            {
                var listUsers = await _unitOfWork._repositoryUserQuery.GetAll();
                if (ControllerHelper.CheckIfListIsNull(listUsers))
                {
                    _logger.LogInformation("La lista de usuarios está vacía.");
                    throw new KeyNotFoundException("No se encontraron usuarios.");
                }
                return _mapper.Map<List<UserGetDTO>>(listUsers);
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al obtener los usuarios.");
                throw;
            }
        }
    }
}
