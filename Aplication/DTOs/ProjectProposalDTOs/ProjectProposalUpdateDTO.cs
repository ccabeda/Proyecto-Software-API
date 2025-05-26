namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs
{
    public record ProjectProposalUpdateDTO
    {
        /// <example>Sistema de Gestión de Inventarios y Logística</example>
        public string? Title { get; set; }

        /// <example>Desarrollo de un sistema integral para administrar el inventario y la logística de la empresa</example>
        public string? Description { get; set; }

        /// <example>120</example>
        public int Duration { get; set; }
    }
}

