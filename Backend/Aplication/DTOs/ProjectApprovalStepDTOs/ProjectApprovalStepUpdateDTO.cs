namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs
{
    public record ProjectApprovalStepUpdateDTO(long Id,
                                        Guid projectProposalId,
                                        int? ApproverUserId,
                                        int ApproverRoleId,
                                        int Status,
                                        int stepOrder,
                                        string? observations);
}
