using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace Laboratory_Schedule.Models
{
    public class Request
    {
        public int Id { get; set; }

        [RegularExpression(@"^[0-9]{10}$")]
        public int NationalResidenceId { get; set; }

        
        public int UniversityNumber { get; set; }

        public string StudentStatus { get; set; }
        public string Collage { get; set; }

        [StringLength(20, MinimumLength = 1)]
        public string FirstNameEng { get; set; }

        public string FatherNameEng { get; set;}

        public string GrandFatherNameEng { get; set;}

        public string FamilyNameEng { get;set;}

        [StringLength(20, MinimumLength = 1)]
        public string FirstNameArb { get; set;}

        public string FatherNameArb { get;set;}
        
        public string GrandFatherNameArb { get; set; }

        public string FamilyNameArb { get; set; }

        public string Email { get; set; }

        [RegularExpression(@"^05[0-9]{8}$", ErrorMessage = "Invalid Mobile Number.")]
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int? MedicalFileNO { get; set; }


        public DateTime TestDate { get; set; }
    }
}
