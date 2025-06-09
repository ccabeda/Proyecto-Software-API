namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs
{
    public record ProjectProposalCreateDTO
    {

        /// <example>Sistema de Control de Acceso</example>
        public string? Title { get; set; }

        /// <example>Implementación de un sistema biométrico para el control de acceso al edificio</example>
        public string? Description { get; set; }

        /// <example>75000</example>
        public decimal Amount { get; set; }

        /// <example>120</example>
        public int Duration { get; set; }

        /// <example>2</example>
        public int Area { get; set; }

        /// <example>1</example>
        public int User { get; set; } 

        /// <example>1</example>
        public int Type { get; set; }
    }
}
