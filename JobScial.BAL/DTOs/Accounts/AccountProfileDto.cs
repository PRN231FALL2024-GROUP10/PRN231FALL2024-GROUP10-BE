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
        public string Username { get; set; }
        // Thêm các thuộc tính khác cần thiết từ lớp Account
    }

    public class AccountCertificateDto
    {
        public int AccountId { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
    }

    public class AccountEducationDto
    {
        public int AccountId { get; set; }

        public int SchoolId { get; set; }

        public int YearStart { get; set; }

        public int? Timespan { get; set; }

        public int? TimespanUnit { get; set; }

        public string? Description { get; set; }

        public string? SchoolName { get; set; }
        public string? TimespanUnitName { get; set; }
    }

    public class AccountExperienceDto
    {
        public int AccountId { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public int YearStart { get; set; }

        public string? JobTitle { get; set; }

        public int? Timespan { get; set; }

        public int? TimespanUnit { get; set; }

        public string? Description { get; set; }
        public string? TimespanUnitName { get; set; }
    }

    public class AccountSkillDto
    {
        public int AccountId { get; set; }

        public int SkillCategoryId { get; set; }

        public int? SkillLevel { get; set; }

        public int? Timespan { get; set; }

        public int? TimespanUnit { get; set; }

        public string? Description { get; set; }
    }

    public class AccountProfileDto
    {
        public AccountDto Account { get; set; }
        public IEnumerable<AccountCertificateDto> Certificates { get; set; } = new List<AccountCertificateDto>();
        public IEnumerable<AccountEducationDto> Educations { get; set; } = new List<AccountEducationDto>();
        public IEnumerable<AccountExperienceDto> Experiences { get; set; } = new List<AccountExperienceDto>();
        public IEnumerable<AccountSkillDto> Skills { get; set; } = new List<AccountSkillDto>();
    }
}
