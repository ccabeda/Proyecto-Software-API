using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;


namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalCreate : IServiceProjectProposalCreate
    {
        private readonly IUnitOfWorkProjects _unitOfWorkProjects;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly ProjectCreator _projectCreator;
        private readonly MapProjectWithStep _mapProjectWithStep;

        public ServiceProjectProposalCreate(IUnitOfWorkProjects unitOfWorkProjects, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger, ProjectCreator projectCreator, MapProjectWithStep mapProjectWithStep)
        {
            _unitOfWorkProjects = unitOfWorkProjects;
            _mapper = mapper;
            _logger = logger;
            _projectCreator = projectCreator;
            _mapProjectWithStep = mapProjectWithStep;
        }
        public async Task<ProjectProposalCompleteGetDTO?> CreateProjectProposal(ProjectProposalCreateDTO projectProposal)
        {
            try
            {
                if (!await _projectCreator.ValidateProjectProposal(projectProposal))
                {
                    return null;
                }
                var entity = _mapper.Map<ProjectProposal>(projectProposal);
                entity.Status = 1;
                entity.CreateBy = projectProposal.User;
                entity.CreateAt = DateTime.Now;
                await _unitOfWorkProjects._repositoryProjectProposalCommand.Create(entity);
                var pasos = await _projectCreator.GenerateSteps(entity);
                foreach (var paso in pasos)
                {
                    await _unitOfWorkProjects._repositoryProjectApprovalStepCommand.Create(paso);

                }
                await _unitOfWorkProjects.Save();
                var view = await _unitOfWorkProjects._repositoryProjectProposalQuery.GetById(entity.Id);
                var view2 = await _mapProjectWithStep.MapProjectWithSteps(view!);
                return view2;

            }
            catch (Exception)
            {
                _logger.LogError("Ocurrió un error inesperado al crear una solicitud de proyecto.");
                throw;
            }
        }
    }
}

