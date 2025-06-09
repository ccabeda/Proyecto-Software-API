using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Software_Individual.Domain.Models
{
    public class ProjectApprovalStep
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; } //long es el equivalente a bigint pedido
        public Guid ProjectProposalId { set; get; }
        [ForeignKey("ProjectProposalId")]
        public ProjectProposal ProjectProposal { get; set; } = null!;
        public int? ApproverUserId { set; get; }
        [ForeignKey("ApproverUserId")]
        public User? User { get; set; }
        public int ApproverRoleId { set; get; }
        [ForeignKey("ApproverRoleId")]
        public ApproverRole ApproverRole { get; set; } = null!;
        public int Status { set; get; }
        [ForeignKey("Status")]
        public ApprovalStatus ApprovalStatus { get; set; } = null!;
        public int StepOrder { set; get; }
        public DateTime? DesicionDate { set; get; }
        public string? Observations { set; get; }
    }
}
