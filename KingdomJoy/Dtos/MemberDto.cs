using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KingdomJoy.Dtos
{
    public class MemberDto
    {


        [Required]
        public int TitleID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public byte Gender { get; set; }

        [Required]
        public byte Status { get; set; }

        public DateTime Dob { get; set; }

        public DateTime Dom { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        public byte BornAgain { get; set; }

        public byte Baptized { get; set; }

        public byte FoundationSchool { get; set; }

        public int DesignationId { get; set; }
    }


    public class MemberDataTablesDto
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Gender { get; set; }

        public string Status { get; set; }

        public string Dob { get; set; }

        public string Dom { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string BornAgain { get; set; }

        public string Baptized { get; set; }

        public string FoundationSchool { get; set; }

        public string Designation { get; set; }

        public string Edit { get; set; }
    }
}