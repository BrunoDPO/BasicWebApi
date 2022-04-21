using BrunoDPO.BasicAPI.Domain.Models;
using FluentValidation;
using System.Data.SqlTypes;

namespace BrunoDPO.BasicAPI.Application.Validators
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
                .WithMessage((p, v) => $"Birth date {v.ToShortDateString()} is invalid");

            RuleFor(p => p.Gender)
                .IsInEnum()
                .NotEmpty()
                .WithMessage((p, v) => $"Gender {v} is invalid");
        }
    }
}
