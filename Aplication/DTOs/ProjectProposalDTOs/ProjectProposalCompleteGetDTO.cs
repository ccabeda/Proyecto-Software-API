using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectTypeDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;

namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs
{
    public record ProjectProposalCompleteGetDTO
    {
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid Id { get; set; }

        /// <example>Sistema de Gestión de Inventarios</example>
        public string? title { get; set; }

        /// <example>Desarrollo de un sistema para administrar el inventario de la empresa</example>
        public string? Description { get; set; }

        /// <example>50000</example>
        public decimal? Amount { get; set; }

        /// <example>90</example>
        public int? Duration { get; set; }

        public AreaGetDTO? Area { get; set; }

        public ProjectTypeGetDTO? ProjectType { get; set; }

        public ApprovalStatusGetDTO? ApprovalStatus { get; set; }

        public UserGetDTO? User { get; set; }

        public List<ProjectApprovalStepGetDTO>? Steps { get; set; }
    }
}

