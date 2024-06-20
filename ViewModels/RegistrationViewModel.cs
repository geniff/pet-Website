using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    public class RegistrationViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-_]).{10,}$",
                ErrorMessage = "Пароль слишком простой")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwerty12345")
            {
                yield return new ValidationResult("Пароль слишком простой", ["Password"]);
            }

        }

    }
}
