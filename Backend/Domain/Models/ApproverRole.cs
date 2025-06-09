using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Software_Individual.Domain.Models
{
    public class ApproverRole
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [MaxLength(25)]
        public required string Name { get; set; }
    }
}
