using FluentValidation;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;

namespace QuestionnaireAPI.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterDtoValidator(QuestionnaireDbContext dbContext)
        {
            RuleFor(x => x.Mail).NotEmpty().EmailAddress().MinimumLength(5).MaximumLength(80);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(4);
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(60).MinimumLength(6);
            // RuleFor(x => x.UserType).Custom((value, context)=> {
            //     if(!(value.ToString() == "Admin" || value.ToString() == "User"))
            //     {

            //         context.AddFailure("UserType", "Wrong user type");
            //     }
            // });

        }
    }
}