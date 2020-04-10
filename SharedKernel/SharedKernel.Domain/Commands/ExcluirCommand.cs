using SharedKernel.Domain.Validations;

namespace SharedKernel.Domain.Commands
{
    public class ExcluirCommand : Command, ICommand
    {
        public int Id { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new ExcluirCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
