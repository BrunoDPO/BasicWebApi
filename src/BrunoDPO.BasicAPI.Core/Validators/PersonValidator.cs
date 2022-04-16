using BrunoDPO.BasicAPI.Core.Domain;
using FluentValidation;
using System.Data.SqlTypes;

namespace BrunoDPO.BasicAPI.Core.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name is mandatory");

            RuleFor(p => p.BirthDate)
                .InclusiveBetween(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value)
                .WithMessage("Invalid birth date");

            RuleFor(p => p.Gender)
                .IsInEnum()
                .NotEmpty()
                .WithMessage("Invalid gender");
        }
    }
}
