using MaisArte.Models;
using FluentValidation;

namespace MaisArte.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Title).Must(n => ValidateStringEmpty(n)).WithMessage("O titulo não pode ser vazio!");
        }

        private bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
    }
}