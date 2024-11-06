using JobScial.BAL.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Profile
{
    public class UpdateProfileDto
    {
        public AccountDto Account { get; set; }
        public IEnumerable<AccountCertificateDto> Certificates { get; set; } = new List<AccountCertificateDto>();
        public IEnumerable<AccountEducationDto> Educations { get; set; } = new List<AccountEducationDto>();
        public IEnumerable<AccountExperienceDto> Experiences { get; set; } = new List<AccountExperienceDto>();
        public IEnumerable<AccountSkillDto> Skills { get; set; } = new List<AccountSkillDto>();
    }
}
