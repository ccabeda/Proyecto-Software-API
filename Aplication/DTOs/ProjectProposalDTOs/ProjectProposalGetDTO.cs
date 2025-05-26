namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs
{
    public record ProjectProposalGetDTO
    {
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid Id { get; set; }

        /// <example>Sistema de Gestión de Inventarios</example>
        public string? Title { get; set; } 

        /// <example>Desarrollo de un sistema integral para administrar el inventario</example>
        public string? Description { get; set; }

        /// <example>50000.00</example>
        public decimal Amount { get; set; }

        /// <example>90</example>
        public int Duration { get; set; }

        /// <example>Tecnología</example>
        public string? Area { get; set; } 

        /// <example>Pendiente</example>
        public string? Status { get; set; } 

        /// <example>Desarrollo</example>
        public string? Type { get; set; } 
    }
}

