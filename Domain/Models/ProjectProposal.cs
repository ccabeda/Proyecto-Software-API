using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Software_Individual.Domain.Models
{
    public class ProjectProposal
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(255)]
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int Area { set; get; }
        [ForeignKey("Area")]
        public Area AreaNavegation { get; set; } = null!;
        public int Type { set; get; }
        [ForeignKey("Type")]
        public ProjectType ProjectType { get; set; } = null!;
        public decimal EstimatedAmount { set; get; }
        public int EstimatedDuration { set; get; }
        public int Status { set; get; }
        [ForeignKey("Status")]
        public ApprovalStatus ApprovalStatus { get; set; } = null!;
        public DateTime CreateAt { set; get; }
        public int CreateBy { set; get; }
        [ForeignKey("CreateBy")]
        public User User { get; set; } = null!;
    }
}
