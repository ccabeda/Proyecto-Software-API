namespace Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs
{
    public record ProjectProposalFilterDTO
    {
        /// <summary>
        /// Filtro por título del proyecto.
        /// </summary>
        /// <example>Sistema de Gestión</example>
        public string? title { get; set; }

        /// <summary>
        /// Filtro por ID del estado del proyecto.
        /// </summary>
        /// <example>1</example>
        public int? status { get; set; }

        /// <summary>
        /// Filtro por ID del solicitante (usuario que creó la solicitud).
        /// </summary>
        /// <example>2</example>
        public int? applicant { get; set; }

        /// <summary>
        /// Filtro por ID del usuario aprobador asignado a algún paso.
        /// </summary>
        /// <example>1</example>
        public int? approvalUser { get; set; }
    }
}
