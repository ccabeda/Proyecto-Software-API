namespace Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs
{
    public record ApproverRoleGetDTO
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Administrador</example>
        public string? Name { get; set; }
    }
}
