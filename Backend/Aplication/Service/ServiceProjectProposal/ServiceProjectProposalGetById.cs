using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalGetById : IServiceProjectProposalGetById
    {
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWorkProjectProposalQuery;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly MapProjectWithStep _mapProjectWithStep;

        public ServiceProjectProposalGetById(IUnitOfWorkProjectProposalQuery unitOfWorkProjectProposalQuery, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger, MapProjectWithStep mapProjectWithStep)
        {
            _unitOfWorkProjectProposalQuery = unitOfWorkProjectProposalQuery;
            _logger = logger;
            _mapProjectWithStep = mapProjectWithStep;
        }

        public async Task<ProjectProposalCompleteGetDTO?> GetProjectProposalById(Guid id)
        {
            try
            {
                var projectproposal = await _unitOfWorkProjectProposalQuery._repositoryProjectProposalQuery.GetById(id);
                if (Helper.CheckIfNull(projectproposal))
                {
                    _logger.LogInformation("La lista de solicitudes de proyectos está vacía.");
                    return null;
                }
                var view = await _mapProjectWithStep.MapProjectWithSteps(projectproposal!);
                return view;
            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al la solicitud de proyecto.");
                throw;
            }
        }
    }
}
