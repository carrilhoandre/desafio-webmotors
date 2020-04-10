using FluentValidation.Results;
using Newtonsoft.Json;
using SharedKernel.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Domain.Commands
{
    public abstract class Command : ICommand
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
        public IReadOnlyCollection<Notification> Errors { get { return ValidationResult.Errors?.Select(c => new Notification(c.ErrorCode, c.ErrorMessage)).ToList(); } }

        protected Command()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
    }
}
