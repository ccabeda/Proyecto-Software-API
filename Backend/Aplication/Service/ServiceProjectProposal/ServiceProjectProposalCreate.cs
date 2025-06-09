using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;


namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalCreate : IServiceProjectProposalCreate
    {
        private readonly IUnitOfWorkProjectProposalCommand _unitOfWorkProjectProposalCommand;
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWorkProjectProposalQuery;
        private readonly IUnitOfWorkProjectApprovalStepCommand _unitOfWorkProjectApprovalStepCommand;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly ProjectCreator _projectCreator;
        private readonly MapProjectWithStep _mapProjectWithStep;

        public ServiceProjectProposalCreate(IUnitOfWorkProjectProposalCommand unitOfWorkProjectProposalCommand, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger, IUnitOfWorkProjectApprovalStepCommand unitOfWorkProjectApprovalStepCommand,
                                            ProjectCreator projectCreator, MapProjectWithStep mapProjectWithStep, IUnitOfWorkProjectProposalQuery unitOfWorkProjectProposalQuery)
        {
            _unitOfWorkProjectProposalCommand = unitOfWorkProjectProposalCommand;
            _mapper = mapper;
            _logger = logger;
            _projectCreator = projectCreator;
            _mapProjectWithStep = mapProjectWithStep;
            _unitOfWorkProjectApprovalStepCommand = unitOfWorkProjectApprovalStepCommand;
            _unitOfWorkProjectProposalQuery = unitOfWorkProjectProposalQuery;
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
                await _unitOfWorkProjectProposalCommand._repositoryProjectProposalCommand.Create(entity);
                var pasos = await _projectCreator.GenerateSteps(entity);
                foreach (var paso in pasos)
                {
                    await _unitOfWorkProjectApprovalStepCommand._repositoryProjectApprovalStepCommand.Create(paso);

                }
                await _unitOfWorkProjectApprovalStepCommand.Save();
                var view = await _unitOfWorkProjectProposalQuery._repositoryProjectProposalQuery.GetById(entity.Id);
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

