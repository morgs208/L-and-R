using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Display(Name="First Name")]
        [Required]
        [MinLength(2, ErrorMessage="Must be at least 2 characters")]
        public string FirstName {get;set;}

        [Display(Name="Last Name")]
        [Required]
        [MinLength(2, ErrorMessage=" must be at least 4 characters")]
        public string LastName {get;set;}

        [Display(Name="Email")]
        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Display(Name="Password")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="must be at least 5 characters long")]
        public string Password {get;set;}

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string ConfirmPassword {get;set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}