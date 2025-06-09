using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;

namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs
{
    public record ProjectApprovalStepGetDTO
    {

        /// <example>1</example>
        public long Id { get; set; }

        /// <example>1</example>
        public int? StepOrder { get; set; }

        /// <example>null</example>
        public DateTime? DecisionDate { get; set; }

        /// <example>null</example>
        public string? Observations { get; set; }

        public UserGetDTO? ApproverUser { get; set; }

        public ApproverRoleGetDTO? ApproverRole { get; set; }

        public ApprovalStatusGetDTO? Status { get; set; }
    }
}


