using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Accounts
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp Account
    }

    public class AccountCertificateDto
    {
        public string CertificateName { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp AccountCertificate
    }

    public class AccountEducationDto
    {
        public string InstitutionName { get; set; }
        public string Degree { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp AccountEducation
    }

    public class AccountExperienceDto
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp AccountExperience
    }

    public class AccountSkillDto
    {
        public string SkillName { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp AccountSkill
    }

    public class AccountProfileDto
    {
        public AccountDto Account { get; set; }
/*        public IEnumerable<AccountCertificateDto> Certificates { get; set; }
        public IEnumerable<AccountEducationDto> Educations { get; set; }
        public IEnumerable<AccountExperienceDto> Experiences { get; set; }
        public IEnumerable<AccountSkillDto> Skills { get; set; }*/
    }
}
