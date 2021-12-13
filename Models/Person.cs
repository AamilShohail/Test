using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    [Table("Person")]
    public class Person
    {
		[Key]
        public int PersonId { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Address In Mumbai")]
        public string AddressInMumbai { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "PIN Code")]
        public string PINCode { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "IFS Code")]
        public string IFSCode { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Aadhar Card")]
        public string AadharCard { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Union Membership No")]
        public string UnionMembershipNo { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "PAN Card No")]
        public string PANCardNo { get; set; }
        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }
        [Display(Name = "No of Direct Dependents")]
        public int DirectDependents { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DoB { get; set; }
        public string FullName()
        {
            return FirstName + " " + LastName;
        }

    }
}
public enum Gender
{
    Male,
    Female,
    Other
}