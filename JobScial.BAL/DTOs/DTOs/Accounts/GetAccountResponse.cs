using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Accounts
{
    public class GetAccountResponse
    {
        [Key]
        public int AccountId { get; set; }

        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public int? Gender { get; set; }

        public int? Role { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? Image { get; set; }

        public string? FullName { get; set; }

        public string? FullNameSearch { get; set; }

        public DateTime? DoB { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsFollowing { get; set; }
    }

    public class SubAccountResponse
    {
        public int AccountId { get; set; }
        public int Id { get; set; }
    }
}

