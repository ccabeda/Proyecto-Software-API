using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalGetFiltered : IServiceProjectProposalGetFiltered
    {
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWorkProjectProposalQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly ProjectFilters _filter;

        public ServiceProjectProposalGetFiltered(IUnitOfWorkProjectProposalQuery unitOfWorkProjectProposalQuery, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger, ProjectFilters filter)
        {
            _unitOfWorkProjectProposalQuery = unitOfWorkProjectProposalQuery;
            _mapper = mapper;
            _logger = logger;
            _filter = filter;
        }

        public async Task<List<ProjectProposalGetDTO>?> GetProjectProposalFiltered(ProjectProposalFilterDTO filtersDTO)
        {
            try
            {
                var allProposals = await _unitOfWorkProjectProposalQuery._repositoryProjectProposalQuery.GetAll();
                if (Helper.CheckIfListIsNull(allProposals))
                {
                    _logger.LogInformation("La lista de solicitudes de proyectos está vacía.");
                    return null;
                }
                var query = await _filter.ApplyFilters(allProposals.AsQueryable(), filtersDTO);

                return _mapper.Map<List<ProjectProposalGetDTO>>(query.ToList());
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al buscar las solicitudes de proyecto.");
                throw;
            }
        }
    }
}
