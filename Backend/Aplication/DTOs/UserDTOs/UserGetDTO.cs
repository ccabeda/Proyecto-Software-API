using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;

namespace Proyecto_Software_Individual.Aplication.DTOs.UserDTOs
{
    public record UserGetDTO
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Juan Pérez</example>
        public string? Name { get; set; }

        /// <example>juan.perez@empresa.com</example>
        public string? Email { get; set; }

        public ApproverRoleGetDTO? Role { get; set; }
    }
}
