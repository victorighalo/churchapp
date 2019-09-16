using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KingdomJoy.Models
{
    public class MembersViewModel
    {
        public Member Member { get; set; }

        public IEnumerable<Designation> Designations { get; set; }

        public IEnumerable<MemberTitle> Titles { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Member Title")]
        public int TitleID { get; set; }

        [Display(Name = "Title")]
        public virtual MemberTitle Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        public byte Gender { get; set; }

        [Display(Name = "Married status")]
        public byte Status { get; set; }

        [Display(Name = "Date of birth?")]
        public DateTime Dob { get; set; }

        [Display(Name = "Date of Marriage?")]
        public DateTime Dom { get; set; }

        [Display(Name = "Mobile")]
        public string Phone { get; set; }

        [Display(Name = "Mobile 2")]
        public string Phone2 { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        [Display(Name = "Born again?")]
        public byte BornAgain { get; set; }

        [Display(Name = "Baptized?")]
        public byte Baptized { get; set; }

        [Display(Name = "Foundation school?")]
        public byte FoundationSchool { get; set; }

        public int DesignationId { get; set; }

        public virtual Designation designation { get; set; }
    }

    public class MemberTitle
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class NewTitleViewModel
    {
        public IEnumerable<MemberTitle> MemberTitles { get; set; }
        public MemberTitle MemberTitle { get; set; }
    }

    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DesignationViewModel
    {
        public IEnumerable<Designation> Designations { get; set; }
        public Designation Designation { get; set; }
    }

    public class Designation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}