using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Shared
{
    public class SchoolDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CompanyDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

    }

    public class SkillDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class PostCategoryDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class TimeSpanDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class JobTitleDto
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
