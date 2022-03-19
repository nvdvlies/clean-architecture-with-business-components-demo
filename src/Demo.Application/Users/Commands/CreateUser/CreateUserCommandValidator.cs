using Demo.Application.Users.Commands.CreateUser.Dtos;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.FamilyName).NotEmpty();
            RuleFor(user => user.UserRoles).NotEmpty();
            RuleForEach(user => user.UserRoles).ChildRules(userRole =>
            {
                userRole.RuleFor(x => x.RoleId).NotEmpty();
            });
            RuleFor(user => user.UserRoles)
                .Must((userRoles) => HasUniqueRoles(userRoles))
                .WithMessage(x => "UserRoles cannot contain duplicate roles");
        }

        private static bool HasUniqueRoles(IEnumerable<CreateUserCommandUserRole> userRoles)
        {
            return userRoles.GroupBy(x => x.RoleId).Count() == userRoles.Count();
        }
    }
}