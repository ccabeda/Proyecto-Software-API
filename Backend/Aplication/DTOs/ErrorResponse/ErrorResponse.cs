namespace Proyecto_Software_Individual.Aplication.DTOs
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
    }

    public class ProyectInvalidResponse : ErrorResponse
    {
        public ProyectInvalidResponse()
        {
            Message = "Datos del proyecto inválidos";
        }
        /// <example>Datos del proyecto inválidos</example>
        public new string? Message { get; set; }
    }

    public class ProjectNotFound : ErrorResponse
    {
        public ProjectNotFound()
        {
            Message = "Parámetro de consulta inválido";
        }
        /// <example>Parámetro de consulta inválido</example>
        public new string? Message { get; set; }
    }

    public class ProjectNotExist : ErrorResponse
    {
        public ProjectNotExist()
        {
            Message = "Proyecto no encontrado";
        }
        /// <example>Proyecto no encontrado</example>
        public new string? Message { get; set; }
    }

    public class ProjectNotModification : ErrorResponse
    {
        public ProjectNotModification()
        {
            Message = "El proyecto ya no se encuentra en un estado que permite modificaciones";
        }
        /// <example>El proyecto ya no se encuentra en un estado que permite modificaciones</example>
        public new string? Message { get; set; }
    }

    public class InvalidDates : ErrorResponse
    {
        public InvalidDates()
        {
            Message = "Datos de decisión inválidos";
        }
        /// <example>Datos de decisión inválidos</example>
        public new string? Message { get; set; }
    }

    public class ProyectUpdateInvalidResponse : ErrorResponse
    {
        public ProyectUpdateInvalidResponse()
        {
            Message = "Datos de actualización inválidos";
        }
        /// <example>Datos de actualización inválidos</example>
        public new string? Message { get; set; } 
    }
}