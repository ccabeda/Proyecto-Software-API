using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Software_Individual.Domain.Models
{
    public class ApprovalRule
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; } //long es el equivalente a bigint
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int? Area { set; get; }
        [ForeignKey("Area")]
        public Area? AreaNavegation { get; set; }
        public int? Type { set; get; }
        [ForeignKey("Type")]
        public ProjectType? ProjectType { get; set; }
        public int StepOrder { set; get; }
        public int ApproverRoleId { set; get; }
        [ForeignKey("ApproverRoleId")]
        public ApproverRole? ApproverRole { get; set; }
    }
}
