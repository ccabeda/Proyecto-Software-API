namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs
{
    public record ProjectApprovalStepCreateDTO(Guid ProjectProposalId,
                                               int? ApproverUserId,
                                               int ApproverRoleId,
                                               int Status,
                                               int StepOrder,
                                               string? observations);
}
