namespace Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs
{
    public record ApprovalStatusGetDTO
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Pendiente</example>
        public string? Name { get; set; }
    }
}
