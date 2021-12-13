using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(15)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Required]
        public string ConfirmPassword { get; set; }
        [MaxLength(15)]
        public string Role { get; set; }
        [Display(Name ="Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DoB { get; set; }
        public bool IsEmailConfirmed { get; set; }

    }
}
public enum Role
{
    Admin,
    Employee
}