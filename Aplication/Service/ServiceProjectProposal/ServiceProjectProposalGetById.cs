using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalGetById : IServiceProjectProposalGetById
    {
        private readonly IUnitOfWorkProjects _unitOfWorkProjects;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly MapProjectWithStep _mapProjectWithStep;

        public ServiceProjectProposalGetById(IUnitOfWorkProjects unitOfWorkProjects, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger, MapProjectWithStep mapProjectWithStep)
        {
            _unitOfWorkProjects = unitOfWorkProjects;
            _logger = logger;
            _mapProjectWithStep = mapProjectWithStep;
        }

        public async Task<ProjectProposalCompleteGetDTO?> GetProjectProposalById(Guid id)
        {
            try
            {
                var projectproposal = await _unitOfWorkProjects._repositoryProjectProposalQuery.GetById(id);
                if (ControllerHelper.CheckIfNull(projectproposal))
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
