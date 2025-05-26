using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ProjectUpdater
    {
        private readonly IUnitOfWorkProjects _unitOfWorkProjects;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;

        public ProjectUpdater(IUnitOfWorkProjects unitOfWorkProjects, IMapper mapper, ILogger<ServiceProjectProposalCreate> logger)
        {
            _unitOfWorkProjects = unitOfWorkProjects;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProjectProposal?> GetProjectOrThrow(Guid id)
        {
            var project = await _unitOfWorkProjects._repositoryProjectProposalQuery.GetById(id);
            if (project == null)
            {
                _logger.LogInformation("Proyecto {ProjectId} no encontrado", id);
                return null;
            }
            if (project.Status != 4)
            {
                _logger.LogInformation("El Proyecto de id {ProjectId} no está en estado observado", id);
                throw new InvalidOperationException("El proyecto no se encuentra en un estado que permite modificaciones.");
            }
            return project;
        }

        public async Task ValidateUpdateData(ProjectProposalUpdateDTO projectProposalDTO, Guid id)
        {
            if (string.IsNullOrWhiteSpace(projectProposalDTO.Title) || projectProposalDTO.Duration <= 0)
            {
                _logger.LogWarning("Datos inválidos para actualizar el proyecto");
                throw new ArgumentException("Datos de actualización inválidos.");
            }

            var existing = await _unitOfWorkProjects._repositoryProjectProposalQuery.GetByName(projectProposalDTO.Title!);
            if (existing != null && existing.Id != id)
            {
                _logger.LogInformation("Ya existe otro proyecto con el título '{Title}'", projectProposalDTO.Title);
                throw new ArgumentException("Ya existe otro proyecto con ese título.");
            }
        }

        public async Task SaveProject(ProjectProposalUpdateDTO projectProposalDTO, ProjectProposal project)
        {
            _mapper.Map(projectProposalDTO, project);
            await _unitOfWorkProjects._repositoryProjectProposalCommand.Update(project);
            await _unitOfWorkProjects.Save();
        }
    }
}
