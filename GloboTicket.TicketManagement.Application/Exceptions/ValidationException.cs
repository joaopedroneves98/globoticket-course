namespace GloboTicket.TicketManagement.Application.Exceptions
{
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            this.ValidationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                this.ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
