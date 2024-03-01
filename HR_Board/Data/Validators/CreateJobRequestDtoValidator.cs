using FluentValidation;
using HR_Board.Data.ModelDTO;

namespace HR_Board.Data.Validators
{
    public class CreateJobRequestDtoValidator : AbstractValidator<CreateJobRequestDto>
    {
        public CreateJobRequestDtoValidator()
        {
            RuleFor(c => c.Title).MinimumLength(3).MaximumLength(50);
            RuleFor(c => c.ShortDescription).MinimumLength(5).MaximumLength(15);
            RuleFor(c => c.LongDescription).MinimumLength(5).MaximumLength(250);
            RuleFor(c => c.Logo).Must(IsAValidUrl).WithMessage("not a valid Url");
            RuleFor(c => c.CompanyName).MinimumLength(5).MaximumLength(50);

        }

        private bool IsAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
