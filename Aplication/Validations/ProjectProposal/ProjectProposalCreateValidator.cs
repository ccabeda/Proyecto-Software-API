using FluentValidation;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;

namespace Proyecto_Software_Individual.Aplication.Validations.ProjectProposal
{
    public class ProjectProposalCreateValidator : AbstractValidator<ProjectProposalCreateDTO>
    {
        public ProjectProposalCreateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("El titulo no puede estar vacio.")
            .MaximumLength(255).WithMessage("El titulo del proyecto no puede tener más de 255 caracteres.");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Debe ingresar un número decimal.");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Debe ingresar un número entero.");
        }
    }
}
