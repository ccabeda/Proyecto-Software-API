using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ProjectFilters
    {
        private readonly IUnitOfWorkProjectApprovalStepQuery _unitOfWorkProjectApprovalStepQuery;
        private readonly IUnitOfWorkUserQuery _userQuery;

        public ProjectFilters(IUnitOfWorkProjectApprovalStepQuery unitOfWorkProjectApprovalStepQuery, IUnitOfWorkUserQuery userQuery)
        {
            _unitOfWorkProjectApprovalStepQuery = unitOfWorkProjectApprovalStepQuery;
            _userQuery = userQuery;
        }

        public async Task<IQueryable<ProjectProposal>> ApplyFilters(IQueryable<ProjectProposal> query, ProjectProposalFilterDTO filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.title))
            {
                query = query.Where(p => p.Title.Contains(filters.title, StringComparison.OrdinalIgnoreCase));
            }
            if (filters.status.HasValue)
            {
                query = query.Where(p => p.Status == filters.status.Value);
            }
            if (filters.applicant.HasValue)
            {
                query = query.Where(p => p.CreateBy == filters.applicant.Value);
            }
            if (filters.approvalUser.HasValue)
            {
                var usuario = await _userQuery._repositoryUserQuery.GetById(filters.approvalUser.Value);
                if (usuario != null)
                {
                    var allSteps = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetAll();

                    var proyectosValidos = allSteps
                        .GroupBy(s => s.ProjectProposalId)
                        .Where(g => EsProyectoAprobablePorUsuario(g.OrderBy(s => s.StepOrder).ToList(), usuario.Role))
                        .Select(g => g.Key)
                        .ToHashSet();

                    query = query.Where(p => proyectosValidos.Contains(p.Id));
                }
                else
                {
                    // Usuario no existe → devolver lista vacía
                    return Enumerable.Empty<ProjectProposal>().AsQueryable();
                }
            }

            return query;
        }

        private bool EsProyectoAprobablePorUsuario(List<ProjectApprovalStep> pasos, int rolUsuario)
        {
            foreach (var paso in pasos)
            {
                if (paso.Status == 1 || paso.Status == 4) // pendiente u observado
                {
                    // Si es del rol del usuario, entonces puede aprobarlo
                    if (paso.ApproverRoleId == rolUsuario)
                        return true;

                    // Si el primer pendiente/observado no es del rol del usuario, no puede aprobar
                    return false;
                }
            }

            // No hay pasos pendientes u observados
            return false;
        }

    }
}
