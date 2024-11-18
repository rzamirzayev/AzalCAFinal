using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
     class AddServiceRequestDtoValidator:AbstractValidator<AddServiceRequestDto>
    {
        public AddServiceRequestDtoValidator() {
            RuleFor(m => m.Name)
                .NotNull()
                .WithMessage("Name bos ola bilmez")
                .MinimumLength(3)
                .WithMessage("Name en az 3 simvol olmalidir");
            RuleFor(m => m.Title)
                .NotNull()
                .WithMessage("Title bos ola bilmez")
                .MinimumLength(3)
                .WithMessage("Title en az 3 simvol olmalidir");
            RuleFor(m => m.Description)
    .NotNull()
    .WithMessage("Description bos ola bilmez")
    .MinimumLength(3)
    .WithMessage("Description en az 10 simvol olmalidir");


        }

    }
}
