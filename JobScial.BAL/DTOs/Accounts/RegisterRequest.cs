using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Accounts
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public int? Gender { get; set; }

        public string? Image { get; set; }

        public string? FullName { get; set; }

        public string? FullNameSearch { get; set; }

        public DateTime? DoB { get; set; }
    }
}
