namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs
{
    public record ProjectApprovalStepDesitionDTO
    {
        /// <example>1</example>
        public long Id { get; set; }

        /// <example>1</example>
        public int User { get; set; }

        /// <example>2</example>
        public int Status { get; set; }

        /// <example>Proyecto aprobado con modificaciones menores</example>
        public string? Observation { get; set; }
    }
}