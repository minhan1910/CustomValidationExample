using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person : IValidatableObject
    {
        [Display(Name = "Person Name")]
        [Required(ErrorMessage = "{0} can not be empty or null")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphbets, space and dot(.)")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "{0} should contain 10 digits")]
        [ValidateNever]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Password { get; set; }

        [Display(Name = "Re-enter Password")]
        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} do not match")]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price { get; set; }

        [MinimumYearValidator(2005, ErrorMessage = "Year should be larger than {1}")]
        public DateTime? DateOfBirth { get; set; }        

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "'From date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }

        public override string ToString()
        {
            return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, ConfirmPassword: {ConfirmPassword}, Price: {Price}, Age: {Age}";
        }

        /*
            Xài khi ko muốn dùng constructor hoặc reflection
            Nhưng chỉ nên dùng với các yêu cầu nhỏ và không muốn tái sử dụng lại validate nên mới dùng IValidatableObject để làm luôn trong Model Object.
         */
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!DateOfBirth.HasValue && !Age.HasValue)
            {
                // sử dụng cái nameof(Age) thì cái Age mốt đổi tên dùng shortcut đổi 1 lượt thì nó sẽ đổi luôn ở chỗ nameof chứ không phải dùng const string thì phải đổi nó thủ công
                yield return new ValidationResult("Either of Date of Birth or Age must be supplied", new[] { nameof(Age) });
            }

            // yield để trở về nhiều kết quả vd như check thêm condition bên dưới và dùng yield để trả về luôn
        }
    }
}
